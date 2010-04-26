// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module holds the program entry point and handles all things related to
// the main GUI window.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

namespace Nova.Gui.Dialogs
{
   public partial class PlayerRelations : Form
   {
      private Hashtable Relation = ClientState.Data.PlayerRelations;


// ============================================================================
// Construction
// ============================================================================

      public PlayerRelations()
      {
         InitializeComponent();

         foreach (string raceName in ClientState.Data.InputTurn.AllRaceNames) {
            if (raceName != ClientState.Data.RaceName) {
               RaceList.Items.Add(raceName);
            }
         }

         if (RaceList.Items.Count > 0) {
            RaceList.SelectedIndex = 0;
         }
      }


// ============================================================================
// Exit dialog button pressed
// ============================================================================

      private void DoneBUtton_Click(object sender, EventArgs e)
      {
         Close();
      }


// ============================================================================
// Selected race has changed, update the relation details
// ============================================================================

      private void SelectedRaceChanged(object sender, EventArgs e)
      {
         string selectedRace = RaceList.SelectedItem as string;

         if (Relation[selectedRace] as string == "Enemy") {
            EnemyButton.Checked = true;
         }
         else if (Relation[selectedRace] as string == "Neutral") {
            NeutralButton.Checked = true;
         }
         else {
            FriendButton.Checked = true;
         }
      }


// ============================================================================
// Player relationship changed
// ============================================================================

      private void RelationChanged(object sender, EventArgs e)
      {
         string      selectedRace = RaceList.SelectedItem as string;
         RadioButton button       = sender as RadioButton;
         Relation[selectedRace]   = button.Text;
      }
   }
}
