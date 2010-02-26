// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is the main entry point for the Windows form for the Nova Console.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

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

// ============================================================================
// Nova console application class.
// ============================================================================

   public class NovaConsoleMain : System.Windows.Forms.Form
   {
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


// ============================================================================
// Nova Console construction (and any dynamic initialisation required).
// ============================================================================

      public NovaConsoleMain()
      {
         InitializeComponent();
      }


// ============================================================================
// Clean up any resources being used.
// ============================================================================

      protected override void Dispose(bool disposing)
      {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

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



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            // ensure registry keys are initialised
            FileSearcher.SetKeys();
            Application.Run(new NovaConsoleMain());
        }


// ============================================================================
// This function is called when the Nova Console form is loaded. First, we see
// if a game folder has been defined (a registry key is set if it has). If not,
// then the user is invited to select one.
//
// If the console state data file exists, then we load the data from it. If
// not, then everything has their default value (mostly nothing). If nothing is
// defined then the only option is to read the player details and enable the
// "New Game" button. Otherwise, we load any player turn files present and see
// if they are ready for the next turn.
// ============================================================================

      private void OnFirstShow(object sender, EventArgs e)
      {
          ServerState stateData = ServerState.Data;

          // Try to open a current game, if one exists
          // We will only check the registry key, otherwise let the user chose new/open from the console menu.
          stateData.StatePathName = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
          stateData.GameFolder = FileSearcher.GetFolder(Global.ServerFolderName, "Game Files");
          FolderPath.Text = stateData.GameFolder;
          ServerState.Restore();

          if (stateData.StatePathName != null && File.Exists(stateData.StatePathName))
          {
              stateData.GameInProgress = true;
              turnYearLabel.Text = stateData.TurnYear.ToString();
              // load the game settings also
              GameSettings.Restore();
          }
          
          if (stateData.AllRaces.Count == 0)
          {
              stateData.AllRaces = FileSearcher.GetAvailableRaces();
              NewGameMenuItem.Enabled = true;
              turnYearLabel.Text = "Create a new game.";
          }
          else if (stateData.GameInProgress == false)
          {
              NewGameMenuItem.Enabled = true;
              turnYearLabel.Text = "Create a new game.";
          }
          else
          {
              GenerateTurnMenuItem.Enabled = true;
              turnYearLabel.Text = stateData.TurnYear.ToString();
          }

          OrderReader.ReadOrders();
          
          SetPlayerList();
      }


      /// <summary>
      /// Set the player list "Turn In" field.
      /// </summary>
      /// <returns>true if all players are turned in</returns>
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


// ============================================================================
// Save console persistent data on exit.
// ============================================================================

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


// ============================================================================
// Display the About box dialog
// ============================================================================

      private void OnAboutClick(object sender, EventArgs e)
      {
         AboutBox aboutBox = new AboutBox();
         aboutBox.ShowDialog();
         aboutBox.Dispose();
      }

        //-------------------------------------------------------------------
        /// <summary>
        /// Select a new Game Folder
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="eventArgs">
        /// A <see cref="EventArgs"/> that contains the event data.
        /// </param>
        //-------------------------------------------------------------------
        private void SelectNewFolder(object sender, EventArgs eventArgs )
        {
            using( RegistryKey regKey = Registry.CurrentUser.CreateSubKey( Global.RootRegistryKey ) )
            {
                regKey.SetValue( Global.ServerFolderKey, "" );
            }

            ServerState.Clear();
            this.OnFirstShow(null, null);
        }

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

      // ============================================================================
      // This function is called when the Exit button is pressed.
      // ============================================================================
      private void ExitMenuItem_Click(object sender, EventArgs e)
      {
          Close();
      }

      // ============================================================================
      // Refresh the turn in fields...
      // ============================================================================
      private void RefreshMenuItem_Click(object sender, EventArgs e)
      {
          ServerState.Data.AllRaces = FileSearcher.GetAvailableRaces();
          
          OrderReader.ReadOrders();

          if (SetPlayerList())
              GenerateTurnMenuItem.Enabled = true;
      }

      // ============================================================================
      // This function is called when the Generate IntelWriter button is pressed.
      // ============================================================================
      private void GenerateTurnMenuItem_Click(object sender, EventArgs e)
      {
          GenerateTurn();
      }

      private void ForceMenuItem_Click(object sender, EventArgs e)
      {
          GenerateTurn();
      }

      private void AddStatusMessage(String message)
      {
          StatusBox.Text += Environment.NewLine + message;
          StatusBox.SelectionStart = StatusBox.Text.Length;
          StatusBox.ScrollToCaret();
          StatusBox.Refresh();
      }

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

          SetPlayerList();
      }

      private void PlayerList_DoubleClick(object sender, EventArgs e)
      {
          String raceName = PlayerList.SelectedItems[0].SubItems[1].Text;
          String NovaGuiApp;
          NovaGuiApp = FileSearcher.GetFile(Global.NovaGuiKey, false, Global.NovaGuiPath_Development, Global.NovaGuiPath_Deployed, "Nova GUI.exe", true);
          try
          {
              CommandArguments args = new CommandArguments();
              args.Add(CommandArguments.Option.RaceName, raceName);
              args.Add(CommandArguments.Option.Turn, ServerState.Data.TurnYear + 1);
              Process.Start(NovaGuiApp, args.ToString());
          }
          catch
          {
              Report.Error("NovaConsole.cs : PlayerList_DoubleClick() - Failed to launch \"Nova GUI.exe\".");
          }
      }

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

                       if (settings.AiProgram == "Default AI")
                       {
                           // run the default AI
                           String NovaAI;
                           NovaAI = FileSearcher.GetFile(Global.NovaAiKey, false, Global.NovaAiPath_Development, Global.NovaAiPath_Deployed, "Nova_AI.exe", true);
                           CommandArguments args = new CommandArguments();
                           args.Add(CommandArguments.Option.RaceName, settings.RaceName);
                           args.Add(CommandArguments.Option.Turn, ServerState.Data.TurnYear);
                           try
                           {
                               Process.Start(NovaAI, args.ToString());

                           }
                           catch
                           {
                               Report.Error("Failed to launch Nova AI.");
                           }
                           
                       }
                       else
                       {
                           // run a custom AI. The AI name is the AI program.
                           // run the default AI
                           CommandArguments args = new CommandArguments();
                           args.Add(CommandArguments.Option.RaceName, settings.RaceName);
                           args.Add(CommandArguments.Option.Turn, ServerState.Data.TurnYear);
                           try
                           {
                               Process.Start(settings.AiProgram, args.ToString());

                               // FIXME (priority 3) - there must be a smarter way to do this than guessing how long the AI will take to run.
                               System.Threading.Thread.Sleep(3000); // prevent multiple AI's trying to access the same files at the same time, and crashing. 

                           }
                           catch
                           {
                               Report.Error("Failed to launch AI program \"" + settings.AiProgram + "\".");
                           }
                       }
                       // don't process any more than one AI at a time. It will crash if multiple AI's try to access the same files at the same time. FIXME (priority 3).
                       return;
                   }
                       
               }
           }
       }

       private void runAiCheckBox_CheckedChanged(object sender, EventArgs e)
       {
           // Commented out as it is controlled by a timmer for now.
           // RunAI();
       }

       private void consoleTimer_Tick(object sender, EventArgs e)
       {
           ServerState.Data.AllRaces = FileSearcher.GetAvailableRaces();

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
   }

}
