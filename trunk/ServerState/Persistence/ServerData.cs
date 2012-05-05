#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.Commands;
    using Nova.Common.DataStructures;
    using Message = Nova.Common.Message;
    
    /// <summary>
    /// This file contains data that are persistent across multiple invocations of
    /// Nova Server. (It also holds the odd item that doesn't need to be persistent
    /// but it's just convenient to keep all "global" data in one place.
    /// </summary>
    [Serializable]
    public sealed class ServerData
    {
        public Dictionary<int, Stack<ICommand>> AllCommands     = new Dictionary<int, Stack<ICommand>>();
        public List<BattleReport>               AllBattles      = new List<BattleReport>();
        public List<PlayerSettings>             AllPlayers      = new List<PlayerSettings>(); // Player number, race, ai (program name or "Default AI" or "Human")
        public Dictionary<int, int>             AllTechLevels   = new Dictionary<int, int>(); // Sum of a player's techlevels, for scoring purposes.
        public Dictionary<int, EmpireData>      AllEmpires      = new Dictionary<int, EmpireData>(); // Game specific data about the race; relations, battle plans, research, etc.
        public Dictionary<string, Race>         AllRaces        = new Dictionary<string, Race>(); // Data about the race (traits etc)
        public Dictionary<string, Star>         AllStars        = new Dictionary<string, Star>();
        public Dictionary<long, Minefield>      AllMinefields   = new Dictionary<long, Minefield>();
        public List<Message>                    AllMessages     = new List<Message>(); // All messages generated this turn.

        public bool GameInProgress      = false;
        public int TurnYear             = Global.StartingYear;
        public string GameFolder        = null; // The path&folder where client files are held.
        public string StatePathName     = null; // path&file name to the saved state data
        
        /// <summary>
        /// Creates a new fresh server state.
        /// </summary>
        public ServerData()
        { 
        }
  
        /// <summary>
        /// Load <see cref="Intel">ServerState</see> from an xml document.
        /// </summary>
        /// <param name="xmldoc">Produced using XmlDocument.Load(filename).</param>
        public ServerData(XmlDocument xmldoc)
        {            
            XmlNode xmlnode = xmldoc.DocumentElement;
            XmlNode textNode;
            
            while (xmlnode != null)
            {
                try
                {
                    switch (xmlnode.Name.ToLower())
                    {
                        case "root":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        case "serverstate":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        
                        case "gameinprogress":
                            GameInProgress = bool.Parse(xmlnode.FirstChild.Value);
                            break;                                                
                        case "turnyear":
                            TurnYear = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;                        
                        case "gamefolder":
                            GameFolder = xmlnode.FirstChild.Value;
                            break;                        
                        case "statepathname":
                            StatePathName = xmlnode.FirstChild.Value;
                            break;
                        
                        // The collections are retrieved via loops: we trust
                        // they are in the correct format.
                        
                        case "allbattles":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllBattles.Add(new BattleReport(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allplayers":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllPlayers.Add(new PlayerSettings(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "alltechlevels":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllTechLevels.Add(int.Parse(textNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber), int.Parse(textNode.FirstChild.Value));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allempires":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllEmpires.Add(
                                    int.Parse(textNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber),
                                    new EmpireData(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allraces":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                Race race = new Race();
                                race.LoadRaceFromXml(textNode);
                                AllRaces.Add(textNode.Attributes["Key"].Value, race);
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allstars":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllStars.Add(textNode.Attributes["Key"].Value, new Star(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allminefields":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllMinefields.Add(int.Parse(textNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber), new Minefield(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                        
                        case "allmessages":
                            textNode = xmlnode.FirstChild;
                            while (textNode != null)
                            {
                                AllMessages.Add(new Message(textNode));
                                textNode = textNode.NextSibling;
                            }
                            break;
                    }
                    
                    xmlnode = xmlnode.NextSibling;

                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e);
                }    
            }
        }

#if USE_COMMAND_ORDERS
        /// <summary>
        /// Restore the persistent data. 
        /// </summary>
        public void Restore()
        {
            using (FileStream stateFile = new FileStream(StatePathName, FileMode.Open))
            {
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(stateFile);
                
                // Temporary data store only!
                ServerData restoredState = new ServerData(xmldoc);
                
                // We need to copy the restored values
                AllCommands     = restoredState.AllCommands;
                AllBattles      = restoredState.AllBattles;
                AllPlayers      = restoredState.AllPlayers;
                AllTechLevels   = restoredState.AllTechLevels;
                AllEmpires      = restoredState.AllEmpires;
                AllRaces        = restoredState.AllRaces;
                AllStars        = restoredState.AllStars;
                AllMinefields   = restoredState.AllMinefields;
                AllMessages     = restoredState.AllMessages;
        
                GameInProgress    = restoredState.GameInProgress;
                TurnYear          = restoredState.TurnYear;
                GameFolder        = restoredState.GameFolder; // The path&folder where client files are held.
                StatePathName     = restoredState.StatePathName;
                
                LinkServerStateReferences(); 
            }
        }
#else
        /// <summary>
        /// Restore the persistent data. 
        /// </summary>
        public ServerData Restore()
        {
            using (FileStream stateFile = new FileStream(StatePathName, FileMode.Open))
            {
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(stateFile);

                // Temporary data store only!
                ServerData restoredState = new ServerData(xmldoc);

                // We need to copy the restored values
                AllCommands = restoredState.AllCommands;
                AllBattles = restoredState.AllBattles;
                AllPlayers = restoredState.AllPlayers;
                AllTechLevels = restoredState.AllTechLevels;
                AllDesigns = restoredState.AllDesigns;
                AllFleets = restoredState.AllFleets;
                AllEmpires = restoredState.AllEmpires;
                AllRaces = restoredState.AllRaces;
                AllStars = restoredState.AllStars;
                AllMinefields = restoredState.AllMinefields;
                AllMessages = restoredState.AllMessages;

                GameInProgress = restoredState.GameInProgress;
                TurnYear = restoredState.TurnYear;
                GameFolder = restoredState.GameFolder; // The path&folder where client files are held.
                StatePathName = restoredState.StatePathName;

                LinkServerStateReferences();

                return restoredState;
            }
        }
#endif

        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        public void Save()
        {
            if (StatePathName == null)
            {
                // TODO (priority 5) add the nicities. Update the game files location.
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Choose a location to save the game.";

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StatePathName = fd.FileName;
                }
                else
                {
                    throw new System.IO.IOException("File dialog cancelled");
                }
            }

            ToXml();
        }
        
        /// <summary>
        /// Save: Serialise this object to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Intel</returns>
        public void ToXml()
        {            
            XmlDocument xmldoc = new XmlDocument();
            XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);
            XmlElement child;
            
            // create the outer element
            XmlElement xmlelServerState = xmldoc.CreateElement("ServerState");
            xmlRoot.AppendChild(xmlelServerState);
            
            Global.SaveData(xmldoc, xmlelServerState, "GameInProgress", GameInProgress.ToString());
            // Global.SaveData(xmldoc, xmlelServerState, "FleetID", FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelServerState, "TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelServerState, "GameFolder", GameFolder);
            Global.SaveData(xmldoc, xmlelServerState, "StatePathName", StatePathName);
            
            // Store the players
            XmlElement xmlelAllPlayers = xmldoc.CreateElement("AllPlayers");
            foreach (PlayerSettings playerSettings in AllPlayers)
            {
                xmlelAllPlayers.AppendChild(playerSettings.ToXml(xmldoc));
            }
            xmlelServerState.AppendChild(xmlelAllPlayers);        
            
            // Store the Races
            XmlElement xmlelAllRaces = xmldoc.CreateElement("AllRaces");
            foreach (KeyValuePair<string, Race> race in AllRaces)
            {
                child = race.Value.ToXml(xmldoc);
                child.SetAttribute("Key", race.Key);                
                xmlelAllRaces.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllRaces);
            
            // Store the Empire's Data
            XmlElement xmlelAllEmpires = xmldoc.CreateElement("AllEmpires");
            foreach (KeyValuePair<int, EmpireData> empireData in AllEmpires)
            {
                child = empireData.Value.ToXml(xmldoc);
                child.SetAttribute("Key", empireData.Key.ToString("X"));
                xmlelAllEmpires.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllEmpires);
            
            // Store the tech level sums.
            XmlElement xmlelAllTechLevels = xmldoc.CreateElement("AllTechLevels");
            foreach (KeyValuePair<int, int> techLevels in AllTechLevels)
            {
                child = xmldoc.CreateElement("TechLevels");                
                child.SetAttribute("Key", techLevels.Key.ToString("X"));
                child.InnerText = techLevels.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                xmlelAllTechLevels.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllTechLevels);  
            
            // Store the Stars
            XmlElement xmlelAllStars = xmldoc.CreateElement("AllStars");
            foreach (KeyValuePair<string, Star> star in AllStars)
            {
                child = star.Value.ToXml(xmldoc);
                child.SetAttribute("Key", star.Key);                
                xmlelAllStars.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllStars);

            // Store the Minefields
            XmlElement xmlelAllMinefields = xmldoc.CreateElement("AllMinefields");
            foreach (KeyValuePair<long, Minefield> minefield in AllMinefields)
            {
                child = minefield.Value.ToXml(xmldoc);
                child.SetAttribute("Key", minefield.Key.ToString("X"));                
                xmlelAllMinefields.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllMinefields);
            
            // Store the battle reports
            XmlElement xmlelAllBattles = xmldoc.CreateElement("AllBattles");
            foreach (BattleReport battleReport in AllBattles)
            {
                xmlelAllBattles.AppendChild(battleReport.ToXml(xmldoc));
            }
            xmlelServerState.AppendChild(xmlelAllBattles);
            
            // Store the Messages
            XmlElement xmlelAllMessages = xmldoc.CreateElement("AllMessages");
            foreach (Message message in AllMessages)
            {
                xmlelAllMessages.AppendChild(message.ToXml(xmldoc));
            }
            xmlelServerState.AppendChild(xmlelAllMessages);
            
            xmldoc.Save(StatePathName);
        }
  
        /// <summary>
        /// When state is loaded from file, objects may contain references to other objects.
        /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
        /// up these references once the file is fully loaded and all objects exist.
        /// In most cases a placeholder object has been created with the Name set from the file,
        /// and we need to find the actual reference using this Name.
        /// Objects can't do this themselves as they don't have access to the state data, 
        /// so we do it here.
        /// </summary>
        private void LinkServerStateReferences()
        {            
            foreach (Star star in AllStars.Values)
            {
                // Star reference to the Race that owns it
                if (star.ThisRace != null)
                {
                    // Redundant, but works to check if race is valid...
                    if (star.Owner == AllEmpires[star.Owner].Id)
                    {
                        star.ThisRace = AllRaces[star.ThisRace.Name];
                    }
                    else
                    {
                        star.ThisRace = null;
                    }
                }

                // Star reference to it's Starbase if any
                if (star.Starbase != null)
                {
                    star.Starbase = AllEmpires[star.Starbase.Key.Owner()].OwnedFleets[star.Starbase.Key];
                }
            }
            
            // Link inside EmpireData
            foreach (EmpireData empire in AllEmpires.Values)
            {
                foreach (ShipDesign design in empire.Designs.Values)
                {
                    foreach (HullModule module in (design.Blueprint.Properties["Hull"] as Hull).Modules)
                    {
                        if (module.AllocatedComponent != null && module.AllocatedComponent.Name != null)
                        {
                            AllComponents.Data.Components.TryGetValue(module.AllocatedComponent.Name, out module.AllocatedComponent);
                        }
                    }
                }
            
                foreach (Fleet fleet in empire.OwnedFleets.Values)
                {
                    // Fleet reference to Star it is orbiting
                    if (fleet.InOrbit != null)
                    {
                        fleet.InOrbit = AllStars[fleet.InOrbit.Name];
                    }
                    // Ship reference to Design
                    foreach (ShipToken token in fleet.Tokens)
                    {
                        token.Design = empire.Designs[token.Design.Key];
                    }
                }
                 
                foreach (Star star in empire.OwnedStars.Values)
                {
                    if (star.ThisRace != null)
                    {
                        // Reduntant, but works to check if race name is valid...
                        if (star.Owner == empire.Id)
                        {
                            star.ThisRace = empire.Race;
                        }
                        else
                        {
                            star.ThisRace = null;
                        }
                    }
    
                    if (star.Starbase != null)
                    {
                        star.Starbase = empire.OwnedFleets[star.Starbase.Key];
                    }
                }
            }
        }     

        
        /// <summary>
        /// Reset all values to the defaults.
        /// </summary>
        public void Clear()
        {   
            AllCommands.Clear();
            AllBattles.Clear();
            AllPlayers.Clear();  
            AllTechLevels.Clear();
            AllEmpires.Clear();
            AllRaces.Clear();
            AllStars.Clear();
            AllMinefields.Clear();
            AllMessages.Clear();
            
            GameFolder     = null;
            GameInProgress = false;
            TurnYear       = Global.StartingYear;            
            StatePathName  = null;  
        }
        
        
        /// <summary>
        /// Iterates through all Fleets in all Empires, in order.
        /// </summary>
        /// <returns>An enumerator containing all Fleets from all empires.</returns>
        public IEnumerable<Fleet> IterateAllFleets()
        {
            return AllEmpires.Values.SelectMany(empire => empire.OwnedFleets.Values);
        }
        
        
        /// <summary>
        /// Iterates through all Designs in all Empires, in order.
        /// </summary>
        /// <returns>An enumerator containing all Designs from all empires.</returns>
        public IEnumerable<ShipDesign> IterateAllDesigns()
        {
            return AllEmpires.Values.SelectMany(empire => empire.Designs.Values);
        }
    }
}
