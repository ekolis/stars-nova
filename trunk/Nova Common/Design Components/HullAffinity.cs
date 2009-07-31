// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale Apr 2009
//
// This class defines a simple property used for any one value property.
// Possible uses are shield, armor, cargoPod, Jammer, 
// Capacitor, Cloak (or Tachyon Detector), Mass Driver or Mining Robot.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Runtime.Serialization;

// ============================================================================
// Simple Property Class
// ============================================================================

namespace NovaCommon
{
    [Serializable]
    public class HullAffinity : ComponentProperty
    {
        public String Value = String.Empty;
        // public string Name = null; // not required - use the dictionary key to identify the property.

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public HullAffinity()
        {

        }


        // ============================================================================
        // Construction from a ComponentProperty object
        // ============================================================================

        public HullAffinity(HullAffinity existing)
        {
            this.Value = existing.Value;
        }

        // ============================================================================
        // Construction from an int
        // ============================================================================
        public HullAffinity(String existing)
        {
            this.Value = existing;
        }
        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new HullAffinity(this);
        }
        //============================================================================
        // Provide a way to add properties in the ship design.
        // Doesn't mean anything in the context of HullAffinity
        //============================================================================
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

