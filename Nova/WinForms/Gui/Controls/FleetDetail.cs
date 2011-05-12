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
// This module provides the Fleet Detail control.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// This is the hook to listen for a new selected Fleet.
    /// Objects who subscribe to this should respond to the Fleet
    /// selection change by using the FleetSelectionArgs supplied which hold
    /// the newly selected Fleet data.
    /// </summary>
    public delegate void FleetSelectionChanged(object sender, FleetSelectionArgs e);
    
    /// <summary>
    /// Holds data related to the current Fleet selection. 
    /// </summary>
    public class FleetSelectionArgs : System.EventArgs
    {
        public Fleet detail;
        public Fleet summary;
        
        public FleetSelectionArgs(Fleet detail, Fleet summary)
        {
            this.detail = detail;
            this.summary = summary;
        }
    }    

    /// <summary>
    /// Ship detail display panel.
    /// </summary>
    public class FleetDetail : System.Windows.Forms.UserControl
    {
        private Fleet selectedFleet;
        private int currentFleet;
        
        /// <summary>
        /// This event should be fired when the selected Fleet
        /// changes.
        /// </summary>
        public event FleetSelectionChanged FleetSelectionChangedEvent;
        
        /// <summary>
        /// This event should be fired in addition to
        /// FleetSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </summary>
        public event CursorChanged CursorChangedEvent;

        #region VS-Generated variables
        public ComboBox WaypointTasks;
        private Label label14;
        private Label label15;
        private Gauge fuel;
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
        private Label label4;
        private Gauge cargo;
        private Button cargoButton;
        private Label label6;
        private Label legTime;
        private Label label7;
        private Button manageFleet;
        private WaypointListBox wayPoints;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ListView fleetComposition;
        private ColumnHeader typeHeader;
        private ColumnHeader numberHeader;
        private GroupBox groupBox5;
        private Button previousFleet;
        private Button nextFleet;
        private System.ComponentModel.Container components = null;
        #endregion

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the FleetDetail class.
        /// </summary>
        public FleetDetail()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        // ============================================================================
        // Required method for Designer support - do not modify the contents of this
        // method with the code editor.
        // ============================================================================
        private void InitializeComponent()
        {
            this.WaypointTasks = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.fuel = new Nova.ControlLibrary.Gauge();
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
            this.label4 = new System.Windows.Forms.Label();
            this.cargo = new Nova.ControlLibrary.Gauge();
            this.cargoButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.legTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.manageFleet = new System.Windows.Forms.Button();
            this.wayPoints = new Nova.WinForms.Gui.WaypointListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fleetComposition = new System.Windows.Forms.ListView();
            this.typeHeader = new System.Windows.Forms.ColumnHeader();
            this.numberHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.previousFleet = new System.Windows.Forms.Button();
            this.nextFleet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.warpFactor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // WaypointTasks
            // 
            this.WaypointTasks.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WaypointTasks.ItemHeight = 13;
            this.WaypointTasks.Items.AddRange(new object[] {
            "None",
            "Colonise",
            "Invade",
            "Scrap",
            "Unload Cargo",
            "Lay Mines"});
            this.WaypointTasks.Location = new System.Drawing.Point(9, 38);
            this.WaypointTasks.Name = "WaypointTasks";
            this.WaypointTasks.Size = new System.Drawing.Size(152, 21);
            this.WaypointTasks.TabIndex = 67;
            this.WaypointTasks.TabStop = false;
            this.WaypointTasks.Text = "None";
            this.WaypointTasks.SelectedIndexChanged += new System.EventHandler(this.WaypointTaskChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(9, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 16);
            this.label14.TabIndex = 70;
            this.label14.Text = "Waypoint Task";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 16);
            this.label15.TabIndex = 71;
            this.label15.Text = "Fuel";
            // 
            // fuel
            // 
            this.fuel.BarColour = System.Drawing.Color.LightGreen;
            this.fuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fuel.BottomValue = 0;
            this.fuel.Location = new System.Drawing.Point(54, 18);
            this.fuel.Marker = 0;
            this.fuel.MarkerColour = System.Drawing.Color.Green;
            this.fuel.Maximum = 0;
            this.fuel.Minimum = 0;
            this.fuel.Name = "fuel";
            this.fuel.ShowText = true;
            this.fuel.Size = new System.Drawing.Size(100, 16);
            this.fuel.TabIndex = 72;
            this.fuel.TopValue = 0;
            this.fuel.Units = "mg";
            this.fuel.Value = 0;
            // 
            // warpText
            // 
            this.warpText.Location = new System.Drawing.Point(9, 62);
            this.warpText.Name = "warpText";
            this.warpText.Size = new System.Drawing.Size(48, 16);
            this.warpText.TabIndex = 76;
            this.warpText.Text = "Warp 4";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 77;
            this.label8.Text = "Leg Fuel Use";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "mg";
            // 
            // legFuel
            // 
            this.legFuel.Location = new System.Drawing.Point(105, 174);
            this.legFuel.Name = "legFuel";
            this.legFuel.Size = new System.Drawing.Size(40, 16);
            this.legFuel.TabIndex = 79;
            this.legFuel.Text = "0";
            this.legFuel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // warpFactor
            // 
            this.warpFactor.AutoSize = false;
            this.warpFactor.Location = new System.Drawing.Point(57, 62);
            this.warpFactor.Name = "warpFactor";
            this.warpFactor.Size = new System.Drawing.Size(104, 26);
            this.warpFactor.TabIndex = 75;
            this.warpFactor.TabStop = false;
            this.warpFactor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.warpFactor.Value = 4;
            this.warpFactor.Scroll += new System.EventHandler(this.SpeedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 80;
            this.label1.Text = "Leg Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "ly";
            // 
            // legDistance
            // 
            this.legDistance.Location = new System.Drawing.Point(105, 158);
            this.legDistance.Name = "legDistance";
            this.legDistance.Size = new System.Drawing.Size(40, 16);
            this.legDistance.TabIndex = 82;
            this.legDistance.Text = "0";
            this.legDistance.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 83;
            this.label3.Text = "Route Fuel Use";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "mg";
            // 
            // routeFuelUse
            // 
            this.routeFuelUse.Location = new System.Drawing.Point(105, 206);
            this.routeFuelUse.Name = "routeFuelUse";
            this.routeFuelUse.Size = new System.Drawing.Size(40, 16);
            this.routeFuelUse.TabIndex = 85;
            this.routeFuelUse.Text = "0";
            this.routeFuelUse.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 87;
            this.label4.Text = "Cargo";
            // 
            // cargo
            // 
            this.cargo.BarColour = System.Drawing.Color.Tan;
            this.cargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cargo.BottomValue = 0;
            this.cargo.Location = new System.Drawing.Point(54, 40);
            this.cargo.Marker = 0;
            this.cargo.MarkerColour = System.Drawing.Color.Green;
            this.cargo.Maximum = 0;
            this.cargo.Minimum = 0;
            this.cargo.Name = "cargo";
            this.cargo.ShowText = true;
            this.cargo.Size = new System.Drawing.Size(100, 16);
            this.cargo.TabIndex = 74;
            this.cargo.TopValue = 0;
            this.cargo.Units = "kT";
            this.cargo.Value = 0;
            // 
            // cargoButton
            // 
            this.cargoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cargoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cargoButton.Location = new System.Drawing.Point(9, 88);
            this.cargoButton.Name = "cargoButton";
            this.cargoButton.Size = new System.Drawing.Size(56, 23);
            this.cargoButton.TabIndex = 88;
            this.cargoButton.Text = "Cargo";
            this.cargoButton.Click += new System.EventHandler(this.CargoButton_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 89;
            this.label6.Text = "Leg Journey Time";
            // 
            // legTime
            // 
            this.legTime.Location = new System.Drawing.Point(105, 190);
            this.legTime.Name = "legTime";
            this.legTime.Size = new System.Drawing.Size(40, 16);
            this.legTime.TabIndex = 90;
            this.legTime.Text = "0";
            this.legTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(145, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 91;
            this.label7.Text = "yr";
            // 
            // manageFleet
            // 
            this.manageFleet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.manageFleet.Location = new System.Drawing.Point(15, 206);
            this.manageFleet.Name = "manageFleet";
            this.manageFleet.Size = new System.Drawing.Size(56, 23);
            this.manageFleet.TabIndex = 92;
            this.manageFleet.Text = "Manage";
            this.manageFleet.UseVisualStyleBackColor = true;
            this.manageFleet.Click += new System.EventHandler(this.MangeFleet_Click);
            // 
            // wayPoints
            // 
            this.wayPoints.FormattingEnabled = true;
            this.wayPoints.Location = new System.Drawing.Point(9, 83);
            this.wayPoints.Name = "wayPoints";
            this.wayPoints.Size = new System.Drawing.Size(152, 69);
            this.wayPoints.TabIndex = 93;
            this.wayPoints.SelectedIndexChanged += new System.EventHandler(this.WaypointSelection);
            this.wayPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.wayPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(7, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 50);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cargo);
            this.groupBox2.Controls.Add(this.fuel);
            this.groupBox2.Controls.Add(this.cargoButton);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(181, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 118);
            this.groupBox2.TabIndex = 95;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // groupBox3
            // 
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
            this.groupBox3.Controls.Add(this.WaypointTasks);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.warpText);
            this.groupBox3.Controls.Add(this.routeFuelUse);
            this.groupBox3.Location = new System.Drawing.Point(5, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 239);
            this.groupBox3.TabIndex = 96;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Waypoints";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fleetComposition);
            this.groupBox4.Controls.Add(this.manageFleet);
            this.groupBox4.Location = new System.Drawing.Point(181, 134);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 239);
            this.groupBox4.TabIndex = 97;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fleet Composition";
            // 
            // fleetComposition
            // 
            this.fleetComposition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeHeader,
            this.numberHeader});
            this.fleetComposition.Location = new System.Drawing.Point(5, 22);
            this.fleetComposition.Name = "fleetComposition";
            this.fleetComposition.Size = new System.Drawing.Size(159, 130);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.previousFleet);
            this.groupBox5.Controls.Add(this.nextFleet);
            this.groupBox5.Location = new System.Drawing.Point(9, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(166, 61);
            this.groupBox5.TabIndex = 98;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fleet Selection";
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
            // FleetDetail
            // 
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FleetDetail";
            this.Size = new System.Drawing.Size(356, 380);
            ((System.ComponentModel.ISupportInitialize)(this.warpFactor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when the warp factor slider is moved.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SpeedChanged(object sender, System.EventArgs e)
        {
            warpText.Text = "Warp " + this.warpFactor.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (this.wayPoints.SelectedItems.Count > 0)
            {
                int index = this.wayPoints.SelectedIndices[0];
                Waypoint waypoint = this.selectedFleet.Waypoints[index] as Waypoint;
                waypoint.WarpFactor = this.warpFactor.Value;

                DisplayLegDetails(index);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// On a waypoint being selected update the speed and tasks controls to
        /// reflect the values of the selected waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void WaypointSelection(object sender, System.EventArgs e)
        {
            if (this.wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = this.wayPoints.SelectedIndices[0];
            DisplayLegDetails(index);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Cargo button pressed. Pop up the cargo transfer dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CargoButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                CargoDialog cargoDialog = new CargoDialog();

                cargoDialog.SetTarget(this.selectedFleet);
                cargoDialog.ShowDialog();
                cargoDialog.Dispose();
                ClientState.Data.StarReports[this.selectedFleet.InOrbit.Name] = new StarReport(this.selectedFleet.InOrbit);
                this.cargo.Value = this.selectedFleet.Cargo.Mass;
            }
            catch
            {
                Report.Debug("FleetDetail.cs : CargoButton_Click() - Failed to open cargo dialog.");
            }
                
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Catch the backspace key to delete a fleet waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = this.wayPoints.SelectedIndices[0];

            // backspace
            if (index == 0 || !(e.KeyChar == (char)8)) 
            {
                return;
            }

            this.selectedFleet.Waypoints.RemoveAt(index);
            this.wayPoints.Items.RemoveAt(index);
            this.wayPoints.SelectedIndex = this.wayPoints.Items.Count - 1;

            Utilities.MapRefresh();
        }

        /// <summary>
        /// Process the delete key to delete a fleet waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int index = this.wayPoints.SelectedIndices[0];
                if (index > 0)
                {
                    this.selectedFleet.Waypoints.RemoveAt(index);
                    this.wayPoints.Items.RemoveAt(index);
                    this.wayPoints.SelectedIndex = this.wayPoints.Items.Count - 1;

                    Utilities.MapRefresh();
                }
                e.Handled = true;
            }

        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// If a waypoint task changes, and a waypoint is selected, change the task at
        /// that waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void WaypointTaskChanged(object sender, EventArgs e)
        {
            if (this.wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = this.wayPoints.SelectedIndices[0];
            Waypoint waypoint = this.selectedFleet.Waypoints[index] as Waypoint;

            waypoint.Task = WaypointTasks.Text;
        }
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A waypoint has been added or deleted.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// ----------------------------------------------------------------------------
        public void WaypointListChanged(object sender)
        {
            this.Refresh();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The manage fleet button has been pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MangeFleet_Click(object sender, EventArgs e)
        {
            ManageFleetDialog manageDialog = new ManageFleetDialog();
            manageDialog.ManagedFleet = this.selectedFleet;
            manageDialog.ShowDialog();
            manageDialog.Dispose();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the Next button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextFleet_Click(object sender, System.EventArgs e)
        {
            List<Fleet> myFleets = ClientState.Data.PlayerFleets;

            if (myFleets.Count == 1)
            {
                this.previousFleet.Enabled = false;
                this.nextFleet.Enabled = false;
                return;
            }

            this.previousFleet.Enabled = true;
            this.nextFleet.Enabled = true;

            if (currentFleet < myFleets.Count - 1)
            {
                currentFleet++;
            }
            else
            {
                currentFleet = 0;
            }
            
            Fleet current = myFleets[currentFleet];
            
            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(current, this.selectedFleet);
            CursorArgs cursorArgs = new CursorArgs((Point)this.selectedFleet.Position);
            
            // Inform of the selection change to all listening objects.
            FleetSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the previous button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousFleet_Click(object sender, EventArgs e)
        {
            List<Fleet> myFleets = ClientState.Data.PlayerFleets;

            if (myFleets.Count == 1)
            {
                this.previousFleet.Enabled = false;
                this.nextFleet.Enabled = false;
                return;
            }

            this.previousFleet.Enabled = true;
            this.nextFleet.Enabled = true;

            if (currentFleet > 0)
            {
                currentFleet--;
            }
            else
            {
                currentFleet = myFleets.Count - 1;
            }

            Fleet current = myFleets[currentFleet];
            
            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(current, this.selectedFleet);
            CursorArgs cursorArgs = new CursorArgs((Point)this.selectedFleet.Position);
            
            // Inform of the selection change to all listening objects.
            FleetSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// If there is another waypoint before the selected one, display the fuel,
        /// time, etc. required for this leg.
        /// </summary>
        /// <param name="index">Index of the waypoint to display.</param>
        /// ----------------------------------------------------------------------------
        public void DisplayLegDetails(int index)
        {
            Waypoint thisWaypoint = this.selectedFleet.Waypoints[index] as Waypoint;
            Race race = ClientState.Data.PlayerRace;

            WaypointTasks.Text = thisWaypoint.Task;

            if (this.selectedFleet.Waypoints.Count == 1)
            {
                thisWaypoint.WarpFactor = 0;
            }

            this.selectedFleet.Waypoints[index] = thisWaypoint as Waypoint;
            this.warpFactor.Value = thisWaypoint.WarpFactor;
            warpText.Text = "Warp " + thisWaypoint.WarpFactor;

            if (index > 0 && thisWaypoint.WarpFactor > 0)
            {
                Waypoint from = this.selectedFleet.Waypoints[index - 1] as Waypoint;
                Waypoint to = this.selectedFleet.Waypoints[index] as Waypoint;
                double distance = PointUtilities.Distance(from.Position, to.Position);

                double time = distance / (to.WarpFactor * to.WarpFactor);
                double fuelUsed = this.selectedFleet.FuelConsumption(to.WarpFactor, race)
                                * time;

                legDistance.Text = String.Format("{0}", distance.ToString("f1"));
                legFuel.Text = String.Format("{0}", fuelUsed.ToString("f1"));
                legTime.Text = String.Format("{0}", time.ToString("f1"));
            }
            else
            {
                legDistance.Text = "0";
                legFuel.Text = "0";
                legTime.Text = "0";
            }

            Waypoint previous = null;
            double fuelRequired = 0;

            // Sum up the total fuel required for all waypoints in the current
            // route (as long as there is more than one waypoint).

            foreach (Waypoint waypoint in this.selectedFleet.Waypoints)
            {
                if (previous != null && waypoint.WarpFactor > 0)
                {
                    double distance = PointUtilities.Distance(waypoint.Position, previous.Position);
                    int warpFactor = waypoint.WarpFactor;
                    double speed = warpFactor * warpFactor;
                    double travelTime = distance / speed;

                    fuelRequired += this.selectedFleet.FuelConsumption(warpFactor, race)
                                  * travelTime;
                }
                previous = waypoint;
            }

            routeFuelUse.Text = fuelRequired.ToString("f1");
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a new waypoint into the waypoint list control.
        /// </summary>
        /// <param name="waypoint">A new waypoint to add.</param>
        /// ----------------------------------------------------------------------------
        public void AddWaypoint(Waypoint waypoint)
        {
            this.wayPoints.Items.Add(waypoint.Destination);
            this.wayPoints.SelectedIndex = this.wayPoints.Items.Count - 1;
            DisplayLegDetails(this.wayPoints.SelectedIndex);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set up all the display controls to reflect the selected fleet
        /// </summary>
        /// <param name="fleet">The selected fleet.</param>
        /// ----------------------------------------------------------------------------
        private void SetFleetDetails(Fleet fleet)
        {
            this.selectedFleet = fleet;

            List<Fleet> myFleets = ClientState.Data.PlayerFleets;
            int i;

            for (i = 0; i < myFleets.Count; i++)
            {
                if (fleet.Name == myFleets[i].Name)
                {
                    currentFleet = i;
                    break;
                }
            }

            if (myFleets.Count > 1)
            {
                this.previousFleet.Enabled = true;
                this.nextFleet.Enabled = true;
            }
            else
            {
                this.previousFleet.Enabled = false;
                this.previousFleet.Enabled = false;
            }

            Hashtable designs = fleet.Composition;
            this.fleetComposition.Items.Clear();

            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(((int)designs[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                this.fleetComposition.Items.Add(listItem);
            }

            this.fuel.Maximum = (int)fleet.TotalFuelCapacity;
            this.fuel.Value = (int)fleet.FuelAvailable;
            this.cargo.Maximum = (int)fleet.TotalCargoCapacity;
            this.cargo.Value = (int)fleet.Cargo.Mass;

            this.wayPoints.Items.Clear();
            foreach (Waypoint waypoint in fleet.Waypoints)
            {
                this.wayPoints.Items.Add(waypoint.Destination);
            }

            this.wayPoints.SelectedIndex = this.wayPoints.Items.Count - 1;
            DisplayLegDetails(this.wayPoints.Items.Count - 1);

            // If we are in orbit around a planet and we have a cargo carrying
            // capacity, enable the Cargo Dialog Button.

            if (fleet.InOrbit != null)
            {
                if (fleet.TotalCargoCapacity > 0)
                {
                    this.cargoButton.Enabled = true;
                }
                else
                {
                    // CargoButton.Enabled = false; // FIXME (priority 4) - disabled due to it sometimes being disabled when it shouldn't be Dan 22 May 10 
                }
            }
        }


        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property to set or get the fleet currently being displayed.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Fleet Value
        {
            set { if (value != null) SetFleetDetails(value); }
            get { return this.selectedFleet; }
        }


        /// <summary>
        /// Get warp factor value.
        /// </summary>
        public int Speed
        {
            get { return this.warpFactor.Value; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get waypoint task.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string Task
        {
            get { return WaypointTasks.Text; }
        }

        #endregion

 

    }


}
