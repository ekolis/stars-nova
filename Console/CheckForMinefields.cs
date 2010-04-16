// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Check to see if a fleet is in an enemy Minefield and inflict appropriate
// damage.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public Static License version 2 as published by the
// Free Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using NovaCommon;
using NovaServer;

namespace NovaConsole
{

   public static class CheckForMinefields
   {
      private static Random RandomNumber = new Random();

      public static bool Check(Fleet fleet)
      { 
         foreach (Minefield Minefield in 
                  ServerState.Data.AllMinefields.Values) {

            if (IsInField(fleet, Minefield)) {
               bool hit = CheckForHit(fleet, Minefield);
               if (hit) {
                  InflictDamage(fleet, Minefield);
               }
            }
            
            // Minefields dedcay 1% each year. Fields of less than 10 mines are
            // just not worth bothering about.

            Minefield.NumberOfMines -= Minefield.NumberOfMines / 100;
            if (Minefield.NumberOfMines <= 10) {
               ServerState.Data.AllMinefields.Remove(Minefield);
            }
         }

         return false;
      }


// ============================================================================
// Determine if a fleet is within a Minefield. The fleet is inside the
// circle if the distance between the field and the center of the field is
// less than the radius of the field.
// ============================================================================

      private static bool IsInField(Fleet fleet, Minefield Minefield) 
      {

         // If we are travelling at a "safe" speed we can just pretend we are
         // not in a Minefield.

         if (fleet.Speed <= Minefield.SafeSpeed) return false;

         double distance = PointUtilities.Distance(fleet.Position, 
                                                   Minefield.Position);

         if (distance < Minefield.Radius) {
            return true;
         }

         return false;
      }


// ============================================================================
// The probability of hitting a mine is 0.3% per light year traveled for each
// warp over the safe speed.
//
// Example: A fleet traveling at Warp 9 has a 1.5% chance per light year
// traveled in a turn.  Traveling 10 light years through the Minefield that
// turn, the fleet has a 10.5% chance of triggering a mine.
// ============================================================================

      private static bool CheckForHit(Fleet fleet, Minefield Minefield)
      {
         // Calculate how long we are going to be in the Minefield. This is the
         // lesser of the distance to the next waypoint and the radius of the
         // field.

         Point currentPosition   = fleet.Position;
         Waypoint targetWaypoint = fleet.Waypoints[0] as Waypoint;
         Point    targetPosition = targetWaypoint.Position;

         double travelDistance   = PointUtilities.Distance(currentPosition, 
                                                           targetPosition);
         if (Minefield.Radius > (int) travelDistance) {
            travelDistance = Minefield.Radius;
         }

         double speeding    = fleet.Speed - Minefield.SafeSpeed;
         double probability = (0.03 * travelDistance * speeding) * 100;
         double dice        = RandomNumber.Next(0, 100);

         if (dice < probability) {
            return true;
         }

         return false;
      }


// ============================================================================
// We've hit a mine. Inflict appropriate damage to the fleet and bring it to a
// stop. If all ships are gone destroy the fleet.
//
// Let's start with the simplest algoritm:
//
// 5 destroyers take 500dp damage = 100dp each = 50dp to armor, 50dp to shields
// (absorbed)
// ============================================================================

      private static void InflictDamage(Fleet fleet, Minefield Minefield)
      {
         int shipDamage = 100 / 2;
         int shipsLost  = 0;
         fleet.Speed    = 0;

         ArrayList shipsToRemove = new ArrayList();

         foreach (Ship ship in fleet.FleetShips) {
            ship.Armor -= shipDamage;

            if (ship.Design.Armor < 0) {
               shipsToRemove.Add(ship);
               shipsLost++;
            }
         }
         
         foreach (Ship removeShip in shipsToRemove) {
            fleet.FleetShips.Remove(removeShip);
         }
         
         Message message  = new Message();
         message.Audience = fleet.Owner;
         message.Event    = "Minefield";
         message.Text="Fleet " + fleet.Name 
            + " has hit a Minefield." + "\n\n";
         
         if (shipsLost == 0) {
            message.Text += "None of your ships were destroyed.";
            fleet.Speed   = 0;
         }   
         else if (fleet.FleetShips.Count != 0) {
            message.Text += shipsLost.ToString(System.Globalization.CultureInfo.InvariantCulture) 
               + " of your ships were destroyed.\n";
         }
         else {
            message.Text += "All of your ships were destroyed.\n";
            message.Text += "You lost this fleet.";
            ServerState.Data.AllFleets.Remove(fleet.Key);
         }

         ServerState.Data.AllMessages.Add(message);
      }
   }      
} 
