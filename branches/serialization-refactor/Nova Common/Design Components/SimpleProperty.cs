// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale Apr 2009
//
// This class defines a simple property used for any property which has no 
// value - it simply exists or not. For example 'Transport Ships Only' or
// 'Unarmed Ships Only' or 'Doubles Minelayer Efficiency'.
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
   public class SimpleProperty : ComponentProperty
   {
        // This property has no data!

// ============================================================================
// Construction from scratch
// ============================================================================
      public SimpleProperty() {}

// ============================================================================
// Construction from a ComponentProperty object
// ============================================================================
      public SimpleProperty(SimpleProperty existing) {}

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new SimpleProperty();
      }       

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type equal to one of the simple 
//               property types in a Nova compenent definition file (xml document).
// ============================================================================
      public SimpleProperty(XmlNode node) {}


      //============================================================================
      // Provide a way to add properties in the ship design.
      //============================================================================
      public static SimpleProperty operator +(SimpleProperty op1, SimpleProperty op2)
      {
          return op1;
      }

      // ============================================================================
      // Return an XmlElement representation of the Property
      // ============================================================================
      public override XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelProperty = xmldoc.CreateElement("Property");

          return xmlelProperty;
      }
   }
}

