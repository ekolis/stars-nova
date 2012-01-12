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

namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Temperature
    {
        public static string FormatWithUnit(int value)
        {
            return Format(value) + GetUnit();
        }

        public static string Format(int value)
        {
            // always trunkate temperature for GUI values to 0 decimal points
            return BarPositionToEnvironmentValue(value).ToString("F0");
        }

        public static string GetUnit()
        {
            return "°C";
        }
        
        public static double BarPositionToEnvironmentValue(int pos)
        {
            return (pos * 4) - 200;
        }       
    }
}
