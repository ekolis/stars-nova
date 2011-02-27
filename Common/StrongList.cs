#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 stars-nova
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
// A template type for a strongly typed ArrayList
// ===========================================================================
#endregion


namespace Nova.Common
{
    using System;
    using System.Collections;

    [Serializable]
    public class StrongList<TVal> : CollectionBase
    {
        public void Add(TVal newString)
        {
            List.Add(newString);
        }

        public void Remove(TVal oldString)
        {
            List.Remove(oldString);
        }

        public StrongList()
        {
        }

        public TVal this[int stringIndex]
        {
            get
            {
                return (TVal)List[stringIndex];
            }
            set
            {
                List[stringIndex] = value;
            }
        }
    }
}
