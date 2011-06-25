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
// Control to act as a container to hold the appropriate Detail control of a
// selected Item.
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
    /// Control to act as a container to hold the appropriate Detail control of a
    /// selected Item.
    /// </Summary>
    public partial class SelectionDetail : UserControl
    {
        private Item selectedItem = null;
        private UserControl selectedControl = null;
  
        private Dictionary<string, StarIntel> starReports;
        private List<Fleet> playerFleets;
        private StarList playerStars;
        private Race playerRace;
        private int researchBudget;
        
        private Dictionary<string, Fleet> allFleets; // FIXME:(???) Do we need allFleets here? Can't we use the player's fleets instead? -Aeglos 21 Jun 11 
        private List<string> deletedFleets;
        
        public PlanetDetail PlanetDetail; 
        public FleetDetail FleetDetail;
        
        // FIXME:(priority 3) this should not be here. It is only needed to pass it
        // down to the PlanetDetail (Who passes it to ProductionDialog). In any case,
        // ProductionDialog shouldn't need the whole state either. Must refactor this.
        private ClientState stateData;

        #region Construction

        /// <Summary>
        /// Initializes a new instance of the SelectionDetail class.
        /// </Summary>
        public SelectionDetail(Dictionary<string, StarIntel> starReports,
                               Dictionary<string, Fleet> allFleets,
                               List<Fleet> playerFleets,
                               List<string> deletedFleets,
                               Race playerRace,
                               int researchBudget,
                               ClientState stateData)
        {
            this.starReports = starReports;
            this.playerStars = playerStars;
            this.playerFleets = playerFleets;
            this.playerRace = playerRace;
            this.researchBudget = researchBudget;
            
            // FIXME: (priority 3) see declaration.
            this.stateData = stateData;
            
            PlanetDetail = new PlanetDetail(playerStars, starReports, playerFleets, researchBudget, stateData);
            FleetDetail = new FleetDetail(starReports, allFleets, playerFleets, deletedFleets, playerRace);
            
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display planet Detail
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayPlanet(Item item)
        {
            PlanetDetail.Value = item as Star;
            
            if (selectedItem is Fleet || selectedItem == null)
            {
                selectedControl = PlanetDetail;
                Controls.Clear();
                Controls.Add(PlanetDetail);
            }

            selectedItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display fleet Detail
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void DisplayFleet(Item item)
        {
            FleetDetail.Value = item as Fleet;
            
            selectedControl = FleetDetail;
            Controls.Clear();
            Controls.Add(FleetDetail);

            selectedItem = item;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set the content of the Detail control. Depending on the type of the Item
        /// selected this may either be a planet (in which case the planet Detail
        /// control is displayed) or a fleet (which will cause the fleet Detail control
        /// to be displayed).
        /// </Summary>
        /// <param name="Item">The <see cref="Item"/> to display (a <see cref="Fleet"/> or <see cref="Star"/>).</param>
        /// ----------------------------------------------------------------------------
        private void SetItem(Item item)
        {
            if (item == null)
            {
                Enabled = false;
                return;
            }

            // To avoid confusion when another race's fleet or planet is selected
            // grey out (disable) the Detail panel.

            if (item.Owner == playerRace.Name)
            {
                Enabled = true;
            }
            else
            {
                Enabled = false;
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
        
        public Item ReportItem()
        {
            return selectedItem;
        }

        public void SelectionChanged(object sender, SelectionArgs e)
        {
            this.Value = e.Item;
        }
        
        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Property to access the selected Item (Fleet or Star).
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Item Value
        {
            set { SetItem(value); }
            get { return selectedItem; }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Property to access the actual Detail control being displayed.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public UserControl Control
        {
            get { return selectedControl; }
        }

        #endregion
    }
}
