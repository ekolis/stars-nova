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

using NovaCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.ComponentEditor.Dialogs
{
    public partial class BasicProperties : UserControl
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
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
        public NovaCommon.Resources Cost
        {
            get
            {
                NovaCommon.Resources cost = new NovaCommon.Resources();

                cost.Ironium = (int)IroniumAmount.Value;
                cost.Boranium = (int)BoraniumAmount.Value;
                cost.Germanium = (int)GermaniumAmount.Value;
                cost.Energy = (int)EnergyAmount.Value;

                return cost;
            }


            // Set the resource build cost in the control
            set
            {
                IroniumAmount.Value = (int)value.Ironium;
                BoraniumAmount.Value = (int)value.Boranium;
                GermaniumAmount.Value = (int)value.Germanium;
                EnergyAmount.Value = (int)value.Energy;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set the mass of the component
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Mass
        {
            get { return (int)ComponentMass.Value; }
            set { ComponentMass.Value = value; }
        }

        #endregion

    }
}
