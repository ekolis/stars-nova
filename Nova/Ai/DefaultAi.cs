#region Copyright Notice
// ============================================================================
// Copyright (C) 2009 - 2017 stars-nova
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
    using Nova.Common.DataStructures;
    using Nova.Common.Waypoints;

    public class DefaultAi : AbstractAI
    {
        private Intel turnData;
        private FleetList fuelStations = null;
        private DefaultAIPlanner aiPlan = null;
        
        // Sub AIs to manage planets, fleets and stuff
        private Dictionary<string, DefaultPlanetAI> planetAIs = new Dictionary<string, DefaultPlanetAI>();
        private Dictionary<long, DefaultFleetAI> fleetAIs = new Dictionary<long, DefaultFleetAI>();

        /// <summary>
        /// This is the entry point to the AI proper. 
        /// Currently this does not use anything recognized by Computer Science as AI,
        /// just functional programming to complete a list of tasks.
        /// </summary>
        public override void DoMove()
        {
            aiPlan = new DefaultAIPlanner(clientState);

            // create the helper AIs
            foreach (Star star in clientState.EmpireState.OwnedStars.Values)
            {
                if (star.Owner == clientState.EmpireState.Id)
                {
                    DefaultPlanetAI planetAI = new DefaultPlanetAI(star, clientState, this.aiPlan);
                    planetAIs.Add(star.Key, planetAI);
                }
            }

            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                if (fleet.Owner == clientState.EmpireState.Id)
                {
                    aiPlan.CountFleet(fleet);
                    DefaultFleetAI fleetAI = new DefaultFleetAI(fleet, clientState, fuelStations);
                    fleetAIs.Add(fleet.Id, fleetAI);

                    // reset all waypoint orders
                    for (int wpIndex = 1; wpIndex < fleet.Waypoints.Count; wpIndex++)
                    {
                        WaypointCommand command = new WaypointCommand(CommandMode.Delete, fleet.Key, wpIndex);
                        command.ApplyToState(clientState.EmpireState);
                        clientState.Commands.Push(command);
                    }
                }
            }

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
            foreach (DefaultPlanetAI ai in planetAIs.Values)
            {
                ai.HandleProduction();
            }
        }

        private void HandleScouting()
        {
            List<Fleet> scoutFleets = new List<Fleet>();
            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                if (fleet.Name.Contains("Scout") == true)
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
                    StarIntel starToScout = fleetAIs[fleet.Id].Scout(excludedStars);
                    if (starToScout != null)
                    {
                        excludedStars.Add(starToScout);
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
                        // send fleet to colonise
                        fleetAIs[colonyFleet.Id].Colonise(report);
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


        /// <Summary>
        /// Manage research.
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

                if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Propulsion] < 3)
                {
                    // Prop 3 - Long Hump 6 - Warp 6 engine (or fuel mizer at Prop 2)
                    targetResearchField = TechLevel.ResearchField.Propulsion;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Electronics] < 1)
                {
                    // Elec 1 - Rhino Scanner - 50 ly scan
                    targetResearchField = TechLevel.ResearchField.Electronics;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Construction] < 3)
                {
                    // Cons 3 - Destroyer & Medium Freighter
                    targetResearchField = TechLevel.ResearchField.Construction;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Electronics] < 5)
                {
                    // Elec 5 - Scanners
                    targetResearchField = TechLevel.ResearchField.Electronics;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Weapons] < 6)
                {
                    // Wep 6 - Beta Torp (@5) and Yakimora Light Phaser
                    targetResearchField = TechLevel.ResearchField.Weapons;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Propulsion] < 7)
                {
                    // Prop 7 - Warp 8 engine
                    targetResearchField = TechLevel.ResearchField.Propulsion;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Construction] < 6)
                {
                    // Cons 6 - Frigate
                    targetResearchField = TechLevel.ResearchField.Construction;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Biotechnology] < 4)
                {
                    // Bio 4 - Unlock terraform and prep for mines
                    targetResearchField = TechLevel.ResearchField.Biotechnology;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Energy] < 3)
                {
                    // Energy 3 - Mines and shields
                    targetResearchField = TechLevel.ResearchField.Energy;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Construction] < 9)
                {
                    // Cons 9 - Cruiser
                    targetResearchField = TechLevel.ResearchField.Construction;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Energy] < 6)
                {
                    // Energy 6 - Shields
                    targetResearchField = TechLevel.ResearchField.Energy;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Weapons] < 12)
                {
                    // Weapons 12 - Jihad Missile
                    targetResearchField = TechLevel.ResearchField.Weapons;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Construction] < 13)
                {
                    // Cons 13 - Battleships
                    targetResearchField = TechLevel.ResearchField.Construction;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Energy] < 11)
                {
                    // Energy 11 - Bear Neutrino at 10, and unlocks Syncro Sapper (need weapons 21)
                    targetResearchField = TechLevel.ResearchField.Energy;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Electronics] < 11)
                {
                    // Elect 11 - Jammer 20 and Super Computer
                    targetResearchField = TechLevel.ResearchField.Electronics;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Propulsion] < 12)
                {
                    // Prop 12 - Warp 10 and Overthruster
                    targetResearchField = TechLevel.ResearchField.Propulsion;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Biotechnology] < 7)
                {
                    // Bio 7 maybe - scanners, Anti-matter generator, smart bombs
                    targetResearchField = TechLevel.ResearchField.Biotechnology;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Weapons] < 24)
                {
                    // Weapons 24 - research all remaining weapons technologies
                    targetResearchField = TechLevel.ResearchField.Weapons;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Construction] < 26)
                {
                    // Cons 26 - Nubian
                    targetResearchField = TechLevel.ResearchField.Construction;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Electronics] < 19)
                {
                    // Elect 19 - Battle nexus
                    targetResearchField = TechLevel.ResearchField.Electronics;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Energy] < 22)
                {
                    // Energy 22 - Complete Phase Shield
                    targetResearchField = TechLevel.ResearchField.Energy;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Propulsion] < 23)
                {
                    // Prop 23 - Trans-Star 10
                    targetResearchField = TechLevel.ResearchField.Propulsion;
                }
                else if (clientState.EmpireState.ResearchLevels[TechLevel.ResearchField.Biotechnology] < 10)
                {
                    // Bio 10 - RNA Scanner
                    targetResearchField = TechLevel.ResearchField.Biotechnology;
                }
                else
                {
                    // research lowest tech field
                    for (TechLevel.ResearchField field = TechLevel.FirstField; field <= TechLevel.LastField; field++)
                    {
                        if (clientState.EmpireState.ResearchLevels[field] < minLevel)
                        {
                            minLevel = clientState.EmpireState.ResearchLevels[field];
                            targetResearchField = field;
                        }
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
