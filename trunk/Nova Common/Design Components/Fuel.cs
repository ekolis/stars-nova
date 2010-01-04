// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified 2009 Daniel Vale dan_vale@sourceforge.net
//
// This class defines a Fuel property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

// ============================================================================
// Fuel class
// ============================================================================

namespace NovaCommon
{



    [Serializable]
    public class Fuel : ComponentProperty
    {
        public int Capacity = 0;
        public int Generation = 0;


        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public Fuel()
        {

        }

        // ============================================================================
        // Construction from capacity and generation.
        // ============================================================================
        public Fuel(int capacity, int generation)
        {
            this.Capacity = capacity;
            this.Generation = generation;
        }

        // ============================================================================
        // Copy constructor
        // ============================================================================

        public Fuel(Fuel existing)
        {
            this.Capacity = existing.Capacity;
            this.Generation = existing.Generation;
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new Fuel(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static Fuel operator +(Fuel op1, Fuel op2)
        {
            Fuel sum = new Fuel(op1);
            sum.Capacity = op1.Capacity + op2.Capacity;
            sum.Generation = op1.Generation + op2.Generation;
            return sum;
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static Fuel operator *(Fuel op1, int scalar)
        {
            Fuel sum = new Fuel(op1);
            sum.Capacity = op1.Capacity * scalar;
            sum.Generation = op1.Generation * scalar;
            return sum;
        }
       
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Fuel" in a Nova 
//               compenent definition file (xml document).
// ============================================================================

        public Fuel(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "capacity")
                  {
                      Capacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "generation")
                  {
                      Generation = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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

            // Capacity
            XmlElement xmlelCapacity = xmldoc.CreateElement("Capacity");
            XmlText xmltxtCapacity = xmldoc.CreateTextNode(this.Capacity.ToString());
            xmlelCapacity.AppendChild(xmltxtCapacity);
            xmlelProperty.AppendChild(xmlelCapacity);
            // Generation
            XmlElement xmlelGeneration = xmldoc.CreateElement("Generation");
            XmlText xmltxtGeneration = xmldoc.CreateTextNode(this.Generation.ToString());
            xmlelGeneration.AppendChild(xmltxtGeneration);
            xmlelProperty.AppendChild(xmlelGeneration);

            return xmlelProperty;
        }

    }
}

