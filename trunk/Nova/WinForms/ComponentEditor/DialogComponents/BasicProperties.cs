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
// This module contains the basic properties of the component.
// ===========================================================================
#endregion

using System.Windows.Forms;

namespace Nova.WinForms.ComponentEditor
{
    public partial class BasicProperties : UserControl
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the BasicProperties class.
        /// </summary>
        public BasicProperties()
        {
            InitializeComponent();
        }

        #endregion


        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the resource build cost from the control
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Nova.Common.Resources Cost
        {
            get
            {
                Nova.Common.Resources cost = new Nova.Common.Resources();

                cost.Ironium = (int)this.ironiumAmount.Value;
                cost.Boranium = (int)this.boraniumAmount.Value;
                cost.Germanium = (int)this.germaniumAmount.Value;
                cost.Energy = (int)this.energyAmount.Value;

                return cost;
            }


            // Set the resource build cost in the control
            set
            {
                this.ironiumAmount.Value = (int)value.Ironium;
                this.boraniumAmount.Value = (int)value.Boranium;
                this.germaniumAmount.Value = (int)value.Germanium;
                this.energyAmount.Value = (int)value.Energy;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set the mass of the component
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Mass
        {
            get { return (int)this.componentMass.Value; }
            set { this.componentMass.Value = value; }
        }

        #endregion

    }
}
