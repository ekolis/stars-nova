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
// This class defines a probability property, which is any single valued property
// which sums as the sum of independant probabilities. This includes beam deflectors, 
// cloaking, tachyon detectors & jammers.
// Defenses are treated separately as they have additional complications (see Defense).
// Capacitors sum geometrically, (see Capacitor). 
//
// Computers have their own class because they also add Initiative.
// (Could have made Initiative a seperate property.)
// ===========================================================================
#endregion

namespace Nova.Common.Components
{
    using System;
    using System.Xml;

    /// <summary>
    /// Probability property.
    /// </summary>
    [Serializable]
    public class ProbabilityProperty : ComponentProperty
    {
        public double Value = 0;
        // public string Name = null; // not required - use the dictionary key to identify the property.

        #region Construction

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProbabilityProperty() 
        { 
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing"></param>
        public ProbabilityProperty(ProbabilityProperty existing)
        {
            this.Value = existing.Value;
        }

        /// <summary>
        /// Initializing constructor.
        /// </summary>
        /// <param name="existing">Initial value.</param>
        public ProbabilityProperty(double existing)
        {
            this.Value = existing;
        }

        #endregion

        #region Interface ICloneable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>Clone of this object.</returns>
        public override object Clone()
        {
            return new ProbabilityProperty(this);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            Value = (this + (ProbabilityProperty)op2).Value;
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
        /// <returns>A <see cref="ProbabilityProperty"/> with value (as a percentage) 
        /// equal to the independent probability of op1 or op2.</returns>
        public static ProbabilityProperty operator +(ProbabilityProperty op1, ProbabilityProperty op2)
        {
            return new ProbabilityProperty(100 - (((100 - op1.Value) * (100 - op2.Value)) / 100));
        }

        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property in the stack.</param>
        /// <param name="scalar">Number of items in the stack.</param>
        /// <returns>A single property with value equal to the total independent probability of the stack.</returns>
        public static ProbabilityProperty operator *(ProbabilityProperty op1, int scalar)
        {
            double value = op1.Value;
            value /= 100;
            value = 1.0 - Math.Pow(1.0 - value, scalar);
            value *= 100;

            return new ProbabilityProperty(value);
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load from XML: initializing constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova component definition file (xml document).
        /// </param>
        public ProbabilityProperty(XmlNode node)
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
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialize this property to an <see cref="XmlElement"/>.
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

