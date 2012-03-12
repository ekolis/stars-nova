#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.Server
{
    using System;
    
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Server;
    
    /// <summary>
    /// Class to carry out fleet waypoint tasks.
    /// </summary>
    public class WaypointTasks
    {
        private ServerData serverState;
        
        public WaypointTasks(ServerData serverState)
        {
            this.serverState = serverState;
        }

        /// <summary>
        /// Perform the waypoint task.
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        public void Perform(Fleet fleet, Waypoint waypoint)
        {
            if (waypoint.Task == WaypointTask.Colonise)
            {
                Colonise(fleet, waypoint);
            }
            else if (waypoint.Task == WaypointTask.Invade)
            {
                Invade(fleet);
            }
            else if (waypoint.Task ==  WaypointTask.Scrap)
            {
                Scrap(fleet, serverState.AllStars[fleet.InOrbit.Name], false);
            }
            else if (waypoint.Task == WaypointTask.UnloadCargo)
            {
                UnloadCargo(fleet, waypoint);
            }
            else if (waypoint.Task == WaypointTask.LayMines)
            {
                LayMines(fleet);
            }
        }


        /// <summary>
        /// Colonise a planet.
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        private void Colonise(Fleet fleet, Waypoint waypoint)
        {
            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = fleet.Name
                         + " attempted to colonise " + waypoint.Destination;

            if (!serverState.AllStars.ContainsKey(waypoint.Destination))
            {
                message.Text += " but " + waypoint.Destination + " is not a star.";
            }
            else
            {
                Star target = serverState.AllStars[waypoint.Destination];

                if (target.Colonists != 0)
                {
                    message.Text += " but it is already occupied.";
                }
                else if (fleet.Cargo.ColonistsInKilotons == 0)
                {
                    message.Text += " but no colonists were on board.";
                }
                else if (fleet.CanColonize == false)
                {
                    message.Text += " but no ships with colonization module were present.";
                }
                else
                {
                    message.Text = "You have colonised " + waypoint.Destination;
                    waypoint.Task =  WaypointTask.None;
                    Star star = serverState.AllStars[waypoint.Destination];

                    star.ResourcesOnHand.Ironium = fleet.Cargo.Ironium;
                    star.ResourcesOnHand.Boranium = fleet.Cargo.Boranium;
                    star.ResourcesOnHand.Germanium = fleet.Cargo.Germanium;
                    star.Colonists = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
                    star.Owner = fleet.Owner;
                    fleet.Cargo = new Cargo();
                    Scrap(fleet, star, true);
                }
            }

            serverState.AllMessages.Add(message);
        }


        /// <summary>
        /// Unload a fleet's cargo.
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        private void UnloadCargo(Fleet fleet, Waypoint waypoint)
        {
            Message message = new Message();
            message.Audience = fleet.Owner;

            if (fleet.InOrbit == null)
            {
                message.Text = fleet.Name
                   + " attempted to unload cargo while not in orbit.";
                serverState.AllMessages.Add(message);
                return;
            }

            Star targetStar = serverState.AllStars[waypoint.Destination];

            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at "
                          + targetStar.Name;

            serverState.AllMessages.Add(message);

            waypoint.Task =  WaypointTask.None;

            targetStar.ResourcesOnHand.Ironium += fleet.Cargo.Ironium;
            targetStar.ResourcesOnHand.Boranium += fleet.Cargo.Boranium;
            targetStar.ResourcesOnHand.Germanium += fleet.Cargo.Germanium;

            fleet.Cargo.Ironium = 0;
            fleet.Cargo.Boranium = 0;
            fleet.Cargo.Germanium = 0;

            // check if this is normal transportation or an invasion
            if (fleet.Owner != targetStar.Owner && fleet.Cargo.ColonistsInKilotons != 0)
            {
                Invade(fleet);

            }
            else
            {
                targetStar.Colonists += fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
                fleet.Cargo.ColonistsInKilotons = 0;
            }

        }


        /// <summary>
        /// Scrap a single ship.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="star"></param>
        /// <param name="amount"></param>
        /// <param name="resources"></param>
        public void Scrap(ShipToken token, Star star, double amount, double resources)
        {
            double factor = amount / 100;            
            
            star.ResourcesOnHand += token.Design.Cost * token.Quantity * factor;
        }


        /// <summary>
        /// Scrap a fleet (fleets include starbases).
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="star"></param>
        /// <param name="colonise"></param>
        /// <remarks>
        /// The minerals depositied are:
        /// 
        /// Colonisation                        - 75%
        /// Scrap at a starbase                 - 80%
        /// Scrap at a planet without a stabase - 33%
        /// Scrap in space                      - 0%
        ///
        /// If the secondary trait Ulitmate Recycling has been selected when you scrap a
        /// fleet at a starbase, you recover 90% of the minerals and 70% of the
        /// resources used to produce the fleet. The resources are available or use the
        /// next year. Scrapping at a planet gives you 45% of the minerals and 35% of
        /// the resources.These resources are not strictly additive.
        /// </remarks>
        public void Scrap(Fleet fleet, Star star, bool colonise)
        {
            double amount = 0;
            EmpireData empire = serverState.AllEmpires[fleet.Owner];
            double resources = 0;

            if (star != null)
            {
                if (empire.Race.HasTrait("UR"))
                {
                    if (star != null)
                    {
                        if (star.Starbase != null)
                        {
                            amount = 90;
                            resources = 70;
                        }
                        else
                        {
                            amount = 45;
                        }
                    }
                }
                else
                {
                    if (colonise)
                    {
                        amount = 75;
                    }
                    else if (star != null)
                    {
                        if (star.Starbase != null)
                        {
                            amount = 80;
                        }
                        else
                        {
                            amount = 33;
                        }
                    }
                }

                foreach (ShipToken token in fleet.Tokens)
                {
                    Scrap(token, star, amount, resources);
                }
            }
            else
            {
                // TODO (priority 4) - create a scrap packet in space
            }

            // ServerState.Data.AllFleets.Remove(fleet.Key); // issue 2998887 - causes a crash on colonising due to modification of the itterator list
            fleet.Tokens.Clear(); // disapear the ships. The (now empty) fleet will be cleaned up latter.


            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = fleet.Name + " has been scrapped";
            serverState.AllMessages.Add(message);
        }
        

        /// <summary>
        /// See if we can lay mines here. If so, lay them. If there is no Minefield here
        /// start a new one. 
        /// </summary>
        /// <param name="fleet">A (potential) mine laying fleet.</param>
        public void LayMines(Fleet fleet)
        {
            if (fleet.NumberOfMines == 0)
            {
                return;
            }

            // See if a Minefield is already here (owned by us). We allow a
            // certaintolerance in distance because it is unlikely that the
            // waypoint has been set exactly right.

            foreach (Minefield minefield in serverState.AllMinefields.Values)
            {
                if (PointUtilities.IsNear(fleet.Position, minefield.Position))
                {
                    if (minefield.Owner == fleet.Owner)
                    {
                        minefield.NumberOfMines += fleet.NumberOfMines;
                        return;
                    }
                }
            }

            // No Minefield found. Start a new one.

            Minefield newField = new Minefield();

            newField.Position = fleet.Position;
            newField.Owner = fleet.Owner;
            newField.NumberOfMines = fleet.NumberOfMines;

            serverState.AllMinefields[newField.Key] = newField;
        }
        
        
        /// <summary>
        /// Invade a star system.
        /// </summary>
        /// <param name="fleet">The invading fleet.</param>
        public void Invade(Fleet fleet)
        {
            // First check that we are actuallly in orbit around a planet.

            if (fleet.InOrbit == null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade but the waypoint is not a planet";
                serverState.AllMessages.Add(message);
                return;
            }

            // and that we have troops.

            int troops = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
            Star star = serverState.AllStars[fleet.InOrbit.Name];

            if (troops == 0)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade " + star.Name + " but there are no troops on board";
                serverState.AllMessages.Add(message);
                return;
            }

            // Consider the diplomatic situation
            if (fleet.Owner == star.Owner)
            {
                // already own this planet, so colonists can beam down safely
                star.Colonists += troops;
                fleet.Cargo.ColonistsInKilotons = 0;
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                               "invade " + star.Name +
                               " but it is already ours. Troops have joined the local populace.";
                serverState.AllMessages.Add(message);
                return;
            }
            if (star.Owner == Global.Nobody)
            {
                // This star has not been colonised. Can't invade.
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                               "invade " + star.Name +
                               " but it is not colonised. You must send a ship with a colony module and orders to colonise to take this system.";
                serverState.AllMessages.Add(message);
                return;
            }
            PlayerRelation relation = serverState.AllEmpires[fleet.Owner].EmpireReports[star.Owner].Relation;
            switch (relation)
            {
                case PlayerRelation.Friend:
                case PlayerRelation.Neutral:
                    {
                        Message message = new Message();
                        message.Audience = fleet.Owner;
                        message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                                       "invade " + star.Name +
                                       " but the " + star.Owner + " are not our enemies. Order has been cancelled.";
                        serverState.AllMessages.Add(message);
                        return;
                    }
                case PlayerRelation.Enemy:
                    {
                        // continue with the invasion
                        break;
                    }
                default:
                    {
                        Report.Error("An unrecognised relationship \"" + relation + "\" was encountered. Invasion of " + star.Name + " has been cancelled.");
                        break;
                    }

            }

            // check for starbase
            if (star.Starbase != null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                               "invade " + star.Name +
                               " but the starbase at " + star.Name + " would kill all invading troops. Order has been cancelled.";
                serverState.AllMessages.Add(message);
                return;
            }

            // The troops are now committed to take the star or die trying
            fleet.Cargo.ColonistsInKilotons = 0; 

            // Set up the message recipients before the star (potentially) changes hands.
            Message wolfMessage = new Message();
            wolfMessage.Audience = fleet.Owner;
            Message lambMessage = new Message();
            lambMessage.Audience = star.Owner;

            // Take into account the Defenses
            Defenses.ComputeDefenseCoverage(star);
            int troopsOnGround = (int)(troops * (1.0 - Defenses.InvasionCoverage));

            // Apply defender and attacker bonuses
            double attackerBonus = 1.1;
            if (serverState.AllEmpires[fleet.Owner].Race.HasTrait("WM"))
            {
                attackerBonus *= 1.5;
            }

            double defenderBonus = 1.0;
            if (serverState.AllEmpires[fleet.Owner].Race.HasTrait("IS"))
            {
                defenderBonus *= 2.0;
            }

            int defenderStrength = (int)(star.Colonists * defenderBonus);
            int attackerStrength = (int)(troopsOnGround * attackerBonus);
            int survivorStrength = defenderStrength - attackerStrength; // will be negative if attacker wins

            string messageText = fleet.Owner + " fleet " + fleet.Name + " attacked " +
                                 star.Name + " with " + troops + " troops. ";

            if (survivorStrength > 0)
            {
                // defenders win
                int remainingDefenders = (int)(survivorStrength / defenderBonus);
                remainingDefenders = Math.Max(remainingDefenders, Global.ColonistsPerKiloton);
                int defendersKilled = star.Colonists - remainingDefenders;
                star.Colonists = remainingDefenders;

                messageText += "The attackers were slain but "
                            + defendersKilled +
                            " colonists were killed in the attack.";

                wolfMessage.Text = messageText;
                serverState.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                serverState.AllMessages.Add(lambMessage);
            }
            else if (survivorStrength < 0)
            {
                // attacker wins
                star.ManufacturingQueue.Clear();
                int remainingAttackers = (int)(-survivorStrength / attackerBonus);
                remainingAttackers = Math.Max(remainingAttackers, Global.ColonistsPerKiloton);
                int attackersKilled = troops - remainingAttackers;
                star.Colonists = remainingAttackers;
                // star.Owner = fleet.Owner; // FIXME (priority 4) - This doesn't work. Is star a copy?
                serverState.AllStars[star.Name].Owner = fleet.Owner;

                messageText += "The defenders were slain but "
                            + attackersKilled +
                            " troops were killed in the attack.";

                wolfMessage.Text = messageText;
                serverState.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                serverState.AllMessages.Add(lambMessage);
            }
            else
            {
                // no survivors!
                messageText += "Both sides fought to the last and none were left to claim the planet!";

                wolfMessage.Text = messageText;
                serverState.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                serverState.AllMessages.Add(lambMessage);

                // clear out the colony
                star.ManufacturingQueue.Clear();
                star.Colonists = 0;
                star.Mines = 0;
                star.Factories = 0;
                star.Owner = Global.Nobody;
            }

        }
    }
}
