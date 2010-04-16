// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale Apr 2009
//
// This class defines a probability property, which is any single valued property
// which sums as the sum of independant probabilities. This includes beam deflectors, 
// cloaking, tachyon detectors & jammers.
// Defenses are treated separately as they have additional complications (see Defense).
// Capacitors sum geometrically, (see Capacitor). 
//
// Computers have their own class because they also add Initiative.
// (Could have made Initiative a seperate property.)
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
    public class ProbabilityProperty : ComponentProperty
    {
        public double Value = 0;
        // public string Name = null; // not required - use the dictionary key to identify the property.

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public ProbabilityProperty()
        {

        }


        // ============================================================================
        // Construction from a ComponentProperty object
        // ============================================================================

        public ProbabilityProperty(ProbabilityProperty existing)
        {
            this.Value = existing.Value;
        }

        // ============================================================================
        // Construction from an int
        // ============================================================================
        public ProbabilityProperty(double existing)
        {
            this.Value = existing;
        }
        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new ProbabilityProperty(this);
        }
        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static ProbabilityProperty operator +(ProbabilityProperty op1, ProbabilityProperty op2)
        {
            return new ProbabilityProperty(100 - ((100 - op1.Value) * (100 - op2.Value))/100);
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static ProbabilityProperty operator *(ProbabilityProperty op1, int scalar)
        {
            double value = op1.Value;
            value /= 100;
            value = 1.0 - Math.Pow(1.0 - value, scalar);
            value *= 100;

            return new ProbabilityProperty(value);
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to one of the simple 
        //               property types in a Nova compenent definition file (xml document).
        // ============================================================================
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

        // ============================================================================
        // Return an XmlElement representation of the Property
        // ============================================================================
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
    }
}

