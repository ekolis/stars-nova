// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This dialog allows shield components to be created and edited.
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

namespace ComponentEditor
{
    public partial class ShieldDialog : Form
    {
      private Hashtable AllComponents = null;


// ============================================================================
// Construction
// ============================================================================

       public ShieldDialog()
       {
          InitializeComponent();
          CommonProperties.ListBoxChanged += OnSelectionChanged;
          AllComponents = NovaCommon.AllComponents.Data.Components;

          CommonProperties.UpdateListBox(typeof(Shield));
       }


// ============================================================================
// Done button pressed
// ============================================================================

       private void DoneButton_Click(object sender, EventArgs e)
       {
          Close();
       }


// ============================================================================
// Save button pressed. Build an shield object and add it to the list of all
// available components (this will also result in any existing design with the
// same name being overwritten).
// ============================================================================

      private void SaveButton_Click(object sender, EventArgs e)
      {
         string componentName = CommonProperties.ComponentName.Text;

         if (componentName == null || componentName == "") {
            Report.Error("You must specify a component name");
            return;
         }

         Shield shield              = new Shield (CommonProperties.Value);
         shield.Strength            = (int) ShieldStrength.Value;
         shield.Armour              = (int) ArmourStrength.Value;
         AllComponents[shield.Name] = shield;

         CommonProperties.UpdateListBox(typeof(Shield));
         Report.Information("Shield design has been saved");
      }


// ============================================================================
// When a selection in the list box changes repopulate the dialog with the
// values for that shield. The processing of this event is delegated from the
// Common Properties control.
// ============================================================================

       private void OnSelectionChanged(object sender, EventArgs e)
       {
          ListBox listBox        = sender as ListBox;
          string  shieldName     = listBox.SelectedItem as string;
          Shield  shield         = AllComponents[shieldName] as Shield;

          CommonProperties.Value = shield;
          ShieldStrength.Value   = shield.Strength;
          ArmourStrength.Value   = shield.Armour;
       }


// ============================================================================
// Component delete button pressed.
// ============================================================================

       private void DeleteButton_Click(object sender, EventArgs e)
       {
         CommonProperties.DeleteComponent();
       }

    }
}
