#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;

    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;

    public class IntelReader
    {
        private ClientState stateData;
        
        public IntelReader(ClientState stateData)
        {
            this.stateData = stateData;
        }

        /// <summary>
        /// Read and process the <RaceName>.intel generated by the Nova
        /// Console. 
        /// This file must be present before the GUI will run, since it
        /// contains key data, such as the race name, as well as any information about
        /// events that happend in the previous game year (battles, mine hits, etc. 
        /// It is also used for a kind of boot strapping process as there may or may not
        /// be a <RaceName>.state file to load:
        /// 1. open the .intel and determine the race, and hence the name of any .state file.
        /// 2. open the .state file, if any. This contains any historical information, 
        ///    and is used to reconstruct the stateData (we can create a new one with 
        ///    no history if required, such as on the first turn)
        /// 3. process the .intel file to update the stateData
        ///
        /// Note that this file is not read again after the first time a new turn is
        /// received. Once fully loaded all further processing is done using 
        /// stateData (which is subsequently used to generate <RaceName>.orders.
        /// </summary>
        /// <param name="turnFileName">Path and file name of the RaceName.intel file.</param>
        public void ReadIntel(string turnFileName)
        {
            if (!File.Exists(turnFileName))
            {
                Report.FatalError("The Nova GUI cannot start unless a turn file is present");
            }

            using (Stream turnFile = new FileStream(turnFileName, FileMode.Open))
            {
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(turnFile);
                Intel newIntel = new Intel(xmldoc);

                // check this is a new turn, not an old one or the same one.
                if (newIntel.EmpireState.TurnYear >= stateData.EmpireState.TurnYear)
                {
                    stateData.GameFolder = Path.GetDirectoryName(turnFileName);
                    stateData.Restore();
                    stateData.InputTurn = newIntel;
                    ProcessIntel();
                }
                else
                {
                    // exit without saving any files
                    throw new System.Exception("Turn Year missmatch");
                }
            }
        }

        /// <summary>
        /// This function processes the stateData.TurnData for this turn
        /// and updates the stateData.
        /// </summary>
        public void ProcessIntel()
        {
            stateData.EmpireState = stateData.InputTurn.EmpireState;

            // Clear old turn data from StateData
            stateData.DeletedFleets.Clear();
            stateData.DeletedDesigns.Clear();
            stateData.Messages.Clear();

            // fix object references after loading
            LinkIntelReferences();

            ProcessMessages();
            ProcessResearch();
        }

        /// <summary>
        /// When intel is loaded from file, objects may contain references to other objects.
        /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
        /// up these references once the file is fully loaded and all objects exist.
        /// In most cases a placeholder object has been created with the Name set from the file,
        /// and we need to find the actual reference using this Name.
        /// Objects can't do this themselves as they don't have access to the state data, 
        /// so we do it here.
        /// </summary>
        private void LinkIntelReferences()
        {
            // HullModule reference to a component
            foreach (Design design in stateData.InputTurn.AllDesigns.Values)
            {
                if (design.Type.ToLower() == "ship" || design.Type.ToLower() == "starbase")
                {
                    ShipDesign ship = design as ShipDesign;
                    foreach (HullModule module in ((Hull)ship.ShipHull.Properties["Hull"]).Modules)
                    {
                        if (module.AllocatedComponent != null && module.AllocatedComponent.Name != null)
                        {
                            AllComponents.Data.Components.TryGetValue(module.AllocatedComponent.Name, out module.AllocatedComponent);
                        }
                    }
                }
            }

            // Fleet reference to Star
            foreach (FleetIntel fleet in stateData.EmpireState.FleetReports.Values)
            {
                if (fleet.InOrbit != null)
                {
                    fleet.InOrbit = stateData.EmpireState.StarReports[fleet.InOrbit.Name];
                }
                // Ship reference to Design
                foreach (Ship ship in fleet.FleetShips)
                {
                    ship.DesignUpdate(stateData.InputTurn.AllDesigns[ship.DesignId] as ShipDesign);
                }
            }
            
            // Star reference to Race
            // Star reference to Fleet (starbase)
            foreach (StarIntel star in stateData.EmpireState.StarReports.Values)
            {
                if (star.ThisRace != null)
                {
                    if (star.Owner == stateData.EmpireState.Id)
                    {
                        star.ThisRace = stateData.EmpireState.Race;
                    }
                    else
                    {
                        star.ThisRace = null;
                    }
                }

                if (star.Starbase != null)
                {
                    star.Starbase = stateData.EmpireState.FleetReports[star.Starbase.Id];                  
                }
            }


            // link the ship designs in battle reports to the stacks
            foreach (BattleReport battle in stateData.InputTurn.Battles)
            {
                foreach (Fleet fleet in battle.Stacks.Values)
                {
                    foreach (Ship ship in fleet.FleetShips)
                    {
                        if (stateData.EnemyDesigns.ContainsKey(ship.DesignId))
                        {
                            ship.DesignUpdate((ShipDesign)stateData.EnemyDesigns[ship.DesignId]);
                        }
                        else
                        {
                            ship.DesignUpdate((ShipDesign)stateData.InputTurn.AllDesigns[ship.DesignId]);
                        }
                    }
                }
            }

            // Messages to Event objects
            foreach (Message message in stateData.InputTurn.Messages)
            {
                switch (message.Type)
                {
                    case "TechAdvance":
                        // TODO (priority 5) - link the tech advance message to the research control panel.
                        break;

                    case "NewComponent":
                        // TODO (priority 5) - Link the new component message to the technology browser (when it is available in game).
                        break;

                    case "BattleReport":
                        // The message is loaded such that the Event is a string containing the BattleReport.Key.
                        // FIXME (priority 4) - linking battle messages to the right battle report is inefficient because the turnData.Battles does not have a meaningful key.
                        foreach (BattleReport battle in stateData.InputTurn.Battles)
                        {
                            if (battle.Key == ((string)message.Event))
                            {
                                message.Event = battle;
                                break;
                            }
                        }
                        break;

                    default:
                        message.Event = null;
                        break;
                }
            }
        }

        /// <summary>
        /// Run through the full list of messages and populate the message store in the
        /// state data with the messages relevant to the player's selected race. The
        /// actual message control will be populated within the main window
        /// initialisation.
        /// </summary>
        private void ProcessMessages()
        {
            foreach (Message message in stateData.InputTurn.Messages)
            {
                if ((message.Audience == stateData.EmpireState.Id) ||
                    (message.Audience == Global.AllEmpires))
                {
                    stateData.Messages.Add(message);
                }
            }
        }

        /// <summary>
        /// Recieve research updates.
        /// </summary>
        private void ProcessResearch()
        {
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (stateData.EmpireState.ResearchLevelsGained == null)
                {
                    return;
                }

                while (stateData.EmpireState.ResearchLevelsGained[area] > 0)
                {
                    // Report new levels.
                    stateData.EmpireState.ResearchLevelsGained[area] = stateData.EmpireState.ResearchLevelsGained[area] - 1;
                    ReportLevelUpdate(area, stateData.EmpireState.ResearchLevels[area]);
                }
            }
        }

        /// <summary>
        /// Report an update in tech level and any new components that have became
        /// available.
        /// </summary>
        /// <param name="area">The field of research.</param>
        /// <param name="level">The new level obtained.</param>
        private void ReportLevelUpdate(TechLevel.ResearchField area, int level)
        {
            Message techAdvanceMessage = new Message(
                stateData.EmpireState.Id,
                "Your race has advanced to Tech Level " + level + " in the " + area.ToString() + " field",
                "TechAdvance",
                null);
            stateData.Messages.Add(techAdvanceMessage);

            Dictionary<string, Component> allComponents = AllComponents.Data.Components;
            TechLevel oldResearchLevel = stateData.EmpireState.ResearchLevels;
            TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

            newResearchLevel[area] = level;

            foreach (Component component in allComponents.Values)
            {
                if (component.RequiredTech > oldResearchLevel && component.RequiredTech <= newResearchLevel)
                {
                    stateData.AvailableComponents.Add(component);
                    Message newComponentMessage = null;
                    if (component.Properties.ContainsKey("Scaner") && component.Type == "Planetary Installations")
                    {
                        newComponentMessage = new Message(
                            stateData.EmpireState.Id,
                            null,
                            "All existing planetary scanners has been replaced by " + component.Name + " " + component.Type,
                            null);
                        foreach (StarIntel report in stateData.EmpireState.StarReports.Values)
                        {
                            if (report.Owner == stateData.EmpireState.Id &&
                                report.ScannerType != string.Empty)
                            {
                                report.ScannerType = component.Name;
                            }
                        }
                    }
                    else
                    {
                        newComponentMessage = new Message(
                           stateData.EmpireState.Id,
                           null,
                           "You now have available the " + component.Name + " " + component.Type + " component",
                           null);
                    }
                    stateData.Messages.Add(newComponentMessage);
                }
            }

            stateData.EmpireState.ResearchLevels = newResearchLevel;
        }

    }
}