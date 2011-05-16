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
// Control to hold the Summary of a selected Item.
// ===========================================================================
#endregion

using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    /// <Summary>
    /// Control to display a Summary of the selected map Item.
    /// </Summary>
    public partial class SelectionSummary : UserControl
    {

        private Item summaryItem = null;
        private PlanetSummary planetSummary = new PlanetSummary();
        private FleetSummary fleetSummary = new FleetSummary();

        #region Construction

        /// <Summary>
        /// Initializes a new instance of the SelectionSummary class.
        /// </Summary>
        public SelectionSummary()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display a planet Summary
        /// </Summary>
        /// <param name="Item"></param>
        /// ----------------------------------------------------------------------------
        private void DisplayPlanet(Item item)
        {
            if (ClientState.Data.StarReports.Contains(item.Name) == false)
            {
                this.selectedItem.Text = item.Name + " is unexplored";
                summaryItem = null;
                this.selectedItem.Controls.Clear();
                return;
            }

            this.selectedItem.Text = "Summary of " + item.Name;
            planetSummary.Location = new Point(5, 15);
            planetSummary.Value = item as Star;

            // If we are displaying a fleet clear it out and add the planet
            // Summary display.

            if (summaryItem is Fleet || summaryItem == null)
            {
                this.selectedItem.Controls.Clear();
                this.selectedItem.Controls.Add(planetSummary);
            }

            summaryItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display a fleet Summary
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayFleet(Item item)
        {
            this.selectedItem.Text = "Summary of " + item.Name;
            fleetSummary.Location = new Point(5, 15);
            fleetSummary.Value = item as Fleet;

            if (summaryItem is Star || summaryItem == null)
            {
                this.selectedItem.Controls.Clear();
                this.selectedItem.Controls.Add(fleetSummary);
            }

            summaryItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set the content of the Summary control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Summary
        /// control is displayed) or a fleet (which will cause the fleet Summary control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void SetItem(Item item)
        {
            if (item == null)
            {
                this.selectedItem.Text = "Nothing Selected";
                this.selectedItem.Controls.Clear();
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
        
        public void SelectionChanged(object sender, SelectionArgs e)
        {
            this.Value = e.Item;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Property to access the displayed Item
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Item Value
        {
            set { SetItem(value); }
            get { return summaryItem; }
        }

        #endregion

    }
}
