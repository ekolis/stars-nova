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

        public ArrayList DeletedDesigns = new ArrayList();
        public ArrayList DeletedFleets = new ArrayList();
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
        public TechLevel ResearchLevel = new TechLevel(); // current level of technology
        public TechLevel ResearchResources = new TechLevel();
        public bool FirstTurn = true;
        public double ResearchAllocation = 0;
        public int ResearchBudget = 10;
        public int TurnYear = 0;
        public string GameFolder = null;
        public string RaceName = null;
        public TechLevel.ResearchField ResearchTopic = TechLevel.ResearchField.Energy;


        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static ClientState Instance;
        private static string StatePathName; // path&filename
        private static AllComponents ComponentData = AllComponents.Data;


        #region Constructors
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Private constructor for static only class preventing external object from instantiating this class directly.
        /// </summary>
        /// <remarks>
        /// Use a private constructor so the default constructor is not created since all methods are static.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private ClientState() { }
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
                    if (Instance == null)
                    {
                        Instance = new ClientState();
                    }
                }
                return Instance;
            }

            set
            {
                Instance = value;
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
            // Need to identify the RaceName so we can load the correct race's intel.
            // We also want to identify the ClientState data store, if any, so we
            // can load it and use any historical information in there. 
            // Last resort we will ask the user what to open.

            // There are a number of starting scenarios:
            // 1. the Nova GUI was started directly (e.g. in the debugger). 
            //    There will be zero options/arguments in the argArray.
            //    We will continue an existing game, if any. 
            //     - get GameFolder from the registry
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
            Data.RaceName = null;
            string IntelFileName = null;
            int TurnToPlay = -1;

            // process the arguments
            CommandArguments commandArguments = new CommandArguments(argArray);

            if (commandArguments.Contains(CommandArguments.Option.RaceName))
            {
                Data.RaceName = commandArguments[CommandArguments.Option.RaceName];
            }
            if (commandArguments.Contains(CommandArguments.Option.Turn))
            {
                try
                {
                    TurnToPlay = int.Parse(commandArguments[CommandArguments.Option.Turn], System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    string message = "ClientState.cs: Initialize() - Invalid turn number \"" + commandArguments[CommandArguments.Option.Turn] + "\".";
                    Report.FatalError(message);

                }
            }
            if (commandArguments.Contains(CommandArguments.Option.StateFileName))
            {
                StatePathName = commandArguments[CommandArguments.Option.StateFileName];
            }
            if (commandArguments.Contains(CommandArguments.Option.IntelFileName))
            {
                IntelFileName = commandArguments[CommandArguments.Option.IntelFileName];
            }


            // ----------------------------------------------------------------------------
            // Get the name of the folder where all the game files will be stored. 
            // Normally this would be placed in the registry by the NewGame wizard.
            // We also cache a copy in the ClientState.Data.GameFolder
            // ----------------------------------------------------------------------------

            ClientState.Data.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            if (ClientState.Data.GameFolder == null)
            {
                Report.FatalError("ClientState.cs Initialize() - An expected registry entry is missing\n" +
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
                // - get GameFolder from the registry - already done.

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
                        IntelFileName = fd.FileName;
                    }
                    catch
                    {
                        Report.FatalError("ClientState.cs Initialize() - Unable to open a game. Try running the NovaLauncher.");

                    }
                }

            }


            // 2. the Nova GUI was started from the launcher open a game option. 
            //    There will be a .intel file listed in the argArray.
            if (! isLoaded && IntelFileName != null)
            {
                if (File.Exists(IntelFileName))
                {
                    // Evenything we need should be found in there. 
                    IntelReader.ReadIntel(IntelFileName);
                    isLoaded = true;
                }
                else
                {
                    Report.FatalError("ClientState.cs Initialize() - Could not locate .intel file \"" + IntelFileName + "\".");
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
                    ClientState.Restore();
                    string newIntelFileName = Path.GetDirectoryName(StatePathName) +
                        ClientState.Data.RaceName + Global.IntelExtension;
                    IntelReader.ReadIntel(IntelFileName);
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

            // ----------------------------------------------------------------------------
            // Restore the component definitions.
            // ----------------------------------------------------------------------------


            if (ClientState.Data.FirstTurn)
            {
                ClientState.Data.BattlePlans.Add("Default", new BattlePlan());
                ProcessRaceDefinition();
            }

            try
            {
                AllComponents.Restore();
                ClientState.Data.AvailableComponents.DetermineRaceComponents(ClientState.Data.PlayerRace, Data.ResearchLevel);
            }
            catch
            {
                Report.FatalError("Could not restore component definition file.");
            }

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

                ProcessPrimaryTraits();
                ProcessSecondaryTraits();
            }
            else
            {
                Report.FatalError("Unable to locate race definition \"" + raceFileName + "\"");
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the Primary Traits for this race.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private static void ProcessPrimaryTraits()
        {
            // TODO (priority 4) Special Components
            // Races are granted access to components currently based on tech level and primary/secondary traits (not tested).
            // Need to grant special access in a few circumstances
            // 1. JOAT Hulls with pen scans. (either make a different hull with a built in pen scan, of the same name and layout; or modify scanning and scan display functions)
            // 2. Mystery Trader Items - probably need to implement the idea of 'hidden' technology to cover this.

            // TODO (priority 4) Starting Tech
            // Need to specify starting tech levels. These must be checked by the server/console. Started below - Dan 26 Jan 10

            // TODO (priority 4) Implement Starting Items

            ClientState.Data.ResearchLevel = new TechLevel(0);

            switch (ClientState.Data.PlayerRace.Traits.Primary.Code)
            {
                case "HE":
                    // Start with one armed scout + 3 mini-colony ships
                    break;

                case "SS":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Electronics] = 5;
                    // Start with one scout + one colony ship.
                    break;

                case "WM":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] = 1;
                    // Start with one armed scout + one colony ship.
                    break;

                case "CA":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Weapons] = 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] = 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Biotechnology] = 6;
                    // Start with an orbital terraforming ship
                    break;

                case "IS":
                    // Start with one scout and one colony ship
                    break;

                case "SD":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = 2;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Biotechnology] = 2;
                    // Start with one scout, one colony ship, Two mine layers (one standard, one speed trap)
                    break;

                case "PP":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = 5;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Construction] = 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Biotechnology] = 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Electronics] = 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] = 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Weapons] = 3;
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;

                default:
                    Report.Error("GuiState.cs - ProcessPrimaryTraits() - Unknown Primary Trait \"" + ClientState.Data.PlayerRace.Traits.Primary.Code + "\"");
                    break;
            } // switch on PRT

#if (DEBUG)
            // Just for testing
            // TODO (priority 4) get this from a settings file, or other central location for convenience.
            ClientState.Data.ResearchLevel = new TechLevel(26);
#endif

            if (ClientState.Data.AvailableComponents == null)
                ClientState.Data.AvailableComponents = new RaceComponents(ClientState.Data.PlayerRace, ClientState.Data.ResearchLevel);
            else
                ClientState.Data.AvailableComponents.DetermineRaceComponents(ClientState.Data.PlayerRace, ClientState.Data.ResearchLevel);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Read the Secondary Traits for this race.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private static void ProcessSecondaryTraits()
        {
            // TODO (priority 4) finish the rest of the LRTs.
            // Not all of these properties are fully implemented here, as they may require changes elsewhere in the game engine.
            // Where a trait is listed as 'TODO ??? (priority 4)' this means it first needs to be checked if it has been implemented elsewhere.

            if (ClientState.Data.PlayerRace.Traits.Contains("IFE"))
            {
                // Ships burn 15% less fuel : TODO ??? (priority 4)

                // Fuel Mizer and Galaxy Scoop engines available : Implemented in component definitions.

                // propulsion tech starts one level higher
                int level = (int)ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion];
                level++;
                ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] = level;
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("TT"))
            {
                // Begin the game able to adjust each environment attribute up to 3%
                // Higher levels of terraforming are available : implemented in component definitions.
                // Total Terraforming requires 30% fewer resources : implemented in component definitions.
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("ARM"))
            {
                // Grants access to three additional mining hulls and two new robots : implemented in component definitions.
                // Start the game with two midget miners : TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("ISB"))
            {
                // Two additional starbase designs (space dock & ultra station) : implemented in component definitions.
                // Starbases have built in 20% cloacking : TODO ??? (priority 4)

                // Improved Starbases gives a 20% discount to starbase hulls.
                foreach (Component component in ClientState.Data.AvailableComponents.Values)
                {
                    // TODO (priority 3) - work out why it sometimes is null.
                    if (component == null || component.Type != "Hull") continue;
                    Hull hull = component.Properties["Hull"] as Hull;
                    if (hull == null || !hull.IsStarbase) continue;

                    Resources cost = component.Cost;
                    cost.Boranium *= 0.8;
                    cost.Ironium *= 0.8;
                    cost.Germanium *= 0.8;
                    cost.Energy *= 0.8;
                }
            }

            if (ClientState.Data.PlayerRace.Traits.Contains("GR"))
            {
                // 50% resources go to selected research field. 15% to each other field. 115% total. TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("UR"))
            {
                // Affects minerals and resources returned due to scrapping. TODO ??? (priority 4).
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("MA"))
            {
                // One instance of mineral alchemy costs 25 resources instead of 100. TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("NRSE"))
            {
                // affects which engines are available : implemented in component definitions.
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("OBRM"))
            {
                // affects which mining robots will be available : implemented in component definitions.
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("CE"))
            {
                // Engines cost 50% less TODO (priority 4)
                // Engines have a 10% chance of not engaging above warp 6 : TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("NAS"))
            {
                // No access to standard penetrating scanners : implemented in component definitions.
                // Ranges of conventional scanners are doubled : TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("LSP"))
            {
                // Starting population is 17500 instead of 25000 : TODO ??? (priority 4)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("BET"))
            {
                // TODO ??? (priority 4)
                // New technologies initially cost twice as much to build. 
                // Once all tech requirements are exceeded cost is normal. 
                // Miniaturization occurs at 5% per level up to 80% (instead of 4% per level up to 75%)
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("RS"))
            {
                // TODO ??? (priority 4)
                // All shields are 40% stronger than the listed rating.
                // Shields regenrate at 10% of max strength each round of combat.
                // All armors are 50% of their rated strength.
            }
            if (ClientState.Data.PlayerRace.Traits.Contains("ExtraTech"))
            {
                //All extra technologies start on level 3 or 4 with JOAT
                if (ClientState.Data.PlayerRace.Traits.Primary.Code == "JOAT")
                {
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] += 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Construction] += 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Biotechnology] += 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Electronics] += 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] += 1;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Weapons] += 1;
                }
                else
                {
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Propulsion] += 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Construction] += 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Biotechnology] += 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Electronics] += 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Energy] += 3;
                    ClientState.Data.ResearchLevel[TechLevel.ResearchField.Weapons] += 3;
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

            StatePathName = Path.Combine(gameFolder, raceName + Global.ClientStateExtension);

            if (File.Exists(StatePathName))
            {
                try
                {
                    using (Stream stateFile = new FileStream(StatePathName, FileMode.Open))
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

            using (Stream stateFile = new FileStream(StatePathName, FileMode.Create))
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

