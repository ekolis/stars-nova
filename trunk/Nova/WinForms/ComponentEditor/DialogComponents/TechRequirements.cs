#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// User control for accessing research tech levels.
// ===========================================================================
#endregion

#region Using Statements
using Nova.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Nova.WinForms.ComponentEditor
{
    public partial class TechRequirements : UserControl
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction
        /// </summary>
        /// ----------------------------------------------------------------------------
        public TechRequirements()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property for getting/setting the Tech levels in the control.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public TechLevel Value
        {
            // Get the tech levels from the control
            get
            {
                TechLevel techLevel = new TechLevel();

                techLevel[TechLevel.ResearchField.Biotechnology] = (int)BioTechLevel.Value;
                techLevel[TechLevel.ResearchField.Construction] = (int)ConstructionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Electronics] = (int)ElectronicsTechLevel.Value;
                techLevel[TechLevel.ResearchField.Energy] = (int)EnergyTechLevel.Value;
                techLevel[TechLevel.ResearchField.Propulsion] = (int)PropulsionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Weapons] = (int)WeaponsTechLevel.Value;

                return techLevel;
            }


            // Set the tech levels in the control     
            set
            {
                BioTechLevel.Value = (int)value[TechLevel.ResearchField.Biotechnology];
                ElectronicsTechLevel.Value = (int)value[TechLevel.ResearchField.Electronics];
                EnergyTechLevel.Value = (int)value[TechLevel.ResearchField.Energy];
                PropulsionTechLevel.Value = (int)value[TechLevel.ResearchField.Propulsion];
                WeaponsTechLevel.Value = (int)value[TechLevel.ResearchField.Weapons];
                ConstructionTechLevel.Value = (int)value[TechLevel.ResearchField.Construction];
            }

        }
    }
}
