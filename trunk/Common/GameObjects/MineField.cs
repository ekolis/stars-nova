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
using System.Xml;

namespace Nova.Common
{
    [Serializable]
    public class Minefield : Item
    {
        public int NumberOfMines;
        public int SafeSpeed = 4;
        private static int keyId;


        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Minefield()
        {
            keyId++;
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
                return (int)Math.Sqrt(NumberOfMines);
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Override the Key method of Item
        /// </summary>
        /// ----------------------------------------------------------------------------
        public override string Key
        {
            get { return keyId.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        #endregion

        #region To From Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the Minefield for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the Minefield</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelMinefield = xmldoc.CreateElement("Minefiled");

            xmlelMinefield.AppendChild(base.ToXml(xmldoc));

            Global.SaveData(xmldoc, xmlelMinefield, "NumberOfMines", NumberOfMines.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelMinefield, "SafeSpeed", SafeSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelMinefield, "keyId", keyId.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelMinefield;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="Minefield"/> node Nova save file (xml document)</param>
        /// ----------------------------------------------------------------------------
        public Minefield(XmlNode node)
            : base(node.SelectSingleNode("Item"))
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "numberofmines":
                            NumberOfMines = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "safespeed":
                            SafeSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "keyid":
                            keyId = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Minefield : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }   
        }

        #endregion

    }
}
