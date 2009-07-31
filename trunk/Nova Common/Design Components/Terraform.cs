// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified 2009 Daniel Vale dan_vale@sourceforge.net
//
// This class defines a terraforming property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

// ============================================================================
// Terraform class
// ============================================================================

namespace NovaCommon
{



    [Serializable]
    public class Terraform : ComponentProperty
    {
        public int MaxModifiedGravity = 0;
        public int MaxModifiedTemperature = 0;
        public int MaxModifiedRadiation = 0;


        // ============================================================================
        // Construction from scratch
        // ============================================================================

        public Terraform()
        {
            
        }

        // ============================================================================
        // Construction from an ComponentProperty object
        // ============================================================================

        public Terraform(Terraform existing)
        {
            this.MaxModifiedGravity = existing.MaxModifiedGravity;
            this.MaxModifiedTemperature = existing.MaxModifiedTemperature;
            this.MaxModifiedRadiation = existing.MaxModifiedRadiation;
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public override object Clone()
        {
            return new Terraform(this);
        }

        //============================================================================
        // Provide a way to add properties in the ship design.
        //============================================================================
        public static Terraform operator +(Terraform op1, Terraform op2)
        {
            Terraform sum = new Terraform(op1);
            sum.MaxModifiedGravity = Math.Max(op1.MaxModifiedGravity, op2.MaxModifiedGravity);
            sum.MaxModifiedRadiation = Math.Max(op1.MaxModifiedRadiation, op2.MaxModifiedRadiation); 
            sum.MaxModifiedTemperature = Math.Max(op1.MaxModifiedTemperature, op2.MaxModifiedTemperature);
            return sum;
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        // Terraformers don't scale, as the modifications represent maximums.
        // Note this represents the terraforming capability of a component,
        // not multiple terraforming units produced by a planet, which work differently (1% each).
        //============================================================================
        public static Terraform operator *(Terraform op1, int scalar)
        {
            return op1;
        }     
  
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Terraform" in a Nova 
//               compenent definition file (xml document).
// ============================================================================
        public Terraform(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "maxmodifiedgravity")
                  {
                      MaxModifiedGravity = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  if (subnode.Name.ToLower() == "maxmodifiedtemperature")
                  {
                      MaxModifiedTemperature = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  if (subnode.Name.ToLower() == "maxmodifiedradiation")
                  {
                      MaxModifiedRadiation = int.Parse(((XmlText)subnode.FirstChild).Value);
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

            // MaxModifiedGravity
            XmlElement xmlelMaxModifiedGravity = xmldoc.CreateElement("MaxModifiedGravity");
            XmlText xmltxtMaxModifiedGravity = xmldoc.CreateTextNode(this.MaxModifiedGravity.ToString());
            xmlelMaxModifiedGravity.AppendChild(xmltxtMaxModifiedGravity);
            xmlelProperty.AppendChild(xmlelMaxModifiedGravity);
            // MaxModifiedTemperature
            XmlElement xmlelMaxModifiedTemperature = xmldoc.CreateElement("MaxModifiedTemperature");
            XmlText xmltxtMaxModifiedTemperature = xmldoc.CreateTextNode(this.MaxModifiedTemperature.ToString());
            xmlelMaxModifiedTemperature.AppendChild(xmltxtMaxModifiedTemperature);
            xmlelProperty.AppendChild(xmlelMaxModifiedTemperature);
            // MaxModifiedRadiation
            XmlElement xmlelMaxModifiedRadiation = xmldoc.CreateElement("MaxModifiedRadiation");
            XmlText xmltxtMaxModifiedRadiation = xmldoc.CreateTextNode(this.MaxModifiedRadiation.ToString());
            xmlelMaxModifiedRadiation.AppendChild(xmltxtMaxModifiedRadiation);
            xmlelProperty.AppendChild(xmlelMaxModifiedRadiation);

            return xmlelProperty;
        }
 

    }
}

