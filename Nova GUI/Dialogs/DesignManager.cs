// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Dialog to display and optionally delete designs.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nova
{
   public partial class DesignManager : Form
   {

      private GuiState   StateData = null;
      private Intel TurnData  = null;

// ============================================================================
// Dialog constrution.
// ============================================================================

      public DesignManager()
      {
         InitializeComponent();
         StateData  = GuiState.Data;
         TurnData   = StateData.InputTurn;
         HullGrid.ModuleSelected += DesignModuleSelected;
      }


// ============================================================================
// Done button pressed. Just exit the dialog.
// ============================================================================

      private void Done_Click(object sender, EventArgs e)
      {
         Close();
      }


// ============================================================================
// Populate the available designs items list box with the existing ship designs
// (we don't include anything that does not have a hull in the list).
// ============================================================================

      private void DesignManager_Load(object sender, EventArgs e)
      {
         // Populate the "Design Owner" ComboBox with a list of players and
         // select the current race as the default

         foreach (string raceName in TurnData.AllRaceNames) {
            DesignOwner.Items.Add(raceName);
         }

         DesignOwner.SelectedItem = StateData.RaceName;
         ListDesigns(StateData.RaceName);
      }


// ============================================================================
// Populate the design list with a list of designs for a specified race if we
// know what that design is (we only discover other race's designs after a
// battle. We also only deal with ship or starbase designs.
// ============================================================================

      private void ListDesigns(string raceName)
      {
         DesignList.Items.Clear();
         DesignList.BeginUpdate();

         foreach (Design design in TurnData.AllDesigns.Values) {
            if (design.Type == "Ship" || design.Type == "Starbase") {
               if (design.Owner == raceName) {
                  if (raceName == StateData.RaceName ||
                      StateData.KnownEnemyDesigns.Contains(design.Key)) {

                     AddToDesignList(design);
                  }
               }
            }
         }

        DesignList.EndUpdate();
      }


// ============================================================================
// Add a design into the list of designs
// ============================================================================

      private void AddToDesignList(Design design)
      {
         ListViewItem itemToAdd = new ListViewItem();
                  
         itemToAdd.Text = design.Name;
         itemToAdd.Tag  = design;
                  
         if (design.Owner == StateData.RaceName) {
            int quantity  = CountDesigns(design);
            itemToAdd.SubItems.Add(quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
         }
         else {
            itemToAdd.SubItems.Add("Unknown");
         }

         DesignList.Items.Add(itemToAdd);
      }


// ============================================================================
// Count the number of ships based on a specific design
// ============================================================================

      private int CountDesigns(Design design)
      {
         int quantity = 0;

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            foreach (Ship ship in fleet.FleetShips) {   
               if (ship.Design.Key == design.Key) {
                  quantity++;
               }
            }
         }

         return quantity;
      }


// ============================================================================
// Display Design
// ============================================================================

      private void DisplayDesign(ShipDesign design)
      {
         design.Update(); 
         Hull HullProperties = design.ShipHull.Properties["Hull"] as Hull;
         HullGrid.ActiveModules = HullProperties.Modules;
         HullImage.Image        = design.ShipHull.ComponentImage;
         DesignResources.Value  = design.Cost;
         DesignName.Text        = design.Name;
         ShipMass.Text          = design.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
         ShipArmor.Text         = design.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
         ShipShields.Text       = design.Shield.ToString(System.Globalization.CultureInfo.InvariantCulture);
         CargoCapacity.Text     = design.CargoCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);

         if (design.Type == "Starbase") {
            CapacityType.Text  = "Dock Capacity";
            CapacityUnits.Text = "kT";
            MaxCapacity.Text   = design.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
         }
         else {
            CapacityType.Text  = "Fuel Capacity";
            CapacityUnits.Text = "mg";
            MaxCapacity.Text   = design.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
         }
      }


// ============================================================================
// A new design has been selected, display it.
// ============================================================================

      private void DesignList_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (DesignList.SelectedItems.Count <= 0) return;

         string name = DesignList.SelectedItems[0].Text;
         string race = DesignOwner.SelectedItem.ToString();

         ShipDesign design = 
                    TurnData.AllDesigns[race + "/" + name] as ShipDesign;
         
         DisplayDesign(design);
      }


// ============================================================================
// Deal with a hull module being selected
// ============================================================================

      private void DesignModuleSelected(object sender, EventArgs e)
      {
         Panel gridModule  = sender as Panel;
         HullModule module = gridModule.Tag as HullModule;

         if (module.AllocatedComponent == null) {
            return;
         }

         NovaCommon.Component component = module.AllocatedComponent;

         ComponentSummary.Text = component.Description;
      }


// ============================================================================
// The delete button has been pressed. Confirm he really means it and, if he
// does, delete all ships based on that design, if that leaves the fleet
// containing the ship empty delete that too. Then delete the actual design.
// ============================================================================

      private void Delete_Click(object sender, EventArgs e)
      {
         string text = "You are about to delete the selected design."
         + "If you do this you will destroy all ships of that design."
         + "\r\n"
         + "Are you sure you want to do this?";
         
         DialogResult result = MessageBox.Show(text, "Nova - Warning", 
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning);


         Design design = DesignList.SelectedItems[0].Tag as Design;


         // Note that we are not allowed to delete the ships or fleets on the
         // scan through FleetShips and AlFleetsand as that is not allowed (it
         // destroys the validity of the iterator). Consequently we identify
         // anything that need deleting and remove them separately from their
         // identification.

         List<Fleet> fleetsToRemove = new List<Fleet>();

         foreach (Fleet fleet in TurnData.AllFleets.Values) {

            List<Ship> shipsToRemove = new List<Ship>();

            foreach (Ship ship in fleet.FleetShips) {
               if (ship.Design.Key == design.Key) {
                  shipsToRemove.Add(ship);
               }
            }
            
            foreach (Ship ship in shipsToRemove) {
               fleet.FleetShips.Remove(ship);
            }

            if (fleet.FleetShips.Count == 0) {
               fleetsToRemove.Add(fleet);
            }
         }

         foreach (Fleet fleet in fleetsToRemove) {
            TurnData.AllFleets.Remove(fleet.Key);
            StateData.DeletedFleets.Add(fleet.Key);
         }

         TurnData.AllDesigns.Remove(design.Key);
         DesignOwner_SelectedIndexChanged(null, null);

         // Ensure the star map is updated in case we've completely removed any
         // fleets that are being displayed.

         Utilities.MapRefresh();

      }


// ============================================================================
// A new race has been selected. Update the design list to reflect this and
// reset the design display. Only allow the delete button to be used if the
// race selected is the current one being played.
// ============================================================================

      private void DesignOwner_SelectedIndexChanged(object sender, EventArgs e)
      {
         ListDesigns(DesignOwner.SelectedItem.ToString());
         HullGrid.Clear(true);
         DesignName.Text       = null;
         DesignResources.Value = new NovaCommon.Resources();
         ShipMass.Text         = "0";
         MaxCapacity.Text      = "0";
         ShipArmor.Text       = "0";
         ShipShields.Text      = "0";
         ShipCloak.Text        = "0";

         string race = DesignOwner.SelectedItem.ToString();

         if (race == StateData.RaceName) {
            Delete.Enabled = true;
         }
         else {
            Delete.Enabled = false;
         }
      }

   }

}
