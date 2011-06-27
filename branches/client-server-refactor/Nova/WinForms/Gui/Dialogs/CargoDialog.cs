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
// Dialog for transferring cargo between a planet and a ship.
// ===========================================================================
#endregion

using System;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.ControlLibrary
{
    /// <summary>
    /// A dialog for transferring cargo between a planet and a ship.
    /// </summary>
    public partial class CargoDialog : Form
    {
        private Fleet fleet;
        private Cargo fleetCargo;
        private Cargo starCargo;

        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the CargoDialog class.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public CargoDialog()
        {
            InitializeComponent();
            cargoIron.ValueChanged += cargoIron_ValueChanged;
            cargoBoron.ValueChanged += cargoBoron_ValueChanged;
            cargoGerman.ValueChanged += cargoGerman_ValueChanged;
            cargoColonists.ValueChanged += cargoColonists_ValueChanged;
        }

        void cargoIron_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Ironium + newValue > meterCargo.Maximum)
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Ironium;

            int total = fleetCargo.Ironium + starCargo.Ironium;

            if (newValue > total)
                newValue = total;

            fleetCargo.Ironium = newValue;
            starCargo.Ironium = total - newValue;

            UpdateMeters();
        }

        void cargoBoron_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Boranium + newValue > meterCargo.Maximum)
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Boranium;

            int total = fleetCargo.Boranium + starCargo.Boranium;

            if (newValue > total)
                newValue = total;

            fleetCargo.Boranium = newValue;
            starCargo.Boranium = total - newValue;
            UpdateMeters();
        }

        void cargoGerman_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.Germanium + newValue > meterCargo.Maximum)
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.Germanium;

            int total = fleetCargo.Germanium + starCargo.Germanium;

            if (newValue > total)
                newValue = total;

            fleetCargo.Germanium = newValue;
            starCargo.Germanium = total - newValue;
            UpdateMeters();
        }

        void cargoColonists_ValueChanged(int newValue)
        {
            if (fleetCargo.Mass - fleetCargo.ColonistsInKilotons + newValue > meterCargo.Maximum)
                newValue = meterCargo.Maximum - fleetCargo.Mass + fleetCargo.ColonistsInKilotons;

            int total = fleetCargo.ColonistsInKilotons + starCargo.ColonistsInKilotons;

            if (newValue > total)
                newValue = total;

            fleetCargo.ColonistsInKilotons = newValue;
            starCargo.ColonistsInKilotons = total - newValue;
            UpdateMeters();
        }

        
        private void UpdateMeters()
        {
            cargoIron.Value = fleetCargo.Ironium;
            cargoBoron.Value = fleetCargo.Boranium;
            cargoGerman.Value = fleetCargo.Germanium;
            cargoColonists.Value = fleetCargo.ColonistsInKilotons;

            labelIron.Text = starCargo.Ironium + "KT";
            labelBoron.Text = starCargo.Boranium + "KT";
            labelGerman.Text = starCargo.Germanium + "KT";
            labelColonists.Text = starCargo.ColonistsInKilotons + "KT";

            meterCargo.CargoLevels = fleetCargo;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            fleet.Cargo = fleetCargo;

            Star star = fleet.InOrbit;
            star.ResourcesOnHand.Ironium = starCargo.Ironium;
            star.ResourcesOnHand.Boranium = starCargo.Boranium;
            star.ResourcesOnHand.Germanium = starCargo.Germanium;
            int remainder = star.Colonists % Global.ColonistsPerKiloton;
            star.Colonists = starCargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
            star.Colonists += remainder;

            DialogResult = DialogResult.OK;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialise the various fields in the dialog.
        /// </summary>
        /// <param name="targetFleet">The <see cref="Fleet"/> transferring cargo.</param>
        /// ----------------------------------------------------------------------------
        public void SetTarget(Fleet targetFleet)
        {
            fleet = targetFleet;

            fleetCargo = new Cargo(targetFleet.Cargo); // clone this so it can hold values in case we cancel
            starCargo = new Cargo();
            starCargo.Ironium = targetFleet.InOrbit.ResourcesOnHand.Ironium;
            starCargo.Boranium =  targetFleet.InOrbit.ResourcesOnHand.Boranium;
            starCargo.Germanium =  targetFleet.InOrbit.ResourcesOnHand.Germanium;
            starCargo.ColonistsInKilotons = (targetFleet.InOrbit.Colonists/Global.ColonistsPerKiloton);

            cargoIron.Maximum = targetFleet.TotalCargoCapacity;
            cargoBoron.Maximum = targetFleet.TotalCargoCapacity;
            cargoGerman.Maximum = targetFleet.TotalCargoCapacity;
            cargoColonists.Maximum = targetFleet.TotalCargoCapacity;

            meterCargo.Maximum = targetFleet.TotalCargoCapacity;

            UpdateMeters();
        }
    }
}
