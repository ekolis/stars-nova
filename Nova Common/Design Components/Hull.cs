// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
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
// The definition of the individual modules that make up a hull.
// These are the slots which define what components may be fitted.
// ============================================================================

   [Serializable]
   public class HullModule : ICloneable
   {
      public Component AllocatedComponent  = null;
      public Image     ComponentImage      = null;
      public int       CellNumber          = -1;
      public int       ComponentCount      = 0;
      public int       ComponentMaximum    = 1;
      public string    ComponentType       = null;

// ============================================================================
// Construction
// ============================================================================
      
      public HullModule()
      {
      }


      // ============================================================================
      // Copy constructor 
      // ============================================================================

      public HullModule(HullModule existing)
      {
         AllocatedComponent = existing.AllocatedComponent;
         CellNumber         = existing.CellNumber;
         ComponentCount     = existing.ComponentCount;
         ComponentImage     = existing.ComponentImage;
         ComponentMaximum   = existing.ComponentMaximum;
         ComponentType      = existing.ComponentType;

      }

      //============================================================================
      // Implement the ICloneable interface so modules can be cloned.
      //============================================================================
      public object Clone()
      {
          return new HullModule(this);
      }

      // ============================================================================
      // Initialising Constructor from an xml node.
      // Precondition: node is named "Module" within a "Property" node with Type=="Hull" 
      //               in a Nova compenent definition file (xml document).
      // ============================================================================
      public HullModule(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "cellnumber")
                  {
                      CellNumber = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "componentmaximum")
                  {
                      ComponentMaximum = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "componentcount")
                  {
                      ComponentCount = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "componenttype")
                  {
                      ComponentType = ((XmlText)subnode.FirstChild).Value;
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
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelModule = xmldoc.CreateElement("Module");

          // CellNumber
          XmlElement xmlelCellNumber = xmldoc.CreateElement("CellNumber");
          XmlText xmltxtCellNumber = xmldoc.CreateTextNode(this.CellNumber.ToString());
          xmlelCellNumber.AppendChild(xmltxtCellNumber);
          xmlelModule.AppendChild(xmlelCellNumber);
          // ComponentCount
          XmlElement xmlelComponentCount = xmldoc.CreateElement("ComponentCount");
          XmlText xmltxtComponentCount = xmldoc.CreateTextNode(this.ComponentCount.ToString());
          xmlelComponentCount.AppendChild(xmltxtComponentCount);
          xmlelModule.AppendChild(xmlelComponentCount);
          // ComponentMaximum
          XmlElement xmlelComponentMaximum = xmldoc.CreateElement("ComponentMaximum");
          XmlText xmltxtComponentMaximum = xmldoc.CreateTextNode(this.ComponentMaximum.ToString());
          xmlelComponentMaximum.AppendChild(xmltxtComponentMaximum);
          xmlelModule.AppendChild(xmlelComponentMaximum);
          // ComponentType
          XmlElement xmlelComponentType = xmldoc.CreateElement("ComponentType");
          XmlText xmltxtComponentType = xmldoc.CreateTextNode(this.ComponentType);
          xmlelComponentType.AppendChild(xmltxtComponentType);
          xmlelModule.AppendChild(xmlelComponentType);



          return xmlelModule;
      }
   }
   

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
                      FuelCapacity = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "dockcapacity")
                  {
                      DockCapacity = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "basecargo")
                  {
                      BaseCargo = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "armaxpop")
                  {
                      ARMaxPop = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "armorstrength")
                  {
                      ArmorStrength = int.Parse(((XmlText)subnode.FirstChild).Value);
                  }
                  else if (subnode.Name.ToLower() == "battleinitiative")
                  {
                      BattleInitiative = int.Parse(((XmlText)subnode.FirstChild).Value);
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
          XmlText xmltxtFuelCapacity = xmldoc.CreateTextNode(this.FuelCapacity.ToString());
          xmlelFuelCapacity.AppendChild(xmltxtFuelCapacity);
          xmlelProperty.AppendChild(xmlelFuelCapacity);
          // DockCapacity
          XmlElement xmlelDockCapacity = xmldoc.CreateElement("DockCapacity");
          XmlText xmltxtDockCapacity = xmldoc.CreateTextNode(this.DockCapacity.ToString());
          xmlelDockCapacity.AppendChild(xmltxtDockCapacity);
          xmlelProperty.AppendChild(xmlelDockCapacity);
          // ARMaxPop
          XmlElement xmlelARMaxPop = xmldoc.CreateElement("ARMaxPop");
          XmlText xmltxtARMaxPop = xmldoc.CreateTextNode(this.ARMaxPop.ToString());
          xmlelARMaxPop.AppendChild(xmltxtARMaxPop);
          xmlelProperty.AppendChild(xmlelARMaxPop);
          // BaseCargo
          XmlElement xmlelBaseCargo = xmldoc.CreateElement("BaseCargo");
          XmlText xmltxtBaseCargo = xmldoc.CreateTextNode(this.BaseCargo.ToString());
          xmlelBaseCargo.AppendChild(xmltxtBaseCargo);
          xmlelProperty.AppendChild(xmlelBaseCargo);
          // ArmorStrength
          XmlElement xmlelArmorStrength = xmldoc.CreateElement("ArmorStrength");
          XmlText xmltxtArmorStrength = xmldoc.CreateTextNode(this.ArmorStrength.ToString());
          xmlelArmorStrength.AppendChild(xmltxtArmorStrength);
          xmlelProperty.AppendChild(xmlelArmorStrength);
          // BattleInitiative
          XmlElement xmlelBattleInitiative = xmldoc.CreateElement("BattleInitiative");
          XmlText xmltxtBattleInitiative = xmldoc.CreateTextNode(this.BattleInitiative.ToString());
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

