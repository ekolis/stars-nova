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
// A type used by an EnabledCounter control.
// ===========================================================================
#endregion

using System;
using System.ComponentModel;

using Nova.Common.Converters;

namespace Nova.Common
{

    /// <summary>
    /// A type used by an EnabledCounter control.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(EnabledValueConverter))]
    public class EnabledValue
    {
        public bool IsChecked = false;
        public int NumericValue = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="check"></param>
        /// <param name="value"></param>
        /// ----------------------------------------------------------------------------
        public EnabledValue(bool check, int value)
        {
            IsChecked = check;
            NumericValue = value;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnabledValue() 
        { 
        }

        #endregion

    }

}
