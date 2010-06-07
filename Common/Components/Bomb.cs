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
// This class defines a bomb property.
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common.Components
{
    /// <summary>
    /// Bomb class
    /// </summary>
    [Serializable]
    public class Bomb : ComponentProperty
    {
        public int Installations = 0;
        public double PopKill = 0;
        public int MinimumKill = 0;
        public bool IsSmart = false;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Bomb() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing"></param>
        /// ----------------------------------------------------------------------------
        public Bomb(Bomb existing)
        {
            this.Installations = existing.Installations;
            this.PopKill = existing.PopKill;
            this.MinimumKill = existing.MinimumKill;
            this.IsSmart = existing.IsSmart;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a clone of this object.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Bomb(this);
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand</param>
        /// <param name="op2">RHS operand</param>
        /// <returns>Sum of the properties.</returns>
        /// ----------------------------------------------------------------------------
        public static Bomb operator +(Bomb op1, Bomb op2)
        {
            if (op1.IsSmart != op2.IsSmart)
            {
                Report.Error("Bomb.operator+ attempted to combine conventional and smart bombs.");
                return op1;
            }

            Bomb sum = new Bomb(op1);
            if (sum.IsSmart)
            {
                sum.Installations = 0;
                sum.MinimumKill = 0;
                sum.PopKill = (1 - ((1 - op1.PopKill) * (1 - op2.PopKill)));
            }
            else
            {
                sum.Installations = op1.Installations + op2.Installations;
                sum.MinimumKill = op1.MinimumKill + op2.MinimumKill;
                sum.PopKill = op1.PopKill + op2.PopKill;
            }

            return sum;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to scale (multiply) bombs.
        /// </summary><param name="bomb">
        /// The bomb property to be scaled (multiplied).
        /// </param> <param name="bombCount">
        /// The scalar (number of bombs) to multiply by.
        /// </param>
        /// ----------------------------------------------------------------------------
        public static Bomb operator *(Bomb bomb, int bombCount)
        {
            Bomb sum = new Bomb(bomb);
            if (sum.IsSmart)
            {
                sum.Installations = 0;
                sum.MinimumKill = 0;
                sum.PopKill = (1 - (Math.Pow(1 - bomb.PopKill, bombCount)));
            }
            else
            {
                sum.Installations = bomb.Installations * bombCount;
                sum.MinimumKill = bomb.MinimumKill * bombCount;
                sum.PopKill = bomb.PopKill * bombCount;
            }

            return sum;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public Bomb(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "installations")
                    {
                        Installations = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "popkill")
                    {
                        PopKill = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "minimumkill")
                    {
                        MinimumKill = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "issmart")
                    {
                        IsSmart = bool.Parse(((XmlText)subnode.FirstChild).Value);
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // Installations
            XmlElement xmlelInstallations = xmldoc.CreateElement("Installations");
            XmlText xmltxtInstallations = xmldoc.CreateTextNode(this.Installations.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelInstallations.AppendChild(xmltxtInstallations);
            xmlelProperty.AppendChild(xmlelInstallations);
            // PopKill
            XmlElement xmlelPopKill = xmldoc.CreateElement("PopKill");
            XmlText xmltxtPopKill = xmldoc.CreateTextNode(this.PopKill.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelPopKill.AppendChild(xmltxtPopKill);
            xmlelProperty.AppendChild(xmlelPopKill);
            // MinimumKill
            XmlElement xmlelMinimumKill = xmldoc.CreateElement("MinimumKill");
            XmlText xmltxtMinimumKill = xmldoc.CreateTextNode(this.MinimumKill.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMinimumKill.AppendChild(xmltxtMinimumKill);
            xmlelProperty.AppendChild(xmlelMinimumKill);
            // IsSmart
            XmlElement xmlelIsSmart = xmldoc.CreateElement("IsSmart");
            XmlText xmltxtIsSmart = xmldoc.CreateTextNode(this.IsSmart.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelIsSmart.AppendChild(xmltxtIsSmart);
            xmlelProperty.AppendChild(xmlelIsSmart);

            return xmlelProperty;
        }

        #endregion

    }
}

