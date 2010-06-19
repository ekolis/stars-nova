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
// This module is invoked when NewTurn is selected in the Nova Console.
// ===========================================================================
#endregion

using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System;

using Nova.Common;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class to process a new turn.
    /// </summary>
    public static class ProcessTurn
    {
        private static ServerState StateData = null;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate a new turn by reading in the player turn files to update the master
        /// copy of stars, ships, etc. Then do the processing required to take in the
        /// passage of one year of time and, finally, write out the new turn file.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void Generate()
        {
            StateData = ServerState.Data;

            BackupTurn();

            OrderReader.ReadOrders();

            foreach (Fleet fleet in StateData.AllFleets.Values)
            {
                bool destroyed = ProcessFleet(fleet);
                if (destroyed)
                {
                    StateData.AllFleets.Remove(fleet.Key);
                }
            }

            foreach (Star star in StateData.AllStars.Values)
            {
                ProcessStar(star);
            }

            // ------------------------------------------------------------------------------------------------------------------------------
            // FIXME (priority 6) - Fix up the removal of destroyed fleets and space stations.
            // currently this is done multiple times to ensure the fleets are removed. This is done out of ignorance.
            // need to analyse the way the turn is procecessed and ensure fleets are removed as soon as possible after being destroyed.
            // NB: may need to generate player messages or some other action before the fleet removed from the game.

            // First shot at removing fleets in order to make sure dead fleets are gone before running the battle engine.
            ArrayList destroyedFleets = new ArrayList();
            foreach (Fleet fleet in ServerState.Data.AllFleets.Values)
            {
                if (fleet.FleetShips.Count == 0)
                    destroyedFleets.Add(fleet.Key);
            }
            foreach (string key in destroyedFleets)
            {
                ServerState.Data.AllFleets.Remove(key);
            }

            BattleEngine.Run();

            // Second shot at removing fleets post battle.
            destroyedFleets = new ArrayList();
            foreach (Fleet fleet in ServerState.Data.AllFleets.Values)
            {
                if (fleet.FleetShips.Count == 0)
                    destroyedFleets.Add(fleet.Key);
            }
            foreach (string key in destroyedFleets)
            {
                ServerState.Data.AllFleets.Remove(key);
            }

            // And remove stations too.
            ArrayList destroyedStations = new ArrayList();
            foreach (Star star in ServerState.Data.AllStars.Values)
            {
                if (star.Starbase != null && star.Starbase.FleetShips.Count == 0)
                    destroyedStations.Add(star.Name);
            }
            foreach (string key in destroyedStations)
            {
                ((Star)ServerState.Data.AllStars[key]).Starbase = null;

            }

            // END OF FIX ME--------------------------------------------------------------------------------------------------------------------------

            VictoryCheck.Victor();

            StateData.TurnYear++;
            IntelWriter.WriteIntel();

            // remove old messages, do this last so that the 1st turn intro message is not removed before it is delivered.
            StateData.AllMessages = new ArrayList();

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy all turn files to a sub-directory prior to generating the new turn.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private static void BackupTurn()
        {
            // TODO (priority 3) - Add a setting to control the number of backups.
            int currentTurn = ServerState.Data.TurnYear;
            string gameFolder = ServerState.Data.GameFolder;


            try
            {
                string backupFolder = Path.Combine(gameFolder, currentTurn.ToString());
                DirectoryInfo source = new DirectoryInfo(gameFolder);
                DirectoryInfo target = new DirectoryInfo(backupFolder);

                // Check if the target directory exists, if not, create it.
                if (Directory.Exists(target.FullName) == false)
                {
                    Directory.CreateDirectory(target.FullName);
                }

                // Copy each file into it’s new directory.
                foreach (FileInfo fi in source.GetFiles())
                {
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }


            }
            catch (Exception e)
            {
                Report.Error("There was a problem backing up the game files: " + Environment.NewLine + e.Message);
            }


        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the elapse of one year (turn) for a star.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> to process.</param>
        /// ----------------------------------------------------------------------------
        private static void ProcessStar(Star star)
        {
            string owner = star.Owner;
            if (owner == null) return; // nothing to do for an empty star system.
            Race race = StateData.AllRaces[star.Owner] as Race;
            int initialPopulation = star.Colonists;
            star.Update(race);
            int finalPopulation = star.Colonists;

            if (finalPopulation < initialPopulation)
            {
                int died = initialPopulation - finalPopulation;
                Message message = new Message();
                message.Audience = star.Owner;
                message.Text = died.ToString(System.Globalization.CultureInfo.InvariantCulture)
                   + " of your colonists have been killed"
                   + " by the environment on " + star.Name;
                ServerState.Data.AllMessages.Add(message);
            }

            Manufacture.Items(star);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the elapse of one year (turn) for a fleet.
        /// </summary>
        /// <param name="fleet">The fleet to process a turn for.</param>
        /// <returns>false</returns>
        /// ??? (priority 4) - why does this always return false?
        /// ----------------------------------------------------------------------------
        private static bool ProcessFleet(Fleet fleet)
        {
            bool destroyed = UpdateFleet(fleet);
            if (destroyed == true) return true;

            // See if the fleet is orbiting a star
            foreach (Star star in StateData.AllStars.Values)
            {
                if (star.Position.X == fleet.Position.X && star.Position.Y == fleet.Position.Y)
                {
                    fleet.InOrbit = star;
                }
            }

            // refuel/repair
            RegenerateFleet(fleet);

            // Check for no fuel.

            if (fleet.FuelAvailable == 0 && !fleet.IsStarbase)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = fleet.Name + " has ran out of fuel.";
                ServerState.Data.AllMessages.Add(message);
            }

            // See if we need to bomb this place.

            if (fleet.InOrbit != null && fleet.HasBombers)
            {
                Bombing.Bomb(fleet, fleet.InOrbit);
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Refuel and Repair
        /// </summary>
        /// <param name="fleet"></param>
        /// <remarks>
        /// To refuel a ship must be in orbit of a planet with a starbase with a dock capacity > 0.
        /// Repair is:
        /// 0% while bombing (or orbiting an enemy planet with attack orders).
        /// 1% moving through space
        /// 2% stopped in space
        /// 3% orbiting, but not bombing an enemy planet
        /// 5% orbiting own planet without a starbase.
        /// 8% orbiting own planet with starbase but 0 dock.
        /// 20 orbiting own planet with dock.
        /// +repair% if stopped or orbiting.
        /// TODO (priority 3) - A starbase is not counted towards repairs if it is under attack. 
        /// TODO (priority 3) - reference where these rules are from.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private static void RegenerateFleet(Fleet fleet)
        {
            if (fleet == null) return;

            Star star = fleet.InOrbit;
            // refuel
            if (star != null && star.Owner == fleet.Owner /* TODO (priority 6) or friendly*/ && star.Starbase != null && star.Starbase.CanRefuel)
            {
                fleet.FuelAvailable = fleet.FuelCapacity;
            }

            // repair, TODO (priority 3) skip if fleet has no damage, if that is more efficient 

            int repairRate = 0;
            if (star != null)
            {
                if (star.Owner == fleet.Owner /* TODO (priority 6) or friend */)
                {
                    if (star.Starbase != null /* TODO (priority 6) and not under attack */)
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
                    // TODO (priority 6) 0% if bombing
                    // orbiting, but not bombing an enemy planet
                    repairRate = 3;

                }
            }
            else
            {
                // TODO (priority 4) - check if a stopped fleet has 1 or 0 waypoints
                if (fleet.Waypoints.Count == 0)
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



            foreach (Ship ship in fleet.FleetShips)
            {
                ship.Shields = ship.Design.Shield;
                if (repairRate > 0)
                {
                    int repairAmount = Math.Min(ship.Design.Armor * repairRate / 100, 1);
                    ship.Armor += repairAmount;
                    ship.Armor = Math.Min(ship.Armor, ship.Design.Armor);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the status of a fleet moving through waypoints and performing any
        /// specified waypoint tasks.
        /// </summary>
        /// <param name="fleet"></param>
        /// <returns>false</returns>
        /// ??? (priority 4) - why does this always return false
        /// ----------------------------------------------------------------------------
        private static bool UpdateFleet(Fleet fleet)
        {
            Race race = ServerState.Data.AllRaces[fleet.Owner] as Race;

            Waypoint currentPosition = new Waypoint();
            double availableTime = 1.0;

            while (fleet.Waypoints.Count > 0)
            {
                Waypoint thisWaypoint = fleet.Waypoints[0] as Waypoint;

                Fleet.TravelStatus result = fleet.Move(ref availableTime, race);
                bool destroyed = CheckForMinefields.Check(fleet);

                if (destroyed == true)
                {
                    return true;
                }

                if (result == Fleet.TravelStatus.InTransit)
                {
                    currentPosition.Position = fleet.Position;
                    currentPosition.Task = "None";
                    currentPosition.Destination = "Space at " + fleet.Position;
                    currentPosition.WarpFactor = thisWaypoint.WarpFactor;
                    break;
                }
                else
                {
                    Star star = StateData.AllStars[thisWaypoint.Destination] as Star;

                    if (star != null)
                    {
                        fleet.InOrbit = star;
                        star.OrbitingFleets = true;
                    }

                    WaypointTasks.Perform(fleet, thisWaypoint);

                    if (thisWaypoint.Task != "Lay Mines")
                    {
                        thisWaypoint.Task = "None";
                    }
                }

                currentPosition = fleet.Waypoints[0] as Waypoint;
                fleet.Waypoints.RemoveAt(0);

            }

            fleet.Waypoints.Insert(0, currentPosition);

            if (fleet.Waypoints.Count > 1)
            {
                Waypoint nextWaypoint = fleet.Waypoints[1] as Waypoint;

                double dx = fleet.Position.X - nextWaypoint.Position.X;
                double dy = fleet.Position.Y - nextWaypoint.Position.Y;
                fleet.Bearing = ((Math.Atan2(dy, dx) * 180) / Math.PI) + 90;
            }

            return false;
        }

    }
}
