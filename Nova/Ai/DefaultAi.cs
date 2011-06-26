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

        private void HandleProduction()
        {
            foreach (Star star in stateData.PlayerStars.Values)
            {
                star.ManufacturingQueue.Queue.Clear();
                ProductionQueue.Item item = new ProductionQueue.Item();
                Design design;

                // build factories (limited by Germanium, and don't want to use it all)
                if (star.ResourcesOnHand.Germanium > 50)
                {
                    item.Name = "Factory";
                    item.Quantity = (int)((star.ResourcesOnHand.Germanium - 50) / 5);
                    item.Quantity = Math.Max(0, item.Quantity);

                    design = turnData.AllDesigns[stateData.PlayerRace.Name + "/" + item.Name];

                    item.BuildState = design.Cost;

                    star.ManufacturingQueue.Queue.Add(item);

                }

                // build mines
                item = new ProductionQueue.Item();
                item.Name = "Mine";
                item.Quantity = 100;
                design = turnData.AllDesigns[stateData.PlayerRace.Name + "/" + item.Name];
                item.BuildState = design.Cost;
                star.ManufacturingQueue.Queue.Add(item);

                // build defenses
                int defenceToBuild = Global.MaxDefenses - star.Defenses;
                item = new ProductionQueue.Item();
                item.Name = "Defenses";
                item.Quantity = defenceToBuild;
                design = turnData.AllDesigns[stateData.PlayerRace.Name + "/" + item.Name];
                item.BuildState = design.Cost;
                star.ManufacturingQueue.Queue.Add(item);
            }
        }
        public override void DoMove()
        {
            turnData = stateData.InputTurn;

            HandleProduction();
            HandleResearch();
            HandleMovements();
        }

        private void HandleMovements()
        {
            //scout
            List<Fleet> scoutFleets = new List<Fleet>();
            foreach (Fleet fleet in stateData.PlayerFleets)
            {
                if (fleet.Name.Contains("Scout") == true && fleet.Waypoints.Count == 1)
                {
                    scoutFleets.Add(fleet);
                }
            }
            List<Star> excludedStars = new List<Star>();
            if (scoutFleets.Count > 0)
            {
                foreach (Fleet fleet in scoutFleets)
                {
                    Star s = CloesestStar(fleet, excludedStars);
                    if (s != null)
                    {
                        excludedStars.Add(s);
                        SendFleet(s, fleet, WaypointTask.None );
                    }
                }
            }
            //colonization
            List<Fleet> colonyShipsFleets = new List<Fleet>();
            foreach (Fleet fleet in stateData.PlayerFleets)
            {
                if (fleet.CanColonize == true && fleet.Waypoints.Count == 1)
                    colonyShipsFleets.Add(fleet);
            }
            
            if (colonyShipsFleets.Count > 0)
            {
                //check if there is any good star to colonize
                foreach (Star star in turnData.AllStars.Values)
                {
                    if (star.HabitalValue(stateData.PlayerRace) > 0 && star.Owner == null)
                    {
                        SendFleet(star, colonyShipsFleets[0], WaypointTask.Colonise);
                        colonyShipsFleets.RemoveAt(0);
                        if (colonyShipsFleets.Count == 0)
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Return closest star to current fleet
        /// </summary>
        /// <param name="fleet"></param>
        /// <returns></returns>
        private Star CloesestStar(Fleet fleet, List<Star> excludedStars)
        {
            Star star = null;
            Double distance = double.MaxValue;
            foreach (Star s in turnData.AllStars.Values)
            {
                if (excludedStars.Contains(s) == true) 
                    continue;

                if (distance > Math.Sqrt(Math.Pow(fleet.Position.X - s.Position.X, 2) + Math.Pow(fleet.Position.Y - s.Position.Y, 2)))
                {
                    star = s;
                    distance = Math.Sqrt(Math.Pow(fleet.Position.X - s.Position.X, 2) + Math.Pow(fleet.Position.Y - s.Position.Y, 2));
                }
            }
            return star;
        }
        private void SendFleet(Star star, Fleet fleet, WaypointTask task)
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
            foreach (Message msg in stateData.Messages)
            {
                if (msg.Text.Contains("Your race has advanced to Tech Level") == true)
                {
                    int minLevel = int.MaxValue;
                    Nova.Common.TechLevel.ResearchField targetResearchField = TechLevel.ResearchField.Weapons; // default to researching weapons
                    for (TechLevel.ResearchField field = TechLevel.FirstField; field < TechLevel.LastField; field++)
                    {
                        if (stateData.ResearchLevels[field] < minLevel)
                        {
                            minLevel = stateData.ResearchLevels[field];
                            targetResearchField = field;
                        }
                    }
                    stateData.ResearchTopics.Zero();
                    stateData.ResearchTopics[targetResearchField] = 1;                        
                }
            }
           
        }
    }
}
