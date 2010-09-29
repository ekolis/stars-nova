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
// Manufacture the items in a star's queue
// ===========================================================================
#endregion

using System;
using System.Collections;

using Nova.Common;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class to manufacture new items.
    /// </summary>
    public static class Manufacture
    {

        private static ArrayList deletions = new ArrayList();
        private static ServerState stateData = ServerState.Data;


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Manufacture the items in a production queue (resources permitting).  Note
        /// that, although we deplete the minerals, the resources per year count is
        /// preserved.
        /// </summary>
        /// <param name="star">The star doing production.</param>
        /// ----------------------------------------------------------------------------
        public static void Items(Star star)
        {
            double energy = star.ResourcesOnHand.Energy;

            deletions.Clear();

            foreach (ProductionQueue.Item item in star.ManufacturingQueue.Queue)
            {

                bool resourcesExhausted = BuildQueueItem(item, star);
                if (resourcesExhausted)
                {
                    break;
                }
            }

            foreach (ProductionQueue.Item item in deletions)
            {
                star.ManufacturingQueue.Queue.Remove(item);
            }

            star.ResourcesOnHand.Energy = energy;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Deal with one entry in the production queue (which may be for a quantity of
        /// more than one of that design).
        /// </summary>
        /// <param name="item">An item to be produced.</param>
        /// <param name="star">The star doing production.</param>
        /// <returns>true if all resources have been exhausted.</returns>
        /// ----------------------------------------------------------------------------
        private static bool BuildQueueItem(ProductionQueue.Item item, Star star)
        {
            bool resourcesExhausted = false;

            while (item.Quantity > 0)
            {

                resourcesExhausted = BuildDesign(item, star);
                if (resourcesExhausted)
                {
                    break;
                }
            }

            return resourcesExhausted;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Build one instance of a particular design. Even if we can't complete
        /// manufacture this year decrement the required resources by the percentage
        /// we can achieve.
        /// </summary>
        /// <param name="item">An item to be produced.</param>
        /// <param name="star">The star system doing production</param>
        /// <returns>true if the star is unable to finish productio of this item.</returns>
        /// ----------------------------------------------------------------------------
        private static bool BuildDesign(ProductionQueue.Item item, Star star)
        {
            string designName = star.Owner + "/" + item.Name;
            Design design = stateData.AllDesigns[designName] as Design;
            Nova.Common.Resources needed = item.BuildState;

            // If we've ran out of resources then give up. Note that there may be
            // a surplus in some areas and a deficit in others so we have to check
            // the individual resource components for over-payment.

            if (!(star.ResourcesOnHand >= needed))
            {
                PartialBuild(item, needed, star);
                return true;
            }

            star.ResourcesOnHand -= needed;
            switch (design.Type)
            {
                case "Mine":
                    star.Mines++;
                    break;

                case "Factory":
                    star.Factories++;
                    break;

                case "Defenses":
                    star.Defenses++;
                    if (star.Defenses >= Global.MaxDefenses)
                    {
                        star.Defenses = Global.MaxDefenses; // This should never be required, but just in case.
                        item.Quantity = 0;
                    }
                    break;

                case "Ship":
                    CreateShip(design as ShipDesign, star);
                    break;

                case "Starbase":
                    CreateShip(design as ShipDesign, star);
                    break;

                default:
                    Report.Error("Unknown item in production queue " + design.Type);
                    break;
            }

            item.Quantity--;

            if (item.Quantity == 0)
            {
                deletions.Add(item);
            }
            else
            {
                item.BuildState = design.Cost;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// We do not have quite enough resources to complete production so use the percent
        /// that we can achieve, deplete the reserves, and adjust the BuildState accordingly.
        /// </summary>
        /// <param name="item">The item to be partially produced.</param>
        /// <param name="neededResources">The Resources cost to complete production of the item (either BuildState or design.Cost).</param>
        /// <param name="star">The Star System doing the production.</param>
        /// ----------------------------------------------------------------------------
        private static void PartialBuild(
            ProductionQueue.Item item,
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
            usedResources.Ironium = Convert.ToInt32(percentCompleted * neededResources.Ironium);
            usedResources.Boranium = Convert.ToInt32(percentCompleted * neededResources.Boranium);
            usedResources.Germanium = Convert.ToInt32(percentCompleted * neededResources.Germanium);
            usedResources.Energy = Convert.ToInt32(percentCompleted * neededResources.Energy);

            star.ResourcesOnHand = star.ResourcesOnHand - usedResources;

            neededResources = neededResources - usedResources;
            item.BuildState = neededResources;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Create a new ship or starbase at the specified location. Starbases are
        /// handled just like ships except that they cannot move.
        /// </summary>
        /// <param name="design">A ShipDesign to be constructed.</param>
        /// <param name="star">The star system producing the ship.</param>
        /// ----------------------------------------------------------------------------
        private static void CreateShip(ShipDesign design, Star star)
        {
            ServerState stateData = ServerState.Data;
            Race race = stateData.AllRaces[star.Owner] as Race;

            Ship ship = new Ship(design);
            ship.Name = design.Name;
            ship.Owner = star.Owner;

            Message message = new Message();
            message.Audience = star.Owner;
            message.Text = star.Name + " has produced a new " + design.Name;
            stateData.AllMessages.Add(message);

            Fleet fleet = new Fleet(ship, star);
            fleet.Name = ship.Name + " #" + stateData.FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture);
            fleet.FleetID = stateData.FleetID;

            // Add the fleet to the state data so it can be tracked.
            stateData.AllFleets[fleet.Key] = fleet;
            stateData.FleetID++;

            if (design.Type == "Starbase")
            {
                if (star.Starbase != null)
                {
                    WaypointTasks.Scrap(star.Starbase, star, false);
                }
                star.Starbase = fleet;
                fleet.Type = "Starbase";
                fleet.Name = ship.DesignName;
                fleet.InOrbit = star;

                if (race.HasTrait("ISB"))
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
