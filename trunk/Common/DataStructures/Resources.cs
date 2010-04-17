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
// The resources needed to construct a game item;
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace NovaCommon
{

    /// <summary>
    /// Resource class. Individual resource values are either kT (minerals on hand)
    /// or percent (mineral concentrations).
    /// </summary>
    [Serializable]
    public class Resources
    {
        public double Boranium = 0;
        public double Ironium = 0;
        public double Germanium = 0;
        public double Energy = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising Constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources(int i, int b, int g, int e)
        {
            Ironium = i;
            Boranium = b;
            Germanium = g;
            Energy = e;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">object to copy</param>
        /// ----------------------------------------------------------------------------
        public Resources(Resources copy)
        {
            this.Ironium = copy.Ironium;
            this.Boranium = copy.Boranium;
            this.Germanium = copy.Germanium;
            this.Energy = copy.Energy;
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if a resource set is greater than another.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public static bool operator >=(Resources lhs, Resources rhs)
        {
            if (lhs.Ironium >= rhs.Ironium && lhs.Boranium >= rhs.Boranium &&
                lhs.Germanium >= rhs.Germanium && lhs.Energy >= rhs.Energy)
            {
                return true;
            }
            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if a resources set is less than another.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public static bool operator <=(Resources lhs, Resources rhs)
        {
            return (rhs >= lhs);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Subtract one resource set from another.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public static Resources operator -(Resources lhs, Resources rhs)
        {
            Resources result = new Resources();

            result.Ironium = lhs.Ironium - rhs.Ironium;
            result.Boranium = lhs.Boranium - rhs.Boranium;
            result.Germanium = lhs.Germanium - rhs.Germanium;
            result.Energy = lhs.Energy - rhs.Energy;

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a resource set to another.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static Resources operator +(Resources lhs, Resources rhs)
        {
            Resources result = new Resources();

            result.Ironium = lhs.Ironium + rhs.Ironium;
            result.Boranium = lhs.Boranium + rhs.Boranium;
            result.Germanium = lhs.Germanium + rhs.Germanium;
            result.Energy = lhs.Energy + rhs.Energy;

            return result;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the mass of a resource set (Energy does not contribute to the mass).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Mass
        {
            get { return (int)(Ironium + Boranium + Germanium); }
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">node is a "resource" <see cref="XmlNode"/> in a Nova compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public Resources(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "ironium":
                            Ironium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "boranium":
                            Boranium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "germanium":
                            Germanium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "energy":
                            Energy = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }
                subnode = subnode.NextSibling;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this Resources to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>Return an <see cref="XmlElement"/> representation of the resource cost.</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelResource = xmldoc.CreateElement("Resource");

            // Boranium
            XmlElement xmlelBoranium = xmldoc.CreateElement("Boranium");
            XmlText xmltxtBoranium = xmldoc.CreateTextNode(this.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelBoranium.AppendChild(xmltxtBoranium);
            xmlelResource.AppendChild(xmlelBoranium);
            // Ironium
            XmlElement xmlelIronium = xmldoc.CreateElement("Ironium");
            XmlText xmltxtIronium = xmldoc.CreateTextNode(this.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelIronium.AppendChild(xmltxtIronium);
            xmlelResource.AppendChild(xmlelIronium);
            // Germanium
            XmlElement xmlelGermanium = xmldoc.CreateElement("Germanium");
            XmlText xmltxtGermanium = xmldoc.CreateTextNode(this.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelGermanium.AppendChild(xmltxtGermanium);
            xmlelResource.AppendChild(xmlelGermanium);
            // Energy
            XmlElement xmlelEnergy = xmldoc.CreateElement("Energy");
            XmlText xmltxtEnergy = xmldoc.CreateTextNode(this.Energy.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelEnergy.AppendChild(xmltxtEnergy);
            xmlelResource.AppendChild(xmlelEnergy);

            return xmlelResource;
        }

        #endregion

    }
}
