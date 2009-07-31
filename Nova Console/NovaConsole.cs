// This file needs -*- c++ -*- mode
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
      private System.Windows.Forms.Button NewGame;
      private System.Windows.Forms.Button GenerateTurn;
      private System.Windows.Forms.Button ExitButton;
      private GroupBox groupBox1;
      private Label StatusBox;
      private GroupBox groupBox2;
      private MenuStrip MainMenu;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem helpToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;
      private ToolStripMenuItem SelectNewFolderMenuItem;
      private Label FolderPath;
      private Label label1;
      private ListView PlayerList;
      private ColumnHeader RaceName;
      private ColumnHeader TurnIn;
      private Button btn_Refresh;
      private Button btn_force;
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
         this.NewGame = new System.Windows.Forms.Button();
         this.GenerateTurn = new System.Windows.Forms.Button();
         this.ExitButton = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.PlayerList = new System.Windows.Forms.ListView();
         this.RaceName = new System.Windows.Forms.ColumnHeader();
         this.TurnIn = new System.Windows.Forms.ColumnHeader();
         this.StatusBox = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.btn_Refresh = new System.Windows.Forms.Button();
         this.FolderPath = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.MainMenu = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SelectNewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.btn_force = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.MainMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // NewGame
         // 
         this.NewGame.Enabled = false;
         this.NewGame.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.NewGame.Location = new System.Drawing.Point(5, 19);
         this.NewGame.Name = "NewGame";
         this.NewGame.Size = new System.Drawing.Size(47, 32);
         this.NewGame.TabIndex = 0;
         this.NewGame.Text = "New Game";
         this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
         // 
         // GenerateTurn
         // 
         this.GenerateTurn.Enabled = false;
         this.GenerateTurn.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.GenerateTurn.Location = new System.Drawing.Point(136, 19);
         this.GenerateTurn.Name = "GenerateTurn";
         this.GenerateTurn.Size = new System.Drawing.Size(63, 32);
         this.GenerateTurn.TabIndex = 1;
         this.GenerateTurn.Text = "Generate Next Turn";
         this.GenerateTurn.Click += new System.EventHandler(this.GenerateTurn_Click);
         // 
         // ExitButton
         // 
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.ExitButton.Location = new System.Drawing.Point(265, 19);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(38, 32);
         this.ExitButton.TabIndex = 2;
         this.ExitButton.Text = "Exit";
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.PlayerList);
         this.groupBox1.Location = new System.Drawing.Point(12, 145);
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
         this.StatusBox.Location = new System.Drawing.Point(6, 64);
         this.StatusBox.Name = "StatusBox";
         this.StatusBox.Size = new System.Drawing.Size(296, 18);
         this.StatusBox.TabIndex = 4;
         this.StatusBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.btn_force);
         this.groupBox2.Controls.Add(this.btn_Refresh);
         this.groupBox2.Controls.Add(this.FolderPath);
         this.groupBox2.Controls.Add(this.label1);
         this.groupBox2.Controls.Add(this.NewGame);
         this.groupBox2.Controls.Add(this.StatusBox);
         this.groupBox2.Controls.Add(this.GenerateTurn);
         this.groupBox2.Controls.Add(this.ExitButton);
         this.groupBox2.Location = new System.Drawing.Point(12, 27);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(308, 112);
         this.groupBox2.TabIndex = 5;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Game Control";
         // 
         // btn_Refresh
         // 
         this.btn_Refresh.Location = new System.Drawing.Point(78, 19);
         this.btn_Refresh.Name = "btn_Refresh";
         this.btn_Refresh.Size = new System.Drawing.Size(52, 32);
         this.btn_Refresh.TabIndex = 7;
         this.btn_Refresh.Text = "Refresh";
         this.btn_Refresh.UseVisualStyleBackColor = true;
         this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
         // 
         // FolderPath
         // 
         this.FolderPath.Location = new System.Drawing.Point(75, 90);
         this.FolderPath.Name = "FolderPath";
         this.FolderPath.Size = new System.Drawing.Size(224, 14);
         this.FolderPath.TabIndex = 6;
         this.FolderPath.Text = "None selected";
         this.FolderPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(5, 91);
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
            this.SelectNewFolderMenuItem,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // SelectNewFolderMenuItem
         // 
         this.SelectNewFolderMenuItem.Name = "SelectNewFolderMenuItem";
         this.SelectNewFolderMenuItem.Size = new System.Drawing.Size(164, 22);
         this.SelectNewFolderMenuItem.Text = "Select New Folder";
         this.SelectNewFolderMenuItem.Click += new System.EventHandler(this.SelectNewFolder);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
         this.helpToolStripMenuItem.Text = "Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
         this.aboutToolStripMenuItem.Text = "About";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClick);
         // 
         // btn_force
         // 
         this.btn_force.Location = new System.Drawing.Point(205, 19);
         this.btn_force.Name = "btn_force";
         this.btn_force.Size = new System.Drawing.Size(42, 32);
         this.btn_force.TabIndex = 8;
         this.btn_force.Text = "Force";
         this.btn_force.UseVisualStyleBackColor = true;
         this.btn_force.Click += new System.EventHandler(this.btn_force_Click);
         // 
         // NovaConsoleMain
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(336, 338);
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
// This function is called when the New Game button is pressed.
// ============================================================================

      private void NewGame_Click(object sender, EventArgs e)
      {
         bool gameStarted = NovaConsole.NewGame.Start();
         if (gameStarted == false) {
            return;
         }

         NewGame.Enabled                  = false;
         GenerateTurn.Enabled             = false;
         ConsoleState.Data.GameInProgress = true;
         StatusBox.Text                   = "New game generated";
      }


// ============================================================================
// This function is called when the Generate Turn button is pressed.
// ============================================================================

      private void GenerateTurn_Click(object sender, System.EventArgs e)
      {
         NewTurn.Generate();

         NewGame.Enabled      = false;
         GenerateTurn.Enabled = false;

         StatusBox.Text = "New turn generated for year " 
                        + ConsoleState.Data.TurnYear;

         SetPlayerList();
      }


// ============================================================================
// This function is called when the Exit button is pressed.
// ============================================================================

      private void ExitButton_Click(object sender, EventArgs e)
      {
         Close();
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
             NewGame.Enabled = true;
          }
          else if (stateData.GameInProgress == false) {
             NewGame.Enabled = true;
          }
          else {
             GenerateTurn.Enabled = true;
          }

          Players.ReadData();
          SetPlayerList();
       }


// ============================================================================
// Set the player list "Turn In" field.
// ============================================================================

      private bool SetPlayerList() //returns true if ready
      {
          bool result = true;
          PlayerList.Items.Clear();
          ConsoleState stateData = ConsoleState.Data;

          foreach (Race race in stateData.AllRaces.Values) {
             ListViewItem listItem = new ListViewItem(race.Name);

             RaceData raceData = stateData.AllRaceData[race.Name] as RaceData;
             if (raceData.TurnYear == stateData.TurnYear) {
                listItem.SubItems.Add("Y");
             }
             else {
                listItem.SubItems.Add("N");
                result = false; //dont allow the turn to be generated
             }
             
             PlayerList.Items.Add(listItem);
          }

          return result;
      }

// ============================================================================
// Refresh the turn in fields...
// ============================================================================

      private void btn_Refresh_Click(object sender, EventArgs e)
      {
         Players.Identify();
         Players.ReadData();

         if (SetPlayerList())
            GenerateTurn.Enabled = true;
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

      private void btn_force_Click(object sender, EventArgs e)
      {
         GenerateTurn.Enabled = true;
      }
      
   }

}
