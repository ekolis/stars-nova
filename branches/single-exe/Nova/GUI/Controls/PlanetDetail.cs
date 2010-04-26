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
// Planet detail display pane.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nova.Gui.Dialogs;
using NovaCommon;
using ControlLibrary;
using NovaClient;

namespace Nova.Gui.Controls
{

    /// <summary>
    /// Planet detail display pane.
    /// </summary>
    public class PlanetDetail : System.Windows.Forms.UserControl
    {
        private Star star = null;


        #region Designer generated variables
        private Label Mines;
        private Label label3;
        private Label label5;
        private ListView ProductionQueue;
        private ColumnHeader Description;
        private ColumnHeader Quantity;
        private Button ChangeProductionQueue;
        private GroupBox groupBox1;
        private Label Population;
        private Label label6;
        private Label Factories;
        private Label label4;
        private Label DefenseCoverage;
        private ResourceDisplay ResourceDisplay;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label ScannerRange;
        private Label ScannerType;
        private Label Defenses;
        private Label DefenseType;
        private Label label8;
        private Label label7;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox StarbasePanel;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label MassDriverDestination;
        private Label MassDriverType;
        private Label StarbaseCapacity;
        private Label StarbaseDamage;
        private Label StarbaseShields;
        private Label StarbaseArmor;
        private Button TargetButton;
        private Label label25;
        private Label label11;
        private Label label26;
        private Label label27;
        private Label label12;
        private Label label21;
        private Button NextPlanet;
        private Button PreviousPlanet;
        private GroupBox groupBox6;
        private System.ComponentModel.Container components = null;
        #endregion

        #region Construction and Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public PlanetDetail()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanetDetail));
            this.Mines = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ProductionQueue = new System.Windows.Forms.ListView();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.Quantity = new System.Windows.Forms.ColumnHeader();
            this.ChangeProductionQueue = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Population = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Factories = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DefenseCoverage = new System.Windows.Forms.Label();
            this.ResourceDisplay = new ControlLibrary.ResourceDisplay();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ScannerRange = new System.Windows.Forms.Label();
            this.ScannerType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DefenseType = new System.Windows.Forms.Label();
            this.Defenses = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StarbasePanel = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.TargetButton = new System.Windows.Forms.Button();
            this.StarbaseCapacity = new System.Windows.Forms.Label();
            this.MassDriverDestination = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.StarbaseDamage = new System.Windows.Forms.Label();
            this.MassDriverType = new System.Windows.Forms.Label();
            this.StarbaseShields = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.StarbaseArmor = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NextPlanet = new System.Windows.Forms.Button();
            this.PreviousPlanet = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.StarbasePanel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Mines
            // 
            this.Mines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Mines.Location = new System.Drawing.Point(92, 35);
            this.Mines.Name = "Mines";
            this.Mines.Size = new System.Drawing.Size(64, 16);
            this.Mines.TabIndex = 14;
            this.Mines.Text = "0 of 0";
            this.Mines.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // ProductionQueue
            // 
            this.ProductionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductionQueue.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProductionQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductionQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Description,
            this.Quantity});
            this.ProductionQueue.Location = new System.Drawing.Point(6, 19);
            this.ProductionQueue.MultiSelect = false;
            this.ProductionQueue.Name = "ProductionQueue";
            this.ProductionQueue.Scrollable = false;
            this.ProductionQueue.Size = new System.Drawing.Size(166, 112);
            this.ProductionQueue.TabIndex = 13;
            this.ProductionQueue.UseCompatibleStateImageBehavior = false;
            this.ProductionQueue.View = System.Windows.Forms.View.Details;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 92;
            // 
            // Quantity
            // 
            this.Quantity.Text = "Quantity";
            this.Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Quantity.Width = 59;
            // 
            // ChangeProductionQueue
            // 
            this.ChangeProductionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChangeProductionQueue.Location = new System.Drawing.Point(5, 137);
            this.ChangeProductionQueue.Name = "ChangeProductionQueue";
            this.ChangeProductionQueue.Size = new System.Drawing.Size(75, 23);
            this.ChangeProductionQueue.TabIndex = 14;
            this.ChangeProductionQueue.Text = "Change";
            this.ChangeProductionQueue.UseVisualStyleBackColor = true;
            this.ChangeProductionQueue.Click += new System.EventHandler(this.ChangeProductionQueue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ChangeProductionQueue);
            this.groupBox1.Controls.Add(this.ProductionQueue);
            this.groupBox1.Location = new System.Drawing.Point(172, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 166);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Production Queue";
            // 
            // Population
            // 
            this.Population.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Population.Location = new System.Drawing.Point(78, 84);
            this.Population.Name = "Population";
            this.Population.Size = new System.Drawing.Size(64, 16);
            this.Population.TabIndex = 12;
            this.Population.Text = "0";
            this.Population.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Factories";
            // 
            // Factories
            // 
            this.Factories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Factories.Location = new System.Drawing.Point(92, 19);
            this.Factories.Name = "Factories";
            this.Factories.Size = new System.Drawing.Size(64, 16);
            this.Factories.TabIndex = 17;
            this.Factories.Text = "0 of 0";
            this.Factories.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Defense Coverage (%)";
            // 
            // DefenseCoverage
            // 
            this.DefenseCoverage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefenseCoverage.Location = new System.Drawing.Point(128, 18);
            this.DefenseCoverage.Name = "DefenseCoverage";
            this.DefenseCoverage.Size = new System.Drawing.Size(28, 16);
            this.DefenseCoverage.TabIndex = 24;
            this.DefenseCoverage.Text = "0";
            this.DefenseCoverage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ResourceDisplay
            // 
            this.ResourceDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceDisplay.Location = new System.Drawing.Point(6, 19);
            this.ResourceDisplay.Name = "ResourceDisplay";
            this.ResourceDisplay.Size = new System.Drawing.Size(150, 68);
            this.ResourceDisplay.TabIndex = 20;
            this.ResourceDisplay.Value = new NovaCommon.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Population);
            this.groupBox2.Controls.Add(this.ResourceDisplay);
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
            this.groupBox3.Controls.Add(this.ScannerRange);
            this.groupBox3.Controls.Add(this.ScannerType);
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
            // ScannerRange
            // 
            this.ScannerRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScannerRange.Location = new System.Drawing.Point(94, 36);
            this.ScannerRange.Name = "ScannerRange";
            this.ScannerRange.Size = new System.Drawing.Size(47, 16);
            this.ScannerRange.TabIndex = 32;
            this.ScannerRange.Text = "0";
            this.ScannerRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScannerType
            // 
            this.ScannerType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScannerType.Location = new System.Drawing.Point(90, 16);
            this.ScannerType.Name = "ScannerType";
            this.ScannerType.Size = new System.Drawing.Size(68, 16);
            this.ScannerType.TabIndex = 31;
            this.ScannerType.Text = "None";
            this.ScannerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Defenses";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Defense Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DefenseType
            // 
            this.DefenseType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefenseType.Location = new System.Drawing.Point(93, 53);
            this.DefenseType.Name = "DefenseType";
            this.DefenseType.Size = new System.Drawing.Size(65, 13);
            this.DefenseType.TabIndex = 29;
            this.DefenseType.Text = "None";
            this.DefenseType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Defenses
            // 
            this.Defenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Defenses.Location = new System.Drawing.Point(128, 35);
            this.Defenses.Name = "Defenses";
            this.Defenses.Size = new System.Drawing.Size(28, 16);
            this.Defenses.TabIndex = 30;
            this.Defenses.Text = "0";
            this.Defenses.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.DefenseCoverage);
            this.groupBox4.Controls.Add(this.DefenseType);
            this.groupBox4.Controls.Add(this.Defenses);
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
            this.groupBox5.Controls.Add(this.Mines);
            this.groupBox5.Controls.Add(this.Factories);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(4, 178);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(162, 53);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Production";
            // 
            // StarbasePanel
            // 
            this.StarbasePanel.Controls.Add(this.label21);
            this.StarbasePanel.Controls.Add(this.label26);
            this.StarbasePanel.Controls.Add(this.label27);
            this.StarbasePanel.Controls.Add(this.label12);
            this.StarbasePanel.Controls.Add(this.label25);
            this.StarbasePanel.Controls.Add(this.TargetButton);
            this.StarbasePanel.Controls.Add(this.StarbaseCapacity);
            this.StarbasePanel.Controls.Add(this.MassDriverDestination);
            this.StarbasePanel.Controls.Add(this.label18);
            this.StarbasePanel.Controls.Add(this.StarbaseDamage);
            this.StarbasePanel.Controls.Add(this.MassDriverType);
            this.StarbasePanel.Controls.Add(this.StarbaseShields);
            this.StarbasePanel.Controls.Add(this.label17);
            this.StarbasePanel.Controls.Add(this.StarbaseArmor);
            this.StarbasePanel.Controls.Add(this.label16);
            this.StarbasePanel.Controls.Add(this.label15);
            this.StarbasePanel.Controls.Add(this.label14);
            this.StarbasePanel.Controls.Add(this.label13);
            this.StarbasePanel.Location = new System.Drawing.Point(172, 176);
            this.StarbasePanel.Name = "StarbasePanel";
            this.StarbasePanel.Size = new System.Drawing.Size(178, 196);
            this.StarbasePanel.TabIndex = 31;
            this.StarbasePanel.TabStop = false;
            this.StarbasePanel.Text = "Starbase";
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
            // label25
            // 
            this.label25.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label25.Location = new System.Drawing.Point(3, 94);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(160, 23);
            this.label25.TabIndex = 16;
            this.label25.Text = "__________________________________";
            // 
            // TargetButton
            // 
            this.TargetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TargetButton.Enabled = false;
            this.TargetButton.Location = new System.Drawing.Point(7, 167);
            this.TargetButton.Name = "TargetButton";
            this.TargetButton.Size = new System.Drawing.Size(75, 23);
            this.TargetButton.TabIndex = 15;
            this.TargetButton.Text = "Target";
            this.TargetButton.UseVisualStyleBackColor = true;
            // 
            // StarbaseCapacity
            // 
            this.StarbaseCapacity.Location = new System.Drawing.Point(83, 71);
            this.StarbaseCapacity.Name = "StarbaseCapacity";
            this.StarbaseCapacity.Size = new System.Drawing.Size(62, 13);
            this.StarbaseCapacity.TabIndex = 9;
            this.StarbaseCapacity.Text = "0";
            this.StarbaseCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MassDriverDestination
            // 
            this.MassDriverDestination.Location = new System.Drawing.Point(83, 144);
            this.MassDriverDestination.Name = "MassDriverDestination";
            this.MassDriverDestination.Size = new System.Drawing.Size(72, 13);
            this.MassDriverDestination.TabIndex = 11;
            this.MassDriverDestination.Text = "None";
            this.MassDriverDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 144);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Destination";
            // 
            // StarbaseDamage
            // 
            this.StarbaseDamage.Location = new System.Drawing.Point(74, 52);
            this.StarbaseDamage.Name = "StarbaseDamage";
            this.StarbaseDamage.Size = new System.Drawing.Size(71, 13);
            this.StarbaseDamage.TabIndex = 8;
            this.StarbaseDamage.Text = "0";
            this.StarbaseDamage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MassDriverType
            // 
            this.MassDriverType.Location = new System.Drawing.Point(76, 126);
            this.MassDriverType.Name = "MassDriverType";
            this.MassDriverType.Size = new System.Drawing.Size(79, 13);
            this.MassDriverType.TabIndex = 10;
            this.MassDriverType.Tag = "";
            this.MassDriverType.Text = "None";
            this.MassDriverType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StarbaseShields
            // 
            this.StarbaseShields.Location = new System.Drawing.Point(80, 34);
            this.StarbaseShields.Name = "StarbaseShields";
            this.StarbaseShields.Size = new System.Drawing.Size(65, 13);
            this.StarbaseShields.TabIndex = 7;
            this.StarbaseShields.Text = "0";
            this.StarbaseShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Mass Driver";
            // 
            // StarbaseArmor
            // 
            this.StarbaseArmor.Location = new System.Drawing.Point(87, 16);
            this.StarbaseArmor.Name = "StarbaseArmor";
            this.StarbaseArmor.Size = new System.Drawing.Size(58, 13);
            this.StarbaseArmor.TabIndex = 6;
            this.StarbaseArmor.Text = "0";
            this.StarbaseArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Armor";
            // 
            // NextPlanet
            // 
            this.NextPlanet.Enabled = false;
            this.NextPlanet.Location = new System.Drawing.Point(9, 19);
            this.NextPlanet.Name = "NextPlanet";
            this.NextPlanet.Size = new System.Drawing.Size(59, 23);
            this.NextPlanet.TabIndex = 32;
            this.NextPlanet.Text = "Next";
            this.NextPlanet.UseVisualStyleBackColor = true;
            this.NextPlanet.Click += new System.EventHandler(this.NextPlanet_Click);
            // 
            // PreviousPlanet
            // 
            this.PreviousPlanet.Enabled = false;
            this.PreviousPlanet.Location = new System.Drawing.Point(93, 19);
            this.PreviousPlanet.Name = "PreviousPlanet";
            this.PreviousPlanet.Size = new System.Drawing.Size(59, 23);
            this.PreviousPlanet.TabIndex = 33;
            this.PreviousPlanet.Text = "Previous";
            this.PreviousPlanet.UseVisualStyleBackColor = true;
            this.PreviousPlanet.Click += new System.EventHandler(this.PreviousPlanet_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.PreviousPlanet);
            this.groupBox6.Controls.Add(this.NextPlanet);
            this.groupBox6.Location = new System.Drawing.Point(4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(162, 55);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Planet Selection";
            // 
            // PlanetDetail
            // 
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.StarbasePanel);
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
            this.StarbasePanel.ResumeLayout(false);
            this.StarbasePanel.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The change queue button has been pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ChangeProductionQueue_Click(object sender, EventArgs e)
        {
            ProductionDialog productionDialog = new ProductionDialog(star);

            productionDialog.ShowDialog();
            productionDialog.Dispose();

            QueueList.Populate(ProductionQueue, star.ManufacturingQueue);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Next planet button pressed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                PreviousPlanet.Enabled = false;
                NextPlanet.Enabled = false;
                return;
            }

            PreviousPlanet.Enabled = true;
            NextPlanet.Enabled = true;

            star = myStars.GetNext(star);

            MainWindow.nova.SelectionDetail.Value = star;
            MainWindow.nova.SelectionSummary.Value = star;
            MainWindow.nova.MapControl.SetCursor(star.Position);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Previous planet button pressed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                PreviousPlanet.Enabled = false;
                NextPlanet.Enabled = false;
                return;
            }

            PreviousPlanet.Enabled = true;
            NextPlanet.Enabled = true;

            star = myStars.GetPrevious(star);

            MainWindow.nova.SelectionDetail.Value = star;
            MainWindow.nova.SelectionSummary.Value = star;
            MainWindow.nova.MapControl.SetCursor(star.Position);
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the star which is to be displayed
        /// </summary>
        /// <param name="selectedStar">The star to be displayed</param>
        /// ----------------------------------------------------------------------------
        void SetStarDetails(Star selectedStar)
        {
            star = selectedStar;

            UpdateFields();

            if (ClientState.Data.PlayerStars.Count > 1)
            {
                PreviousPlanet.Enabled = true;
                NextPlanet.Enabled = true;
            }
            else
            {
                PreviousPlanet.Enabled = false;
                PreviousPlanet.Enabled = false;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update all the fields in the planet detail display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void UpdateFields()
        {
            if (star == null)
            {
                return;
            }

            QueueList.Populate(ProductionQueue, star.ManufacturingQueue);
            int resources = star.Colonists / 1000;
            string manned = " of " + resources.ToString(System.Globalization.CultureInfo.InvariantCulture);

            NovaCommon.Defenses.ComputeDefenseCoverage(star);

            DefenseType.Text = star.DefenseType;
            Defenses.Text = star.Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            DefenseCoverage.Text = NovaCommon.Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            Factories.Text = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture) + manned;
            Mines.Text = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture) + manned;
            Population.Text = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ResourceDisplay.Value = star.ResourcesOnHand;

            ScannerRange.Text = star.ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ScannerType.Text = star.ScannerType;

            if (star.Starbase == null)
            {
                StarbasePanel.Text = "No Starbase";
                StarbasePanel.Enabled = false;
                return;
            }

            Fleet starbase = star.Starbase;
            StarbaseArmor.Text = starbase.ArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            StarbaseCapacity.Text = starbase.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            StarbaseDamage.Text = "0";
            StarbasePanel.Enabled = true;
            StarbasePanel.Text = starbase.Name;
            StarbaseShields.Text = "0";

            MassDriverType.Text = "None";
            MassDriverDestination.Text = "None";
            TargetButton.Enabled = false;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Access to the star whose details are displayed in the panel.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Star Value
        {
            set { SetStarDetails(value); }
            get { return star; }
        }

        #endregion
    }
}
