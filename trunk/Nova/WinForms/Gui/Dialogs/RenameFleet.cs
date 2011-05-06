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
// Dialog to rename a fleet.
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
    /// Dialog to rename a fleet.
    /// </summary>
    public partial class RenameFleet : Form
    {
        public event FleetSelectionChanged FleetSelectionChangedEvent;
        
        /// <summary>
        /// Initializes a new instance of the RenameFleet class.
        /// </summary>
        public RenameFleet()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Cancel button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CancelRename_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Rename button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OKButton_Click(object sender, EventArgs e)
        {
            Hashtable allFleets = ClientState.Data.InputTurn.AllFleets;

            string newName = this.newName.Text;
            string newKey = ClientState.Data.RaceName + "/" + newName;
            string oldKey = ClientState.Data.RaceName + "/" + this.ExistingName.Text;

            if (allFleets.Contains(newKey))
            {
                Report.Error("A fleet already has that name");
                return;
            }

            Fleet fleet = allFleets[oldKey] as Fleet;
            allFleets.Remove(oldKey);
            ClientState.Data.DeletedFleets.Add(oldKey);

            fleet.Name = newName;
            allFleets[newKey] = fleet;

            // Ensure the main display gets updated to reflect the new name

            FleetSelectionArgs selectionArgs = new FleetSelectionArgs(fleet, fleet);
            FleetSelectionChangedEvent(this, selectionArgs);
            
            //this.SelectionSummary.Value = fleet as Item;
            //this.SelectionDetail.Value = fleet as Item;

            Close();
        }
    }
}
