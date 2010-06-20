#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
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

#region Module Description
// ===========================================================================
// Deal with combat between races.
// ===========================================================================
#endregion

using Nova.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class to process conflicts.
    /// </summary>
    public static class BattleEngine
    {
        private static ServerState StateData = ServerState.Data;
        private static double MaxBattleTime = 16;
        private static BattleReport Battle = new BattleReport();
        private static int StackID;
        private static Random RandomNumber = new Random();

        /// Residual fractional movement points left over between phases/turns of combat.
        private static IDictionary<Fleet, double> residualMovement = new Dictionary<Fleet, double>();


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Deal with any fleet battles. How the battle engine in Stars! works is
        /// documented in the Stars! FAQ (a copy is included in the documentation).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void Run()
        {
            // Determine the positions of any potential battles. For a battle to
            // take place 2 or more fleets must be at the same location.

            ArrayList fleetPositions = DetermineCoLocatedFleets();

            // If there are no co-located fleets then there are no fleets at all
            // so there is nothing more to do so we can give up here.

            if (fleetPositions.Count == 0)
            {
                return;
            }

            // Eliminate potential battle locations where there is only one race
            // present.

            ArrayList battlePositions = EliminateSingleRaces(fleetPositions);

            // Again this could result in an empty array. If so, give up here.

            if (battlePositions.Count == 0)
            {
                return;
            }

            // We now have a list of every collection of fleets of more than one
            // race at the same location. Run through each possible combat zone,
            // build the fleet stacks and invoke the battle at each location
            // between any enemies.

            foreach (ArrayList combatZone in battlePositions)
            {
                ArrayList zoneStacks = GenerateStacks(combatZone);

                // If no targets get selected (for whatever reason) then there is
                // no battle so we can give up here.

                if (SelectTargets(zoneStacks) == 0)
                {
                    return;
                }

                Battle = new BattleReport();
                StackID = 0;
                Fleet sample = combatZone[0] as Fleet;

                if (sample.InOrbit != null)
                {
                    Battle.Location = sample.InOrbit.Name;
                }
                else
                {
                    Battle.Location = "coordinates " + sample.Position.ToString();
                }

                PositionStacks(zoneStacks);

                // Copy the full list of stacks into the battle report. We need a
                // full list to start with as the list in the battle engine will
                // get depleted during the battle and may not (and most likely will
                // not) be fully populated by the time we serialise the
                // report. Ensure we take a copy at this point as the "real" stack
                // will mutate as processing proceeds and even ships may vanish.

                foreach (Fleet stack in zoneStacks)
                {
                    Battle.Stacks[stack.Name] = new Fleet(stack);
                }

                DoBattle(zoneStacks);
                ReportLosses();

                ServerState.Data.AllBattles.Add(Battle);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine the positions of any potential battles where the number of fleets
        /// is more than one (this scan could be more efficient but this is easier to
        /// read). 
        /// </summary>
        /// <returns>A list of all lists of co-located fleets as an ArrayList of ArrayLists of Fleets.</returns>
        /// ----------------------------------------------------------------------------
        public static ArrayList DetermineCoLocatedFleets()
        {
            ArrayList allFleetPositions = new ArrayList();
            Hashtable fleetDone = new Hashtable();

            foreach (Fleet fleetA in StateData.AllFleets.Values)
            {

                if (fleetDone.Contains(fleetA.Name) == false)
                {
                    ArrayList coLocatedFleets = new ArrayList();

                    foreach (Fleet fleetB in StateData.AllFleets.Values)
                    {
                        if (fleetB.Position == fleetA.Position)
                        {
                            coLocatedFleets.Add(fleetB);
                            fleetDone[fleetB.Name] = true;
                        }
                    }

                    if (coLocatedFleets.Count > 1)
                    {
                        allFleetPositions.Add(coLocatedFleets);
                    }
                }
            }
            return allFleetPositions;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Eliminate single race groupings. Note that we know there must be at least
        /// two fleets when we determined co-located fleets earlier.
        /// </summary>
        /// <param name="fleetPositions">A list of all lists of co-located fleets as an ArrayList of ArrayLists of Fleets.</param>
        /// <returns>The positions of all potential battles as an ArrayList (battlePositions) of ArrayLists (coLocatedFleets) of Fleets.</returns>
        /// ----------------------------------------------------------------------------
        public static ArrayList EliminateSingleRaces(ArrayList fleetPositions)
        {
            ArrayList battlePositions = new ArrayList();

            foreach (ArrayList coLocatedFleets in fleetPositions)
            {
                Hashtable races = new Hashtable();

                foreach (Fleet fleet in coLocatedFleets)
                {
                    races[fleet.Owner] = true;
                }

                if (races.Count > 1)
                {
                    battlePositions.Add(coLocatedFleets);
                }
            }
            return battlePositions;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Each ship present at the battle will form part of a token (AKA a stack), it
        /// is possible to have a token comprised of just a single ship. Tokens are
        /// always of ships of the same design. Each ship design in each fleet will
        /// create a token (i.e. if multiple fleets are present there may be multiple
        /// tokens of the same design present at the battle).
        /// </summary>
        /// <param name="fleet">The <see cref="Fleet"/> to be converted to token stacks.</param>
        /// <returns>A list of stacks as an ArrayList of <see cref="Fleet"/>s.</returns>
        /// ----------------------------------------------------------------------------
        public static ArrayList BuildFleetStacks(Fleet fleet)
        {
            Hashtable fleetStacks = new Hashtable();

            foreach (Ship ship in fleet.FleetShips)
            {
                ship.Cost = ship.DesignCost;
                Fleet stack = fleetStacks[ship.DesignName] as Fleet;

                // If no stack exists for this design then create one now.

                if (stack == null)
                {
                    string name = "Stack #" + StackID.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    StackID++;
                    stack = new Fleet(name, fleet.Owner, fleet.Position);

                    stack.BattlePlan = fleet.BattlePlan;
                    stack.BattleSpeed = ship.BattleSpeed;

                    fleetStacks[ship.DesignName] = stack;
                }

                // Add this ship into the stack but give each ship a name (normally
                // ships don't have names, only fleets do) based on its source
                // stack.

                ship.Name = fleet.Name;
                stack.FleetShips.Add(ship);
            }

            // Convert the hashtable into an ArrayList 
            // TODO (priority 4) - historical and could be removed

            ArrayList stackList = new ArrayList();

            foreach (Fleet thisFleet in fleetStacks.Values)
            {
                stackList.Add(thisFleet);
            }

            return stackList;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Run through all of the fleets and convert them to stacks of the same ship
        /// design and battle plan. We will return a complete list of all stacks at
        /// this battle location.
        /// </summary>
        /// <param name="coLocatedFleets">A list of fleets at the given location (as an ArrayList of Fleet).</param>
        /// <returns>A list of token stacks representing the given fleets (as an ArrayList of Fleet).</returns>
        /// ----------------------------------------------------------------------------
        public static ArrayList GenerateStacks(ArrayList coLocatedFleets)
        {
            ArrayList zoneStacks = new ArrayList();

            foreach (Fleet fleet in coLocatedFleets)
            {
                ArrayList fleetStacks = BuildFleetStacks(fleet);

                foreach (Fleet stack in fleetStacks)
                {
                    zoneStacks.Add(stack);
                }
            }

            return zoneStacks;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the initial position of all of the stacks
        /// </summary>
        /// <param name="zoneStacks">All stacks in this battle (as an ArrayList of Fleet).</param>
        /// ----------------------------------------------------------------------------
        public static void PositionStacks(ArrayList zoneStacks)
        {
            Hashtable races = new Hashtable();
            Hashtable racePositions = new Hashtable();

            foreach (Fleet stack in zoneStacks)
            {
                races[stack.Owner] = stack.Owner;
            }

            SpaceAllocator spaceAllocator = new SpaceAllocator(races.Count);

            // Ensure that we allocate enough space so that all race stacks are
            // out of weapons range (scaled).

            int spaceSize = spaceAllocator.GridAxisCount * Global.MaxWeaponRange;

            spaceAllocator.AllocateSpace(spaceSize);
            Battle.SpaceSize = spaceSize;

            // Now allocate a position for each race in the centre of one of the
            // allocated spacial chunks.

            foreach (string raceName in races.Values)
            {
                Rectangle newPosition = spaceAllocator.GetBox();
                Point position = new Point();

                position.X = newPosition.X + (newPosition.Width / 2);
                position.Y = newPosition.Y + (newPosition.Height / 2);

                racePositions[raceName] = position;
                Battle.Losses[raceName] = 0;
            }

            // Place all stacks belonging to the same race at the same position.

            foreach (Fleet stack in zoneStacks)
            {
                stack.Position = (Point)racePositions[stack.Owner];
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Deal with a battle. This function will execute until all target fleets are
        /// destroyed or a pre-set maximum time has elapsed.   
        /// </summary>
        /// <param name="zoneStacks">All stacks in this battle (as an ArrayList of Fleet).</param>
        /// ----------------------------------------------------------------------------
        public static void DoBattle(ArrayList zoneStacks)
        {
            double time = 0;

            while (true)
            {
                if (SelectTargets(zoneStacks) == 0)
                {
                    break;
                }


                foreach (Fleet stack in zoneStacks)
                    residualMovement.Add(stack, 0);

                MoveStacks(zoneStacks);
                FireWeapons(zoneStacks);

                time++;
                if (time > MaxBattleTime)
                {
                    break;
                }

                residualMovement.Clear();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select targets (if any). Targets are set on a stack-by-stack basis.
        /// </summary>
        /// <param name="zoneStacks">All stacks in this battle (as an ArrayList of Fleet).</param>
        /// <returns>The number of targeted stacks.</returns>
        /// ----------------------------------------------------------------------------
        public static int SelectTargets(ArrayList zoneStacks)
        {
            foreach (Fleet wolf in zoneStacks)
            {
                wolf.Target = null;

                if (wolf.IsArmed == false)
                {
                    continue;
                }

                double maxAttractiveness = 0;

                foreach (Fleet lamb in zoneStacks)
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
            }

            int numberOfTargets = 0;

            foreach (Fleet stack in zoneStacks)
            {
                if (stack.Target != null)
                {
                    numberOfTargets++;
                }
            }

            return numberOfTargets;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine how attractive a fleet is to attack.
        /// </summary>
        /// <param name="target">A stack.</param>
        /// <returns>A measure of attractiveness.</returns>
        /// FIXME (priority 3) - Implement the Stars! attractiveness modle (and possibly others as options). Provide a reference to the source of the algorithm.
        /// ----------------------------------------------------------------------------
        public static double GetAttractiveness(Fleet target)
        {
            double cost = target.Mass + target.TotalCost.Energy;
            double dp = target.Defenses;

            return cost / dp;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if one stack is a potential target of the other. This depends not
        /// just on the relation (friend, enemy, etc.) but also on the battle plan of
        /// the "wolf" stack (e.g. attack everyone, attack enemies, etc.).
        /// </summary>
        /// <param name="wolf">Potential attacker.</param>
        /// <param name="lamb">Potential target.</param>
        /// <returns>True if lamb is a valid target for wolf.</returns>
        /// ----------------------------------------------------------------------------
        public static bool AreEnemies(Fleet wolf, Fleet lamb)
        {
            if (wolf.Owner == lamb.Owner)
            {
                return false;
            }

            RaceData wolfData = StateData.AllRaceData[wolf.Owner] as RaceData;
            string lambRelation = wolfData.PlayerRelations[lamb.Owner] as string;

            BattlePlan battlePlan = wolfData.BattlePlans[wolf.BattlePlan]
                                      as BattlePlan;

            string attackTarget = battlePlan.Attack;

            if (attackTarget == "Everyone")
            {
                return true;
            }
            else if (attackTarget == lamb.Owner)
            {
                return true;
            }
            else if (attackTarget == "Enemies" && lambRelation == "Enemy")
            {
                return true;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Move stacks towards their targets (if any). Record each movement in the
        /// battle report.
        /// </summary>
        /// <param name="zoneStacks">All stacks in the battle.</param>
        /// ----------------------------------------------------------------------------
        public static void MoveStacks(ArrayList zoneStacks)
        {
            // each turn has 3 phases
            // TODO (priority 3) - verify that a ship should be able to move 1 square per phase if it has 3 move points, or is it limited to 1 per turn?
            var phases = 3;
            for (var phase = 0; phase < phases; phase++)
            {
                foreach (Fleet stack in zoneStacks)
                {
                    if (stack.Target != null)
                    {
                        Point from = stack.Position;
                        Point to = stack.Target.Position;
                        double distance = PointUtilities.Distance(from, to);

                        // stack accumulates its full movement over the course of the 3 phases
                        residualMovement[stack] += stack.BattleSpeed / (double)phases;

                        // stack can move only after accumulating at least 1 move point, and after doing so expends that 1 move point
                        if (residualMovement[stack] >= 1.0)
                        {
                            stack.Position = PointUtilities.BattleMoveTo(from, to);
                            residualMovement[stack] -= 1.0;
                        }

                        // Update the battle report with these movements.

                        BattleReport.Movement report = new BattleReport.Movement();
                        report.StackName = stack.Name;
                        report.Position = stack.Position;
                        Battle.Steps.Add(report);
                    }
                    // TODO (priority 5) - shouldn't stacks without targets flee the battle if their strategy says to do so? they're sitting ducks now!
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Fire weapons at selected targets.
        /// </summary>
        /// <param name="zoneStacks">All stacks in the battle.</param>
        /// ----------------------------------------------------------------------------
        private static void FireWeapons(ArrayList zoneStacks)
        {
            // First, identify all of the weapons and their characteristics for
            // every ship present at the battle and who they are pointed at.

            List<WeaponDetails> allWeapons = new List<WeaponDetails>();

            foreach (Fleet stack in zoneStacks)
            {
                foreach (Ship ship in stack.FleetShips)
                {
                    foreach (Weapon weaponSystem in ship.Weapons)
                    {
                        WeaponDetails weapon = new WeaponDetails();

                        weapon.SourceStack = stack;
                        weapon.TargetStack = stack.Target;
                        weapon.Weapon = weaponSystem;

                        allWeapons.Add(weapon);
                        Attack(ship, allWeapons);
                    }
                }
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Launch the attack. We need the ship object as it may have components that
        /// affect the hit power of a weapon discharge (capacitors and battle computers)
        /// </summary>
        /// <param name="ship">A single ship.</param>
        /// <param name="allWeapons">A list of the ship's weapons.</param>
        /// FIXME (priority 6) - It seems this allows one ship to fire each of its weapons 
        /// before any other ship. Each weapon in the battle should fire in priority order.
        /// ----------------------------------------------------------------------------
        private static void Attack(Ship ship, List<WeaponDetails> allWeapons)
        {

            // Sort the weapon list according to weapon system initiative and then
            // fire the weapons in that order.

            allWeapons.Sort();

            foreach (WeaponDetails weapon in allWeapons)
            {
                Fire(ship, weapon);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Fire a weapon. 
        /// Note that, unlike Stars!, we fire at individual ships (not
        /// stacks). So, we have to be careful that the target originally identified has
        /// not already been destroyed.
        /// FIXME (priority 3) - implement the stars system of targeting stacks (posibly with targeting ships as an optional rule).
        /// </summary>
        /// <param name="ship">A ship in the battle.</param>
        /// <param name="weapon">One of ship's weapons.</param>
        /// ----------------------------------------------------------------------------
        private static void Fire(Ship ship, WeaponDetails weapon)
        {
            // First, check that the target stack we originally identified has not
            // been destroyed (actually, the stack still exists at this point but
            // it may have no ships left). In which case, don't bother trying to
            // fire this weapon system (we'll wait until the next battle clock
            // "tick" and re-target then).

            if (weapon.TargetStack == null)
            {
                return;
            }

            Fleet targetStack = weapon.TargetStack;

            if (targetStack.FleetShips.Count == 0)
            {
                return;
            }

            // If the target stack is not within the range of this weapon system
            // then there is no point in trying to fire it.

            Point from = weapon.SourceStack.Position;
            Point to = targetStack.Position;
            double targetDistance = PointUtilities.Distance(from, to);

            if (targetDistance > weapon.Weapon.Range)
            {
                return;
            }

            // We pick off the ships in the stack one-by-one so make the actual
            // target of this weapon system the first (perhaps only) ship in the
            // stack.

            Ship target = targetStack.FleetShips[0] as Ship;
            DischargeWeapon(ship, weapon, target);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// DischargeWeapon. We know the weapon and we know the target ship so attack.
        /// </summary>
        /// <param name="ship">The firing ship.</param>
        /// <param name="details">The weapon being fired.</param>
        /// <param name="target">The target ship.</param>
        /// ----------------------------------------------------------------------------
        private static void DischargeWeapon(Ship ship, WeaponDetails details, Ship target)
        {
            BattleReport.Target report = new BattleReport.Target();
            report.TargetShip = new Ship(target);
            Battle.Steps.Add(report);

            // Identify the attack parameters that have to take into account
            // factors other than the base values (e.g. jammers, capacitors, etc.)

            Weapon weapon = details.Weapon;
            double hitPower = CalculateWeaponPower(ship, weapon, target);
            double accuracy = CalculateWeaponAccuracy(ship, weapon, target);

            if (weapon.IsMissile)
            {
                FireMissile(target, hitPower, accuracy);
            }
            else
            {
                FireBeam(target, hitPower);
            }
            // If we still have some Armor then the ship hasn't been destroyed
            // yet so this is the end of this shot.

            if (target.Armor > 0)
            {
                return;
            }

            // All Defenses have gone. Remove the ship from its stack (which
            // exists only during the battle and, more importantly, remove the
            // ship from its "real" fleet. Also, generate a "destroy" event to
            // update the battle visualisation display.

            details.TargetStack.FleetShips.Remove(target);

            BattleReport.Destroy destroy = new BattleReport.Destroy();
            destroy.ShipName = target.Name;
            destroy.StackName = details.TargetStack.Name;

            Battle.Steps.Add(destroy);

            foreach (Fleet fleet in StateData.AllFleets.Values)
            {
                if (fleet.FleetShips.Contains(target))
                {
                    fleet.FleetShips.Remove(target);
                    break;
                }
            }

            string targetRace = details.TargetStack.Owner;
            Battle.Losses[targetRace] = (int)Battle.Losses[targetRace] + 1;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Do beam weapon damage.
        /// </summary>
        /// <param name="target">Weapon target.</param>
        /// <param name="hitPower">Damage done by the weapon.</param>
        /// ----------------------------------------------------------------------------
        private static void FireBeam(Ship target, double hitPower)
        {
            // First we have to take down the shields of the target ship. If
            // there is any power left over from firing this weapon system at the
            // shields then it will carry forward to attack Armor. If all we have
            // done is weaken the shields then that is the end of this shot.

            hitPower = AttackShields(target, hitPower);

            if (target.Shields > 0 || hitPower <= 0)
            {
                return;
            }

            AttackArmor(target, hitPower);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Fire a missile weapon system
        /// </summary>
        /// <param name="target">Missile weapon target.</param>
        /// <param name="hitPower">Damage the weapon can do.</param>
        /// <param name="accuracy">Missile accuracy.</param>
        /// <remarks>
        /// FIXME (priority 3) - Missile accuracy is not calculated this way in Stars! The effect of computers and jammers must be considered at the same time.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private static void FireMissile(Ship target, double hitPower, double accuracy)
        {
            // First, determine if this missile is going to hit or miss (based on
            // it's accuracy. 
            // FIXME (priority 4) - This algorithm for determining hit or miss is crude. We need a better one.

            int probability = RandomNumber.Next(0, 100);

            if (accuracy >= probability)
            {      // A hit
                double shieldsHit = hitPower / 2;
                double ArmorHit = AttackShields(target, shieldsHit);
                AttackArmor(target, ArmorHit);
            }
            else
            {                              // A miss
                double minDamage = hitPower * 0.125;
                AttackShields(target, minDamage);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Attack the shields
        /// </summary>
        /// <param name="target">Ship being fired on.</param>
        /// <param name="hitPower">Damage output of the weapon.</param>
        /// <returns>Residual damage after shields or zero.</returns>
        /// ----------------------------------------------------------------------------
        private static double AttackShields(Ship target, double hitPower)
        {
            if (target.Shields <= 0)
            {
                return hitPower;
            }

            double initialShields = target.Shields;
            target.Shields -= hitPower;

            if (target.Shields < 0) target.Shields = 0;

            hitPower -= initialShields - target.Shields; // FIXME (priority 6) - This seems wrong, has it been tested? Why reduce the hitPower twice? - Dan 25/4/10

            BattleReport.Weapons fire = new BattleReport.Weapons();
            fire.HitPower = hitPower;
            fire.Targeting = "Shields";
            fire.WeaponTarget.TargetShip = new Ship(target);
            Battle.Steps.Add(fire);

            return hitPower;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Attack the Armor
        /// </summary>
        /// <param name="target">Target being fired on.</param>
        /// <param name="hitPower">Weapon damage.</param>
        /// ----------------------------------------------------------------------------
        private static void AttackArmor(Ship target, double hitPower)
        {
            target.Armor -= hitPower;

            BattleReport.Weapons Armor = new BattleReport.Weapons();
            Armor.HitPower = hitPower;
            Armor.Targeting = "Armor";
            Armor.WeaponTarget.TargetShip = new Ship(target);
            Battle.Steps.Add(Armor);
        }


        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        private static double CalculateWeaponPower(Ship ship, Weapon weapon, Ship target)
        {
            // TODO (priority 5) Stub - just return the base power of weapon. Also need to comment the return value of this function with what defences have been considered by this (when done).
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


        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        private static double CalculateWeaponAccuracy(Ship ship, Weapon weapon, Ship target)
        {
            double weaponAccuracy = weapon.Accuracy;

            if (weapon.IsMissile)
            {
                // TODO (priority 6) - computers and jammer stuff needs to go here *************
            }

            return weaponAccuracy;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Report ship losses to each player.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private static void ReportLosses()
        {
            foreach (string race in Battle.Losses.Keys)
            {
                Message message = new Message();
                message.Audience = race;
                message.Event = Battle;

                message.Text = "There was a battle at " + Battle.Location + "\r\n";

                if ((int)Battle.Losses[race] == 0)
                {
                    message.Text += "None of your ships were destroyed";
                }
                else
                {
                    message.Text += ((int)Battle.Losses[race]).ToString(System.Globalization.CultureInfo.InvariantCulture) +
                       " of your ships were destroyed";
                }

                ServerState.Data.AllMessages.Add(message);
            }
        }

    }
}

