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
        private List<ProductionItem> deletions = new List<ProductionItem>();
        private ServerState stateData;
  
        public Manufacture(ServerState serverState)
        {
            this.stateData = serverState;
        }

        /// <summary>
        /// manufacture the items in a production queue (resources permitting).
        /// </summary>
        /// <param name="star">The star doing production.</param>
        /// <remarks>
        /// Dont't preserve resource count as resource depletion is needed to
        /// contribute with leftover resources for research.
        /// </remarks>
        public void Items(Star star)
        {
            deletions.Clear();

            foreach (ProductionItem item in star.ManufacturingQueue.Queue)
            {

                bool resourcesExhausted = BuildQueueItem(item, star);
                if (resourcesExhausted)
                {
                    break;
                }
            }

            foreach (ProductionItem item in deletions)
            {
                star.ManufacturingQueue.Queue.Remove(item);
            }
        }

        /// <summary>
        /// Deal with one entry in the production queue (which may be for a quantity of
        /// more than one of that design).
        /// </summary>
        /// <param name="productionItem">An item to be produced.</param>
        /// <param name="star">The star doing production.</param>
        /// <returns>true if all resources have been exhausted.</returns>
        private bool BuildQueueItem(ProductionItem productionItem, Star star)
        {
            bool resourcesExhausted = false;

            while (productionItem.Quantity > 0)
            {

                resourcesExhausted = BuildDesign(productionItem, star);
                if (resourcesExhausted)
                {
                    break;
                }
            }

            return resourcesExhausted;
        }

        /// <summary>
        /// Build one instance of a particular design. Even if we can't complete
        /// manufacture this year decrement the required resources by the percentage
        /// we can achieve.
        /// </summary>
        /// <param name="productionItem">An item to be produced.</param>
        /// <param name="star">The star system doing production</param>
        /// <returns>true if the star is unable to finish productio of this item.</returns>
        private bool BuildDesign(ProductionItem productionItem, Star star)
        {
            Design design = stateData.AllDesigns[productionItem.Key];
            Nova.Common.Resources needed = productionItem.BuildState;

            // Try and build as many of this item as we can
            // If we've ran out of resources then give up. Note that there may be
            // a surplus in some areas and a deficit in others so we have to check
            // the individual resource components for over-payment.
            int countToBuild = 0;
            bool doAPartialBuild = false;
            while (productionItem.Quantity > 0)
            {
                if (!(star.ResourcesOnHand >= needed))
                {
                    doAPartialBuild = true;
                    break;                    
                }

                star.ResourcesOnHand -= needed;
                productionItem.Quantity--;
                countToBuild++;                
            }

            if (countToBuild > 0)
            {
                switch (design.Type)
                {
                    case ItemType.Mine:
                        star.Mines += countToBuild;
                        break;

                    case ItemType.Factory:
                        star.Factories += countToBuild;
                        break;

                    case ItemType.Defenses:
                        star.Defenses += countToBuild;
                        if (star.Defenses >= Global.MaxDefenses)
                        {
                            star.Defenses = Global.MaxDefenses; // This should never be required, but just in case.
                            // TODO: Should probably refund the resources if it is required though!
                            productionItem.Quantity = 0;
                        }
                        break;

                    case ItemType.Ship:
                        CreateShip(design as ShipDesign, star, countToBuild);
                        break;

                    case ItemType.Starbase:
                        // first remove the old starbase
                        while (countToBuild > 0)
                        {
                            if (star.Starbase != null)
                            {
                                stateData.AllFleets.Remove(star.Starbase.Key);
                                star.Starbase = null;
                            }

                            CreateShip(design as ShipDesign, star, 1);
                            countToBuild--;
                        }
                        break;

                    default:
                        Report.Error("Unknown item in production queue " + design.Type.ToDescription());
                        break;
                }
            }


            if (doAPartialBuild)
            {
                PartialBuild(productionItem, needed, star);
                return true;
            }
            else
            {
                if (productionItem.Quantity == 0)
                {
                    deletions.Add(productionItem);
                }
                else
                {
                    productionItem.BuildState = design.Cost;
                }
                return false;
            }
        }

        /// <summary>
        /// We do not have quite enough resources to complete production so use the percent
        /// that we can achieve, deplete the reserves, and adjust the BuildState accordingly.
        /// </summary>
        /// <param name="productionItem">The item to be partially produced.</param>
        /// <param name="neededResources">The Resources cost to complete production of the item (either BuildState or design.Cost).</param>
        /// <param name="star">The Star System doing the production.</param>
        private void PartialBuild(
            ProductionItem productionItem,
            Resources neededResources,
            Star star)
        {
            Resources insufficientResources = new Resources();  // used to temporarily store the amount of resources we are short for any resources that are short
            insufficientResources = neededResources - star.ResourcesOnHand;

            // determine which resource limits production (i.e. is able to complete the smallest percentage of production)
            double percentCompleted = 1.0;
            if (percentCompleted > (1 - ((float)insufficientResources.Ironium / neededResources.Ironium)) && insufficientResources.Ironium > 0)
            {
                percentCompleted = 1 - ((float)insufficientResources.Ironium / neededResources.Ironium);
            }

            if (percentCompleted > (1 - ((float)insufficientResources.Boranium / neededResources.Boranium)) && insufficientResources.Boranium > 0)
            {
                percentCompleted = 1 - ((float)insufficientResources.Boranium / neededResources.Boranium);
            }

            if (percentCompleted > (1 - ((float)insufficientResources.Germanium / neededResources.Germanium)) && insufficientResources.Germanium > 0)
            {
                percentCompleted = 1 - ((float)insufficientResources.Germanium / neededResources.Germanium);
            }

            if (percentCompleted > (1 - ((float)insufficientResources.Energy / neededResources.Energy)) && insufficientResources.Energy > 0)
            {
                percentCompleted = 1 - ((float)insufficientResources.Energy / neededResources.Energy);
            }


            Resources usedResources = new Resources();
            
            usedResources = neededResources * percentCompleted;
 
            star.ResourcesOnHand -= usedResources;

            neededResources -= usedResources;
            productionItem.BuildState = neededResources;
        }

        /// <summary>
        /// Create a new ship or starbase at the specified location. Starbases are
        /// handled just like ships except that they cannot move.
        /// </summary>
        /// <param name="design">A ShipDesign to be constructed.</param>
        /// <param name="star">The star system producing the ship.</param>
        private void CreateShip(ShipDesign design, Star star, int countToBuild)
        {
            EmpireData empire = stateData.AllEmpires[star.Owner];

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
            stateData.AllMessages.Add(message);

            
            // Add the fleet to the state data so it can be tracked.
            stateData.AllFleets[fleet.Key] = fleet;          

            if (design.Type == ItemType.Starbase)
            {
                if (star.Starbase != null)
                {
                    // Old starbases are not scrapped. Instead, the reduced
                    // upgrade cost should have already been factored when first
                    // queuing the "upgrade", so the old SB is just
                    // discarded and replaced at this point. -Aeglos 2 Aug 11
                    star.Starbase = null;
                    //waypointTasks.Scrap(star.Starbase, star, false);
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
