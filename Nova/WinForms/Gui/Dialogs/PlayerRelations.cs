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
// This module holds the program entry point and handles all things related to
// the main GUI window.
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

using Nova.Common;
using Nova.Client;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// This module holds the program entry point and handles all things related to
    /// the main GUI window.
    /// </summary>
    public partial class PlayerRelations : Form
    {
        private Hashtable Relation = ClientState.Data.PlayerRelations;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public PlayerRelations()
        {
            InitializeComponent();

            foreach (string raceName in ClientState.Data.InputTurn.AllRaceNames)
            {
                if (raceName != ClientState.Data.RaceName)
                {
                    RaceList.Items.Add(raceName);
                }
            }

            if (RaceList.Items.Count > 0)
            {
                RaceList.SelectedIndex = 0;
            }
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Exit dialog button pressed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DoneBUtton_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Selected race has changed, update the relation details
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SelectedRaceChanged(object sender, EventArgs e)
        {
            string selectedRace = RaceList.SelectedItem as string;

            if (Relation[selectedRace] as string == "Enemy")
            {
                EnemyButton.Checked = true;
            }
            else if (Relation[selectedRace] as string == "Neutral")
            {
                NeutralButton.Checked = true;
            }
            else
            {
                FriendButton.Checked = true;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Player relationship changed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RelationChanged(object sender, EventArgs e)
        {
            string selectedRace = RaceList.SelectedItem as string;
            RadioButton button = sender as RadioButton;
            Relation[selectedRace] = button.Text;
        }

        #endregion

    }
}
