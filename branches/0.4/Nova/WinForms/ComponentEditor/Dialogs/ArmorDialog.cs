// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This dialog allows armour components to be created and edited.
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
    public partial class ArmorDialog : Form
    {
       private Hashtable AllComponents = null;


// ============================================================================
// Construction
// ============================================================================

       public ArmorDialog()
       {
          InitializeComponent();
          CommonProperties.ListBoxChanged += OnSelectionChanged;
          AllComponents = NovaCommon.AllComponents.Data.Components;

          CommonProperties.UpdateListBox(typeof(NovaCommon.Component));
       }


// ============================================================================
// Done button pressed
// ============================================================================

       private void DoneButton_Click(object sender, EventArgs e)
       {
          Close();
       }


// ============================================================================
// Save button pressed. Build an armour object and add it to the list of all
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

         // Create a new component and copy the form details to it.   
         NovaCommon.Component armorComponent = new NovaCommon.Component(CommonProperties.Value);

         Armor armorProperties   = new Armor();
         armorProperties.Strength = (int) ArmourStrength.Value; // TODO change the form property to American spelling
         armorProperties.Shields = (int) Shielding.Value;

         armorComponent.Properties.Add("Armor", armorProperties);

         AllComponents[armorComponent.Name] = armorComponent;

         CommonProperties.UpdateListBox(typeof(NovaCommon.Component));
         Report.Information("Armour design has been saved");
      }


// ============================================================================
// When a selection in the list box changes repopulate the dialog with the
// values for that armour. The processing of this event is delegated from the
// Common Properties control.
// ============================================================================

       private void OnSelectionChanged(object sender, EventArgs e)
       {
          ListBox listBox        = sender as ListBox;
          string  armorName     = listBox.SelectedItem as string;
          NovaCommon.Component armorComponent = AllComponents[armorName] as NovaCommon.Component;

          CommonProperties.Value = armorComponent;
          try
          {
              Armor armorProperties = armorComponent.Properties["Armor"] as Armor;
              ArmourStrength.Value = armorProperties.Strength;
              Shielding.Value = armorProperties.Shields;
          }
          catch
          {
              MessageBox.Show("The selected component has no Armor properties.");
          }
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
