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
// This class defines a computer property.
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common.Components
{
    [Serializable]
    public class Computer : ComponentProperty
    {
        public int Initiative = 0; // computers  
        public double Accuracy = 0; // computers

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Computer() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing <see cref="Computer"/>.</param>
        /// ----------------------------------------------------------------------------
        public Computer(Computer existing)
        {
            this.Initiative = existing.Initiative;
            this.Accuracy = existing.Accuracy;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Computer(this);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            Computer temp = this + (Computer)op2;
            Initiative = temp.Initiative;
            Accuracy = temp.Accuracy;
        }

        /// <summary>
        /// Polymorphic multiplication of properties.
        /// </summary>
        /// <param name="scalar"></param>
        public override void Scale(int scalar)
        {
            Computer temp = this * scalar;
            Initiative = temp.Initiative;
            Accuracy = temp.Accuracy;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand.</param>
        /// <param name="op2">RHS operand.</param>
        /// <returns>Sum of the properties.</returns>
        /// ----------------------------------------------------------------------------
        public static Computer operator +(Computer op1, Computer op2)
        {
            Computer sum = new Computer(op1);
            sum.Initiative = op1.Initiative + op2.Initiative;
            // Sum of two independant probabilities: (1 - ( (1-Accuracy1 ) * (1-Accuracy2) )
            // Using 100.0 as Accuracy is on a 1 to 100 (%) scale not 0 to 1 (normalised) scale
            sum.Accuracy = 100.0 - ((100.0 - op1.Accuracy) * (100.0 - op2.Accuracy) / 100.0);
            return op1;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property to scale.</param>
        /// <param name="scalar">Number of instances of this property.</param>
        /// <returns>A single property that represents all these instances.</returns>
        /// ----------------------------------------------------------------------------
        public static Computer operator *(Computer op1, int scalar)
        {
            Computer sum = new Computer(op1);
            sum.Initiative = op1.Initiative * scalar;
            // Sum of independant probabilities: (1 - ( (1-Accuracy1 )^scalar )
            sum.Accuracy = (1.0 - Math.Pow(1.0 - (op1.Accuracy / 100.0), scalar)) * 100.0;
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
        public Computer(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "initiative")
                    {
                        Initiative = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "accuracy")
                    {
                        Accuracy = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // Initiative
            XmlElement xmlelInitiative = xmldoc.CreateElement("Initiative");
            XmlText xmltxtInitiative = xmldoc.CreateTextNode(this.Initiative.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelInitiative.AppendChild(xmltxtInitiative);
            xmlelProperty.AppendChild(xmlelInitiative);
            // Accuracy
            XmlElement xmlelAccuracy = xmldoc.CreateElement("Accuracy");
            XmlText xmltxtAccuracy = xmldoc.CreateTextNode(this.Accuracy.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelAccuracy.AppendChild(xmltxtAccuracy);
            xmlelProperty.AppendChild(xmlelAccuracy);

            return xmlelProperty;
        }

        #endregion
    }
}

