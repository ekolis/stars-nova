#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project.
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

namespace Nova.WinForms
{
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

    /// <Summary>
    /// The Stars! Nova - New Game Wizard <see cref="Form"/>.
    /// This dialog will determine the objectives and settings of the game.
    /// </Summary>
    public partial class NewGameWizard : Form
    {
        private int numberOfPlayers;
        private ServerState stateData;

        public Dictionary<string, Race> KnownRaces = new Dictionary<string, Race>();
        private RaceNameGenerator raceNameGenerator = new RaceNameGenerator();

        /// <Summary>
        /// Initializes a new instance of the NewGameWizard class.
        /// </Summary>
        public NewGameWizard()
        {
            InitializeComponent();
            
            // New clean server state
            this.stateData = new ServerState();

            // Setup the list of known races.
            FileSearcher.GetAvailableRaces().ForEach(race => AddKnownRace(race));

            foreach (string raceName in KnownRaces.Keys)
            {
                // add known race to selectable races in race selection drop down
                raceSelectionBox.Items.Add(raceName);
            }

            Random rand = new Random();
            List<String> racenames = new List<string>(KnownRaces.Keys);
            for( int i = 0; i < 2; ++i ) // Add 2 players to a new game
            {
                // add known race to list of players
                string racename = racenames[rand.Next(racenames.Count)];
                racenames.Remove(racename);
                this.numberOfPlayers++;
                ListViewItem player = new ListViewItem("  " + this.numberOfPlayers.ToString());
                player.SubItems.Add(racename);
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

        private string AddKnownRace(Race race)
        {
            string name = race.Name;
            // De-dupe race names here. Not the ideal solution but for now it'll have to do.
            race.Name = raceNameGenerator.GenerateNextName(race.Name);

            if (race.Name != name)
            {
                race.PluralName = race.Name + "s";                
            }                            
            KnownRaces[race.Name] = race;
            return race.Name;
        }

        /// <Summary>
        /// run the wizard
        /// </Summary>
        private bool CreateGame()
        {
            // New game dialog was OK, create a game
            // Get a location to save the game:
            FolderBrowserDialog gameFolderBrowser = new FolderBrowserDialog();
            gameFolderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            gameFolderBrowser.SelectedPath = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            gameFolderBrowser.Description = "Choose New Game Folder";
            DialogResult gameFolderBrowserResult = gameFolderBrowser.ShowDialog(this);

            // Check for cancel being pressed (in the new game save file dialog).
            if (gameFolderBrowserResult != DialogResult.OK)
            {
                return false;
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
            stateData.AllPlayers = Players;

            // Assemble empires.
            foreach (PlayerSettings settings in stateData.AllPlayers)
            {
                if (stateData.AllRaces.ContainsKey(settings.RaceName))
                {
                    // Copy the race to a new key and change the setting to de-duplicate the race.
                    // This may mean a 2nd copy of the race in the data, but it's not much data and a copy of it doesn't really matter
                    settings.RaceName = AddKnownRace(KnownRaces[settings.RaceName].Clone());
                }
                stateData.AllRaces.Add(settings.RaceName, KnownRaces[settings.RaceName]);

                // Initialize clean data for them. 
                EmpireData empireData = new EmpireData();
                empireData.Id = settings.PlayerNumber;
                empireData.Race = stateData.AllRaces[settings.RaceName];

                stateData.AllEmpires[empireData.Id] = empireData;

                // Add initial state to the intel files.
                ProcessPrimaryTraits(empireData);
                ProcessSecondaryTraits(empireData);
            }

            // Create initial relations.
            foreach (EmpireData wolf in stateData.AllEmpires.Values)
            {
                foreach (EmpireData lamb in stateData.AllEmpires.Values)
                {
                    if (wolf.Id != lamb.Id)
                    {
                        wolf.EmpireReports.Add(lamb.Id, new EmpireIntel(lamb));
                        wolf.EmpireReports[lamb.Id].Relation = PlayerRelation.Enemy;
                    }
                }

            }

            StarMapInitialiser starMapInitialiser = new StarMapInitialiser(stateData);
            starMapInitialiser.GenerateStars();

            starMapInitialiser.InitialisePlayerData();

            try
            {
                TurnGenerator firstTurn = new TurnGenerator(stateData);
                firstTurn.AssembleEmpireData();
                Scores scores = new Scores(stateData);
                IntelWriter intelWriter = new IntelWriter(stateData, scores);
                intelWriter.WriteIntel();
                stateData.Save();
                GameSettings.Save();
            }
            catch (Exception e)
            {
                Report.Error("Creation of new game failed. Details:" + e.Message + Environment.NewLine + e.ToString());
                return false;
            }

            // start the server
            try
            {
                NovaConsoleMain novaConsole = new NovaConsoleMain();
                novaConsole.Show();
                return true;
            }
            catch (Exception e)
            {
                Report.FatalError("Unable to launch Console. Details:" + Environment.NewLine + e.ToString());
                return false;
            }
        }

        #region Event Methods

        /// <Summary>
        /// Occurs when the OK button is clicked.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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
            
            if (CreateGame() == true)
            {                
                DialogResult = DialogResult.OK;    
            }
            else
            {
                // Game creation aborted, signal that the Wizard should run again.
                DialogResult = DialogResult.Retry;
            }
            
            //this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs eventArgs)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <Summary>
        /// Occurs when the Tutorial button is clicked.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void TutorialButton_Click(object sender, EventArgs eventArgs)
        {
            Report.Information("Sorry, there is no tutorial yet.");
            // TODO (priority 7): Load or create the tutorial client data.
        }

        /// <Summary>
        /// Add a new player to the player list
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// When the 'New Race' button is pressed, launch the Race Designer.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// Update the GUI when the currently selected player changes.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayerDetails();
            UpdatePlayerListButtons();
        }

        /// <Summary>
        /// When the 'Delete' button is pressed, delete the currently selected player from the game.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// When the 'Up' button is pressed, move the currently selected player up one slot in the list.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// When the 'Down' button is pressed, move the currently selected player down in the list.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// When the 'Browse' button beside the race name is pressed, open a file browser to search for additional races.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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
                    AddKnownRace(race);
                    raceSelectionBox.Items.Add(race.Name);
                    raceSelectionBox.SelectedIndex = raceSelectionBox.Items.Count - 1;
                }
            }
            catch
            {
                Report.Error("Error opening race file.");
            }
        }

        /// <Summary>
        /// When the 'Browse' button is pressed beside the AI/Human drop-down, open a file dialog to search for additional AIs.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// Update the number of stars to be generated when changed in the <see cref="NumericUpDown"/> control.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GameSettings.Data.NumberOfStars = (int)numberOfStars.Value;
        }

        /// <Summary>
        /// Change the race of the currently selected player to the race chosen from the drop down.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RaceSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndices.Count < 1)
            {
                return;
            }

            playerList.Items[playerList.SelectedIndices[0]].SubItems[1].Text = raceSelectionBox.SelectedItem.ToString();
        }

        /// <Summary>
        /// Change the ai/human status of the currently selected player to the ai/human chosen from the drop down.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <Summary>
        /// Update the New/Modify Player details based on the selected player.
        /// </Summary>
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

        /// <Summary>
        /// Enable/Disable the Up/Down/Delete player buttons depending on the selected player.
        /// </Summary>
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

        /// <Summary>
        /// Update the player numbers in the list to be in the order presented (e.g. after moving/deleting a player).
        /// </Summary>
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

        /// <Summary>
        /// Provide access to the list of players.
        /// </Summary>
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
                        
        /// <Summary>
        /// Process the Primary Traits for this race.
        /// </Summary>
        private void ProcessPrimaryTraits(EmpireData empire)
        {
            // TODO (priority 4) Special Components
            // Races are granted access to components currently based on tech level and primary/secondary traits (not tested).
            // Need to grant special access in a few circumstances
            // 1. JOAT Hulls with pen scans. (either make a different hull with a built in pen scan, of the same name and layout; or modify scanning and scan display functions)
            // 2. Mystery Trader Items - probably need to implement the idea of 'hidden' technology to cover this.

            // TODO (priority 4) Starting Tech
            // Need to specify starting tech levels. These must be checked by the server/console. Started below - Dan 26 Jan 10

            // TODO (priority 4) Implement Starting Items
   
            empire.ResearchLevels = new TechLevel(0);

            switch (empire.Race.Traits.Primary.Code)
            {
                case "HE":
                    // Start with one armed scout + 3 mini-colony ships
                    break;

                case "SS":
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] = 5;
                    // Start with one scout + one colony ship.
                    break;

                case "WM":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    // Start with one armed scout + one colony ship.
                    break;

                case "CA":
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 6;
                    // Start with an orbital terraforming ship
                    break;

                case "IS":
                    // Start with one scout and one colony ship
                    break;

                case "SD":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 2;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 2;
                    // Start with one scout, one colony ship, Two mine layers (one standard, one speed trap)
                    break;

                case "PP":
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 5;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] = 3;
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;

                default:
                    Report.Error("NewGameWizard.cs - ProcessPrimaryTraits() - Unknown Primary Trait \"" + empire.Race.Traits.Primary.Code + "\"");
                    break;
            } // switch on PRT
        }
        
        /// <Summary>
        /// Read the Secondary Traits for this race.
        /// </Summary>
        private void ProcessSecondaryTraits(EmpireData empire)
        {
            // TODO (priority 4) finish the rest of the LRTs.
            // Not all of these properties are fully implemented here, as they may require changes elsewhere in the game engine.
            // Where a trait is listed as 'TODO ??? (priority 4)' this means it first needs to be checked if it has been implemented elsewhere.
                        
            if (empire.Race.Traits.Contains("IFE"))
            {
                // Ships burn 15% less fuel : TODO ??? (priority 4)

                // Fuel Mizer and Galaxy Scoop engines available : Implemented in component definitions.

                // propulsion tech starts one level higher
                empire.ResearchLevels[TechLevel.ResearchField.Propulsion]++;
            }
            if (empire.Race.Traits.Contains("TT"))
            {
                // Begin the game able to adjust each environment attribute up to 3%
                // Higher levels of terraforming are available : implemented in component definitions.
                // Total Terraforming requires 30% fewer resources : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("ARM"))
            {
                // Grants access to three additional mining hulls and two new robots : implemented in component definitions.
                // Start the game with two midget miners : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("ISB"))
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

            if (empire.Race.Traits.Contains("GR"))
            {
                // 50% resources go to selected research field. 15% to each other field. 115% total. TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("UR"))
            {
                // Affects minerals and resources returned due to scrapping. TODO ??? (priority 4).
            }
            if (empire.Race.Traits.Contains("MA"))
            {
                // One instance of mineral alchemy costs 25 resources instead of 100. TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("NRSE"))
            {
                // affects which engines are available : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("OBRM"))
            {
                // affects which mining robots will be available : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("CE"))
            {
                // Engines cost 50% less TODO (priority 4)
                // Engines have a 10% chance of not engaging above warp 6 : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("NAS"))
            {
                // No access to standard penetrating scanners : implemented in component definitions.
                // Ranges of conventional scanners are doubled : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("LSP"))
            {
                // Starting population is 17500 instead of 25000 : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("BET"))
            {
                // TODO ??? (priority 4)
                // New technologies initially cost twice as much to build. 
                // Once all tech requirements are exceeded cost is normal. 
                // Miniaturization occurs at 5% per level up to 80% (instead of 4% per level up to 75%)
            }
            if (empire.Race.Traits.Contains("RS"))
            {
                // TODO ??? (priority 4)
                // All shields are 40% stronger than the listed rating.
                // Shields regenrate at 10% of max strength each round of combat.
                // All armors are 50% of their rated strength.
            }
            if (empire.Race.Traits.Contains("ExtraTech"))
            {
                // All extra technologies start on level 3 or 4 with JOAT
                if (empire.Race.Traits.Primary.Code == "JOAT")
                {
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] += 1;
                }
                else
                {
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] += 3;
                }
            }
        }
                        
        #endregion

        private class RaceNameGenerator
        {
            /// ----------------------------------------------------------------------------
            /// <summary>
            /// The list of race names we can return. Taken from Stars!.exe. May need changing?
            /// </summary>
            /// ----------------------------------------------------------------------------
            private static readonly string[] raceNames = 
            {
                "Berserker",
                "Bulushi",
                "Golem",
                "Nulon",
                "Tritizoid",
                "Valadiac",
                "Ubert",
                "Felite",
                "Ferret",
                "House",
                "Cat",
                "Crusher",
                "Picardi",
                "Rush'n",
                "American",
                "Hawk",
                "Eagle",
                "Mensoid",
                "Loraxoid",
                "Hicardi",
                "Nairnian",
                "Cleaver",
                "Hooveron",
                "Nee",
                "Kurkonian"
            };
            
            private HashSet<string> usedNames = new HashSet<string>();
            private List<string> namePool = new List<string>();
            private Random rand = new Random();

            public RaceNameGenerator()
            {
                namePool.AddRange(raceNames);
            }
            
            public string GenerateNextName(string name)
            {
                int counter = 0;
                string origname = name;
                while (usedNames.Contains(name))
                {
                    if (namePool.Count > 1)
                    {
                        name = namePool[rand.Next(namePool.Count)];
                        namePool.Remove(name);
                    }
                    else
                    {
                        // none left. eek! - fall back to just adding a number
                        name = origname + counter++;
                    }
                }                
                usedNames.Add(name);
                return name;
            }

        }

    }

}
