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
// Planet Detail display pane.
// ===========================================================================
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{
    /// <Summary>
    /// Planet Detail display pane.
    /// </Summary>
    public partial class PlanetDetail : System.Windows.Forms.UserControl
    {
        private Star star;
        
        /// <Summary>
        /// This event should be fired when the selected Star
        /// changes.
        /// </Summary>
        public event StarSelectionChanged StarSelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired in addition to
        /// StarSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </Summary>
        public event CursorChanged CursorChangedEvent;
        
        #region Construction and Disposal

        /// <Summary>
        /// Initializes a new instance of the PlanetDetail class.
        /// </Summary>
        public PlanetDetail()
        {
            InitializeComponent();
        }

        #endregion


        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// The change queue button has been pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ChangeProductionQueue_Click(object sender, EventArgs e)
        {
            ProductionDialog productionDialog = new ProductionDialog(star);

            productionDialog.ShowDialog();
            productionDialog.Dispose();
            
            this.UpdateFields();

            QueueList.Populate(this.productionQueue, star.ManufacturingQueue);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Next planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                this.previousPlanet.Enabled = false;
                this.nextPlanet.Enabled = false;
                return;
            }

            this.previousPlanet.Enabled = true;
            this.nextPlanet.Enabled = true;

            star = myStars.GetNext(star);

            StarSelectionArgs selectionArgs = new StarSelectionArgs(star);
            CursorArgs cursorArgs = new CursorArgs((Point)star.Position);
            
            // Inform of the selection change to all listening objects.
            StarSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Previous planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousPlanet_Click(object sender, EventArgs e)
        {
            StarList myStars = ClientState.Data.PlayerStars;

            if (myStars.Count == 1)
            {
                this.previousPlanet.Enabled = false;
                this.nextPlanet.Enabled = false;
                return;
            }

            this.previousPlanet.Enabled = true;
            this.nextPlanet.Enabled = true;

            star = myStars.GetPrevious(star);
            
            StarSelectionArgs selectionArgs = new StarSelectionArgs(star);
            CursorArgs cursorArgs = new CursorArgs((Point)star.Position);
            
            // Inform of the selection change to all listening objects.
            StarSelectionChangedEvent(this, selectionArgs);
            CursorChangedEvent(this, cursorArgs);
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set the Star which is to be displayed
        /// </Summary>
        /// <param name="selectedStar">The Star to be displayed</param>
        /// ----------------------------------------------------------------------------
        private void SetStarDetails(Star selectedStar)
        {
            star = selectedStar;

            UpdateFields();

            if( selectedStar != null )
                groupPlanetSelect.Text = "Planet " + selectedStar.Name;
            else
                groupPlanetSelect.Text = "No Planet Selected";

            if (ClientState.Data.PlayerStars.Count > 1)
            {                
                this.previousPlanet.Enabled = true;
                this.nextPlanet.Enabled = true;
            }
            else
            {
                this.previousPlanet.Enabled = false;
                this.previousPlanet.Enabled = false;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update all the fields in the planet Detail display.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void UpdateFields()
        {
            if (star == null)
            {
                return;
            }

            QueueList.Populate(this.productionQueue, star.ManufacturingQueue);

            Nova.Common.Defenses.ComputeDefenseCoverage(star);

            this.defenseType.Text = star.DefenseType;
            this.defenses.Text = star.Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.defenseCoverage.Text = Nova.Common.Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            this.factories.Text = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                    + " of " + star.GetOperableFactories().ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.mines.Text = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    + " of " + star.GetOperableMines().ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.population.Text = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            this.resourceDisplay.ResourceRate = star.GetResourceRate();
            
            if (star.OnlyLeftover == false)
            {
                this.resourceDisplay.ResearchPercentage = ClientState.Data.ResearchBudget;
            }
            else
            {
                // We treat Stars contributing only leftover resources as having
                // a 0% budget allocation.
                this.resourceDisplay.ResearchPercentage = 0;
            }
            this.resourceDisplay.Value = star.ResourcesOnHand;

            this.scannerRange.Text = star.ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.scannerType.Text = star.ScannerType;

            if (star.Starbase == null)
            {
                this.starbasePanel.Text = "No Starbase";
                this.starbasePanel.Enabled = false;
                return;
            }

            Fleet starbase = star.Starbase;
            this.starbaseArmor.Text = starbase.TotalArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.starbaseCapacity.Text = starbase.TotalDockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.starbaseDamage.Text = "0";
            this.starbasePanel.Enabled = true;
            this.starbasePanel.Text = starbase.Name;
            this.starbaseShields.Text = starbase.TotalShieldStrength.ToString();

            this.massDriverType.Text = "None";
            this.massDriverDestination.Text = "None";
            this.targetButton.Enabled = false;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Access to the Star whose details are displayed in the panel.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public Star Value
        {
            set { SetStarDetails(value); }
            get { return star; }
        }

        #endregion

        private void starbasePanel_Enter(object sender, EventArgs e)
        {

        }
    }


    
}
