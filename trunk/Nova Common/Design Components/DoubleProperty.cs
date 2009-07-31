// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified 2009 Daniel Vale dan_vale@sourceforge.net
//
// This class defines a DoubleProperty property. That is a property that contains
// a double precision floating point number only. This property is used for
// battle movement. (defenses don't add liniarly)
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

// ============================================================================
// DoubleProperty class
// ============================================================================

namespace NovaCommon
{

    [Serializable]
    public class DoubleProperty : ComponentProperty
    {
        public double Value = 0.00;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public DoubleProperty()
        {

        }

        // ============================================================================
        // Construction from a double
        // ============================================================================

        public DoubleProperty(double existing)
        {
            this.Value = existing;
        }
        // ============================================================================
        // Construction from an ComponentProperty object
        // ============================================================================

        public DoubleProperty(DoubleProperty existing)
        {
            this.Value = existing.Value;
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new DoubleProperty(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static DoubleProperty operator +(DoubleProperty op1, DoubleProperty op2)
        {
            return new DoubleProperty(op1.Value + op2.Value);
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static DoubleProperty operator *(DoubleProperty op1, int scalar)
        {
            return new DoubleProperty(op1.Value * scalar);
        }

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type equal to one of the double 
//               property types in a Nova compenent definition file (xml document).
// ============================================================================
        public DoubleProperty(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "value")
                    {
                        Value = double.Parse(((XmlText)subnode.FirstChild).Value);
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
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString(""));
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }
    }
}

