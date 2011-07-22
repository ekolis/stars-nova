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
        private void DisplaySummary(FleetIntel report)
        {
            ushort ownerId = report.Owner;
            
            fleetShipCount.Text = report.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
            fleetMass.Text = report.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            fleetSpeed.Text = report.Speed.ToString(System.Globalization.CultureInfo.InvariantCulture);
            fleetImage.Image = report.Icon.Image;
            
            if (ownerId != empireState.Id)
            {
                raceIcon.Image = empireState.EmpireReports[ownerId].Icon.Image;
                fleetOwner.Text = empireState.EmpireReports[ownerId].RaceName;
            }
            else
            {
                raceIcon.Image = empireState.Race.Icon.Image;
                fleetOwner.Text = empireState.Race.Name;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Select the fleet whose details are to be displayed
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public FleetIntel Value
        {
            set { DisplaySummary(value); }
        }
    }
}
