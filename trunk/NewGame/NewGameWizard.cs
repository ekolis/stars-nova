#region Copyright
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This dialog will determine the objectives of the game.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
#endregion

using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

using NovaCommon;
using NovaServer;

namespace Nova.NewGame
{
   public partial class NewGameWizard : Form
   {
      ServerState stateData = ServerState.Data;
      public Hashtable KnownRaces = new Hashtable();
      int NumberOfPlayers = 0;


// ============================================================================
// Construction and initial field population.
// ============================================================================

      public NewGameWizard()
      {
          InitializeComponent();

          // Setup the list of known races.
          KnownRaces = FileSearcher.GetAvailableRaces();

          foreach (String raceName in KnownRaces.Keys)
          {

              // add known race to selectable races in race selection drop down
              raceSelectionBox.Items.Add(raceName);
              // add known race to list of players
              NumberOfPlayers++;
              ListViewItem player = new ListViewItem("  " + NumberOfPlayers.ToString());
              player.SubItems.Add(raceName);
              player.SubItems.Add("Human");
              // playerList.Items.Add(raceName);
              playerList.Items.Add(player);

          }
          if (NumberOfPlayers > 0)
          {
              raceSelectionBox.SelectedIndex = 0;
          }

          // Setup initial button states
          playerUpButton.Enabled = false;
          playerDownButton.Enabled = false;
          playerDeleteButton.Enabled = false;



      }

        #region Methods
        //-------------------------------------------------------------------
        /// <summary>
        /// Occurs when the OK button is clicked.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="eventArgs">
        /// A <see cref="EventArgs"/> that contains the event data.
        /// </param>
        //-------------------------------------------------------------------
        private void okButton_Click( object sender, EventArgs e )
        {
            GameSettings.Data.PlanetsOwned = PlanetsOwned.Value;
            GameSettings.Data.TechLevels = TechLevels.Value;
            GameSettings.Data.NumberOfFields = NumberOfFields.Value;
            GameSettings.Data.TotalScore = TotalScore.Value;
            GameSettings.Data.ProductionCapacity = ProductionCapacity.Value;
            GameSettings.Data.CapitalShips = CapitalShips.Value;
            GameSettings.Data.HighestScore = HighestScore.Value;
            GameSettings.Data.TargetsToMeet = Int32.Parse( TargetsToMeet.Text, System.Globalization.CultureInfo.InvariantCulture );
            GameSettings.Data.MinimumGameTime = Int32.Parse( MinimumGameTime.Text, System.Globalization.CultureInfo.InvariantCulture );
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Occurs when the Tutorial button is clicked.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="eventArgs">
        /// A <see cref="EventArgs"/> that contains the event data.
        /// </param>
        //-------------------------------------------------------------------
        private void TutorialButton_Click( object sender, EventArgs eventArgs )
        {
            //
            //  TODO: Load or create the tutorial client data.
            //
        }
        #endregion

      private void mapHeight_ValueChanged(object sender, EventArgs e)
      {
          GameSettings.Data.MapHeight = (int) mapHeight.Value;
      }

      private void mapWidth_ValueChanged(object sender, EventArgs e)
      {
          GameSettings.Data.MapWidth = (int)mapWidth.Value;
      }

      private void addPlayerButton_Click(object sender, EventArgs e)
      {
          // Add player (with a dummy number)
          ListViewItem player = new ListViewItem("  ##");
          player.SubItems.Add(raceSelectionBox.SelectedItem.ToString());
          player.SubItems.Add(aiSelectionBox.SelectedItem.ToString());
          playerList.Items.Add(player);

          RenumberPlayers();

          playerList.SelectedIndices.Clear();
          playerList.SelectedIndices.Add(playerList.Items.Count - 1);
          NumberOfPlayers = playerList.Items.Count;

      }

      private void newRaceButton_Click(object sender, EventArgs e)
      {
          String RaceDesigner;
          RaceDesigner = FileSearcher.GetFile(Global.RaceDesignerKey, false, Global.RaceDesignerPath_Development, Global.RaceDesignerPath_Deployed, "RaceDesigner.exe", true);
          try
          {
              Process.Start(RaceDesigner);
          }
          catch
          {
              Report.Error("Failed to launch Nova Race Designer.");
          }
      }

      private void playerList_SelectedIndexChanged(object sender, EventArgs e)
      {

          UpdatePlayerListButtons();
      }

      private void UpdatePlayerListButtons()
      {
          NumberOfPlayers = playerList.Items.Count;
          // Up button
          if (playerList.SelectedIndices.Contains(0) || NumberOfPlayers == 0)
              playerUpButton.Enabled = false;
          else
              playerUpButton.Enabled = true;

          // Down button
          if (playerList.SelectedIndices.Contains(NumberOfPlayers - 1) || NumberOfPlayers == 0)
              playerDownButton.Enabled = false;
          else
              playerDownButton.Enabled = true;

          // delete button
          if (NumberOfPlayers == 0)
              playerDeleteButton.Enabled = false;
          else
              playerDeleteButton.Enabled = true;

      }

      private void playerDeleteButton_Click(object sender, EventArgs e)
      {
          int selectedIndex = -1;
          foreach (int index in playerList.SelectedIndices)
          {
              selectedIndex = index;
              playerList.Items.RemoveAt(index);

          }

          // update the number of players
          NumberOfPlayers = playerList.Items.Count;

          RenumberPlayers();

          if (selectedIndex != -1 && NumberOfPlayers > 0)
          {
              playerList.SelectedIndices.Clear();
              playerList.SelectedIndices.Add(selectedIndex);
          }
          UpdatePlayerListButtons();
      }

      private void playerUpButton_Click(object sender, EventArgs e)
      {
          int selectedIndex = -1;

          foreach (int index in playerList.SelectedIndices)
          {
              selectedIndex = index;
              // there will be only one, this is the best way I can figure to find it.
              if (index <=0 ) 
              {
                  Report.Error("NewGameWizard: playerUpButton_Click() - Indexing error in player list.");
                  return;
              }
              ListViewItem player;
              {
                  player = playerList.Items[index].Clone() as ListViewItem;
                  playerList.Items[index] = playerList.Items[index - 1].Clone() as ListViewItem;
                  playerList.Items[index - 1] = player;
              }
              
          }
          if (selectedIndex != -1) playerList.SelectedIndices.Add(selectedIndex-1);
          RenumberPlayers();

      }

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

      private void playerDownButton_Click(object sender, EventArgs e)
      {
          int selectedIndex = -1;
          foreach (int index in playerList.SelectedIndices)
          {
              selectedIndex = index;
              // there will be only one, this is the best way I can figure to find it.
              if (index == NumberOfPlayers-1)
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

      private void raceBrowseButton_Click(object sender, EventArgs e)
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

      private void aiBrowseButton_Click(object sender, EventArgs e)
      {
          // browse for an AI
          DialogResult yesno = 
          MessageBox.Show("To specify an AI program you must know where to find a Nova compatable AI. Are you sure you want to specify an AI program?", "Select AI", MessageBoxButtons.YesNo); 
          
          if (yesno == DialogResult.Yes)
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



   }

}
