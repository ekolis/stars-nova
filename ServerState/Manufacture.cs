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

namespace Nova.Server
{
    using System.Collections.Generic;
    
    using Nova.Common;
    using Nova.Common.Components;

    /// <summary>
    /// Class to manufacture the items in a star's queue.
    /// </summary>
    public class Manufacture
    {
        private ServerData serverState;
  
        public Manufacture(ServerData serverState)
        {
            this.serverState = serverState;
        }

        /// <summary>
        /// Manufacture the items in a production queue (resources permitting).
        /// </summary>
        /// <param name="star">The star doing production.</param>
        /// <remarks>
        /// Dont't preserve resource count as resource depletion is needed to
        /// contribute with leftover resources for research.
        /// </remarks>
        public void Items(Star star)
        {
            List<ProductionOrder> completed = new List<ProductionOrder>();
            
            foreach (ProductionOrder productionOrder in star.ManufacturingQueue.Queue)
            {
                if (productionOrder.IsBlocking(star))
                {
                    // Items block the queue when they can't be processed (i.e. not enough resources)
                    // AND they are not autobuild orders (autobuild never blocks the Queue).
                    break;
                }
                
                // Deal with the production Order.
                int done = productionOrder.Process(star);
                
                if (done > 0 && productionOrder.Unit is ShipProductionUnit)
                {
                    CreateShips(serverState.AllDesigns[(productionOrder.Unit as ShipProductionUnit).DesignKey] as ShipDesign, star, done);
                }
                    
                if (productionOrder.Quantity == 0)
                {
                    completed.Add(productionOrder);
                }
            }
            
            foreach (ProductionOrder done in completed)
            {
                star.ManufacturingQueue.Queue.Remove(done);
            }
        }
        

        /// <summary>
        /// Create a new ship or starbase at the specified location. Starbases are
        /// handled just like ships except that they cannot move.
        /// </summary>
        /// <param name="design">A ShipDesign to be constructed.</param>
        /// <param name="star">The star system producing the ship.</param>
        private void CreateShips(ShipDesign design, Star star, int countToBuild)
        {
            EmpireData empire = serverState.AllEmpires[star.Owner];

            Fleet fleet = null;
            for (int i = 0; i < countToBuild; ++i)
            {
                Ship ship = new Ship(design, empire.GetNextShipKey());
                ship.Name = design.Name;
                ship.Owner = star.Owner;
                if (fleet == null)
                {
                    fleet = new Fleet(ship, star, empire.GetNextFleetKey());
                    fleet.Name = design.Name + " #" + fleet.Id;
                }
                else
                {
                    fleet.FleetShips.Add(ship);
                }
                fleet.FuelAvailable = fleet.TotalFuelCapacity;
            }
            

            Message message = new Message();
            message.Audience = star.Owner;
            message.Text = star.Name + " has produced " + countToBuild + " new " + design.Name;
            serverState.AllMessages.Add(message);

            
            // Add the fleet to the state data so it can be tracked.
            serverState.AllFleets[fleet.Key] = fleet;          

            if (design.Type == ItemType.Starbase)
            {
                if (star.Starbase != null)
                {
                    // Old starbases are not scrapped. Instead, the reduced
                    // upgrade cost should have already been factored when first
                    // queuing the "upgrade", so the old SB is just
                    // discarded and replaced at this point. -Aeglos 2 Aug 11
                    star.Starbase = null;
                    // waypointTasks.Scrap(star.Starbase, star, false);
                }
                star.Starbase = fleet;
                fleet.Type = ItemType.Starbase;
                fleet.Name = star.Name + " " + fleet.Type;
                fleet.InOrbit = star;

                if (empire.Race.HasTrait("ISB"))
                {
                    fleet.Cloaked = 20;
                }

            }
            else
            {
                fleet.InOrbit = star;
            }
        }
    }
}
