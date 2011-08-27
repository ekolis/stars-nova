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
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;

    /// <Summary>
    /// Control to display a Summary of the selected map Item.
    /// </Summary>
    public partial class SelectionSummary : UserControl
    {
        private readonly EmpireData empireState;
        
        private Item summaryItem = null;
        private PlanetSummary planetSummary;
        private FleetSummary fleetSummary;

        /// <Summary>
        /// Initializes a new instance of the SelectionSummary class.
        /// </Summary>
        public SelectionSummary(EmpireData empireState)
        {
            this.empireState = empireState;
            
            planetSummary = new PlanetSummary(empireState);
            fleetSummary = new FleetSummary(empireState);
            
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        /// <Summary>
        /// Display a planet Summary
        /// </Summary>
        /// <param name="Item"></param>
        private void DisplayPlanet(StarIntel report)
        {
            if (empireState.StarReports[report.Name].Year == Global.Unset)
            {
                this.selectedItem.Text = report.Name + " is unexplored";
                summaryItem = null;
                this.selectedItem.Controls.Clear();
                return;
            }

            this.selectedItem.Text = "Summary of " + report.Name;
            planetSummary.Location = new Point(5, 15);
            planetSummary.Value = report;

            // If we are displaying a fleet clear it out and add the planet
            // Summary display.

            if (summaryItem is FleetIntel || summaryItem == null)
            {
                this.selectedItem.Controls.Clear();
                this.selectedItem.Controls.Add(planetSummary);
            }

            summaryItem = report;
        }

        /// <Summary>
        /// Display a fleet Summary
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void DisplayFleet(FleetIntel report)
        {
            this.selectedItem.Text = "Summary of " + report.Name;
            fleetSummary.Location = new Point(5, 15);
            fleetSummary.Value = report;

            if (summaryItem is StarIntel || summaryItem == null)
            {
                this.selectedItem.Controls.Clear();
                this.selectedItem.Controls.Add(fleetSummary);
            }

            summaryItem = report;
        }

        /// <Summary>
        /// Set the content of the Summary control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Summary
        /// control is displayed) or a fleet (which will cause the fleet Summary control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void SetItem(Item item)
        {
            if (item == null)
            {
                this.selectedItem.Text = "Nothing Selected";
                this.selectedItem.Controls.Clear();
                return;
            }

            if (item is FleetIntel)
            {
                DisplayFleet(item as FleetIntel);
            }
            else
            {
                DisplayPlanet(item as StarIntel);
            }
        }
        
        public void SummaryChangeSelection(object sender, SummarySelectionArgs e)
        {
            this.Value = e.Summary;
        }
       
        /// <Summary>
        /// Property to access the displayed Item
        /// </Summary>
        public Item Value
        {
            set { SetItem(value); }
            get { return summaryItem; }
        }
    }
}
