#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.Ai
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
    using Nova.Common.Waypoints;

    public class DefaultAi : AbstractAI
    {
        private Intel turnData;

        /// <summary>
        /// This is the entry point to the AI proper. 
        /// Currently this does not use anything recognized by Computer Science as AI,
        /// just functional programming to complete a list of tasks.
        /// </summary>
        public override void DoMove()
        {
            turnData = clientState.InputTurn;

            HandleProduction();
            HandleResearch();
            HandleScouting();
            HandleColonizing();
        }

        /// <summary>
        /// Setup the production queue for the AI.
        /// </summary>
        private void HandleProduction()
        {
            // Clear the current manufacturing queue (except for partially built ships/starbases.
            Queue<ProductionCommand> clearProductionList = new Queue<ProductionCommand>();
            foreach (Star star in clientState.EmpireState.OwnedStars.Values)
            {
                if (star.Owner == clientState.EmpireState.Id)
                {
                    foreach (ProductionOrder productionOrderToclear in star.ManufacturingQueue.Queue)
                    {
                        if (productionOrderToclear.Unit.Cost == productionOrderToclear.Unit.RemainingCost)
                        {
                            ProductionCommand clearProductionCommand = new ProductionCommand(CommandMode.Delete, productionOrderToclear, star.Key);
                            if (clearProductionCommand.IsValid(clientState.EmpireState))
                            {
                                // Put the items to be cleared in a queue, as the actual cleanup can not be done while itterating the list.
                                clearProductionList.Enqueue(clearProductionCommand);
                                clientState.Commands.Push(clearProductionCommand);
                            }
                        }
                    }
                }
            }

            foreach (ProductionCommand clearProductionCommand in clearProductionList)
            {
                clearProductionCommand.ApplyToState(clientState.EmpireState); 
            }

            foreach (Star star in clientState.EmpireState.OwnedStars.Values)
            {
                if (star.Owner == clientState.EmpireState.Id)
                {
                    // build factories (limited by Germanium, and don't want to use it all)
                    if (star.ResourcesOnHand.Germanium > 50)
                    {
                        int factoryBuildCostGerm = clientState.EmpireState.Race.HasTrait("CF") ? 3 : 4;
                        int factoriesToBuild = (int)((star.ResourcesOnHand.Germanium - 50) / factoryBuildCostGerm);
                        if (factoriesToBuild > (star.GetOperableFactories() - star.Factories))
                        {
                            factoriesToBuild = star.GetOperableFactories() - star.Factories;
                        }

                        ProductionOrder factoryOrder = new ProductionOrder(factoriesToBuild, new FactoryProductionUnit(clientState.EmpireState.Race), false);
                        ProductionCommand factoryCommand = new ProductionCommand(CommandMode.Add, factoryOrder, star.Key);
                        if (factoryCommand.IsValid(clientState.EmpireState))
                        {
                            factoryCommand.ApplyToState(clientState.EmpireState);
                            clientState.Commands.Push(factoryCommand);
                        }
                    }

                    // build mines
                    int maxMines = star.GetOperableMines();
                    if (star.Mines < maxMines) 
                    {
                        ProductionOrder mineOrder = new ProductionOrder(maxMines - star.Mines, new MineProductionUnit(clientState.EmpireState.Race), false);
                        ProductionCommand mineCommand = new ProductionCommand(CommandMode.Add, mineOrder, star.Key);
                        if (mineCommand.IsValid(clientState.EmpireState))
                        {
                            mineCommand.ApplyToState(clientState.EmpireState);
                            clientState.Commands.Push(mineCommand);
                        }
                    }

                    // build defenses
                    int defenseToBuild = Global.MaxDefenses - star.Defenses;
                    if (defenseToBuild > 0)
                    {
                        ProductionOrder defenseOrder = new ProductionOrder(defenseToBuild, new DefenseProductionUnit(), false);
                        ProductionCommand defenseCommand = new ProductionCommand(CommandMode.Add, defenseOrder, star.Key);
                        if (defenseCommand.IsValid(clientState.EmpireState))
                        {
                            defenseCommand.ApplyToState(clientState.EmpireState);
                            clientState.Commands.Push(defenseCommand);
                        }
                    }
                }
            }
        }

        private void HandleScouting()
        {
            List<Fleet> scoutFleets = new List<Fleet>();
            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                if (fleet.Name.Contains("Scout") == true && fleet.Waypoints.Count == 1)
                {
                    scoutFleets.Add(fleet);
                }
            }

            // Find the stars we do not need to scout (eg home world)
            List<StarIntel> excludedStars = new List<StarIntel>();
            foreach (StarIntel report in turnData.EmpireState.StarReports.Values)
            {
                if (report.Year != Global.Unset)
                {
                    excludedStars.Add(report);
                }
            }

            if (scoutFleets.Count > 0)
            {
                foreach (Fleet fleet in scoutFleets)
                {
                    StarIntel s = CloesestStar(fleet, excludedStars);
                    if (s != null)
                    {
                        excludedStars.Add(s);
                        SendFleet(s, fleet, new NoTask());
                    }
                }
            }
        }

        private void HandleColonizing()
        {
            List<Fleet> colonyShipsFleets = new List<Fleet>();
            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                if (fleet.CanColonize == true && fleet.Waypoints.Count == 1)
                {
                    colonyShipsFleets.Add(fleet);
                }
            }

            if (colonyShipsFleets.Count > 0)
            {
                Fleet colonyFleet = colonyShipsFleets[0];
                // check if there is any good star to colonize
                foreach (StarIntel report in turnData.EmpireState.StarReports.Values)
                {
                    if (clientState.EmpireState.Race.HabitalValue(report) > 0 && report.Owner == Global.Nobody)
                    {
                        // ensure we take some colonists, maybe some Germainium
                        if (colonyFleet.Cargo.ColonistsInKilotons < 1)
                        {
                            if (colonyFleet.InOrbit != null && colonyFleet.InOrbit.Owner == colonyFleet.Owner)
                            {
                                // there is one of our planets here, so try to beam up colonists
                                Star ourStar = (Star)colonyFleet.InOrbit;

                                // How much germanium to load to seed factory production?
                                // 1/4 of cargo capacity, so long as we can load 100 kT of colonists
                                int germaniumToLoad = System.Math.Min(colonyFleet.TotalCargoCapacity / 4, colonyFleet.TotalCargoCapacity - 100);
                                // but leave at least 50 G behind
                                germaniumToLoad = System.Math.Min(germaniumToLoad, ourStar.ResourcesOnHand.Germanium - 50);
                                // do not try to load a negative number of G
                                germaniumToLoad = System.Math.Max(germaniumToLoad, 0);

                                // How many colonists to load?
                                // fill up space left after G
                                int colonistsToLoadKt = colonyFleet.TotalCargoCapacity - germaniumToLoad;
                                // but do not take the Star below 250,000 (max % growth)
                                colonistsToLoadKt = System.Math.Min(colonistsToLoadKt, (ourStar.Colonists - 250000) / Nova.Common.Global.ColonistsPerKiloton);
                                // ensure we load at least 1 kT of colonists
                                colonistsToLoadKt = System.Math.Max(colonistsToLoadKt, 1);

                                // load up
                                CargoTask wpTask = new CargoTask();
                                wpTask.Mode = CargoMode.Load;
                                wpTask.Amount.ColonistsInKilotons = colonistsToLoadKt;
                                wpTask.Amount.Germanium = germaniumToLoad;

                                Waypoint wp = new Waypoint();
                                wp.Task = wpTask;
                                wp.Position = ourStar.Position;
                                wp.WarpFactor = colonyFleet.FreeWarpSpeed;
                                wp.Destination = ourStar.Name;

                                WaypointCommand loadCommand = new WaypointCommand(CommandMode.Add, wp, colonyFleet.Key);
                                loadCommand.ApplyToState(clientState.EmpireState);
                                clientState.Commands.Push(loadCommand);
                            }
                            else
                            {
                                // TODO (priority 5) - go get some colonists
                            }
                        }

                        SendFleet(report, colonyFleet, new ColoniseTask());
                        colonyShipsFleets.RemoveAt(0);
                        if (colonyShipsFleets.Count == 0)
                        {
                            break;
                        }
                        colonyFleet = colonyShipsFleets[0];
                    }
                }
            }
        }


        /// <summary>
        /// Return closest star to current fleet.
        /// </summary>
        /// <param name="fleet"></param>
        /// <returns></returns>
        private StarIntel CloesestStar(Fleet fleet, List<StarIntel> excludedStars)
        {
            StarIntel target = null;
            double distance = double.MaxValue;
            foreach (StarIntel report in turnData.EmpireState.StarReports.Values)
            {
                if (excludedStars.Contains(report) != true)
                {
                    if (distance > Math.Sqrt(Math.Pow(fleet.Position.X - report.Position.X, 2) + Math.Pow(fleet.Position.Y - report.Position.Y, 2)))
                    {
                        target = report;
                        distance = Math.Sqrt(Math.Pow(fleet.Position.X - target.Position.X, 2) + Math.Pow(fleet.Position.Y - target.Position.Y, 2));
                    }
                }
            }
            return target;
        }
        
        private void SendFleet(StarIntel star, Fleet fleet, IWaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = star.Position;
            w.Destination = star.Name;
            w.Task = task;
            
            WaypointCommand command = new WaypointCommand(CommandMode.Add, w, fleet.Key);
            command.ApplyToState(clientState.EmpireState);
            clientState.Commands.Push(command);
        }

        /// <Summary>
        /// Manage research.
        /// Trivial implementation - research the lowest tech field.
        /// Only changes research field after completing the previous research level.
        /// </Summary>
        private void HandleResearch()
        {
            // Generate a research command to describe the changes.
            ResearchCommand command = new ResearchCommand();
            command.Topics.Zero();
            // Set the percentage of production to dedicate to research
            command.Budget = 0;

            // check if messages contains info about tech advence. Could be more than one, so use a flag to prevent setting the research level multiple times.
            bool hasAdvanced = false;
            foreach (Message msg in clientState.Messages)
            {
                if (!string.IsNullOrEmpty(msg.Text) && msg.Text.Contains("Your race has advanced to Tech Level") == true)
                {
                    hasAdvanced = true;
                }
            }

            if (hasAdvanced)
            {
                // pick next topic
                int minLevel = int.MaxValue;
                Nova.Common.TechLevel.ResearchField targetResearchField = TechLevel.ResearchField.Weapons; // default to researching weapons
                for (TechLevel.ResearchField field = TechLevel.FirstField; field <= TechLevel.LastField; field++)
                {
                    if (clientState.EmpireState.ResearchLevels[field] < minLevel)
                    {
                        minLevel = clientState.EmpireState.ResearchLevels[field];
                        targetResearchField = field;
                    }
                }

                command.Topics[targetResearchField] = 1;
            }

            if (command.IsValid(clientState.EmpireState))
            {
                clientState.Commands.Push(command);
                command.ApplyToState(clientState.EmpireState);
            }
        }
    }
}
