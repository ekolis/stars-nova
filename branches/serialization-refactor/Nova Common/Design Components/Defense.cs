// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified 2009 Daniel Vale dan_vale@sourceforge.net
//
// This class defines a Defense property, for components that form planetary defenses.
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
    public class Defense : ComponentProperty
    {
        public double Value = 0.0;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public Defense()
        {

        }

        // ============================================================================
        // Construction from a double
        // ============================================================================

        public Defense(double existing)
        {
            this.Value = existing;
        }
        // ============================================================================
        // Construction from an ComponentProperty object
        // ============================================================================

        public Defense(Defense existing)
        {
            this.Value = existing.Value;
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new Defense(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static Defense operator +(Defense op1, Defense op2)
        {
            return new Defense(1.0 - ((1.0 - op1.Value) * (1.0 - op2.Value)));
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static Defense operator *(Defense op1, int scalar)
        {
            return new Defense(1.0 - Math.Pow(1.0 - op1.Value, scalar));
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to one of the double 
        //               property types in a Nova compenent definition file (xml document).
        // ============================================================================
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
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException(); // TODO XML deserialization of Defense
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Value", Value.ToString(""));
        }
    }
}

