// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// A simple control that combines a checkbox with an up-down counter.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ControlLibrary
{

   public partial class EnabledCounter : UserControl
   {

// ============================================================================
// Construction
// ============================================================================

      public EnabledCounter()
      {
         InitializeComponent();
      }


// ============================================================================
// Get and set individual control values. These are meant to used at design
// time:
//
// Counter value (ControlCounter)
// Control text  (ControlText)
// Enabled mode  (ControlSelected)
// Minimum Value (Minimum)
// Maximum Value (Maximum)
// ============================================================================

      public int ControlCounter
      {
         get { return (int) UpDownCounter.Value; }
         set { UpDownCounter.Value = value; }
      }

      public string ControlText
      {
         get {return CheckBox.Text;  }
         set {CheckBox.Text = value; }
      }

      public bool ControlSelected
      {
         get {return CheckBox.Checked;  }
         set {CheckBox.Checked = value; }
      }

      public int Minimum
      {
         get {return (int) UpDownCounter.Minimum;  }
         set {UpDownCounter.Minimum = value; }
      }

      public int Maximum
      {
         get {return (int) UpDownCounter.Maximum;  }
         set {UpDownCounter.Maximum = value; }
      }


// ============================================================================
// Get and set control value as an EnabledValue. This interface is meant to be
// used at run-time.
// ============================================================================

      public EnabledValue Value
      {
         get {
            EnabledValue result = new EnabledValue();
            result.IsChecked    = CheckBox.Checked;
            result.NumericValue = (int) UpDownCounter.Value;
            return result;
         }

         set {
            UpDownCounter.Value = value.NumericValue;
            CheckBox.Checked    = value.IsChecked;
         }
      }
   }


// ============================================================================
// Type used by control.
// ============================================================================
      
   [Serializable]
   public class EnabledValue
   {
      public bool IsChecked    = false;
      public int  NumericValue = 0;

// ============================================================================
// Construction (iniialising and non-initialising)
// ============================================================================

      public EnabledValue(bool check, int value)
      {
         IsChecked    = check;
         NumericValue = value;
      }

      public EnabledValue() { }
      
   }

}
