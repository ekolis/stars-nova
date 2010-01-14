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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{


// ============================================================================
// Resource class. Individual resource values are either kT (minerals on hand)
// or percent (mineral concentrations).
// ============================================================================

   [Serializable]
   public class Resources : IXmlSerializable
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
                          Ironium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "boranium":
                          Boranium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "germanium":
                          Germanium = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "energy":
                          Energy = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                  }
              }
              catch (Exception e)
              {
                  Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
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

       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Resources
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Resource");

           writer.WriteElementString("Boranium", Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("Ironium", Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("Germanium", Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("Energy", Energy.ToString(System.Globalization.CultureInfo.InvariantCulture));

           writer.WriteEndElement();
       }
   }
}
