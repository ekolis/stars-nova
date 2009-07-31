// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NovaCommon;
using System.Web.Security;


namespace ControlLibrary
{
   public partial class CheckPassword : Form
   {

      Race RaceData = null;

// ============================================================================
// Construction
// ============================================================================

      public CheckPassword(Race raceData)
      {
         InitializeComponent();
         RaceData = raceData;
         AcceptButton = OKButton;
      }


// ============================================================================
// Cancel Button Pressed.
// ============================================================================

      private void CancelButton_Click(object sender, EventArgs e)
      {
         Dispose();
      }


// ============================================================================
// OK Button Pressed.
// ============================================================================

      private void OKButton_Click(object sender, EventArgs e)
      {
         string enteredPassword = PassWord.Text;
         string oldPasswordHash = RaceData.Password;

         string newPasswordHash = FormsAuthentication.
            HashPasswordForStoringInConfigFile(enteredPassword, "MD5");

         if (newPasswordHash != oldPasswordHash) {
            Report.Error("Incorrect password - Access denied");
            DialogResult = DialogResult.Cancel;
         }
         else {
            DialogResult = DialogResult.OK;
         }

         Dispose();
      }

   }
}
