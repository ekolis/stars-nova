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
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common; 

    /// <Summary>
    /// Control to act as a container to hold the appropriate Detail control of a
    /// selected Item.
    /// </Summary>
    public partial class SelectionDetail : UserControl
    {
        // FIXME:(priority 3) this should not be here. It is only needed to pass it
        // down to the PlanetDetail (Who passes it to ProductionDialog). In any case,
        // ProductionDialog shouldn't need the whole state either. Must refactor this.
        private ClientData clientState;
        
        private readonly EmpireData empireState;
        
        private UserControl selectedControl = null;
        
        public PlanetDetail PlanetDetail
        {
            get { return planetDetail; }
            set { planetDetail = value; }
        }
        
        public FleetDetail FleetDetail
        {
            get { return fleetDetail; }
            set { fleetDetail = value; }
        }
        
        
        /// <Summary>
        /// Initializes a new instance of the SelectionDetail class.
        /// </Summary>
        public SelectionDetail(EmpireData empireState, ClientData clientState)
        {
            this.empireState = empireState;
            
            // FIXME: (priority 3) see declaration.
            this.clientState = clientState;
            
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
            
            PlanetDetail.Show();
            FleetDetail.Hide();  
            Invalidate();
        }


        /// <Summary>
        /// Display fleet Detail
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void DisplayFleet(Fleet fleet)
        {
            FleetDetail.Value = fleet;
            selectedControl = FleetDetail;
            
            FleetDetail.Show();
            PlanetDetail.Hide(); 
            Invalidate();
        }


        /// <Summary>
        /// Set the content of the Detail control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Detail
        /// control is displayed) or a fleet (which will cause the fleet Detail control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        private void SetItem(Mappable item)
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

            // Detail panel works with owned objects, so retrieve them from their collections.
            if (item is FleetIntel || item is Fleet)
            {
                DisplayFleet(empireState.OwnedFleets[item.Key]);
            }
            else
            {
                // We should use item.Key here, but we know stars are keyed by Name and
                // we save two casts (star and starintel, which are not polymorphic)
                DisplayPlanet(empireState.OwnedStars[item.Name]);
            }
        }

        
        /// <summary>
        /// Updates the selected Mappable when it has changed from other panels (Like the Starmap).
        /// </summary>
        /// <param name="sender">The source of the selection change</param>
        /// <param name="e">The new selection</param>
        public void DetailChangeSelection(object sender, SelectionArgs e)
        {
            this.Value = e.Selection;
        }

        
        /// <Summary>
        /// Property to set the Mappable to display (Fleet or Star).
        /// </Summary>
        public Mappable Value
        {
            set { SetItem(value); }
        }


        /// <Summary>
        /// Checks if we are showing a Planet or a Fleet.
        /// </Summary>
        public bool isPlanetDetail()
        {
            return (selectedControl == PlanetDetail);
        }
        
        public void CurrentSelection(object sender, SelectionArgs e)
        {
            e.Selection = Reload();
        }            
        
        /// <summary>
        /// Reloads the currently selected item's data, for updating.
        /// </summary>
        public Mappable Reload()
        {
            return isPlanetDetail() ? PlanetDetail.Value as Mappable : FleetDetail.Value as Mappable;
        }
    }
}
