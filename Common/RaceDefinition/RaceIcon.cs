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
using System.Drawing;
using System.Xml;

namespace Nova.Common
{
    /// <summary>
    /// A race's icon image.
    /// </summary>
    [Serializable]
    public class RaceIcon : ICloneable
    {
        public string Source = string.Empty;
        private Bitmap image; 
        public Bitmap Image
        {
            get
            {
                if (image == null)
                {
                    // atempt to retrieve image
                    try
                    {
                        image = new Bitmap(Source);
                    }
                    catch
                    {
                    }
                }
                return image;
            }
            set
            {
                image = value;
            }
        }

        #region Construction

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RaceIcon()
        {
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="source">The path and file name to the icon.</param>
        /// <param name="image">The loaded image.</param>
        public RaceIcon(string source, Bitmap image)
        {
            Source = source;
            Image = image;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Increment the current icon image.
        /// </summary>
        /// <param name="icon">The currently selected icon.</param>
        /// <returns>The next race icon in the AllRaceIcons collection.</returns>
        static public RaceIcon operator ++(RaceIcon icon)
        {
            if (AllRaceIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator++ - Race Icons failed to load.");
                return icon;
            }
            int next = AllRaceIcons.Data.IconList.IndexOf(icon) + 1;
            if (next > AllRaceIcons.Data.IconList.Count - 1)
            {
                next = 0;
            }
            return AllRaceIcons.Data.IconList[next];
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
            if (prev < 0)
            {
                prev = AllRaceIcons.Data.IconList.Count - 1;
            }
            return AllRaceIcons.Data.IconList[prev];
        }

        #endregion

        #region Interface ICloneable

        /// <summary>
        /// Return a clone of this object.
        /// </summary>
        public object Clone()
        {
            RaceIcon clone = new RaceIcon(Source, Image);
            return clone as object;
        }

        #endregion

        #region Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="xmlnode">An <see cref="XmlNode"/> within 
        /// a Nova game file (xml document).
        /// </param>
        public RaceIcon(XmlNode xmlnode)
        {
            XmlNode subnode = xmlnode.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "raceicon")
                    {
                        Source = subnode.FirstChild.Value;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialise this object to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the ScoreRecord</returns>
        /// <remarks>FIXME (priority 6) - Currently the icon is saved as the path to the icon. This is broken if the server is saving .intel and the client then loads it with the icons in a different location.</remarks>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelRaceIcon = xmldoc.CreateElement("RaceIcon");

            // Source;
            Global.SaveData(xmldoc, xmlelRaceIcon, "RaceIcon", Source);

            return xmlelRaceIcon;
        }

        #endregion
    }
}
