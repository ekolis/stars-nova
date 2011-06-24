#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    [Serializable]
    public class TemperatureTolerance : EnvironmentTolerance
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization?.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public TemperatureTolerance() 
        { 
        }

        #endregion

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range. 
        // Temperature is in the range -200 to 200.
        override protected int MakeInternalValue(double value)
        {
            return (int)((200 + value) / 4);
        }

        override protected string Format(int value)
        {
            return Temperature.FormatWithUnit(value);
        }
    }
}
