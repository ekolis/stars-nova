// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified from Electrical component to computer property Daniel Vale Apr 2009.
//
// This class defines a computer property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;

// ============================================================================
// Electrical class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class Computer : ComponentProperty
   {
      public int Initiative      = 0; // computers  
      public double Accuracy        = 0; // computers

// ============================================================================
// Construction from scratch
// ============================================================================

      public Computer()
      {

      }


// ============================================================================
// Construction from a Computer object
// ============================================================================

      public Computer(Computer existing) 
      {
          this.Initiative = existing.Initiative;
          this.Accuracy = existing.Accuracy;
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Computer(this);
      }

      //============================================================================
      // Provide a way to add properties in the ship design.
      //============================================================================
      public static Computer operator +(Computer op1, Computer op2)
      {
          Computer sum = new Computer(op1);
          sum.Initiative = op1.Initiative + op2.Initiative;
          // Sum of two independant probabilities: (1 - ( (1-Accuracy1 ) * (1-Accuracy2) )
          // Using 100.0 as Accuracy is on a 1 to 100 (%) scale not 0 to 1 (normalised) scale
          sum.Accuracy = 100.0 - ((100.0 - op1.Accuracy) * (100.0 - op2.Accuracy) / 100.0);
          return op1;
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      //============================================================================
      public static Computer operator *(Computer op1, int scalar)
      {
          Computer sum = new Computer(op1);
          sum.Initiative = op1.Initiative * scalar;
          // Sum of independant probabilities: (1 - ( (1-Accuracy1 )^scalar )
          sum.Accuracy = (1.0 - (Math.Pow(1.0 - (op1.Accuracy / 100.0), scalar))) * 100.0;
          return sum;
      }

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Computer" in a Nova 
//               compenent definition file (xml document).
// ============================================================================

      public Computer(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "initiative")
                  {
                      Initiative = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  if (subnode.Name.ToLower() == "accuracy")
                  {
                      Accuracy = double.Parse(((XmlText)subnode.FirstChild).Value);
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

          // Initiative
          XmlElement xmlelInitiative = xmldoc.CreateElement("Initiative");
          XmlText xmltxtInitiative = xmldoc.CreateTextNode(this.Initiative.ToString());
          xmlelInitiative.AppendChild(xmltxtInitiative);
          xmlelProperty.AppendChild(xmlelInitiative);
          // Accuracy
          XmlElement xmlelAccuracy = xmldoc.CreateElement("Accuracy");
          XmlText xmltxtAccuracy = xmldoc.CreateTextNode(this.Accuracy.ToString());
          xmlelAccuracy.AppendChild(xmltxtAccuracy);
          xmlelProperty.AppendChild(xmlelAccuracy);

          return xmlelProperty;
      }
   }
}

