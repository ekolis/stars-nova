// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This component defines common properties.
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ComponentEditor
{

// ============================================================================
// Dialog component dealing with properties common to all components.
// ============================================================================

   public partial class CommonProperties : UserControl
   {
      public event EventHandler ListBoxChanged;

      private Hashtable AllComponents = null;

// ============================================================================
// Construction
// ============================================================================

      public CommonProperties()
      {
         InitializeComponent();
         AllComponents = NovaCommon.AllComponents.Data.Components;
      }


// ============================================================================
// Populate the list box wih all components of a specified type.
// ============================================================================

      public void UpdateListBox(Type objectType)
      {
         ComponentList.Items.Clear();

         foreach (NovaCommon.Component thing in AllComponents.Values) {
            if (thing.GetType() == objectType) {
               ComponentList.Items.Add(thing.Name);
            }
         }
      }


// ============================================================================
// Populate the list box wih all components of a specified type name (used when
// the actua type isn't known.
// ============================================================================

      public void UpdateListBox(string objectName)
      {
         ComponentList.Items.Clear();

         foreach (NovaCommon.Component thing in AllComponents.Values) {
            if (thing.Type == objectName) {
               ComponentList.Items.Add(thing.Name);
            }
         }
      }


// ============================================================================
// List box selection changed. Delegate the processing of this event to the
// appropriate dialog.
// ============================================================================

      private void ComponentList_SelectedIndexChanged(object sender, 
                                                      EventArgs e)
      {
         if (ComponentList.SelectedItem == null) {
            return;
         }

         if (ListBoxChanged != null) {
            ListBoxChanged(sender, e);
         }
      }


// ============================================================================
// Delete the currently selected component.
// ============================================================================

      public void DeleteComponent()
      {
         string componentName = ComponentName.Text;

         if (componentName == null || componentName == "") {
            Report.Error("You must select a component to delete");
            return;
         }

         AllComponents.Remove(componentName);
         ComponentList.Items.Remove(componentName);

         if (ComponentList.Items.Count > 0) {
            ComponentList.SelectedIndex = 0;
         }
      }


// ============================================================================
// Get and set the properties common to all components.
// ============================================================================

      public NovaCommon.Component Value 
      {
         get {
            NovaCommon.Component component = new NovaCommon.Component();

            component.ComponentImage = ComponentImage.Image;
            component.Cost           = BasicProperties.Cost;
            component.Description    = Description.Text;
            component.Mass           = BasicProperties.Mass;
            component.Name           = ComponentName.Text;
            component.RequiredTech   = TechRequirements.Value;

            return component;
         }

         set {
            BasicProperties.Cost   = value.Cost;
            BasicProperties.Mass   = value.Mass;
            ComponentImage.Image   = value.ComponentImage;
            ComponentName.Text     = value.Name;
            Description.Text       = value.Description;
            TechRequirements.Value = value.RequiredTech;
         }
      }

   }
}
