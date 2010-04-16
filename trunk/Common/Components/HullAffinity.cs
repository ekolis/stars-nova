// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This module defines a property for components which can are restricted to 
// certain hulls. For example an Orbital Construction Module can only be fitted
// to a coloniser hull.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Runtime.Serialization;

namespace NovaCommon
{
    [Serializable]
    public class HullAffinity : ComponentProperty
    {
        // The name of a hull this can be fitted too. 
        // Multiple instances of this property can be used if more than one.
        public String Value = String.Empty;

        #region Construction and Initialisation

        /// <summary>
        /// Construction 
        /// </summary>
        public HullAffinity() { }


        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing <see cref="HullAffinity"/> object.</param>
        public HullAffinity(HullAffinity existing)
        {
            this.Value = existing.Value;
        }


        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="existing">The name of the hull type this affinity is for.</param>
        public HullAffinity(String existing)
        {
            this.Value = existing;
        }

        #endregion

        #region IColoneable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        public override object Clone()
        {
            return new HullAffinity(this);
        }

        #endregion

        #region Operators

        /// <summary><para>
        /// Operator+ 
        /// </para><para>
        /// Provide a way to sum up properties in the ship design.
        /// </para><para>
        /// Doesn't mean anything in the context of HullAffinity
        /// </para></summary>
        /// <param name="op1">The LHS parameter.</param>
        /// <param name="op2">The RHS parameter.</param>
        /// <returns>op1</returns>
        public static HullAffinity operator +(HullAffinity op1, HullAffinity op2)
        {
            return op1;
        }


        /// <summary><para>
        /// Operator*
        /// </para><para>
        /// Used to scale (multiply) properties in the ship design.
        /// </para><para>
        /// Doesn't mean anything in the context of HullAffinity.
        /// </para></summary>
        /// <param name="op1">Propertie to be scaled.</param>
        /// <param name="scalar">Scaling factor.</param>
        /// <returns>op1</returns>
        public static HullAffinity operator *(HullAffinity op1, int scalar)
        {
            return op1;
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A "Property" <see cref="XmlNode"/> with Type equal 
        /// to "Hull Affinity" in a Nova compenent definition file (xml document).</param>
        public HullAffinity(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "value")
                    {
                        Value =((XmlText)subnode.FirstChild).Value;
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
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // store the value
            XmlElement xmlelValue = xmldoc.CreateElement("Value");
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value);
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }

        #endregion
    }
}

