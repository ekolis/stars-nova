// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This dialog allows cargo pod components to be created and edited.
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
   public partial class CargoPodDialog : Form
   {
      private Hashtable AllComponents = null;


// ============================================================================
// Construction
// ============================================================================
      
      public CargoPodDialog()
        {
           InitializeComponent();
           commonProperties1.ListBoxChanged += OnSelectionChanged;
           AllComponents = NovaCommon.AllComponents.Data.Components;

           commonProperties1.UpdateListBox(typeof(NovaCommon.Component));

        }


// ============================================================================
// When a selection in the list box changes repopulate the dialog with the
// values for that armour. The processing of this event is delegated from the
// Common Properties control.
// ============================================================================

       private void OnSelectionChanged(object sender, EventArgs e)
       {
          ListBox listBox      = sender as ListBox;
          string  CargoPodName = listBox.SelectedItem as string;
          NovaCommon.Component cargoPodComponent = AllComponents[CargoPodName] as NovaCommon.Component;
          commonProperties1.Value = cargoPodComponent;

          try
          {
              CargoPod cargoPodProperties = cargoPodComponent.Properties["CargoPod"] as CargoPod;
              Capacity.Value = cargoPodProperties.Capacity;
          }
          catch
          {
              MessageBox.Show("Selected component does not have Cargo Pod properties.");
          }
       }


// ============================================================================
// Component delete button pressed.
// ============================================================================

       private void DeleteButton_Click(object sender, EventArgs e)
       {
         commonProperties1.DeleteComponent();
       }


// ============================================================================
// Done button pressed
// ============================================================================

        private void DoneButton_Click(object sender, EventArgs e)
        {
           Close();
        }

// ============================================================================
// Save button pressed. Build a Cago Pod  object and add it to the list of all
// available components (this will also result in any existing design with the
// same name being overwritten).
// ============================================================================

      private void SaveButton_Click(object sender, EventArgs e)
      {
         string componentName = commonProperties1.ComponentName.Text;

         if (componentName == null || componentName == "") {
            Report.Error("You must specify a component name");
            return;
         }

         NovaCommon.Component cargoPodComponent = new NovaCommon.Component(commonProperties1.Value);
         CargoPod cargoPodProperties = new CargoPod();

         cargoPodProperties.Capacity = (int)Capacity.Value;
         cargoPodComponent.Properties.Add("CargoPod", cargoPodProperties);

         AllComponents[cargoPodComponent.Name] = cargoPodComponent;

         commonProperties1.UpdateListBox(typeof(NovaCommon.Component));
         Report.Information("CargoPod design has been saved");
      }


    }
}
