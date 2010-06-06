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

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;

using Nova.Common;
using Nova.ControlLibrary;
using Nova.Client;

namespace Nova.WinForms.Gui
{

    /// <summary>
    /// Ship detail display panel.
    /// </summary>
    public class FleetDetail : System.Windows.Forms.UserControl
    {
        private Fleet SelectedFleet;
        private int currentFleet = 0;

        #region VS-Generated variables
        public ComboBox WaypointTasks;
        private Label label14;
        private Label label15;
        private Gauge Fuel;
        private Label warpText;
        private Label label8;
        private Label label9;
        private Label legFuel;
        private TrackBar WarpFactor;
        private Label label1;
        private Label label2;
        private Label legDistance;
        private Label label3;
        private Label label5;
        private Label routeFuelUse;
        private Label label4;
        private Gauge Cargo;
        private Button CargoButton;
        private Label label6;
        private Label legTime;
        private Label label7;
        private Button ManageFleet;
        private ListBox WayPoints;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ListView FleetComposition;
        private ColumnHeader TypeHeader;
        private ColumnHeader NumberHeader;
        private GroupBox groupBox5;
        private Button PreviousFleet;
        private Button NextFleet;
        private System.ComponentModel.Container components = null;
        #endregion

        #region Construction and Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public FleetDetail()
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
        // ============================================================================
        // Required method for Designer support - do not modify the contents of this
        // method with the code editor.
        // ============================================================================
        private void InitializeComponent()
        {
            this.WaypointTasks = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Fuel = new Nova.ControlLibrary.Gauge();
            this.warpText = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.legFuel = new System.Windows.Forms.Label();
            this.WarpFactor = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.legDistance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.routeFuelUse = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cargo = new Nova.ControlLibrary.Gauge();
            this.CargoButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.legTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ManageFleet = new System.Windows.Forms.Button();
            this.WayPoints = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.FleetComposition = new System.Windows.Forms.ListView();
            this.TypeHeader = new System.Windows.Forms.ColumnHeader();
            this.NumberHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.PreviousFleet = new System.Windows.Forms.Button();
            this.NextFleet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WarpFactor)).BeginInit();
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
            // Fuel
            // 
            this.Fuel.BarColour = System.Drawing.Color.LightGreen;
            this.Fuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Fuel.BottomValue = 0;
            this.Fuel.Location = new System.Drawing.Point(54, 18);
            this.Fuel.Marker = 0;
            this.Fuel.MarkerColour = System.Drawing.Color.Green;
            this.Fuel.Maximum = 0;
            this.Fuel.Minimum = 0;
            this.Fuel.Name = "Fuel";
            this.Fuel.ShowText = true;
            this.Fuel.Size = new System.Drawing.Size(100, 16);
            this.Fuel.TabIndex = 72;
            this.Fuel.TopValue = 0;
            this.Fuel.Units = "mg";
            this.Fuel.Value = 0;
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
            // WarpFactor
            // 
            this.WarpFactor.AutoSize = false;
            this.WarpFactor.Location = new System.Drawing.Point(57, 62);
            this.WarpFactor.Name = "WarpFactor";
            this.WarpFactor.Size = new System.Drawing.Size(104, 16);
            this.WarpFactor.TabIndex = 75;
            this.WarpFactor.TabStop = false;
            this.WarpFactor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.WarpFactor.Value = 4;
            this.WarpFactor.Scroll += new System.EventHandler(this.SpeedChanged);
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
            // Cargo
            // 
            this.Cargo.BarColour = System.Drawing.Color.Tan;
            this.Cargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cargo.BottomValue = 0;
            this.Cargo.Location = new System.Drawing.Point(54, 40);
            this.Cargo.Marker = 0;
            this.Cargo.MarkerColour = System.Drawing.Color.Green;
            this.Cargo.Maximum = 0;
            this.Cargo.Minimum = 0;
            this.Cargo.Name = "Cargo";
            this.Cargo.ShowText = true;
            this.Cargo.Size = new System.Drawing.Size(100, 16);
            this.Cargo.TabIndex = 74;
            this.Cargo.TopValue = 0;
            this.Cargo.Units = "kT";
            this.Cargo.Value = 0;
            // 
            // CargoButton
            // 
            this.CargoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CargoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CargoButton.Location = new System.Drawing.Point(9, 88);
            this.CargoButton.Name = "CargoButton";
            this.CargoButton.Size = new System.Drawing.Size(56, 23);
            this.CargoButton.TabIndex = 88;
            this.CargoButton.Text = "Cargo";
            this.CargoButton.Click += new System.EventHandler(this.CargoButton_Click);
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
            // ManageFleet
            // 
            this.ManageFleet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ManageFleet.Location = new System.Drawing.Point(15, 206);
            this.ManageFleet.Name = "ManageFleet";
            this.ManageFleet.Size = new System.Drawing.Size(56, 23);
            this.ManageFleet.TabIndex = 92;
            this.ManageFleet.Text = "Manage";
            this.ManageFleet.UseVisualStyleBackColor = true;
            this.ManageFleet.Click += new System.EventHandler(this.MangeFleet_Click);
            // 
            // WayPoints
            // 
            this.WayPoints.FormattingEnabled = true;
            this.WayPoints.Location = new System.Drawing.Point(9, 83);
            this.WayPoints.Name = "WayPoints";
            this.WayPoints.Size = new System.Drawing.Size(152, 69);
            this.WayPoints.TabIndex = 93;
            this.WayPoints.SelectedIndexChanged += new System.EventHandler(this.WaypointSelection);
            this.WayPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
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
            this.groupBox2.Controls.Add(this.Cargo);
            this.groupBox2.Controls.Add(this.Fuel);
            this.groupBox2.Controls.Add(this.CargoButton);
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
            this.groupBox3.Controls.Add(this.WayPoints);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.WarpFactor);
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
            this.groupBox4.Controls.Add(this.FleetComposition);
            this.groupBox4.Controls.Add(this.ManageFleet);
            this.groupBox4.Location = new System.Drawing.Point(181, 134);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 239);
            this.groupBox4.TabIndex = 97;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fleet Composition";
            // 
            // FleetComposition
            // 
            this.FleetComposition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TypeHeader,
            this.NumberHeader});
            this.FleetComposition.Location = new System.Drawing.Point(5, 22);
            this.FleetComposition.Name = "FleetComposition";
            this.FleetComposition.Size = new System.Drawing.Size(159, 130);
            this.FleetComposition.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.FleetComposition.TabIndex = 0;
            this.FleetComposition.UseCompatibleStateImageBehavior = false;
            this.FleetComposition.View = System.Windows.Forms.View.Details;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 84;
            // 
            // NumberHeader
            // 
            this.NumberHeader.Text = "Number";
            this.NumberHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumberHeader.Width = 53;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PreviousFleet);
            this.groupBox5.Controls.Add(this.NextFleet);
            this.groupBox5.Location = new System.Drawing.Point(9, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(166, 61);
            this.groupBox5.TabIndex = 98;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fleet Selection";
            // 
            // PreviousFleet
            // 
            this.PreviousFleet.Enabled = false;
            this.PreviousFleet.Location = new System.Drawing.Point(101, 23);
            this.PreviousFleet.Name = "PreviousFleet";
            this.PreviousFleet.Size = new System.Drawing.Size(56, 23);
            this.PreviousFleet.TabIndex = 1;
            this.PreviousFleet.Text = "Previous";
            this.PreviousFleet.UseVisualStyleBackColor = true;
            this.PreviousFleet.Click += new System.EventHandler(this.PreviousFleet_Click);
            // 
            // NextFleet
            // 
            this.NextFleet.Enabled = false;
            this.NextFleet.Location = new System.Drawing.Point(7, 23);
            this.NextFleet.Name = "NextFleet";
            this.NextFleet.Size = new System.Drawing.Size(56, 23);
            this.NextFleet.TabIndex = 0;
            this.NextFleet.Text = "Next";
            this.NextFleet.UseVisualStyleBackColor = true;
            this.NextFleet.Click += new System.EventHandler(this.NextFleet_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.WarpFactor)).EndInit();
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
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SpeedChanged(object sender, System.EventArgs e)
        {
            warpText.Text = "Warp " + WarpFactor.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (WayPoints.SelectedItems.Count > 0)
            {
                int index = WayPoints.SelectedIndices[0];
                Waypoint waypoint = SelectedFleet.Waypoints[index] as Waypoint;
                waypoint.WarpFactor = WarpFactor.Value;

                DisplayLegDetails(index);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// On a waypoint being selected update the speed and tasks controls to
        /// reflect the values of the selected waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void WaypointSelection(object sender, System.EventArgs e)
        {
            if (WayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = WayPoints.SelectedIndices[0];
            DisplayLegDetails(index);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Cargo button pressed. Pop up the cargo transfer dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CargoButton_Click(object sender, System.EventArgs e)
        {
            CargoDialog cargoDialog = new CargoDialog();

            cargoDialog.SetTarget(SelectedFleet);
            cargoDialog.ShowDialog();
            cargoDialog.Dispose();

            Cargo.Value = SelectedFleet.Cargo.Mass;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// If a waypoint is selected (other than the current position) and the delete
        /// key is pressed, delete the selected waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// FIXME (priority 7) - doesn't work with my keyboard, Vista 64 bit environment - Dan 25/4/10
        /// ----------------------------------------------------------------------------
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (WayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = WayPoints.SelectedIndices[0];

            if (index == 0 || e.KeyChar != (char)8)
            {
                return;
            }

            SelectedFleet.Waypoints.RemoveAt(index);
            WayPoints.Items.RemoveAt(index);
            WayPoints.SelectedIndex = WayPoints.Items.Count - 1;

            Utilities.MapRefresh();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// If a waypoint task changes, and a waypoint is selected, change the task at
        /// that waypoint.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void WaypointTaskChanged(object sender, EventArgs e)
        {
            if (WayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = WayPoints.SelectedIndices[0];
            Waypoint waypoint = SelectedFleet.Waypoints[index] as Waypoint;

            waypoint.Task = WaypointTasks.Text;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The manage fleet button has been pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MangeFleet_Click(object sender, EventArgs e)
        {
            ManageFleetDialog manageDialog = new ManageFleetDialog();
            manageDialog.ManagedFleet = SelectedFleet;
            manageDialog.ShowDialog();
            manageDialog.Dispose();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the Next button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextFleet_Click(object sender, System.EventArgs e)
        {
            List<Fleet> myFleets = ClientState.Data.PlayerFleets;

            if (myFleets.Count == 1)
            {
                PreviousFleet.Enabled = false;
                NextFleet.Enabled = false;
                return;
            }

            PreviousFleet.Enabled = true;
            NextFleet.Enabled = true;

            if (currentFleet < myFleets.Count - 1)
            {
                currentFleet++;
            }
            else
            {
                currentFleet = 0;
            }

            MainWindow.nova.SelectionDetail.Value = myFleets[currentFleet];
            MainWindow.nova.SelectionSummary.Value = SelectedFleet;

            MainWindow.nova.MapControl.SetCursor(SelectedFleet.Position);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the previous button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousFleet_Click(object sender, EventArgs e)
        {
            List<Fleet> myFleets = ClientState.Data.PlayerFleets;

            if (myFleets.Count == 1)
            {
                PreviousFleet.Enabled = false;
                NextFleet.Enabled = false;
                return;
            }

            PreviousFleet.Enabled = true;
            NextFleet.Enabled = true;

            if (currentFleet > 0)
            {
                currentFleet--;
            }
            else
            {
                currentFleet = myFleets.Count - 1;
            }

            MainWindow.nova.SelectionDetail.Value = myFleets[currentFleet];
            MainWindow.nova.SelectionSummary.Value = SelectedFleet;

            MainWindow.nova.MapControl.SetCursor(SelectedFleet.Position);
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
            Waypoint thisWaypoint = SelectedFleet.Waypoints[index] as Waypoint;
            Race race = ClientState.Data.PlayerRace;

            WaypointTasks.Text = thisWaypoint.Task;

            if (SelectedFleet.Waypoints.Count == 1)
            {
                thisWaypoint.WarpFactor = 0;
            }

            SelectedFleet.Waypoints[index] = thisWaypoint as Waypoint;
            WarpFactor.Value = thisWaypoint.WarpFactor;
            warpText.Text = "Warp " + thisWaypoint.WarpFactor;

            if (index > 0 && thisWaypoint.WarpFactor > 0)
            {
                Waypoint from = SelectedFleet.Waypoints[index - 1] as Waypoint;
                Waypoint to = SelectedFleet.Waypoints[index] as Waypoint;
                double distance = PointUtilities.Distance(from.Position,
                                                              to.Position);

                double time = distance / (to.WarpFactor * to.WarpFactor);
                double fuelUsed = SelectedFleet.FuelConsumption(to.WarpFactor, race)
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

            foreach (Waypoint waypoint in SelectedFleet.Waypoints)
            {
                if (previous != null && waypoint.WarpFactor > 0)
                {
                    double distance = PointUtilities.Distance(waypoint.Position,
                                                              previous.Position);

                    int warpFactor = waypoint.WarpFactor;
                    double speed = warpFactor * warpFactor;
                    double travelTime = distance / speed;

                    fuelRequired += SelectedFleet.FuelConsumption(warpFactor, race)
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
            WayPoints.Items.Add(waypoint.Destination);
            WayPoints.SelectedIndex = WayPoints.Items.Count - 1;
            DisplayLegDetails(WayPoints.SelectedIndex);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set up all the display controls to reflect the selected fleet
        /// </summary>
        /// <param name="fleet">The selected fleet.</param>
        /// ----------------------------------------------------------------------------
        private void SetFleetDetails(Fleet fleet)
        {
            SelectedFleet = fleet;

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
                PreviousFleet.Enabled = true;
                NextFleet.Enabled = true;
            }
            else
            {
                PreviousFleet.Enabled = false;
                PreviousFleet.Enabled = false;
            }

            Hashtable designs = fleet.Composition;
            FleetComposition.Items.Clear();

            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(((int)designs[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                FleetComposition.Items.Add(listItem);
            }

            Fuel.Maximum = (int)fleet.FuelCapacity;
            Fuel.Value = (int)fleet.FuelAvailable;
            Cargo.Maximum = (int)fleet.CargoCapacity;
            Cargo.Value = (int)fleet.Cargo.Mass;

            WayPoints.Items.Clear();
            foreach (Waypoint waypoint in fleet.Waypoints)
            {
                WayPoints.Items.Add(waypoint.Destination);
            }

            WayPoints.SelectedIndex = WayPoints.Items.Count - 1;
            DisplayLegDetails(WayPoints.Items.Count - 1);

            // If we are in orbit around a planet and we have a cargo carrying
            // capacity, enable the Cargo Dialog Button.

            if (fleet.InOrbit != null)
            {
                if (fleet.CargoCapacity > 0)
                {
                    CargoButton.Enabled = true;
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
            get { return SelectedFleet; }
        }


        /// <summary>
        /// Get warp factor value.
        /// </summary>
        public int Speed
        {
            get { return WarpFactor.Value; }
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
