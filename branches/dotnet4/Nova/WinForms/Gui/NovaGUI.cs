#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// The Nova GUI is the program used to play a turn of nova. In a multiplayer/network
// game it is the main client side program. The Nova GUI reads in a .intel
// file to determine what the player race knows about the universe and when a 
// turn is submitted, generates a .orders file for processing of the next game
// year. A history is maintained by the ConsoleState object as a .state file.
//
// This module holds the program entry Point and handles all things related to
// the main GUI window.
// ===========================================================================
#endregion

namespace Nova.WinForms.Gui
{
    using System;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;

    /// <Summary>
    /// Main Windows form class.
    /// </Summary>
    public class NovaGUI : System.Windows.Forms.Form
    {
        public Messages Messages;
        public SelectionSummary SelectionSummary;
        public SelectionDetail SelectionDetail;
        public StarMap MapControl;

        public int CurrentTurn;      // control turnvar used for to decide to load new turn... (Thread)
        public string CurrentRace;   // control var used for to decide to load new turn... (Thread)

        #region VS-Designer Generated Variables

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.GroupBox groupBox2;

        private MenuStrip mainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem researchToolStripMenuItem;
        private ToolStripMenuItem shipDesignerToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem playerRelationslMenuItem;
        private ToolStripMenuItem battlePlansMenu;
        private ToolStripMenuItem designManagerMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem planetReportMenu;
        private ToolStripMenuItem fleetReportMenu;
        private ToolStripMenuItem battlesReportMenu;
        private ToolStripMenuItem scoresMenuItem;
        private ToolStripMenuItem generateTurnToolStripMenuItem;
        private ToolStripMenuItem loadNextTurnToolStripMenuItem;

        #endregion

        #region Construction and Disposal

        /// <Summary>
        /// Construct the main window.
        /// </Summary>
        public NovaGUI()
        {
            InitializeComponent();
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

        #endregion

        #region Windows Form Designer generated code

        /// <Summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </Summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaGUI));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MapControl = new StarMap();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.researchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.battlePlansMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.playerRelationslMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNextTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planetReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fleetReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.battlesReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.scoresMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectionDetail = new SelectionDetail();
            this.SelectionSummary = new SelectionSummary();
            this.Messages = new Messages();
            this.groupBox2.SuspendLayout();
            this.mainMenu.SuspendLayout();
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
            this.MapControl.RequestSelectionEvent += new RequestSelection(this.SelectionDetail.ReportItem);
            this.MapControl.SelectionChangedEvent += new SelectionChanged(this.SelectionDetail.SelectionChanged);
            this.MapControl.SelectionChangedEvent += new SelectionChanged(this.SelectionSummary.SelectionChanged);
            this.MapControl.WaypointChangedEvent += new WaypointChanged(this.SelectionDetail.FleetDetail.WaypointListChanged);
            // 
            // MainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(993, 24);
            this.mainMenu.TabIndex = 20;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.designManagerMenuItem,
            this.shipDesignerToolStripMenuItem,
            this.researchToolStripMenuItem,
            this.battlePlansMenu,
            this.playerRelationslMenuItem,
            this.generateTurnToolStripMenuItem,
            this.loadNextTurnToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.toolsToolStripMenuItem.Text = "&Commands";
            // 
            // designManagerMenuItem
            // 
            this.designManagerMenuItem.Name = "designManagerMenuItem";
            this.designManagerMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.designManagerMenuItem.Size = new System.Drawing.Size(205, 22);
            this.designManagerMenuItem.Text = "Ship Design &Manager";
            this.designManagerMenuItem.Click += new System.EventHandler(this.DesignManagerMenuItem_Click);
            // 
            // shipDesignerToolStripMenuItem
            // 
            this.shipDesignerToolStripMenuItem.Name = "shipDesignerToolStripMenuItem";
            this.shipDesignerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.shipDesignerToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.shipDesignerToolStripMenuItem.Text = "&Ship Designer";
            this.shipDesignerToolStripMenuItem.Click += new System.EventHandler(this.MenuShipDesign);
            // 
            // researchToolStripMenuItem
            // 
            this.researchToolStripMenuItem.Name = "researchToolStripMenuItem";
            this.researchToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.researchToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.researchToolStripMenuItem.Text = "&Research";
            this.researchToolStripMenuItem.Click += new System.EventHandler(this.MenuResearch);
            // 
            // BattlePlansMenu
            // 
            this.battlePlansMenu.Name = "BattlePlansMenu";
            this.battlePlansMenu.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.battlePlansMenu.Size = new System.Drawing.Size(205, 22);
            this.battlePlansMenu.Text = "&Battle Plans";
            this.battlePlansMenu.Click += new System.EventHandler(this.BattlePlansMenuItem);
            // 
            // playerRelationslMenuItem
            // 
            this.playerRelationslMenuItem.Name = "playerRelationslMenuItem";
            this.playerRelationslMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.playerRelationslMenuItem.Size = new System.Drawing.Size(205, 22);
            this.playerRelationslMenuItem.Text = "&Player Relations";
            this.playerRelationslMenuItem.Click += new System.EventHandler(this.PlayerRelationsMenuItem_Click);
            // 
            // generateTurnToolStripMenuItem
            // 
            this.generateTurnToolStripMenuItem.Name = "generateTurnToolStripMenuItem";
            this.generateTurnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.generateTurnToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.generateTurnToolStripMenuItem.Text = "Save && Submit &Turn";
            this.generateTurnToolStripMenuItem.Click += new System.EventHandler(this.SaveAndSubmitTurnToolStripMenuItem_Click);
            // 
            // loadNextTurnToolStripMenuItem
            // 
            this.loadNextTurnToolStripMenuItem.Enabled = false;
            this.loadNextTurnToolStripMenuItem.Name = "loadNextTurnToolStripMenuItem";
            this.loadNextTurnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.loadNextTurnToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.loadNextTurnToolStripMenuItem.Text = "&Load Next Turn";
            this.loadNextTurnToolStripMenuItem.Click += new System.EventHandler(this.LoadNextTurnToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planetReportMenu,
            this.fleetReportMenu,
            this.battlesReportMenu,
            this.scoresMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // PlanetReportMenu
            // 
            this.planetReportMenu.Name = "PlanetReportMenu";
            this.planetReportMenu.Size = new System.Drawing.Size(155, 22);
            this.planetReportMenu.Text = "Player\'s &Planets";
            this.planetReportMenu.Click += new System.EventHandler(this.PlanetReportMenu_Click);
            // 
            // FleetReportMenu
            // 
            this.fleetReportMenu.Name = "FleetReportMenu";
            this.fleetReportMenu.Size = new System.Drawing.Size(155, 22);
            this.fleetReportMenu.Text = "Player\'s &Fleets";
            this.fleetReportMenu.Click += new System.EventHandler(this.FleetReportMenu_Click);
            // 
            // BattlesReportMenu
            // 
            this.battlesReportMenu.Name = "BattlesReportMenu";
            this.battlesReportMenu.Size = new System.Drawing.Size(155, 22);
            this.battlesReportMenu.Text = "&Battles";
            this.battlesReportMenu.Click += new System.EventHandler(this.BattlesReportMenu_Click);
            // 
            // ScoresMenuItem
            // 
            this.scoresMenuItem.Name = "ScoresMenuItem";
            this.scoresMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.scoresMenuItem.Size = new System.Drawing.Size(155, 22);
            this.scoresMenuItem.Text = "&Scores";
            this.scoresMenuItem.Click += new System.EventHandler(this.ScoresMenuItem_Click);
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
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.MenuAbout);
            // 
            // SelectionDetail
            // 
            this.SelectionDetail.Location = new System.Drawing.Point(8, 24);
            this.SelectionDetail.Name = "SelectionDetail";
            this.SelectionDetail.Size = new System.Drawing.Size(360, 400);
            this.SelectionDetail.TabIndex = 21;
            this.SelectionDetail.Value = null;
            this.SelectionDetail.FleetDetail.FleetSelectionChangedEvent += new FleetSelectionChanged(this.FleetChangeSelection);
            this.SelectionDetail.FleetDetail.CursorChangedEvent += new CursorChanged(this.MapControl.ChangeCursor);
            this.SelectionDetail.FleetDetail.RefreshStarMapEvent += new RefreshStarMap(this.MapControl.RefreshStarMap);
            this.SelectionDetail.PlanetDetail.StarSelectionChangedEvent += new StarSelectionChanged(this.StarChangeSelection);
            this.SelectionDetail.PlanetDetail.CursorChangedEvent += new CursorChanged(this.MapControl.ChangeCursor);
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
            this.Messages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.Messages.Location = new System.Drawing.Point(8, 427);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(360, 113);
            this.Messages.TabIndex = 18;
            this.Messages.Year = 2100;
            // 
            // NovaGUI
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(993, 730);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.SelectionDetail);
            this.Controls.Add(this.SelectionSummary);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(928, 670);
            this.Name = "NovaGUI";
            this.Text = "Nova";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NovaGUI_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Event Methods

        /// <Summary>
        /// Exit menu Item selected.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuExit_Click(object sender, System.EventArgs e)
        {
            ClientState.Save();
            Close();
        }

        /// <Summary>
        /// Pop up the ship design dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuShipDesign(object sender, System.EventArgs e)
        {
            ShipDesignDialog shipDesignDialog = new ShipDesignDialog();
            shipDesignDialog.ShowDialog();
            shipDesignDialog.Dispose();
        }

        /// <Summary>
        /// Deal with keys being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
          
            switch (e.KeyChar)
            {
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

        /// <Summary>
        /// Display the "About" dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuAbout(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        /// <Summary>
        /// Display the research dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuResearch(object sender, EventArgs e)
        {
            ResearchDialog newResearchDialog = new ResearchDialog();
            newResearchDialog.ResearchAllocationChangedEvent += new ResearchAllocationChanged(this.UpdateResearchBudgets);
            newResearchDialog.ShowDialog();
            newResearchDialog.Dispose();
        }

        /// <Summary>
        /// Main Window is closing (i.e. the "X" button has been pressed on the frame of
        /// the form. Save the local state data.
        /// </Summary><remarks>
        /// NB: Don't generate the orders file unless Save&Submit is selected.
        /// TODO (priority 7) - ask the user if they want to submit the current turn before closing.
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NovaGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientState.Save();
            // OrderWriter.WriteOrders(); // don't do this here, do it only on save & submit.
        }

        /// <Summary>
        /// Pop up the player relations dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlayerRelationsMenuItem_Click(object sender, EventArgs e)
        {
            PlayerRelations relationshipDialog = new PlayerRelations();
            relationshipDialog.ShowDialog();
            relationshipDialog.Dispose();
        }

        /// <Summary>
        /// Pop up the battle plans dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void BattlePlansMenuItem(object sender, EventArgs e)
        {
            BattlePlans battlePlans = new BattlePlans();
            battlePlans.ShowDialog();
            battlePlans.Dispose();
        }

        /// <Summary>
        /// Pop up the Design Manager Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignManagerMenuItem_Click(object sender, EventArgs e)
        {
            DesignManager designManager = new DesignManager();
            designManager.RefreshStarMapEvent += new RefreshStarMap(this.MapControl.RefreshStarMap);
            designManager.ShowDialog();
            designManager.Dispose();
        }

        /// <Summary>
        /// Pop up the Planet Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlanetReportMenu_Click(object sender, EventArgs e)
        {
            PlanetReport planetReport = new PlanetReport();
            planetReport.ShowDialog();
            planetReport.Dispose();
        }

        /// <Summary>
        /// Pop up the Fleet Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void FleetReportMenu_Click(object sender, EventArgs e)
        {
            FleetReport fleetReport = new FleetReport();
            fleetReport.ShowDialog();
            fleetReport.Dispose();
        }

        /// <Summary>
        /// Pop up the battle Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void BattlesReportMenu_Click(object sender, EventArgs e)
        {
            BattleReportDialog battleReport = new BattleReportDialog();
            battleReport.ShowDialog();
            battleReport.Dispose();
        }

        /// <Summary>
        /// Pop up the score report dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ScoresMenuItem_Click(object sender, EventArgs e)
        {
            ScoreReport scoreReport = new ScoreReport();
            scoreReport.ShowDialog();
            scoreReport.Dispose();
        }

        /// <Summary>
        /// Menu->Commands->Save & Submit
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void SaveAndSubmitTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientState.Save();
            OrderWriter.WriteOrders();
            Application.Exit();
        }

        /// <Summary>
        /// Load Next Turn
        /// </Summary>
        /// <remarks>
        /// This menu Item has been disabled as it does not currently detect if there is
        /// a valid next turn.
        /// TODO (priority 6) - detect when a new turn is available.
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void LoadNextTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prepare the arguments that will tell how to re-initialise.
            CommandArguments commandArguments = new CommandArguments();
            commandArguments.Add(CommandArguments.Option.RaceName, ClientState.Data.RaceName);
            commandArguments.Add(CommandArguments.Option.Turn, ClientState.Data.TurnYear + 1);

            ClientState.Initialize(commandArguments.ToArray());
            this.NextTurn();
        }
        
        /// <Summary>
        /// Reacts to Fleet selection information. 
        /// </Summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>The source of the event.</param>
        /// <param name="e">A <see cref="FleetSelectionArgs"/> that contains the event data.</param>
        public void FleetChangeSelection(object sender, FleetSelectionArgs e)
        {
            this.SelectionDetail.Value = e.Detail;
            this.SelectionSummary.Value = e.Summary;
        }
        
        /// <Summary>
        /// Reacts to Star selection information. 
        /// </Summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>The source of the event.</param>
        /// <param name="e">A <see cref="FleetSelectionArgs"/> that contains the event data.</param>
        public void StarChangeSelection(object sender, StarSelectionArgs e)
        {
            this.SelectionDetail.Value = e.Star;
            this.SelectionSummary.Value = e.Star;
        }

        #endregion
        
        /// <Summary>
        /// Load controls with any data we may have for them.
        /// </Summary>
        public void InitialiseControls()
        {
            this.Messages.Year = ClientState.Data.TurnYear;
            this.Messages.MessageList = ClientState.Data.Messages;

            this.CurrentTurn = ClientState.Data.TurnYear;
            this.CurrentRace = ClientState.Data.RaceName;

            this.MapControl.Initialise();

            // Select a Star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    this.MapControl.SetCursor((System.Drawing.Point)star.Position);
                    this.SelectionDetail.Value = star;
                    this.SelectionSummary.Value = star;
                    break;
                }
            }
        }

        /// <Summary>
        /// Refresh the display for a new turn.
        /// </Summary>
        public void NextTurn()
        {
            this.Messages.Year = ClientState.Data.TurnYear;
            this.Messages.MessageList = ClientState.Data.Messages;

            this.Invalidate(true);

            this.MapControl.Initialise();
            this.MapControl.Invalidate();

            // Select a Star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    this.MapControl.SetCursor((System.Drawing.Point)star.Position);
                    this.SelectionDetail.Value = star;
                    this.SelectionSummary.Value = star;
                    break;
                }
            }
        }

        /// <Summary>
        /// Makes the Planet Detail reflect new research budgets. 
        /// </Summary>
        /// <returns>
        /// A <see cref="System.Boolean"/> indicating if the planet Detail
        /// was updated or not.
        /// </returns>
        private bool UpdateResearchBudgets()
        {
            if (this.SelectionDetail.Control == this.SelectionDetail.PlanetDetail)
            {
                // Ugly hack so panel updates right away.
                this.SelectionDetail.Value = this.SelectionDetail.Value;
                return true;
            }
            
            return false;
        }
    }
}