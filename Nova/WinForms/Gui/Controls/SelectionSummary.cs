#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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
    using System.Drawing;
    using System.Windows.Forms;
    
    using Nova.Common;

    /// <Summary>
    /// Control to display a Summary of the selected map Item.
    /// </Summary>
    public partial class SelectionSummary : UserControl
    {
        private readonly EmpireData empireState;
        
        private Mappable summaryItem = null;
        
        public PlanetSummary PlanetSummary
        {
            get { return planetSummary; }
            set { planetSummary = value; }
        }
        
        public FleetSummary FleetSummary
        {
            get { return fleetSummary; }
            set { fleetSummary = value; }
        }

        /// <Summary>
        /// Initializes a new instance of the SelectionSummary class.
        /// </Summary>
        public SelectionSummary(EmpireData empireState)
        {
            this.empireState = empireState;
            
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
                this.summaryFrame.Text = report.Name + " is unexplored";
                summaryItem = null;
                PlanetSummary.Hide();
                FleetSummary.Hide();
                Invalidate();
                return;
            }
            
            // If we are displaying a fleet clear it out and add the planet
            // Summary display.

            if (summaryItem is FleetIntel || summaryItem == null)
            {
                PlanetSummary.Show();
                FleetSummary.Hide();
                Invalidate();
            }

            summaryItem = report;            

            this.summaryFrame.Text = "Summary of " + report.Name;
            PlanetSummary.Location = new Point(5, 15);
            PlanetSummary.Value = report;
        }

        /// <Summary>
        /// Display a fleet Summary
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void DisplayFleet(FleetIntel report)
        {
            if (summaryItem is StarIntel || summaryItem == null)
            {
                FleetSummary.Show();
                PlanetSummary.Hide();
                Invalidate();
            }

            summaryItem = report;
            this.summaryFrame.Text = "Summary of " + report.Name;
            FleetSummary.Location = new Point(5, 15);
            FleetSummary.Value = report;
        }

        /// <Summary>
        /// Set the content of the Summary control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Summary
        /// control is displayed) or a fleet (which will cause the fleet Summary control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void SetItem(Mappable item)
        {
            if (item == null)
            {
                this.summaryFrame.Text = "Nothing Selected";
                FleetSummary.Hide();
                PlanetSummary.Hide();
                Invalidate();
                return;
            }

            if (item is FleetIntel || item is Fleet)
            {
                FleetIntel report = null;
                empireState.FleetReports.TryGetValue(item.Key, out report);
                if (report != null)
                {
                    DisplayFleet(report);
                }
                else
                {
                    SetItem(null);
                }
            }
            else
            {
                DisplayPlanet(empireState.StarReports[item.Name]);
            }
        }
        
        /// <summary>
        /// ??? Selection Summary event handler for when the selected Mappable object is changed.
        /// </summary>
        /// <param name="sender">The control signaling the event.</param>
        /// <param name="e">The event args, possibly null.</param>
        /// <remarks>
        /// Currently this occurs when selected via the StarMap, or when the goto button is pressed in teh FleetDetail or PlanetDetail controls.
        /// </remarks>
        public void SummaryChangeSelection(object sender, SelectionArgs e)
        {
            // FIXME (Priority 8) - Orbiting planet goto crash.
            // Dan 6 May 17 
            // In Rev# 879, with a fleet selected that is orbiting a planet, 
            // pressing the Fleet Details->Orbiting Planet->Goto (FleetDetail.buttonGotoPlanet) button
            // caused this to crash as it did not handle e == null
            if (e != null)
            {
                // Change the selection to the selected object (planet or ship) from the map.
                Value = e.Selection;
            }
            else
            {
                if (sender != null)
                {
                    try
                    {
                        // Handle the case that we are selecting an the planet from and orbiting fleet.
                        Value = (sender as FleetDetail).Value.InOrbit;
                    }
                    catch
                    {
                        // Default to showing nothing selected.
                        Value = null;
                    }
                }
                else
                {
                    // was the selection changed to nothing?
                    Value = null;
                }
            }
        }
       
        /// <Summary>
        /// Property to access the displayed Item
        /// </Summary>
        public Mappable Value
        {
            set { SetItem(value); }
        }
    }
}
