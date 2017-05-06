#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.WinForms.Console
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    
    using Nova.Common;
    using Nova.Server;

    /// <Summary>
    /// The Nova console is the interface for the server functionality
    /// </Summary>
    public partial class NovaConsole : Form
    {
        private ServerData serverState;       

        /// <Summary>
        /// Initializes a new instance of the NovaConsoleMain class.
        /// </Summary>
        public NovaConsole()
        {
            InitializeComponent();
            // Fresh state; load game in progress later.            
            this.serverState = new ServerData();
        }

        /// <Summary>
        /// Clean up any resources being used.
        /// </Summary>
        /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <Summary>
        /// Populate the nova console form when first loaded.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnFirstShow(object sender, EventArgs e)
        {
            /// This function is called when the Nova Console form is loaded. 
            /// Normally this is to manage a game in progress or one newly created,
            /// by the NewGame wizard. However it may be launched directly with either
            /// no game in progress or the in progress game unknown.
            /// 
            /// First we try to open a current game based on the config file settings:
            serverState.StatePathName = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
            serverState.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            folderPath.Text = serverState.GameFolder;            

            if (File.Exists(serverState.StatePathName))
            { 
                // We have a known game in progress. Load it:
                serverState.Restore();
                serverState.GameInProgress = true;
                
                // load the game settings also
                GameSettings.Restore();
                generateTurnMenuItem.Enabled = true;
                turnYearLabel.Text = serverState.TurnYear.ToString();
                
                OrderReader orderReader = new OrderReader(serverState);
                orderReader.ReadOrders();
                
                SetPlayerList();
                Invalidate();
            }
            else
            {
                /// If nothing is defined then the only option is to enable the 
                /// "New Game" button. 
                serverState.GameInProgress = false;
                newGameMenuItem.Enabled = true;
                turnYearLabel.Text = "Create a new game.";
            }
        }

        /// <Summary>
        /// Save console persistent data on exit.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ConsoleFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serverState.GameInProgress)
                {
                    serverState.Save();
                }
            }
            catch
            {
                Report.Error("Error saving Nova Console data.");
            }
        }

        /// <Summary>
        /// Display the About box dialog
        /// <Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        /// <Summary>
        /// Select a new Game Folder
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void SelectNewFolder(object sender, EventArgs e)
        {
            serverState.Clear();
            OnFirstShow(null, null);
        }

        /// <Summary>
        /// This function is called when the New Game menu Item is selected.
        /// Launches the New Game wizard.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NewGameMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RunNewGameWizard();
            }
            catch
            {
                Report.Error("Failed to launch New Game Wizard.");
            }
        }

        /// <Summary>
        /// This function is called when the Exit button is pressed.
        /// </Summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <Summary>
        /// Refresh the turn in fields...
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            // FIXME (priority 4) - Delete this once proven to work without it (release testing). - Dan 09 Jul 11
            // Commented this out as console is searching for files each time the timer goes off. 
            // ServerState.Data.AllRaces = FileSearcher.GetAvailableRaces();
   
            OrderReader orderReader = new OrderReader(serverState);
            orderReader.ReadOrders();

            if (SetPlayerList())
            {
                generateTurnMenuItem.Enabled = true;
            }
        }

        /// <Summary>
        /// This function is called when the Generate Turn button is pressed. 
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void GenerateTurnMenuItem_Click(object sender, EventArgs e)
        {
            GenerateTurn();
        }

        /// <Summary>
        /// This function is called when the Force Generate Turn button is pressed. 
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ForceMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "One or more races have not yet turned in. Are you sure you want to generate the next turn?",
                "Nova - Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            GenerateTurn();
        }

        /// <Summary>
        /// Launch the Nova GUI to play a turn for a give player.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlayerList_DoubleClick(object sender, EventArgs e)
        {
            // On occasion this fires but SelectedItems is empty. Ignore and let the user re-click. See issue #2974019.
            if (playerList.SelectedItems.Count == 0)
            {
                return;
            }

            // Find what was clicked
            string raceName = playerList.SelectedItems[0].SubItems[1].Text;

            try
            {
                // Launch the nova GUI
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.RaceName, raceName);
                args.Add(CommandArguments.Option.Turn, serverState.TurnYear);
                args.Add(CommandArguments.Option.IntelFileName, Path.Combine(serverState.GameFolder, raceName + Global.IntelExtension));
                
                Nova.WinForms.Gui.NovaGUI gui = new Nova.WinForms.Gui.NovaGUI(args.ToArray());
                gui.Show();
            }
            catch (Exception ex)
            {
                Report.Error("NovaConsole.cs : PlayerList_DoubleClick() - Failed to launch GUI. Details:" +
                             Environment.NewLine + ex.ToString());
            }
        }

        /// <Summary>
        /// Process the selection of the Run AI checkbox.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RunAiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Commented out as it is controlled by a timmer for now.
            // RunAI();
        }

        /// <Summary>
        /// Process the timer event, used to determine how often the console,
        /// checks if the AI needs to run or a new turn can be generated.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ConsoleTimer_Tick(object sender, EventArgs e)
        {
            // Don't want multiple ticks
            consoleTimer.Enabled = false;

            try
            {
                // TODO (priority 4) - reading all the .orders files is overkill. Only really want to read orders for races that aren't turned in yet, and only if they have changed.
                OrderReader orderReader = new OrderReader(serverState);
                orderReader.ReadOrders();

                if (SetPlayerList())
                {
                    generateTurnMenuItem.Enabled = true;
                    if (autoGenerateCheckBox.Checked)
                    {
                        // TODO (Priority 4) Grey out the players or something so it is clearer you can not click to play at the moment.
                        GenerateTurn();
                    }
                }
                else
                {
                    if (runAiCheckBox.Checked)
                    {
                        // Look for AIs to run, and launch their processes. Do this in a seperate thread, so it does not stop the human player from opening a turn.
                        // Dan 7 May 17 - not sure if putting this in a thread improved performance or not. Doesn't seem to hurt so I am leaving it in.
                        ThreadPool.QueueUserWorkItem(new WaitCallback(RunAI));
                    }
                }
            }
            finally
            {
                consoleTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Open an existing game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OpenGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // have the user identify the game to open
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Open Game";
                fd.FileName = "NovaServer" + Global.ServerStateExtension;
                DialogResult result = fd.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                serverState.StatePathName = fd.FileName;
            }
            catch
            {
                Report.FatalError("Unable to open a game.");
            }

            // TODO (priority 4) - This code is a repeat of what we do when the console is normally opened. Consider consolodating these sections.
            serverState.GameFolder = System.IO.Path.GetDirectoryName(serverState.StatePathName);
            folderPath.Text = serverState.GameFolder;            

            if (File.Exists(serverState.StatePathName))
            {
                // We have a known game in progress. Load it:
                serverState.Restore();
                serverState.GameInProgress = true;
                turnYearLabel.Text = serverState.TurnYear.ToString();
                // load the game settings also
                GameSettings.Restore();
                generateTurnMenuItem.Enabled = true;
                turnYearLabel.Text = serverState.TurnYear.ToString();
                OrderReader orderReader = new OrderReader(this.serverState);
                orderReader.ReadOrders();
                SetPlayerList();
                Invalidate();
            }
            else
            {
                /// If nothing is defined then the only option is to enable the 
                /// "New Game" button. 
                serverState.GameInProgress = false;
                this.newGameMenuItem.Enabled = true;
                turnYearLabel.Text = "Create a new game.";
            }
        }

        /// <summary>
        ///  FIXME (priority 4) - what is this for?.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlayerList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (playerList.Items.Count > 0)
                {
                    ListViewItem item;
                    if (playerList.FocusedItem == null)
                    {
                        item = playerList.GetItemAt(e.X, e.Y);
                    }
                    else
                    {
                        item = playerList.FocusedItem;
                    }
                    playerMenu.Items.Clear();
                    playerMenu.Items.Add(item.Text);
                    playerMenu.Items.Add("-");
                    playerMenu.Show(playerList, new System.Drawing.Point(e.X, e.Y));
                }
            }
        }
        

        /// <Summary>
        /// Set the player list "Turn In" field.
        /// </Summary>
        /// <returns>Returns true if all players are turned in.</returns>
        private bool SetPlayerList()
        {
            bool allTurnedIn = true;
            playerList.Items.Clear();

            if (serverState.GameInProgress)
            {
                turnYearLabel.Text = serverState.TurnYear.ToString();
            }

            foreach (PlayerSettings settings in serverState.AllPlayers)
            {
                ListViewItem listItem = new ListViewItem();

                // show if it is an AI player
                listItem.Text = settings.AiProgram;

                // Show the race name
                listItem.SubItems.Add(settings.RaceName);

                // Show what turn the race/player last submitted, 
                // and color code to highlight which races we are waiting on (if we wait).

                EmpireData empireData;
                serverState.AllEmpires.TryGetValue(settings.PlayerNumber, out empireData);

                ListViewItem.ListViewSubItem yearItem = new ListViewItem.ListViewSubItem();
                if (empireData == null ||
                    (empireData.TurnYear == Global.StartingYear && !empireData.TurnSubmitted) ||
                    empireData.LastTurnSubmitted == 0)
                {
                    yearItem.Text = "No Orders";
                }
                else 
                {
                    yearItem.Text = empireData.LastTurnSubmitted.ToString();
                }


                if (empireData == null || empireData.TurnYear != serverState.TurnYear || !empireData.TurnSubmitted)
                {
                    // FIXME (priority 3) - Display the turn year color coded - red for waiting, green for turned in.
                    yearItem.ForeColor = System.Drawing.Color.Red;

                    allTurnedIn = false; // don't allow the turn to be generated (unless forced)
                }
                else
                {
                    yearItem.ForeColor = System.Drawing.Color.Green;
                }
                listItem.SubItems.Add(yearItem);

                playerList.Items.Add(listItem);
                // PlayerList.Invalidate(); // Tried this in an attempt to get the colors to show. Dan - 6 Feb 10
            }

            return allTurnedIn;
        }

        /// <Summary>
        /// Add a new message to the scrolling status window on the console.
        /// </Summary>
        /// <param name="message">The message to be displayed.</param>
        private void AddStatusMessage(string message)
        {
            statusBox.Text += Environment.NewLine + message;
            statusBox.SelectionStart = statusBox.Text.Length;
            statusBox.ScrollToCaret();
            statusBox.Refresh();
        }

        /// <Summary>
        /// Generate a new turn, if able, and update the Nova Console UI
        /// </Summary>
        private void GenerateTurn()
        {
            /* FIXME (priority 5) This gives a flase negative indication, i.e. GameInProgress is false even when a game is in progress.
            if (ServerState.Data.GameInProgress == false)
            {
                Report.Error("There is no game in progress. Open a current game or create a new game.");
                return;
            }
             */

            if (!File.Exists(serverState.StatePathName))
            {
                Report.Error("There is no game open. Open a current game or create a new game.");
                return;
            }
            TurnGenerator turn = new TurnGenerator(serverState);
            turn.Generate();

            newGameMenuItem.Enabled = false;
            generateTurnMenuItem.Enabled = false;

            AddStatusMessage("New turn generated for year " + serverState.TurnYear);
            turnYearLabel.Text = serverState.TurnYear.ToString();

            serverState.Save();

            SetPlayerList();
        }

        /// <Summary>
        /// Launch the AI program to take any turns for AI players.
        /// 
        /// </Summary>
        /// <param name="status">Required as this is called in as a worker thread, but unused.</param>
        private void RunAI(object status)
        {
            if (runAiCheckBox.Checked)
            {
                // MessageBox.Show("Run AI");

                foreach (PlayerSettings settings in serverState.AllPlayers)
                {
                    if (settings.AiProgram == "Human")
                    {
                        continue;
                    }

                    EmpireData empireData;
                    serverState.AllEmpires.TryGetValue(settings.PlayerNumber, out empireData);
                    if (empireData == null || empireData.TurnYear != serverState.TurnYear || !empireData.TurnSubmitted)
                    {
                        // TODO: Add support for running custom AIs based on settings.AiProgram.
                        CommandArguments args = new CommandArguments();
                        args.Add(CommandArguments.Option.AiSwitch);
                        args.Add(CommandArguments.Option.RaceName, settings.RaceName);
                        args.Add(CommandArguments.Option.Turn, serverState.TurnYear);
                        args.Add(CommandArguments.Option.IntelFileName, Path.Combine(serverState.GameFolder, settings.RaceName + Global.IntelExtension));
                        try
                        {
                            Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                        }
                        catch
                        {
                            Report.Error("Failed to launch AI.");
                        }
                        // FIXME (priority 3) - can not process any more than one AI at a time. 
                        // It will crash if multiple AI's try to access the same files at the same time. 
                        return;
                    }
                }
            }
        }
        
        private void NewGameWizardClosing(object sender, FormClosingEventArgs e)
        {
            switch ((sender as Form).DialogResult)
            {
            case DialogResult.OK:
                Close();
                break;
            case DialogResult.Retry:
                RunNewGameWizard();
                break;
            }    
        }
        
        private void RunNewGameWizard()
        {
            NewGameWizard wizard = new NewGameWizard();
            wizard.FormClosing += NewGameWizardClosing;
            wizard.Show();
        }
    }
}