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
// Control to hold the summary of a selected item.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Nova.Common;
using Nova.Client;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Control to display a summary of the selected map item.
    /// </summary>
    public partial class SelectionSummary : UserControl
    {

        private Item summaryItem = null;
        private PlanetSummary planetSummary = new PlanetSummary();
        private FleetSummary fleetSummary = new FleetSummary();

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public SelectionSummary()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display a planet summary
        /// </summary>
        /// <param name="item"></param>
        /// ----------------------------------------------------------------------------
        private void DisplayPlanet(Item item)
        {
            if (ClientState.Data.StarReports.Contains(item.Name) == false)
            {
                SelectedItem.Text = item.Name + " is unexplored";
                summaryItem = null;
                SelectedItem.Controls.Clear();
                return;
            }

            SelectedItem.Text = "Summary of " + item.Name;
            planetSummary.Location = new Point(5, 15);
            planetSummary.Value = item as Star;

            // If we are displaying a fleet clear it out and add the planet
            // summary display.

            if (summaryItem is Fleet || summaryItem == null)
            {
                SelectedItem.Controls.Clear();
                SelectedItem.Controls.Add(planetSummary);
            }

            summaryItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display a fleet summary
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayFleet(Item item)
        {
            SelectedItem.Text = "Summary of " + item.Name;
            fleetSummary.Location = new Point(5, 15);
            fleetSummary.Value = item as Fleet;

            if (summaryItem is Star || summaryItem == null)
            {
                SelectedItem.Controls.Clear();
                SelectedItem.Controls.Add(fleetSummary);
            }

            summaryItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the content of the summary control. Depending on the type of the item
        /// selected this may either be a planet (in which case the planet summary
        /// control is displayed) or a fleet (which will cause the fleet summary control
        /// to be displayed).
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void SetItem(Item item)
        {
            if (item == null)
            {
                SelectedItem.Text = "Nothing Selected";
                SelectedItem.Controls.Clear();
                return;
            }

            if (item is Fleet)
            {
                DisplayFleet(item);
            }
            else
            {
                DisplayPlanet(item);
            }
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property to access the displayed item
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Item Value
        {
            set { SetItem(value); }
            get { return summaryItem; }
        }

        #endregion

    }
}
