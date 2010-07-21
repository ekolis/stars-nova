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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Nova.Common;
using Nova.NewGame;
using Nova.Server;

namespace Nova.WinForms
{
    /// <summary>
    /// The Stars! Nova - New Game Wizard <see cref="Form"/>.
    /// </summary>
    public partial class NewGameWizard : Form
    {
        public Hashtable KnownRaces = new Hashtable();
        private int numberOfPlayers;


        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the NewGameWizard class.
        /// </summary>
        public NewGameWizard()
        {
            InitializeComponent();

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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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

                    ServerState.Data.GameFolder = gameFolderBrowser.SelectedPath;
                    // Don't set ClientFolderKey in case we want to simulate a LAN game 
                    // on one PC for testing. 
                    // Should be set when the ClientState is initialised. If that is by 
                    // launching Nova GUI from the console then the GameFolder will be 
                    // passed as the path to the .intel and the ClientFolderKey will then 
                    // be updated.

                    // Construct appropriate state and settings file names
                    ServerState.Data.StatePathName = gameFolderBrowser.SelectedPath + Path.DirectorySeparatorChar +
                                                     GameSettings.Data.GameName + Global.ServerStateExtension;
                    GameSettings.Data.SettingsPathName = gameFolderBrowser.SelectedPath + Path.DirectorySeparatorChar +
                                                         GameSettings.Data.GameName + Global.SettingsExtension;
                    conf[Global.ServerStateKey] = ServerState.Data.StatePathName;
                }
                // Copy the player & race data to the ServerState
                ServerState.Data.AllPlayers = newGameWizard.Players;
                foreach (PlayerSettings settings in ServerState.Data.AllPlayers)
                {
                    // TODO (priority 7) - need to decide how to handle two races of the same name. If they are the same, fine. If they are different, problem! Maybe the race name is a poor key???
                    // Stars! solution is to rename the race using a list of standard names. 
                    if (!ServerState.Data.AllRaces.Contains(settings.RaceName))
                        ServerState.Data.AllRaces.Add(settings.RaceName, newGameWizard.KnownRaces[settings.RaceName]);
                }

                

                StarMapInitialiser.GenerateStars();

                StarMapInitialiser.InitialisePlayerData();

                try
                {
                    IntelWriter.WriteIntel();
                    ServerState.Save();
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
                    Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.ConsoleSwitch);
                    Application.Exit();
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
        /// <summary>
        /// Occurs when the OK button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OkButton_Click(object sender, EventArgs e)
        {
            GameSettings.Data.GameName = gameName.Text;
            GameSettings.Data.PlanetsOwned = this.planetsOwned.Value;
            GameSettings.Data.TechLevels = this.techLevels.Value;
            GameSettings.Data.NumberOfFields = this.numberOfFields.Value;
            GameSettings.Data.TotalScore = this.totalScore.Value;
            GameSettings.Data.ProductionCapacity = this.productionCapacity.Value;
            GameSettings.Data.CapitalShips = this.capitalShips.Value;
            GameSettings.Data.HighestScore = this.highestScore.Value;
            GameSettings.Data.TargetsToMeet = Int32.Parse(this.targetsToMeet.Text, System.Globalization.CultureInfo.InvariantCulture);
            GameSettings.Data.MinimumGameTime = Int32.Parse(this.minimumGameTime.Text, System.Globalization.CultureInfo.InvariantCulture);

            GameSettings.Data.MapHeight = (int)mapHeight.Value;
            GameSettings.Data.MapWidth = (int)mapWidth.Value;

            GameSettings.Data.StarSeparation = (int)starSeparation.Value;
            GameSettings.Data.StarDensity = (int)starDensity.Value;
            GameSettings.Data.StarUniformity = (int)starUniformity.Value;

            GameSettings.Data.AcceleratedStart = acceleratedStart.Checked;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Occurs when the Tutorial button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void TutorialButton_Click(object sender, EventArgs eventArgs)
        {
            Report.Information("Sorry, there is no tutorial yet.");
            // TODO (priority 7): Load or create the tutorial client data.
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a new player to the player list
        /// </summary>
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
            this.numberOfPlayers = playerList.Items.Count;

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'New Race' button is pressed, launch the Race Designer.
        /// </summary>
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
        /// <summary>
        /// Update the GUI when the currently selected player changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayerDetails();
            UpdatePlayerListButtons();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Delete' button is pressed, delete the currently selected player from the game.
        /// </summary>
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
            this.numberOfPlayers = playerList.Items.Count;

            RenumberPlayers();

            if (selectedIndex != -1 && this.numberOfPlayers > 0)
            {
                playerList.SelectedIndices.Clear();
                if (selectedIndex >= playerList.Items.Count)
                    playerList.SelectedIndices.Add(playerList.Items.Count - 1);
                else
                    playerList.SelectedIndices.Add(selectedIndex);
            }
            UpdatePlayerListButtons();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Up' button is pressed, move the currently selected player up one slot in the list.
        /// </summary>
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
            if (selectedIndex != -1) playerList.SelectedIndices.Add(selectedIndex - 1);
            RenumberPlayers();

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Down' button is pressed, move the currently selected player down in the list.
        /// </summary>
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
                if (index == this.numberOfPlayers - 1)
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

            if (selectedIndex != -1) playerList.SelectedIndices.Add(selectedIndex + 1);
            RenumberPlayers();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Browse' button beside the race name is pressed, open a file browser to search for additional races.
        /// </summary>
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
        /// <summary>
        /// When the 'Browse' button is pressed beside the AI/Human drop-down, open a file dialog to search for additional AIs.
        /// </summary>
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
        /// <summary>
        /// Update the number of stars to be generated when changed in the <see cref="NumericUpDown"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GameSettings.Data.NumberOfStars = (int)numberOfStars.Value;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Change the race of the currently selected player to the race chosen from the drop down.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RaceSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndices.Count < 1) return;
            playerList.Items[playerList.SelectedIndices[0]].SubItems[1].Text = raceSelectionBox.SelectedItem.ToString();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Change the ai/human status of the currently selected player to the ai/human chosen from the drop down.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AiSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndices.Count < 1) return;
            playerList.Items[playerList.SelectedIndices[0]].SubItems[2].Text = aiSelectionBox.SelectedItem.ToString();
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the New/Modify Player details based on the selected player.
        /// </summary>
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
        /// <summary>
        /// Enable/Disable the Up/Down/Delete player buttons depending on the selected player.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void UpdatePlayerListButtons()
        {
            this.numberOfPlayers = playerList.Items.Count;
            // Up button
            if (this.numberOfPlayers == 0 || playerList.SelectedIndices.Contains(0))
                playerUpButton.Enabled = false;
            else
                playerUpButton.Enabled = true;

            // Down button
            if (this.numberOfPlayers == 0 || playerList.SelectedIndices.Contains(this.numberOfPlayers - 1))
                playerDownButton.Enabled = false;
            else
                playerDownButton.Enabled = true;

            // delete button
            if (this.numberOfPlayers == 0)
                playerDeleteButton.Enabled = false;
            else
                playerDeleteButton.Enabled = true;

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the player numbers in the list to be in the order presented (e.g. after moving/deleting a player).
        /// </summary>
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
        /// <summary>
        /// Provide access to the list of players as an <see cref="ArrayList"/> of <see cref="PlayerSettings"/>.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ArrayList Players
        {
            get
            {
                ArrayList players = new ArrayList();
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

    }

}
