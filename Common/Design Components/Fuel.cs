// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This class defines a Fuel property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

namespace NovaCommon
{

    /// <summary>
    /// Fuel Property
    /// </summary>
    [Serializable]
    public class Fuel : ComponentProperty
    {
        public int Capacity = 0;
        public int Generation = 0;


        #region Construction

        /// <summary>
        /// Default constructor
        /// </summary>
        public Fuel()
        {

        }

        /// <summary>
        /// Initialising constructor
        /// </summary>
        /// <param name="capacity">Fuel capacity added by this property.</param>
        /// <param name="generation">Fuel generation per year added by this property.</param>
        public Fuel(int capacity, int generation)
        {
            this.Capacity = capacity;
            this.Generation = generation;
        }


        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="Fuel"/> property to copy.</param>
        public Fuel(Fuel existing)
        {
            this.Capacity = existing.Capacity;
            this.Generation = existing.Generation;
        }

        #endregion

        #region Interface ICloneable

        /// <summary>
        /// Implementation of the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A copy of this <see cref="Fuel"/> property.</returns>
        public override object Clone()
        {
            return new Fuel(this);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operator</param>
        /// <param name="op2">RHS operator</param>
        /// <returns>A new <see cref="Fuel"/> property with total capacity and fuel generation of both operands.</returns>
        public static Fuel operator +(Fuel op1, Fuel op2)
        {
            Fuel sum = new Fuel();
            sum.Capacity = op1.Capacity + op2.Capacity;
            sum.Generation = op1.Generation + op2.Generation;
            return sum;
        }


        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">The property to be scaled.</param>
        /// <param name="scalar">The number of instances of the property.</param>
        /// <returns>The scaled property.</returns>
        public static Fuel operator *(Fuel op1, int scalar)
        {
            Fuel sum = new Fuel(op1);
            sum.Capacity = op1.Capacity * scalar;
            sum.Generation = op1.Generation * scalar;
            return sum;
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova compenent definition file (xml document).
        /// </param>
        public Fuel(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "capacity")
                    {
                        Capacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "generation")
                    {
                        Generation = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // Capacity
            XmlElement xmlelCapacity = xmldoc.CreateElement("Capacity");
            XmlText xmltxtCapacity = xmldoc.CreateTextNode(this.Capacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelCapacity.AppendChild(xmltxtCapacity);
            xmlelProperty.AppendChild(xmlelCapacity);
            // Generation
            XmlElement xmlelGeneration = xmldoc.CreateElement("Generation");
            XmlText xmltxtGeneration = xmldoc.CreateTextNode(this.Generation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelGeneration.AppendChild(xmltxtGeneration);
            xmlelProperty.AppendChild(xmlelGeneration);

            return xmlelProperty;
        }

        #endregion
    }
}

