#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011, 2012 The Stars-Nova Project
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
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
        
    /// <Summary>
    /// Dialog to display and optionally delete designs.
    /// </Summary>
    public partial class DesignManager : Form
    {
        private readonly ClientData clientState;

        /// <Summary>
        /// This event should be fired when a waypoint is deleted,
        /// so the StarMap updates right away.
        /// </Summary>
        public event RefreshStarMap RefreshStarMapEvent;
        
        /// <Summary>
        /// Initializes a new instance of the DesignManager class.
        /// </Summary>
        public DesignManager(ClientData clientState)
        {
            InitializeComponent();
            this.clientState = clientState;
            this.hullGrid.ModuleSelected += DesignModuleSelected;
        }

        /// <Summary>
        /// Done button pressed. Just exit the dialog.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <Summary>
        /// Populate the available designs items list box with the existing ship designs
        /// (we don't include anything that does not have a hull in the list).
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignManager_Load(object sender, EventArgs e)
        {
            // Populate the "Design Owner" ComboBox with a list of players and
            // select the current race as the default

            ComboBoxItem<int> thisRace = new ComboBoxItem<int>(clientState.EmpireState.Race.Name, clientState.EmpireState.Id);
            comboDesignOwner.Items.Add(thisRace);

            foreach (ushort empireId in clientState.EmpireState.EmpireReports.Keys)
            {
                comboDesignOwner.Items.Add(new ComboBoxItem<int>(clientState.EmpireState.EmpireReports[empireId].RaceName, empireId));
            }

            comboDesignOwner.SelectedItem = thisRace;
            ListDesigns(clientState.EmpireState.Id);
        }

        private ushort GetSelectedEmpireId()
        {
            ComboBoxItem<int> item = this.comboDesignOwner.SelectedItem as ComboBoxItem<int>;
            if (item != null)
            {
                return (ushort)(item.Tag);
            }
            return 0;
        }

        /// <Summary>
        /// A new design has been selected, display it.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.designList.SelectedItems.Count <= 0)
            {
                return;
            }

            ShipDesign design;
            long designKey = (this.designList.SelectedItems[0].Tag as ShipDesign).Key;
            ushort owner = (this.designList.SelectedItems[0].Tag as ShipDesign).Owner;

            if (owner == clientState.EmpireState.Id)
            {
                design = clientState.EmpireState.Designs[designKey];
            }
            else
            {
                design = clientState.EmpireState.EmpireReports[owner].Designs[designKey];
            }

            DisplayDesign(design);
        }

        /// <Summary>
        /// Deal with a hull module being selected
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignModuleSelected(object sender, EventArgs e)
        {            
            Panel gridModule = sender as Panel;         
            
            if (gridModule.Tag == null)
            {
                return;
            }
            
            HullModule module = gridModule.Tag as HullModule;

            if (module.AllocatedComponent == null)
            {
                return;
            }

            Nova.Common.Components.Component component = module.AllocatedComponent;

            this.componentSummary.Text = component.Description;
        }

        /// <Summary>
        /// The delete button has been pressed. Confirm he really means it and, if he
        /// does, delete all ships based on that design, if that leaves the fleet
        /// containing the ship empty delete that too. Then delete the actual design.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
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

            ShipDesign design = designList.SelectedItems[0].Tag as ShipDesign;

            DesignCommand command = new DesignCommand(CommandMode.Delete, design.Key);
            
            if (command.isValid(clientState.EmpireState))
            {
                clientState.Commands.Push(command);
                command.ApplyToState(clientState.EmpireState);
            }
            
            DesignOwner_SelectedIndexChanged(null, null);

            // Ensure the Star map is updated in case we've completely removed any
            // fleets that are being displayed.

            RefreshStarMapEvent();

        }

        /// <Summary>
        /// A new race has been selected. Update the design list to reflect this and
        /// reset the design display. Only allow the delete button to be used if the
        /// race selected is the current one being played.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort empireId = GetSelectedEmpireId();
            ListDesigns(empireId);
            hullGrid.Clear(true);
            designName.Text = null;
            designResources.Value = new Nova.Common.Resources();
            shipMass.Text = "0";
            maxCapacity.Text = "0";
            shipArmor.Text = "0";
            shipShields.Text = "0";
            shipCloak.Text = "0";
           
            if (empireId == clientState.EmpireState.Id)
            {
                delete.Enabled = true;
            }
            else
            {
                delete.Enabled = false;
            }
        }

        /// <Summary>
        /// Populate the design list with a list of designs for a specified race if we
        /// know what that design is (we only discover other race's designs after a
        /// battle. We also only deal with ship or starbase designs.
        /// </Summary>
        /// <param name="raceName"></param>
        private void ListDesigns(ushort ownerId)
        {
            designList.Items.Clear();
            designList.BeginUpdate();
            
            Dictionary<long, ShipDesign> designSource;

            if (ownerId == clientState.EmpireState.Id)
            {     
                designSource = clientState.EmpireState.Designs;
            }
            else
            {
                designSource = clientState.EmpireState.EmpireReports[ownerId].Designs;
            }
            
            foreach (ShipDesign design in designSource.Values)
            {
                AddToDesignList(design);    
            }

            designList.EndUpdate();
        }

        /// <Summary>
        /// Add a design into the list of designs
        /// </Summary>
        /// <param name="design">The design to add to the design list.</param>
        private void AddToDesignList(ShipDesign design)
        {
            ListViewItem itemToAdd = new ListViewItem();

            itemToAdd.Text = design.Name;
            itemToAdd.Tag = design;

            if (design.Owner == clientState.EmpireState.Id)
            {
                int quantity = CountDesigns(design);
                itemToAdd.SubItems.Add(quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                itemToAdd.SubItems.Add("Unknown");
            }

            designList.Items.Add(itemToAdd);
        }

        /// <Summary>
        /// Count the number of ships based on a specific design
        /// </Summary>
        /// <param name="design">The design to count instances of.</param>
        /// <returns>The number of ships of the given design that have been built.</returns>
        private int CountDesigns(ShipDesign design)
        {
            int quantity = 0;

            foreach (Fleet fleet in clientState.EmpireState.OwnedFleets.Values)
            {
                foreach (ShipToken token in fleet.Tokens.Values)
                {
                    if (token.Design.Key == design.Key)
                    {
                        quantity++;
                    }
                }
            }

            return quantity;
        }

        /// <Summary>
        /// Display Design
        /// </Summary>
        /// <param name="design">The <see cref="ShipDesign"/> to display.</param>
        private void DisplayDesign(ShipDesign design)
        {
            design.Update();
            Hull hullProperties = design.Blueprint.Properties["Hull"] as Hull;
            this.hullGrid.ActiveModules = hullProperties.Modules;
            this.hullImage.Image = design.Blueprint.ComponentImage;
            this.designResources.Value = design.Cost;
            this.designName.Text = design.Name;
            this.shipMass.Text = design.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.shipArmor.Text = design.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.shipShields.Text = design.Shield.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.cargoCapacity.Text = design.CargoCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (design.Type == ItemType.Starbase)
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
    }
}
