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

namespace Nova.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Message = Nova.Common.Message;
    
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// This file contains data that are persistent across multiple invocations of
    /// Nova Server. (It also holds the odd item that doesn't need to be persistent
    /// but it's just convenient to keep all "global" data in one place.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public sealed class ServerState
    {
        public List<BattleReport>               AllBattles      = new List<BattleReport>();
        public List<PlayerSettings>             AllPlayers      = new List<PlayerSettings>(); // Player number, race, ai (program name or "Default AI" or "Human")
        public Dictionary<int, int>             AllTechLevels   = new Dictionary<int, int>(); // Sum of a player's techlevels, for scoring purposes.
        public Dictionary<long, Design>         AllDesigns = new Dictionary<long, Design>();
        public Dictionary<long, Fleet>          AllFleets = new Dictionary<long, Fleet>();
        public Dictionary<int, EmpireData>      AllEmpires      = new Dictionary<int, EmpireData>(); // Game specific data about the race; relations, battle plans, research, etc.
        public Dictionary<string, Race>         AllRaces        = new Dictionary<string, Race>(); // Data about the race (traits etc)
        public Dictionary<string, Star>         AllStars        = new Dictionary<string, Star>();
        public Dictionary<long, Minefield>      AllMinefields = new Dictionary<long, Minefield>();
        public List<Message>                    AllMessages     = new List<Message>(); // All messages generated this turn.

        public bool GameInProgress      = false;
        public int TurnYear             = Global.StartingYear;
        public string GameFolder        = null; // The path&folder where client files are held.
        public string StatePathName     = null; // path&file name to the saved state data
        

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Creates a new fresh server state.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ServerState()
        { 
        }
  
        /// <summary>
        /// Load <see cref="Intel">ServerState</see> from an xml document
        /// </summary>
        /// <param name="xmldoc">produced using XmlDocument.Load(filename)</param>
        public ServerState(XmlDocument xmldoc)
        {            
            XmlNode xmlnode = xmldoc.DocumentElement;
            XmlNode tNode;
            
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
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllBattles.Add(new BattleReport(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allplayers":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllPlayers.Add(new PlayerSettings(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "alltechlevels":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllTechLevels.Add(int.Parse(tNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber),
                                                  int.Parse(tNode.FirstChild.Value));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "alldesigns":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                long designKey = long.Parse(tNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber);
                                if (tNode.Name.ToLower() == "design")
                                {
                                    AllDesigns.Add(designKey, new Design(tNode));
                                }
                                else if (tNode.Name.ToLower() == "shipdesign")
                                {
                                    AllDesigns.Add(designKey, new ShipDesign(tNode));
                                }
                                else
                                {
                                    throw new System.NotImplementedException("Unrecognised design type.");
                                }
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allfleets":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                Fleet fleet = new Fleet(tNode);
                                AllFleets.Add(fleet.Key, fleet);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allempires":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllEmpires.Add(int.Parse(tNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber),
                                               new EmpireData(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allraces":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                Race race = new Race();
                                race.LoadRaceFromXml(tNode);
                                AllRaces.Add(tNode.Attributes["Key"].Value, race);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allstars":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllStars.Add(tNode.Attributes["Key"].Value, new Star(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allminefields":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllMinefields.Add(int.Parse(tNode.Attributes["Key"].Value, System.Globalization.NumberStyles.HexNumber), new Minefield(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allmessages":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllMessages.Add(new Message(tNode));
                                tNode = tNode.NextSibling;
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Restore the persistent data. 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ServerState Restore()
        {
            using (FileStream stateFile = new FileStream(StatePathName, FileMode.Open))
            {
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(stateFile);
                
                ServerState serverState = new ServerState(xmldoc);
                
                LinkServerStateReferences();
                
                return serverState;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        /// ----------------------------------------------------------------------------
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
            //Global.SaveData(xmldoc, xmlelServerState, "FleetID", FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture));
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

            // Store the designs
            XmlElement xmlelAllDesigns = xmldoc.CreateElement("AllDesigns");
            foreach (KeyValuePair<long, Design> design in AllDesigns)
            {
                
                if (design.Value.Type == ItemType.Ship || design.Value.Type == ItemType.Starbase)
                {
                    child = ((ShipDesign)design.Value).ToXml(xmldoc);                    
                }
                else
                {
                    child = design.Value.ToXml(xmldoc);
                }    
                
                child.SetAttribute("Key", design.Key.ToString("X"));
                xmlelAllDesigns.AppendChild(child);                                            
            }
            xmlelServerState.AppendChild(xmlelAllDesigns);
            
            // Store the fleets
            XmlElement xmlelAllFleets = xmldoc.CreateElement("AllFleets");
            foreach (KeyValuePair<long, Fleet> fleet in AllFleets)
            {   
                child = fleet.Value.ToXml(xmldoc);
                child.SetAttribute("Key", fleet.Key.ToString("X"));                
                xmlelAllFleets.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllFleets);

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
            // Fleet reference to Star
            foreach (Fleet fleet in AllFleets.Values)
            {
                if (fleet.InOrbit != null)
                {
                    fleet.InOrbit = AllStars[fleet.InOrbit.Name];
                }

                // Ship reference to Design
                foreach (Ship ship in fleet.FleetShips)
                {
                    ship.DesignUpdate(AllDesigns[ship.DesignKey] as ShipDesign);
                }
            }
            
            // HullModule reference to a component
            foreach (Design design in AllDesigns.Values)
            {
                if (design.Type == ItemType.Ship || design.Type == ItemType.Starbase)
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
            
            // Star reference to Race
            // Star reference to Fleet (starbase)
            foreach (Star star in AllStars.Values)
            {
                if (star.ThisRace != null)
                {
                    // Reduntant, but works to check if race is valid...
                    if (star.Owner == AllEmpires[star.Owner].Id)
                    {
                        star.ThisRace = AllRaces[star.ThisRace.Name];
                    }
                    else
                    {
                        star.ThisRace = null;
                    }
                }

                if (star.Starbase != null)
                {
                    star.Starbase = AllFleets[star.Starbase.Key];
                }
            }
            
            // Also link inside empiredata.
            foreach (EmpireData empire in AllEmpires.Values)
            {
                foreach (Fleet fleet in empire.OwnedFleets.Values)
                {
                    if (fleet.InOrbit != null)
                    {
                        fleet.InOrbit = AllStars[fleet.InOrbit.Name];
                    }
                    // Ship reference to Design
                    foreach (Ship ship in fleet.FleetShips)
                    {
                        ship.DesignUpdate(AllDesigns[ship.DesignKey] as ShipDesign);
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
                        star.Starbase = AllFleets[star.Starbase.Key];
                    }
                }
            }
        }     

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reset all values to the defaults
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Clear()
        {            
            AllBattles.Clear();
            AllPlayers.Clear();  
            AllTechLevels.Clear();
            AllDesigns.Clear();
            AllFleets.Clear();
            AllEmpires.Clear();
            AllRaces.Clear();
            AllStars.Clear();
            AllMinefields.Clear();
            AllMessages.Clear();
            
            GameFolder     = null;
            GameInProgress = false;
            //FleetID        = 1;
            TurnYear       = Global.StartingYear;            
            StatePathName  = null;  
        }

    }
}
