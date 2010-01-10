// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// The resources needed to construct a game item;
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;

namespace NovaCommon
{


// ============================================================================
// Resource class. Individual resource values are either kT (minerals on hand)
// or percent (mineral concentrations).
// ============================================================================

   [Serializable]
   public class Resources
   {
      public double Boranium  = 0;
      public double Ironium   = 0;
      public double Germanium = 0;
      public double Energy    = 0;


// ============================================================================
/// Default Constructor.
// ============================================================================

      public Resources() { }


      // ============================================================================
      // Initialising Constructor.
      // ============================================================================

      public Resources(int i, int b, int g, int e)
      {
          Ironium = i;
          Boranium = b;
          Germanium = g;
          Energy = e;
      }
       
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "resource" node in a Nova compenent definition file (xml document).
// ============================================================================

      public Resources(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "ironium":
                          Ironium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "boranium":
                          Boranium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "germanium":
                          Germanium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "energy":
                          Energy = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
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
// Copy constructor.
// ============================================================================

      public Resources (Resources copy)
      {
         this.Ironium   = copy.Ironium;
         this.Boranium  = copy.Boranium;
         this.Germanium = copy.Germanium;
         this.Energy    = copy.Energy;
      }


// ============================================================================
// See if a resource set is greater than another.
// ============================================================================

      public static bool operator>(Resources lhs, Resources rhs)
      {
         if (lhs.Ironium   <= rhs.Ironium   && lhs.Boranium <= rhs.Boranium &&
             lhs.Germanium <= rhs.Germanium && lhs.Energy   <= rhs.Energy) {
            return false;
         }
         return true;
      }


// ============================================================================
// See if a resources set is less than another.
// ============================================================================

      public static bool operator<(Resources lhs, Resources rhs)
      {
         if (lhs.Ironium   >= rhs.Ironium   && lhs.Boranium >= rhs.Boranium &&
             lhs.Germanium >= rhs.Germanium && lhs.Energy   >= rhs.Energy) {
            return false;
         }
         return true;
      }


// ============================================================================
// Subtract one resource set from another.
// ============================================================================

      public static Resources operator-(Resources lhs, Resources rhs)
      {
         Resources result = new Resources();

         result.Ironium   = lhs.Ironium   - rhs.Ironium;
         result.Boranium  = lhs.Boranium  - rhs.Boranium;
         result.Germanium = lhs.Germanium - rhs.Germanium;
         result.Energy    = lhs.Energy    - rhs.Energy;

         return result;
      }


// ============================================================================
/// Add a resource set to another.
// ============================================================================

      public static Resources operator+(Resources lhs, Resources rhs)
      {
         Resources result = new Resources();

         result.Ironium   = lhs.Ironium   + rhs.Ironium;
         result.Boranium  = lhs.Boranium  + rhs.Boranium;
         result.Germanium = lhs.Germanium + rhs.Germanium;
         result.Energy    = lhs.Energy    + rhs.Energy;

         return result;
      }


// ============================================================================
// Return the mass of a resource set (Energy does not contribute to the mass).
// ============================================================================

      public int Mass
      {
         get { return (int) (Ironium + Boranium + Germanium); }
      }

// ============================================================================
// Return an XmlElement representation of the resource cost
// ============================================================================
       public XmlElement ToXml(XmlDocument xmldoc)
       {
           XmlElement xmlelResource = xmldoc.CreateElement("Resource");

           // Boranium
           XmlElement xmlelBoranium = xmldoc.CreateElement("Boranium");
           XmlText xmltxtBoranium = xmldoc.CreateTextNode(this.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           xmlelBoranium.AppendChild(xmltxtBoranium);
           xmlelResource.AppendChild(xmlelBoranium);
           // Ironium
           XmlElement xmlelIronium = xmldoc.CreateElement("Ironium");
           XmlText xmltxtIronium = xmldoc.CreateTextNode(this.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           xmlelIronium.AppendChild(xmltxtIronium);
           xmlelResource.AppendChild(xmlelIronium);
           // Germanium
           XmlElement xmlelGermanium = xmldoc.CreateElement("Germanium");
           XmlText xmltxtGermanium = xmldoc.CreateTextNode(this.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           xmlelGermanium.AppendChild(xmltxtGermanium);
           xmlelResource.AppendChild(xmlelGermanium);
           // Energy
           XmlElement xmlelEnergy = xmldoc.CreateElement("Energy");
           XmlText xmltxtEnergy = xmldoc.CreateTextNode(this.Energy.ToString(System.Globalization.CultureInfo.InvariantCulture));
           xmlelEnergy.AppendChild(xmltxtEnergy);
           xmlelResource.AppendChild(xmlelEnergy);

           return xmlelResource;
       }

   }
}
