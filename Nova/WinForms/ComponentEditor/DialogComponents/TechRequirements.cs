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

namespace Nova.WinForms.ComponentEditor
{
    using System.Windows.Forms;
    using Nova.Common;

    /// <summary>
    /// User control for accessing research tech levels.
    /// </summary>
    public partial class TechRequirements : UserControl
    {
        /// <Summary>
        /// Initializes a new instance of the TechRequirements class.
        /// </Summary>
        public TechRequirements()
        {
            InitializeComponent();
        }

        /// <Summary>
        /// Property for getting/setting the Tech levels in the control.
        /// </Summary>
        public TechLevel Value
        {
            // Get the tech levels from the control
            get
            {
                TechLevel techLevel = new TechLevel();

                techLevel[TechLevel.ResearchField.Biotechnology] = (int)this.bioTechLevel.Value;
                techLevel[TechLevel.ResearchField.Construction] = (int)this.constructionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Electronics] = (int)this.electronicsTechLevel.Value;
                techLevel[TechLevel.ResearchField.Energy] = (int)this.energyTechLevel.Value;
                techLevel[TechLevel.ResearchField.Propulsion] = (int)this.propulsionTechLevel.Value;
                techLevel[TechLevel.ResearchField.Weapons] = (int)this.weaponsTechLevel.Value;

                return techLevel;
            }

            // Set the tech levels in the control  
            set
            {
                this.bioTechLevel.Value = (int)value[TechLevel.ResearchField.Biotechnology];
                this.electronicsTechLevel.Value = (int)value[TechLevel.ResearchField.Electronics];
                this.energyTechLevel.Value = (int)value[TechLevel.ResearchField.Energy];
                this.propulsionTechLevel.Value = (int)value[TechLevel.ResearchField.Propulsion];
                this.weaponsTechLevel.Value = (int)value[TechLevel.ResearchField.Weapons];
                this.constructionTechLevel.Value = (int)value[TechLevel.ResearchField.Construction];
            }
        }
    }
}
