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
// Manage an individual fleet
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Manage the details of a fleet (composition, etc.).
    /// </summary>
    public partial class ManageFleetDialog : Form
    {
        private readonly Hashtable allFleets;
        private Fleet selectedFleet;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the ManageFleetDialog class.
        /// </summary>
        public ManageFleetDialog()
        {
            InitializeComponent();
            this.allFleets = ClientState.Data.InputTurn.AllFleets;
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Rename a fleet. The rename is only allowed if the fleet access key is
        /// unique.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RenameButton_Click(object sender, EventArgs e)
        {
            RenameFleet renameDialog = new RenameFleet();
            renameDialog.ExistingName.Text = this.fleetName.Text;

            renameDialog.ShowDialog();
            renameDialog.Dispose();

            this.fleetName.Text = this.selectedFleet.Name;


        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A co-located fleet is selected, activate the merge facility
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CoLocatedFleets_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mergeButton.Enabled = true;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Merge the ships from a co-located fleet into the selected fleet
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MergeButton_Click(object sender, EventArgs e)
        {
            string fleetName = this.coLocatedFleets.SelectedItems[0].Text;
            string fleetKey = ClientState.Data.RaceName + "/" + fleetName;

            Fleet fleetToMerge = this.allFleets[fleetKey] as Fleet;
            foreach (Ship ship in fleetToMerge.FleetShips)
            {
                this.selectedFleet.FleetShips.Add(ship);
            }

            this.allFleets.Remove(fleetKey);
            ClientState.Data.DeletedFleets.Add(fleetKey);
            UpdateDialogDetails();

            this.mergeButton.Enabled = false;
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the dialog fields
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void UpdateDialogDetails()
        {
            this.fleetName.Text = this.selectedFleet.Name;
            Hashtable designs = this.selectedFleet.Composition;

            this.fleetComposition.Items.Clear();
            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(((int)designs[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                this.fleetComposition.Items.Add(listItem);
            }

            this.coLocatedFleets.Items.Clear();

            foreach (Fleet fleet in this.allFleets.Values)
            {
                if (fleet.Name != this.selectedFleet.Name)
                {
                    if (fleet.Position == this.selectedFleet.Position)
                    {
                        this.coLocatedFleets.Items.Add(fleet.Name);
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select the fleet to be managed.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Fleet ManagedFleet
        {
            set
            {
                this.selectedFleet = value;
                UpdateDialogDetails();
            }
            get
            {
                return this.selectedFleet;
            }
        }

        #endregion

    }
}
