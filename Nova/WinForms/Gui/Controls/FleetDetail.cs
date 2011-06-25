#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project.
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

using Nova.WinForms.Gui.Dialogs;

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
        private StarIntelList starReports;
        private Dictionary<string, Fleet> allFleets; // FIXME:(???) Do we need allFleets here? Can't we use the player's fleets instead? -Aeglos 21 Jun 11 
        private List<Fleet> playerFleets;
        private List<string> deletedFleets;
        private Race playerRace;
            
        private Dictionary<string, Fleet> fleetsAtLocation = new Dictionary<string, Fleet>();

        /// <Summary>
        /// This event should be fired when the selected Fleet
        /// changes.
        /// </Summary>
        public event FleetSelectionChanged FleetSelectionChangedEvent;

        public event StarSelectionChanged StarSelectionChangedEvent;
        
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

        /// <Summary>
        /// Initializes a new instance of the FleetDetail class.
        /// </Summary>
        public FleetDetail(StarIntelList starReports,
                           Dictionary<string, Fleet> allFleets,
                           List<Fleet> playerFleets,
                           List<string> deletedFleets,
                           Race playerRace)
        {
            this.starReports = starReports;
            this.allFleets = allFleets;
            this.playerFleets = playerFleets;
            this.deletedFleets = deletedFleets;
            this.playerRace = playerRace;
            
            InitializeComponent();
        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Called when the warp factor slider is moved.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SpeedChanged(object sender, System.EventArgs e)
        {
            warpText.Text = "Warp " + warpFactor.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (wayPoints.SelectedItems.Count > 0)
            {
                int index = wayPoints.SelectedIndices[0];
                Waypoint waypoint = selectedFleet.Waypoints[index];
                waypoint.WarpFactor = warpFactor.Value;

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
            if (wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = wayPoints.SelectedIndices[0];
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

                cargoDialog.SetTarget(selectedFleet);
                cargoDialog.ShowDialog();
                cargoDialog.Dispose();
                
                meterCargo.CargoLevels = selectedFleet.Cargo;

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
            if (wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = wayPoints.SelectedIndices[0];

            // backspace
            if (index == 0 || !(e.KeyChar == (char)8)) 
            {
                return;
            }

            selectedFleet.Waypoints.RemoveAt(index);
            wayPoints.Items.RemoveAt(index);
            wayPoints.SelectedIndex = wayPoints.Items.Count - 1;

            if( RefreshStarMapEvent != null )
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
                int index = wayPoints.SelectedIndices[0];
                if (index > 0)
                {
                    selectedFleet.Waypoints.RemoveAt(index);
                    wayPoints.Items.RemoveAt(index);
                    wayPoints.SelectedIndex = wayPoints.Items.Count - 1;

                    if( RefreshStarMapEvent != null )
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
            if (wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = wayPoints.SelectedIndices[0];
            Waypoint waypoint = selectedFleet.Waypoints[index];

            waypoint.SetTask(WaypointTasks.Text);
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// A waypoint has been added or deleted.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// ----------------------------------------------------------------------------
        public void WaypointListChanged(object sender)
        {
            Refresh();
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
            ManageFleetDialog manageDialog = new ManageFleetDialog(allFleets, deletedFleets, playerRace.Name);
            manageDialog.ManagedFleet = selectedFleet;
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
            if (playerFleets.Count == 1)
            {
                previousFleet.Enabled = false;
                nextFleet.Enabled = false;
                return;
            }

            previousFleet.Enabled = true;
            nextFleet.Enabled = true;

            if (currentFleet < playerFleets.Count - 1)
            {
                currentFleet++;
            }
            else
            {
                currentFleet = 0;
            }
            
            Fleet current = playerFleets[currentFleet];
            
            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(current, selectedFleet);
            CursorArgs cursorArgs = new CursorArgs((Point)selectedFleet.Position);
            
            // Inform of the selection change to all listening objects.
            if( FleetSelectionChangedEvent  != null )
                FleetSelectionChangedEvent(this, selectionArgs);
            if( CursorChangedEvent != null )
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
            if (playerFleets.Count == 1)
            {
                previousFleet.Enabled = false;
                nextFleet.Enabled = false;
                return;
            }

            previousFleet.Enabled = true;
            nextFleet.Enabled = true;

            if (currentFleet > 0)
            {
                currentFleet--;
            }
            else
            {
                currentFleet = playerFleets.Count - 1;
            }

            Fleet current = playerFleets[currentFleet];

            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(current, selectedFleet);
            CursorArgs cursorArgs = new CursorArgs((Point)selectedFleet.Position);

            // Inform of the selection change to all listening objects.
            if (FleetSelectionChangedEvent != null)
                FleetSelectionChangedEvent(this, selectionArgs);
            if (CursorChangedEvent != null)
                CursorChangedEvent(this, cursorArgs);
        }

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

            WaypointTasks.Text = thisWaypoint.GetTask();

            if (selectedFleet.Waypoints.Count == 1)
            {
                thisWaypoint.WarpFactor = 0;
            }

            selectedFleet.Waypoints[index] = thisWaypoint;
            warpFactor.Value = thisWaypoint.WarpFactor;
            warpText.Text = "Warp " + thisWaypoint.WarpFactor;

            if (index > 0 && thisWaypoint.WarpFactor > 0)
            {
                Waypoint from = selectedFleet.Waypoints[index - 1];
                Waypoint to = selectedFleet.Waypoints[index];
                double distance = PointUtilities.Distance(from.Position, to.Position);

                double time = distance / (to.WarpFactor * to.WarpFactor);

                double fuelUsed = selectedFleet.FuelConsumption(to.WarpFactor, playerRace)

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

            foreach (Waypoint waypoint in selectedFleet.Waypoints)
            {
                if (previous != null && waypoint.WarpFactor > 0)
                {
                    double distance = PointUtilities.Distance(waypoint.Position, previous.Position);
                    int warp = waypoint.WarpFactor;
                    double speed = warp * warp;
                    double travelTime = distance / speed;

                    fuelRequired += selectedFleet.FuelConsumption(warp, playerRace) * travelTime;
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
            wayPoints.Items.Add(waypoint.Destination);
            wayPoints.SelectedIndex = wayPoints.Items.Count - 1;
            DisplayLegDetails(wayPoints.SelectedIndex);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set up all the display controls to reflect the selected fleet
        /// </Summary>
        /// <param name="fleet">The selected fleet.</param>
        /// ----------------------------------------------------------------------------
        private void SetFleetDetails(Fleet fleet)
        {
            selectedFleet = fleet;

            for (int i = 0; i < playerFleets.Count; ++i)
            {
                if (fleet.Name == playerFleets[i].Name)
                {
                    currentFleet = i;
                    break;
                }
            }

            if (playerFleets.Count > 1)
            {
                previousFleet.Enabled = true;
                nextFleet.Enabled = true;
            }
            else
            {
                previousFleet.Enabled = false;
                previousFleet.Enabled = false;
            }

            groupFleetSelection.Text = "Fleet " + fleet.Name;

            Dictionary<string, int> designs = fleet.Composition;
            fleetComposition.Items.Clear();

            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(designs[key].ToString(System.Globalization.CultureInfo.InvariantCulture));
                fleetComposition.Items.Add(listItem);
            }

            meterFuel.Maximum = fleet.TotalFuelCapacity;
            meterFuel.Value = (int)fleet.FuelAvailable;
            meterCargo.Maximum = fleet.TotalCargoCapacity;
            meterCargo.CargoLevels = fleet.Cargo;

            wayPoints.Items.Clear();
            foreach (Waypoint waypoint in fleet.Waypoints)
            {
                wayPoints.Items.Add(waypoint.Destination);
            }

            wayPoints.SelectedIndex = wayPoints.Items.Count - 1;
            DisplayLegDetails(wayPoints.Items.Count - 1);

            // If we are in orbit around a planet and we have a cargo carrying
            // capacity, enable the Cargo Dialog Button.
            bool inOrbit = fleet.InOrbit != null;
            groupOrbitPlanet.Text = inOrbit ? "Orbiting " + fleet.InOrbit.Name : "In deep space";
            buttonCargo.Enabled = inOrbit && fleet.TotalCargoCapacity > 0;            
            buttonGotoPlanet.Enabled = inOrbit;           

            List<String> fleetnames = new List<string>();
            fleetsAtLocation = new Dictionary<string, Fleet>();
            foreach (Fleet other in playerFleets)
            {
                if (fleet.Position == other.Position && !other.IsStarbase && fleet.FleetID != other.FleetID)
                {
                    fleetnames.Add(other.Name);
                    fleetsAtLocation[other.Name] = other;
                }
            }
            fleetnames.Sort();
            comboOtherFleets.Items.Clear();
            bool haveFleets = fleetnames.Count > 0;
            if ( haveFleets )
            {
                comboOtherFleets.Items.AddRange(fleetnames.ToArray());
                comboOtherFleets.SelectedIndex = 0;
            }
            buttonMerge.Enabled = haveFleets;
            buttonCargoXfer.Enabled = haveFleets && fleet.TotalCargoCapacity > 0;
            buttonGotoFleet.Enabled = haveFleets;
            
            Invalidate();
        }

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
                return selectedFleet; 
            }
        }
        
        /// <Summary>
        /// Get warp factor value.
        /// </Summary>
        public int Speed
        {
            get { return warpFactor.Value; }
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

        private Fleet GetSelectedFleetAtLocation()
        {
            if (comboOtherFleets.SelectedItem == null)
                return null;

            Fleet fleet;
            if (!fleetsAtLocation.TryGetValue(comboOtherFleets.SelectedItem.ToString(), out fleet))
                return null;

            return fleet;
        }

        private void comboOtherFleets_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetAtLocation();
            if (fleet == null)
            {
                meterFuelOther.Value = 0;
                meterFuelOther.Maximum = 0;
                meterCargoOther.CargoLevels = new Cargo();
                meterCargoOther.Maximum = 0;
            }
            else
            {                
                meterFuelOther.Maximum = fleet.TotalFuelCapacity;
                meterFuelOther.Value = (int)fleet.FuelAvailable;
                meterCargoOther.Maximum = fleet.TotalCargoCapacity;
                meterCargoOther.CargoLevels = fleet.Cargo;
            }
            Invalidate();
        }

        private void buttonGotoPlanet_Click(object sender, EventArgs e)
        {
            if (selectedFleet != null && selectedFleet.InOrbit != null)
            {
                if (StarSelectionChangedEvent != null)
                    StarSelectionChangedEvent(this, new StarSelectionArgs(selectedFleet.InOrbit));
                if( CursorChangedEvent != null )
                    CursorChangedEvent(this, new CursorArgs((Point)selectedFleet.InOrbit.Position));
            }
        }

        private void buttonGotoFleet_Click(object sender, EventArgs e)
        {
            Fleet newFleet = GetSelectedFleetAtLocation();
            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(newFleet, newFleet);
            CursorArgs cursorArgs = new CursorArgs((Point)newFleet.Position);

            // Inform of the selection change to all listening objects.
            if (FleetSelectionChangedEvent != null)
                FleetSelectionChangedEvent(this, selectionArgs);
            if (CursorChangedEvent != null)
                CursorChangedEvent(this, cursorArgs);
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "This will allow fleet merging");
        }

        private void buttonCargoXfer_Click(object sender, EventArgs e)
        {
            using (CargoTransferDialog dia = new CargoTransferDialog())
            {
                dia.ShowDialog();
            }
        }
    }
}
