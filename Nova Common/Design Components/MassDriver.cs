// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale Apr 2009
//
// This class defines an integer property used for any int value property.
// Possible uses are shield, armor, cargoPod, Mining Robot.
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
    public class MassDriver : ComponentProperty
    {
        public int Value = 0;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public MassDriver()
        {

        }


        // ============================================================================
        // Construction from a ComponentProperty object
        // ============================================================================

        public MassDriver(MassDriver existing)
        {
            this.Value = existing.Value;
        }

        // ============================================================================
        // Construction from an int
        // ============================================================================
        public MassDriver(int existing)
        {
            this.Value = existing;
        }
        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new MassDriver(this);
        }
        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static MassDriver operator +(MassDriver op1, MassDriver op2)
        {
            if (op1.Value == op2.Value)
                return new MassDriver(op1.Value + 1);
            else
                return new MassDriver(Math.Max(op1.Value, op2.Value));
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        // Mass Driver doesn't scale
        //============================================================================
        public static MassDriver operator *(MassDriver op1, int scalar)
        {
            return new MassDriver(op1.Value);
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to one of the simple 
        //               property types in a Nova compenent definition file (xml document).
        // ============================================================================
        public MassDriver(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "value")
                    {
                        Value = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString());
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }
    }
}

