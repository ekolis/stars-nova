#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
    using System.ComponentModel;
    using System.Windows.Forms;
    
    public partial class NovaConsole
    {
        /// <Summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </Summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaConsole));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.playerList = new System.Windows.Forms.ListView();
            this.ai = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.raceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.turnIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.runAiCheckBox = new System.Windows.Forms.CheckBox();
            this.autoGenerateCheckBox = new System.Windows.Forms.CheckBox();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.folderPath = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.turnYearLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTurnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guiLaunchLabel = new System.Windows.Forms.Label();
            this.consoleTimer = new System.Windows.Forms.Timer(this.components);
            this.playerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.playerList);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 254);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Players";
            // 
            // playerList
            // 
            this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ai,
            this.raceName,
            this.turnIn});
            this.playerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerList.FullRowSelect = true;
            this.playerList.Location = new System.Drawing.Point(3, 16);
            this.playerList.MultiSelect = false;
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(444, 235);
            this.playerList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.playerList.TabIndex = 0;
            this.playerList.UseCompatibleStateImageBehavior = false;
            this.playerList.View = System.Windows.Forms.View.Details;
            this.playerList.DoubleClick += new System.EventHandler(this.PlayerList_DoubleClick);
            this.playerList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayerList_MouseClick);
            // 
            // ai
            // 
            this.ai.Text = "AI";
            this.ai.Width = 112;
            // 
            // raceName
            // 
            this.raceName.Text = "Race Name";
            this.raceName.Width = 209;
            // 
            // turnIn
            // 
            this.turnIn.Text = "Last Turned In";
            this.turnIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.turnIn.Width = 108;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.runAiCheckBox);
            this.groupBox2.Controls.Add(this.autoGenerateCheckBox);
            this.groupBox2.Controls.Add(this.statusBox);
            this.groupBox2.Controls.Add(this.folderPath);
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
            this.runAiCheckBox.CheckedChanged += new System.EventHandler(this.RunAiCheckBox_CheckedChanged);
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
            // statusBox
            // 
            this.statusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBox.Location = new System.Drawing.Point(9, 86);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.statusBox.Size = new System.Drawing.Size(431, 69);
            this.statusBox.TabIndex = 9;
            // 
            // folderPath
            // 
            this.folderPath.AutoSize = true;
            this.folderPath.Location = new System.Drawing.Point(76, 19);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(76, 13);
            this.folderPath.TabIndex = 6;
            this.folderPath.Text = "None selected";
            this.folderPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.turnToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(472, 24);
            this.mainMenu.TabIndex = 6;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenuItem,
            this.openGameToolStripMenuItem,
            this.selectNewFolderMenuItem,
            this.toolStripSeparator1,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newGameMenuItem
            // 
            this.newGameMenuItem.Name = "newGameMenuItem";
            this.newGameMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newGameMenuItem.Text = "&New Game";
            this.newGameMenuItem.Click += new System.EventHandler(this.NewGameMenuItem_Click);
            // 
            // openGameToolStripMenuItem
            // 
            this.openGameToolStripMenuItem.Name = "openGameToolStripMenuItem";
            this.openGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openGameToolStripMenuItem.Text = "&Open Game";
            this.openGameToolStripMenuItem.Click += new System.EventHandler(this.OpenGameToolStripMenuItem_Click);
            // 
            // selectNewFolderMenuItem
            // 
            this.selectNewFolderMenuItem.Name = "selectNewFolderMenuItem";
            this.selectNewFolderMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectNewFolderMenuItem.Text = "&Select New Folder";
            this.selectNewFolderMenuItem.Click += new System.EventHandler(this.SelectNewFolder);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // turnToolStripMenuItem
            // 
            this.turnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateTurnMenuItem,
            this.forceMenuItem,
            this.toolStripSeparator2,
            this.refreshMenuItem});
            this.turnToolStripMenuItem.Name = "turnToolStripMenuItem";
            this.turnToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.turnToolStripMenuItem.Text = "&Turn";
            // 
            // generateTurnMenuItem
            // 
            this.generateTurnMenuItem.Name = "generateTurnMenuItem";
            this.generateTurnMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.generateTurnMenuItem.Size = new System.Drawing.Size(183, 22);
            this.generateTurnMenuItem.Text = "&Generate";
            this.generateTurnMenuItem.Click += new System.EventHandler(this.GenerateTurnMenuItem_Click);
            // 
            // forceMenuItem
            // 
            this.forceMenuItem.Name = "forceMenuItem";
            this.forceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.forceMenuItem.Size = new System.Drawing.Size(183, 22);
            this.forceMenuItem.Text = "&Force Next Turn";
            this.forceMenuItem.Click += new System.EventHandler(this.ForceMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshMenuItem.Size = new System.Drawing.Size(183, 22);
            this.refreshMenuItem.Text = "&Refresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
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
            // guiLaunchLabel
            // 
            this.guiLaunchLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.guiLaunchLabel.AutoSize = true;
            this.guiLaunchLabel.Location = new System.Drawing.Point(154, 26);
            this.guiLaunchLabel.Name = "guiLaunchLabel";
            this.guiLaunchLabel.Size = new System.Drawing.Size(136, 13);
            this.guiLaunchLabel.TabIndex = 9;
            this.guiLaunchLabel.Text = "Double Click to Play a Turn";
            // 
            // consoleTimer
            // 
            this.consoleTimer.Enabled = true;
            this.consoleTimer.Interval = 5000;
            this.consoleTimer.Tick += new System.EventHandler(this.ConsoleTimer_Tick);
            // 
            // playerMenu
            // 
            this.playerMenu.Name = "playerMenu";
            this.playerMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // NovaConsole
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(472, 474);
            this.Controls.Add(this.guiLaunchLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "NovaConsole";
            this.Text = "Stars! Nova - Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleFormClosing);
            this.Shown += new System.EventHandler(this.OnFirstShow);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
            
            private GroupBox groupBox1;
            private GroupBox groupBox2;
            private MenuStrip mainMenu;
            private ToolStripMenuItem fileToolStripMenuItem;
            private ToolStripMenuItem exitMenuItem;
            private ToolStripMenuItem helpToolStripMenuItem;
            private ToolStripMenuItem aboutToolStripMenuItem;
            private ToolStripMenuItem selectNewFolderMenuItem;
            private Label folderPath;
            private Label label1;
            private ListView playerList;
            private ColumnHeader raceName;
            private ColumnHeader turnIn;
            private ToolStripMenuItem newGameMenuItem;
            private ToolStripSeparator toolStripSeparator1;
            private ToolStripMenuItem turnToolStripMenuItem;
            private ToolStripMenuItem generateTurnMenuItem;
            private ToolStripMenuItem forceMenuItem;
            private ToolStripSeparator toolStripSeparator2;
            private ToolStripMenuItem refreshMenuItem;
            private ColumnHeader ai;
            private Label turnYearLabel;
            private Label yearLabel;
            private Label guiLaunchLabel;
            private ToolStripMenuItem openGameToolStripMenuItem;
            private TextBox statusBox;
            private CheckBox autoGenerateCheckBox;
            private CheckBox runAiCheckBox;
            private Timer consoleTimer;
            private ContextMenuStrip playerMenu;
            private IContainer components;
            
    }
}
