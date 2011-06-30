using System.Windows.Forms;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{
    public partial class NovaGUI
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaGUI));
            
            this.groupBox2  = new System.Windows.Forms.GroupBox();
            this.mainMenu   = new System.Windows.Forms.MenuStrip();
            
            this.fileToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem         = new System.Windows.Forms.ToolStripMenuItem();
            this.designManagerMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.shipDesignerToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.researchToolStripMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.battlePlansMenu                = new System.Windows.Forms.ToolStripMenuItem();
            this.playerRelationslMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTurnToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNextTurnToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.planetReportMenu               = new System.Windows.Forms.ToolStripMenuItem();
            this.fleetReportMenu                = new System.Windows.Forms.ToolStripMenuItem();
            this.battlesReportMenu              = new System.Windows.Forms.ToolStripMenuItem();
            this.scoresMenuItem                 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem         = new System.Windows.Forms.ToolStripMenuItem();
            
            this.messages           = new Nova.WinForms.Gui.Messages();
            this.selectionDetail    = new Nova.WinForms.Gui.SelectionDetail(stateData.EmpireIntel,
                                                                            stateData.DeletedFleets,
                                                                            stateData);
            this.selectionSummary   = new Nova.WinForms.Gui.SelectionSummary(stateData.EmpireIntel);
            this.mapControl         = new Nova.WinForms.Gui.StarMap();

            this.groupBox2.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.mapControl);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(374, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(611, 701);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Star Map";
            // 
            // mapControl
            // 
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.Location = new System.Drawing.Point(3, 16);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(605, 682);
            this.mapControl.TabIndex = 0;
            // 
            // mainMenu
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
            // battlePlansMenu
            // 
            this.battlePlansMenu.Name = "battlePlansMenu";
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
            // planetReportMenu
            // 
            this.planetReportMenu.Name = "planetReportMenu";
            this.planetReportMenu.Size = new System.Drawing.Size(155, 22);
            this.planetReportMenu.Text = "Player\'s &Planets";
            this.planetReportMenu.Click += new System.EventHandler(this.PlanetReportMenu_Click);
            // 
            // fleetReportMenu
            // 
            this.fleetReportMenu.Name = "fleetReportMenu";
            this.fleetReportMenu.Size = new System.Drawing.Size(155, 22);
            this.fleetReportMenu.Text = "Player\'s &Fleets";
            this.fleetReportMenu.Click += new System.EventHandler(this.FleetReportMenu_Click);
            // 
            // battlesReportMenu
            // 
            this.battlesReportMenu.Name = "battlesReportMenu";
            this.battlesReportMenu.Size = new System.Drawing.Size(155, 22);
            this.battlesReportMenu.Text = "&Battles";
            this.battlesReportMenu.Click += new System.EventHandler(this.BattlesReportMenu_Click);
            // 
            // scoresMenuItem
            // 
            this.scoresMenuItem.Name = "scoresMenuItem";
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
            // messages
            // 
            this.messages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.messages.Location = new System.Drawing.Point(8, 412);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(360, 116);
            this.messages.TabIndex = 18;
            this.messages.Year = Global.StartingYear;
            // 
            // selectionDetail
            // 
            this.selectionDetail.Location = new System.Drawing.Point(8, 24);
            this.selectionDetail.Margin = new System.Windows.Forms.Padding(0);
            this.selectionDetail.Name = "selectionDetail";
            this.selectionDetail.Size = new System.Drawing.Size(360, 406);
            this.selectionDetail.TabIndex = 21;
            this.selectionDetail.Value = null;
            // 
            // selectionSummary
            // 
            this.selectionSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectionSummary.Location = new System.Drawing.Point(8, 534);
            this.selectionSummary.Name = "selectionSummary";
            this.selectionSummary.Size = new System.Drawing.Size(360, 191);
            this.selectionSummary.TabIndex = 19;
            this.selectionSummary.Value = null;
            // 
            // NovaGUI
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(993, 732);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.selectionDetail);
            this.Controls.Add(this.selectionSummary);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(928, 770);
            this.Name = "NovaGUI";
            this.Text = "Nova - " + stateData.EmpireIntel.EmpireRace.PluralName;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NovaGUI_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.groupBox2.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private GroupBox groupBox2;

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
        private Messages messages;
        private SelectionSummary selectionSummary;
        private SelectionDetail selectionDetail;
        private StarMap mapControl;
    }
}