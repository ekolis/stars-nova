// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Dialog to rename a fleet.
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
   public partial class RenameFleet : Form
   {

// ============================================================================
// Construction
// ============================================================================

      public RenameFleet()
      {
         InitializeComponent();
      }


// ============================================================================
// Cancel button pressed.
// ============================================================================

      private void CancelRename_Click(object sender, EventArgs e)
      {
         Close();
      }


// ============================================================================
// Rename button pressed.
// ============================================================================

      private void OKButton_Click(object sender, EventArgs e)
      {
         Hashtable AllFleets = GUIstate.Data.InputTurn.AllFleets;

         string newName = NewName.Text;
         string newKey  = GUIstate.Data.RaceName + "/" + newName;
         string oldKey  = GUIstate.Data.RaceName + "/" + ExistingName.Text;
         
         if (AllFleets.Contains(newKey)) {
            Report.Error("A fleet already has that name");
            return;
         }

         Fleet fleet = AllFleets[oldKey] as Fleet;
         AllFleets.Remove(oldKey);
         GUIstate.Data.DeletedFleets.Add(oldKey);

         fleet.Name        = newName;
         AllFleets[newKey] = fleet;
         
         // Ensure the main display gets updated to reflect the new name

         MainWindow.nova.SelectionSummary.Value = fleet as Item;
         MainWindow.nova.SelectionDetail.Value  = fleet as Item;
         
         Close();
      }
   }
}
