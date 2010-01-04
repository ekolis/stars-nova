// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module is invoked when New Turn is selected in the Nova Console. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System;

namespace NovaConsole {


// ============================================================================
// Class to process a new turn.
// ============================================================================

   public class NewTurn
   {
      private static BinaryFormatter Formatter = new BinaryFormatter();
      private static ConsoleState    StateData = null;


// ============================================================================
// Generate a new turn by reading in the player turn files to update the master
// copy of stars, ships, etc. Then do the processing required to take in the
// passage of one year of time and, finally, write out the new turn file.
// ============================================================================

      public static void Generate()
      {
         StateData = ConsoleState.Data;

         StateData.AllTechLevels.Clear();

         foreach (Race Race in StateData.AllRaces.Values) {
            ReadPlayerTurn(Race);
         }

         // remove any destroyed fleets - TODO should be done when they are destroyed to allow bombing when a base is destroyed
         ArrayList destroyedFleets = new ArrayList();
         foreach (Fleet fleet in ConsoleState.Data.AllFleets.Values)
         {
             if (fleet.FleetShips.Count == 0)
                 destroyedFleets.Add(fleet.Key);
         }
         foreach (String name in destroyedFleets)
         {
             ConsoleState.Data.AllFleets.Remove(name);
         }

         BattleEngine.Run();

         // remove any destroyed fleets - TODO should be done when they are destroyed to allow bombing when a base is destroyed
         destroyedFleets = new ArrayList();
         foreach (Fleet fleet in ConsoleState.Data.AllFleets.Values)
         {
             if (fleet.FleetShips.Count == 0)
                 destroyedFleets.Add(fleet.Key);
         }
         foreach (String name in destroyedFleets)
         {
             ConsoleState.Data.AllFleets.Remove(name);
         }

         // remove destroyed space stations - TODO should be done when they are destroyed
         ArrayList destroyedStations = new ArrayList();
         foreach (Star star in ConsoleState.Data.AllStars.Values)
         {
             if (star.Starbase != null && star.Starbase.FleetShips.Count == 0)
                 destroyedStations.Add(star.Name);
         }
         foreach (String name in destroyedStations)
         {
             ((Star)(ConsoleState.Data.AllStars[name])).Starbase = null;
             
         }
          



         VictoryCheck.Victor();

         StateData.TurnYear++;
         Turn.BuildAndSave();
      }


// ============================================================================
// Read in the player turns and update the relevant data stores with the
// (possibly) new values. There is a special case where a player may decide not
// to join the first turn so their will be no turn. So, check for this
// condition and generate a sensible error report instead of just letting an
// exception be thrown.
// ============================================================================

      private static void ReadPlayerTurn(Race race)
      {
         RaceTurn    inputTurn = new RaceTurn();
         string      fileName  = Path.Combine(StateData.GameFolder, race.Name + ".turn");
         
         // Check for the special condition mentioned in the header.

         if (File.Exists(fileName) == false) {
            Report.FatalError(  "There is no turn file for the " + race.Name 
                              + " race.\n\nYou may only generate the first "
                              + "turn of a game when all race turn files are "
                              + "present.");
         }

         FileStream  turnFile  = new FileStream(fileName, FileMode.Open);

         inputTurn = Formatter.Deserialize(turnFile) as RaceTurn;
         turnFile.Close();

         foreach (Design design in inputTurn.RaceDesigns) {
            StateData.AllDesigns[design.Key] = design;
         }

         foreach (string fleetKey in inputTurn.DeletedFleets) {
            StateData.AllFleets.Remove(fleetKey);
         }

         foreach (string designKey in inputTurn.DeletedDesigns) {
            StateData.AllDesigns.Remove(designKey);
         }

         foreach (Fleet fleet in inputTurn.RaceFleets) {
            StateData.AllFleets[fleet.Key] = fleet;
            bool destroyed = ProcessFleet(fleet);
            if (destroyed) {
               StateData.AllFleets.Remove(fleet.Key);
            }
         }

         foreach (Star star in inputTurn.RaceStars) {
            StateData.AllStars[star.Name] = star;
            ProcessStar(star, race);
         }

         StateData.AllRaceData[race.Name]   = inputTurn.PlayerData;
         StateData.AllTechLevels[race.Name] = inputTurn.TechLevel;
      }


// ============================================================================
// Process the elapse of one year (turn) for a star.
// ============================================================================

      private static void ProcessStar(Star star, Race race)
      {
         int initialPopulation = star.Colonists;
         star.Update(race);
         int finalPopulation   = star.Colonists;

         if (finalPopulation < initialPopulation) {
            int     died     = initialPopulation - finalPopulation;
            Message message  = new Message();
            message.Audience = star.Owner;
            message.Text     = died.ToString() 
               + " of your colonists have been killed"
               + " by the environment on " + star.Name;
            GlobalTurn.Data.Messages.Add(message);
         }

         Manufacture.Items(star);
      }


// ============================================================================
// Process the elapse of one year (turn) for a fleet.
// ============================================================================

      private static bool ProcessFleet(Fleet fleet)
      {
         bool destroyed = UpdateFleet(fleet);
         if (destroyed == true) return true;

         // If we are in orbit around a planet that we own, and it has a
         // starbase, refuel and repair the fleet.

         Waypoint location = fleet.Waypoints[0] as Waypoint;
         Star     star     = StateData.AllStars[location.Destination] as Star;

         if (star != null) {
            fleet.InOrbit = star;
         }

         RegenerateFleet(fleet);


         // Check for no fuel.

         if (fleet.FuelAvailable == 0 && ! fleet.IsStarbase) {
            Message message  = new Message();
            message.Audience = fleet.Owner;
            message.Text     = fleet.Name + " has ran out of fuel.";
            GlobalTurn.Data.Messages.Add(message);
         }

         // See if we need to bomb this place.

         if (fleet.InOrbit != null && fleet.HasBombers) {
            Bombing.Bomb(fleet, star);
         }

         return false;
      }


// ============================================================================
// Refuel and Repair
// To refuel a ship must be in orbit of a planet with a starbase with a dock capacity > 0.
// Repair is:
// 0% while bombing (or orbiting an enemy planet with attack orders).
// 1% moving through space
// 2% stopped in space
// 3% orbiting, but not bombing an enemy planet
// 5% orbiting own planet without a starbase.
// 8% orbiting own planet with starbase but 0 dock.
// 20 orbiting own planet with dock.
// +repair% if stopped or orbiting.
// A starbase is not counted towards repairs if it is under attack. TODO
// ============================================================================

      private static void RegenerateFleet(Fleet fleet)
      {
          if (fleet == null) return;

          Star star = fleet.InOrbit;
          // refuel
          if (star != null && star.Owner == fleet.Owner /* TODO or friendly*/ && star.Starbase != null && star.Starbase.CanRefuel)
          {
              fleet.FuelAvailable = fleet.FuelCapacity;
          }

          // repair, TODO skip if fleet has no damage, if that is more efficient 
          
          int repairRate = 0;
          if (star != null)
          {
              if (star.Owner == fleet.Owner /* TODO or friend */)
              {
                  if (star.Starbase != null /* TODO and not under attack */)
                  {
                      if (star.Starbase.CanRefuel)
                      {
                          // orbiting own planet with dock.
                          repairRate = 20;
                      }
                      else
                      {
                          // orbiting own planet with starbase but 0 dock.
                          repairRate = 8; 
                      }
                  }
                  else
                  {
                      // friendly planet, no base
                      repairRate = 5;
                  }
              }
              else
              {
                  // TODO 0% if bombing
                  // orbiting, but not bombing an enemy planet
                  repairRate = 3;

              }
          }
          else
          {
              if (fleet.Waypoints.Count == 0) // TODO - check if a stopped fleet has 1 or 0 waypoints
              {
                  // stopped in space
                  repairRate = 2;
              }
              else
              {
                  // moving through space
                  repairRate = 1;
              }
          }

         
          
         foreach (Ship ship in fleet.FleetShips) {
             ship.Shields = ship.Design.Shield;
             if (repairRate > 0)
             {
                 int repairAmount = Math.Min(ship.Design.Armor * repairRate / 100, 1);
                 ship.Armor += repairAmount;
                 ship.Armor = Math.Min(ship.Armor, ship.Design.Armor);
             }
         }
      }



// ============================================================================
// Update the status of a fleet moving through waypoints and performing any
// specified waypoint tasks.
// ============================================================================

      private static bool UpdateFleet(Fleet fleet)
      {
         Race race = ConsoleState.Data.AllRaces[fleet.Owner] as Race;

         Waypoint currentPosition = new Waypoint();
         double   availableTime   = 1.0;

         while (fleet.Waypoints.Count > 0) {
            Waypoint thisWaypoint = fleet.Waypoints[0] as Waypoint;

            Fleet.TravelStatus result = fleet.Move(ref availableTime, race);
            bool destroyed            = CheckForMinefields.Check(fleet);

            if (destroyed == true) {
               return true;
            }

            if (result == Fleet.TravelStatus.InTransit) {
               currentPosition.Position    = fleet.Position;
               currentPosition.Task        = "None";
               currentPosition.Destination = "Space at " + fleet.Position;
               currentPosition.WarpFactor  = thisWaypoint.WarpFactor;
               break;
            }
            else {
               Star star=StateData.AllStars[thisWaypoint.Destination] as Star;

               if (star != null) {
                  fleet.InOrbit = star;
               }

               WaypointTasks.Perform(fleet, thisWaypoint);
               thisWaypoint.Task =  "None";
            }

            currentPosition = fleet.Waypoints[0] as Waypoint;
            fleet.Waypoints.RemoveAt(0);

         }

         fleet.Waypoints.Insert(0, currentPosition);

         if (fleet.Waypoints.Count > 1) {
            Waypoint nextWaypoint = fleet.Waypoints[1] as Waypoint;

            double dx     = fleet.Position.X - nextWaypoint.Position.X;
            double dy     = fleet.Position.Y - nextWaypoint.Position.Y;
            fleet.Bearing = ((Math.Atan2(dy, dx) * 180) / Math.PI) + 90;
         }

         return false;
      }



   }
}
