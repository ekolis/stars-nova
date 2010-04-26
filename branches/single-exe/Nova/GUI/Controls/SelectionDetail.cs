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
// Control to act as a container to hold the appropriate detail control of a
// selected item.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

namespace Nova.Gui.Controls
{
    /// <summary>
    /// Control to act as a container to hold the appropriate detail control of a
    /// selected item.
    /// </summary>
    public partial class SelectionDetail : UserControl
    {

        private Item selectedItem = null;
        private UserControl selectedControl = null;
        private PlanetDetail planetDetail = new PlanetDetail();
        private FleetDetail fleetDetail = new FleetDetail();

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction
        /// </summary>
        /// ----------------------------------------------------------------------------
        public SelectionDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display planet detail
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayPlanet(Item item)
        {
            planetDetail.Location = new Point(5, 15);
            planetDetail.Value = item as Star;
            DetailPanel.Text = "System " + item.Name;

            if (selectedItem is Fleet || selectedItem == null)
            {
                selectedControl = planetDetail;
                DetailPanel.Controls.Clear();
                DetailPanel.Controls.Add(planetDetail);
            }

            selectedItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display fleet detail
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayFleet(Item item)
        {
            fleetDetail.Location = new Point(5, 15);
            fleetDetail.Value = item as Fleet;
            DetailPanel.Text = "Fleet " + item.Name;

            //  if (selectedItem is Star || selectedItem == null) {
            selectedControl = fleetDetail;
            DetailPanel.Controls.Clear();
            DetailPanel.Controls.Add(fleetDetail);
            //}

            selectedItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the content of the detail control. Depending on the type of the item
        /// selected this may either be a planet (in which case the planet detail
        /// control is displayed) or a fleet (which will cause the fleet detail control
        /// to be displayed).
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void SetItem(Item item)
        {
            if (item == null)
            {
                selectedItem = null;
                selectedControl = null;
                DetailPanel.Text = "Nothing Selected";
                DetailPanel.Controls.Clear();
                return;
            }

            // To avoid confusion when another race's fleet or planet is selected
            // grey out (disable) the detail panel.

            if (item.Owner == ClientState.Data.RaceName)
            {
                DetailPanel.Enabled = true;
            }
            else
            {
                DetailPanel.Enabled = false;
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
        /// Property to access the selected item (Fleet or Star).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Item Value
        {
            set { SetItem(value); }
            get { return selectedItem; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property to access the actual detail control being displayed.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public UserControl Control
        {
            get { return selectedControl; }
        }

        #endregion

    }
}
