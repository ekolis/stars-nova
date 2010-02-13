// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module deals with Fleet waypoint tasks
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using NovaCommon;
using NovaServer;

namespace NovaConsole 
{


// ============================================================================
// Class to process a new turn.
// ============================================================================

   public static class WaypointTasks
   {


// ============================================================================
// Perform the waypoint task
// ============================================================================

      public static void Perform(Fleet fleet, Waypoint waypoint)
      {
         if (waypoint.Task == "Colonise") {
            Colonise(fleet, waypoint);
         }
         else if (waypoint.Task == "Invade") {
            Invade.Planet(fleet);
         }
         else if (waypoint.Task == "Scrap") {
            Scrap(fleet, fleet.InOrbit, false);
         }
         else if (waypoint.Task == "Unload Cargo") {
            UnloadCargo(fleet, waypoint);
         }
         else if (waypoint.Task == "Lay Mines") {
            LayMines.DoMines(fleet);
         }
      }


// ============================================================================
// Colonise a planet.
// ============================================================================

      private static void Colonise(Fleet fleet, Waypoint waypoint)
      {
         Message message  = new Message();
         message.Audience = fleet.Owner;
         message.Text = fleet.Name 
                      + " attempted to colonise " + waypoint.Destination;

         Star target = ServerState.Data.AllStars[waypoint.Destination]
                       as Star;

         if (target.Colonists != 0) {
            message.Text += " but it is already occupied.";
         }
         else if (fleet.Cargo.Colonists == 0) {
            message.Text += " but no colonists were on board.";
         }
         else {
            message.Text  = "You have colonised " + waypoint.Destination;
            waypoint.Task = "None";
            Star star     = ServerState.Data.AllStars[waypoint.Destination]
                            as Star;

            star.ResourcesOnHand.Ironium   = fleet.Cargo.Ironium;
            star.ResourcesOnHand.Boranium  = fleet.Cargo.Boranium;
            star.ResourcesOnHand.Germanium = fleet.Cargo.Germanium;
            star.Colonists                 = fleet.Cargo.Colonists * 1000;
            star.Owner                     = fleet.Owner;
            fleet.Cargo                    = new Cargo();
            Scrap(fleet, star, true);
         }

         Intel.Data.Messages.Add(message);
      }


// ============================================================================
// Unload a fleet's cargo
// ============================================================================

      private static void UnloadCargo(Fleet fleet, Waypoint waypoint)
      {
         Message message  = new Message();
         message.Audience = fleet.Owner;

         if (fleet.InOrbit == null) {
            message.Text = fleet.Name 
               + " attempted to unload cargo while not in orbit.";
            Intel.Data.Messages.Add(message);
            return;
         }

         Star target = ServerState.Data.AllStars[waypoint.Destination]
                     as Star;

         message.Text  = "Fleet " + fleet.Name + " has unloaded its cargo at "
                       + target.Name;

         Intel.Data.Messages.Add(message);
         
         waypoint.Task = "None";
         Star star     = ServerState.Data.AllStars[waypoint.Destination]
                         as Star;

         star.ResourcesOnHand.Ironium   = fleet.Cargo.Ironium;
         star.ResourcesOnHand.Boranium  = fleet.Cargo.Boranium;
         star.ResourcesOnHand.Germanium = fleet.Cargo.Germanium;
         star.Colonists                 = fleet.Cargo.Colonists * 1000;
         star.Owner                     = fleet.Owner;
         fleet.Cargo                    = new Cargo();
      }



// ============================================================================
// Scrap a single ship
// ============================================================================

      public static void Scrap(Ship ship, Star star, double amount, 
                               double resources)
      {
         double factor = amount / 100;

         star.ResourcesOnHand.Ironium   = ship.Design.Cost.Ironium   * factor;
         star.ResourcesOnHand.Boranium  = ship.Design.Cost.Boranium  * factor;
         star.ResourcesOnHand.Germanium = ship.Design.Cost.Germanium * factor;
         star.ResourcesOnHand.Energy    = ship.Design.Cost.Energy    * factor;
      }


// ============================================================================
// Scrap a fleet (fleets include starbases). The minerals depositied are:
// 
// Colonisation                        - 75%
// Scrap at a starbase                 - 80%
// Scrap at a planet without a stabase - 33%
// Scrap in space                      - 0%
//
// If the secondary trait Ulitmate Recycling has been selected when you scrap a
// fleet at a starbase, you recover 90% of the minerals and 70% of the
// resources used to produce the fleet. The resources are available or use the
// next year. Scrapping at a planet gives you 45% of the minerals and 35% of
// the resources.These resources are not strictly additive. 
// ============================================================================

      public static void Scrap(Fleet fleet, Star star, bool colonise)
      {
         double amount    = 0;
         Race   race      = ServerState.Data.AllRaces[fleet.Owner] as Race;
         double resources = 0;

         if (race.HasTrait("UR")) 
         {
            if (star != null) 
            {
               if (star.Starbase != null) 
               {
                  amount    = 90;
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

         ServerState.Data.AllFleets.Remove(fleet.Key);
         Message message  = new Message();
         message.Audience = fleet.Owner;
         message.Text     = fleet.Name + " has been scrapped";
         Intel.Data.Messages.Add(message);
      }
   }
}
