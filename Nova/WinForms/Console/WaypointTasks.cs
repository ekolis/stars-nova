#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// This module deals with Fleet waypoint tasks
// ===========================================================================
#endregion

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using Nova.Common;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class to process a new turn.
    /// </summary>
    public static class WaypointTasks
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Perform the waypoint task
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        /// ----------------------------------------------------------------------------
        public static void Perform(Fleet fleet, Waypoint waypoint)
        {
            if (waypoint.Task == "Colonise")
            {
                Colonise(fleet, waypoint);
            }
            else if (waypoint.Task == "Invade")
            {
                Invade.Planet(fleet);
            }
            else if (waypoint.Task == "Scrap")
            {
                Scrap(fleet, fleet.InOrbit, false);
            }
            else if (waypoint.Task == "Unload Cargo")
            {
                UnloadCargo(fleet, waypoint);
            }
            else if (waypoint.Task == "Lay Mines")
            {
                LayMines.DoMines(fleet);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Colonise a planet.
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        /// ----------------------------------------------------------------------------
        private static void Colonise(Fleet fleet, Waypoint waypoint)
        {
            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = fleet.Name
                         + " attempted to colonise " + waypoint.Destination;

            if (!ServerState.Data.AllStars.Contains(waypoint.Destination))
            {
                message.Text += " but " + waypoint.Destination + " is not a star.";
            }
            else
            {
                Star target = ServerState.Data.AllStars[waypoint.Destination]
                              as Star;

                if (target.Colonists != 0)
                {
                    message.Text += " but it is already occupied.";
                }
                else if (fleet.Cargo.ColonistsInKilotons == 0)
                {
                    message.Text += " but no colonists were on board.";
                }
                else
                {
                    message.Text = "You have colonised " + waypoint.Destination;
                    waypoint.Task = "None";
                    Star star = ServerState.Data.AllStars[waypoint.Destination]
                                    as Star;

                    star.ResourcesOnHand.Ironium = fleet.Cargo.Ironium;
                    star.ResourcesOnHand.Boranium = fleet.Cargo.Boranium;
                    star.ResourcesOnHand.Germanium = fleet.Cargo.Germanium;
                    star.Colonists = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
                    star.Owner = fleet.Owner;
                    fleet.Cargo = new Cargo();
                    Scrap(fleet, star, true);
                }
            }

            ServerState.Data.AllMessages.Add(message);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Unload a fleet's cargo
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        /// ----------------------------------------------------------------------------
        private static void UnloadCargo(Fleet fleet, Waypoint waypoint)
        {
            Message message = new Message();
            message.Audience = fleet.Owner;

            if (fleet.InOrbit == null)
            {
                message.Text = fleet.Name
                   + " attempted to unload cargo while not in orbit.";
                ServerState.Data.AllMessages.Add(message);
                return;
            }

            Star targetStar = ServerState.Data.AllStars[waypoint.Destination]
                        as Star;

            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at "
                          + targetStar.Name;

            ServerState.Data.AllMessages.Add(message);

            waypoint.Task = "None";


            targetStar.ResourcesOnHand.Ironium += fleet.Cargo.Ironium;
            targetStar.ResourcesOnHand.Boranium += fleet.Cargo.Boranium;
            targetStar.ResourcesOnHand.Germanium += fleet.Cargo.Germanium;
            targetStar.Colonists += fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;

            fleet.Cargo = new Cargo();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Scrap a single ship
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="star"></param>
        /// <param name="amount"></param>
        /// <param name="resources"></param>
        /// ----------------------------------------------------------------------------
        public static void Scrap(Ship ship, Star star, double amount,
                                 double resources)
        {
            double factor = amount / 100;

            star.ResourcesOnHand.Ironium = ship.Design.Cost.Ironium * factor;
            star.ResourcesOnHand.Boranium = ship.Design.Cost.Boranium * factor;
            star.ResourcesOnHand.Germanium = ship.Design.Cost.Germanium * factor;
            star.ResourcesOnHand.Energy = ship.Design.Cost.Energy * factor;
        }


        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        public static void Scrap(Fleet fleet, Star star, bool colonise)
        {
            double amount = 0;
            Race race = ServerState.Data.AllRaces[fleet.Owner] as Race;
            double resources = 0;

            if (race.HasTrait("UR"))
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


            foreach (Ship ship in fleet.FleetShips)
            {
                Scrap(ship, star, amount, resources);
            }

            // ServerState.Data.AllFleets.Remove(fleet.Key); // issue 2998887 - causes a crash on colonising due to modification of the itterator list
            fleet.FleetShips.Clear(); // disapear the ships. The (now empty) fleet will be cleaned up latter.


            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = fleet.Name + " has been scrapped";
            ServerState.Data.AllMessages.Add(message);
        }
    }
}
