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
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.ControlLibrary;

    /// <Summary>
    /// Dialog for designing a ship or starbase.
    /// </Summary>
    public partial class ShipDesignDialog : System.Windows.Forms.Form
    {
        private readonly ClientData clientState;
        private readonly Dictionary<string, Component> allComponents;
        private readonly Dictionary<long, Design> allDesigns;
        private readonly Dictionary<string, int> imageIndices = new Dictionary<string, int>();
        private readonly ImageList componentImages = new ImageList();

        private Component selectedHull;
        private int designMass;
        private ShipIcon shipIcon;


        /// <Summary>
        /// Initializes a new instance of the ShipDesignDialog class.
        /// </Summary>
        public ShipDesignDialog(ClientData clientState)
        {
            InitializeComponent();

            // Some abbreviations (just to save a bit of typing)

            this.clientState = clientState;
            this.allComponents = Nova.Common.Components.AllComponents.Data.Components;
            this.allDesigns = clientState.EmpireState.Designs;

            this.componentImages.ImageSize = new Size(64, 64);
            this.componentImages.ColorDepth = ColorDepth.Depth32Bit;

            PopulateComponentList();
            TreeView.ExpandAll();

            // Populate the combo-box control with the available hulls, select the
            // first one in the list as the default. Also, make the default design
            // name the same as the hull name as a first guess. 
            HullList.Items.Clear();
            foreach (Component component in clientState.EmpireState.AvailableComponents.Values)
            {
                if (component.Properties.ContainsKey("Hull"))
                {
                    HullList.Items.Add(component.Name);
                }
            }

            if (HullList.Items.Count != 0)
            {
                HullList.SelectedIndex = 0;
                string selectedHullName = HullList.SelectedItem as string;
                this.selectedHull = clientState.EmpireState.AvailableComponents[selectedHullName];
                this.selectedHull.Name = selectedHullName;
                HullGrid.HullName = selectedHullName;
                UpdateHullFields();
                SaveButton.Enabled = true;
            }
            else
            {
                SaveButton.Enabled = false;
            }

            // Populate the tree view control from the AvailableComponents
            List<string> techList = new List<string>();

            foreach (Component component in clientState.EmpireState.AvailableComponents.Values)
            {
                if (component.Type.ToDescription().Contains("Planetary"))
                {
                    continue;
                }
                if (component.Type == ItemType.Defenses)
                {
                    continue;
                }
                if (component.Type == ItemType.Hull)
                {
                    continue;
                }

                if (!techList.Contains(component.Type.ToDescription()))
                {
                    techList.Add(component.Type.ToDescription());
                }
            }
            techList.Sort();

            TreeView.BeginUpdate();
            TreeView.Nodes.Clear();
            TreeView.Nodes.Add("Available Technology");
            foreach (string techGroup in techList)
            {
                if (!TreeView.Nodes[0].Nodes.ContainsKey(techGroup))
                {
                    TreeView.Nodes[0].Nodes.Add(techGroup, techGroup); // key, Text
                }
            }

            foreach (TreeNode node in TreeView.Nodes[0].Nodes)
            {
                node.Tag = node.Text;
            }
            TreeView.EndUpdate();
        }


        /// <Summary>
        /// Save the the design when the OK button is pressed
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OK_Click(object sender, System.EventArgs e)
        {
            ShipDesign newDesign = new ShipDesign(clientState.EmpireState.GetNextDesignKey());
            Hull hullProperties = this.selectedHull.Properties["Hull"] as Hull;

            hullProperties.Modules = HullGrid.ActiveModules;
            newDesign.Name = DesignName.Text;
            newDesign.Owner = clientState.EmpireState.Id;
            newDesign.ShipHull = this.selectedHull;
            newDesign.Cost = DesignResources.Value;
            newDesign.Mass = Convert.ToInt32(ShipMass.Text);
            newDesign.Icon = shipIcon;
            newDesign.Update();

            if (hullProperties.IsStarbase)
            {
                newDesign.Type = ItemType.Starbase;
            }
            else
            {
                newDesign.Type = ItemType.Ship;
                if (newDesign.Engine == null)
                {
                    Report.Error("A ship design must have an engine");
                    return;
                }
            }

            /* 
             * If you create a new design you might accidently give it the same 
             * name as another design, esspecialy if you keep the hull name for 
             * the ship name. However if you are edditing a design then this 
             * might be exactly what you want to do. Need to at least ask the 
             * user. TODO (priority 6).
           if (AllDesigns.Contains(newDesign.Key)) 
           {
              Report.Error("Design names must be unique");
              return;
             
           }
            */

            this.allDesigns[newDesign.Key] = newDesign;
            Close();
        }

        /// <Summary>
        /// A new tree node has been selected. Update the list control
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void TreeNodeSelected(object sender, TreeViewEventArgs e)
        {
            Description.Text = null;

            TreeNode node = e.Node;
            if (node.Parent == null)
            {
                return;
            }

            string nodeType = node.Text as string;

            ListView.Items.Clear();
            if (nodeType == null)
            {
                return;
            }

            ListView.LargeImageList = this.componentImages;

            foreach (Component component in clientState.EmpireState.AvailableComponents.Values)
            {
                if (component.Type.ToDescription() == nodeType)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = component.Name;
                    item.ImageIndex = this.imageIndices[component.Name];

                    ListView.Items.Add(item);
                }
            }
        }

        /// <Summary>
        /// A new Item has been selected. Update the cost box and description.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ListSelectionChanged(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem item = ListView.SelectedItems[0];
            Component selection = this.allComponents[item.Text];
            ComponentCost.Value = selection.Cost;
            ComponentMass.Text = selection.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            Description.Text = selection.Description;

            if (selection.Type == ItemType.Engine)
            {
                graph1.Data = (selection.Properties["Engine"] as Engine).FuelConsumption;
            }
            else
            {
                graph1.Data = null;
            }
            // Call the Mouse down routine (it must have gone down to change the
            // selection) so that we can select and drag in one operation (rather
            // than select and then drag as two separate steps).

            ListView_MouseDown(null, null);
        }

        /// <Summary>
        /// Instigate Drag and Drop of the selected ListView Item
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (ListView.SelectedItems.Count <= 0)
            {
                return;
            }

            HullGrid.DragDropData dragData = new HullGrid.DragDropData();

            ListViewItem item = ListView.SelectedItems[0];
            dragData.HullName = this.selectedHull.Name;
            dragData.ComponentCount = 1;
            dragData.SelectedComponent = this.allComponents[item.Text];
            dragData.Operation = DragDropEffects.Copy;

            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                dragData.ComponentCount = 10;
            }

            DragDropEffects result = DoDragDrop(dragData, DragDropEffects.Copy);

            if (result != DragDropEffects.Copy)
            {
                return;
            }

            // The component has been dropped into the design. Update the relevant
            // design Summary fields.
            UpdateDesignParameters();
        }

        private void HullGrid_ModuleUpdated(object sender, EventArgs e)
        {
            // A module has been updated. Update the relevant design Summary fields.
            UpdateDesignParameters();
        }

        /// <Summary>
        /// Update cost and primary characteristics.
        /// </Summary>
        private void UpdateDesignParameters()
        {
            Hull hull = this.selectedHull.Properties["Hull"] as Hull;
            Resources cost = this.selectedHull.Cost;
            int mass = this.selectedHull.Mass;
            int armor = hull.ArmorStrength;
            int shield = 0;
            int cargo = hull.BaseCargo;
            int fuel = hull.FuelCapacity;

            foreach (HullModule module in HullGrid.ActiveModules)
            {
                Component component = module.AllocatedComponent;
                if (component == null)
                {
                    continue;
                }

                cost += module.ComponentCount * component.Cost;
                mass += module.ComponentCount * component.Mass;

                if (component.Properties.ContainsKey("Armor"))
                {
                    IntegerProperty armorProperty = component.Properties["Armor"] as IntegerProperty;
                    armor += module.ComponentCount * armorProperty.Value;
                }
                if (component.Properties.ContainsKey("Shield"))
                {
                    IntegerProperty shieldProperty = component.Properties["Shield"] as IntegerProperty;
                    shield += module.ComponentCount * shieldProperty.Value;
                }
                if (component.Properties.ContainsKey("Cargo"))
                {
                    IntegerProperty cargoProperty = component.Properties["Cargo"] as IntegerProperty;
                    cargo += module.ComponentCount * cargoProperty.Value;
                }
                if (component.Properties.ContainsKey("Fuel"))
                {
                    Fuel fuelProperty = component.Properties["Fuel"] as Fuel;
                    fuel += module.ComponentCount * fuelProperty.Capacity;
                }
            }

            DesignResources.Value = cost;
            this.designMass = mass;

            ShipMass.Text = this.designMass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ShipArmor.Text = armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ShipShields.Text = shield.ToString(System.Globalization.CultureInfo.InvariantCulture);
            CargoCapacity.Text = cargo.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (!hull.IsStarbase)
            {
                MaxCapacity.Text = fuel.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <Summary>
        /// Hull selection changed. Ensure we take a copy of the hull design so that we
        /// don't end up messing with the master copy.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void HullList_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedHullName = HullList.SelectedItem as string;

            DesignName.Text = selectedHullName;
            Nova.Common.Components.Component hull = clientState.EmpireState.AvailableComponents[selectedHullName];
            this.selectedHull = new Nova.Common.Components.Component(hull);
            this.selectedHull.Name = selectedHullName;
            HullGrid.HullName = selectedHullName;

            UpdateHullFields();
        }

        /// <Summary>
        /// Select the previous available icon for this ship design.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PrevImageButton_Click(object sender, EventArgs e)
        {
            shipIcon--;
            HullImage.Image = shipIcon.Image;
        }

        /// <Summary>
        /// Select the next available icon for this ship design.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextImageButton_Click(object sender, EventArgs e)
        {
            shipIcon++;
            HullImage.Image = shipIcon.Image;
        }
        

        /// <Summary>
        /// Draw the seleced hull design by filling in the hull grid and the populating
        /// the costs and characteristics fields on the form.
        /// </Summary>
        /// <remarks>
        /// ??? (priority 5) We don't seem to have a ShipDesign at this stage, just a Hull component
        /// with attached modules? This makes determining Summary information difficult
        /// as that is what the ShipDesign is for. Need to decide if using a ShipDesign
        /// from the start would be better.
        /// </remarks>
        private void UpdateHullFields()
        {
            Hull hullProperties = this.selectedHull.Properties["Hull"] as Hull;
            HullGrid.ActiveModules = hullProperties.Modules;
            // get the base hull of this icon

            /*
            string[] baseHullParts = selectedHull.ImageFile.Split(System.IO.Path.DirectorySeparatorChar);

            string baseHull = baseHullParts[baseHullParts.Length - 1].Substring(0, baseHullParts[baseHullParts.Length - 1].IndexOf('.') - Global.ShipIconNumberingLength);

            if (AllShipIcons.Data.Hulls.ContainsKey(baseHull))
            {
                ShipIcon = (ShipIcon)AllShipIcons.Data.Hulls[baseHull][0];
            }
            else
            {
                ShipIcon = (ShipIcon)AllShipIcons.Data.IconList[0];
            }
            HullImage.Image = ShipIcon.Image;
            */
            shipIcon = AllShipIcons.Data.GetIconBySource(selectedHull.ImageFile);
            HullImage.Image = shipIcon.Image;

            Description.Text = this.selectedHull.Description;

            if (hullProperties.IsStarbase)
            {
                CapacityType.Text = "Dock Capacity";
                CapacityUnits.Text = "kT";
                MaxCapacity.Text = hullProperties.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                CapacityType.Text = "Fuel Capacity";
                CapacityUnits.Text = "mg";
                MaxCapacity.Text = hullProperties.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            UpdateDesignParameters();
        }

        /// <Summary>
        /// Build a table of components available. Note that some components are only
        /// availble if certain racial traits are selected.
        /// </Summary>
        private void PopulateComponentList()
        {
            int index = 0;

            foreach (Component component in clientState.EmpireState.AvailableComponents.Values)
            {
                // TODO (priority 4) - work out why it sometimes is null.
                if (component != null)
                {
                    this.imageIndices[component.Name] = index;
                    this.componentImages.Images.Add(component.ComponentImage);
                    index++;
                }
            }
        }
    }
}
