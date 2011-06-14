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
// This class defines an energy capacitor property, which improves the power of 
// beam weapons.
// ===========================================================================
#endregion

namespace Nova.Common.Components
{
    #region Using Statements
    using System;
    using System.Xml;
    #endregion

    [Serializable]
    public class CapacitorProperty : ComponentProperty
    {
        public double Value = 0;
        public static double Maximum = 250;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CapacitorProperty class.
        /// </summary>
        public CapacitorProperty()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CapacitorProperty class.
        /// </summary>
        /// <param name="existing">An existing property to copy.</param>
        public CapacitorProperty(CapacitorProperty existing)
        {
            Value = Math.Min(existing.Value, Maximum);
        }

        /// <summary>
        /// Initializes a new instance of the CapacitorProperty class.
        /// </summary>
        /// <param name="existing">Capacitance boost of this property.</param>
        public CapacitorProperty(double existing)
        {
            this.Value = Math.Min(existing, CapacitorProperty.Maximum);
        }

        #endregion

        #region Interface ICloneable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A clone of this property.</returns>
        public override object Clone()
        {
            return new CapacitorProperty(this);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            Value = (this + (CapacitorProperty)op2).Value;
        }

        /// <summary>
        /// Polymorphic multiplication of properties.
        /// </summary>
        /// <param name="scalar"></param>
        public override void Scale(int scalar)
        {
            Value = (this * scalar).Value;
        }

        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand.</param>
        /// <param name="op2">RHS operand.</param>
        /// <returns>Sum of the properties.</returns>
        public static CapacitorProperty operator +(CapacitorProperty op1, CapacitorProperty op2)
        {
            return new CapacitorProperty((((100 + op1.Value) * (100 + op2.Value)) / 100) - 100);
        }

        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property to scale.</param>
        /// <param name="scalar">Number of instances of this property.</param>
        /// <returns>A single property that represents all these instances.</returns>
        public static CapacitorProperty operator *(CapacitorProperty op1, int scalar)
        {
            double value = op1.Value;
            value /= 100;
            value = Math.Pow(1.0 + value, scalar) - 1.0;
            value *= 100;

            return new CapacitorProperty(value);
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova compenent definition file (xml document).
        /// </param>
        public CapacitorProperty(XmlNode node)
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

        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // store the value
            XmlElement xmlelValue = xmldoc.CreateElement("Value");
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }

        #endregion
    }
}

