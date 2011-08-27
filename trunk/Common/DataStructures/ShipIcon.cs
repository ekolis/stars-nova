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
    using System.Drawing;
    using System.Xml;

    /// <summary>
    /// This object defines the class ShipIcon which manages an icon as a paired
    /// Bitmap and a String holding the image file's path. The Bitmap is for 
    /// display purposes and the flie path is for loading/saving.
    /// </summary>
    [Serializable]
    public class ShipIcon : ICloneable
    {
        private readonly int index;
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

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShipIcon()
        {
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="source">The path and file name to the icon.</param>
        /// <param name="image">The loaded image.</param>
        public ShipIcon(string source, Bitmap image)
        {
            Source = source;
            Image = image;

            // fi.Name format is <baseHull><iconNumber>.png where the length of <Number> in characters is defined by Global.ShipIconNumberingLength.
            int extensionSeperatorIndex = Source.LastIndexOf('.'); // position of the '.' in the file name

            // get the hull number of this icon
            index = int.Parse(Source.Substring(extensionSeperatorIndex - Global.ShipIconNumberingLength, Global.ShipIconNumberingLength));

        }

        /// <summary>
        /// Increment the current icon image.
        /// </summary>
        /// <param name="icon">The currently selected icon.</param>
        /// <returns>The next race icon in the AllRaceIcons collection.</returns>
        static public ShipIcon operator ++(ShipIcon icon)
        {
            if (AllShipIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator++ - Race Icons failed to load.");
                return icon;
            }

            // icon.Source format is <baseHull><iconNumber>.png where the length of <Number> in characters is defined by Global.ShipIconNumberingLength.
            // need to split this up to get the baseHull 
            // (the number is stored as icon.Index, which is the key in the dictonary of ship icons associated with the baseHull)
            // then find the number of available icons, locate the next one and look up that icon.
            string baseHull = icon.Source.Substring(0, icon.Source.IndexOf('.') - Global.ShipIconNumberingLength);
            int iconCount = AllShipIcons.Data.Hulls[baseHull].Count;
            int nextIconIndex = icon.index + 1;
            if (nextIconIndex > (iconCount - 1))
            {
                nextIconIndex = 0;
            }
            return (ShipIcon)AllShipIcons.Data.Hulls[baseHull][nextIconIndex];
        }

        /// <summary>
        /// Decrement the current icon image.
        /// </summary>
        /// <param name="icon">The currently selected icon.</param>
        /// <returns>The previous icon in the AllRaceIcons collection.</returns>
        static public ShipIcon operator --(ShipIcon icon)
        {
            if (AllShipIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator-- - Race Icons failed to load.");
                return icon;
            }
            string baseHull = icon.Source.Substring(0, icon.Source.IndexOf('.') - Global.ShipIconNumberingLength);
            int iconCount = AllShipIcons.Data.Hulls[baseHull].Count;
            int prevIconIndex = icon.index - 1;
            if (prevIconIndex < 0)
            {
                prevIconIndex = iconCount - 1;
            }
            return (ShipIcon)AllShipIcons.Data.Hulls[baseHull][prevIconIndex];
        }

        /// <summary>
        /// Return a clone of this object.
        /// </summary>
        public object Clone()
        {
            ShipIcon clone = new ShipIcon(Source, Image);
            return clone as object;
        }

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="xmlnode">An <see cref="XmlNode"/> within 
        /// a Nova game file (xml document).
        /// </param>
        public ShipIcon(XmlNode xmlnode)
        {
            XmlNode subnode = xmlnode.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "shipicon")
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
        /// <returns>An <see cref="XmlElement"/> representation of the ScoreRecord.</returns>
        /// <remarks>FIXME (priority 6) - Currently the icon is saved as the path to the icon. This is broken if the server is saving .intel and the client then loads it with the icons in a different location.</remarks>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelRaceIcon = xmldoc.CreateElement("ShipIcon");

            // Source;
            Global.SaveData(xmldoc, xmlelRaceIcon, "ShipIcon", Source);

            return xmlelRaceIcon;
        }
    }
}
