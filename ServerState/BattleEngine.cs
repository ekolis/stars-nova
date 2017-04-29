#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.Server
{
    using System;    
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Nova.Server;

    /// <summary>
    /// Deal with combat between races.
    /// </summary>
    public class BattleEngine
    {
        private readonly Random random = new Random();
        private readonly int movementPhasesPerRound = 3;
        private readonly int maxBattleRounds = 16;
        // The above table as a 2d lookup. Note round 8 moved to the first postion as we use battleRound % 8.
        private readonly int[,] MovementTable = new int[,] 
        {
            {
                0, 1, 0, 1, 0, 1, 0, 1
            },
            {
                1, 1, 1, 0, 1, 1, 1, 0
            },
            {
                1, 1, 1, 1, 1, 1, 1, 1
            },
            {
                1, 2, 1, 1, 1, 2, 1, 1
            },
            {
                1, 2, 1, 2, 1, 2, 1, 2
            },
            {
                2, 2, 2, 1, 2, 2, 2, 1
            },
            {
                2, 2, 2, 2, 2, 2, 2, 2
            },
            {
                2, 3, 2, 2, 2, 3, 2, 2
            },
            {
                2, 3, 2, 3, 2, 3, 2, 3
            }
        };


        private ServerData serverState;
        private BattleReport battle;

        /// <summary>
        /// Used to generate fleet id numbers for battle stacks.
        /// </summary>
        private uint stackId;

        private int battleRound = 0;

        /// <summary>
        /// Creates a new battle engine.
        /// </summary>
        /// <param name="serverState">
        /// A <see cref="ServerState"/> which holds the state of the game.
        /// </param>
        /// <param name="battleReport">
        /// A <see cref="BattleReport"/> onto which to write the battle results.
        /// </param>
        public BattleEngine(ServerData serverState, BattleReport battleReport)
        {
            this.serverState = serverState;
            this.battle = battleReport;
        }

        /// <summary>
        /// Deal with any fleet battles. How the battle engine in Stars! works is
        /// documented in the Stars! FAQ (a copy is included in the documentation).
        /// </summary>
        public void Run()
        {
            // Determine the positions of any potential battles. For a battle to
            // take place 2 or more fleets must be at the same location.

            List<List<Fleet>> potentialBattles = DetermineCoLocatedFleets();

            // If there are no co-located fleets then there are no fleets at all
            // so there is nothing more to do so we can give up here.

            if (potentialBattles.Count == 0)
            {
                return;
            }

            // Eliminate potential battle locations where there is only one race
            // present.

            List<List<Fleet>> engagements = EliminateSingleRaces(potentialBattles);

            // Again this could result in an empty array. If so, give up here.

            if (engagements.Count == 0)
            {
                return;
            }

            // We now have a list of every collection of fleets of more than one
            // race at the same location. Run through each possible combat zone,
            // build the fleet stacks and invoke the battle at each location
            // between any enemies.

            foreach (List<Fleet> battlingFleets in engagements)
            {
                List<Stack> battlingStacks = GenerateStacks(battlingFleets);

                // If no targets get selected (for whatever reason) then there is
                // no battle so we can give up here.

                if (SelectTargets(battlingStacks) == 0)
                {
                    return;
                }

                stackId = 0;
                Fleet sample = battlingFleets.First() as Fleet;

                if (sample.InOrbit != null)
                {
                    battle.Location = sample.InOrbit.Name;
                }
                else
                {
                    battle.Location = "coordinates " + sample.Position.ToString();
                }

                PositionStacks(battlingStacks);

                // Copy the full list of stacks into the battle report. We need a
                // full list to start with as the list in the battle engine will
                // get depleted during the battle and may not (and most likely will
                // not) be fully populated by the time we Serialize the
                // report. Ensure we take a copy at this point as the "real" stack
                // will mutate as processing proceeds and even ships may vanish.
                                
                foreach (Stack stack in battlingStacks)
                {
                    battle.Stacks[stack.Key] = new Stack(stack);
                }

                DoBattle(battlingStacks);
                
                ReportBattle();
            }
        }

        /// <summary>
        /// Determine the positions of any potential battles where the number of fleets
        /// is more than one (this scan could be more efficient but this is easier to
        /// read). 
        /// </summary>
        /// <returns>A list of all lists of co-located fleets.</returns>
        public List<List<Fleet>> DetermineCoLocatedFleets()
        {
            List<List<Fleet>> allColocatedFleets = new List<List<Fleet>>();
            Dictionary<long, bool> fleetDone = new Dictionary<long, bool>();
            
            foreach (Fleet fleetA in serverState.IterateAllFleets())
            {
                if (fleetDone.ContainsKey(fleetA.Key))
                {
                    continue;
                }

                List<Fleet> coLocatedFleets = new List<Fleet>();

                foreach (Fleet fleetB in serverState.IterateAllFleets())
                {
                    if (fleetB.Position != fleetA.Position)
                    {
                        continue;
                    }

                    coLocatedFleets.Add(fleetB);
                    fleetDone[fleetB.Key] = true;
                }

                if (coLocatedFleets.Count > 1)
                {
                    allColocatedFleets.Add(coLocatedFleets);
                }
            }
            
            return allColocatedFleets;
        }

        /// <summary>
        /// Eliminate single race groupings. Note that we know there must be at least
        /// two fleets when we determined co-located fleets earlier.
        /// </summary>
        /// <param name="fleetPositions">A list of all lists of co-located fleets.</param>
        /// <returns>The positions of all potential battles.</returns>
        public List<List<Fleet>> EliminateSingleRaces(List<List<Fleet>> allColocatedFleets)
        {
            List<List<Fleet>> allEngagements = new List<List<Fleet>>();

            foreach (List<Fleet> coLocatedFleets in allColocatedFleets)
            {
                Dictionary<int, bool> empires = new Dictionary<int, bool>();

                foreach (Fleet fleet in coLocatedFleets)
                {
                    empires[fleet.Owner] = true;
                }

                if (empires.Count > 1)
                {
                    allEngagements.Add(coLocatedFleets);
                }
            }
            return allEngagements;
        }

        /// <summary>
        /// Extract a list of stacks from a fleet; each ship design present on
        /// the fleet will form a distinct stack. If multiple fleets are present there may be multiple
        /// stacks of the same design present at the battle. (Or if stack ship limits are exceeded
        /// by a single fleet).
        /// </summary>
        /// <param name="fleet">The <see cref="Fleet"/> to be converted to stacks.</param>
        /// <returns>A list of stacks extracted from the fleet.</returns>
        public List<Stack> BuildFleetStacks(Fleet fleet)
        {
            List<Stack> stackList = new List<Stack>();
            
            Stack newStack = null;
            
            foreach (ShipToken token in fleet.Composition.Values)
            {             
                newStack = new Stack(fleet, stackId, token);
                
                stackList.Add(newStack);
                
                stackId++;                
            }

            // Note that each of this Stacks has it's Key UNIQUE within this battle (separate from the
            // Fleet's key),
            // consisting of Owner + stackId. The token inside is still Keyed by design.Key.
            return stackList;
        }

        /// <summary>
        /// Run through all of the fleets in an engagement and convert them to stacks of the
        /// same ship design and battle plan. We will return a complete list of all stacks at
        /// this engagement location.
        /// </summary>
        /// <param name="coLocatedFleets">A list of fleets at the given location.</param>
        /// <returns>A list of Fleets representing all stack in the engagement (1 fleet per unique stack).</returns>
        public List<Stack> GenerateStacks(List<Fleet> coLocatedFleets)
        {
            List<Stack> battlingStacks = new List<Stack>();

            foreach (Fleet fleet in coLocatedFleets)
            {
                List<Stack> fleetStacks = BuildFleetStacks(fleet);

                foreach (Stack stack in fleetStacks)
                {
                    battlingStacks.Add(stack);
                }
            }

            return battlingStacks;
        }

        /// <summary>
        /// Set the initial position of all of the stacks.
        /// </summary>
        /// <param name="battlingStacks">All stacks in this battle.</param>
        public void PositionStacks(List<Stack> battlingStacks)
        {
            Dictionary<int, int> empires = new Dictionary<int, int>();
            Dictionary<int, Point> racePositions = new Dictionary<int, Point>();

            foreach (Stack stack in battlingStacks)
            {
                empires[stack.Owner] = stack.Owner;
            }

            SpaceAllocator spaceAllocator = new SpaceAllocator(empires.Count);

            // Ensure that we allocate enough space so that all race stacks are
            // out of weapons range (scaled).

            int spaceSize = spaceAllocator.GridAxisCount * Global.MaxWeaponRange;

            // spaceAllocator.AllocateSpace(spaceSize);
            spaceAllocator.AllocateSpace(10); // Set to the standard Stars! battle board size - Dan 26 Jun 11
            battle.SpaceSize = spaceSize;

            // Now allocate a position for each race in the centre of one of the
            // allocated spacial chunks.

            foreach (int empireId in empires.Values)
            {
                Rectangle newPosition = spaceAllocator.GetBox();
                Point position = new Point();

                position.X = newPosition.X + (newPosition.Width / 2);
                position.Y = newPosition.Y + (newPosition.Height / 2);

                racePositions[empireId] = position;
                battle.Losses[empireId] = 0;
            }

            // Place all stacks belonging to the same race at the same position.

            foreach (Stack stack in battlingStacks)
            {
                stack.Position = racePositions[stack.Owner];
            }
            
            // Update the known designs of enemy ships.
            foreach (int empireId in empires.Values)
            {
                foreach (Stack stack in battlingStacks)
                {
                    if (stack.Owner != empireId)
                    {
                        foreach (ShipToken token in stack.Composition.Values)
                        {
                            if (serverState.AllEmpires[empireId].EmpireReports[stack.Owner].Designs.ContainsKey(token.Design.Key))
                            {
                                serverState.AllEmpires[empireId].EmpireReports[stack.Owner].Designs[token.Design.Key] = token.Design;
                            }
                            else
                            {
                                serverState.AllEmpires[empireId].EmpireReports[stack.Owner].Designs.Add(token.Design.Key, token.Design);
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deal with a battle. This function will execute until all target fleets are
        /// destroyed or a pre-set maximum time has elapsed.   
        /// </summary>
        /// <param name="battlingStacks">All stacks in this battle.</param>
        public void DoBattle(List<Stack> battlingStacks)
        {
            battleRound = 1;
            for (battleRound = 1; battleRound <= maxBattleRounds; ++battleRound)
            {
                if (SelectTargets(battlingStacks) == 0)
                {
                    // no more targets
                    break;
                }

                MoveStacks(battlingStacks);
                
                List<WeaponDetails> allAttacks = GenerateAttacks(battlingStacks);
                
                foreach (WeaponDetails attack in allAttacks)
                {
                    ProcessAttack(attack);
                }
            }
        }

        /// <summary>
        /// Select targets (if any). Targets are set on a stack-by-stack basis.
        /// </summary>
        /// <param name="battlingStacks">All stacks in this battle.</param>
        /// <returns>The number of targeted stacks.</returns>
        public int SelectTargets(List<Stack> battlingStacks)
        {
            int numberOfTargets = 0;
            
            foreach (Stack wolf in battlingStacks)
            {
                wolf.Target = null;

                if (wolf.IsArmed == false)
                {
                    continue;
                }

                double maxAttractiveness = 0;

                foreach (Stack lamb in battlingStacks)
                {
                    if (AreEnemies(wolf, lamb))
                    {
                        double attractiveness = GetAttractiveness(lamb);
                        if (attractiveness > maxAttractiveness)
                        {
                            wolf.Target = lamb;
                            maxAttractiveness = attractiveness;
                        }
                    }
                }
                
                if (wolf.Target != null)
                {
                    numberOfTargets++;
                }
            }

            return numberOfTargets;
        }

        /// <summary>
        /// Determine how attractive a fleet is to attack.
        /// </summary>
        /// <param name="target">A stack.</param>
        /// <returns>A measure of attractiveness.</returns>
        /// FIXME (priority 3) - Implement the Stars! attractiveness model (and possibly others as options). Provide a reference to the source of the algorithm.
        public double GetAttractiveness(Stack target)
        {
            if (target == null || target.IsDestroyed) return 0;

            double cost = target.Mass + target.TotalCost.Energy;
            double dp = target.Defenses;

            return cost / dp;
        }

        /// <summary>
        /// Determine if one stack is a potential target of the other. This depends not
        /// just on the relation (friend, enemy, etc.) but also on the battle plan of
        /// the "wolf" stack (e.g. attack everyone, attack enemies, etc.).
        /// </summary>
        /// <param name="wolf">Potential attacker.</param>
        /// <param name="lamb">Potential target.</param>
        /// <returns>True if lamb is a valid target for wolf.</returns>
        public bool AreEnemies(Fleet wolf, Fleet lamb)
        {
            if (wolf.Owner == lamb.Owner)
            {
                return false;
            }

            EmpireData wolfData = serverState.AllEmpires[wolf.Owner];
            PlayerRelation lambRelation = wolfData.EmpireReports[lamb.Owner].Relation;

            BattlePlan battlePlan = wolfData.BattlePlans[wolf.BattlePlan];

            if (battlePlan.Attack == "Everyone")
            {
                return true;
            }
            else if (battlePlan.TargetId == lamb.Owner)
            {
                return true;
            }
            else if (battlePlan.Attack == "Enemies" && lambRelation == PlayerRelation.Enemy)
            {
                return true;
            }

            return false;
        }

         /// <summary>
        /// Move stacks towards their targets (if any). Record each movement in the
        /// battle report.
        /// </summary>
        /// <param name="battlingStacks">All stacks in the battle.</param>
        public void MoveStacks(List<Stack> battlingStacks)
        {
            // Movement in Squares per Round
            //                  Round
            // Movement  1  2  3  4  5  6  7  8
            // 1/2       1  0  1  0  1  0  1  0
            // 3/4       1  1  0  1  1  1  0  1
            // 1         1  1  1  1  1  1  1  1
            // 1 1/4     2  1  1  1  2  1  1  1
            // 1 1/2     2  1  2  1  2  1  2  1
            // 1 3/4     2  2  1  2  2  2  1  2
            // 2         2  2  2  2  2  2  2  2
            // 2 1/4     3  2  2  2  3  2  2  2
            // 2 1/2     3  2  3  2  3  2  3  2
            // repeats for rounds 9 - 16

            // In Stars! each round breaks movement into 3 phases.
            // Phase 1: All stacks that can move 3 squares this round get to move 1 square.
            // Phase 2: All stacks that can move 2 or more squares this round get to move 1 square.
            // Phase 3: All stacks that can move this round get to move 1 square.
            // TODO (priority 3) - verify that a ship should be able to move 1 square per phase if it has 3 move points, or is it limited to 1 per turn?
            for (var phase = 1; phase <= movementPhasesPerRound; phase++)
            {
                // TODO (priority 5) - Move in order of ship mass, juggle by 15%
                foreach (Stack stack in battlingStacks)
                {
                    if (stack.Target != null & !stack.IsStarbase)
                    {
                        NovaPoint from = stack.Position;
                        NovaPoint to = stack.Target.Position;

                        int movesThisRound = 1; // FIXME (priority 6) - kludge until I implement the above table 
                        if (stack.BattleSpeed <= 0.5)
                        {
                            movesThisRound = MovementTable[0, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 0.75)
                        {
                            movesThisRound = MovementTable[1, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 1.0)
                        {
                            movesThisRound = MovementTable[2, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 1.25)
                        {
                            movesThisRound = MovementTable[3, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 1.5)
                        {
                            movesThisRound = MovementTable[4, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 1.75)
                        {
                            movesThisRound = MovementTable[5, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 2.0)
                        {
                            movesThisRound = MovementTable[6, battleRound % 8];
                        }
                        else if (stack.BattleSpeed <= 2.25)
                        {
                            movesThisRound = MovementTable[7, battleRound % 8];
                        }
                        else 
                        {
                            // stack.BattleSpeed > 2.25
                            movesThisRound = MovementTable[8, battleRound % 8];
                        }

                        bool moveThisPhase = true;
                        switch (phase)
                        {
                            case 1:
                                {
                                    moveThisPhase = movesThisRound == 3;
                                    break;
                                }
                            case 2:
                                {
                                    moveThisPhase = movesThisRound >= 2;
                                    break;
                                }
                            case 3:
                                {
                                    moveThisPhase = movesThisRound >= 1;
                                    break;
                                }
                        }

                        // stack can move only after accumulating at least 1 move point, and after doing so expends that 1 move point
                        if (moveThisPhase)
                        {
                            stack.Position = PointUtilities.BattleMoveTo(from, to);

                            // Update the battle report with these movements.
                            BattleStepMovement report = new BattleStepMovement();
                            report.StackKey = stack.Key;
                            report.Position = stack.Position;
                            battle.Steps.Add(report);
                        }
                    }
                    // TODO (priority 7) - shouldn't stacks without targets flee the battle if their strategy says to do so? they're sitting ducks now!
                }
            }
        }

        /// <summary>
        /// Fire weapons at selected targets.
        /// </summary>
        /// <param name="battlingStacks">All stacks in the battle.</param>
        private List<WeaponDetails> GenerateAttacks(List<Stack> battlingStacks)
        {
            // First, identify all of the weapons and their characteristics for
            // every ship stack present at the battle and who they are pointed at.

            List<WeaponDetails> allAttacks = new List<WeaponDetails>();

            foreach (Stack stack in battlingStacks)
            {
                if (!stack.IsDestroyed) // skip destroyed stacks
                {
                    // generate an attack for each weapon slot in the Design (all ships in the Token fire weapons in the same slot at the same time)
                    foreach (Weapon weaponSystem in stack.Token.Design.Weapons)
                    {
                        WeaponDetails weapon = new WeaponDetails();

                        weapon.SourceStack = stack;
                        weapon.TargetStack = stack.Target;
                        weapon.Weapon = weaponSystem;

                        allAttacks.Add(weapon);
                    }
                }
            }
            
            // Sort the weapon list according to weapon system initiative.
            allAttacks.Sort();
            
            return allAttacks;
        }

        /// <summary>
        /// Attempt an attack.
        /// </summary>
        /// <param name="allAttacks">A list of WeaponDetails representing a round of attacks.</param>
        private bool ProcessAttack(WeaponDetails attack)
        {     
            // First, check that the target stack we originally identified has not
            // been destroyed (actually, the stack still exists at this point but
            // it may have no ship tokens left). In which case, don't bother trying to
            // fire this weapon system (we'll wait until the next battle clock
            // "tick" and re-target then).    
            if (attack.TargetStack == null || attack.TargetStack.IsDestroyed) 
            {
                return false;
            }

            if (attack.SourceStack == null || attack.SourceStack.IsDestroyed) 
            {
                // Report.Error("attacking stack no longer exists");
                return false;
            }

            // If the target stack is not within the range of this weapon system
            // then there is no point in trying to fire it.       
            if (PointUtilities.Distance(attack.SourceStack.Position, attack.TargetStack.Position) > attack.Weapon.Range)
            {
                return false;
            }

            // Target is valid; execute attack. 
            ExecuteAttack(attack);
            
            return true;
        }

        /// <summary>
        /// DischargeWeapon. We know the weapon and we know the target stack so attack.
        /// </summary>
        /// <param name="ship">The firing stack.</param>
        /// <param name="details">The weapon being fired.</param>
        /// <param name="target">The target stack.</param>
        private void ExecuteAttack(WeaponDetails attack)
        {
            // the two stacks involved in the attack          
            Stack attacker = attack.SourceStack;
            Stack target = attack.TargetStack;
            
            // Report on the targeting.
            BattleStepTarget report = new BattleStepTarget();
            report.StackKey = attack.SourceStack.Key;
            report.TargetKey = attack.TargetStack.Key;
            battle.Steps.Add(report);

            // Identify the attack parameters that have to take into account
            // factors other than the base values (e.g. jammers, capacitors, etc.)
            double hitPower = CalculateWeaponPower(attacker.Token.Design, attack.Weapon, target.Token.Design);
            double accuracy = CalculateWeaponAccuracy(attacker.Token.Design, attack.Weapon, target.Token.Design);

            if (attack.Weapon.IsMissile)
            {
                FireMissile(attacker, target, hitPower, accuracy);
            }
            else
            {
                FireBeam(attacker, target, hitPower);
            }

            // If we still have some Armor then the stack hasn't been destroyed
            // yet so this is the end of this shot.

            // FIXME (Priority 7) What about losses of a single ship within the token???
            if (target.Token.Armor <= 0) 
            {
                DestroyStack(target);
            }
        }

        /// <summary>
        /// All Defenses are gone. Remove the stack from the battle (which
        /// exists only during the battle) and, more importantly, remove the
        /// token from its "real" fleet. Also, generate a "destroy" event to
        /// update the battle visualisation display.
        /// </summary>
        /// <param name="target"></param>
        private void DestroyStack(Stack target)
        {
            // report the losses
            battle.Losses[target.Owner] = battle.Losses[target.Owner] + target.Token.Quantity; 

            // for the battle viewer / report
            BattleStepDestroy destroy = new BattleStepDestroy();
            destroy.StackKey = target.Key;
            battle.Steps.Add(destroy);

            // remove the Token from the Fleet, if it exists
            if (serverState.AllEmpires[target.Owner].OwnedFleets.ContainsKey(target.ParentKey))
            {
                serverState.AllEmpires[target.Owner].OwnedFleets[target.ParentKey].Composition.Remove(target.Token.Key); // remove the token from the fleet

                if (serverState.AllEmpires[target.Owner].OwnedFleets[target.ParentKey].Composition.Count == 0) // remove the fleet if no more tokens
                {
                    serverState.AllEmpires[target.Owner].OwnedFleets.Remove(target.ParentKey);
                    serverState.AllEmpires[target.Owner].FleetReports.Remove(target.ParentKey);
                }
            }

            // remove the token from the Stack (do this last so target.Token remains valid above)
            target.Composition.Remove(target.Key);
        }

        /// <summary>
        /// Do beam weapon damage.
        /// </summary>
        /// <param name="attacker">Token firing the beam</param>
        /// <param name="target">Weapon target.</param>
        /// <param name="hitPower">Damage done by the weapon.</param>
        private void FireBeam(Stack attacker, Stack target, double hitPower)
        {
            // First we have to take down the shields of the target ship. If
            // there is any power left over from firing this weapon system at the
            // shields then it will carry forward to attack Armor. If all we have
            // done is weaken the shields then that is the end of this shot.

            hitPower = DamageShields(attacker, target, hitPower);

            if (target.Token.Shields > 0 || hitPower <= 0)
            {
                return;
            }

            DamageArmor(attacker, target, hitPower);

            // TODO (Priority 6) - beam weapon overkill can hit other staks (up to one stack per ship in the attacking stack)
        }

        /// <summary>
        /// Fire a missile weapon system.
        /// </summary>
        /// <param name="attacker">token firing the missile</param>
        /// <param name="target">Missile weapon target.</param>
        /// <param name="hitPower">Damage the weapon can do.</param>
        /// <param name="accuracy">Missile accuracy.</param>
        /// <remarks>
        /// FIXME (priority 3) - Missile accuracy is not calculated this way in Stars! The effect of computers and jammers must be considered at the same time.
        /// </remarks>
        private void FireMissile(Stack attacker, Stack target, double hitPower, double accuracy)
        {
            // First, determine if this missile is going to hit or miss (based on
            // it's accuracy. 
            // FIXME (priority 4) - This algorithm for determining hit or miss is crude. We need a better one.

            int probability = random.Next(0, 100);

            if (accuracy >= probability)
            {      // A hit
                double shieldsHit = hitPower / 2;

                double armorHit = (hitPower / 2) + DamageShields(attacker, target, shieldsHit); // FIXME (Priority 5) - do double damage if it is a capital ship missile and all shields have been depleted.
                DamageArmor(attacker, target, armorHit);
            }
            else
            {                              // A miss
                double minDamage = hitPower / 8;
                DamageShields(attacker, target, minDamage);
            }

        }

        /// <summary>
        /// Attack the shields.
        /// </summary>
        /// <param name="attacker">token firing a weapon</param>
        /// <param name="target">Ship being fired on.</param>
        /// <param name="hitPower">Damage output of the weapon.</param>
        /// <returns>Residual damage after shields or zero.</returns>
        private double DamageShields(Stack attacker, Stack target, double hitPower)
        {
            if (target.Token.Shields <= 0)
            {
                return hitPower;
            }

            double initialShields = target.Token.Shields;
            target.Token.Shields -= hitPower;

            if (target.Token.Shields < 0)
            {
                target.Token.Shields = 0;
            }

            // Calculate remianing weapon power, after damaging shields (if any)
            double damageDone = (initialShields - target.Token.Shields);
            double remainingPower = hitPower - damageDone;
            
            BattleStepWeapons battleStepReport = new BattleStepWeapons();
            battleStepReport.Damage = damageDone;
            battleStepReport.Targeting = BattleStepWeapons.TokenDefence.Shields;
            battleStepReport.WeaponTarget.StackKey = attacker.Key; 
            battleStepReport.WeaponTarget.TargetKey = target.Key;

            battle.Steps.Add(battleStepReport);

            return remainingPower;
        }

        /// <summary>
        /// Attack the Armor.
        /// </summary>
        /// <param name="attacker">token making the attack</param>
        /// <param name="target">Target being fired on.</param>
        /// <param name="hitPower">Weapon damage.</param>
        private void DamageArmor(Stack attacker, Stack target, double hitPower)
        {
            // FIXME (Priority 6) - damage is being spread over all ships in the stack. Should destroy whole ships first, then spread remaining damage.
            target.Token.Armor -= (int) hitPower;

            BattleStepWeapons battleStepReport = new BattleStepWeapons();
            battleStepReport.Damage = hitPower;
            battleStepReport.Targeting = BattleStepWeapons.TokenDefence.Armor;
            battleStepReport.WeaponTarget.StackKey = attacker.Key;
            battleStepReport.WeaponTarget.TargetKey = target.Key;
            battle.Steps.Add(battleStepReport);
        }

        /// <summary>
        /// Calculate weapon power. For beam weapons, this damage will dissipate over
        /// the range of the beam (no dissipation at range 0, 5% dissipation at range 1,
        /// 10% dissipation at range 2 and 15% at range 3). Also capacitors and
        /// deflectors will modify the weapon power.
        ///
        /// For missiles, the power is simply the base power.
        /// </summary>
        /// <param name="ship">Firing ship.</param>
        /// <param name="weapon">Firing weapon.</param>
        /// <param name="target">Ship being fired on.</param>
        /// <returns>Damage weapon is able to do.</returns>
        private double CalculateWeaponPower(ShipDesign ship, Weapon weapon, ShipDesign target)
        {
            // TODO (priority 5) Stub - just return the base power of weapon. Also need to comment the return value of this function with what defenses have been considered by this (when done).
            return weapon.Power;
            /*
           double weaponPower = weapon.GetPower(ship);

           if (weapon.WeaponType == "Beam") {
              weaponPower -= Math.Pow(0.9, target.Design.BeamDeflectors);

              switch (weapon.Range) {
              case 1:
                 weaponPower *= 0.95;             // 5% reduction
                 break;
              case 2:
                 weaponPower *= 0.9;              // 10% reduction
                 break;
              case 3:
                 weaponPower *= 0.85;             // 15% reduction
                 break;
              default:
                 Report.Error("Unexpected beam range");
                 break;
              }
           }

           return weaponPower;
             * */
        }

        /// <summary>
        /// Calculate weapon accuracy. For beam weapons, this is 100%.
        ///
        /// For missiles, the chance to hit is based on the base accuracy, the computers
        /// on the ship and the enemy jammers.
        /// </summary>
        /// <param name="ship">Attacking ship.</param>
        /// <param name="weapon">Firing weapon.</param>
        /// <param name="target">Ship being fired on.</param>
        /// <returns>Chance that weapon will hit.</returns>
        private double CalculateWeaponAccuracy(ShipDesign ship, Weapon weapon, ShipDesign target)
        {
            double weaponAccuracy = weapon.Accuracy;

            if (weapon.IsMissile)
            {
                // TODO (priority 6) - computers and jammer stuff needs to go here *************
            }

            return weaponAccuracy;
        }

        /// <summary>
        /// Report the battle and losses to each player.
        /// </summary>
        private void ReportBattle()
        {
            foreach (int empire in battle.Losses.Keys)
            {
                Message message = new Message(
                    empire,
                    "There was a battle at " + battle.Location + "\r\n",
                    "BattleReport",
                    battle);

                if (battle.Losses[empire] == 0)
                {
                    message.Text += "None of your ships were destroyed";
                }
                else
                {
                    message.Text += battle.Losses[empire].ToString(System.Globalization.CultureInfo.InvariantCulture) +
                       " of your ships were destroyed";
                }

                serverState.AllMessages.Add(message);
                
                serverState.AllEmpires[empire].BattleReports.Add(battle);
            }
        }
    }
}

