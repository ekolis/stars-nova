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
// Cargo that may be carried by a ship (if it has a cargo pod).
// ===========================================================================
#endregion

using System;
using System.Xml;

// ============================================================================
// 
// ============================================================================

namespace NovaCommon
{
    /// <summary>
    /// Cargo class
    /// </summary>
    [Serializable]
    public class Cargo
    {
        public int Ironium = 0;
        public int Boranium = 0;
        public int Germanium = 0;
        public int Colonists = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Defualt constructor (needed if there is a copy constructor).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Cargo() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="copy">Cargo object to copy</param>
        /// ----------------------------------------------------------------------------
        public Cargo(Cargo copy)
        {
            this.Ironium = copy.Ironium;
            this.Boranium = copy.Boranium;
            this.Germanium = copy.Germanium;
            this.Colonists = copy.Colonists;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the Mass of the cargo.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Mass
        {
            get { return Ironium + Boranium + Germanium + Colonists; }
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova xml document.
        /// </param>
        /// ----------------------------------------------------------------------------
        public Cargo(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {

                    switch (subnode.Name.ToLower())
                    {
                        case "ironium":
                            {
                                Ironium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "boranium":
                            {
                                Boranium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "germanium":
                            {
                                Germanium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "colonists":
                            {
                                Colonists = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this object to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Cargo</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCargo = xmldoc.CreateElement("Cargo");

            NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Ironium", this.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Boranium", this.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Germanium", this.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Colonists", this.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelCargo;
        }

        #endregion

    }
}
