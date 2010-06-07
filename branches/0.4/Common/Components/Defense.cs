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
// This class defines a Defense property, for components that form planetary defenses.
// ===========================================================================
#endregion

using System;
using System.Xml;
using Nova.Common;

namespace Nova.Common.Components
{

    [Serializable]
    public class Defense : ComponentProperty
    {
        public double Value = 0.0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Defense() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="existing">Defence value of 1 defence of this type.</param>
        /// ----------------------------------------------------------------------------
        public Defense(double existing)
        {
            this.Value = existing;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing object to copy.</param>
        /// ----------------------------------------------------------------------------
        public Defense(Defense existing)
        {
            this.Value = existing.Value;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Defense(this);
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
        public static Defense operator +(Defense op1, Defense op2)
        {
            return new Defense(1.0 - ((1.0 - op1.Value) * (1.0 - op2.Value)));
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property to scale.</param>
        /// <param name="scalar">Number of instances of this property.</param>
        /// <returns>A single property that represents all these instances.</returns>
        /// ----------------------------------------------------------------------------
        public static Defense operator *(Defense op1, int scalar)
        {
            return new Defense(1.0 - Math.Pow(1.0 - op1.Value, scalar));
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
        public Defense(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "value")
                    {
                        Value = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // store the value
            XmlElement xmlelValue = xmldoc.CreateElement("Value");
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString(""));
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }

        #endregion

    }
}

