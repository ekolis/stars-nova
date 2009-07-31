// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// 
// Dialog for designing a hull.
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
   public partial class HullDialog : Form
   {
      private Hashtable AllComponents = null;


// ============================================================================
// Construction.
// ============================================================================

      public HullDialog()
      {
         InitializeComponent();
         AllComponents = NovaCommon.AllComponents.Data.Components;
      }


// ============================================================================
// Exit button selected, close the dialog.
// ============================================================================

      private void Close_Click(object sender, EventArgs e)
      {
          Close();
      }


// ============================================================================
// Save button selected, build a hull object and add it to the list of all
// available components (this will also result in an existing hull design being
// overwritten).
// ============================================================================
       /*
      private void SaveButton_Click(object sender, EventArgs e)
      {
         NovaCommon.Component hullComponent = new NovaCommon.Component();
         hullComponent.Name = HullName.Text;

         if (hullComponent.Name == null || hullComponent.Name == "")
         {
            Report.Error("You must specify a hull name");
            return;
         }

         hullComponent.ComponentImage = HullImage.Image;

         Hull hullProperties = new Hull();
         hullProperties.Modules = new ArrayList();
         foreach (HullModule module in HullGrid.ActiveModules)
         {
             HullModule newModule = new HullModule(module);
             hullProperties.Modules.Add(newModule);
         }

         hullComponent.Properties.Add("Hull", hullProperties);

         AllComponents[hullComponent.Name] = hullComponent;
         UpdateListBox();

         Report.Information("The hull design has been saved");
      }
       */


   }
}
