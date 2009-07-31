// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Some error reporting utilities.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NovaCommon
{
   public static class Report
   {

// ============================================================================
// Report an error
// ============================================================================
      public static void Error(string text)
      {
          MessageBox.Show("Nova has encountered an error, but will continue anyway."+Environment.NewLine+
              "Details: "+text, "Nova - Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 
              MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
          
      }


// ============================================================================
// Report informations
// ============================================================================

      public static void Information(string text)
      {
         MessageBox.Show(text, "Nova - Information", MessageBoxButtons.OK,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
      }


// ============================================================================
// Report a fatal error
// ============================================================================

      public static void FatalError(string text)
      {
         MessageBox.Show(
           text + "\r\n\r\n(This error will terminate the program)",
           "Nova - Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

         System.Threading.Thread.CurrentThread.Abort();
      }

// ============================================================================
// Report Debug Messages
// ============================================================================

      public static void Debug(string text)
      {
#if (DEBUG)
          MessageBox.Show(text, "Nova - Debug", MessageBoxButtons.OK,
                          MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
#endif
      }

   }
}
