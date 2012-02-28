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

namespace Nova.Server
{
    using System;    
    using System.Collections.Generic;
    using System.IO;
    
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.DataStructures;
    using Nova.Server.TurnSteps;

    /// <summary>
    /// Class to process a new turn.
    /// </summary>
    public class TurnGenerator
    {
        private ServerData serverState;
        
        private SortedList<int, ITurnStep> turnSteps;
        
        // Used to order turn steps.
        private const int FIRSTSTEP = 00;
        private const int STARSTEP = 12;
        private const int SCANSTEP = 99;
        
        // TODO: (priority 5) refactor all these into ITurnStep(s).
        private OrderReader orderReader;
        private IntelWriter intelWriter;
        private BattleEngine battleEngine;
        private Bombing bombing;
        private CheckForMinefields checkForMinefields;
        private Manufacture manufacture;
        private Scores scores;
        private VictoryCheck victoryCheck;
        private WaypointTasks waypointTasks;
        
        /// <summary>
        /// Construct a turn processor. 
        /// </summary>
        public TurnGenerator(ServerData serverState)
        {
            this.serverState = serverState;
            
            turnSteps = new SortedList<int, ITurnStep>();
            
            // Now that there is a state, comopose the turn processor.
            // TODO ??? (priority 4): Use dependency injection for this? It would
            // generate a HUGE constructor call... a factory to
            // abstract it perhaps? -Aeglos
            orderReader = new OrderReader(this.serverState);
            battleEngine = new BattleEngine(this.serverState, new BattleReport());
            bombing = new Bombing(this.serverState);
            checkForMinefields = new CheckForMinefields(this.serverState);
            waypointTasks = new WaypointTasks(this.serverState);
            manufacture = new Manufacture(this.serverState);
            scores = new Scores(this.serverState);
            intelWriter = new IntelWriter(this.serverState, this.scores);
            victoryCheck = new VictoryCheck(this.serverState, this.scores);
            
            turnSteps.Add(FIRSTSTEP, new FirstStep());
            turnSteps.Add(SCANSTEP, new ScanStep());
            turnSteps.Add(STARSTEP, new StarUpdateStep());
        }

        /// <summary>
        /// Generate a new turn by reading in the player turn files to update the master
        /// copy of stars, ships, etc. Then do the processing required to take in the
        /// passage of one year of time and, finally, write out the new turn file.
        /// </summary>
        public void Generate()
        {
            BackupTurn();

            // For now, just copy the command stacks right away.
            // TODO (priority 6): Integrity check the new turn before
            // updating the state (cheats, errors).
            orderReader.ReadOrders();
            
            ParseCommands();

            // Do all fleet movement and actions 
            // TODO (priority 4) - split this up into waypoint zero and waypoint 1 actions
            foreach (Fleet fleet in serverState.AllFleets.Values)
            {
                ProcessFleet(fleet);
            }
            CleanupFleets();

            // Handle star based actions - growth and production
            // TODO (priority 4) - split these up as per Stars! turn order
            // UPDATE May 11: Some of this is updated -Aeglos
            foreach (Star star in serverState.AllStars.Values)
            {                
                ProcessStar(star);
            }
                        
            CleanupFleets();

            serverState.AllBattles.Clear(); // remove battle from old turns
            battleEngine.Run();
            
            CleanupFleets();            

            victoryCheck.Victor();

            serverState.TurnYear++;
            
            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {
                empire.TurnYear = serverState.TurnYear;
                empire.TurnSubmitted = false;
            }
                       
            foreach (ITurnStep turnStep in turnSteps.Values)
            {
                turnStep.Process(serverState);    
            }
            
            intelWriter.WriteIntel();

            // remove old messages, do this last so that the 1st turn intro message is not removed before it is delivered.
            serverState.AllMessages = new List<Message>();
            
            CleanupOrders();
        }

        /// <summary>
        /// Validates and applies all commands sent by the clients and read for this turn.
        /// </summary>
        private void ParseCommands()
        {
            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {
                if (!serverState.AllCommands.ContainsKey(empire.Id))
                {
                    continue;   
                }
                
                foreach (ICommand command in serverState.AllCommands[empire.Id])
                {
                    if (command.isValid(empire))
                    {
                        command.ApplyToState(empire);
                    }
                    else
                    {
                        // Flag as cheater or something?
                    }
                }
                
                foreach (Fleet fleet in empire.OwnedFleets.Values)
                {
                    serverState.AllFleets[fleet.Key] = fleet;
                }
                
                foreach (Star star in empire.OwnedStars.Values)
                {
                    serverState.AllStars[star.Key] = star;
                }
            }
        }
        
        /// <summary>
        /// Remove fleets that no longer have ships.
        /// This needs to be done after each time the fleet list is processed, as fleets can not be destroyed until the itterator complets.
        /// </summary>
        private void CleanupFleets()
        {
            // create a list of all fleets that have been destroyed
            List<long> destroyedFleets = new List<long>();
            foreach (Fleet fleet in serverState.AllFleets.Values)
            {
                if (fleet.FleetShips.Count == 0)
                {
                    destroyedFleets.Add(fleet.Key);
                }
            }
            foreach (int key in destroyedFleets)
            {
                serverState.AllFleets.Remove(key);
            }

            // And remove stations too.
            List<string> destroyedStations = new List<string>();
            foreach (Star star in serverState.AllStars.Values)
            {
                if (star.Starbase != null && star.Starbase.FleetShips.Count == 0)
                {
                    destroyedStations.Add(star.Name);
                }
            }
            foreach (string key in destroyedStations)
            {
                serverState.AllStars[key].Starbase = null;

            }            
        }
        
        /// <summary>
        /// Delete order files, done after turn generation.
        /// </summary>
        private void CleanupOrders()
        {
            // Delete orders on turn generation.
            // Copy each file into it’s new directory.
            DirectoryInfo source = new DirectoryInfo(serverState.GameFolder);
            foreach (FileInfo fi in source.GetFiles())
            {
                if (fi.Name.ToLower().EndsWith(Global.OrdersExtension))
                {
                    File.Delete(fi.FullName);
                }
            }
        }

        /// <summary>
        /// Copy all turn files to a sub-directory prior to generating the new turn.
        /// </summary>
        private void BackupTurn()
        {
            // TODO (priority 3) - Add a setting to control the number of backups.
            int currentTurn = serverState.TurnYear;
            string gameFolder = serverState.GameFolder;


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

        /// <summary>
        /// Process the elapse of one year (turn) for a star.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> to process.</param>
        private void ProcessStar(Star star)
        {            
            if (star.Owner == Global.Nobody)
            {
                return; // nothing to do for an empty star system.
            }

            manufacture.Items(star);           
        }

        /// <summary>
        /// Process the elapse of one year (turn) for a fleet.
        /// </summary>
        /// <param name="fleet">The fleet to process a turn for.</param>
        /// <returns>Always returns false.</returns>
        private bool ProcessFleet(Fleet fleet)
        {
            if (fleet == null)
            {
                return true;
            }
            
            bool destroyed = UpdateFleet(fleet);
            if (destroyed == true)
            {
                return true;
            }

            // See if the fleet is orbiting a star
            fleet.InOrbit = null;
            foreach (Star star in serverState.AllStars.Values)
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
                serverState.AllMessages.Add(message);
            }

            // See if we need to bomb this place.

            if (fleet.InOrbit != null && fleet.HasBombers)
            {
                bombing.Bomb(fleet, serverState.AllStars[fleet.InOrbit.Name]);
            }

            // ??? (priority 4) - why does this always return false?
            return false;
        }

        /// <summary>
        /// Refuel and Repair.
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
        private void RegenerateFleet(Fleet fleet)
        {
            if (fleet == null)
            {
                return;
            }
            
            Star star = null;
            
            if (fleet.InOrbit != null)
            {
                star = serverState.AllStars[fleet.InOrbit.Name];
            }
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

        /// <summary>
        /// Update the status of a fleet moving through waypoints and performing any
        /// specified waypoint tasks.
        /// </summary>
        /// <param name="fleet"></param>
        /// <returns>Always returns false.</returns>
        private bool UpdateFleet(Fleet fleet)
        {
            Race race = serverState.AllEmpires[fleet.Owner].Race;

            Waypoint currentPosition = new Waypoint();
            double availableTime = 1.0;
            Random rand = new Random(); // FIXME (priority 4) - There should be only one random number source for the server.

            while (fleet.Waypoints.Count > 0)
            {
                Waypoint thisWaypoint = fleet.Waypoints[0];

                Fleet.TravelStatus fleetMoveResult;

                // Check for Cheap Engines failing to start
                if (thisWaypoint.WarpFactor > 6 && race.Traits.Contains("CE") && rand.Next(10) == 1)
                {
                    // Engines fail
                    Message message = new Message();
                    message.Audience = fleet.Owner;
                    message.Text = "Fleet " + fleet.Name + "'s engines failed to start. Fleet has not moved this turn.";
                    message.Type = "Cheap Engines";
                    message.Event = this;
                    serverState.AllMessages.Add(message);
                    fleetMoveResult = Fleet.TravelStatus.InTransit;
                }
                else
                {
                     fleetMoveResult = fleet.Move(ref availableTime, race);
                }

                bool destroyed = checkForMinefields.Check(fleet);

                if (destroyed == true)
                {
                    return true;
                }

                if (fleetMoveResult == Fleet.TravelStatus.InTransit)
                {
                    currentPosition.Position = fleet.Position;
                    currentPosition.Task =  WaypointTask.None;
                    currentPosition.Destination = "Space at " + fleet.Position;
                    currentPosition.WarpFactor = thisWaypoint.WarpFactor;
                    break;
                }
                else
                {
                    Star star;
                    serverState.AllStars.TryGetValue(thisWaypoint.Destination, out star);

                    if (star != null)
                    {
                        fleet.InOrbit = star;
                    }

                    waypointTasks.Perform(fleet, thisWaypoint);

                    if (thisWaypoint.Task != WaypointTask.LayMines)
                    {
                        thisWaypoint.Task =  WaypointTask.None;
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

            // ??? (priority 4) - why does this always return false.
            return false;
        }
        
        /// <summary>
        /// This is a utility function. Sets intel for the first tun.
        /// </summary>
        public void AssembleEmpireData()
        {
            // Generates initial reports.
            turnSteps[FIRSTSTEP].Process(serverState);
            turnSteps[SCANSTEP].Process(serverState);
        }
    }
}
