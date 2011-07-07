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
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    
    using Nova.Client;
    using Nova.Common;
        
    /// <Summary>
    /// The fleet Summary panel.
    /// </Summary>
    public partial class FleetSummary : UserControl
    {
        private readonly EmpireData empireState;

        /// <Summary>
        /// Initializes a new instance of the FleetSummary class.
        /// </Summary>
        public FleetSummary(EmpireData empireState)
        {
            this.empireState = empireState;
            
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display the fleet Summary
        /// </Summary>
        /// <param name="fleet">The <see cref="Fleet"/> to display.</param>
        /// ----------------------------------------------------------------------------
        private void DisplaySummary(Fleet fleet)
        {
            int ownerId = fleet.Owner;
            this.fleetShipCount.Text = fleet.Composition.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.fleetMass.Text = fleet.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.fleetSpeed.Text = fleet.Speed.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.fleetImage.Image = fleet.Icon.Image;
            // FIXME:(priority 3) look for enemy name inside EmpireData.
            // this needs the appropiate collections to be refactores.
            this.fleetOwner.Text = ownerId.ToString("X");
            if (ownerId != empireState.Id)
            {
                this.raceIcon.Image = empireState.EmpireReports[ownerId].Icon.Image;
            }
            else
            {
                this.raceIcon.Image = empireState.EmpireRace.Icon.Image;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Select the fleet whose details are to be displayed
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Fleet Value
        {
            set { DisplaySummary(value); }
        }
    }
}
