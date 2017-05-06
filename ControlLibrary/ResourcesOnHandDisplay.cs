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
// Control used to display extended resource information of a planet.
// ===========================================================================
#endregion

namespace Nova.ControlLibrary
{
    using System;
    using System.ComponentModel;
    using Nova.Common;

    /// <summary>
    /// Control used to display extended resource information of a planet.
    /// </summary>
    public class ResourcesOnHandDisplay : ResourceDisplay
    {
        // Used to keep track of the resources not on hand.
        private int resourceRate;
        private int percentage;
        
        public ResourcesOnHandDisplay() : base()
        {
            this.resourceRate = 0;
            this.percentage = 0;
        }
        
        public int ResourceRate
        {
            set
            {
                this.resourceRate = value;
            }
        }
        
        public int ResearchBudget
        {
            set
            {
                this.percentage = value;
            }
        }

        [Browsable(false)]
        public override Resources Value
        {
            set
            {
                try
                {
                    if (value == null)
                    {
                        return;
                    }

                    Resources resources = value;
                    
                    // Do not refactor this to "resourceRate*(1-percentage/100)" as they are ints...
                    int currentEnergy = this.resourceRate - (this.resourceRate * percentage / 100);
                    
                    this.ironium.Text = resources.Ironium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.boranium.Text = resources.Boranium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.germanium.Text = resources.Germanium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.energy.Text = currentEnergy.ToString(System.Globalization.CultureInfo.CurrentCulture) + " of "
                        + this.resourceRate.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    Report.Error("Unable to convert resource values.");
                }
            }
        }
    }
}

