// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Cargo that may be carried by a ship (if it has a cargo pod).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;

// ============================================================================
// Cargo class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class Cargo
   {
      public int Ironium   = 0;
      public int Boranium  = 0;
      public int Germanium = 0;
      public int Colonists = 0;


// ============================================================================
// Defualt constructor (needed if there is a copy constructor).
// ============================================================================

     public Cargo() { }


// ============================================================================
// Copy Constructor
// ============================================================================

      public Cargo(Cargo copy)
      {
         this.Ironium   = copy.Ironium;
         this.Boranium  = copy.Boranium;
         this.Germanium = copy.Germanium;
         this.Colonists = copy.Colonists;
      }


// ============================================================================
// Return the Mass of the cargo
// ============================================================================
      
      public int Mass {
         get { return Ironium + Boranium + Germanium + Colonists; }
      }

      // ============================================================================
      // Load: Initialising Constructor from an xml node.
      // Precondition: node is a "Cargo" node Nova save file (xml document).
      // ============================================================================
      public Cargo(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {

                  switch (subnode.Name.ToLower())
                  {
                      case "ironium":
                          {
                              Ironium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                              break;
                          }
                      case "boranium":
                          {
                              Boranium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                              break;
                          }
                      case "germanium":
                          {
                              Germanium = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                              break;
                          }
                      case "colonists":
                          {
                              Colonists = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
      // Save: Return an XmlElement representation of the Cargo
      // ============================================================================
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelCargo = xmldoc.CreateElement("Cargo");

          NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Ironium", this.Ironium.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Boranium", this.Boranium.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Germanium", this.Germanium.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelCargo, "Colonists", this.Colonists.ToString());

          return xmlelCargo;
      }
 

   }
}
