// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// User control for accessing research tech levels.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

#region Using Statements
using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace ComponentEditor
{
    public partial class TechRequirements : UserControl
    {

        /// <summary>
        /// Construction
        /// </summary>
        public TechRequirements()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Property for getting/setting the Tech levels in the control.
        /// </summary>
        public TechLevel Value
        {
            // Get the tech levels from the control
            get
            {
                TechLevel techLevel = new TechLevel();

                techLevel[TechLevel.ResearchField.Biotechnology] = (int)BioTechLevel.Value;
                techLevel[TechLevel.ResearchField.Construction]  = (int)ConstructionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Electronics]   = (int)ElectronicsTechLevel.Value;
                techLevel[TechLevel.ResearchField.Energy]        = (int)EnergyTechLevel.Value;
                techLevel[TechLevel.ResearchField.Propulsion]    = (int)PropulsionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Weapons]       = (int)WeaponsTechLevel.Value;

                return techLevel;
            }


            // Set the tech levels in the control     
            set
            {
                BioTechLevel.Value          = (int)value[TechLevel.ResearchField.Biotechnology];
                ElectronicsTechLevel.Value  = (int)value[TechLevel.ResearchField.Electronics];
                EnergyTechLevel.Value       = (int)value[TechLevel.ResearchField.Energy];
                PropulsionTechLevel.Value   = (int)value[TechLevel.ResearchField.Propulsion];
                WeaponsTechLevel.Value      = (int)value[TechLevel.ResearchField.Weapons];
                ConstructionTechLevel.Value = (int)value[TechLevel.ResearchField.Construction];
            }

        }
    }//TechRequirements
}//namespace