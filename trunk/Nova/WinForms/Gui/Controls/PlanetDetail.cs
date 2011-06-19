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
// Planet Detail display pane.
// ===========================================================================
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{
    /// <Summary>
    /// Planet Detail display pane.
    /// </Summary>
    public class PlanetDetail : System.Windows.Forms.UserControl
    {
        private Star star;
        
        /// <Summary>
        /// This event should be fired when the selected Star
        /// changes.
        /// </Summary>
        public event StarSelectionChanged StarSelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired in addition to
        /// StarSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </Summary>
        public event CursorChanged CursorChangedEvent;

        #region Designer generated variables
        private Label mines;
        private Label label3;
        private Label label5;
        private ListView productionQueue;
        private ColumnHeader description;
        private ColumnHeader quantity;
        private Button changeProductionQueue;
        private GroupBox groupBox1;
        private Label population;
        private Label label6;
        private Label factories;
        private Label label4;
        private Label defenseCoverage;
        private ResourcesOnHandDisplay resourceDisplay;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label scannerRange;
        private Label scannerType;
        private Label defenses;
        private Label defenseType;
        private Label label8;
        private Label label7;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox starbasePanel;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label massDriverDestination;
        private Label massDriverType;
        private Label starbaseCapacity;
        private Label starbaseDamage;
        private Label starbaseShields;
        private Label starbaseArmor;
        private Button targetButton;
        private Label label11;
        private Label label26;
        private Label label27;
        private Label label12;
        private Label label21;
        private Button nextPlanet;
        private Button previousPlanet;
        private GroupBox groupBox6;
        private Panel panel1;
        private System.ComponentModel.Container components = null;
        #endregion

        #region Construction and Disposal

        /// <Summary>
        /// Initializes a new instance of the PlanetDetail class.
        /// </Summary>
        public PlanetDetail()
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

        #region Component Designer generated code
        /// <Summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </Summary>
        private void InitializeComponent()
        {
            this.mines = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productionQueue = new System.Windows.Forms.ListView();
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.changeProductionQueue = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.population = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.factories = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.defenseCoverage = new System.Windows.Forms.Label();
            this.resourceDisplay = new Nova.ControlLibrary.ResourcesOnHandDisplay();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.scannerRange = new System.Windows.Forms.Label();
            this.scannerType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defenseType = new System.Windows.Forms.Label();
            this.defenses = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.starbasePanel = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.targetButton = new System.Windows.Forms.Button();
            this.starbaseCapacity = new System.Windows.Forms.Label();
            this.massDriverDestination = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.starbaseDamage = new System.Windows.Forms.Label();
            this.massDriverType = new System.Windows.Forms.Label();
            this.starbaseShields = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.starbaseArmor = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.nextPlanet = new System.Windows.Forms.Button();
            this.previousPlanet = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.starbasePanel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // mines
            // 
            this.mines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mines.Location = new System.Drawing.Point(67, 35);
            this.mines.Name = "mines";
            this.mines.Size = new System.Drawing.Size(89, 16);
            this.mines.TabIndex = 14;
            this.mines.Text = "0 of 0";
            this.mines.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Population";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mines";
            // 
            // productionQueue
            // 
            this.productionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.productionQueue.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.productionQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productionQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.description,
            this.quantity});
            this.productionQueue.Location = new System.Drawing.Point(6, 19);
            this.productionQueue.MultiSelect = false;
            this.productionQueue.Name = "productionQueue";
            this.productionQueue.Scrollable = false;
            this.productionQueue.Size = new System.Drawing.Size(166, 112);
            this.productionQueue.TabIndex = 13;
            this.productionQueue.UseCompatibleStateImageBehavior = false;
            this.productionQueue.View = System.Windows.Forms.View.Details;
            // 
            // description
            // 
            this.description.Text = "Description";
            this.description.Width = 92;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            this.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.quantity.Width = 59;
            // 
            // changeProductionQueue
            // 
            this.changeProductionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeProductionQueue.Location = new System.Drawing.Point(5, 137);
            this.changeProductionQueue.Name = "changeProductionQueue";
            this.changeProductionQueue.Size = new System.Drawing.Size(75, 23);
            this.changeProductionQueue.TabIndex = 14;
            this.changeProductionQueue.Text = "Change";
            this.changeProductionQueue.UseVisualStyleBackColor = true;
            this.changeProductionQueue.Click += new System.EventHandler(this.ChangeProductionQueue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.changeProductionQueue);
            this.groupBox1.Controls.Add(this.productionQueue);
            this.groupBox1.Location = new System.Drawing.Point(172, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 166);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Production Queue";
            // 
            // population
            // 
            this.population.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.population.Location = new System.Drawing.Point(78, 84);
            this.population.Name = "population";
            this.population.Size = new System.Drawing.Size(64, 16);
            this.population.TabIndex = 12;
            this.population.Text = "0";
            this.population.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Factories";
            // 
            // factories
            // 
            this.factories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.factories.Location = new System.Drawing.Point(64, 19);
            this.factories.Name = "factories";
            this.factories.Size = new System.Drawing.Size(92, 16);
            this.factories.TabIndex = 17;
            this.factories.Text = "0 of 0";
            this.factories.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Defense Coverage (%)";
            // 
            // defenseCoverage
            // 
            this.defenseCoverage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenseCoverage.Location = new System.Drawing.Point(128, 18);
            this.defenseCoverage.Name = "defenseCoverage";
            this.defenseCoverage.Size = new System.Drawing.Size(28, 16);
            this.defenseCoverage.TabIndex = 24;
            this.defenseCoverage.Text = "0";
            this.defenseCoverage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // resourceDisplay
            // 
            this.resourceDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceDisplay.Location = new System.Drawing.Point(6, 19);
            this.resourceDisplay.Name = "resourceDisplay";
            this.resourceDisplay.Size = new System.Drawing.Size(150, 68);
            this.resourceDisplay.TabIndex = 20;
            this.resourceDisplay.Value = null;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.population);
            this.groupBox2.Controls.Add(this.resourceDisplay);
            this.groupBox2.Location = new System.Drawing.Point(4, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 107);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resources on Hand";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.scannerRange);
            this.groupBox3.Controls.Add(this.scannerType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(4, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 56);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scanner";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(142, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "ly";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scannerRange
            // 
            this.scannerRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerRange.Location = new System.Drawing.Point(94, 36);
            this.scannerRange.Name = "scannerRange";
            this.scannerRange.Size = new System.Drawing.Size(47, 16);
            this.scannerRange.TabIndex = 32;
            this.scannerRange.Text = "0";
            this.scannerRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scannerType
            // 
            this.scannerType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerType.Location = new System.Drawing.Point(90, 16);
            this.scannerType.Name = "scannerType";
            this.scannerType.Size = new System.Drawing.Size(68, 16);
            this.scannerType.TabIndex = 31;
            this.scannerType.Text = "None";
            this.scannerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Scanner Type";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Scanner Range";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Defenses";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Defense Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // defenseType
            // 
            this.defenseType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenseType.Location = new System.Drawing.Point(93, 53);
            this.defenseType.Name = "defenseType";
            this.defenseType.Size = new System.Drawing.Size(65, 13);
            this.defenseType.TabIndex = 29;
            this.defenseType.Text = "None";
            this.defenseType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // defenses
            // 
            this.defenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenses.Location = new System.Drawing.Point(128, 35);
            this.defenses.Name = "defenses";
            this.defenses.Size = new System.Drawing.Size(28, 16);
            this.defenses.TabIndex = 30;
            this.defenses.Text = "0";
            this.defenses.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.defenseCoverage);
            this.groupBox4.Controls.Add(this.defenseType);
            this.groupBox4.Controls.Add(this.defenses);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(4, 237);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 72);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Defenses";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.mines);
            this.groupBox5.Controls.Add(this.factories);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(4, 178);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(162, 53);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Production";
            // 
            // starbasePanel
            // 
            this.starbasePanel.Controls.Add(this.panel1);
            this.starbasePanel.Controls.Add(this.label21);
            this.starbasePanel.Controls.Add(this.label26);
            this.starbasePanel.Controls.Add(this.label27);
            this.starbasePanel.Controls.Add(this.label12);
            this.starbasePanel.Controls.Add(this.targetButton);
            this.starbasePanel.Controls.Add(this.starbaseCapacity);
            this.starbasePanel.Controls.Add(this.massDriverDestination);
            this.starbasePanel.Controls.Add(this.label18);
            this.starbasePanel.Controls.Add(this.starbaseDamage);
            this.starbasePanel.Controls.Add(this.massDriverType);
            this.starbasePanel.Controls.Add(this.starbaseShields);
            this.starbasePanel.Controls.Add(this.label17);
            this.starbasePanel.Controls.Add(this.starbaseArmor);
            this.starbasePanel.Controls.Add(this.label16);
            this.starbasePanel.Controls.Add(this.label15);
            this.starbasePanel.Controls.Add(this.label14);
            this.starbasePanel.Controls.Add(this.label13);
            this.starbasePanel.Location = new System.Drawing.Point(172, 176);
            this.starbasePanel.Name = "starbasePanel";
            this.starbasePanel.Size = new System.Drawing.Size(178, 168);
            this.starbasePanel.TabIndex = 31;
            this.starbasePanel.TabStop = false;
            this.starbasePanel.Text = "Starbase";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(156, 69);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 13);
            this.label21.TabIndex = 37;
            this.label21.Text = "kT";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(158, 49);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 13);
            this.label26.TabIndex = 35;
            this.label26.Text = "%";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(158, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(19, 13);
            this.label27.TabIndex = 36;
            this.label27.Text = "dp";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(158, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "dp";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetButton
            // 
            this.targetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.targetButton.Enabled = false;
            this.targetButton.Location = new System.Drawing.Point(6, 137);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(75, 23);
            this.targetButton.TabIndex = 15;
            this.targetButton.Text = "Target";
            this.targetButton.UseVisualStyleBackColor = true;
            // 
            // starbaseCapacity
            // 
            this.starbaseCapacity.Location = new System.Drawing.Point(83, 71);
            this.starbaseCapacity.Name = "starbaseCapacity";
            this.starbaseCapacity.Size = new System.Drawing.Size(62, 13);
            this.starbaseCapacity.TabIndex = 9;
            this.starbaseCapacity.Text = "0";
            this.starbaseCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // massDriverDestination
            // 
            this.massDriverDestination.Location = new System.Drawing.Point(99, 112);
            this.massDriverDestination.Name = "massDriverDestination";
            this.massDriverDestination.Size = new System.Drawing.Size(72, 13);
            this.massDriverDestination.TabIndex = 11;
            this.massDriverDestination.Text = "None";
            this.massDriverDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Destination";
            // 
            // starbaseDamage
            // 
            this.starbaseDamage.Location = new System.Drawing.Point(74, 52);
            this.starbaseDamage.Name = "starbaseDamage";
            this.starbaseDamage.Size = new System.Drawing.Size(71, 13);
            this.starbaseDamage.TabIndex = 8;
            this.starbaseDamage.Text = "0";
            this.starbaseDamage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // massDriverType
            // 
            this.massDriverType.Location = new System.Drawing.Point(92, 96);
            this.massDriverType.Name = "massDriverType";
            this.massDriverType.Size = new System.Drawing.Size(79, 13);
            this.massDriverType.TabIndex = 10;
            this.massDriverType.Tag = "";
            this.massDriverType.Text = "None";
            this.massDriverType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // starbaseShields
            // 
            this.starbaseShields.Location = new System.Drawing.Point(80, 34);
            this.starbaseShields.Name = "starbaseShields";
            this.starbaseShields.Size = new System.Drawing.Size(65, 13);
            this.starbaseShields.TabIndex = 7;
            this.starbaseShields.Text = "0";
            this.starbaseShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Mass Driver";
            // 
            // starbaseArmor
            // 
            this.starbaseArmor.Location = new System.Drawing.Point(87, 16);
            this.starbaseArmor.Name = "starbaseArmor";
            this.starbaseArmor.Size = new System.Drawing.Size(58, 13);
            this.starbaseArmor.TabIndex = 6;
            this.starbaseArmor.Text = "0";
            this.starbaseArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Dock Capacity";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Damage";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Shields";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Armor";
            // 
            // nextPlanet
            // 
            this.nextPlanet.Enabled = false;
            this.nextPlanet.Location = new System.Drawing.Point(9, 19);
            this.nextPlanet.Name = "nextPlanet";
            this.nextPlanet.Size = new System.Drawing.Size(59, 23);
            this.nextPlanet.TabIndex = 32;
            this.nextPlanet.Text = "Next";
            this.nextPlanet.UseVisualStyleBackColor = true;
            this.nextPlanet.Click += new System.EventHandler(this.NextPlanet_Click);
            // 
            // previousPlanet
            // 
            this.previousPlanet.Enabled = false;
            this.previousPlanet.Location = new System.Drawing.Point(93, 19);
            this.previousPlanet.Name = "previousPlanet";
            this.previousPlanet.Size = new System.Drawing.Size(59, 23);
            this.previousPlanet.TabIndex = 33;
            this.previousPlanet.Text = "Previous";
            this.previousPlanet.UseVisualStyleBackColor = true;
            this.previousPlanet.Click += new System.EventHandler(this.PreviousPlanet_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.previousPlanet);
            this.groupBox6.Controls.Add(this.nextPlanet);
            this.groupBox6.Location = new System.Drawing.Point(4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(162, 55);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Planet Selection";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(7, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 1);
            this.panel1.TabIndex = 38;
            // 
            // PlanetDetail
            // 
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.starbasePanel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PlanetDetail";
            this.Size = new System.Drawing.Size(356, 380);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.starbasePanel.ResumeLayout(false);
            this.starbasePanel.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// The change queue button has been pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ChangeProductionQueue_Click(object sender, EventArgs e)
        {
            ProductionDialog productionDialog = new ProductionDialog(star);

            productionDialog.ShowDialog();
            productionDialog.Dispose();
            
            this.UpdateFields();

            QueueList.Populate(this.productionQueue, star.ManufacturingQueue);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Next planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                this.previousPlanet.Enabled = false;
                this.nextPlanet.Enabled = false;
                return;
            }

            this.previousPlanet.Enabled = true;
            this.nextPlanet.Enabled = true;

            star = myStars.GetNext(star);

            StarSelectionArgs selectionArgs = new StarSelectionArgs(star);
            CursorArgs cursorArgs = new CursorArgs((Point)star.Position);
            
            // Inform of the selection change to all listening objects.
            StarSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Previous planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                this.previousPlanet.Enabled = false;
                this.nextPlanet.Enabled = false;
                return;
            }

            this.previousPlanet.Enabled = true;
            this.nextPlanet.Enabled = true;

            star = myStars.GetPrevious(star);
            
            StarSelectionArgs selectionArgs = new StarSelectionArgs(star);
            CursorArgs cursorArgs = new CursorArgs((Point)star.Position);
            
            // Inform of the selection change to all listening objects.
            StarSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set the Star which is to be displayed
        /// </Summary>
        /// <param name="selectedStar">The Star to be displayed</param>
        /// ----------------------------------------------------------------------------
        private void SetStarDetails(Star selectedStar)
        {
            star = selectedStar;

            UpdateFields();

            if (ClientState.Data.PlayerStars.Count > 1)
            {
                this.previousPlanet.Enabled = true;
                this.nextPlanet.Enabled = true;
            }
            else
            {
                this.previousPlanet.Enabled = false;
                this.previousPlanet.Enabled = false;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update all the fields in the planet Detail display.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void UpdateFields()
        {
            if (star == null)
            {
                return;
            }

            QueueList.Populate(this.productionQueue, star.ManufacturingQueue);

            Nova.Common.Defenses.ComputeDefenseCoverage(star);

            this.defenseType.Text = star.DefenseType;
            this.defenses.Text = star.Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.defenseCoverage.Text = Nova.Common.Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            this.factories.Text = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                    + " of " + star.GetOperableFactories().ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.mines.Text = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    + " of " + star.GetOperableMines().ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.population.Text = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            this.resourceDisplay.ResourceRate = star.GetResourceRate();
            
            if (star.OnlyLeftover == false)
            {
                this.resourceDisplay.ResearchPercentage = ClientState.Data.ResearchBudget;
            }
            else
            {
                // We treat Stars contributing only leftover resources as having
                // a 0% budget allocation.
                this.resourceDisplay.ResearchPercentage = 0;
            }
            this.resourceDisplay.Value = star.ResourcesOnHand;

            this.scannerRange.Text = star.ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.scannerType.Text = star.ScannerType;

            if (star.Starbase == null)
            {
                this.starbasePanel.Text = "No Starbase";
                this.starbasePanel.Enabled = false;
                return;
            }

            Fleet starbase = star.Starbase;
            this.starbaseArmor.Text = starbase.TotalArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.starbaseCapacity.Text = starbase.TotalDockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.starbaseDamage.Text = "0";
            this.starbasePanel.Enabled = true;
            this.starbasePanel.Text = starbase.Name;
            this.starbaseShields.Text = starbase.TotalShieldStrength.ToString();

            this.massDriverType.Text = "None";
            this.massDriverDestination.Text = "None";
            this.targetButton.Enabled = false;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Access to the Star whose details are displayed in the panel.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Star Value
        {
            set { SetStarDetails(value); }
            get { return star; }
        }

        #endregion
    }


    
}
