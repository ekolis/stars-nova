// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified 2009 Daniel Vale dan_vale@sourceforge.net
//
// This class defines a Colonizer property. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

// ============================================================================
// Colonizer class
// ============================================================================

namespace NovaCommon
{

    [Serializable]
    public class Colonizer : ComponentProperty
    {
        public bool Orbital = false;

        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public Colonizer()
        {

        }

        // ============================================================================
        // Construction from an ComponentProperty object
        // ============================================================================
        public Colonizer(Colonizer existing)
        {
            this.Orbital = existing.Orbital;
        }
        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new Colonizer(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        // Colonizer's don't add.
        //============================================================================
        public static Colonizer operator +(Colonizer op1, Colonizer op2)
        {
            return op1;
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        // Colonizer's don't scale.
        //============================================================================
        public static Colonizer operator *(Colonizer op1, int scalar)
        {
            return op1;
        }

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Colonizer" in a Nova 
//               compenent definition file (xml document).
// ============================================================================

        public Colonizer(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "orbital")
                  {
                      Orbital = bool.Parse(((XmlText)subnode.FirstChild).Value);
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
            throw new NotImplementedException(); // TODO XML deserialization of Colonizer
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Orbital", Orbital.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}

