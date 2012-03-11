#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011, 2012 The Stars-Nova Project.
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
    using Nova.ControlLibrary;
    using Nova.WinForms.Gui.Dialogs;

    /// <Summary>
    /// Ship Detail display panel.
    /// </Summary>
    public partial class FleetDetail : System.Windows.Forms.UserControl
    {
        private readonly EmpireData empireState;
        private Stack<ICommand> commands;

        private Fleet selectedFleet;
        private Dictionary<long, Fleet> fleetsAtLocation = new Dictionary<long, Fleet>();

        /// <Summary>
        /// This event should be fired when the selected Fleet
        /// changes.
        /// </Summary>
        public event SummarySelectionChanged SummarySelectionChangedEvent;

        public event DetailSelectionChanged DetailSelectionChangedEvent;
        
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
        /// Property to set or get the fleet currently being displayed.
        /// </Summary>
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
        /// Initializes a new instance of the FleetDetail class.
        /// </Summary>
        public FleetDetail(ClientData clientState)
        {
            this.empireState = clientState.EmpireState;
            this.commands = clientState.Commands;
            
            InitializeComponent();
        }

        /// <Summary>
        /// Called when the warp factor slider is moved.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void WaypointSpeedChanged(object sender, System.EventArgs e)
        {
            warpText.Text = "Warp " + warpFactor.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (wayPoints.SelectedItems.Count > 0)
            {
                int index = wayPoints.SelectedIndices[0];
                
                Waypoint editedWaypoint = new Waypoint();
                
                editedWaypoint.Destination = selectedFleet.Waypoints[index].Destination;
                editedWaypoint.Position = selectedFleet.Waypoints[index].Position;
                editedWaypoint.Task = selectedFleet.Waypoints[index].Task;                
                
                editedWaypoint.WarpFactor = warpFactor.Value;
                
                WaypointCommand command = new WaypointCommand(CommandMode.Edit, editedWaypoint, selectedFleet.Key, index);
                
                // Minimizing clutter. If the last command was a speed/task change for this same waypoint,
                // then just use that instead of adding a potentialy huge pile of speed edits.
                
                ICommand lastCommand = commands.Peek();
                
                // Make sure it's the same waypoint except for speed/task, and that it's not a freshly added
                // waypoint.
                if (lastCommand is WaypointCommand)
                {
                    if ((lastCommand as WaypointCommand).Waypoint.Destination == editedWaypoint.Destination &&
                        (lastCommand as WaypointCommand).Waypoint.Position == editedWaypoint.Position &&
                        (lastCommand as WaypointCommand).Mode != CommandMode.Add)
                    {
                        //Discard it.
                        commands.Pop();
                    }
                }
                
                commands.Push(command);
            
                if (command.isValid(empireState))
                {
                    command.ApplyToState(empireState);
                }

                DisplayLegDetails(index);
            }
        }
        
        /// <Summary>
        /// On a waypoint being selected update the speed and tasks controls to
        /// reflect the values of the selected waypoint.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void WaypointSelection(object sender, System.EventArgs e)
        {
            if (wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = wayPoints.SelectedIndices[0];
            DisplayLegDetails(index);
        }
        
        /// <Summary>
        /// Cargo button pressed. Pop up the cargo transfer dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void CargoButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                using (CargoDialog cargoDialog = new CargoDialog())
                {
                    cargoDialog.SetTarget(selectedFleet);
                    cargoDialog.ShowDialog();
                    UpdateCargoMeters();
                    Invalidate();
                }

                meterCargo.CargoLevels = selectedFleet.Cargo;

            }
            catch
            {
                Report.Debug("FleetDetail.cs : CargoButton_Click() - Failed to open cargo dialog.");
            }
                
        }
        
        /// <Summary>
        /// Catch the backspace key to delete a fleet waypoint.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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
            
            WaypointCommand command = new WaypointCommand(CommandMode.Delete, selectedFleet.Key, index);
            
            commands.Push(command);
            
            if (command.isValid(empireState))
            {
                command.ApplyToState(empireState);
            }
            
            // Refresh the waypoint list on the GUI.
            WaypointListChanged(this);                     

            if (RefreshStarMapEvent != null)
            {
                RefreshStarMapEvent();
            }
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
                    WaypointCommand command = new WaypointCommand(CommandMode.Delete, selectedFleet.Key, index);

                    commands.Push(command);
                    
                    if (command.isValid(empireState))
                    {
                        command.ApplyToState(empireState);
                    }
                    
                    // Refresh the waypoint list on the GUI.
                    WaypointListChanged(this);
                    
                    if (RefreshStarMapEvent != null)
                    {
                        RefreshStarMapEvent();
                    }
                }
                e.Handled = true;
            }
        }

        /// <Summary>
        /// If a waypoint task changes, and a waypoint is selected, change the task at
        /// that waypoint.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void WaypointTaskChanged(object sender, EventArgs e)
        {
            if (wayPoints.SelectedItems.Count <= 0)
            {
                return;
            }

            int index = wayPoints.SelectedIndices[0];
            
            Waypoint waypoint = selectedFleet.Waypoints[index];
            
            Waypoint editedWaypoint = new Waypoint();
                
            editedWaypoint.Destination = selectedFleet.Waypoints[index].Destination;
            editedWaypoint.Position = selectedFleet.Waypoints[index].Position;
            editedWaypoint.WarpFactor = selectedFleet.Waypoints[index].WarpFactor;                
            
            editedWaypoint.SetTask(WaypointTasks.Text);
            
            WaypointCommand command = new WaypointCommand(CommandMode.Edit, editedWaypoint, selectedFleet.Key, index);
            
            // Minimizing clutter. If the last command was a speed/task change for this same waypoint,
            // then just use that instead of adding a potentialy huge pile of task edits.
            
            ICommand lastCommand = commands.Peek();
            
            // Make sure it's the same waypoint except for speed/task, and that it's not a freshly added
            // waypoint.
            if (lastCommand is WaypointCommand)
            {
                if ((lastCommand as WaypointCommand).Waypoint.Destination == editedWaypoint.Destination &&
                    (lastCommand as WaypointCommand).Waypoint.Position == editedWaypoint.Position &&
                    (lastCommand as WaypointCommand).Mode != CommandMode.Add)
                {
                    //Discard it.
                    commands.Pop();
                }
            }
            
            commands.Push(command);
        
            if (command.isValid(empireState))
            {
                command.ApplyToState(empireState);
            }
        }

        /// <Summary>
        /// The manage fleet button has been pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void SplitFleetClick(object sender, EventArgs e)
        {
            DoSplitMerge(null);
        }

        /// <Summary>
        /// Process the Next button being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextFleet_Click(object sender, System.EventArgs e)
        {
            if (empireState.OwnedFleets.Count == 1)
            {
                previousFleet.Enabled = false;
                nextFleet.Enabled = false;
                return;
            }

            previousFleet.Enabled = true;
            nextFleet.Enabled = true;

            selectedFleet = empireState.OwnedFleets.GetNext(empireState.OwnedFleets[selectedFleet.Key]);
            
            UpdateListeners(selectedFleet);
        }


        /// <Summary>
        /// Process the previous button being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PreviousFleet_Click(object sender, EventArgs e)
        {
            if (empireState.OwnedFleets.Count == 1)
            {
                previousFleet.Enabled = false;
                nextFleet.Enabled = false;
                return;
            }

            previousFleet.Enabled = true;
            nextFleet.Enabled = true;

            selectedFleet = empireState.OwnedFleets.GetPrevious(empireState.OwnedFleets[selectedFleet.Key]);

            UpdateListeners(selectedFleet);
        }

        /// <Summary>
        /// If there is another waypoint before the selected one, display the fuel,
        /// time, etc. required for this leg.
        /// </Summary>
        /// <param name="index">Index of the waypoint to display.</param>
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

                double fuelUsed = selectedFleet.FuelConsumption(to.WarpFactor, empireState.Race)

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

                    fuelRequired += selectedFleet.FuelConsumption(warp, empireState.Race) * travelTime;
                }
                previous = waypoint;
            }

            routeFuelUse.Text = fuelRequired.ToString("f1");
        }

        /// <Summary>
        /// Set up all the display controls to reflect the selected fleet
        /// </Summary>
        /// <param name="fleet">The selected fleet.</param>
        private void SetFleetDetails(Fleet selectedFleet)
        {
            if (selectedFleet == null)
            {
                return;
            }

            this.selectedFleet = selectedFleet;
            
            if (empireState.OwnedFleets.Count > 1)
            {
                previousFleet.Enabled = true;
                nextFleet.Enabled = true;
            }
            else
            {
                previousFleet.Enabled = false;
                previousFleet.Enabled = false;
            }

            groupFleetSelection.Text = "Fleet " + selectedFleet.Name;

            Dictionary<ShipDesign, int> designs = selectedFleet.Composition;
            fleetComposition.Items.Clear();

            foreach (KeyValuePair<ShipDesign, int> design in designs)
            {
                ListViewItem listItem = new ListViewItem(design.Key.Name);
                listItem.SubItems.Add(design.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                fleetComposition.Items.Add(listItem);
            }

            wayPoints.DataSource = selectedFleet.Waypoints;
            wayPoints.DisplayMember = "Destination";
            wayPoints.SelectedIndex = wayPoints.Items.Count - 1;
            
            DisplayLegDetails(wayPoints.Items.Count - 1);

            // If we are in orbit around a planet and we have a cargo carrying
            // capacity, enable the Cargo Dialog Button.
            bool inOrbit = selectedFleet.InOrbit != null;
            groupOrbitPlanet.Text = inOrbit ? "Orbiting " + selectedFleet.InOrbit.Name : "In deep space";
            buttonCargo.Enabled = inOrbit && selectedFleet.TotalCargoCapacity > 0;            
            buttonGotoPlanet.Enabled = inOrbit;

            List<ComboBoxItem<Fleet>> fleets = new List<ComboBoxItem<Fleet>>();
            fleetsAtLocation = new Dictionary<long, Fleet>();
            foreach (Fleet other in empireState.OwnedFleets.Values)
            {
                if (selectedFleet.Position == other.Position && !other.IsStarbase && selectedFleet.Key != other.Key)
                {
                    fleets.Add(new ComboBoxItem<Fleet>(other.Name, other));
                    fleetsAtLocation[other.Key] = other;
                }
            }
            
            fleets.Sort(delegate(ComboBoxItem<Fleet> x, ComboBoxItem<Fleet> y) 
            { 
                return x.DisplayName.CompareTo(y.DisplayName); 
            });
            
            comboOtherFleets.Items.Clear();
            bool haveFleets = fleets.Count > 0;
            
            if (haveFleets)
            {
                comboOtherFleets.Items.AddRange(fleets.ToArray());
                comboOtherFleets.SelectedIndex = 0;
            }
            
            buttonMerge.Enabled = haveFleets;
            buttonCargoXfer.Enabled = haveFleets;
            buttonGotoFleet.Enabled = haveFleets;
            
            UpdateCargoMeters();
            Invalidate();
        }

        private void UpdateCargoMeters()
        {
            meterFuel.Maximum = selectedFleet.TotalFuelCapacity;
            meterFuel.Value = (int)selectedFleet.FuelAvailable;
            meterCargo.Maximum = selectedFleet.TotalCargoCapacity;
            meterCargo.CargoLevels = selectedFleet.Cargo;
            ComboOtherFleets_SelectedIndexChanged(null, null); // Updates the other meters to current selection          
        }



        private Fleet GetSelectedFleetAtLocation()
        {
            if (comboOtherFleets.SelectedItem == null)
            {
                return null;
            }

            ComboBoxItem<Fleet> selected = comboOtherFleets.SelectedItem as ComboBoxItem<Fleet>;
            Fleet fleet;
            if (!fleetsAtLocation.TryGetValue(selected.Tag.Key, out fleet))
            {
                return null;
            }
            return fleet;
        }

        private void ComboOtherFleets_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ButtonGotoPlanet_Click(object sender, EventArgs e)
        {
            if (selectedFleet != null && selectedFleet.InOrbit != null)
            {
                UpdateListeners(selectedFleet.InOrbit);
            }
        }

        private void ButtonGotoFleet_Click(object sender, EventArgs e)
        {
            Fleet newFleet = GetSelectedFleetAtLocation();

            // Inform of the selection change to all listening objects.
            UpdateListeners(newFleet);
        }

        private void ButtonMerge_Click(object sender, EventArgs e)
        {
            Fleet newFleet = GetSelectedFleetAtLocation();
            DoSplitMerge(newFleet);
        }

        /// <summary>
        /// Raise the Split/Merge fleet dialog.
        /// </summary>
        /// <param name="otherFleet">The second fleet to merge with or split into (may be null)</param>
        private void DoSplitMerge(Fleet otherFleet)
        {
            using (SplitFleetDialog splitFleet = new SplitFleetDialog())
            {
                splitFleet.SetFleet(selectedFleet, otherFleet);
                if (splitFleet.ShowDialog() == DialogResult.OK)
                {
                    // TODO need to create a new fleet for split. The below will ignore the changes
                    if (otherFleet == null)
                    {
                        // Need a new fleet. Clone existing then change stuff
                        otherFleet = MakeNewFleet(selectedFleet);
                        empireState.OwnedFleets[otherFleet.Key] = otherFleet;
                    }
                    splitFleet.ReassignShips(selectedFleet, otherFleet);

                    // Now remove any empty fleets...
                    if (otherFleet.FleetShips.Count == 0)
                    {
                        empireState.OwnedFleets.Remove(otherFleet.Key);
                    }
                    if (selectedFleet.FleetShips.Count == 0)
                    {
                        empireState.OwnedFleets.Remove(selectedFleet.Key);
                        // Set the other fleet to be selected
                        selectedFleet = otherFleet;
                    }
                }
                ReselectFleetToUpdateUi();
            }
        }

        private Fleet MakeNewFleet(Fleet existing)
        {
            Fleet newFleet = new Fleet(empireState.GetNextFleetKey());

            newFleet.Type = ItemType.Fleet;

            // Have one waypoint to reflect the fleet's current position and the
            // planet it is in orbit around.

            Waypoint w = new Waypoint();
            w.Position = existing.Waypoints[0].Position;
            w.Destination = existing.Waypoints[0].Destination;
            w.WarpFactor = 0;

            newFleet.Waypoints.Add(w);

            // Inititialise the fleet elements that come from the star.

            newFleet.Position = existing.Position;
            newFleet.InOrbit = existing.InOrbit;

            newFleet.Name = "New Fleet #" + newFleet.Id;

            return newFleet;
        }

        private void ButtonCargoXfer_Click(object sender, EventArgs e)
        {
            using (CargoTransferDialog dia = new CargoTransferDialog())
            {
                Fleet other = GetSelectedFleetAtLocation();
                if (other != null && other.Key != selectedFleet.Key)
                {
                    dia.SetFleets(selectedFleet, other);
                    if (dia.ShowDialog() == DialogResult.OK)
                    {
                        selectedFleet.Cargo = dia.LeftCargo;
                        selectedFleet.FuelAvailable = dia.LeftFuel;
                        other.Cargo = dia.RightCargo;
                        other.FuelAvailable = dia.RightFuel;

                        UpdateCargoMeters();
                        Invalidate();
                    }
                }
            }
        }
        
        private void RenameClick(object sender, EventArgs e)
        {
            using (RenameFleetDialog dia = new RenameFleetDialog())
            {
                dia.FleetName = selectedFleet.Name;
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    selectedFleet.Name = dia.FleetName;
                    // Reselect fleet to update all UI
                    ReselectFleetToUpdateUi();
                }
            }
        }

        // DEPRECATED. Left here in case things ASPLODE!
        private void ReselectFleetToUpdateUi()
        {
            DetailSelectionArgs detailArgs = new DetailSelectionArgs(selectedFleet);
            if (DetailSelectionChangedEvent != null)
            {
                DetailSelectionChangedEvent(this, detailArgs);
            }
        }
        
        private void UpdateListeners(Item item)
        {
            if (DetailSelectionChangedEvent != null && item.Owner == empireState.Id)
            {
                DetailSelectionChangedEvent(this, new DetailSelectionArgs(item));
            }
            
            if (SummarySelectionChangedEvent != null)
            {
                SummarySelectionArgs summaryArgs;
                
                if (item.Type == ItemType.Fleet)
                {
                    summaryArgs = new SummarySelectionArgs(empireState.FleetReports[item.Key]);
                }
                else
                {
                    summaryArgs = new SummarySelectionArgs(empireState.StarReports[item.Name]);
                }
                
                SummarySelectionChangedEvent(this, summaryArgs);
            }
            
            if (CursorChangedEvent != null)
            {
                CursorChangedEvent(this, new CursorArgs((Point)item.Position));
            }    
        }

        // Handles updating of the waypointList in the GUI when waypoints
        // are added, removed, or edited.
        public void WaypointListChanged(object sender)
        {
            ((CurrencyManager)wayPoints.BindingContext[wayPoints.DataSource]).Refresh();
            
            wayPoints.SelectedIndex = wayPoints.Items.Count - 1;
        }
    }
}
