#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;

    /// <Summary>
    /// Manage the details of a fleet (composition, etc.).
    /// </Summary>
    public partial class ManageFleetDialog : Form
    {
        private readonly FleetIntelList fleetReports;
        private List<string> deletedFleets;
        private string raceName;
        private Fleet selectedFleet;

        /// <Summary>
        /// Initializes a new instance of the ManageFleetDialog class.
        /// </Summary>
        public ManageFleetDialog(FleetIntelList fleetReports, List<string> deletedFleets, string raceName)
        {
            // FIXME:(???) Do we need allFleets here? Can't we use the player's fleets instead? -Aeglos 21 Jun 11 
            this.fleetReports = fleetReports; 
            this.deletedFleets = deletedFleets;
            this.raceName = raceName;
            
            InitializeComponent();            
        }
        
        #region Event Methods

        /// <Summary>
        /// Rename a fleet. The rename is only allowed if the fleet access key is
        /// unique.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RenameButton_Click(object sender, EventArgs e)
        {
            RenameFleet renameDialog = new RenameFleet(fleetReports, deletedFleets, raceName);

            // TODO (priority 5): Implement the fleet selection event.

            renameDialog.ExistingName.Text = this.fleetName.Text;

            renameDialog.ShowDialog();
            renameDialog.Dispose();

            this.fleetName.Text = this.selectedFleet.Name;
        }

        /// <Summary>
        /// Merge the ships from a co-located fleet into the selected fleet
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MergeButton_Click(object sender, EventArgs e)
        {
            string fleetName = this.coLocatedFleets.SelectedItems[0].Text;
            string fleetKey = raceName + "/" + fleetName;

            Fleet fleetToMerge = this.fleetReports[fleetKey];
            foreach (Ship ship in fleetToMerge.FleetShips)
            {
                this.selectedFleet.FleetShips.Add(ship);
            }

            this.fleetReports.Remove(fleetKey);
            deletedFleets.Add(fleetKey);
            UpdateDialogDetails();

            this.mergeButton.Enabled = false;
        }

        #endregion

        #region Utility Methods

        /// <Summary>
        /// Update the dialog fields
        /// </Summary>
        private void UpdateDialogDetails()
        {
            this.fleetName.Text = this.selectedFleet.Name;
            Dictionary<string, int> designs = this.selectedFleet.Composition;

            this.fleetComposition.Items.Clear();
            foreach (string key in designs.Keys)
            {
                ListViewItem listItem = new ListViewItem(key);
                listItem.SubItems.Add(designs[key].ToString(System.Globalization.CultureInfo.InvariantCulture));
                this.fleetComposition.Items.Add(listItem);
            }

            this.coLocatedFleets.Items.Clear();

            foreach (Fleet fleet in this.fleetReports.Values)
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

        /// <Summary>
        /// Select the fleet to be managed.
        /// </Summary>
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
