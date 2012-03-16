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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.Ai
{
    public class DefaultAi : AbstractAI
    {
        private Intel turnData;

        /// <summary>
        /// Setup the production queue for the AI.
        /// </summary>
        private void HandleProduction()
        {
            foreach (Star star in clientState.EmpireState.OwnedStars.Values)
            {
                if (star.Owner == clientState.EmpireState.Id)
                {
                    star.ManufacturingQueue.Clear();
                    ProductionOrder productionOrder;

                    // build factories (limited by Germanium, and don't want to use it all)
                    if (star.ResourcesOnHand.Germanium > 50)
                    {
                        int factoryBuildCostGerm = clientState.EmpireState.Race.HasTrait("CF") ? 3 : 4;
                        int factoriesToBuild = (int)((star.ResourcesOnHand.Germanium - 50) / factoryBuildCostGerm);

                        productionOrder = new ProductionOrder(factoriesToBuild, new FactoryProductionUnit(clientState.EmpireState.Race), true);
                        star.ManufacturingQueue.Queue.Add(productionOrder);
                    }

                    // build mines
                    int maxMines = star.GetOperableMines();
                    if (star.Mines < maxMines) 
                    {
                        productionOrder = new ProductionOrder(maxMines - star.Mines, new MineProductionUnit(clientState.EmpireState.Race), true);
                        star.ManufacturingQueue.Queue.Add(productionOrder);
                    }

                    // build defenses
                    int defenceToBuild = Global.MaxDefenses - star.Defenses;
                    if (defenceToBuild > 0)
                    {
                        productionOrder = new ProductionOrder(defenceToBuild, new DefenseProductionUnit(), false);
                        star.ManufacturingQueue.Queue.Add(productionOrder);
                    }
                }
            }
        }

        public override void DoMove()
        {
            turnData = clientState.InputTurn;

            HandleProduction();
            HandleResearch();
            HandleMovements();
        }

        private void HandleMovements()
        {
            // scout
            List<Fleet> scoutFleets = new List<Fleet>();
            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                if (fleet.Name.Contains("Scout") == true && fleet.Waypoints.Count == 1)
                {
                    scoutFleets.Add(fleet);
                }
            }
            List<StarIntel> excludedStars = new List<StarIntel>();
            if (scoutFleets.Count > 0)
            {
                foreach (Fleet fleet in scoutFleets)
                {
                    StarIntel s = CloesestStar(fleet, excludedStars);
                    if (s != null)
                    {
                        excludedStars.Add(s);
                        SendFleet(s, fleet, WaypointTask.None);
                    }
                }
            }
            // colonization
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
                // check if there is any good star to colonize
                foreach (StarIntel report in turnData.EmpireState.StarReports.Values)
                {
                    if (clientState.EmpireState.Race.HabitalValue(report) > 0 && report.Owner == Global.Nobody)
                    {
                        SendFleet(report, colonyShipsFleets[0], WaypointTask.Colonise);
                        colonyShipsFleets.RemoveAt(0);
                        if (colonyShipsFleets.Count == 0)
                        {
                            break;
                        }
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
                if (excludedStars.Contains(report) == true)
                {
                    continue;
                }
                if (distance > Math.Sqrt(Math.Pow(fleet.Position.X - report.Position.X, 2) + Math.Pow(fleet.Position.Y - report.Position.Y, 2)))
                {
                    target = report;
                    distance = Math.Sqrt(Math.Pow(fleet.Position.X - target.Position.X, 2) + Math.Pow(fleet.Position.Y - target.Position.Y, 2));
                }
            }
            return target;
        }
        
        private void SendFleet(StarIntel star, Fleet fleet, WaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = star.Position;
            w.Destination = star.Name;
            w.Task = task;
            fleet.Waypoints.Add(w);
        }

        /// <Summary>
        /// Always go to the min tech
        /// </Summary>
        private void HandleResearch()
        {
            // check if messages contains info about tech advence
            foreach (Message msg in clientState.Messages)
            {
                if (msg.Text.Contains("Your race has advanced to Tech Level") == true)
                {
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
                    clientState.EmpireState.ResearchTopics.Zero();
                    clientState.EmpireState.ResearchTopics[targetResearchField] = 1;                        
                }
            }
        }
    }
}
