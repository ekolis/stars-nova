// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale Apr 2009
//
// This class defines an energy capacitor property, which improves the power of 
// beam weapons.
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
    public class CapacitorProperty : ComponentProperty
    {
        public double Value = 0;
        public static double MAXIMUM = 250;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public CapacitorProperty()
        {

        }


        // ============================================================================
        // Construction from a ComponentProperty object
        // ============================================================================

        public CapacitorProperty(CapacitorProperty existing)
        {
            this.Value = Math.Min(existing.Value, CapacitorProperty.MAXIMUM);
        }

        // ============================================================================
        // Construction from an int
        // ============================================================================
        public CapacitorProperty(double existing)
        {
            this.Value = Math.Min(existing, CapacitorProperty.MAXIMUM);
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new CapacitorProperty(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static CapacitorProperty operator +(CapacitorProperty op1, CapacitorProperty op2)
        {
            return new CapacitorProperty(((100 + op1.Value) * (100 + op2.Value)) / 100 - 100);
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static CapacitorProperty operator *(CapacitorProperty op1, int scalar)
        {
            double value = op1.Value;
            value /= 100;
            value = Math.Pow(1.0 + value, scalar) - 1.0;
            value *= 100;

            return new CapacitorProperty(value);
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to "Capacitor"
        // ============================================================================
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
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException(); // TODO XML deserialization of Capacitor
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Value", Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}

