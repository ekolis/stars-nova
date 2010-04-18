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
// This class defines a radiation property such as that on the Radiating Hydro-Ram Scoop.
// Radiation sums as the maximum of a given set of radiation properties.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Runtime.Serialization;

// ============================================================================
// Simple Property Class
// ============================================================================

namespace NovaCommon
{
    [Serializable]
    public class Radiation : ComponentProperty
    {
        public double Value = 0;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public Radiation()
        {

        }


        // ============================================================================
        // Construction from a Radiation object
        // ============================================================================

        public Radiation(Radiation existing)
        {
            this.Value = existing.Value;
        }

        // ============================================================================
        // Construction from a double
        // ============================================================================
        public Radiation(double existing)
        {
            this.Value = existing;
        }
        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new Radiation(this);
        }
        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static Radiation operator +(Radiation op1, Radiation op2)
        {
            return new Radiation(Math.Max(op1.Value, op2.Value));
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static Radiation operator *(Radiation op1, int scalar)
        {
            return new Radiation(op1.Value);
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Property" node with Type equal to one of the simple 
        //               property types in a Nova compenent definition file (xml document).
        // ============================================================================
        public Radiation(XmlNode node)
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
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }
    }
}

