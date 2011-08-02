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

namespace Nova.WinForms.Console
{
    using Nova.Common;
    using Nova.Server;
    
    /// <summary>
    /// Class to carry out fleet waypoint tasks
    /// </summary>
    public class WaypointTasks
    {
        private ServerState stateData;
        private Invade invade;
        
        public WaypointTasks(ServerState serverState, Invade invade)
        {
            this.stateData = serverState;
            this.invade = invade;
        }

        /// <summary>
        /// Perform the waypoint task
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="waypoint"></param>
        public void Perform(Fleet fleet, Waypoint waypoint)
        {
            if (waypoint.Task == WaypointTask.Colonise )
            {
                Colonise(fleet, waypoint);
            }
            else if (waypoint.Task == WaypointTask.Invade )
            {
                invade.Planet(fleet);
            }
            else if (waypoint.Task ==  WaypointTask.Scrap)
            {
                Scrap(fleet, stateData.AllStars[fleet.InOrbit.Name], false);
            }
            else if (waypoint.Task == WaypointTask.UnloadCargo )
            {
                UnloadCargo(fleet, waypoint);
            }
            else if (waypoint.Task == WaypointTask.LayMines )
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

            if (!stateData.AllStars.ContainsKey(waypoint.Destination))
            {
                message.Text += " but " + waypoint.Destination + " is not a star.";
            }
            else
            {
                Star target = stateData.AllStars[waypoint.Destination];

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
                    waypoint.Task =  WaypointTask.None ;
                    Star star = stateData.AllStars[waypoint.Destination];

                    star.ResourcesOnHand.Ironium = fleet.Cargo.Ironium;
                    star.ResourcesOnHand.Boranium = fleet.Cargo.Boranium;
                    star.ResourcesOnHand.Germanium = fleet.Cargo.Germanium;
                    star.Colonists = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
                    star.Owner = fleet.Owner;
                    fleet.Cargo = new Cargo();
                    Scrap(fleet, star, true);
                }
            }

            stateData.AllMessages.Add(message);
        }


        /// <summary>
        /// Unload a fleet's cargo
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
                stateData.AllMessages.Add(message);
                return;
            }

            Star targetStar = stateData.AllStars[waypoint.Destination];

            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at "
                          + targetStar.Name;

            stateData.AllMessages.Add(message);

            waypoint.Task =  WaypointTask.None ;

            targetStar.ResourcesOnHand.Ironium += fleet.Cargo.Ironium;
            targetStar.ResourcesOnHand.Boranium += fleet.Cargo.Boranium;
            targetStar.ResourcesOnHand.Germanium += fleet.Cargo.Germanium;

            fleet.Cargo.Ironium = 0;
            fleet.Cargo.Boranium = 0;
            fleet.Cargo.Germanium = 0;

            // check if this is normal transportation or an invasion
            if (fleet.Owner != targetStar.Owner && fleet.Cargo.ColonistsInKilotons != 0)
            {
                invade.Planet(fleet);

            }
            else
            {
                targetStar.Colonists += fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
                fleet.Cargo.ColonistsInKilotons = 0;
            }

        }


        /// <summary>
        /// Scrap a single ship
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="star"></param>
        /// <param name="amount"></param>
        /// <param name="resources"></param>
        public void Scrap(Ship ship, Star star, double amount, double resources)
        {
            double factor = amount / 100;            
            
            star.ResourcesOnHand += ship.Cost * factor;
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
            EmpireData empire = stateData.AllEmpires[fleet.Owner];
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

                foreach (Ship ship in fleet.FleetShips)
                {
                    Scrap(ship, star, amount, resources);
                }
            }
            else
            {
                // TODO (priority 4) - create a scrap packet in space
            }

            // ServerState.Data.AllFleets.Remove(fleet.Key); // issue 2998887 - causes a crash on colonising due to modification of the itterator list
            fleet.FleetShips.Clear(); // disapear the ships. The (now empty) fleet will be cleaned up latter.


            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = fleet.Name + " has been scrapped";
            stateData.AllMessages.Add(message);
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

            foreach (Minefield minefield in stateData.AllMinefields.Values)
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

            stateData.AllMinefields[newField.Key] = newField;
        }
    }
}
