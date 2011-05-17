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

#region Module Description
// ===========================================================================
// This dialog will determine the objectives of the game.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Nova.Common;
using Nova.NewGame;
using Nova.Server;
using Nova.WinForms.Console;

namespace Nova.WinForms
{
    /// <Summary>
    /// The Stars! Nova - New Game Wizard <see cref="Form"/>.
    /// </Summary>
    public partial class NewGameWizard : Form
    {
        private int numberOfPlayers;
        private ServerState stateData;

        public Hashtable KnownRaces = new Hashtable();


        #region Initialisation

        /// <Summary>
        /// Initializes a new instance of the NewGameWizard class.
        /// </Summary>
        public NewGameWizard()
        {
            InitializeComponent();
            
            // New clean server state
            this.stateData = new ServerState();

            // Setup the list of known races.
            KnownRaces = FileSearcher.GetAvailableRaces();

            foreach (string raceName in KnownRaces.Keys)
            {

                // add known race to selectable races in race selection drop down
                raceSelectionBox.Items.Add(raceName);

                // add known race to list of players
                this.numberOfPlayers++;
                ListViewItem player = new ListViewItem("  " + this.numberOfPlayers.ToString());
                player.SubItems.Add(raceName);
                player.SubItems.Add("Human");
                playerList.Items.Add(player);

            }
            if (this.numberOfPlayers > 0)
            {
                raceSelectionBox.SelectedIndex = 0;
            }

            // Setup initial button states
            UpdatePlayerDetails();
            UpdatePlayerListButtons();
        }

        #endregion

        #region Main

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// The main entry Point for the application.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        [STAThread]
        public void Main()
        {

            // Establish the victory conditions:
            NewGameWizard newGameWizard = new NewGameWizard();

            do
            {
                DialogResult result = newGameWizard.ShowDialog();

                // Check for cancel being pressed.
                if (result != DialogResult.OK)
                {
                    // return to the game launcher
                    try
                    {
                        Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.LauncherSwitch);
                    }
                    catch
                    {
                        // there was a problem returning to the launcher and the user doesn't want the new game dialog open. Just quit.
                        Report.FatalError("Could not return to the Launcher.");
                    }
                    Application.Exit();
                    return; // need this as well to prevent execution of the rest of the loop, which will restart the NewGame dialog.
                }

                // New game dialog was OK, create a game
                // Get a location to save the game:
                FolderBrowserDialog gameFolderBrowser = new FolderBrowserDialog();
                gameFolderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
                gameFolderBrowser.SelectedPath = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                gameFolderBrowser.Description = "Choose New Game Folder";
                DialogResult gameFolderBrowserResult = gameFolderBrowser.ShowDialog();

                // Check for cancel being pressed (in the new game save file dialog).
                if (gameFolderBrowserResult != DialogResult.OK)
                {
                    // return to the new game wizard
                    continue;
                }

                // store the updated Game Folder information
                using (Config conf = new Config())
                {
                    conf[Global.ServerFolderKey] = gameFolderBrowser.SelectedPath;

                    stateData.GameFolder = gameFolderBrowser.SelectedPath;
                    // Don't set ClientFolderKey in case we want to simulate a LAN game 
                    // on one PC for testing. 
                    // Should be set when the ClientState is initialised. If that is by 
                    // launching Nova GUI from the console then the GameFolder will be 
                    // passed as the path to the .intel and the ClientFolderKey will then 
                    // be updated.

                    // Construct appropriate state and settings file names
                    stateData.StatePathName = gameFolderBrowser.SelectedPath + Path.DirectorySeparatorChar +
                                                     GameSettings.Data.GameName + Global.ServerStateExtension;
                    GameSettings.Data.SettingsPathName = gameFolderBrowser.SelectedPath + Path.DirectorySeparatorChar +
                                                         GameSettings.Data.GameName + Global.SettingsExtension;
                    conf[Global.ServerStateKey] = stateData.StatePathName;
                }
                // Copy the player & race data to the ServerState
                stateData.AllPlayers = newGameWizard.Players;
                
                foreach (PlayerSettings settings in stateData.AllPlayers)
                {
                    // TODO (priority 7) - need to decide how to handle two races of the same name. If they are the same, fine. If they are different, problem! Maybe the race name is a poor key???
                    // Stars! solution is to rename the race using a list of standard names. 
                    if (!stateData.AllRaces.Contains(settings.RaceName))
                    {
                        stateData.AllRaces.Add(settings.RaceName, newGameWizard.KnownRaces[settings.RaceName]);
                        
                        // Initialize clean data for them
                        stateData.AllRaceData[settings.RaceName] = new RaceData();

                        string raceFolder = FileSearcher.GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
                        string raceFileName = Path.Combine(raceFolder, settings.RaceName + Global.RaceExtension);
                        if (File.Exists(raceFileName))
                        {
                            Race race = new Race(raceFileName);
            
                            // Add initial state to the intel files.
                            ProcessPrimaryTraits(race);
                            ProcessSecondaryTraits(race);
                        }
                        else
                        {
                            Report.FatalError("Unable to locate race definition \"" + raceFileName + "\"");
                        }                    
                        
                        
                        
                    }
                    
                }

                
                StarMapInitialiser starMapInitialiser = new StarMapInitialiser(stateData);
                starMapInitialiser.GenerateStars();

                starMapInitialiser.InitialisePlayerData();

                try
                {
                    Scores scores = new Scores(stateData);
                    IntelWriter intelWriter = new IntelWriter(stateData, scores);
                    intelWriter.WriteIntel();
                    stateData.Save();
                    GameSettings.Save();
                }
                catch
                {
                    Report.Error("Creation of new game failed.");
                    continue;
                }

                // start the server
                try
                {
                    NovaConsoleMain novaConsole = new NovaConsoleMain();
                    Application.EnableVisualStyles();
                    Application.Run(novaConsole);
                    // Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.ConsoleSwitch);

                    return;
                }
                catch
                {
                    Report.FatalError("Unable to launch Console.");
                }

            }
            while (true); // keep trying to make a new game

        } // Main

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Occurs when the OK button is clicked.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OkButton_Click(object sender, EventArgs e)
        {
            GameSettings.Data.GameName = gameName.Text;
            GameSettings.Data.PlanetsOwned = planetsOwned.Value;
            GameSettings.Data.TechLevels = techLevels.Value;
            GameSettings.Data.NumberOfFields = numberOfFields.Value;
            GameSettings.Data.TotalScore = totalScore.Value;
            GameSettings.Data.ProductionCapacity = productionCapacity.Value;
            GameSettings.Data.CapitalShips = capitalShips.Value;
            GameSettings.Data.HighestScore = highestScore.Value;
            GameSettings.Data.TargetsToMeet = Int32.Parse(targetsToMeet.Text, System.Globalization.CultureInfo.InvariantCulture);
            GameSettings.Data.MinimumGameTime = Int32.Parse(minimumGameTime.Text, System.Globalization.CultureInfo.InvariantCulture);

            GameSettings.Data.MapHeight = (int)mapHeight.Value;
            GameSettings.Data.MapWidth = (int)mapWidth.Value;

            GameSettings.Data.StarSeparation = (int)starSeparation.Value;
            GameSettings.Data.StarDensity = (int)starDensity.Value;
            GameSettings.Data.StarUniformity = (int)starUniformity.Value;

            GameSettings.Data.AcceleratedStart = acceleratedStart.Checked;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Occurs when the Tutorial button is clicked.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void TutorialButton_Click(object sender, EventArgs eventArgs)
        {
            Report.Information("Sorry, there is no tutorial yet.");
            // TODO (priority 7): Load or create the tutorial client data.
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Add a new player to the player list
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AddPlayerButton_Click(object sender, EventArgs e)
        {
            // Add player (with a dummy number)
            ListViewItem player = new ListViewItem("  ##");
            player.SubItems.Add(raceSelectionBox.SelectedItem.ToString());
            player.SubItems.Add("Human");
            playerList.Items.Add(player);

            RenumberPlayers();

            playerList.SelectedIndices.Clear();
            playerList.SelectedIndices.Add(playerList.Items.Count - 1);
            numberOfPlayers = playerList.Items.Count;

        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'New Race' button is pressed, launch the Race Designer.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NewRaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.RaceDesignerSwitch);
            }
            catch
            {
                Report.Error("Failed to launch Race Designer.");
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update the GUI when the currently selected player changes.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayerDetails();
            UpdatePlayerListButtons();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'Delete' button is pressed, delete the currently selected player from the game.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerDeleteButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = -1;
            foreach (int index in playerList.SelectedIndices)
            {
                selectedIndex = index;
                playerList.Items.RemoveAt(index);

            }

            // update the number of players
            numberOfPlayers = playerList.Items.Count;

            RenumberPlayers();

            if (selectedIndex != -1 && numberOfPlayers > 0)
            {
                playerList.SelectedIndices.Clear();
                if (selectedIndex >= playerList.Items.Count)
                {
                    playerList.SelectedIndices.Add(playerList.Items.Count - 1);
                }
                else
                {
                    playerList.SelectedIndices.Add(selectedIndex);
                }
            }
            UpdatePlayerListButtons();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'Up' button is pressed, move the currently selected player up one slot in the list.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerUpButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = -1;

            foreach (int index in playerList.SelectedIndices)
            {
                selectedIndex = index;
                // there will be only one, this is the best way I can figure to find it.
                if (index <= 0)
                {
                    Report.Error("NewGameWizard: PlayerUpButton_Click() - Indexing error in player list.");
                    return;
                }
                ListViewItem player;
                {
                    player = playerList.Items[index].Clone() as ListViewItem;
                    playerList.Items[index] = playerList.Items[index - 1].Clone() as ListViewItem;
                    playerList.Items[index - 1] = player;
                }

            }
            if (selectedIndex != -1)
            {
                playerList.SelectedIndices.Add(selectedIndex - 1);
            }
            RenumberPlayers();

        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'Down' button is pressed, move the currently selected player down in the list.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerDownButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = -1;
            foreach (int index in playerList.SelectedIndices)
            {
                selectedIndex = index;
                // there will be only one, this is the best way I can figure to find it.
                if (index == numberOfPlayers - 1)
                {
                    playerDownButton.Enabled = false;
                    return;
                }
                ListViewItem player;
                {
                    player = playerList.Items[index].Clone() as ListViewItem;
                    playerList.Items[index] = playerList.Items[index + 1].Clone() as ListViewItem;
                    playerList.Items[index + 1] = player;
                }

            }

            if (selectedIndex != -1)
            {
                playerList.SelectedIndices.Add(selectedIndex + 1);
            }
            RenumberPlayers();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'Browse' button beside the race name is pressed, open a file browser to search for additional races.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RaceBrowseButton_Click(object sender, EventArgs e)
        {
            // browse for a race
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Select a Race";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Race race = new Race(fd.FileName);
                    KnownRaces[race.Name] = race;
                    raceSelectionBox.Items.Add(race.Name);
                    raceSelectionBox.SelectedIndex = raceSelectionBox.Items.Count - 1;
                }
            }
            catch
            {
                Report.Error("Error opening race file.");
            }

        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// When the 'Browse' button is pressed beside the AI/Human drop-down, open a file dialog to search for additional AIs.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AiBrowseButton_Click(object sender, EventArgs e)
        {
            // browse for an AI
            DialogResult yesno =
            MessageBox.Show("To specify an AI program you must know where to find a Nova compatable AI. Are you sure you want to specify an AI program?", "Select AI", MessageBoxButtons.YesNo);

            if (yesno == DialogResult.Yes)
            {
                try
                {
                    OpenFileDialog fd = new OpenFileDialog();
                    fd.Title = "Select an AI Program";
                    DialogResult result = fd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        aiSelectionBox.Items.Add(fd.FileName);
                        aiSelectionBox.SelectedIndex = aiSelectionBox.Items.Count - 1;
                    }
                }
                catch
                {
                    Report.Error("Error opening race file.");
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update the number of stars to be generated when changed in the <see cref="NumericUpDown"/> control.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GameSettings.Data.NumberOfStars = (int)numberOfStars.Value;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Change the race of the currently selected player to the race chosen from the drop down.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RaceSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndices.Count < 1)
            {
                return;
            }

            playerList.Items[playerList.SelectedIndices[0]].SubItems[1].Text = raceSelectionBox.SelectedItem.ToString();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Change the ai/human status of the currently selected player to the ai/human chosen from the drop down.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AiSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndices.Count < 1)
            {
                return;
            }

            playerList.Items[playerList.SelectedIndices[0]].SubItems[2].Text = aiSelectionBox.SelectedItem.ToString();
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update the New/Modify Player details based on the selected player.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void UpdatePlayerDetails()
        {
            foreach (int selection in playerList.SelectedIndices)
            {
                playerNumberLabel.Text = "Player #" + (selection + 1).ToString();

                raceSelectionBox.SelectedItem = playerList.Items[selection].SubItems[1].Text;
                aiSelectionBox.SelectedItem = playerList.Items[selection].SubItems[2].Text;
                break; // only one player can be selected.
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Enable/Disable the Up/Down/Delete player buttons depending on the selected player.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void UpdatePlayerListButtons()
        {
            numberOfPlayers = playerList.Items.Count;
            // Up button
            if (numberOfPlayers == 0 || playerList.SelectedIndices.Contains(0))
            {
                playerUpButton.Enabled = false;
            }
            else
            {
                playerUpButton.Enabled = true;
            }

            // Down button
            if (numberOfPlayers == 0 || playerList.SelectedIndices.Contains(numberOfPlayers - 1))
            {
                playerDownButton.Enabled = false;
            }
            else
            {
                playerDownButton.Enabled = true;
            }

            // delete button
            if (numberOfPlayers == 0)
            {
                playerDeleteButton.Enabled = false;
            }
            else
            {
                playerDeleteButton.Enabled = true;
            }

        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update the player numbers in the list to be in the order presented (e.g. after moving/deleting a player).
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void RenumberPlayers()
        {
            // update player numbering
            int playerNumber = 1;
            foreach (ListViewItem player in playerList.Items)
            {
                player.Text = "  " + playerNumber.ToString();
                playerNumber++;
            }
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Provide access to the list of players.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public List<PlayerSettings> Players
        {
            get
            {
                List<PlayerSettings> players = new List<PlayerSettings>();
                foreach (ListViewItem player in playerList.Items)
                {
                    PlayerSettings settings = new PlayerSettings();
                    settings.PlayerNumber = Int32.Parse(player.SubItems[0].Text);
                    settings.RaceName = player.SubItems[1].Text;
                    settings.AiProgram = player.SubItems[2].Text;

                    players.Add(settings);
                }
                return players;
            }
        }

        #endregion

        private void MapDensity_ValueChanged(object sender, EventArgs e)
        {

        }
                        
        #region Game Initialization
                        
        // ----------------------------------------------------------------------------
        /// <Summary>
        /// Process the Primary Traits for this race.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void ProcessPrimaryTraits(Race race)
        {
            // TODO (priority 4) Special Components
            // Races are granted access to components currently based on tech level and primary/secondary traits (not tested).
            // Need to grant special access in a few circumstances
            // 1. JOAT Hulls with pen scans. (either make a different hull with a built in pen scan, of the same name and layout; or modify scanning and scan display functions)
            // 2. Mystery Trader Items - probably need to implement the idea of 'hidden' technology to cover this.

            // TODO (priority 4) Starting Tech
            // Need to specify starting tech levels. These must be checked by the server/console. Started below - Dan 26 Jan 10

            // TODO (priority 4) Implement Starting Items
   
            RaceData raceData = stateData.AllRaceData[race.Name] as RaceData;
            raceData.ResearchLevels = new TechLevel(0);

            switch (race.Traits.Primary.Code)
            {
                case "HE":
                    // Start with one armed scout + 3 mini-colony ships
                    break;

                case "SS":
                    raceData.ResearchLevels[TechLevel.ResearchField.Electronics] = 5;
                    // Start with one scout + one colony ship.
                    break;

                case "WM":
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    // Start with one armed scout + one colony ship.
                    break;

                case "CA":
                    raceData.ResearchLevels[TechLevel.ResearchField.Weapons] = 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 6;
                    // Start with an orbital terraforming ship
                    break;

                case "IS":
                    // Start with one scout and one colony ship
                    break;

                case "SD":
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] = 2;
                    raceData.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 2;
                    // Start with one scout, one colony ship, Two mine layers (one standard, one speed trap)
                    break;

                case "PP":
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] = 5;
                    raceData.ResearchLevels[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] = 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Construction] = 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Electronics] = 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] = 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Weapons] = 3;
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;

                default:
                    Report.Error("NewGameWizard.cs - ProcessPrimaryTraits() - Unknown Primary Trait \"" + race.Traits.Primary.Code + "\"");
                    break;
            } // switch on PRT

#if (DEBUG)
            // Just for testing
            // TODO (priority 4) get this from a settings file, or other central location for convenience.
            raceData.ResearchLevels = new TechLevel(0);
#endif
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Read the Secondary Traits for this race.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void ProcessSecondaryTraits(Race race)
        {
            // TODO (priority 4) finish the rest of the LRTs.
            // Not all of these properties are fully implemented here, as they may require changes elsewhere in the game engine.
            // Where a trait is listed as 'TODO ??? (priority 4)' this means it first needs to be checked if it has been implemented elsewhere.
            
            RaceData raceData = stateData.AllRaceData[race.Name] as RaceData;
            
            if (race.Traits.Contains("IFE"))
            {
                // Ships burn 15% less fuel : TODO ??? (priority 4)

                // Fuel Mizer and Galaxy Scoop engines available : Implemented in component definitions.

                // propulsion tech starts one level higher
                raceData.ResearchLevels[TechLevel.ResearchField.Propulsion]++;
            }
            if (race.Traits.Contains("TT"))
            {
                // Begin the game able to adjust each environment attribute up to 3%
                // Higher levels of terraforming are available : implemented in component definitions.
                // Total Terraforming requires 30% fewer resources : implemented in component definitions.
            }
            if (race.Traits.Contains("ARM"))
            {
                // Grants access to three additional mining hulls and two new robots : implemented in component definitions.
                // Start the game with two midget miners : TODO ??? (priority 4)
            }
            if (race.Traits.Contains("ISB"))
            {
                // Two additional starbase designs (space dock & ultra station) : implemented in component definitions.
                // Starbases have built in 20% cloacking : TODO ??? (priority 4)

                // Improved Starbases gives a 20% discount to starbase hulls.
                /*
                foreach (Component component in ClientState.Data.AvailableComponents.Values)
                {
                    // TODO (priority 3) - work out why it sometimes is null.
                    if (component == null || component.Type != "Hull") continue;
                    Hull hull = component.Properties["Hull"] as Hull;
                    if (hull == null || !hull.IsStarbase) continue;

                    Resources cost = component.Cost;
                    cost *= 0.8;
                }
                */
            }

            if (race.Traits.Contains("GR"))
            {
                // 50% resources go to selected research field. 15% to each other field. 115% total. TODO ??? (priority 4)
            }
            if (race.Traits.Contains("UR"))
            {
                // Affects minerals and resources returned due to scrapping. TODO ??? (priority 4).
            }
            if (race.Traits.Contains("MA"))
            {
                // One instance of mineral alchemy costs 25 resources instead of 100. TODO ??? (priority 4)
            }
            if (race.Traits.Contains("NRSE"))
            {
                // affects which engines are available : implemented in component definitions.
            }
            if (race.Traits.Contains("OBRM"))
            {
                // affects which mining robots will be available : implemented in component definitions.
            }
            if (race.Traits.Contains("CE"))
            {
                // Engines cost 50% less TODO (priority 4)
                // Engines have a 10% chance of not engaging above warp 6 : TODO ??? (priority 4)
            }
            if (race.Traits.Contains("NAS"))
            {
                // No access to standard penetrating scanners : implemented in component definitions.
                // Ranges of conventional scanners are doubled : TODO ??? (priority 4)
            }
            if (race.Traits.Contains("LSP"))
            {
                // Starting population is 17500 instead of 25000 : TODO ??? (priority 4)
            }
            if (race.Traits.Contains("BET"))
            {
                // TODO ??? (priority 4)
                // New technologies initially cost twice as much to build. 
                // Once all tech requirements are exceeded cost is normal. 
                // Miniaturization occurs at 5% per level up to 80% (instead of 4% per level up to 75%)
            }
            if (race.Traits.Contains("RS"))
            {
                // TODO ??? (priority 4)
                // All shields are 40% stronger than the listed rating.
                // Shields regenrate at 10% of max strength each round of combat.
                // All armors are 50% of their rated strength.
            }
            if (race.Traits.Contains("ExtraTech"))
            {
                // All extra technologies start on level 3 or 4 with JOAT
                if (race.Traits.Primary.Code == "JOAT")
                {
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] += 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Construction] += 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Electronics] += 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] += 1;
                    raceData.ResearchLevels[TechLevel.ResearchField.Weapons] += 1;
                }
                else
                {
                    raceData.ResearchLevels[TechLevel.ResearchField.Propulsion] += 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Construction] += 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Electronics] += 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Energy] += 3;
                    raceData.ResearchLevels[TechLevel.ResearchField.Weapons] += 3;
                }
            }

        }
                        
        #endregion

    }

}
