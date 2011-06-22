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
        public Dictionary<string, int>          AllTechLevels   = new Dictionary<string, int>(); // Sum of a player's techlevels, for scoring purposes.
        public Dictionary<string, Design>       AllDesigns      = new Dictionary<string, Design>();
        public Dictionary<string, Fleet>        AllFleets       = new Dictionary<string, Fleet>();
        public Dictionary<string, RaceData>     AllRaceData     = new Dictionary<string, RaceData>(); // Game specific data about the race; relations, battle plans, research, etc.
        public Dictionary<string, Race>         AllRaces        = new Dictionary<string, Race>(); // Data about the race (traits etc)
        public Dictionary<string, Star>         AllStars        = new Dictionary<string, Star>();
        public Dictionary<string, Minefield>    AllMinefields   = new Dictionary<string, Minefield>();
        public List<Message>                    AllMessages     = new List<Message>(); // All messages generated this turn.

        public bool GameInProgress      = false;
        public int FleetID              = 1;
        public int TurnYear             = 2100;
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
                        case "fleetid":
                            FleetID = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
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
                                AllTechLevels.Add(tNode.Attributes["Id"].Value, int.Parse(tNode.FirstChild.Value));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "alldesigns":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllDesigns.Add(tNode.Attributes["Id"].Value, new Design(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allfleets":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllFleets.Add(tNode.Attributes["Id"].Value, new Fleet(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allracedata":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllRaceData.Add(tNode.Attributes["Id"].Value, new RaceData(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allraces":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                Race race = new Race();
                                race.LoadRaceFromXml(tNode);
                                AllRaces.Add(xmlnode.Attributes["Id"].Value, race);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allstars":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllStars.Add(tNode.Attributes["Id"].Value, new Star(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        
                        case "allminefields":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                AllMinefields.Add(tNode.Attributes["Id"].Value, new Minefield(tNode));
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
//            using (FileStream stateFile = new FileStream(StatePathName, FileMode.Open))
//            {
//                XmlDocument xmldoc = new XmlDocument();
//
//                xmldoc.Load(stateFile);
//                
//                ServerState serverState = new ServerState(xmldoc);
//                
//                return serverState;
//            }

                ServerState serverState = new ServerState();
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {                    
                    serverState = (ServerState)Serializer.Deserialize(stream);
                
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

            using (FileStream stream = new FileStream(StatePathName, FileMode.Create))
            {
                Serializer.Serialize(stream, this);
                
            }
            
//            ToXml();
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
            Global.SaveData(xmldoc, xmlelServerState, "FleetID", FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture));
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
                child.SetAttribute("Id", race.Key);                
                xmlelAllRaces.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllRaces);
            
            // Store the Race Data
            XmlElement xmlelAllRaceData = xmldoc.CreateElement("AllRaceData");
            foreach (KeyValuePair<string, RaceData> raceData in AllRaceData)
            {
                child = raceData.Value.ToXml(xmldoc);
                child.SetAttribute("Id", raceData.Key);                
                xmlelAllRaceData.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllRaceData);
            
            // Store the tech level sums.
            XmlElement xmlelAllTechLevels = xmldoc.CreateElement("AllTechLevels");
            foreach (KeyValuePair<string, int> techLevels in AllTechLevels)
            {
                child = xmldoc.CreateElement("TechLevels");                
                child.SetAttribute("Id", techLevels.Key);
                child.InnerText = techLevels.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                xmlelAllTechLevels.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllTechLevels);  
            
            // Store the Stars
            XmlElement xmlelAllStars = xmldoc.CreateElement("AllStars");
            foreach (KeyValuePair<string, Star> star in AllStars)
            {
                child = star.Value.ToXml(xmldoc);
                child.SetAttribute("Id", star.Key);                
                xmlelAllStars.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllStars);

            // Store the designs
            XmlElement xmlelAllDesigns = xmldoc.CreateElement("AllDesigns");
            foreach (KeyValuePair<string, Design> design in AllDesigns)
            {
                
                if (design.Value.Type == "Ship" || design.Value.Type == "Starbase")
                {
                    child = ((ShipDesign)design.Value).ToXml(xmldoc);                    
                }
                else
                {
                    child = design.Value.ToXml(xmldoc);
                }    
                
                child.SetAttribute("Id", design.Key);
                xmlelAllDesigns.AppendChild(child);                                            
            }
            xmlelServerState.AppendChild(xmlelAllDesigns);
            
            // Store the fleets
            XmlElement xmlelAllFleets = xmldoc.CreateElement("AllFleets");
            foreach (KeyValuePair<string, Fleet> fleet in AllFleets)
            {   
                child = fleet.Value.ToXml(xmldoc);
                child.SetAttribute("Id", fleet.Key);                
                xmlelAllFleets.AppendChild(child);
            }
            xmlelServerState.AppendChild(xmlelAllFleets);

            // Store the Minefields
            XmlElement xmlelAllMinefields = xmldoc.CreateElement("AllMinefields");
            foreach (KeyValuePair<string, Minefield> minefield in AllMinefields)
            {
                child = minefield.Value.ToXml(xmldoc);
                child.SetAttribute("Id", minefield.Key);                
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
            AllRaceData.Clear();
            AllRaces.Clear();
            AllStars.Clear();
            AllMinefields.Clear();
            AllMessages.Clear();
            
            GameFolder     = null;
            GameInProgress = false;
            FleetID        = 1;
            TurnYear       = 2100;            
            StatePathName  = null;  
        }

    }
}
