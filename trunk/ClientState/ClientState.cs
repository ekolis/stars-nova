#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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

#region Module Description
// ===========================================================================
// This module brings together references to all the data which the GUI needs
// and provides the means to persist that data between sessions. This is also
// used by the AI, hence it is applicable to any Nova client.
// ===========================================================================
#endregion

#region Using Statements

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Nova.Common;
using Nova.Common.Components;

#endregion

namespace Nova.Client
{
    [Serializable]
    public sealed class ClientState
    {
        // ============================================================================
        // Data used by the GUI that will be persistent across multiple program
        // invocations.
        // ============================================================================

        public List<string> DeletedDesigns = new List<string>();
        public List<string> DeletedFleets = new List<string>();
        public ArrayList Messages = new ArrayList();
        public Intel InputTurn = null;
        public Hashtable BattlePlans = new Hashtable();
        public Hashtable KnownEnemyDesigns = new Hashtable();
        public Hashtable PlayerRelations = new Hashtable();
        public Hashtable StarReports = new Hashtable();
        public RaceComponents AvailableComponents = null;
        public List<Fleet> PlayerFleets = new List<Fleet>();
        public StarList PlayerStars = new StarList(); 
        public Race PlayerRace = new Race();
        /// <summary>
        /// Gets information about technology levels for current client
        /// </summary>
        public TechLevel ResearchLevels = new TechLevel(); // current level of technology
        public TechLevel ResearchResources = new TechLevel();
        public TechLevel ResearchTopics = new TechLevel(0, 0, 1, 0, 0, 0);
        public int ResearchBudget = 10;
        
        public bool FirstTurn = true;
        public int TurnYear = 0;
        
        public string GameFolder = null;
        public string RaceName = null;   

        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static ClientState instance;
        private static string statePathName; // path&filename


        #region Constructors
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Private constructor for static only class preventing external object from instantiating this class directly.
        /// </summary>
        /// <remarks>
        /// Use a private constructor so the default constructor is not created since all methods are static.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private ClientState() 
        { 
        }

        #endregion


        #region Properties
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Gets the single instance of this class. Access to the data is thread-safe.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static ClientState Data
        {
            get
            {
                object padlock = new object();

                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClientState();
                    }
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialise the data needed for the GUI to run.
        /// </summary>
        /// <param name="argArray">The command line arguments.</param>
        /// ----------------------------------------------------------------------------
        public static void Initialize(string[] argArray)
        {
            // ----------------------------------------------------------------------------
            // Restore the component definitions. Must be done first so that components used
            // in designs can be linked to when loading the .intel file.
            // ----------------------------------------------------------------------------
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


            statePathName = null;
            Data.RaceName = null;
            string intelFileName = null;

            // process the arguments
            CommandArguments commandArguments = new CommandArguments(argArray);

            if (commandArguments.Contains(CommandArguments.Option.RaceName))
            {
                Data.RaceName = commandArguments[CommandArguments.Option.RaceName];
            }
            if (commandArguments.Contains(CommandArguments.Option.StateFileName))
            {
                statePathName = commandArguments[CommandArguments.Option.StateFileName];
            }
            if (commandArguments.Contains(CommandArguments.Option.IntelFileName))
            {
                intelFileName = commandArguments[CommandArguments.Option.IntelFileName];
            }


            // ----------------------------------------------------------------------------
            // Get the name of the folder where all the game files will be stored. 
            // Normally this would be placed in the config file by the NewGame wizard.
            // We also cache a copy in the ClientState.Data.GameFolder
            // ----------------------------------------------------------------------------

            ClientState.Data.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            if (ClientState.Data.GameFolder == null)
            {
                Report.FatalError("ClientState.cs Initialize() - An expected config file entry is missing\n" +
                                  "Have you ran the Race Designer and \n" +
                                  "Nova Console?");
            }


            // ----------------------------------------------------------------------------
            // Sort out what we need to initialise the ClientState
            // ----------------------------------------------------------------------------
            bool isLoaded = false;


            // 1. the Nova GUI was started directly (e.g. in the debugger). 
            //    There will be zero options/arguments in the argArray.
            //    We will continue an existing game, if any. 
            if (argArray.Length == 0)
            {
                // - get GameFolder from the conf file - already done.

                // - look for races and ask the user to pick one. 
                ClientState.Data.RaceName = SelectRace(ClientState.Data.GameFolder);
                if (ClientState.Data.RaceName != null && ClientState.Data.RaceName != "")
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
                    IntelReader.ReadIntel(intelFileName);
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
                if (File.Exists(statePathName))
                {
                    ClientState.Restore();
                    IntelReader.ReadIntel(intelFileName);
                    isLoaded = true;
                }
                else
                {
                    Report.FatalError("ClientState.cs Initialize() - File not found. Could not continue game \"" + statePathName + "\".");
                }

            }


            if (!isLoaded)
            {
                Report.FatalError("ClientState.cs Initialise() - Failed to find any .intel when initialising turn");
            }



            // Add the default battle plan if this is the first turn.
            if (ClientState.Data.FirstTurn)
            {
                ClientState.Data.BattlePlans.Add("Default", new BattlePlan());
                ProcessRaceDefinition();
            }            
            
            // See which components are available.
            UpdateAvailableComponents();
            
            // ----------------------------------------------------------------------------
            // Check the password for access to this race's data
            // ----------------------------------------------------------------------------
            /* TODO (priority 6) need to rework how passwords are used. They should be used to decrypt files. The current process is week security as the files are not encrypted and the password easily bypassed.
             * TODO (priority 7) ensure the AI can open its files without user input.
             * This section has been commented out until it can be reworked as it does no good and therefore isn't worth working out a bypass for the AI until it is reworked.
             * NB: this is how a hacker/cheat would bypass the current security - build a version of Nova GUI with this section commented out.
            // On first run a password is required to 'decrypt' the race's files. The password should be remembered by the running application between turns, until the application terminates.
            ControlLibrary.CheckPassword password =
               new ControlLibrary.CheckPassword(ClientState.Data.PlayerRace);

            if (bPasswordNeeded)
            {
                password.ShowDialog();
                password.Dispose();

                if (password.DialogResult == DialogResult.Cancel)
                {
                    System.Threading.Thread.CurrentThread.Abort();
                }
            }
            */
            
            // Add some initial state
            if (ClientState.Data.FirstTurn)
            {
                foreach (string raceName in ClientState.Data.InputTurn.AllRaceNames)
                {
                    ClientState.Data.PlayerRelations[raceName] = "Neutral";
                }
            }
            
            ClientState.Data.FirstTurn = false;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Read the race definition file into the persistent data store. If this is the
        /// very first turn of a new game then process it's content to set up initial
        /// race parameters (e.g. initial technology levels, etc.).
        /// </summary>
        /// <remarks>
        /// FIXME (priority 6) - this is unsafe as the .race file may have changed since the game was
        /// generated. Current thinking is that this should be included in the .intel file
        /// every turn. -- Dan Vale 10 Jan 10.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private static void ProcessRaceDefinition()
        {
            string raceFolder = FileSearcher.GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
            string raceFileName = Path.Combine(raceFolder, Data.RaceName + Global.RaceExtension);
            if (File.Exists(raceFileName))
            {
                Data.PlayerRace = new Race(raceFileName);
            }
            else
            {
                Report.FatalError("Unable to locate race definition \"" + raceFileName + "\"");
            }
        }
  
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine which tech components the player has access too
        /// </summary>
        /// ----------------------------------------------------------------------------        
        private static void UpdateAvailableComponents()
        {
            if (ClientState.Data.AvailableComponents == null)
            {
                ClientState.Data.AvailableComponents = new RaceComponents(ClientState.Data.PlayerRace, ClientState.Data.ResearchLevels);
            }
            else
            {
                try
                {
                    ClientState.Data.AvailableComponents.DetermineRaceComponents(ClientState.Data.PlayerRace, ClientState.Data.ResearchLevels);
                }
                catch
                {
                    Report.FatalError("Could not restore component definition file.");
                }
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Restore the GUI persistent data if the state store file exists (it typically
        /// will not on the very first turn of a new game). 
        /// </summary>
        /// <remarks>
        /// Later on, when we read the
        /// file Nova.intel we will reset the persistent data fields if the turn file
        /// indicates the first turn of a new game.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public static void Restore()
        {
            Restore(ClientState.Data.GameFolder, ClientState.Data.RaceName);
        }

        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        public static void Restore(string gameFolder)
        {

            // ----------------------------------------------------------------------------
            // Scan the game directory for .race files. If only one is present then that is
            // the race we will use (single race test bed or remote server). If more than one is
            // present then display a dialog asking the player which race he wants to use
            // (multiplayer game with all players playing from a single game directory).
            // ----------------------------------------------------------------------------

            string raceName = SelectRace(gameFolder);
            Restore(gameFolder, raceName);


        }


        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        public static void Restore(string gameFolder, string raceName)
        {

            statePathName = Path.Combine(gameFolder, raceName + Global.ClientStateExtension);

            if (File.Exists(statePathName))
            {
                try
                {
                    using (Stream stateFile = new FileStream(statePathName, FileMode.Open))
                    {

                        // Read in binary state file
                        ClientState.Data = Serializer.Deserialize(stateFile) as ClientState;

                    }
                }
                catch (Exception e)
                {
                    Report.Error("Unable to read state file, race history will not be available." + Environment.NewLine + "Details: " + e.Message);
                }
            }


            // Copy the race and game folder names into the state data store. This
            // is just a convenient way of making them globally available.

            Data.RaceName = raceName;
            Data.GameFolder = gameFolder;

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save the GUI global data and flag that we should now be able to restore it.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void Save()
        {

            using (Stream stateFile = new FileStream(statePathName, FileMode.Create))
            {
                // Binary Serialization (old)
                Serializer.Serialize(stateFile, ClientState.Data);
            }

            // Xml Serialization - incomplete - Dan 16 Jan 09 - deferred while alternate means are investigated
            /*
           GZipStream compressionStream = new GZipStream(stateFile, CompressionMode.Compress);

           // Setup the XML document
           XmlDocument xmldoc = new XmlDocument();
           XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);

           // add the GuiState to the document
           XmlElement xmlelGuiState = xmldoc.CreateElement("GuiState");
           xmlRoot.AppendChild(xmlelGuiState);

            // Deleted Fleets
           XmlElement xmlelDeletedFleets = xmldoc.CreateElement("DeletedFleets");
           foreach (Fleet fleet in Data.DeletedFleets)
           {
               xmlelDeletedFleets.AppendChild(fleet.ToXml(xmldoc));
           }
           xmlelGuiState.AppendChild(xmlelDeletedFleets);

            // Deleted Designs
           XmlElement xmlelDeletedDesigns = xmldoc.CreateElement("DeletedDesigns");
           foreach (Design design in Data.DeletedDesigns)
           {
               if (design.Type == "Ship" || design.Type == "Starbase")
                   xmlelDeletedDesigns.AppendChild(((ShipDesign)design).ToXml(xmldoc));
               else
                   xmlelDeletedDesigns.AppendChild(design.ToXml(xmldoc));
           }
           xmlelGuiState.AppendChild(xmlelDeletedDesigns);

            // Messages
           foreach (Nova.Common.Message message in Data.Messages)
           {
               xmlelGuiState.AppendChild(message.ToXml(xmldoc));
           }

            // Battle Plans
           foreach (DictionaryEntry de in Data.BattlePlans)
           {
               BattlePlan plan = de.Value;
               xmlelGuiState.AppendChild(plan.ToXml());
           }

            // Player Relations
           foreach (DictionaryEntry de in Data.PlayerRelations)
           {
               XmlElement Relation = xmldoc.CreateElement("Relation");

           }

           // You can comment/uncomment the following lines to turn compression on/off if you are doing a lot of 
           // manual inspection of the save file. Generally though it can be opened by any archiving tool that
           // reads gzip format.
  #if (DEBUG)
           xmldoc.Save(stateFile);                                           //  not compressed
  #else
             xmldoc.Save(compressionStream); compressionStream.Close();    //   compressed 
  #endif

        */
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Pop up a dialog to select the race to play
        /// </summary>
        /// <param name="gameFolder">The folder to look in for races.</param>
        /// <remarks>
        /// FIXME (priority 6) - This is unsafe as these may not be the races playing.
        /// </remarks>
        /// <returns>The name of the race to play.</returns>
        /// ----------------------------------------------------------------------------
        private static string SelectRace(string gameFolder)
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

        #endregion
    }
}

