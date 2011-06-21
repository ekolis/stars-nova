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
using System.Collections.Generic;
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

        public event FleetSelectionChanged FleetSelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired in addition to
        /// StarSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </Summary>
        public event CursorChanged CursorChangedEvent;

        private Dictionary<String, Fleet> fleetsInOrbit = new Dictionary<string, Fleet>();
        
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
            
            UpdateFields();

            QueueList.Populate(productionQueue, star.ManufacturingQueue);
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
                previousPlanet.Enabled = false;
                nextPlanet.Enabled = false;
                return;
            }

            previousPlanet.Enabled = true;
            nextPlanet.Enabled = true;

            star = myStars.GetNext(star);

            // Inform of the selection change to all listening objects.
            FireStarSelectionChangedEvent();
            FireCursorChangedEvent();
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
                previousPlanet.Enabled = false;
                nextPlanet.Enabled = false;
                return;
            }

            previousPlanet.Enabled = true;
            nextPlanet.Enabled = true;

            star = myStars.GetPrevious(star);
            
            // Inform of the selection change to all listening objects.
            FireStarSelectionChangedEvent();
            FireCursorChangedEvent();
        }

        private void FireCursorChangedEvent()
        {
            if( CursorChangedEvent != null )
                CursorChangedEvent( this, new CursorArgs((Point)star.Position));
        }

        private void FireStarSelectionChangedEvent()
        {
            if (StarSelectionChangedEvent != null)
                StarSelectionChangedEvent(this, new StarSelectionArgs(star));
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
            if (selectedStar == null)
                return;

            star = selectedStar;

            UpdateFields();

            groupPlanetSelect.Text = "Planet " + star.Name;

            if (ClientState.Data.PlayerStars.Count > 1)
            {                
                previousPlanet.Enabled = true;
                nextPlanet.Enabled = true;
            }
            else
            {
                previousPlanet.Enabled = false;
                previousPlanet.Enabled = false;
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

            QueueList.Populate(productionQueue, star.ManufacturingQueue);

            Defenses.ComputeDefenseCoverage(star);

            defenseType.Text = star.DefenseType;
            defenses.Text = star.Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            defenseCoverage.Text = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            factories.Text = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture)
                             + " of " +
                             star.GetOperableFactories().ToString(System.Globalization.CultureInfo.InvariantCulture);
            mines.Text = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture)
                         + " of " + star.GetOperableMines().ToString(System.Globalization.CultureInfo.InvariantCulture);
            population.Text = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);

            resourceDisplay.ResourceRate = star.GetResourceRate();

            if (star.OnlyLeftover == false)
            {
                resourceDisplay.ResearchPercentage = ClientState.Data.ResearchBudget;
            }
            else
            {
                // We treat Stars contributing only leftover resources as having
                // a 0% budget allocation.
                resourceDisplay.ResearchPercentage = 0;
            }
            resourceDisplay.Value = star.ResourcesOnHand;

            scannerRange.Text = star.ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
            scannerType.Text = star.ScannerType;

            if (star.Starbase == null)
            {
                starbasePanel.Text = "No Starbase";
                starbasePanel.Enabled = false;
                return;
            }

            Fleet starbase = star.Starbase;
            starbaseArmor.Text = starbase.TotalArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            starbaseCapacity.Text =
                starbase.TotalDockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            starbaseDamage.Text = "0";
            starbasePanel.Enabled = true;
            starbasePanel.Text = starbase.Name;
            starbaseShields.Text = starbase.TotalShieldStrength.ToString();

            massDriverType.Text = "None";
            massDriverDestination.Text = "None";
            targetButton.Enabled = false;

            List<String> fleetnames = new List<string>();
            fleetsInOrbit = new Dictionary<string, Fleet>();
            foreach (Fleet fleet in ClientState.Data.PlayerFleets)
            {
                if ( fleet.InOrbit != null &&  fleet.InOrbit.Name == star.Name && !fleet.IsStarbase)
                {
                    fleetnames.Add(fleet.Name);
                    fleetsInOrbit[fleet.Name] = fleet;
                }
            }
            fleetnames.Sort();
            comboFleetsInOrbit.Items.Clear();
            bool haveFleets = fleetnames.Count > 0;
            if (haveFleets)
            {
                comboFleetsInOrbit.Items.AddRange(fleetnames.ToArray());
                comboFleetsInOrbit.SelectedIndex = 0;
            }
            buttonGoto.Enabled = haveFleets;
            buttonGoto.Enabled = haveFleets;
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

        private Fleet GetSelectedFleetInOrbit()
        {
            if (comboFleetsInOrbit.SelectedItem == null)
                return null;

            Fleet fleet;
            if (!fleetsInOrbit.TryGetValue(comboFleetsInOrbit.SelectedItem.ToString(), out fleet))
                return null;

            return fleet;
        }

        private void comboFleetsInOrbit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetInOrbit();
            if (fleet == null)
            {
                gaugeFuel.Value = 0;
                gaugeFuel.Maximum = 0;
                gaugeCargo.Value = 0;
                gaugeCargo.Maximum = 0;
            }
            else
            {
                gaugeFuel.Maximum = fleet.TotalFuelCapacity;
                gaugeFuel.Value = fleet.FuelAvailable;
                gaugeCargo.Maximum = fleet.TotalCargoCapacity;
                gaugeCargo.Value = fleet.Cargo.Mass;
            }
            Invalidate();
        }

        private void buttonGoto_Click(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetInOrbit();
            if (fleet != null)
            {
                FleetSelectionArgs selectionArgs = new FleetSelectionArgs(fleet, fleet);
                CursorArgs cursorArgs = new CursorArgs((Point)fleet.Position);

                // Inform of the selection change to all listening objects.
                if( FleetSelectionChangedEvent != null )
                    FleetSelectionChangedEvent(this, selectionArgs);
                if( CursorChangedEvent != null )
                CursorChangedEvent(this, cursorArgs);          
            }
        }

        private void buttonCargo_Click(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetInOrbit();
            if (fleet != null)
            {
                try
                {
                    using (CargoDialog cargoDialog = new CargoDialog())
                    {
                        cargoDialog.SetTarget(fleet);
                        cargoDialog.ShowDialog();                        
                    }
                    ClientState.Data.StarReports[fleet.InOrbit.Name] = new StarReport(fleet.InOrbit);  // Not sure why - coppied from FleetDetails!
                    comboFleetsInOrbit_SelectedIndexChanged(null, null);
                }
                catch
                {
                    Report.Debug("FleetDetail.cs : CargoButton_Click() - Failed to open cargo dialog.");
                }
            }
        }
    }


    
}
