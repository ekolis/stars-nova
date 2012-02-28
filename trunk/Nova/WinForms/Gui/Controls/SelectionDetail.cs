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
    /// Control to act as a container to hold the appropriate Detail control of a
    /// selected Item.
    /// </Summary>
    public partial class SelectionDetail : UserControl
    {
        private readonly EmpireData empireState;
        
        private Item selectedItem = null;
        private UserControl selectedControl = null;
        
        public PlanetDetail PlanetDetail; 
        public FleetDetail FleetDetail;
        
        // FIXME:(priority 3) this should not be here. It is only needed to pass it
        // down to the PlanetDetail (Who passes it to ProductionDialog). In any case,
        // ProductionDialog shouldn't need the whole state either. Must refactor this.
        private ClientData clientState;

        /// <Summary>
        /// Initializes a new instance of the SelectionDetail class.
        /// </Summary>
        public SelectionDetail(EmpireData empireState, List<long> deletedFleets,  ClientData clientState)
        {
            this.empireState = empireState;
            
            // FIXME: (priority 3) see declaration.
            this.clientState = clientState;
            
            PlanetDetail = new PlanetDetail(empireState, clientState);
            FleetDetail = new FleetDetail(clientState);
            
            InitializeComponent();
        }

        /// <Summary>
        /// Display planet Detail
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void DisplayPlanet(Star star)
        {
            PlanetDetail.Value = star;
            
            selectedControl = PlanetDetail;
            Controls.Clear();
            Controls.Add(PlanetDetail);

            selectedItem = star;
        }


        /// <Summary>
        /// Display fleet Detail
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void DisplayFleet(Fleet fleet)
        {
            FleetDetail.Value = fleet;

            selectedControl = FleetDetail;
            Controls.Clear();
            Controls.Add(FleetDetail);

            selectedItem = fleet;
        }


        /// <Summary>
        /// Set the content of the Detail control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Detail
        /// control is displayed) or a fleet (which will cause the fleet Detail control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void SetItem(Item item)
        {
            if (item == null)
            {
                Enabled = false;
                return;
            }

            // To avoid confusion when another race's fleet or planet is selected
            // grey out (disable) the Detail panel.

            if (item.Owner == empireState.Id)
            {
                Enabled = true;
            }
            else
            {
                Enabled = false;
                return;
            }

            // Detail panel works with owned objects, so retrieve them from their reports.
            if (item is Fleet)
            {
                DisplayFleet(item as Fleet);
            }
            else
            {
                DisplayPlanet(item as Star);
            }
        }
        
        public Item ReportItem()
        {
            return selectedItem;
        }

        public void DetailChangeSelection(object sender, DetailSelectionArgs e)
        {
            this.Value = e.Detail;
        }

        /// <Summary>
        /// Property to access the selected Item (Fleet or Star).
        /// </Summary>
        public Item Value
        {
            set { SetItem(value); }
            get { return selectedItem; }
        }


        /// <Summary>
        /// Property to access the actual Detail control being displayed.
        /// </Summary>
        public UserControl Control
        {
            get { return selectedControl; }
        }
    }
}
