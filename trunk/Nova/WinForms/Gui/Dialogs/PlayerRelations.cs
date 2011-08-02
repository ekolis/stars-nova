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
    /// Describes the possible player relation stances.
    /// </Summary>
    public partial class PlayerRelations : Form
    {
        private Dictionary<ushort, EmpireIntel> empireReports;
        private ushort empireId;

        /// <Summary>
        /// Initializes a new instance of the PlayerRelations class.
        /// </Summary>
        public PlayerRelations(Dictionary<ushort, EmpireIntel> empireReports, ushort empireId)
        {
            this.empireReports = empireReports;
            this.empireId = empireId;
            
            InitializeComponent();

            foreach (ushort otherEmpireId in this.empireReports.Keys)
            {
                if (otherEmpireId != this.empireId)
                {
                    empireList.Items.Add(otherEmpireId);
                }
            }

            if (empireList.Items.Count > 0)
            {
                empireList.SelectedIndex = 0;
            }
        }


        /// <Summary>
        /// Exit dialog button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DoneBUtton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <Summary>
        /// Selected race has changed, update the relation details
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void SelectedRaceChanged(object sender, EventArgs e)
        {
            ushort selectedEmpire = (ushort) empireList.SelectedItem;

            if (empireReports[selectedEmpire].Relation == PlayerRelation.Enemy)
            {
                enemyButton.Checked = true;
            }
            else if (empireReports[selectedEmpire].Relation == PlayerRelation.Enemy)
            {
                neutralButton.Checked = true;
            }
            else
            {
                friendButton.Checked = true;
            }
        }

        /// <Summary>
        /// Player relationship changed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RelationChanged(object sender, EventArgs e)
        {
            ushort selectedEmpire = (ushort)empireList.SelectedItem;
            RadioButton button = sender as RadioButton;
            if (enemyButton.Checked)
            {
                empireReports[selectedEmpire].Relation = PlayerRelation.Enemy;
            }
            else if (friendButton.Checked)
            {
                empireReports[selectedEmpire].Relation = PlayerRelation.Friend;
            }
            else
            {
                empireReports[selectedEmpire].Relation = PlayerRelation.Neutral;
            }
        }
    }
}
