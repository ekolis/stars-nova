// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This file defines the hull component. Always take a copy of a hull before
// populating it in the ship designer (otherwise the "master" version will end
// up getting modified).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.Serialization;

namespace NovaCommon
{


// ============================================================================
// The definition of a hull object.
// ============================================================================

   [Serializable]
   public class Hull : ComponentProperty
   {
      // Note: several hull properties _could_ be made by adding other properties
      // e.g. fuel / armor. However as all hulls (or many in the case of cargo)
      // have these properties it improves
      // the interface to include them here. Values supplied in additional property
      // tabs in the component edditor will be in addition to those in the base hull.
      public ArrayList Modules          = null;
      public int       FuelCapacity     = 0;
      public int       DockCapacity     = 0;
      public int       BaseCargo        = 0; // Basic Cargo capacity of the empty hull (no pods)
      public int       ARMaxPop         = 0;
      public int       ArmorStrength    = 0;
      public int       BattleInitiative = 0;



// ============================================================================
// Construction
// ============================================================================

      public Hull ()
      {
         
      }


// ============================================================================
// Copy constructor for the hull itself
// ============================================================================

      public Hull(Hull existing) 
      {
         
         FuelCapacity     = existing.FuelCapacity;
         DockCapacity     = existing.DockCapacity;
         BaseCargo        = existing.BaseCargo;
         ARMaxPop         = existing.ARMaxPop;
         ArmorStrength   = existing.ArmorStrength;
         BattleInitiative = existing.BattleInitiative;
         

         Modules = new ArrayList();

         foreach (HullModule module in existing.Modules) {
            Modules.Add(module.Clone());
         }
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Hull(this);
      }

      //============================================================================
      // Provide a way to add properties in the ship design.
      // Has no meaning in the context of a Hull.
      //============================================================================
      public static Hull operator +(Hull op1, Hull op2)
      {
          return op1;
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      // Has no meaning in the context of a Hull.
      //============================================================================
      public static Hull operator *(Hull op1, int scalar)
      {
          return op1;
      }

      // ============================================================================
      // Load: Initialising Constructor from an xml node.
      // Precondition: node is a "Property" node with Type=="Hull" in a Nova 
      //               compenent definition file (xml document).
      // ============================================================================
      public Hull(XmlNode node)
      {
          Modules = new ArrayList();
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "fuelcapacity")
                  {
                      FuelCapacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "dockcapacity")
                  {
                      DockCapacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "basecargo")
                  {
                      BaseCargo = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "armaxpop")
                  {
                      ARMaxPop = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "armorstrength")
                  {
                      ArmorStrength = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "battleinitiative")
                  {
                      BattleInitiative = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  else if (subnode.Name.ToLower() == "module")
                  {
                      HullModule module = new HullModule(subnode);
                      Modules.Add(module);
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
// Determine if this is a starbase hull
// ============================================================================

      public bool IsStarbase
      {
         get { return (FuelCapacity == 0); }
      }

// ============================================================================
// Determine if this is a starbase that can refuel
// ============================================================================

      public bool CanRefuel
      {
          get { return (FuelCapacity == 0 && DockCapacity > 0); }
      }



// ============================================================================
// Save: Return an XmlElement representation of the Property
// ============================================================================
      public override XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelProperty = xmldoc.CreateElement("Property");

          // FuelCapacity
          XmlElement xmlelFuelCapacity = xmldoc.CreateElement("FuelCapacity");
          XmlText xmltxtFuelCapacity = xmldoc.CreateTextNode(this.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelFuelCapacity.AppendChild(xmltxtFuelCapacity);
          xmlelProperty.AppendChild(xmlelFuelCapacity);
          // DockCapacity
          XmlElement xmlelDockCapacity = xmldoc.CreateElement("DockCapacity");
          XmlText xmltxtDockCapacity = xmldoc.CreateTextNode(this.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelDockCapacity.AppendChild(xmltxtDockCapacity);
          xmlelProperty.AppendChild(xmlelDockCapacity);
          // ARMaxPop
          XmlElement xmlelARMaxPop = xmldoc.CreateElement("ARMaxPop");
          XmlText xmltxtARMaxPop = xmldoc.CreateTextNode(this.ARMaxPop.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelARMaxPop.AppendChild(xmltxtARMaxPop);
          xmlelProperty.AppendChild(xmlelARMaxPop);
          // BaseCargo
          XmlElement xmlelBaseCargo = xmldoc.CreateElement("BaseCargo");
          XmlText xmltxtBaseCargo = xmldoc.CreateTextNode(this.BaseCargo.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelBaseCargo.AppendChild(xmltxtBaseCargo);
          xmlelProperty.AppendChild(xmlelBaseCargo);
          // ArmorStrength
          XmlElement xmlelArmorStrength = xmldoc.CreateElement("ArmorStrength");
          XmlText xmltxtArmorStrength = xmldoc.CreateTextNode(this.ArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelArmorStrength.AppendChild(xmltxtArmorStrength);
          xmlelProperty.AppendChild(xmlelArmorStrength);
          // BattleInitiative
          XmlElement xmlelBattleInitiative = xmldoc.CreateElement("BattleInitiative");
          XmlText xmltxtBattleInitiative = xmldoc.CreateTextNode(this.BattleInitiative.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelBattleInitiative.AppendChild(xmltxtBattleInitiative);
          xmlelProperty.AppendChild(xmlelBattleInitiative);
          // Modules
          foreach (HullModule module in this.Modules)
          {
              xmlelProperty.AppendChild(module.ToXml(xmldoc));
          }

          return xmlelProperty;
      }
   }
}

