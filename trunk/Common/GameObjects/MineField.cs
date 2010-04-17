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
// Definition of a Minefield. Note that it over-rides the Key method to provide
// a simple incrementing number each time a new minefield is created. This
// ensures that each minefield can be used in hash tables without having to
// specify a "name" for the minefield.
// ===========================================================================
#endregion


using System;
using System.Text;
using System.Drawing;

namespace NovaCommon
{
    [Serializable]
    public class Minefield : Item
    {
        public int NumberOfMines = 0;
        public int SafeSpeed = 4;
        private static int KeyID = 0;


        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Minefield()
        {
            KeyID++;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine the spacial radius of a Minefield. 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Radius
        {
            get
            {
                return (int) Math.Sqrt(NumberOfMines);
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Override the Key method of Item
        /// </summary>
        /// ----------------------------------------------------------------------------
        public override string Key
        {
            get { return KeyID.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        #endregion

    }
}
