// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 200 Ken Reed
//
// The "About" box dialog.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Nova.Gui.Dialogs
{
   partial class AboutBox : Form
   {

// ============================================================================
// Construction
// ============================================================================

      public AboutBox()
      {
         InitializeComponent();

         this.logoPictureBox.Image = Nova.Properties.Resources.Nova;

         //  Initialize the AboutBox to display the product information from
         //  the assembly information.  Change assembly information settings
         //  for your application through either: -
         //  Project->Properties->Application->Assembly Information -
         //  AssemblyInfo.cs

         this.Text = String.Format("About {0}", AssemblyTitle);
         this.labelProductName.Text = AssemblyProduct;
         this.labelVersion.Text = String.Format("Version {0}",AssemblyVersion);
         this.labelCopyright.Text = AssemblyCopyright;
         this.BoxDescription.Text = 
           "Nova \r\n"                                                     +
           "\r\n"                                                          +
           "This is free software. You can redistribute it and/or modify " +
           "it under the terms of the GNU General Public License version " +
           "2 as published by the Free Software Foundation.";
      }

      #region Assembly Attribute Accessors


// ============================================================================
// Get title
// ============================================================================

      public string AssemblyTitle
      {
         get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            if (attributes.Length > 0) {
               AssemblyTitleAttribute titleAttribute = 
                  (AssemblyTitleAttribute)attributes[0];

               if (titleAttribute.Title != "") {
                  return titleAttribute.Title;
               }
            }

            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
         }
      }


// ============================================================================
// Get version
// ============================================================================

      public string AssemblyVersion
      {
         get {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
         }
      }


// ============================================================================
// Get product
// ============================================================================

      public string AssemblyProduct
      {
         get
         {
            // Get all Product attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            // If there aren't any Product attributes, return an empty string
            if (attributes.Length == 0)
               return "";
            // If there is a Product attribute, return its value
            return ((AssemblyProductAttribute)attributes[0]).Product;
         }
      }


// ============================================================================
// Get copyright
// ============================================================================

      public string AssemblyCopyright
      {
         get
         {
            // Get all Copyright attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            // If there aren't any Copyright attributes, return an empty string
            if (attributes.Length == 0)
               return "";
            // If there is a Copyright attribute, return its value
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
         }
      }


// ============================================================================
// Get company
// ============================================================================

      public string AssemblyCompany
      {
         get
         {
            // Get all Company attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            // If there aren't any Company attributes, return an empty string
            if (attributes.Length == 0)
               return "";
            // If there is a Company attribute, return its value
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
         }
      }
      #endregion
   }
}
