// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// User control for accessing research tech levels.
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
   public partial class TechRequirements : UserControl
   {

// ============================================================================
// Construction
// ============================================================================

      public TechRequirements()
      {
         InitializeComponent();
      }


// ============================================================================
// Get the tech levels from the control
// ============================================================================

      public TechLevel Value {
         get { 
            TechLevel techLevel = new TechLevel();
            Hashtable values    = techLevel.TechValues;

            values["Biotechnology"] = (int) BioTechLevel.Value;
            values["Electronics"]   = (int) ElectronicsTechLevel.Value;
            values["Energy"]        = (int) EnergyTechLevel.Value;
            values["Propulsion"]    = (int) PropulsionTechLevel.Value;
            values["Weapons"]       = (int) WeaponsTechLevel.Value;
            values["Construction"]  = (int) ConstructionTechLevel.Value;

            return techLevel;
         }


// ============================================================================
// Set the tech levels in the control
// ============================================================================

         set { 
            Hashtable levels = value.TechValues;
            if (levels == null) return;

            BioTechLevel.Value          = (int) levels["Biotechnology"];
            ElectronicsTechLevel.Value  = (int) levels["Electronics"];
            EnergyTechLevel.Value       = (int) levels["Energy"];
            PropulsionTechLevel.Value   = (int) levels["Propulsion"];
            WeaponsTechLevel.Value      = (int) levels["Weapons"];
            ConstructionTechLevel.Value = (int) levels["Construction"];
         }

      }


   }
}
