// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Manage an individual fleet
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nova
{

// ============================================================================
// Manage the details of a fleet (composition, etc.).
// ============================================================================

   public partial class ManageFleetDialog : Form
   {
      private Fleet       SelectedFleet = null;
      private Hashtable   AllFleets     = null;


// ============================================================================
// Construction
// ============================================================================

      public ManageFleetDialog()
      {
         InitializeComponent();
         AllFleets = GUIstate.Data.InputTurn.AllFleets;
      }


// ============================================================================
// Update the dialog fields
// ============================================================================

      private void UpdateDialogDetails()
      {
         FleetName.Text    = SelectedFleet.Name;
         Hashtable designs = SelectedFleet.Composition;

         FleetComposition.Items.Clear();
         foreach (string key in designs.Keys) {
            ListViewItem listItem = new ListViewItem(key);
            listItem.SubItems.Add(((int) designs[key]).ToString());
            FleetComposition.Items.Add(listItem);
         }

         CoLocatedFleets.Items.Clear();

         foreach (Fleet fleet in AllFleets.Values) {
            if (fleet.Name != SelectedFleet.Name) {
               if (fleet.Position == SelectedFleet.Position) {
                  CoLocatedFleets.Items.Add(fleet.Name);
               }
            }
         }
      }


// ============================================================================
// Rename a fleet. The rename is only allowed if the fleet access key is
// unique.
// ============================================================================

      private void RenameButton_Click(object sender, EventArgs e)
      {
         RenameFleet renameDialog       = new RenameFleet();
         renameDialog.ExistingName.Text = FleetName.Text;

         renameDialog.ShowDialog();
         renameDialog.Dispose();

         FleetName.Text = SelectedFleet.Name;


      }


// ============================================================================
// Select the fleet to be managed.
// ============================================================================

      public Fleet ManagedFleet
      {
         set {SelectedFleet = value; UpdateDialogDetails(); }
         get {return SelectedFleet; }
      }


// ============================================================================
// A co-located fleet is selected, activate the merge facility
// ============================================================================

      private void CoLocatedFleets_SelectedIndexChanged(object sender,
                                                        EventArgs e)
      {
         MergeButton.Enabled = true;
      }


// ============================================================================
// Merge the ships from a co-located fleet into the selected fleet
// ============================================================================

      private void MergeButton_Click(object sender, EventArgs e)
      {
         string fleetName = CoLocatedFleets.SelectedItems[0].Text;
         string fleetKey  = GUIstate.Data.RaceName + "/" + fleetName;

         Fleet fleetToMerge = AllFleets[fleetKey] as Fleet;
         foreach (Ship ship in fleetToMerge.FleetShips) {
            SelectedFleet.FleetShips.Add(ship);
         }

         AllFleets.Remove(fleetKey);
         GUIstate.Data.DeletedFleets.Add(fleetKey);
         UpdateDialogDetails();

         MergeButton.Enabled = false;
      }

   }
}
