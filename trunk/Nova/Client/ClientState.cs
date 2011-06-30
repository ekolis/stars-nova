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
    using System.Windows.Forms;
    using System.Xml;

    using Nova.Common;
    using Nova.Common.Components;
    using Message = Nova.Common.Message;
    
    /// <summary>
    /// Brings together references to all the data which the GUI needs
    /// and provides the means to persist that data between sessions. This is also
    /// used by the AI, hence it is applicable to any Nova client. 
    /// </summary>
    [Serializable]
    public sealed class ClientState
    {
        public EmpireData       EmpireIntel     = new EmpireData();
        
        public List<string>     DeletedDesigns  = new List<string>();
        public List<string>     DeletedFleets   = new List<string>();
        public List<Message>    Messages        = new List<Message>();
       
        public Dictionary<string, Design> EnemyDesigns = new Dictionary<string, Design>();        
        
        public Intel            InputTurn           = null;
        public RaceComponents   AvailableComponents = null;    
        
        public bool FirstTurn   = true;  
        
        public string GameFolder    = null;
        
        public string StatePathName; // path&filename

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ClientState() 
        { 
        }
        
        /// <summary>
        /// Load <see cref="Intel">ClientState</see> from an xml document
        /// </summary>
        /// <param name="xmldoc">produced using XmlDocument.Load(filename)</param>
        public ClientState(XmlDocument xmldoc)
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
                        case "clientstate":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        
                        case "empiredata":
                            EmpireIntel = new EmpireData(xmlnode);
                            break;                        
                        case "deletedfleets":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                DeletedFleets.Add(tNode.FirstChild.Value);
                                tNode = tNode.NextSibling;
                            }
                            break;                        
                        case "deleteddesigns":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                DeletedDesigns.Add(tNode.FirstChild.Value);
                                tNode = tNode.NextSibling;
                            }
                            break;                        
                        case "message":
                            Messages.Add(new Message(xmlnode));
                            break;                        
                        case "enemydesigns":
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            {
                                if (tNode.Name.ToLower() == "design")
                                {
                                    Design design = new Design(tNode);
                                    EnemyDesigns.Add(design.Key, design);
                                }
                                else if (tNode.Name.ToLower() == "shipdesign")
                                {
                                    ShipDesign design = new ShipDesign(tNode);
                                    EnemyDesigns.Add(design.Key, design);
                                }
                                else
                                {
                                    throw new System.NotImplementedException("Unrecognised design type.");
                                }
                                tNode = tNode.NextSibling;
                            }
                            break;
                        case "intel":
                            //THIS HAS TO GO!
                            InputTurn = new Intel();
                            InputTurn.LoadFromXmlNode(xmlnode);
                            break;                        
                        case "availablecomponents":
                            AvailableComponents = new RaceComponents();
                            tNode = xmlnode.FirstChild;
                            while (tNode != null)
                            { 
                                AvailableComponents.Add(new Component(tNode));
                                tNode = tNode.NextSibling;
                            }
                            break;
                        case "firstturn":
                            FirstTurn = bool.Parse(xmlnode.FirstChild.Value);
                            break;
                        case "gamefolder":
                            GameFolder = xmlnode.FirstChild.Value;
                            break;
                        case "statepathname":
                            StatePathName = xmlnode.FirstChild.Value;
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

        /// <summary>
        /// Initialise the data needed for the GUI to run.
        /// </summary>
        /// <param name="argArray">The command line arguments.</param>
        public void Initialize(string[] argArray)
        {
            // Restore the component definitions. Must be done first so that components used
            // in designs can be linked to when loading the .intel file.
            try
            {
                AllComponents.Restore();
            }
            catch
            {
                Report.FatalError("Could not restore component definition file.");
            }

            // Need to identify the RaceName so we can load the correct race's intel.
            // We also want to identify the ClientState data store, if any, so we
            // can load it and use any historical information in there. 
            // Last resort we will ask the user what to open.

            // There are a number of starting scenarios:
            // 1. the Nova GUI was started directly (e.g. in the debugger). 
            //    There will be zero options/arguments in the argArray.
            //    We will continue an existing game, if any. 
            //     - get GameFolder from the config file
            //     - look for races and ask the user to pick one. If none need to
            //       ask the user to open a game (.intel), then treat as per option 2.
            //     - Build the stateFileName from the GameFolder and Race name and 
            //       load it, if it exists. If not don't care.
            // 2. the Nova GUI was started from Nova Launcher's "open a game option". 
            //    or by double clicking a race in the Nova Console
            //    There will be a .intel file listed in the argArray.
            //    Evenything we need should be found in there. 
            // 3. the Nova GUI was started from the launcher to continue a game. 
            //    There will be a StateFileName in the argArray.
            //    Directly load the state file. If it is missing - FatalError.
            //    (The race name and game folder will be loaded from the state file)
            StatePathName = null;
            string intelFileName = null;

            // process the arguments
            CommandArguments commandArguments = new CommandArguments(argArray);

            if (commandArguments.Contains(CommandArguments.Option.RaceName))
            {
                EmpireIntel.EmpireRace.Name = commandArguments[CommandArguments.Option.RaceName];
            }
            if (commandArguments.Contains(CommandArguments.Option.StateFileName))
            {
                StatePathName = commandArguments[CommandArguments.Option.StateFileName];
            }
            if (commandArguments.Contains(CommandArguments.Option.IntelFileName))
            {
                intelFileName = commandArguments[CommandArguments.Option.IntelFileName];
            }

            // Get the name of the folder where all the game files will be stored. 
            // Normally this would be placed in the config file by the NewGame wizard.
            // We also cache a copy in the ClientState.Data.GameFolder
            GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            
            if (GameFolder == null)
            {
                Report.FatalError("ClientState.cs Initialize() - An expected config file entry is missing\n" +
                                  "Have you ran the Race Designer and \n" +
                                  "Nova Console?");
            }

            // Sort out what we need to initialise the ClientState
            bool isLoaded = false;

            // 1. the Nova GUI was started directly (e.g. in the debugger). 
            //    There will be zero options/arguments in the argArray.
            //    We will continue an existing game, if any. 
            if (argArray.Length == 0)
            {
                // - get GameFolder from the conf file - already done.

                // - look for races and ask the user to pick one. 
                EmpireIntel.EmpireRace.Name = SelectRace(GameFolder);
                if (!string.IsNullOrEmpty(EmpireIntel.EmpireRace.Name))
                {
                    isLoaded = true;
                }
                else
                {
                    // If none need to ask the user to open a game (.intel), 
                    // then treat as per option 2.
                    try
                    {
                        OpenFileDialog fd = new OpenFileDialog();
                        fd.Title = "Open Game";
                        fd.FileName = "*." + Global.IntelExtension;
                        DialogResult result = fd.ShowDialog();
                        if (result != DialogResult.OK)
                        {
                            Report.FatalError("ClientState.cs Initialize() - Open Game dialog canceled. Exiting. Try running the NovaLauncher.");
                        }
                        intelFileName = fd.FileName;
                    }
                    catch
                    {
                        Report.FatalError("ClientState.cs Initialize() - Unable to open a game. Try running the NovaLauncher.");
                    }
                }
            }

            // 2. the Nova GUI was started from the launcher open a game option. 
            //    There will be a .intel file listed in the argArray.
            if (! isLoaded && intelFileName != null)
            {
                if (File.Exists(intelFileName))
                {
                    // Evenything we need should be found in there.
                    IntelReader intelReader = new IntelReader(this);
                    intelReader.ReadIntel(intelFileName);
                    isLoaded = true;
                }
                else
                {
                    Report.FatalError("ClientState.cs Initialize() - Could not locate .intel file \"" + intelFileName + "\".");
                }
            }

            // 3. the Nova GUI was started from the launcher to continue a game. 
            //    There will be a StateFileName in the argArray.
            // NB: we already copied it to ClientState.Data.StateFileName, but other
            // code sets that too, so check the arguments to see if it was there.
            if (!isLoaded && commandArguments.Contains(CommandArguments.Option.StateFileName))
            {
                // The state file is not sufficient to load a turn. We need the .intel
                // for this race. What race? The state file can tell us.
                // (i.e. The race name and game folder will be loaded from the state file)
                // If it is missing - FatalError.
                if (File.Exists(StatePathName))
                {
                    Restore();
                    IntelReader intelReader = new IntelReader(this);
                    intelReader.ReadIntel(intelFileName);
                    isLoaded = true;
                }
                else
                {
                    Report.FatalError("ClientState.cs Initialize() - File not found. Could not continue game \"" + StatePathName + "\".");
                }
            }


            if (!isLoaded)
            {
                Report.FatalError("ClientState.cs Initialise() - Failed to find any .intel when initialising turn");
            }            
            
            // See which components are available.
            UpdateAvailableComponents();
            
            FirstTurn = false;
        }

        /// <summary>
        /// Determine which tech components the player has access too
        /// </summary>
        private void UpdateAvailableComponents()
        {
            if (AvailableComponents == null)
            {
                AvailableComponents = new RaceComponents(EmpireIntel.EmpireRace, EmpireIntel.ResearchLevels);
            }
            else
            {
                try
                {
                    AvailableComponents.DetermineRaceComponents(EmpireIntel.EmpireRace, EmpireIntel.ResearchLevels);
                }
                catch
                {
                    Report.FatalError("Could not restore component definition file.");
                }
            }
        }

        /// <summary>
        /// Restore the GUI persistent data if the state store file exists (it typically
        /// will not on the very first turn of a new game). 
        /// </summary>
        /// <remarks>
        /// Later on, when we read the
        /// file Nova.intel we will reset the persistent data fields if the turn file
        /// indicates the first turn of a new game.
        /// </remarks>
        public ClientState Restore()
        {
            ClientState newState = Restore(GameFolder, EmpireIntel.EmpireRace.Name);
            
            DeletedDesigns  = newState.DeletedDesigns;
            DeletedFleets   = newState.DeletedFleets;
            Messages        = newState.Messages;
           
            EnemyDesigns   = newState.EnemyDesigns;     
            
            InputTurn           = newState.InputTurn;
            AvailableComponents = newState.AvailableComponents;
            
            EmpireIntel = newState.EmpireIntel;
            
            FirstTurn     = newState.FirstTurn;             
            GameFolder    = newState.GameFolder; 
            StatePathName = newState.StatePathName;
            
            LinkClientStateReferences();
            
            return this;
        }

        /// <summary>
        /// Restore the GUI persistent data if the state store file exists (it typically
        /// will not on the very first turn of a new game). 
        /// </summary>
        /// <param name="gameFolder">The path where the game files (specifically RaceName.state can be found.</param>
        /// <remarks>
        /// Later on, when we read the
        /// file Nova.intel we will reset the persistent data fields if the turn file
        /// indicates the first turn of a new game.
        /// </remarks>
        public ClientState Restore(string gameFolder)
        {
            // Scan the game directory for .race files. If only one is present then that is
            // the race we will use (single race test bed or remote server). If more than one is
            // present then display a dialog asking the player which race he wants to use
            // (multiplayer game with all players playing from a single game directory).
            string raceName = SelectRace(gameFolder);
            return Restore(gameFolder, raceName);
        }   
                           
        /// <summary>
        /// Restore the GUI persistent data if the state store file exists (it typically
        /// will not on the very first turn of a new game). 
        /// </summary>
        /// <param name="gameFolder">The path where the game files (specifically RaceName.state can be found.</param>
        /// <param name="raceName">Name of the race to load.</param>
        /// <remarks>
        /// Later on, when we read the
        /// file Nova.intel we will reset the persistent data fields if the turn file
        /// indicates the first turn of a new game.
        /// </remarks>
        public ClientState Restore(string gameFolder, string raceName)
        {            
            StatePathName = Path.Combine(gameFolder, raceName + Global.ClientStateExtension);
            ClientState clientState = new ClientState();
            
            if (File.Exists(StatePathName))
            {
                try
                {
                    using (FileStream stream = new FileStream(StatePathName, FileMode.Open))
                    {
                        XmlDocument xmldoc = new XmlDocument();

                        xmldoc.Load(stream);
                        
                        clientState = new ClientState(xmldoc);                        
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Unable to read state file, race history will not be available." + Environment.NewLine + "Details: " + e.Message);
                }
            }
            else
            {
                // This is temporary, prevents a crash. InputTurn will eventually be removed.
                clientState.InputTurn = new Intel();
            }
            
            // Copy the game folder names into the state data store.
            // Do this here or else there is no default state path
            // if no statefile was loaded.
            clientState.GameFolder      = gameFolder;
            clientState.StatePathName   = StatePathName;

            return clientState;
        }

        /// <summary>
        /// Save the GUI global data and flag that we should now be able to restore it.
        /// </summary>
        public void Save()
        {
            using (FileStream stream = new FileStream(StatePathName, FileMode.Create))
            {
                ToXml(stream);    
            }
        }
        
        private void ToXml(FileStream stream)
        {
            // Setup the XML document
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);
                
                XmlElement xmlelClientState = xmldoc.CreateElement("ClientState");
                xmlRoot.AppendChild(xmlelClientState);
                
                // Empire Data
                xmlelClientState.AppendChild(EmpireIntel.ToXml(xmldoc));
                
                // Deleted Fleets
                XmlElement xmlelDeletedFleets = xmldoc.CreateElement("DeletedFleets");
                foreach (string fleetKey in DeletedFleets)
                {
                    // only need to store enough data to find the deleted fleet.
                    Global.SaveData(xmldoc, xmlelDeletedFleets, "FleetKey", fleetKey);
                }
                xmlelClientState.AppendChild(xmlelDeletedFleets);
                
                // Deleted Designs
                XmlElement xmlelDeletedDesigns = xmldoc.CreateElement("DeletedDesigns");
                foreach (string designKey in DeletedDesigns)
                {
                    Global.SaveData(xmldoc, xmlelDeletedDesigns, "DesignKey", designKey);
                }
                xmlelClientState.AppendChild(xmlelDeletedDesigns);
                
                // Messages
                foreach (Nova.Common.Message message in Messages)
                {
                   xmlelClientState.AppendChild(message.ToXml(xmldoc));
                }
    
                // Enemy Designs
                XmlElement xmlelEnemyDesigns = xmldoc.CreateElement("EnemyDesigns");
                foreach (Design design in EnemyDesigns.Values)
                {
                    if (design.Type == "Starbase" || design.Type == "Ship")
                    {
                        xmlelEnemyDesigns.AppendChild(((ShipDesign)design).ToXml(xmldoc));
                    }
                    else
                    {
                        xmlelEnemyDesigns.AppendChild(design.ToXml(xmldoc));
                    }
                }
                xmlelClientState.AppendChild(xmlelEnemyDesigns);
                
                // THIS HAS TO GO!
                xmlelClientState.AppendChild(InputTurn.ToXml(xmldoc));
                
                // Available Components
                XmlElement xmlelAvaiableComponents = xmldoc.CreateElement("AvailableComponents");
                foreach (Component component in AvailableComponents.Values)
                {
                    xmlelAvaiableComponents.AppendChild(component.ToXml(xmldoc));
                }
                xmlelClientState.AppendChild(xmlelAvaiableComponents);
                
                Global.SaveData(xmldoc, xmlelClientState, "FirstTurn", FirstTurn.ToString());
                Global.SaveData(xmldoc, xmlelClientState, "GameFolder", GameFolder);
                Global.SaveData(xmldoc, xmlelClientState, "StatePathName", StatePathName);
                
                xmldoc.Save(stream);     
        }

        /// <summary>
        /// Pop up a dialog to select the race to play
        /// </summary>
        /// <param name="gameFolder">The folder to look in for races.</param>
        /// <remarks>
        /// FIXME (priority 6) - This is unsafe as these may not be the races playing.
        /// </remarks>
        /// <returns>The name of the race to play.</returns>
        private string SelectRace(string gameFolder)
        {
            string raceName = null;

            DirectoryInfo directory = new DirectoryInfo(gameFolder);
            FileInfo[] raceFiles = directory.GetFiles("*" + Global.RaceExtension);

            if (raceFiles.Length == 0)
            {
                Report.FatalError("The Nova GUI cannot start unless a race file is present");
            }


            if (raceFiles.Length > 1)
            {
                SelectRaceDialog raceDialog = new SelectRaceDialog();

                foreach (FileInfo file in raceFiles)
                {
                    string pathName = file.FullName;
                    raceName = Path.GetFileNameWithoutExtension(pathName);
                    raceDialog.RaceList.Items.Add(raceName);
                }

                raceDialog.RaceList.SelectedIndex = 0;

                DialogResult result = raceDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    Report.FatalError("The Nova GUI cannot start unless a race has been selected");
                }

                raceName = raceDialog.RaceList.SelectedItem as string;

                raceDialog.Dispose();
            }
            else
            {
                string pathName = raceFiles[0].FullName;
                raceName = Path.GetFileNameWithoutExtension(pathName);
            }

            return raceName;
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
        private void LinkClientStateReferences()
        {
            // Fleet reference to Star
            foreach (FleetIntel fleet in EmpireIntel.FleetReports.Values)
            {
                if (fleet.InOrbit != null)
                {
                    fleet.InOrbit = EmpireIntel.StarReports[fleet.InOrbit.Name];
                }
                // Ship reference to Design
                foreach (Ship ship in fleet.FleetShips)
                {
                    ship.DesignUpdate(InputTurn.AllDesigns[ship.Owner + "/" + ship.DesignName] as ShipDesign);
                }
            }
            
            
            // HullModule reference to a component
            foreach (Design design in InputTurn.AllDesigns.Values)
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

            foreach (StarIntel star in EmpireIntel.StarReports.Values)
            {
                if (star.ThisRace != null)
                {
                    // Reduntant, but works to check if race name is valid...
                    if (star.Owner == EmpireIntel.Id)
                    {
                        star.ThisRace = EmpireIntel.EmpireRace;
                    }
                    else
                    {
                        star.ThisRace = null;
                    }
                }

                if (star.Starbase != null)
                {
                    star.Starbase = EmpireIntel.FleetReports[star.Name + " Starbase"];
                }
            }
        }     

    }
}

