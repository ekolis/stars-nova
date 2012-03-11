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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;
    using Nova.ControlLibrary;
    
    /// <Summary>
    /// Planet Detail display pane.
    /// </Summary>
    public partial class PlanetDetail : System.Windows.Forms.UserControl
    {
        private readonly EmpireData empireState;

        private Star selectedStar;
        
        // FIXME:(priority 3) this should not be here. It is only needed to pass it
        // down to the ProductionDialog. In any case, ProductionDialog shouldn't need
        // the whole state either. Must refactor this.
        private ClientData clientState;
        
        /// <Summary>
        /// This event should be fired when the selection changes.
        /// </Summary>
        public event SummarySelectionChanged SummarySelectionChangedEvent;

        public event DetailSelectionChanged DetailSelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired in addition to
        /// StarSelectionChangedEvent to reflect the new selection's
        /// cursor position.
        /// </Summary>
        public event CursorChanged CursorChangedEvent;

        private Dictionary<string, Fleet> fleetsInOrbit = new Dictionary<string, Fleet>();

        /// <Summary>
        /// Initializes a new instance of the PlanetDetail class.
        /// </Summary>
        public PlanetDetail(EmpireData empireState, ClientData clientState)
        {
            this.empireState = empireState;
            this.clientState = clientState;
            
            InitializeComponent();
        }

        /// <Summary>
        /// The change queue button has been pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ChangeProductionQueue_Click(object sender, EventArgs e)
        {
            ProductionDialog productionDialog = new ProductionDialog(selectedStar, clientState);

            productionDialog.ShowDialog();
            productionDialog.Dispose();
            
            UpdateFields();

            productionQueue.Populate(selectedStar);

        }


        /// <Summary>
        /// Next planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextPlanet_Click(object sender, EventArgs e)
        {
            if (empireState.OwnedStars.Count == 1)
            {
                previousPlanet.Enabled = false;
                nextPlanet.Enabled = false;
                return;
            }

            previousPlanet.Enabled = true;
            nextPlanet.Enabled = true;

            selectedStar = empireState.OwnedStars.GetNext(empireState.OwnedStars[selectedStar.Name]);

            // Inform of the selection change to all listening objects.
            FireStarSelectionChangedEvent();
            FireCursorChangedEvent();
        }


        /// <Summary>
        /// Previous planet button pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PreviousPlanet_Click(object sender, EventArgs e)
        {
            if (empireState.OwnedStars.Count == 1)
            {
                previousPlanet.Enabled = false;
                nextPlanet.Enabled = false;
                return;
            }

            previousPlanet.Enabled = true;
            nextPlanet.Enabled = true;

            selectedStar = empireState.OwnedStars.GetPrevious(empireState.OwnedStars[selectedStar.Name]);

            // Inform of the selection change to all listening objects.
            FireStarSelectionChangedEvent();
            FireCursorChangedEvent();
        }

        private void FireCursorChangedEvent()
        {
            if (CursorChangedEvent != null)
            {
                CursorChangedEvent(this, new CursorArgs((Point)selectedStar.Position));
            }
        }

        private void FireStarSelectionChangedEvent()
        {
            if (DetailSelectionChangedEvent != null)
            {
                DetailSelectionChangedEvent(this, new DetailSelectionArgs(selectedStar));
            }
        }

        /// <Summary>
        /// Set the Star which is to be displayed.
        /// </Summary>
        /// <param name="selectedStar">The Star to be displayed.</param>
        private void SetStarDetails(Star selectedStar)
        {
            if (selectedStar == null)
            {
                return;
            }

            this.selectedStar = selectedStar;

            UpdateFields();

            groupPlanetSelect.Text = "Planet " + selectedStar.Name;

            if (empireState.OwnedStars.Count > 1)
            {                
                previousPlanet.Enabled = true;
                nextPlanet.Enabled = true;
            }
            else
            {
                previousPlanet.Enabled = false;
                nextPlanet.Enabled = false;
            }
        }


        /// <Summary>
        /// Update all the fields in the planet Detail display.
        /// </Summary>
        private void UpdateFields()
        {
            if (selectedStar == null)
            {
                return;
            }

            productionQueue.Populate(selectedStar);

            Defenses.ComputeDefenseCoverage(selectedStar);

            defenseType.Text = selectedStar.DefenseType;
            defenses.Text = selectedStar.Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            defenseCoverage.Text = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            factories.Text = selectedStar.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture)
                             + " of " +
                             selectedStar.GetOperableFactories().ToString(System.Globalization.CultureInfo.InvariantCulture);
            mines.Text = selectedStar.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture)
                         + " of " + selectedStar.GetOperableMines().ToString(System.Globalization.CultureInfo.InvariantCulture);
            population.Text = selectedStar.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);

            resourceDisplay.ResourceRate = selectedStar.GetResourceRate();

            if (selectedStar.OnlyLeftover == false)
            {
                resourceDisplay.ResearchBudget = empireState.ResearchBudget;
            }
            else
            {
                // We treat Stars contributing only leftover resources as having
                // a 0% budget allocation.

                resourceDisplay.ResearchBudget = 0;
            }

            resourceDisplay.Value = selectedStar.ResourcesOnHand;

            scannerRange.Text = selectedStar.ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
            scannerType.Text = selectedStar.ScannerType;

            if (selectedStar.Starbase == null)
            {
                starbasePanel.Text = "No Starbase";
                starbasePanel.Enabled = false;
            }
            else
            {
                Fleet starbase = selectedStar.Starbase;
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
            }

            List<string> fleetnames = new List<string>();
            fleetsInOrbit = new Dictionary<string, Fleet>();
            foreach (Fleet fleet in empireState.OwnedFleets.Values)
            {
                if (fleet.InOrbit != null &&  fleet.InOrbit.Name == selectedStar.Name && !fleet.IsStarbase)
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

        /// <Summary>
        /// Access to the Star Report whose details are displayed in the panel.
        /// </Summary>
        public Star Value
        {
            set { SetStarDetails(value); }
            get { return selectedStar; }
        }
        private Fleet GetSelectedFleetInOrbit()
        {
            if (comboFleetsInOrbit.SelectedItem == null)
            {
                return null;
            }

            Fleet fleet;
            if (!fleetsInOrbit.TryGetValue(comboFleetsInOrbit.SelectedItem.ToString(), out fleet))
            {
                return null;
            }

            return fleet;
        }

        private void ComboFleetsInOrbit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetInOrbit();
            if (fleet == null)
            {
                meterFuel.Value = 0;
                meterFuel.Maximum = 0;
                meterCargo.CargoLevels = new Cargo();
                meterCargo.Maximum = 0;
            }
            else
            {
                meterFuel.Maximum = fleet.TotalFuelCapacity;
                meterFuel.Value = (int)fleet.FuelAvailable;
                meterCargo.Maximum = fleet.TotalCargoCapacity;
                meterCargo.CargoLevels = fleet.Cargo;
            }
            Invalidate();
        }

        private void ButtonGoto_Click(object sender, EventArgs e)
        {
            Fleet fleet = GetSelectedFleetInOrbit();
            if (fleet != null)
            {
                UpdateListeners(fleet);
            }
        }

        private void ButtonCargo_Click(object sender, EventArgs e)
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
                        UpdateFields();
                    }

                    ComboFleetsInOrbit_SelectedIndexChanged(null, null);
                }
                catch
                {
                    Report.Debug("FleetDetail.cs : CargoButton_Click() - Failed to open cargo dialog.");
                }
            }
        }
        
        private void UpdateListeners(Item item)
        {
            if (DetailSelectionChangedEvent != null && item.Owner == empireState.Id)
            {
                DetailSelectionChangedEvent(this, new DetailSelectionArgs(item));
            }
            if (SummarySelectionChangedEvent != null)
            {
                SummarySelectionArgs summaryArgs;
                
                if (item.Type == ItemType.Fleet)
                {
                    summaryArgs = new SummarySelectionArgs(empireState.FleetReports[item.Key]);
                }
                else
                {
                    summaryArgs = new SummarySelectionArgs(empireState.StarReports[item.Name]);
                }
                
                SummarySelectionChangedEvent(this, summaryArgs);
            }
            if (CursorChangedEvent != null)
            {
                CursorChangedEvent(this, new CursorArgs((Point)item.Position));
            }    
        }
    }
}
