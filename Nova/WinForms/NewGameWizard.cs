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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using Nova.Common;
    using Nova.Server.NewGame;
    using Nova.WinForms.Console;

    /// <Summary>
    /// The Stars! Nova - New Game Wizard <see cref="Form"/>.
    /// This dialog will determine the objectives and settings of the game.
    /// </Summary>
    public partial class NewGameWizard : Form
    {
        private int numberOfPlayers;
  
        /// <summary>
        /// List of races available to be selected by players or assigned to AIs. 
        /// </summary>
        public Dictionary<string, Race> KnownRaces = new Dictionary<string, Race>();        

        /// <Summary>
        /// Initializes a new instance of the NewGameWizard class.
        /// </Summary>
        public NewGameWizard()
        {
            InitializeComponent();            

            // Setup the list of known races.
            FileSearcher.GetAvailableRaces().ForEach(race => KnownRaces[race.Name] = race);

            foreach (string raceName in KnownRaces.Keys)
            {
                // add known race to selectable races in race selection drop down
                raceSelectionBox.Items.Add(raceName);
            }

            Random rand = new Random();            
            List<string> racenames = new List<string>(KnownRaces.Keys);

            // Add 2 players to a new game, but not more than available if the user changes the race folder
            int maxPlayers = Math.Min(2, racenames.Count);
            for (int i = 0; i < maxPlayers; ++i) 
            {
                // add known race to list of players
                string racename = racenames[rand.Next(racenames.Count)];
                racenames.Remove(racename);
                numberOfPlayers++;
                
                ListViewItem player = new ListViewItem("  " + this.numberOfPlayers.ToString());
                player.SubItems.Add(racename);
                player.SubItems.Add("Human");
                playerList.Items.Add(player);
            }
            if (numberOfPlayers > 0)
            {
                raceSelectionBox.SelectedIndex = 0;
            }

            // Setup initial button states
            UpdatePlayerDetails();
            UpdatePlayerListButtons();

            // Default the game folder.
            gameFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar + "Stars! Nova" + Path.DirectorySeparatorChar + gameName.Text;
        }

        
        /// <Summary>
        /// Create a new game state and settings.
        /// </Summary>
        private bool CreateGame()
        {
            // New game dialog was OK, create a game
            
            // Make sure the location to save the game exists:
            if (!Directory.Exists(gameFolder.Text))
            {
                Directory.CreateDirectory(gameFolder.Text);
            }
   
            try
            {
                GameInitialiser.Initialize(gameFolder.Text, Players, KnownRaces);
                GameSettings.Save();
            }
            catch (Exception e)
            {
                Report.Error("Creation of new game failed. Details:" + e.Message + Environment.NewLine + e.ToString());
                return false;
            }

            // start the server gui
            try
            {
                NovaConsole novaConsole = new NovaConsole();
                novaConsole.Show();
                return true;
            }
            catch (Exception e)
            {
                Report.FatalError("Unable to launch Console. Details:" + Environment.NewLine + e.ToString());
                return false;
            }
        }


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
            
            // this.Close();
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
                    if (KnownRaces.ContainsKey(race.Name))
                    {
                        race.Name = race.Name + " from File " + System.IO.Path.GetFileName(fd.FileName);
                    }

                    if (!KnownRaces.ContainsKey(race.Name))
                    {
                        KnownRaces.Add(race.Name, race);
                        raceSelectionBox.Items.Add(race.Name);
                        raceSelectionBox.SelectedIndex = raceSelectionBox.Items.Count - 1;
                    }
                    else
                    {
                        Report.Error("Error opening race file.");
                    }
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
                    settings.PlayerNumber = ushort.Parse(player.SubItems[0].Text);
                    settings.RaceName = player.SubItems[1].Text;
                    settings.AiProgram = player.SubItems[2].Text;

                    players.Add(settings);
                }
                return players;
            }
        }
        

        private void MapDensity_ValueChanged(object sender, EventArgs e)
        {
        }

        private void gamefolderBrowseButton_Click(object sender, EventArgs e)
        {
            // Get a location to save the game:
            FolderBrowserDialog gameFolderBrowser = new FolderBrowserDialog();
            gameFolderBrowser.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            gameFolderBrowser.SelectedPath = gameFolder.Text;
            gameFolderBrowser.Description = "Choose New Game Folder";
            DialogResult gameFolderBrowserResult = gameFolderBrowser.ShowDialog(this);

            // Check for cancel being pressed (in the new game save file dialog).
            if (gameFolderBrowserResult == DialogResult.OK)
            {
                gameFolder.Text = gameFolderBrowser.SelectedPath;
            }
        }

    }

}
