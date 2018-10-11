#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>;.
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
// along with this program. If not, see <http://www.gnu.org/licenses/>;
// ===========================================================================
#endregion

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.ControlLibrary;

    /// <Summary>
    /// Ship Detail display panel.
    /// </Summary>
    public partial class FleetDetail
    {
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.warpText = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.legFuel = new System.Windows.Forms.Label();
            this.warpFactor = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.legDistance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.routeFuelUse = new System.Windows.Forms.Label();
            this.buttonCargo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.legTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.manageFleet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonGotoPlanet = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxRepeatOrders = new System.Windows.Forms.CheckBox();
            this.buttonWaypointTarget = new System.Windows.Forms.Button();
            this.contextMenuWaypointTargets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.blahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.fleetComposition = new System.Windows.Forms.ListView();
            this.typeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numberHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupFleetSelection = new System.Windows.Forms.GroupBox();
            this.previousFleet = new System.Windows.Forms.Button();
            this.nextFleet = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.buttonCargoXfer = new System.Windows.Forms.Button();
            this.buttonGotoFleet = new System.Windows.Forms.Button();
            this.comboOtherFleets = new System.Windows.Forms.ComboBox();
            this.groupOrbitPlanet = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.WaypointTasks = new System.Windows.Forms.ComboBox();
            this.meterCargoOther = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.meterFuelOther = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.wayPoints = new Nova.WinForms.Gui.WaypointListBox();
            this.meterCargo = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.meterFuel = new Nova.WinForms.Gui.Controls.CargoMeter();
            ((System.ComponentModel.ISupportInitialize)(this.warpFactor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuWaypointTargets.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupFleetSelection.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupOrbitPlanet.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // warpText
            // 
            this.warpText.Location = new System.Drawing.Point(9, 26);
            this.warpText.Name = "warpText";
            this.warpText.Size = new System.Drawing.Size(48, 16);
            this.warpText.TabIndex = 76;
            this.warpText.Text = "Warp 4";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 77;
            this.label8.Text = "Leg Fuel Use";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(143, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "mg";
            // 
            // legFuel
            // 
            this.legFuel.Location = new System.Drawing.Point(101, 149);
            this.legFuel.Name = "legFuel";
            this.legFuel.Size = new System.Drawing.Size(40, 16);
            this.legFuel.TabIndex = 79;
            this.legFuel.Text = "0";
            this.legFuel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // warpFactor
            // 
            this.warpFactor.AutoSize = false;
            this.warpFactor.Location = new System.Drawing.Point(53, 23);
            this.warpFactor.Name = "warpFactor";
            this.warpFactor.Size = new System.Drawing.Size(108, 26);
            this.warpFactor.TabIndex = 75;
            this.warpFactor.TabStop = false;
            this.warpFactor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.warpFactor.Value = 4;
            this.warpFactor.Scroll += new System.EventHandler(this.WaypointSpeedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 80;
            this.label1.Text = "Leg Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "ly";
            // 
            // legDistance
            // 
            this.legDistance.Location = new System.Drawing.Point(101, 133);
            this.legDistance.Name = "legDistance";
            this.legDistance.Size = new System.Drawing.Size(40, 16);
            this.legDistance.TabIndex = 82;
            this.legDistance.Text = "0";
            this.legDistance.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 83;
            this.label3.Text = "Route Fuel Use";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "mg";
            // 
            // routeFuelUse
            // 
            this.routeFuelUse.Location = new System.Drawing.Point(101, 181);
            this.routeFuelUse.Name = "routeFuelUse";
            this.routeFuelUse.Size = new System.Drawing.Size(40, 16);
            this.routeFuelUse.TabIndex = 85;
            this.routeFuelUse.Text = "0";
            this.routeFuelUse.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonCargo
            // 
            this.buttonCargo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCargo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCargo.Location = new System.Drawing.Point(115, 17);
            this.buttonCargo.Name = "buttonCargo";
            this.buttonCargo.Size = new System.Drawing.Size(56, 23);
            this.buttonCargo.TabIndex = 88;
            this.buttonCargo.Text = "Cargo";
            this.buttonCargo.Click += new System.EventHandler(this.CargoButton_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 89;
            this.label6.Text = "Leg Journey Time";
            // 
            // legTime
            // 
            this.legTime.Location = new System.Drawing.Point(101, 165);
            this.legTime.Name = "legTime";
            this.legTime.Size = new System.Drawing.Size(40, 16);
            this.legTime.TabIndex = 90;
            this.legTime.Text = "0";
            this.legTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(146, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 91;
            this.label7.Text = "yr";
            // 
            // manageFleet
            // 
            this.manageFleet.Location = new System.Drawing.Point(5, 116);
            this.manageFleet.Name = "manageFleet";
            this.manageFleet.Size = new System.Drawing.Size(56, 23);
            this.manageFleet.TabIndex = 92;
            this.manageFleet.Text = "Split";
            this.manageFleet.UseVisualStyleBackColor = true;
            this.manageFleet.Click += new System.EventHandler(this.SplitFleetClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(0, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 54);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Battle Plan";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Default"});
            this.comboBox1.Location = new System.Drawing.Point(7, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Default";
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.meterCargo);
            this.groupStatus.Controls.Add(this.meterFuel);
            this.groupStatus.Controls.Add(this.label4);
            this.groupStatus.Controls.Add(this.label12);
            this.groupStatus.Location = new System.Drawing.Point(177, 3);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Size = new System.Drawing.Size(178, 58);
            this.groupStatus.TabIndex = 95;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "Status";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Cargo";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(18, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 96;
            this.label12.Text = "Fuel";
            // 
            // buttonGotoPlanet
            // 
            this.buttonGotoPlanet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGotoPlanet.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGotoPlanet.Location = new System.Drawing.Point(6, 17);
            this.buttonGotoPlanet.Name = "buttonGotoPlanet";
            this.buttonGotoPlanet.Size = new System.Drawing.Size(56, 23);
            this.buttonGotoPlanet.TabIndex = 89;
            this.buttonGotoPlanet.Text = "Goto";
            this.buttonGotoPlanet.Click += new System.EventHandler(this.ButtonGotoPlanet_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxRepeatOrders);
            this.groupBox3.Controls.Add(this.buttonWaypointTarget);
            this.groupBox3.Controls.Add(this.wayPoints);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.warpFactor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.legFuel);
            this.groupBox3.Controls.Add(this.legTime);
            this.groupBox3.Controls.Add(this.legDistance);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.warpText);
            this.groupBox3.Controls.Add(this.routeFuelUse);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(0, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 265);
            this.groupBox3.TabIndex = 96;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Waypoints";
            // 
            // checkBoxRepeatOrders
            // 
            this.checkBoxRepeatOrders.AutoSize = true;
            this.checkBoxRepeatOrders.Enabled = false;
            this.checkBoxRepeatOrders.Location = new System.Drawing.Point(9, 204);
            this.checkBoxRepeatOrders.Name = "checkBoxRepeatOrders";
            this.checkBoxRepeatOrders.Size = new System.Drawing.Size(95, 17);
            this.checkBoxRepeatOrders.TabIndex = 95;
            this.checkBoxRepeatOrders.Text = "Repeat Orders";
            this.checkBoxRepeatOrders.UseVisualStyleBackColor = true;
            // 
            // buttonWaypointTarget
            // 
            this.buttonWaypointTarget.BackColor = System.Drawing.Color.Transparent;
            this.buttonWaypointTarget.ContextMenuStrip = this.contextMenuWaypointTargets;
            this.buttonWaypointTarget.FlatAppearance.BorderSize = 0;
            this.buttonWaypointTarget.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonWaypointTarget.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonWaypointTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWaypointTarget.Image = global::Nova.Properties.Resources.Diamond_Button;
            this.buttonWaypointTarget.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonWaypointTarget.Location = new System.Drawing.Point(138, 196);
            this.buttonWaypointTarget.Margin = new System.Windows.Forms.Padding(0);
            this.buttonWaypointTarget.Name = "buttonWaypointTarget";
            this.buttonWaypointTarget.Size = new System.Drawing.Size(25, 25);
            this.buttonWaypointTarget.TabIndex = 94;
            this.buttonWaypointTarget.UseVisualStyleBackColor = false;
            // 
            // contextMenuWaypointTargets
            // 
            this.contextMenuWaypointTargets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blahToolStripMenuItem});
            this.contextMenuWaypointTargets.Name = "contextMenuWaypointTargets";
            this.contextMenuWaypointTargets.Size = new System.Drawing.Size(153, 48);
            this.contextMenuWaypointTargets.Opened += new System.EventHandler(this.ShowWaypointContext);
            this.contextMenuWaypointTargets.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuWaypointTargets_SelectedIndexChanged);
            // 
            // blahToolStripMenuItem
            // 
            this.blahToolStripMenuItem.Name = "blahToolStripMenuItem";
            this.blahToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blahToolStripMenuItem.Text = "blah";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRename);
            this.groupBox4.Controls.Add(this.fleetComposition);
            this.groupBox4.Controls.Add(this.manageFleet);
            this.groupBox4.Location = new System.Drawing.Point(176, 119);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(179, 150);
            this.groupBox4.TabIndex = 97;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fleet Composition";
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(114, 116);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(56, 23);
            this.btnRename.TabIndex = 93;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.RenameClick);
            // 
            // fleetComposition
            // 
            this.fleetComposition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeHeader,
            this.numberHeader});
            this.fleetComposition.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.fleetComposition.Location = new System.Drawing.Point(5, 22);
            this.fleetComposition.Name = "fleetComposition";
            this.fleetComposition.Size = new System.Drawing.Size(168, 88);
            this.fleetComposition.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fleetComposition.TabIndex = 0;
            this.fleetComposition.UseCompatibleStateImageBehavior = false;
            this.fleetComposition.View = System.Windows.Forms.View.Details;
            // 
            // typeHeader
            // 
            this.typeHeader.Text = "Type";
            this.typeHeader.Width = 84;
            // 
            // numberHeader
            // 
            this.numberHeader.Text = "Number";
            this.numberHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numberHeader.Width = 53;
            // 
            // groupFleetSelection
            // 
            this.groupFleetSelection.Controls.Add(this.previousFleet);
            this.groupFleetSelection.Controls.Add(this.nextFleet);
            this.groupFleetSelection.Location = new System.Drawing.Point(0, 0);
            this.groupFleetSelection.Name = "groupFleetSelection";
            this.groupFleetSelection.Size = new System.Drawing.Size(166, 61);
            this.groupFleetSelection.TabIndex = 98;
            this.groupFleetSelection.TabStop = false;
            this.groupFleetSelection.Text = "Fleet Selection";
            // 
            // previousFleet
            // 
            this.previousFleet.Enabled = false;
            this.previousFleet.Location = new System.Drawing.Point(101, 23);
            this.previousFleet.Name = "previousFleet";
            this.previousFleet.Size = new System.Drawing.Size(56, 23);
            this.previousFleet.TabIndex = 1;
            this.previousFleet.Text = "Previous";
            this.previousFleet.UseVisualStyleBackColor = true;
            this.previousFleet.Click += new System.EventHandler(this.PreviousFleet_Click);
            // 
            // nextFleet
            // 
            this.nextFleet.Enabled = false;
            this.nextFleet.Location = new System.Drawing.Point(7, 23);
            this.nextFleet.Name = "nextFleet";
            this.nextFleet.Size = new System.Drawing.Size(56, 23);
            this.nextFleet.TabIndex = 0;
            this.nextFleet.Text = "Next";
            this.nextFleet.UseVisualStyleBackColor = true;
            this.nextFleet.Click += new System.EventHandler(this.NextFleet_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.meterCargoOther);
            this.groupBox7.Controls.Add(this.meterFuelOther);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.buttonMerge);
            this.groupBox7.Controls.Add(this.buttonCargoXfer);
            this.groupBox7.Controls.Add(this.buttonGotoFleet);
            this.groupBox7.Controls.Add(this.comboOtherFleets);
            this.groupBox7.Location = new System.Drawing.Point(177, 273);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(178, 112);
            this.groupBox7.TabIndex = 99;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Other fleets at this Location";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(10, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 101;
            this.label10.Text = "Cargo";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(18, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 100;
            this.label11.Text = "Fuel";
            // 
            // buttonMerge
            // 
            this.buttonMerge.Location = new System.Drawing.Point(57, 81);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(59, 23);
            this.buttonMerge.TabIndex = 94;
            this.buttonMerge.Text = "Merge";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.ButtonMerge_Click);
            // 
            // buttonCargoXfer
            // 
            this.buttonCargoXfer.Location = new System.Drawing.Point(122, 81);
            this.buttonCargoXfer.Name = "buttonCargoXfer";
            this.buttonCargoXfer.Size = new System.Drawing.Size(50, 23);
            this.buttonCargoXfer.TabIndex = 93;
            this.buttonCargoXfer.Text = "Cargo";
            this.buttonCargoXfer.UseVisualStyleBackColor = true;
            this.buttonCargoXfer.Click += new System.EventHandler(this.ButtonCargoXfer_Click);
            // 
            // buttonGotoFleet
            // 
            this.buttonGotoFleet.Location = new System.Drawing.Point(3, 81);
            this.buttonGotoFleet.Name = "buttonGotoFleet";
            this.buttonGotoFleet.Size = new System.Drawing.Size(50, 23);
            this.buttonGotoFleet.TabIndex = 92;
            this.buttonGotoFleet.Text = "Goto";
            this.buttonGotoFleet.UseVisualStyleBackColor = true;
            this.buttonGotoFleet.Click += new System.EventHandler(this.ButtonGotoFleet_Click);
            // 
            // comboOtherFleets
            // 
            this.comboOtherFleets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOtherFleets.FormattingEnabled = true;
            this.comboOtherFleets.Location = new System.Drawing.Point(9, 19);
            this.comboOtherFleets.Name = "comboOtherFleets";
            this.comboOtherFleets.Size = new System.Drawing.Size(162, 21);
            this.comboOtherFleets.TabIndex = 0;
            this.comboOtherFleets.SelectedIndexChanged += new System.EventHandler(this.ComboOtherFleets_SelectedIndexChanged);
            // 
            // groupOrbitPlanet
            // 
            this.groupOrbitPlanet.Controls.Add(this.buttonCargo);
            this.groupOrbitPlanet.Controls.Add(this.buttonGotoPlanet);
            this.groupOrbitPlanet.Location = new System.Drawing.Point(177, 67);
            this.groupOrbitPlanet.Name = "groupOrbitPlanet";
            this.groupOrbitPlanet.Size = new System.Drawing.Size(178, 46);
            this.groupOrbitPlanet.TabIndex = 96;
            this.groupOrbitPlanet.TabStop = false;
            this.groupOrbitPlanet.Text = "Orbiting Planet";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.WaypointTasks);
            this.groupBox2.Location = new System.Drawing.Point(0, 392);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 60);
            this.groupBox2.TabIndex = 100;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoint Task";
            // 
            // WaypointTasks
            // 
            this.WaypointTasks.BackColor = System.Drawing.SystemColors.Window;
            this.WaypointTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WaypointTasks.ItemHeight = 13;
            this.WaypointTasks.Items.AddRange(new object[] {
            "None",
            "Colonise",
            "Invade",
            "Scrap",
            "Unload Cargo",
            "Lay Mines"});
            this.WaypointTasks.Location = new System.Drawing.Point(7, 20);
            this.WaypointTasks.Name = "WaypointTasks";
            this.WaypointTasks.Size = new System.Drawing.Size(152, 21);
            this.WaypointTasks.TabIndex = 68;
            this.WaypointTasks.TabStop = false;
            // 
            // meterCargoOther
            // 
            this.meterCargoOther.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.meterCargoOther.Location = new System.Drawing.Point(61, 62);
            this.meterCargoOther.Name = "meterCargoOther";
            this.meterCargoOther.Size = new System.Drawing.Size(108, 15);
            this.meterCargoOther.TabIndex = 102;
            this.meterCargoOther.Text = "cargoMeter2";
            // 
            // meterFuelOther
            // 
            this.meterFuelOther.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.meterFuelOther.Location = new System.Drawing.Point(61, 45);
            this.meterFuelOther.Name = "meterFuelOther";
            this.meterFuelOther.Size = new System.Drawing.Size(108, 15);
            this.meterFuelOther.TabIndex = 99;
            this.meterFuelOther.Text = "cargoMeter1";
            // 
            // wayPoints
            // 
            this.wayPoints.FormattingEnabled = true;
            this.wayPoints.Location = new System.Drawing.Point(9, 45);
            this.wayPoints.Name = "wayPoints";
            this.wayPoints.Size = new System.Drawing.Size(152, 82);
            this.wayPoints.TabIndex = 93;
            this.wayPoints.SelectedIndexChanged += new System.EventHandler(this.WaypointSelection);
            this.wayPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.wayPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            // 
            // meterCargo
            // 
            this.meterCargo.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.meterCargo.Location = new System.Drawing.Point(61, 32);
            this.meterCargo.Name = "meterCargo";
            this.meterCargo.Size = new System.Drawing.Size(108, 15);
            this.meterCargo.TabIndex = 98;
            this.meterCargo.Text = "cargoMeter2";
            // 
            // meterFuel
            // 
            this.meterFuel.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.meterFuel.Location = new System.Drawing.Point(61, 15);
            this.meterFuel.Name = "meterFuel";
            this.meterFuel.Size = new System.Drawing.Size(108, 15);
            this.meterFuel.TabIndex = 95;
            this.meterFuel.Text = "cargoMeter1";
            // 
            // FleetDetail
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupOrbitPlanet);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupFleetSelection);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupStatus);
            this.Controls.Add(this.groupBox1);
            this.Name = "FleetDetail";
            this.Size = new System.Drawing.Size(355, 453);
            ((System.ComponentModel.ISupportInitialize)(this.warpFactor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupStatus.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.contextMenuWaypointTargets.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupFleetSelection.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupOrbitPlanet.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private Label warpText;
        private Label label8;
        private Label label9;
        private Label legFuel;
        private TrackBar warpFactor;
        private Label label1;
        private Label label2;
        private Label legDistance;
        private Label label3;
        private Label label5;
        private Label routeFuelUse;
        private Button buttonCargo;
        private Label label6;
        private Label legTime;
        private Label label7;
        private Button manageFleet;
        private WaypointListBox wayPoints;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private GroupBox groupStatus;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ListView fleetComposition;
        private ColumnHeader typeHeader;
        private ColumnHeader numberHeader;
        private GroupBox groupFleetSelection;
        private Button previousFleet;
        private Button nextFleet;
        private Button buttonGotoPlanet;
        private GroupBox groupBox7;
        private Button buttonCargoXfer;
        private Button buttonGotoFleet;
        private ComboBox comboOtherFleets;
        private Button buttonMerge;
        private GroupBox groupOrbitPlanet;
        private Controls.CargoMeter meterCargo;
        private Controls.CargoMeter meterFuel;
        private Label label4;
        private Label label12;
        private Controls.CargoMeter meterCargoOther;
        private Controls.CargoMeter meterFuelOther;
        private Label label10;
        private Label label11;
        private Button btnRename;
        private Button buttonWaypointTarget;
        private GroupBox groupBox2;
        private ComboBox WaypointTasks;
        private CheckBox checkBoxRepeatOrders;
        private ContextMenuStrip contextMenuWaypointTargets;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem blahToolStripMenuItem;
    }
}