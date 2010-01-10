// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Manufacture the items in a star's queue
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System;

namespace NovaConsole {


// ============================================================================
// Class to manufacture new items.
// ============================================================================

   public static class Manufacture
   {

      private static ArrayList    deletions = new ArrayList();
      private static ConsoleState stateData = ConsoleState.Data;


// ============================================================================
// Manufacture the items in a production queue (resources permitting).  Note
// that, although we deplete the minerals, the resources per year count is
// preserved.
// ============================================================================

      public static void Items(Star star)
      {
         double energy = star.ResourcesOnHand.Energy;

         deletions.Clear();

         foreach(ProductionQueue.Item item in star.ManufacturingQueue.Queue) {

            bool resourcesExhausted = BuildQueueItem(item, star);
            if (resourcesExhausted) {
               break;
            }
         }

         foreach (ProductionQueue.Item item in deletions) {
            star.ManufacturingQueue.Queue.Remove(item);
         }

         star.ResourcesOnHand.Energy = energy;
      }


// ============================================================================
// Deal with one entry in the production queue (which may be for a quantity of
// more than one of that design).
// ============================================================================

      private static bool BuildQueueItem(ProductionQueue.Item item, Star star)
      {
         bool resourcesExhausted = false;

         while (item.Quantity > 0) {
            resourcesExhausted = BuildDesign(item, star);
            if (resourcesExhausted) {
               break;
            }
         }
         
         return resourcesExhausted;
      }


// ============================================================================
// Build one instance of a particular design. Even if we can't complete
// manufacture this year decrement the required resources with what we can
// donate.
// ============================================================================

      private static bool BuildDesign(ProductionQueue.Item item, Star star)
      {
         String       designName = star.Owner + "/" + item.Name;
         Design       design     = stateData.AllDesigns[designName] as Design;
         NovaCommon.Resources needed = item.BuildState;
         Race          race          = stateData.AllRaces[star.Owner] as Race;

         // If we've ran out of resources then give up. Note that there may be
         // a surplus in some areas and a deficit in others so we have to check
         // the individual resource components for over-payment.

         if (star.ResourcesOnHand < needed) {
            DonateResources(item.BuildState, needed, star.ResourcesOnHand);
            return true;
         }

         star.ResourcesOnHand -= needed;
         switch (design.Type) {
         case "Mine":
            star.Mines++;
            break;

         case "Factory":
            star.Factories++;
            break;

         case "Defenses":
            star.Defenses++;
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
            
         if (item.Quantity == 0) {
            deletions.Add(item);
         }
         else {
            item.BuildState = design.Cost;
         }

         return false;
      }


// ============================================================================
// We don't have quite enough resources to complete manufacture so donate
// what we can and deplete the reserves accordingly.
// ============================================================================

      private static void DonateResources(NovaCommon.Resources currentResources,
                                          NovaCommon.Resources neededResources,
                                          NovaCommon.Resources availableResources)
      {
         neededResources   -= availableResources;
         availableResources = new NovaCommon.Resources();

         if (neededResources.Ironium < 0) {
            availableResources.Ironium = Math.Abs(neededResources.Ironium);
            neededResources.Ironium    = 0;
         }


         if (neededResources.Boranium < 0) {
            availableResources.Boranium = Math.Abs(neededResources.Boranium);
            neededResources.Boranium    = 0;
         }

         if (neededResources.Germanium < 0) {
            availableResources.Germanium = Math.Abs(neededResources.Germanium);
            neededResources.Germanium    = 0;
         }

         if (neededResources.Energy < 0) {
            availableResources.Energy = Math.Abs(neededResources.Energy);
            neededResources.Energy    = 0;
         }
      }



// ============================================================================
// Create a new ship or starbase at the specified location. Starbases are
// handled just like ships except that they cannot move and are not placed in a
// fleet.
// ============================================================================

      private static void CreateShip(ShipDesign design, Star star)
      {
         ConsoleState stateData = ConsoleState.Data;
         Race         race      = stateData.AllRaces[star.Owner] as Race;

         Ship ship  = new Ship(design);
         ship.Name  = design.Name;
         ship.Owner = star.Owner;

         Message message  = new Message();
         message.Audience = star.Owner;
         message.Text     = star.Name + " has produced a new " + design.Name;
         Intel.Data.Messages.Add(message);

         Fleet fleet         = new Fleet(ship, star);
         fleet.Name          = ship.Name + " #" + stateData.FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture);
         fleet.FleetID       = stateData.FleetID;
         fleet.CargoCapacity = ship.Design.CargoCapacity;

         if (design.Type == "Starbase") {
            if (star.Starbase != null) {
               WaypointTasks.Scrap(star.Starbase, star, false);
            } 
            star.Starbase = fleet;
            fleet.Type    = "Starbase";
            fleet.Name = ship.Design.Name;
            fleet.InOrbit = null;
            fleet.CargoCapacity = ship.Design.DockCapacity; //dj this seems to be duplicate use on cargo capacity,
            //we need to think of using shipdesign in fleet for starbases... 
            //AND incase we want fighters etc , smaller than scout we could make like manufacturing carriers later...
            //AND i think we need a starbase class, and a queue of them per planet because we may need more... than 1...

             // Daniel Apr 09 - DockCapacity and CargoCapacity are different. A dock is for building ships and 
             // cargo capacity is for carrying minerals / colonists. A dock capacity > 0 indicates a starbase with
             // refuling capabilities (at least so long as ship producing docks produce free fuel ala Stars! 2.6/7).
             

            if (race.HasTrait("ISB"))
            {
               fleet.Cloaked = 20;
            }

         } 
         else {
            fleet.CargoCapacity = ship.Design.CargoCapacity;
            fleet.InOrbit       = star;
         }

         stateData.AllFleets[fleet.Key] = fleet;
         stateData.FleetID++;
      }

   }
}
