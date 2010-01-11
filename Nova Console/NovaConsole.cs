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
using NovaCommon;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System;

namespace NovaConsole
{

// ============================================================================
// Nova console application class.
// ============================================================================

   public class NovaConsoleMain : System.Windows.Forms.Form
   {
      private GroupBox groupBox1;
      private Label StatusBox;
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
      private System.ComponentModel.Container components = null;


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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaConsoleMain));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.PlayerList = new System.Windows.Forms.ListView();
          this.RaceName = new System.Windows.Forms.ColumnHeader();
          this.TurnIn = new System.Windows.Forms.ColumnHeader();
          this.StatusBox = new System.Windows.Forms.Label();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.FolderPath = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.MainMenu = new System.Windows.Forms.MenuStrip();
          this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.NewGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          this.MainMenu.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.PlayerList);
          this.groupBox1.Location = new System.Drawing.Point(12, 99);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(308, 185);
          this.groupBox1.TabIndex = 3;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Races";
          // 
          // PlayerList
          // 
          this.PlayerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RaceName,
            this.TurnIn});
          this.PlayerList.Dock = System.Windows.Forms.DockStyle.Fill;
          this.PlayerList.Location = new System.Drawing.Point(3, 16);
          this.PlayerList.MultiSelect = false;
          this.PlayerList.Name = "PlayerList";
          this.PlayerList.Size = new System.Drawing.Size(302, 166);
          this.PlayerList.Sorting = System.Windows.Forms.SortOrder.Ascending;
          this.PlayerList.TabIndex = 0;
          this.PlayerList.UseCompatibleStateImageBehavior = false;
          this.PlayerList.View = System.Windows.Forms.View.Details;
          // 
          // RaceName
          // 
          this.RaceName.Text = "Race Name";
          this.RaceName.Width = 207;
          // 
          // TurnIn
          // 
          this.TurnIn.Text = "Turn In";
          this.TurnIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          this.TurnIn.Width = 91;
          // 
          // StatusBox
          // 
          this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.StatusBox.Location = new System.Drawing.Point(6, 16);
          this.StatusBox.Name = "StatusBox";
          this.StatusBox.Size = new System.Drawing.Size(296, 18);
          this.StatusBox.TabIndex = 4;
          this.StatusBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.FolderPath);
          this.groupBox2.Controls.Add(this.label1);
          this.groupBox2.Controls.Add(this.StatusBox);
          this.groupBox2.Location = new System.Drawing.Point(12, 27);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(308, 66);
          this.groupBox2.TabIndex = 5;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Game Control";
          // 
          // FolderPath
          // 
          this.FolderPath.Location = new System.Drawing.Point(75, 42);
          this.FolderPath.Name = "FolderPath";
          this.FolderPath.Size = new System.Drawing.Size(224, 14);
          this.FolderPath.TabIndex = 6;
          this.FolderPath.Text = "None selected";
          this.FolderPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
          // 
          // label1
          // 
          this.label1.Location = new System.Drawing.Point(5, 43);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(73, 13);
          this.label1.TabIndex = 5;
          this.label1.Text = "Game Folder: ";
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
          this.MainMenu.Size = new System.Drawing.Size(336, 24);
          this.MainMenu.TabIndex = 6;
          this.MainMenu.Text = "menuStrip1";
          // 
          // fileToolStripMenuItem
          // 
          this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewGameMenuItem,
            this.SelectNewFolderMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
          this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
          this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
          this.fileToolStripMenuItem.Text = "&File";
          // 
          // NewGameMenuItem
          // 
          this.NewGameMenuItem.Name = "NewGameMenuItem";
          this.NewGameMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
          this.NewGameMenuItem.Size = new System.Drawing.Size(211, 22);
          this.NewGameMenuItem.Text = "&New Game";
          this.NewGameMenuItem.Click += new System.EventHandler(this.NewGameMenuItem_Click);
          // 
          // SelectNewFolderMenuItem
          // 
          this.SelectNewFolderMenuItem.Name = "SelectNewFolderMenuItem";
          this.SelectNewFolderMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
          this.SelectNewFolderMenuItem.Size = new System.Drawing.Size(211, 22);
          this.SelectNewFolderMenuItem.Text = "&Select New Folder";
          this.SelectNewFolderMenuItem.Click += new System.EventHandler(this.SelectNewFolder);
          // 
          // toolStripSeparator1
          // 
          this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
          // 
          // ExitMenuItem
          // 
          this.ExitMenuItem.Name = "ExitMenuItem";
          this.ExitMenuItem.Size = new System.Drawing.Size(211, 22);
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
          this.turnToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
          this.turnToolStripMenuItem.Text = "&Turn";
          // 
          // GenerateTurnMenuItem
          // 
          this.GenerateTurnMenuItem.Name = "GenerateTurnMenuItem";
          this.GenerateTurnMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
          this.GenerateTurnMenuItem.Size = new System.Drawing.Size(188, 22);
          this.GenerateTurnMenuItem.Text = "&Generate";
          this.GenerateTurnMenuItem.Click += new System.EventHandler(this.GenerateTurnMenuItem_Click);
          // 
          // ForceMenuItem
          // 
          this.ForceMenuItem.Name = "ForceMenuItem";
          this.ForceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
          this.ForceMenuItem.Size = new System.Drawing.Size(188, 22);
          this.ForceMenuItem.Text = "&Force Next Turn";
          this.ForceMenuItem.Click += new System.EventHandler(this.ForceMenuItem_Click);
          // 
          // toolStripSeparator2
          // 
          this.toolStripSeparator2.Name = "toolStripSeparator2";
          this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
          // 
          // RefreshMenuItem
          // 
          this.RefreshMenuItem.Name = "RefreshMenuItem";
          this.RefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
          this.RefreshMenuItem.Size = new System.Drawing.Size(188, 22);
          this.RefreshMenuItem.Text = "&Refresh";
          this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
          // 
          // helpToolStripMenuItem
          // 
          this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
          this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
          this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
          this.helpToolStripMenuItem.Text = "&Help";
          // 
          // aboutToolStripMenuItem
          // 
          this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
          this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
          this.aboutToolStripMenuItem.Text = "&About";
          this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClick);
          // 
          // NovaConsoleMain
          // 
          this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
          this.ClientSize = new System.Drawing.Size(336, 294);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.MainMenu);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MainMenuStrip = this.MainMenu;
          this.MaximizeBox = false;
          this.Name = "NovaConsoleMain";
          this.Text = "Nova Console";
          this.Shown += new System.EventHandler(this.OnFirstShow);
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleFormClosing);
          this.groupBox1.ResumeLayout(false);
          this.groupBox2.ResumeLayout(false);
          this.MainMenu.ResumeLayout(false);
          this.MainMenu.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

}
#endregion


// ============================================================================
// The main entry point for the application.
// ============================================================================

      [STAThread]
      static void Main() 
      {
         Application.EnableVisualStyles();
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
          GameFolder.Identify();

          ConsoleState stateData = ConsoleState.Data;
          FolderPath.Text = stateData.GameFolder;

          if (stateData.AllRaces.Count == 0) {
             Players.Identify();
             NewGameMenuItem.Enabled = true;
          }
          else if (stateData.GameInProgress == false) {
              NewGameMenuItem.Enabled = true;
          }
          else {
              GenerateTurnMenuItem.Enabled = true;
          }

          OrderReader.ReadOrders();
          SetPlayerList();
       }


// ============================================================================
// Set the player list "IntelWriter In" field.
// ============================================================================

      private bool SetPlayerList() //returns true if ready
      {
          bool result = true;
          PlayerList.Items.Clear();
          ConsoleState stateData = ConsoleState.Data;

          foreach (Race race in stateData.AllRaces.Values)
          {
              ListViewItem listItem = new ListViewItem(race.Name);

              RaceData raceData = stateData.AllRaceData[race.Name] as RaceData;

              if (raceData == null || raceData.TurnYear != stateData.TurnYear)
              {
                  listItem.SubItems.Add("N");
                  result = false; //dont allow the turn to be generated
              }
              else
              {
                  listItem.SubItems.Add("Y");
              }

              PlayerList.Items.Add(listItem);
          }

          return result;
      }


// ============================================================================
// Save console persistent data on exit.
// ============================================================================

      private void ConsoleFormClosing(object sender, FormClosingEventArgs e)
      {
         ConsoleState.Save();
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


// ============================================================================
// Select a new Game Folder
// ============================================================================

      private void SelectNewFolder(object sender, EventArgs e)
      {
         RegistryKey regKey    = null;
         RegistryKey regSubKey = null;
         
         regKey      = Registry.CurrentUser;
         regSubKey   = regKey.CreateSubKey(Global.RootRegistryKey);
         regSubKey.SetValue(Global.ServerFolderKey, "");

         ConsoleState.Clear();
         OnFirstShow(null, null);
      }

      private void NewGameMenuItem_Click(object sender, EventArgs e)
      {
          bool gameStarted = NovaConsole.NewGame.Start();
          if (gameStarted == false)
          {
              return;
          }

          NewGameMenuItem.Enabled = false;
          GenerateTurnMenuItem.Enabled = false;
          ConsoleState.Data.GameInProgress = true;
          StatusBox.Text = "New game generated";
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
          Players.Identify();
          
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

      private void GenerateTurn()
      {
          ProcessTurn.Generate();

          NewGameMenuItem.Enabled = false;
          GenerateTurnMenuItem.Enabled = false;

          StatusBox.Text = "New turn generated for year "
                         + ConsoleState.Data.TurnYear;

          SetPlayerList();
      }

   }

}
