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
// Dialog to display and optionally delete designs.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.WinForms.Gui
{
    /// <Summary>
    /// Dialog to display and optionally delete designs.
    /// </Summary>
    public partial class DesignManager : Form
    {
        private readonly ClientState stateData;
        private readonly Intel turnData;

        /// <Summary>
        /// This event should be fired when a waypoint is deleted,
        /// so the StarMap updates right away.
        /// </Summary>
        public event RefreshStarMap RefreshStarMapEvent;
        
        #region Construction

        /// <Summary>
        /// Initializes a new instance of the DesignManager class.
        /// </Summary>
        public DesignManager()
        {
            InitializeComponent();
            this.stateData = ClientState.Data;
            this.turnData = this.stateData.InputTurn;
            this.hullGrid.ModuleSelected += DesignModuleSelected;
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Done button pressed. Just exit the dialog.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void Done_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Populate the available designs items list box with the existing ship designs
        /// (we don't include anything that does not have a hull in the list).
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DesignManager_Load(object sender, EventArgs e)
        {
            // Populate the "Design Owner" ComboBox with a list of players and
            // select the current race as the default

            foreach (string raceName in this.turnData.AllRaceNames)
            {
                this.designOwner.Items.Add(raceName);
            }

            this.designOwner.SelectedItem = this.stateData.RaceName;
            ListDesigns(this.stateData.RaceName);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// A new design has been selected, display it.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DesignList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.designList.SelectedItems.Count <= 0)
            {
                return;
            }

            string name = this.designList.SelectedItems[0].Text;
            string race = this.designOwner.SelectedItem.ToString();

            ShipDesign design =
                       this.turnData.AllDesigns[race + "/" + name] as ShipDesign;

            DisplayDesign(design);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Deal with a hull module being selected
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DesignModuleSelected(object sender, EventArgs e)
        {
            Panel gridModule = sender as Panel;
            HullModule module = gridModule.Tag as HullModule;

            if (module.AllocatedComponent == null)
            {
                return;
            }

            Nova.Common.Components.Component component = module.AllocatedComponent;

            this.componentSummary.Text = component.Description;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// The delete button has been pressed. Confirm he really means it and, if he
        /// does, delete all ships based on that design, if that leaves the fleet
        /// containing the ship empty delete that too. Then delete the actual design.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Delete_Click(object sender, EventArgs e)
        {
            string text =
@"You are about to delete the selected design.
If you do this you will destroy all ships of that design.
           
Are you sure you want to do this?";

            DialogResult result = MessageBox.Show(
                text,
                "Nova - Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, 
                MessageBoxDefaultButton.Button2, 
                MessageBoxOptions.DefaultDesktopOnly);

            if (result != DialogResult.Yes)
            {
                return;
            }

            Design design = this.designList.SelectedItems[0].Tag as Design;


            // Note that we are not allowed to delete the ships or fleets on the
            // scan through FleetShips and AllFleets and as that is not allowed (it
            // destroys the validity of the iterator). Consequently we identify
            // anything that needs deleting and remove them separately from their
            // identification.

            List<Fleet> fleetsToRemove = new List<Fleet>();

            foreach (Fleet fleet in this.turnData.AllFleets.Values)
            {

                List<Ship> shipsToRemove = new List<Ship>();

                foreach (Ship ship in fleet.FleetShips)
                {
                    if (ship.DesignKey == design.Key)
                    {
                        shipsToRemove.Add(ship);
                    }
                }

                foreach (Ship ship in shipsToRemove)
                {
                    fleet.FleetShips.Remove(ship);
                }

                if (fleet.FleetShips.Count == 0)
                {
                    fleetsToRemove.Add(fleet);
                }
            }

            foreach (Fleet fleet in fleetsToRemove)
            {
                this.turnData.AllFleets.Remove(fleet.Key);
                this.stateData.DeletedFleets.Add(fleet.Key);
            }

            stateData.DeletedDesigns.Add(design.Key);
            this.turnData.AllDesigns.Remove(design.Key);
            DesignOwner_SelectedIndexChanged(null, null);

            // Ensure the Star map is updated in case we've completely removed any
            // fleets that are being displayed.

            RefreshStarMapEvent();

        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// A new race has been selected. Update the design list to reflect this and
        /// reset the design display. Only allow the delete button to be used if the
        /// race selected is the current one being played.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DesignOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListDesigns(this.designOwner.SelectedItem.ToString());
            this.hullGrid.Clear(true);
            this.designName.Text = null;
            this.designResources.Value = new Nova.Common.Resources();
            this.shipMass.Text = "0";
            this.maxCapacity.Text = "0";
            this.shipArmor.Text = "0";
            this.shipShields.Text = "0";
            this.shipCloak.Text = "0";

            string race = this.designOwner.SelectedItem.ToString();

            if (race == this.stateData.RaceName)
            {
                this.delete.Enabled = true;
            }
            else
            {
                this.delete.Enabled = false;
            }
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Populate the design list with a list of designs for a specified race if we
        /// know what that design is (we only discover other race's designs after a
        /// battle. We also only deal with ship or starbase designs.
        /// </Summary>
        /// <param name="raceName"></param>
        /// ----------------------------------------------------------------------------
        private void ListDesigns(string raceName)
        {
            this.designList.Items.Clear();
            this.designList.BeginUpdate();

            foreach (Design design in this.turnData.AllDesigns.Values)
            {
                if (design.Type == "Ship" || design.Type == "Starbase")
                {
                    if (design.Owner == raceName)
                    {
                        if (raceName == this.stateData.RaceName ||
                            this.stateData.KnownEnemyDesigns.Contains(design.Key))
                        {

                            AddToDesignList(design);
                        }
                    }
                }
            }

            this.designList.EndUpdate();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Add a design into the list of designs
        /// </Summary>
        /// <param name="design">The design to add to the design list.</param>
        /// ----------------------------------------------------------------------------
        private void AddToDesignList(Design design)
        {
            ListViewItem itemToAdd = new ListViewItem();

            itemToAdd.Text = design.Name;
            itemToAdd.Tag = design;

            if (design.Owner == this.stateData.RaceName)
            {
                int quantity = CountDesigns(design);
                itemToAdd.SubItems.Add(quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                itemToAdd.SubItems.Add("Unknown");
            }

            this.designList.Items.Add(itemToAdd);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Count the number of ships based on a specific design
        /// </Summary>
        /// <param name="design">The design to count instances of.</param>
        /// <returns>The number of ships of the given design that have been built.</returns>
        /// ----------------------------------------------------------------------------
        private int CountDesigns(Design design)
        {
            int quantity = 0;

            foreach (Fleet fleet in this.turnData.AllFleets.Values)
            {
                foreach (Ship ship in fleet.FleetShips)
                {
                    if (ship.DesignKey == design.Key)
                    {
                        quantity++;
                    }
                }
            }

            return quantity;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Display Design
        /// </Summary>
        /// <param name="design">The <see cref="ShipDesign"/> to display.</param>
        /// ----------------------------------------------------------------------------
        private void DisplayDesign(ShipDesign design)
        {
            design.Update();
            Hull hullProperties = design.ShipHull.Properties["Hull"] as Hull;
            this.hullGrid.ActiveModules = hullProperties.Modules;
            this.hullImage.Image = design.ShipHull.ComponentImage;
            this.designResources.Value = design.Cost;
            this.designName.Text = design.Name;
            this.shipMass.Text = design.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.shipArmor.Text = design.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.shipShields.Text = design.Shield.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.cargoCapacity.Text = design.CargoCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (design.Type == "Starbase")
            {
                this.capacityType.Text = "Dock Capacity";
                this.capacityUnits.Text = "kT";
                this.maxCapacity.Text = design.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                this.capacityType.Text = "Fuel Capacity";
                this.capacityUnits.Text = "mg";
                this.maxCapacity.Text = design.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        #endregion
    }

}
