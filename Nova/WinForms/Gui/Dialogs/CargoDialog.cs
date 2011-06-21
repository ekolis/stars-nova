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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the CargoDialog class.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public CargoDialog()
        {
            InitializeComponent();
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process cancel button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the OK button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OkButton_Click(object sender, System.EventArgs e)
        {
            fleet.Cargo.Ironium = this.ironiumTransfer.Value;
            fleet.Cargo.Boranium = boroniumTransfer.Value;
            fleet.Cargo.Germanium = this.germaniumTransfer.Value;
            fleet.Cargo.ColonistsInKilotons = colonistsTransfer.Value;

            Star star = fleet.InOrbit;
            star.ResourcesOnHand.Ironium -= this.ironiumTransfer.Taken;
            star.ResourcesOnHand.Boranium -= boroniumTransfer.Taken;
            star.ResourcesOnHand.Germanium -= this.germaniumTransfer.Taken;
            star.Colonists -= colonistsTransfer.Taken * Global.ColonistsPerKiloton;

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

            ironiumTransfer.Maximum = fleet.TotalCargoCapacity;
            boroniumTransfer.Maximum = fleet.TotalCargoCapacity;
            germaniumTransfer.Maximum = fleet.TotalCargoCapacity;
            colonistsTransfer.Maximum = fleet.TotalCargoCapacity;

            ironiumTransfer.Value = (int)fleet.Cargo.Ironium;
            boroniumTransfer.Value = (int)fleet.Cargo.Boranium;
            germaniumTransfer.Value = (int)fleet.Cargo.Germanium;
            colonistsTransfer.Value = (int)fleet.Cargo.ColonistsInKilotons;

            Star star = fleet.InOrbit;
            ironiumTransfer.Available = (int)star.ResourcesOnHand.Ironium;
            boroniumTransfer.Available = (int)star.ResourcesOnHand.Boranium;
            germaniumTransfer.Available = (int)star.ResourcesOnHand.Germanium;
            colonistsTransfer.Available = (int)star.Colonists / Global.ColonistsPerKiloton;

            ironiumTransfer.Limit = cargoBay;
            boroniumTransfer.Limit = cargoBay;
            germaniumTransfer.Limit = cargoBay;
            colonistsTransfer.Limit = cargoBay;

            cargoBay.Maximum = fleet.TotalCargoCapacity;
            cargoBay.Value = fleet.Cargo.Mass;
        }
    }
}
