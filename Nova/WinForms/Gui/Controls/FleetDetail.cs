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
    public partial class FleetDetail : System.Windows.Forms.UserControl
    {
        private Fleet selectedFleet;
        private int currentFleet;
        
        /// <Summary>
        /// This event should be fired when the selected Fleet
        /// changes.
        /// </Summary>
        public event FleetSelectionChanged FleetSelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired in addition to
        /// FleetSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </Summary>
        public event CursorChanged CursorChangedEvent;
        
        /// <Summary>
        /// This event should be fired when a waypoint is deleted,
        /// so the StarMap updates right away.
        /// </Summary>
        public event RefreshStarMap RefreshStarMapEvent;

        #region Construction and Disposal

        /// <Summary>
        /// Initializes a new instance of the FleetDetail class.
        /// </Summary>
        public FleetDetail()
        {
            InitializeComponent();
        }




        #endregion

      
        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Called when the warp factor slider is moved.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SpeedChanged(object sender, System.EventArgs e)
        {
            warpText.Text = "Warp " + this.warpFactor.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (this.wayPoints.SelectedItems.Count > 0)
            {
                int index = this.wayPoints.SelectedIndices[0];
                Waypoint waypoint = this.selectedFleet.Waypoints[index];
                waypoint.WarpFactor = this.warpFactor.Value;

                DisplayLegDetails(index);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// On a waypoint being selected update the speed and tasks controls to
        /// reflect the values of the selected waypoint.
        /// </Summary>
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
        /// <Summary>
        /// Cargo button pressed. Pop up the cargo transfer dialog.
        /// </Summary>
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
        /// <Summary>
        /// Catch the backspace key to delete a fleet waypoint.
        /// </Summary>
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

            RefreshStarMapEvent();
        }

        /// <Summary>
        /// Process the delete key to delete a fleet waypoint.
        /// </Summary>
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

                    RefreshStarMapEvent();
                }
                e.Handled = true;
            }

        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// If a waypoint task changes, and a waypoint is selected, change the task at
        /// that waypoint.
        /// </Summary>
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
            Waypoint waypoint = this.selectedFleet.Waypoints[index];

            waypoint.Task = WaypointTasks.Text;
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// A waypoint has been added or deleted.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// ----------------------------------------------------------------------------
        public void WaypointListChanged(object sender)
        {
            this.Refresh();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// The manage fleet button has been pressed.
        /// </Summary>
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
        /// <Summary>
        /// Process the Next button being pressed.
        /// </Summary>
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
        /// <Summary>
        /// Process the previous button being pressed.
        /// </Summary>
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
        /// <Summary>
        /// If there is another waypoint before the selected one, display the fuel,
        /// time, etc. required for this leg.
        /// </Summary>
        /// <param name="index">Index of the waypoint to display.</param>
        /// ----------------------------------------------------------------------------
        public void DisplayLegDetails(int index)
        {
            Waypoint thisWaypoint = selectedFleet.Waypoints[index];
            Race race = ClientState.Data.PlayerRace;

            WaypointTasks.Text = thisWaypoint.Task;

            if (this.selectedFleet.Waypoints.Count == 1)
            {
                thisWaypoint.WarpFactor = 0;
            }

            selectedFleet.Waypoints[index] = thisWaypoint;
            this.warpFactor.Value = thisWaypoint.WarpFactor;
            warpText.Text = "Warp " + thisWaypoint.WarpFactor;

            if (index > 0 && thisWaypoint.WarpFactor > 0)
            {
                Waypoint from = selectedFleet.Waypoints[index - 1];
                Waypoint to = selectedFleet.Waypoints[index];
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
        /// <Summary>
        /// Add a new waypoint into the waypoint list control.
        /// </Summary>
        /// <param name="waypoint">A new waypoint to add.</param>
        /// ----------------------------------------------------------------------------
        public void AddWaypoint(Waypoint waypoint)
        {
            this.wayPoints.Items.Add(waypoint.Destination);
            this.wayPoints.SelectedIndex = this.wayPoints.Items.Count - 1;
            DisplayLegDetails(this.wayPoints.SelectedIndex);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set up all the display controls to reflect the selected fleet
        /// </Summary>
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

            Dictionary<string, int> designs = fleet.Composition;
            this.fleetComposition.Items.Clear();

            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(designs[key].ToString(System.Globalization.CultureInfo.InvariantCulture));
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
        /// <Summary>
        /// Property to set or get the fleet currently being displayed.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Fleet Value
        {
            set
            {
                if (value != null)
                {
                    SetFleetDetails(value);
                }
            }
            get 
            { 
                return this.selectedFleet; 
            }
        }


        /// <Summary>
        /// Get warp factor value.
        /// </Summary>
        public int Speed
        {
            get { return this.warpFactor.Value; }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Get waypoint task.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public string Task
        {
            get { return WaypointTasks.Text; }
        }

        #endregion
    }
}
