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
// Fleet summary display panel.
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

using NovaCommon;
using NovaClient;

namespace Nova.Gui.Controls
{
    /// <summary>
    /// The fleet summary panel.
    /// </summary>
    public partial class FleetSummary : UserControl
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public FleetSummary()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display the fleet summary
        /// </summary>
        /// <param name="fleet">The <see cref="Fleet"/> to display.</param>
        /// ----------------------------------------------------------------------------
        private void DisplaySummary(Fleet fleet)
        {
            string race = fleet.Owner;
            FleetShipCount.Text = fleet.Composition.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
            FleetMass.Text = fleet.TotalMass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            FleetSpeed.Text = fleet.Speed.ToString(System.Globalization.CultureInfo.InvariantCulture);
            FleetImage.Image = fleet.Image;
            FleetOwner.Text = race;

            RaceIcon.Image = ClientState.Data.InputTurn.RaceIcons[race] as Image;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select the fleet whose details are to be displayed
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Fleet Value
        {
            set { DisplaySummary(value); }
        }
    }
}
