#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
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

#region Module Description
// ===========================================================================
// This is the main entry point for the Windows form for the Nova Console.
// ===========================================================================
#endregion

using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System;
using System.Diagnostics;

using NovaCommon;
using NovaServer;

namespace NovaConsole
{
    /// <summary>
    /// Nova console application class.
    /// </summary>
    public class NovaConsoleMain : System.Windows.Forms.Form
    {
        #region Windows Form Data
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private MenuStrip MainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem SelectNewFolderMenuItem;
        private Label FolderPath;
        private Label label1;
        private ListView PlayerList;
        private ColumnHeader RaceName;
        private ColumnHeader TurnIn;
        private ToolStripMenuItem NewGameMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem turnToolStripMenuItem;
        private ToolStripMenuItem GenerateTurnMenuItem;
        private ToolStripMenuItem ForceMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem RefreshMenuItem;
        private ColumnHeader AI;
        private Label turnYearLabel;
        private Label yearLabel;
        private Label GuiLaunchLabel;
        private ToolStripMenuItem openGameToolStripMenuItem;
        private TextBox StatusBox;
        private CheckBox autoGenerateCheckBox;
        private CheckBox runAiCheckBox;
        private Timer consoleTimer;
        private IContainer components;
        #endregion

        #region Construction Destruction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Nova Console construction (and any dynamic initialisation required).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public NovaConsoleMain()
        {
            InitializeComponent();
        }

    
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">???</param>
        /// ----------------------------------------------------------------------------
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

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaConsoleMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PlayerList = new System.Windows.Forms.ListView();
            this.AI = new System.Windows.Forms.ColumnHeader();
            this.RaceName = new System.Windows.Forms.ColumnHeader();
            this.TurnIn = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.runAiCheckBox = new System.Windows.Forms.CheckBox();
            this.autoGenerateCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.FolderPath = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.turnYearLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectNewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateTurnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GuiLaunchLabel = new System.Windows.Forms.Label();
            this.consoleTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.PlayerList);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 254);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Players";
            // 
            // PlayerList
            // 
            this.PlayerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AI,
            this.RaceName,
            this.TurnIn});
            this.PlayerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayerList.FullRowSelect = true;
            this.PlayerList.Location = new System.Drawing.Point(3, 16);
            this.PlayerList.MultiSelect = false;
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(444, 235);
            this.PlayerList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.PlayerList.TabIndex = 0;
            this.PlayerList.UseCompatibleStateImageBehavior = false;
            this.PlayerList.View = System.Windows.Forms.View.Details;
            this.PlayerList.DoubleClick += new System.EventHandler(this.PlayerList_DoubleClick);
            // 
            // AI
            // 
            this.AI.Text = "AI";
            this.AI.Width = 112;
            // 
            // RaceName
            // 
            this.RaceName.Text = "Race Name";
            this.RaceName.Width = 209;
            // 
            // TurnIn
            // 
            this.TurnIn.Text = "Last Turned In";
            this.TurnIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TurnIn.Width = 108;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.runAiCheckBox);
            this.groupBox2.Controls.Add(this.autoGenerateCheckBox);
            this.groupBox2.Controls.Add(this.StatusBox);
            this.groupBox2.Controls.Add(this.FolderPath);
            this.groupBox2.Controls.Add(this.yearLabel);
            this.groupBox2.Controls.Add(this.turnYearLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 167);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game Information";
            // 
            // runAiCheckBox
            // 
            this.runAiCheckBox.AutoSize = true;
            this.runAiCheckBox.Location = new System.Drawing.Point(236, 63);
            this.runAiCheckBox.Name = "runAiCheckBox";
            this.runAiCheckBox.Size = new System.Drawing.Size(59, 17);
            this.runAiCheckBox.TabIndex = 12;
            this.runAiCheckBox.Text = "Run AI";
            this.runAiCheckBox.UseVisualStyleBackColor = true;
            this.runAiCheckBox.CheckedChanged += new System.EventHandler(this.runAiCheckBox_CheckedChanged);
            // 
            // autoGenerateCheckBox
            // 
            this.autoGenerateCheckBox.AutoSize = true;
            this.autoGenerateCheckBox.Location = new System.Drawing.Point(10, 63);
            this.autoGenerateCheckBox.Name = "autoGenerateCheckBox";
            this.autoGenerateCheckBox.Size = new System.Drawing.Size(125, 17);
            this.autoGenerateCheckBox.TabIndex = 11;
            this.autoGenerateCheckBox.Text = "Auto Generate Turns";
            this.autoGenerateCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusBox
            // 
            this.StatusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBox.Location = new System.Drawing.Point(9, 86);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StatusBox.Size = new System.Drawing.Size(431, 69);
            this.StatusBox.TabIndex = 9;
            // 
            // FolderPath
            // 
            this.FolderPath.AutoSize = true;
            this.FolderPath.Location = new System.Drawing.Point(76, 19);
            this.FolderPath.Name = "FolderPath";
            this.FolderPath.Size = new System.Drawing.Size(76, 13);
            this.FolderPath.TabIndex = 6;
            this.FolderPath.Text = "None selected";
            this.FolderPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(7, 42);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(94, 13);
            this.yearLabel.TabIndex = 8;
            this.yearLabel.Text = "Now Playing Year:";
            // 
            // turnYearLabel
            // 
            this.turnYearLabel.AutoSize = true;
            this.turnYearLabel.Location = new System.Drawing.Point(107, 42);
            this.turnYearLabel.Name = "turnYearLabel";
            this.turnYearLabel.Size = new System.Drawing.Size(35, 13);
            this.turnYearLabel.TabIndex = 7;
            this.turnYearLabel.Text = "YYYY";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Game Name: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.turnToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(472, 24);
            this.MainMenu.TabIndex = 6;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewGameMenuItem,
            this.openGameToolStripMenuItem,
            this.SelectNewFolderMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // NewGameMenuItem
            // 
            this.NewGameMenuItem.Name = "NewGameMenuItem";
            this.NewGameMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewGameMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NewGameMenuItem.Text = "&New Game";
            this.NewGameMenuItem.Click += new System.EventHandler(this.NewGameMenuItem_Click);
            // 
            // openGameToolStripMenuItem
            // 
            this.openGameToolStripMenuItem.Name = "openGameToolStripMenuItem";
            this.openGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openGameToolStripMenuItem.Text = "&Open Game";
            // 
            // SelectNewFolderMenuItem
            // 
            this.SelectNewFolderMenuItem.Name = "SelectNewFolderMenuItem";
            this.SelectNewFolderMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SelectNewFolderMenuItem.Text = "&Select New Folder";
            this.SelectNewFolderMenuItem.Click += new System.EventHandler(this.SelectNewFolder);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // turnToolStripMenuItem
            // 
            this.turnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerateTurnMenuItem,
            this.ForceMenuItem,
            this.toolStripSeparator2,
            this.RefreshMenuItem});
            this.turnToolStripMenuItem.Name = "turnToolStripMenuItem";
            this.turnToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.turnToolStripMenuItem.Text = "&Turn";
            // 
            // GenerateTurnMenuItem
            // 
            this.GenerateTurnMenuItem.Name = "GenerateTurnMenuItem";
            this.GenerateTurnMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.GenerateTurnMenuItem.Size = new System.Drawing.Size(183, 22);
            this.GenerateTurnMenuItem.Text = "&Generate";
            this.GenerateTurnMenuItem.Click += new System.EventHandler(this.GenerateTurnMenuItem_Click);
            // 
            // ForceMenuItem
            // 
            this.ForceMenuItem.Name = "ForceMenuItem";
            this.ForceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.ForceMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ForceMenuItem.Text = "&Force Next Turn";
            this.ForceMenuItem.Click += new System.EventHandler(this.ForceMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // RefreshMenuItem
            // 
            this.RefreshMenuItem.Name = "RefreshMenuItem";
            this.RefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.RefreshMenuItem.Size = new System.Drawing.Size(183, 22);
            this.RefreshMenuItem.Text = "&Refresh";
            this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClick);
            // 
            // GuiLaunchLabel
            // 
            this.GuiLaunchLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GuiLaunchLabel.AutoSize = true;
            this.GuiLaunchLabel.Location = new System.Drawing.Point(154, 26);
            this.GuiLaunchLabel.Name = "GuiLaunchLabel";
            this.GuiLaunchLabel.Size = new System.Drawing.Size(136, 13);
            this.GuiLaunchLabel.TabIndex = 9;
            this.GuiLaunchLabel.Text = "Double Click to Play a Turn";
            // 
            // consoleTimer
            // 
            this.consoleTimer.Enabled = true;
            this.consoleTimer.Interval = 5000;
            this.consoleTimer.Tick += new System.EventHandler(this.consoleTimer_Tick);
            // 
            // NovaConsoleMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(472, 474);
            this.Controls.Add(this.GuiLaunchLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.Name = "NovaConsoleMain";
            this.Text = "Nova Console";
            this.Shown += new System.EventHandler(this.OnFirstShow);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleFormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Main Function

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            // ensure registry keys are initialised
            FileSearcher.SetKeys();
            Application.Run(new NovaConsoleMain());
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the nova console form when first loaded.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnFirstShow(object sender, EventArgs e)
        {
            /// This function is called when the Nova Console form is loaded. 
            /// Normally this is to manage a game in progress or one newly created,
            /// by the NewGame wizard. However it may be launched directly with either
            /// no game in progress or the in progress game unknown.
            /// 
            /// First we try to open a current game based on the registry settings:
            ServerState.Data.StatePathName = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
            ServerState.Data.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            FolderPath.Text = ServerState.Data.GameFolder;
            ServerState.Restore();

            AllComponents.Restore();

            if (ServerState.Data.StatePathName != null && File.Exists(ServerState.Data.StatePathName))
            {
                // We have a known game in progress. Load it:
                ServerState.Data.GameInProgress = true;
                turnYearLabel.Text = ServerState.Data.TurnYear.ToString();
                // load the game settings also
                GameSettings.Restore();
                GenerateTurnMenuItem.Enabled = true;
                turnYearLabel.Text = ServerState.Data.TurnYear.ToString();
                OrderReader.ReadOrders();
                SetPlayerList();
                Invalidate();
            }
            else
            {
                /// If nothing is defined then the only option is to enable the 
                /// "New Game" button. 
                ServerState.Data.GameInProgress = false;
                NewGameMenuItem.Enabled = true;
                turnYearLabel.Text = "Create a new game.";
            }

        }



        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save console persistent data on exit.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ConsoleFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ServerState.Data.GameInProgress) ServerState.Save();
            }
            catch
            {
                Report.Error("Error saving Nova Console data.");
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display the About box dialog
        /// <summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select a new Game Folder
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SelectNewFolder(object sender, EventArgs eventArgs)
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(Global.RootRegistryKey))
            {
                regKey.SetValue(Global.ServerFolderKey, "");
            }

            ServerState.Clear();
            this.OnFirstShow(null, null);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the New Game menu item is selected.
        /// Launches the New Game wizard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NewGameMenuItem_Click(object sender, EventArgs e)
        {
            String NewGameApp;
            NewGameApp = FileSearcher.GetFile(Global.NewGameKey, false, Global.NewGamePath_Development, Global.NewGamePath_Deployed, "NewGame.exe", true);
            try
            {
                Process.Start(NewGameApp);
                Application.Exit();
            }
            catch
            {
                Report.Error("Failed to launch \"NewGame.exe\".");
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the Exit button is pressed.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Refresh the turn in fields...
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            // debug - commented this out as console is searching for files each time the timer goes off
            // ServerState.Data.AllRaces = FileSearcher.GetAvailableRaces();

            OrderReader.ReadOrders();

            if (SetPlayerList())
                GenerateTurnMenuItem.Enabled = true;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the Generate Turn button is pressed. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void GenerateTurnMenuItem_Click(object sender, EventArgs e)
        {
            GenerateTurn();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the Force Generate Turn button is pressed. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ForceMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("One or more races have not yet turned in. Are you sure you want to generate the next turn?", "Nova - Warning",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            GenerateTurn();
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Launch the Nova GUI to play a turn for a give player.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PlayerList_DoubleClick(object sender, EventArgs e)
        {
            // Find what was clicked
            String raceName;
            try
            {
                raceName = PlayerList.SelectedItems[0].SubItems[1].Text;
            }
            catch
            {
                // On occasion this fires but SelectedItems is empty. Ignore and let the user re-click. See issue 2974019.
                return;
            }
            try
            {

                // Find the Nova GUI
                String NovaGuiApp;
                NovaGuiApp = FileSearcher.GetFile(Global.NovaGuiKey, false, Global.NovaGuiPath_Development, Global.NovaGuiPath_Deployed, "Nova GUI.exe", true);

                // Launch the nova GUI
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.RaceName, raceName);
                args.Add(CommandArguments.Option.Turn, ServerState.Data.TurnYear + 1);
                args.Add(CommandArguments.Option.IntelFileName, Path.Combine(ServerState.Data.GameFolder, raceName + ".intel"));
                Process.Start(NovaGuiApp, args.ToString());
            }
            catch
            {
                Report.Error("NovaConsole.cs : PlayerList_DoubleClick() - Failed to launch \"Nova GUI.exe\".");
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void runAiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Commented out as it is controlled by a timmer for now.
            // RunAI();
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the timer event, used to determine how often the console,
        /// checks if the AI needs to run or a new turn can be generated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void consoleTimer_Tick(object sender, EventArgs e)
        {
            // debug - commented this out as console is searching for files each time the timer goes off
            // ServerState.Data.AllRaces = FileSearcher.GetAvailableRaces();

            // TODO (priority 4) - reading all the .orders files is overkill. Only really want to read orders for races that aren't turned in yet, and only if they have changed.
            OrderReader.ReadOrders();

            if (SetPlayerList())
            {
                GenerateTurnMenuItem.Enabled = true;
                if (autoGenerateCheckBox.Checked)
                {
                    GenerateTurn();
                }
            }
            else
            {
                if (runAiCheckBox.Checked) RunAI();
            }
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the player list "Turn In" field.
        /// </summary>
        /// <returns>true if all players are turned in</returns>
        /// ----------------------------------------------------------------------------
        private bool SetPlayerList()
        {
            bool result = true;
            PlayerList.Items.Clear();
            ServerState stateData = ServerState.Data;
            if (stateData.GameInProgress) turnYearLabel.Text = stateData.TurnYear.ToString();

            foreach (PlayerSettings settings in ServerState.Data.AllPlayers)
            {
                ListViewItem listItem = new ListViewItem();

                // show if it is an AI player
                listItem.Text = settings.AiProgram;

                // Show the race name
                listItem.SubItems.Add(settings.RaceName);

                // Show what turn the race/player last submitted, 
                // and color code to highlight which races we are waiting on (if we wait).

                RaceData raceData = stateData.AllRaceData[settings.RaceName] as RaceData;

                ListViewItem.ListViewSubItem yearItem = new ListViewItem.ListViewSubItem();
                if (raceData == null)
                    yearItem.Text = "No Orders";
                else
                    yearItem.Text = raceData.TurnYear.ToString();

                if (raceData == null || raceData.TurnYear != stateData.TurnYear)
                {
                    // FIXME (priority 3) - Display the turn year color coded - red for waiting, green for turned in.
                    yearItem.ForeColor = System.Drawing.Color.Red;

                    result = false; // don't allow the turn to be generated (unless forced)
                }
                else
                {
                    yearItem.ForeColor = System.Drawing.Color.Green;
                }
                listItem.SubItems.Add(yearItem);

                PlayerList.Items.Add(listItem);
                // PlayerList.Invalidate(); // Tryed this in an attempt to get the colors to show. Dan - 6 Feb 10
            }

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a new message to the scrolling status window on the console.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// ----------------------------------------------------------------------------
        private void AddStatusMessage(String message)
        {
            StatusBox.Text += Environment.NewLine + message;
            StatusBox.SelectionStart = StatusBox.Text.Length;
            StatusBox.ScrollToCaret();
            StatusBox.Refresh();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate a new turn, if able, and update the Nova Console UI
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void GenerateTurn()
        {
            /* FIXME (priority 4) This gives a flase negative indication, i.e. GameInProgress is false even when a game is in progress.
            if (ServerState.Data.GameInProgress == false)
            {
                Report.Error("There is no game in progress. Open a current game or create a new game.");
                return;
            }
             */

            if (ServerState.Data.StatePathName == null || !File.Exists(ServerState.Data.StatePathName))
            {
                Report.Error("There is no game open. Open a current game or create a new game.");
                return;
            }
            ProcessTurn.Generate();

            NewGameMenuItem.Enabled = false;
            GenerateTurnMenuItem.Enabled = false;

            AddStatusMessage("New turn generated for year " + ServerState.Data.TurnYear);
            turnYearLabel.Text = ServerState.Data.TurnYear.ToString();

            ServerState.Save();

            SetPlayerList();
        }



        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Launch the AI program to take any turns for AI players.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void RunAI()
        {
            if (runAiCheckBox.Checked)
            {
                // MessageBox.Show("Run AI");

                foreach (PlayerSettings settings in ServerState.Data.AllPlayers)
                {
                    if (settings.AiProgram == "Human")
                    {
                        continue;
                    }

                    RaceData raceData = ServerState.Data.AllRaceData[settings.RaceName] as RaceData;
                    if (raceData == null || raceData.TurnYear != ServerState.Data.TurnYear)
                    {
                        String AiProgram;

                        if (settings.AiProgram == "Default AI")
                        {
                            // run the default AI
                            AiProgram = FileSearcher.GetFile(Global.NovaAiKey, false, Global.NovaAiPath_Development, Global.NovaAiPath_Deployed, "Nova_AI.exe", true);
                        }
                        else
                        {
                            // use a custom AI
                            AiProgram = settings.AiProgram;
                        }
                        CommandArguments args = new CommandArguments();
                        args.Add(CommandArguments.Option.RaceName, settings.RaceName);
                        args.Add(CommandArguments.Option.Turn, ServerState.Data.TurnYear);
                        args.Add(CommandArguments.Option.IntelFileName, Path.Combine(ServerState.Data.GameFolder, settings.RaceName + ".intel"));
                        try
                        {
                            Process.Start(AiProgram, args.ToString());
                        }
                        catch
                        {
                            Report.Error("Failed to launch Nova AI.");
                        }
                        // FIXME (priority 3) - can not process any more than one AI at a time. 
                        // It will crash if multiple AI's try to access the same files at the same time. 
                        return;
                    }

                }
            }
        }

        #endregion

    }

}
