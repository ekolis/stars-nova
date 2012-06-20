#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 stars-nova
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

namespace Nova.ControlLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Waypoints;

    /// <summary>
    /// A dialog for transferring cargo between a planet and a ship.
    /// </summary>
    public partial class CargoDialog : Form
    {
        private Fleet fleet;
        private Cargo fleetCargo;
        private Cargo starCargo;
        private ClientData clientData;
        
        public Dictionary<CargoMode, CargoTask> Tasks {get; private set;}

        /// <summary>
        /// Initializes a new instance of the CargoDialog class.
        /// </summary>
        public CargoDialog(Fleet fleet, ClientData clientData)
        {
            InitializeComponent();
            cargoIron.ValueChanged += CargoIron_ValueChanged;
            cargoBoran.ValueChanged += CargoBoran_ValueChanged;
            cargoGerman.ValueChanged += CargoGermanium_ValueChanged;
            cargoColonistsInKilotons.ValueChanged += CargoColonists_ValueChanged;

            Tasks = new Dictionary<CargoMode, CargoTask>();

            SetTarget(fleet);

            this.clientData = clientData;
        }

        public void CargoIron_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Ironium + newValue > meterCargo.Maximum)
            {
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Ironium;
            }

            int total = fleetCargo.Ironium + starCargo.Ironium;

            if (newValue > total)
            {
                newValue = total;
            }

            fleetCargo.Ironium = newValue;
            starCargo.Ironium = total - newValue;

            UpdateMeters();
        }

        public void CargoBoran_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Boranium + newValue > meterCargo.Maximum)
            {
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Boranium;
            }

            int total = fleetCargo.Boranium + starCargo.Boranium;

            if (newValue > total)
            {
                newValue = total;
            }

            fleetCargo.Boranium = newValue;
            starCargo.Boranium = total - newValue;
            UpdateMeters();
        }

        public void CargoGermanium_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Germanium + newValue > meterCargo.Maximum)
            {
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Germanium;
            }

            int total = fleetCargo.Germanium + starCargo.Germanium;

            if (newValue > total)
            {
                newValue = total;
            }

            fleetCargo.Germanium = newValue;
            starCargo.Germanium = total - newValue;
            UpdateMeters();
        }

        public void CargoColonists_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.ColonistsInKilotons + newValue > meterCargo.Maximum)
            {
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.ColonistsInKilotons;
            }

            int total = fleetCargo.ColonistsInKilotons + starCargo.ColonistsInKilotons;

            if (newValue > total)
            {
                newValue = total;
            }

            fleetCargo.ColonistsInKilotons = newValue;
            starCargo.ColonistsInKilotons = total - newValue;
            UpdateMeters();
        }

        
        private void UpdateMeters()
        {
            cargoIron.Value = fleetCargo.Ironium;
            cargoBoran.Value = fleetCargo.Boranium;
            cargoGerman.Value = fleetCargo.Germanium;
            cargoColonistsInKilotons.Value = fleetCargo.ColonistsInKilotons;

            labelIron.Text = starCargo.Ironium + " kT";
            labelBoran.Text = starCargo.Boranium + " kT";
            labelGerman.Text = starCargo.Germanium + " kT";
            labelColonistsInKilotons.Text = starCargo.ColonistsInKilotons + " kT";

            meterCargo.CargoLevels = fleetCargo;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Tasks.Add(CargoMode.Load, new CargoTask());
            Tasks.Add(CargoMode.Unload, new CargoTask());
            Tasks[CargoMode.Load].Mode = CargoMode.Load;
            Tasks[CargoMode.Unload].Mode = CargoMode.Unload;
            
            // See if this is a Load, Unload or Mixed operation.            
            // If original fleet >= dialog fleet, then Unload. Else, Load.
            CargoMode mode;
            
            foreach(KeyValuePair<ResourceType, int> commodity in fleetCargo.Commodities)
            {
                if (fleet.Cargo[commodity.Key] >= fleetCargo[commodity.Key])
                {
                    mode = CargoMode.Unload;        
                }
                else
                {
                    mode = CargoMode.Load;
                }
                
                Tasks[mode].Amount[commodity.Key] = Math.Abs(fleetCargo[commodity.Key] - fleet.Cargo[commodity.Key]);
            }

            WaypointCommand command;
            foreach (CargoTask task in Tasks.Values)
            {
                if (task.Amount.Mass != 0)
                {
                    Waypoint waypoint = new Waypoint(fleet.Waypoints[0]); // copy first Waypoint
                    waypoint.Task = task;
                    command = new WaypointCommand(CommandMode.Add, waypoint, fleet.Key, 0); // insert always instead of first waypoint. Todo should be: add task always to actual waypoint zero.

                    clientData.Commands.Push(command);

                    if (command.isValid(clientData.EmpireState))
                    {
                        command.ApplyToState(clientData.EmpireState);
                        // Also perform it here, to update client state for manual xfer.
                        if (command.Waypoint.Task.isValid(fleet, fleet.InOrbit, clientData.EmpireState, null))
                        {
                            command.Waypoint.Task.Perform(fleet, fleet.InOrbit, clientData.EmpireState, null); // Load, Unload
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initialise the various fields in the dialog.
        /// </summary>
        /// <param name="targetFleet">The <see cref="Fleet"/> transferring cargo.</param>
        public void SetTarget(Fleet targetFleet)
        {
            fleet = targetFleet;                     
            fleetCargo = new Cargo(targetFleet.Cargo); // clone this so it can hold values in case we cancel
            starCargo = new Cargo();
            
            if (targetFleet.InOrbit.Type == ItemType.Star)
            {
                starCargo.Ironium = (targetFleet.InOrbit as Star).ResourcesOnHand.Ironium;
                starCargo.Boranium =  (targetFleet.InOrbit as Star).ResourcesOnHand.Boranium;
                starCargo.Germanium =  (targetFleet.InOrbit as Star).ResourcesOnHand.Germanium;
                starCargo.ColonistsInKilotons = (targetFleet.InOrbit as Star).Colonists / Global.ColonistsPerKiloton;
            }
            else
            {
                starCargo.Ironium = 0;
                starCargo.Boranium =  0;
                starCargo.Germanium =  0;
                starCargo.ColonistsInKilotons = 0;    
            }

            cargoIron.Maximum = targetFleet.TotalCargoCapacity;
            cargoBoran.Maximum = targetFleet.TotalCargoCapacity;
            cargoGerman.Maximum = targetFleet.TotalCargoCapacity;
            cargoColonistsInKilotons.Maximum = targetFleet.TotalCargoCapacity;

            meterCargo.Maximum = targetFleet.TotalCargoCapacity;

            UpdateMeters();
        }
    }
}
