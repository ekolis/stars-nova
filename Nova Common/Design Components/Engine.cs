// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Definition of an engine object.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace NovaCommon
{

// ============================================================================
// Engine.
// ============================================================================


   [Serializable]
   public class Engine : ComponentProperty
   {
      public int[] FuelConsumption        = new int[10];
      public bool  RamScoop               = false;
      public int   FastestSafeSpeed       = 0;
      public int   OptimalSpeed           = 0;

// ============================================================================
// Engine construction
// ============================================================================

      public Engine ()
      {

      }


// ============================================================================
// Construction from a Component object
// ============================================================================

      public Engine(Engine existing) 
      {
          for (int i = 0; i < FuelConsumption.Length; i++)
          {
              this.FuelConsumption[i] = existing.FuelConsumption[i];
          }
          this.RamScoop = existing.RamScoop;
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Engine(this);
      }

      //============================================================================
      // Provide a way to add properties in the ship design.
      // engines don't add in Nova
      //============================================================================
      public static Engine operator +(Engine op1, Engine op2)
      {
          return op1; 
      }
      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      // engines don't scale in Nova
      //============================================================================
      public static Engine operator *(Engine op1, int scalar)
      {
          return op1;
      }       
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Engine" in a Nova 
//               compenent definition file (xml document).
// ============================================================================

      public Engine(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "ramscoop":
                          RamScoop = bool.Parse(((XmlText)subnode.FirstChild).Value);
                          break;
                      case "fastestsafespeed":
                          FastestSafeSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "optimalspeed":
                          OptimalSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "fuelconsumption":
                          {
                              // load each fuel consumption value

                              for (int warp = 0; warp < FuelConsumption.Length; warp++)
                              {

                                  FuelConsumption[warp] = int.Parse(((XmlText)subnode.SelectSingleNode("Warp" + warp.ToString(System.Globalization.CultureInfo.InvariantCulture)).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                              }
                              break;
                          }
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
// Return an XmlElement representation of the Property for saving.
// ============================================================================
      public override XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelProperty = xmldoc.CreateElement("Property");

          // RamScoop
          XmlElement xmlelRamScoop = xmldoc.CreateElement("RamScoop");
          XmlText xmltxtRamScoop = xmldoc.CreateTextNode(this.RamScoop.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelRamScoop.AppendChild(xmltxtRamScoop);
          xmlelProperty.AppendChild(xmlelRamScoop);
          // FastestSafeSpeed
          XmlElement xmlelFastestSafeSpeed = xmldoc.CreateElement("FastestSafeSpeed");
          XmlText xmltxtFastestSafeSpeed = xmldoc.CreateTextNode(this.FastestSafeSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelFastestSafeSpeed.AppendChild(xmltxtFastestSafeSpeed);
          xmlelProperty.AppendChild(xmlelFastestSafeSpeed);
          // Optimal Speed
          XmlElement xmlelOptimalSpeed = xmldoc.CreateElement("OptimalSpeed");
          XmlText xmltxtOptimalSpeed = xmldoc.CreateTextNode(this.OptimalSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelOptimalSpeed.AppendChild(xmltxtOptimalSpeed);
          xmlelProperty.AppendChild(xmlelOptimalSpeed);
          // FuelConsumption
          XmlElement xmlelFuelConsumption = xmldoc.CreateElement("FuelConsumption");
          for (int warp = 0; warp < FuelConsumption.Length; warp++ )
          {
              XmlElement xmlelWarp = xmldoc.CreateElement("Warp" + warp.ToString(System.Globalization.CultureInfo.InvariantCulture));
              XmlText xmltextWarp = xmldoc.CreateTextNode(this.FuelConsumption[warp].ToString(System.Globalization.CultureInfo.InvariantCulture));
              xmlelWarp.AppendChild(xmltextWarp);
              xmlelFuelConsumption.AppendChild(xmlelWarp);
          }
          xmlelProperty.AppendChild(xmlelFuelConsumption);

          return xmlelProperty;
      }

   }
}
