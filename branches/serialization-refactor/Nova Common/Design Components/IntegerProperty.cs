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
   public class IntegerProperty : ComponentProperty
   {
      public int Value = 0;

// ============================================================================
// Construction from scratch
// ============================================================================

      public IntegerProperty()
      {

      }


// ============================================================================
// Construction from a ComponentProperty object
// ============================================================================

      public IntegerProperty(IntegerProperty existing)
        
      {
          this.Value = existing.Value;
      }

       // ============================================================================
       // Construction from an int
       // ============================================================================
      public IntegerProperty(int existing)
      {
          this.Value = existing;
      }
      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new IntegerProperty(this);
      }

       //============================================================================
      // Provide a way to add properties in the ship design.
      //============================================================================
      public static IntegerProperty operator +(IntegerProperty op1, IntegerProperty op2)
      {
          return new IntegerProperty(op1.Value + op2.Value);
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      //============================================================================
      public static IntegerProperty operator *(IntegerProperty op1, int scalar)
      {
          return new IntegerProperty(op1.Value * scalar);
      }       

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type equal to one of the simple 
//               property types in a Nova compenent definition file (xml document).
// ============================================================================
      public IntegerProperty(XmlNode node)
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

      public override void ReadXml(XmlReader reader)
      {
          throw new NotImplementedException(); // TODO XML deserialization of IntegerProperty
      }

      public override void WriteXml(XmlWriter writer)
      {
          writer.WriteElementString("Value", Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
      }
   }
}

