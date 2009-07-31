// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale 2009
//
// This class defines a Gate Property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;

// ============================================================================
// Gate class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class Gate : ComponentProperty
   {
      public double SafeHullMass = 0;
      public double SafeRange    = 0;


// ============================================================================
// Construction from scratch
// ============================================================================
      public Gate()
      {
      }


// ============================================================================
// Construction from a Gate object
// ============================================================================
      public Gate(Gate existing)
      {
          this.SafeHullMass = existing.SafeHullMass;
          this.SafeRange = existing.SafeRange;
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Gate(this);
      }

      //============================================================================
      // Provide a way to add properties in the ship design.
      // Whilst it is possible to have more than one gate on a hull, and it could
      // make sense to use the best capabilities of each gate, this is not how
      // Stars! works with gates. One gate get precedence and the other is ignored.
      // TODO (low priority) check that the same orbital slot takes precedence as
      // in Stars!
      //============================================================================
      public static Gate operator +(Gate op1, Gate op2)
      {
          return op1;
      }
      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
       // as above, gates don't scale
      //============================================================================
      public static Gate operator *(Gate op1, int scalar)
      {
          return op1;
      }
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Gate" in a Nova 
//               compenent definition file (xml document).
// ============================================================================
      public Gate(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "safehullmass")
                  {
                      SafeHullMass = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  if (subnode.Name.ToLower() == "saferange")
                  {
                      SafeRange = int.Parse(((XmlText)subnode.FirstChild).Value);
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

          // SafeHullMass
          XmlElement xmlelSafeHullMass = xmldoc.CreateElement("SafeHullMass");
          XmlText xmltxtSafeHullMass = xmldoc.CreateTextNode(this.SafeHullMass.ToString());
          xmlelSafeHullMass.AppendChild(xmltxtSafeHullMass);
          xmlelProperty.AppendChild(xmlelSafeHullMass);
          // SafeRange
          XmlElement xmlelSafeRange = xmldoc.CreateElement("SafeRange");
          XmlText xmltxtSafeRange = xmldoc.CreateTextNode(this.SafeRange.ToString());
          xmlelSafeRange.AppendChild(xmltxtSafeRange);
          xmlelProperty.AppendChild(xmlelSafeRange);

          return xmlelProperty;
      }
   }
}

