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
// Class to support the format of each array entry (note that element 0
// of the Cost array is used to hold previous counter values).
// ===========================================================================
#endregion

using System;

namespace Nova.WinForms.RaceDesigner
{

    /// <Summary>
    /// Class to support the format of each array entry (note that element 0
    /// of the Cost array is used to hold previous counter values).
    /// </Summary>
    public class ParameterEntry
    {
        public string ParameterName;
        public int[] Cost;

        public ParameterEntry(string p, int[] c)
        {
            ParameterName = p;
            Cost = c;
        }
    }
}

