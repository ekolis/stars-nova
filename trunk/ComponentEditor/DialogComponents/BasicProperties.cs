// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module contains the basic properties of the component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ComponentEditor
{
   public partial class BasicProperties : UserControl
   {
      public BasicProperties()
      {
         InitializeComponent();
      }


// ============================================================================
// Get the resource build cost from the control
// ============================================================================

      public NovaCommon.Resources Cost {
         get { 
            NovaCommon.Resources cost = new NovaCommon.Resources();

            cost.Ironium   = (int) IroniumAmount.Value;
            cost.Boranium  = (int) BoraniumAmount.Value;
            cost.Germanium = (int) GermaniumAmount.Value;
            cost.Energy    = (int) EnergyAmount.Value;

            return cost;
         }


// ============================================================================
// Set the resource build cost in the control
// ============================================================================

         set { 
            IroniumAmount.Value   = (int) value.Ironium;
            BoraniumAmount.Value  = (int) value.Boranium;
            GermaniumAmount.Value = (int) value.Germanium;
            EnergyAmount.Value    = (int) value.Energy;
         }
      }


// ============================================================================
// Get and set the mass of the component
// ============================================================================

      public int Mass {
         get { return (int) ComponentMass.Value; }
         set { ComponentMass.Value = value; }
      }

   }
}
