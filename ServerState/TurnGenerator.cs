#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.WinForms.Console
{
    using System;    
    using System.Collections.Generic;
    using System.IO;
    
    using Nova.Common;
    using Nova.Common.DataStructures;
    using Nova.Server;

    /// <summary>
    /// Class to process a new turn.
    /// </summary>
    public class TurnGenerator
    {
        private ServerState stateData;
        private ServerState turnData;
        
        private OrderReader orderReader;
        private IntelWriter intelWriter;
        private BattleEngine battleEngine;
        private Bombing bombing;
        private CheckForMinefields checkForMinefields;
        private Invade invade;
        private LayMines layMines;
        private Manufacture manufacture;
        private Scores scores;
        private VictoryCheck victoryCheck;
        private WaypointTasks waypointTasks;
        
        /// <summary>
        /// Construct a turn processor. 
        /// </summary>
        public TurnGenerator(ServerState serverState)
        {
            this.stateData = serverState;
            
            // Now that there is a state, comopose the turn processor.
            // TODO ???: Use dependency injection for this? It would
            // generate a HUGE constructor call... a factory to
            // abstract it perhaps? -Aeglos
            this.orderReader = new OrderReader(this.stateData);            
            this.battleEngine = new BattleEngine(this.stateData, new BattleReport());
            this.bombing = new Bombing(this.stateData);
            this.checkForMinefields = new CheckForMinefields(this.stateData);
            this.invade = new Invade(this.stateData);
            this.layMines = new LayMines(this.stateData);
            this.waypointTasks = new WaypointTasks(this.stateData, this.invade, this.layMines);
            this.manufacture = new Manufacture(this.stateData, this.waypointTasks);
            this.scores = new Scores(this.stateData);
            this.intelWriter = new IntelWriter(this.stateData, this.scores);
            this.victoryCheck = new VictoryCheck(this.stateData, this.scores);
            
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate a new turn by reading in the player turn files to update the master
        /// copy of stars, ships, etc. Then do the processing required to take in the
        /// passage of one year of time and, finally, write out the new turn file.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Generate()
        {
            BackupTurn();

            turnData = orderReader.ReadOrders();
            
            // For now, just copy the new turn right away.
            // TODO: Integrity check the new turn before
            // updating the state (cheats, errors).
            stateData = turnData;
            
            // Do per user operations.
            foreach (Race race in stateData.AllRaces.Values)
            {
                // Does not do much for now, only sets up some basic
                // (but required) variables.
                SanitizeResearch(race);
            }

            // Do all fleet movement and actions 
            // TODO (priority 4) - split this up into waypoint zero and waypoint 1 actions
            foreach (Fleet fleet in stateData.AllFleets.Values)
            {
                ProcessFleet(fleet);
            }
            CleanupFleets();
            

            // Handle star based actions - growth and production
            // TODO (priority 4) - split these up as per Stars! turn order
            // UPDATE May 11: Some of this is updated -Aeglos
            foreach (Star star in stateData.AllStars.Values)
            {
                ProcessStar(star);
            }
                        
            CleanupFleets();

            battleEngine.Run();
            
            CleanupFleets();            

            victoryCheck.Victor();

            stateData.TurnYear++;
            
            intelWriter.WriteIntel();

            // remove old messages, do this last so that the 1st turn intro message is not removed before it is delivered.
            stateData.AllMessages = new List<Message>();

        }

        /// <summary>
        /// Remove fleets that no longer have ships.
        /// This needs to be done after each time the fleet list is processed, as fleets can not be destroyed until the itterator complets.
        /// </summary>
        private void CleanupFleets()
        {
            // create a list of all fleets that have been destroyed
            List<string> destroyedFleets = new List<string>();
            foreach (Fleet fleet in stateData.AllFleets.Values)
            {
                if (fleet.FleetShips.Count == 0)
                {
                    destroyedFleets.Add(fleet.Key);
                }
            }
            foreach (string key in destroyedFleets)
            {
                stateData.AllFleets.Remove(key);
            }

            // And remove stations too.
            List<string> destroyedStations = new List<string>();
            foreach (Star star in stateData.AllStars.Values)
            {
                if (star.Starbase != null && star.Starbase.FleetShips.Count == 0)
                {
                    destroyedStations.Add(star.Name);
                }
            }
            foreach (string key in destroyedStations)
            {
                stateData.AllStars[key].Starbase = null;

            }
            
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy all turn files to a sub-directory prior to generating the new turn.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void BackupTurn()
        {
            // TODO (priority 3) - Add a setting to control the number of backups.
            int currentTurn = stateData.TurnYear;
            string gameFolder = stateData.GameFolder;


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
        private void ProcessStar(Star star)
        {            
            string owner = star.Owner;
            if (owner == null)
            {
                return; // nothing to do for an empty star system.
            }
            Race race = stateData.AllRaces[star.Owner];
            
            star.UpdateMinerals();
            
            // According to the allocated budget submited, update star resources.
            // Note that this sets the allocation for research to zero for all stars
            // which have "contribute only leftover resources to research". This
            // makes those stars be handled after manufacturing.
            int percentage = stateData.AllRaceData[race.Name].ResearchPercentage;
            star.UpdateResearch(percentage);
            star.UpdateResources();
            
            ContributeAllocatedResearch(race, star);
            
            int initialPopulation = star.Colonists;
            star.UpdatePopulation(race);
            int finalPopulation = star.Colonists;

            if (finalPopulation < initialPopulation)
            {
                int died = initialPopulation - finalPopulation;
                Message message = new Message();
                message.Audience = star.Owner;
                message.Text = died.ToString(System.Globalization.CultureInfo.InvariantCulture)
                   + " of your colonists have been killed"
                   + " by the environment on " + star.Name;
                stateData.AllMessages.Add(message);
            }

            manufacture.Items(star);
            
            ContributeLeftoverResearch(race, star);
            
            star.UpdateResearch(percentage);
            star.UpdateResources();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the elapse of one year (turn) for a fleet.
        /// </summary>
        /// <param name="fleet">The fleet to process a turn for.</param>
        /// <returns>false</returns>
        /// ??? (priority 4) - why does this always return false?
        /// ----------------------------------------------------------------------------
        private bool ProcessFleet(Fleet fleet)
        {
            bool destroyed = UpdateFleet(fleet);
            if (destroyed == true)
            {
                return true;
            }

            // See if the fleet is orbiting a star
            foreach (Star star in stateData.AllStars.Values)
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
                stateData.AllMessages.Add(message);
            }

            // See if we need to bomb this place.

            if (fleet.InOrbit != null && fleet.HasBombers)
            {
                bombing.Bomb(fleet, fleet.InOrbit);
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
        private void RegenerateFleet(Fleet fleet)
        {
            if (fleet == null)
            {
                return;
            }

            Star star = fleet.InOrbit;
            // refuel
            if (star != null && star.Owner == fleet.Owner /* TODO (priority 6) or friendly*/ && star.Starbase != null && star.Starbase.CanRefuel)
            {
                fleet.FuelAvailable = fleet.TotalFuelCapacity;
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
                ship.Shields = ship.DesignShield;
                if (repairRate > 0)
                {
                    int repairAmount = Math.Min(ship.DesignArmor * repairRate / 100, 1);
                    ship.Armor += repairAmount;
                    ship.Armor = Math.Min(ship.Armor, ship.DesignArmor);
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
        private bool UpdateFleet(Fleet fleet)
        {
            Race race = stateData.AllRaces[fleet.Owner];

            Waypoint currentPosition = new Waypoint();
            double availableTime = 1.0;

            while (fleet.Waypoints.Count > 0)
            {
                Waypoint thisWaypoint = fleet.Waypoints[0];

                Fleet.TravelStatus result = fleet.Move(ref availableTime, race);
                bool destroyed = checkForMinefields.Check(fleet);

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
                    Star star;
                    stateData.AllStars.TryGetValue(thisWaypoint.Destination, out star);

                    if (star != null)
                    {
                        fleet.InOrbit = star;
                        star.OrbitingFleets = true;
                    }

                    waypointTasks.Perform(fleet, thisWaypoint);

                    if (thisWaypoint.Task != "Lay Mines")
                    {
                        thisWaypoint.Task = "None";
                    }
                }

                currentPosition = fleet.Waypoints[0];
                fleet.Waypoints.RemoveAt(0);

            }

            fleet.Waypoints.Insert(0, currentPosition);

            if (fleet.Waypoints.Count > 1)
            {
                Waypoint nextWaypoint = fleet.Waypoints[1];

                double dx = fleet.Position.X - nextWaypoint.Position.X;
                double dy = fleet.Position.Y - nextWaypoint.Position.Y;
                fleet.Bearing = ((Math.Atan2(dy, dx) * 180) / Math.PI) + 90;
            }

            return false;
        }
        
        private bool SanitizeResearch(Race race)
        {
            // Reset the list of new levels gained.
            stateData.AllRaceData[race.Name].ResearchLevelsGained.Zero();
            
            return true;
            
        }
        
        /// <summary>
        /// Contributes allocated research from the star.
        /// </summary>
        /// <param name="race">Star's owner race</param>
        /// <param name="star">Star to process</param>
        /// <remarks>
        /// Note that stars which contribute only leftovers are not accounted for.
        /// </remarks>
        private void ContributeAllocatedResearch(Race race, Star star)
        {   
            // Paranoia
            string owner = star.Owner;
            if (owner == null || owner != race.Name)
            {
                return;
            }

            RaceData playerData = stateData.AllRaceData[race.Name];

            TechLevel targetAreas = playerData.ResearchTopics;
            TechLevel.ResearchField targetArea = TechLevel.ResearchField.Energy; // default to Energy.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (targetAreas[area] == 1)
                {
                    targetArea = area;
                    break;        
                }
            }
            
            // Consume resources for research for added paranoia.
            playerData.ResearchResources[targetArea] = playerData.ResearchResources[targetArea] + star.ResearchAllocation;
            star.ResearchAllocation = 0;            
            
            TechLevel newLevels = stateData.AllRaceData[race.Name].ResearchLevelsGained;
            
            while (true)
            {
                int cost = Research.Cost(targetArea, race, playerData.ResearchLevels, playerData.ResearchLevels[targetArea] + 1);
                
                if (playerData.ResearchResources[targetArea] >= cost)
                {
                    playerData.ResearchLevels[targetArea] = playerData.ResearchLevels[targetArea] + 1;
                    newLevels[targetArea] = newLevels[targetArea] + 1;
                }
                else
                {
                    break;
                }
            }
        }
        
        private void ContributeLeftoverResearch(Race race, Star star)
        {
            // Paranoia
            string owner = star.Owner;
            if (owner == null || owner != race.Name)
            {
                return;
            }
            
            RaceData playerData = stateData.AllRaceData[race.Name];
            
            TechLevel targetAreas = playerData.ResearchTopics;
            TechLevel.ResearchField targetArea = TechLevel.ResearchField.Energy; // default to Energy.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (targetAreas[area] == 1)
                {
                    targetArea = area;
                    break;        
                }
            }
            
            // Consume resources for research for added paranoia.
            playerData.ResearchResources[targetArea] = playerData.ResearchResources[targetArea] + star.ResourcesOnHand.Energy;
            star.ResourcesOnHand.Energy = 0;
            
            TechLevel newLevels = stateData.AllRaceData[race.Name].ResearchLevelsGained;
            
            while (true)
            {
                int cost = Research.Cost(targetArea, race, playerData.ResearchLevels, playerData.ResearchLevels[targetArea] + 1);
                
                if (playerData.ResearchResources[targetArea] >= cost)
                {
                    playerData.ResearchLevels[targetArea] = playerData.ResearchLevels[targetArea] + 1;
                    newLevels[targetArea] = newLevels[targetArea] + 1;
                }
                else
                {
                  break;
                }
            }
        }
    }
}
