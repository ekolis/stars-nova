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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Manage the details of a fleet (composition, etc.).
    /// </summary>
    public partial class ManageFleetDialog : Form
    {
        private Fleet SelectedFleet = null;
        private Hashtable AllFleets = null;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ManageFleetDialog()
        {
            InitializeComponent();
            AllFleets = ClientState.Data.InputTurn.AllFleets;
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Rename a fleet. The rename is only allowed if the fleet access key is
        /// unique.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RenameButton_Click(object sender, EventArgs e)
        {
            RenameFleet renameDialog = new RenameFleet();
            renameDialog.ExistingName.Text = FleetName.Text;

            renameDialog.ShowDialog();
            renameDialog.Dispose();

            FleetName.Text = SelectedFleet.Name;


        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A co-located fleet is selected, activate the merge facility
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CoLocatedFleets_SelectedIndexChanged(object sender,
                                                          EventArgs e)
        {
            MergeButton.Enabled = true;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Merge the ships from a co-located fleet into the selected fleet
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MergeButton_Click(object sender, EventArgs e)
        {
            string fleetName = CoLocatedFleets.SelectedItems[0].Text;
            string fleetKey = ClientState.Data.RaceName + "/" + fleetName;

            Fleet fleetToMerge = AllFleets[fleetKey] as Fleet;
            foreach (Ship ship in fleetToMerge.FleetShips)
            {
                SelectedFleet.FleetShips.Add(ship);
            }

            AllFleets.Remove(fleetKey);
            ClientState.Data.DeletedFleets.Add(fleetKey);
            UpdateDialogDetails();

            MergeButton.Enabled = false;
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
            FleetName.Text = SelectedFleet.Name;
            Hashtable designs = SelectedFleet.Composition;

            FleetComposition.Items.Clear();
            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(((int)designs[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                FleetComposition.Items.Add(listItem);
            }

            CoLocatedFleets.Items.Clear();

            foreach (Fleet fleet in AllFleets.Values)
            {
                if (fleet.Name != SelectedFleet.Name)
                {
                    if (fleet.Position == SelectedFleet.Position)
                    {
                        CoLocatedFleets.Items.Add(fleet.Name);
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
            set { SelectedFleet = value; UpdateDialogDetails(); }
            get { return SelectedFleet; }
        }

        #endregion

    }
}
