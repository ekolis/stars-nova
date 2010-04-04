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

        /// <summary>
        /// Construction from scratch
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


        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        public override object Clone()
        {
            return new HullAffinity(this);
        }


        /// <summary><para>
        /// Operator+ 
        /// </para><para>
        /// Provide a way to sum up properties in the ship design.
        /// Doesn't mean anything in the context of HullAffinity
        /// </para></summary>
        /// <param name="op1">The LHS parameter.</param>
        /// <param name="op2">The RHS parameter.</param>
        /// <returns>The LHS parameter.</returns>
        public static HullAffinity operator +(HullAffinity op1, HullAffinity op2)
        {
            return op1;
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        // Doesn't mean anything in the context of HullAffinity
        //============================================================================
        public static HullAffinity operator *(HullAffinity op1, int scalar)
        {
            return op1;
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to one of the simple 
        //               property types in a Nova compenent definition file (xml document).
        // ============================================================================
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

        // ============================================================================
        // Return an XmlElement representation of the Property
        // ============================================================================
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
    }
}

