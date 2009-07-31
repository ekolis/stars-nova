// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module holds the program entry point and handles all things related to
// the main GUI window.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Nova
{

// ============================================================================
// Main Windows form class.
// ============================================================================

   public class NovaGUI : System.Windows.Forms.Form
   {
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ToolTip ToolTips;

      public  Messages messages;
      public  SelectionSummary SelectionSummary;
      private MenuStrip MainMenu;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem toolsToolStripMenuItem;
      private ToolStripMenuItem researchToolStripMenuItem;
      private ToolStripMenuItem shipDesignerToolStripMenuItem;
      private ToolStripMenuItem helpToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;
      private ToolStripMenuItem playerRelationslMenuItem;
      private ToolStripMenuItem BattlePlansMenu;
      public  Nova.Controls.SelectionDetail SelectionDetail;
      private ToolStripMenuItem designManagerMenuItem;
      private ToolStripMenuItem reportsToolStripMenuItem;
      private ToolStripMenuItem PlanetReportMenu;
      private ToolStripMenuItem FleetReportMenu;
      private ToolStripMenuItem BattlesReportMenu;
      private ToolStripMenuItem ScoresMenuItem;
      private ToolStripMenuItem generateTurnToolStripMenuItem;
      private ToolStripMenuItem loadNextTurnToolStripMenuItem;
      public  StarMap MapControl;

      public int    currentTurn;      //control turnvar used for to decide to load new turn... (Thread)
      public string currentRace; //control var used for to decide to load new turn... (Thread)


// ============================================================================
// Construct the main window.
// ============================================================================

      public NovaGUI() 
      {
         InitializeComponent();
         //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
         //UpdateStyles();
         //DoubleBuffered = true;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaGUI));
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.MapControl = new Nova.StarMap();
      this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
      this.MainMenu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.BattlePlansMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.playerRelationslMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.researchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.shipDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.designManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadNextTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.BattlesReportMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.PlanetReportMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.FleetReportMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.ScoresMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SelectionDetail = new Nova.Controls.SelectionDetail();
      this.SelectionSummary = new Nova.SelectionSummary();
      this.messages = new Nova.Messages();
      this.groupBox2.SuspendLayout();
      this.MainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.MapControl);
      this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox2.Location = new System.Drawing.Point(374, 24);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(611, 699);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Star Map";
      // 
      // MapControl
      // 
      this.MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MapControl.Location = new System.Drawing.Point(3, 16);
      this.MapControl.Name = "MapControl";
      this.MapControl.Size = new System.Drawing.Size(605, 680);
      this.MapControl.TabIndex = 0;
      // 
      // MainMenu
      // 
      this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
      this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.MainMenu.Location = new System.Drawing.Point(0, 0);
      this.MainMenu.Name = "MainMenu";
      this.MainMenu.Size = new System.Drawing.Size(993, 24);
      this.MainMenu.TabIndex = 20;
      this.MainMenu.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.MenuExit_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BattlePlansMenu,
            this.playerRelationslMenuItem,
            this.researchToolStripMenuItem,
            this.shipDesignerToolStripMenuItem,
            this.designManagerMenuItem,
            this.generateTurnToolStripMenuItem,
            this.loadNextTurnToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
      this.toolsToolStripMenuItem.Text = "Commands";
      // 
      // BattlePlansMenu
      // 
      this.BattlePlansMenu.Name = "BattlePlansMenu";
      this.BattlePlansMenu.Size = new System.Drawing.Size(179, 22);
      this.BattlePlansMenu.Text = "Battle Plans";
      this.BattlePlansMenu.Click += new System.EventHandler(this.BattlePlansMenuItem);
      // 
      // playerRelationslMenuItem
      // 
      this.playerRelationslMenuItem.Name = "playerRelationslMenuItem";
      this.playerRelationslMenuItem.Size = new System.Drawing.Size(179, 22);
      this.playerRelationslMenuItem.Text = "Player Relations";
      this.playerRelationslMenuItem.Click += new System.EventHandler(this.playerRelationsMenuItem_Click);
      // 
      // researchToolStripMenuItem
      // 
      this.researchToolStripMenuItem.Name = "researchToolStripMenuItem";
      this.researchToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.researchToolStripMenuItem.Text = "Research";
      this.researchToolStripMenuItem.Click += new System.EventHandler(this.MenuResearch);
      // 
      // shipDesignerToolStripMenuItem
      // 
      this.shipDesignerToolStripMenuItem.Name = "shipDesignerToolStripMenuItem";
      this.shipDesignerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.shipDesignerToolStripMenuItem.Text = "Ship Designer";
      this.shipDesignerToolStripMenuItem.Click += new System.EventHandler(this.MenuShipDesign);
      // 
      // designManagerMenuItem
      // 
      this.designManagerMenuItem.Name = "designManagerMenuItem";
      this.designManagerMenuItem.Size = new System.Drawing.Size(179, 22);
      this.designManagerMenuItem.Text = "Ship Design Manager";
      this.designManagerMenuItem.Click += new System.EventHandler(this.designManagerMenuItem_Click);
      // 
      // generateTurnToolStripMenuItem
      // 
      this.generateTurnToolStripMenuItem.Name = "generateTurnToolStripMenuItem";
      this.generateTurnToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.generateTurnToolStripMenuItem.Text = "Save && SubmitTurn";
      this.generateTurnToolStripMenuItem.Click += new System.EventHandler(this.generateTurnToolStripMenuItem_Click);
      // 
      // loadNextTurnToolStripMenuItem
      // 
      this.loadNextTurnToolStripMenuItem.Name = "loadNextTurnToolStripMenuItem";
      this.loadNextTurnToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.loadNextTurnToolStripMenuItem.Text = "Load Next Turn";
      this.loadNextTurnToolStripMenuItem.Click += new System.EventHandler(this.loadNextTurnToolStripMenuItem_Click);
      // 
      // reportsToolStripMenuItem
      // 
      this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BattlesReportMenu,
            this.PlanetReportMenu,
            this.FleetReportMenu,
            this.ScoresMenuItem});
      this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
      this.reportsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
      this.reportsToolStripMenuItem.Text = "Reports";
      // 
      // BattlesReportMenu
      // 
      this.BattlesReportMenu.Name = "BattlesReportMenu";
      this.BattlesReportMenu.Size = new System.Drawing.Size(151, 22);
      this.BattlesReportMenu.Text = "Battles";
      this.BattlesReportMenu.Click += new System.EventHandler(this.BattlesReportMenu_Click);
      // 
      // PlanetReportMenu
      // 
      this.PlanetReportMenu.Name = "PlanetReportMenu";
      this.PlanetReportMenu.Size = new System.Drawing.Size(151, 22);
      this.PlanetReportMenu.Text = "Player\'s Planets";
      this.PlanetReportMenu.Click += new System.EventHandler(this.PlanetReportMenu_Click);
      // 
      // FleetReportMenu
      // 
      this.FleetReportMenu.Name = "FleetReportMenu";
      this.FleetReportMenu.Size = new System.Drawing.Size(151, 22);
      this.FleetReportMenu.Text = "Player\'s Fleets";
      this.FleetReportMenu.Click += new System.EventHandler(this.FleetReportMenu_Click);
      // 
      // ScoresMenuItem
      // 
      this.ScoresMenuItem.Name = "ScoresMenuItem";
      this.ScoresMenuItem.Size = new System.Drawing.Size(151, 22);
      this.ScoresMenuItem.Text = "Scores";
      this.ScoresMenuItem.Click += new System.EventHandler(this.ScoresMenuItem_Click);
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
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.MenuAbout);
      // 
      // SelectionDetail
      // 
      this.SelectionDetail.Location = new System.Drawing.Point(8, 24);
      this.SelectionDetail.Name = "SelectionDetail";
      this.SelectionDetail.Size = new System.Drawing.Size(360, 400);
      this.SelectionDetail.TabIndex = 21;
      this.SelectionDetail.Value = null;
      // 
      // SelectionSummary
      // 
      this.SelectionSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SelectionSummary.Location = new System.Drawing.Point(8, 546);
      this.SelectionSummary.Name = "SelectionSummary";
      this.SelectionSummary.Size = new System.Drawing.Size(360, 177);
      this.SelectionSummary.TabIndex = 19;
      this.SelectionSummary.Value = null;
      // 
      // messages
      // 
      this.messages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.messages.Location = new System.Drawing.Point(8, 427);
      this.messages.Name = "messages";
      this.messages.Size = new System.Drawing.Size(360, 113);
      this.messages.TabIndex = 18;
      this.messages.Year = 2100;
      // 
      // NovaGUI
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(993, 730);
      this.Controls.Add(this.MainMenu);
      this.Controls.Add(this.SelectionDetail);
      this.Controls.Add(this.SelectionSummary);
      this.Controls.Add(this.messages);
      this.Controls.Add(this.groupBox2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.MainMenu;
      this.MinimumSize = new System.Drawing.Size(928, 670);
      this.Name = "NovaGUI";
      this.Text = "Nova";
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NovaGUI_FormClosing);
      this.groupBox2.ResumeLayout(false);
      this.MainMenu.ResumeLayout(false);
      this.MainMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

}
#endregion


// ============================================================================
// The main entry point for the application. Initialise the Nova GUI state data
// (this will be a partial initialisation on the very first run of the GUI in a
// new game). Initialise the components of the main GUI window and and then
// start the Windows message processing.
// ============================================================================

      [STAThread]
      static void Main() 
      {
         Application.EnableVisualStyles();
         GUIdata.Load(true);        
         MainWindow.InitialiseControls();
         Application.Run(MainWindow.nova);
      }
   

// ============================================================================
// Exit menu item selected.
// ============================================================================

      private void MenuExit_Click(object sender, System.EventArgs e)
      {
         GUIstate.Save();
         Close();
      }


// ============================================================================
// Pop up the ship design dialog.
// ============================================================================

      private void MenuShipDesign(object sender, System.EventArgs e) {
         ShipDesignDialog ShipDesignDialog = new ShipDesignDialog();
         ShipDesignDialog.ShowDialog();
         ShipDesignDialog.Dispose();
      }
   
   
// ============================================================================
// Deal with keys being pressed.
// ============================================================================

      private void OnKeyPress(object sender, KeyPressEventArgs e) 
      {
         switch (e.KeyChar) {
         case '+':
            e.Handled = true;
            MapControl.ZoomInClick(null, null);
            break;
   
         case '-':
            e.Handled = true;
            MapControl.ZoomOutClick(null, null);
            break;
         }
      }
   
   
// ============================================================================
// Display the "About" dialog
// ============================================================================

      private void MenuAbout(object sender, EventArgs e)
      {
         AboutBox aboutBox = new AboutBox();
         aboutBox.ShowDialog();
         aboutBox.Dispose();
      }


// ============================================================================
// Display the research dialog
// ============================================================================

      private void MenuResearch(object sender, EventArgs e)
      {
         ResearchDialog pResearchDialog = new ResearchDialog();
         pResearchDialog.ShowDialog();
         pResearchDialog.Dispose();
      }


// ============================================================================
// Main Window is closing (i.e. the "X" button has been pressed on the frame of
// the form. Save the local state data and generate the turn file.
// ============================================================================
      
      private void NovaGUI_FormClosing(object sender, FormClosingEventArgs e)
      {
         GUIstate.Save();
         PlayerTurn.Generate();
      }


// ============================================================================
// Pop up the player relations dialog.
// ============================================================================

      private void playerRelationsMenuItem_Click(object sender, EventArgs e)
      {
         PlayerRelations relationshipDialog = new PlayerRelations();
         relationshipDialog.ShowDialog();
         relationshipDialog.Dispose();
      }


// ============================================================================
// Pop up the battle plans dialog.
// ============================================================================

      private void BattlePlansMenuItem(object sender, EventArgs e)
      {
         BattlePlans battlePlans = new BattlePlans();
         battlePlans.ShowDialog();
         battlePlans.Dispose();
      }


// ============================================================================
// Pop up the Design Manager Dialog
// ============================================================================

      private void designManagerMenuItem_Click(object sender, EventArgs e)
      {
         DesignManager designManager = new DesignManager();
         designManager.ShowDialog();
         designManager.Dispose();
      }


// ============================================================================
// Pop up the Planet Report Dialog
// ============================================================================

      private void PlanetReportMenu_Click(object sender, EventArgs e)
      {
         PlanetReport planetReport = new PlanetReport();
         planetReport.ShowDialog();
         planetReport.Dispose();
      }


// ============================================================================
// Pop up the Fleet Report Dialog
// ============================================================================

      private void FleetReportMenu_Click(object sender, EventArgs e)
      {
         FleetReport fleetReport = new FleetReport();
         fleetReport.ShowDialog();
         fleetReport.Dispose();
      }


// ============================================================================
// Pop up the Battle Report Dialog
// ============================================================================

      private void BattlesReportMenu_Click(object sender, EventArgs e)
      {
         BattleReportDialog battleReport = new BattleReportDialog();
         battleReport.ShowDialog();
         battleReport.Dispose();
      }


// ============================================================================
// Pop up the score report dialog.
// ============================================================================

       private void ScoresMenuItem_Click(object sender, EventArgs e)
       {
          ScoreReport scoreReport = new ScoreReport();
          scoreReport.ShowDialog();
          scoreReport.Dispose();
       }

       private void generateTurnToolStripMenuItem_Click(object sender, EventArgs e)
       {
          GUIstate.Save();
          PlayerTurn.Generate();
       }

       private void loadNextTurnToolStripMenuItem_Click(object sender, EventArgs e)
       {
          GUIdata.Load(false);
          MainWindow.NextTurn();
       }
   }


// ============================================================================
// The main window.
// ============================================================================

   public static class MainWindow
   {
      public static NovaGUI nova = new NovaGUI();


// ============================================================================
// Load controls with any data we may have for them.
// ============================================================================

      public static void InitialiseControls()
      {
         nova.messages.Year        = GUIstate.Data.TurnYear;
         nova.messages.MessageList = GUIstate.Data.Messages;
         
         nova.currentTurn = GUIstate.Data.TurnYear;
         nova.currentRace = GUIstate.Data.RaceName;

         nova.MapControl.Initialise();

         // Select a star owned by the player (if any) as the default display.

         foreach (Star star in GUIstate.Data.InputTurn.AllStars.Values) {
            if (star.Owner == GUIstate.Data.RaceName) {
               nova.MapControl.SetCursor(star.Position);
               nova.SelectionDetail.Value  = star;
               nova.SelectionSummary.Value = star;
               break;
            }
         }
      }

      public static void NextTurn()
      {
         nova.messages.Year = GUIstate.Data.TurnYear;
         nova.messages.MessageList = GUIstate.Data.Messages;
         
         nova.Invalidate(true);

         nova.MapControl.Initialise();
         nova.MapControl.Invalidate();

         // Select a star owned by the player (if any) as the default display.

         foreach (Star star in GUIstate.Data.InputTurn.AllStars.Values)
         {
            if (star.Owner == GUIstate.Data.RaceName)
            {
               nova.MapControl.SetCursor(star.Position);
               nova.SelectionDetail.Value = star;
               nova.SelectionSummary.Value = star;
               break;
            }
         }
      }
   }
   
}
