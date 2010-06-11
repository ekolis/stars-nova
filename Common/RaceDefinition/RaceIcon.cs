#region Copyright Notice
// ============================================================================
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
// This module defines the class RaceIcon which manages an icon as a paired
// Bitmap and a String holding the image file's path. The Bitmap is for 
// display purposes and the flie path is for loading/saving.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.IO;


namespace Nova.Common
{
    /// <summary>
    /// A race's icon image.
    /// </summary>
    [Serializable]
    public class RaceIcon : ICloneable
    {
        public string Source = string.Empty;
        public Bitmap Image;

        #region Construction

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RaceIcon()
        {
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="source">The path and file name to the icon.</param>
        /// <param name="image">The loaded image.</param>
        /// ----------------------------------------------------------------------------
        public RaceIcon(string source, Bitmap image)
        {
            Source = source;
            Image = image;
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Increment the current icon image.
        /// </summary>
        /// <param name="icon">The currently selected icon.</param>
        /// <returns>The next race icon in the AllRaceIcons collection.</returns>
        /// ----------------------------------------------------------------------------
        static public RaceIcon operator ++(RaceIcon icon)
        {
            if (AllRaceIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator++ - Race Icons failed to load.");
                return icon;
            }
            int next = AllRaceIcons.Data.IconList.IndexOf(icon) + 1;
            if (next > AllRaceIcons.Data.IconList.Count - 1) next = 0;
            return (RaceIcon)AllRaceIcons.Data.IconList[next];
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Decrement the current icon image.
        /// </summary>
        /// <param name="icon">The currently selected icon.</param>
        /// <returns>The previous icon in the AllRaceIcons collection.</returns>
        /// ----------------------------------------------------------------------------
        static public RaceIcon operator --(RaceIcon icon)
        {
            if (AllRaceIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator-- - Race Icons failed to load.");
                return icon;
            }
            int prev = AllRaceIcons.Data.IconList.IndexOf(icon) - 1;
            if (prev < 0) prev = AllRaceIcons.Data.IconList.Count - 1;
            return (RaceIcon)AllRaceIcons.Data.IconList[prev];
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a clone of this object.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public object Clone()
        {
            RaceIcon clone = new RaceIcon(Source, Image);
            return clone as object;
        }

        #endregion
    }

}
