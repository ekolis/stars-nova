// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class defines a beam weapon component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

// ============================================================================
// Weapon class
// ============================================================================

namespace NovaCommon
{
    
    public enum WeaponType
    {
        standardBeam,
        shieldSapper,
        gatlingGun,
        torpedo,
        missile
    }
    
    
   [Serializable]
   public class Weapon : ComponentProperty
   {
      public int Power      = 0;
      public int Range      = 0;
      public int Initiative = 0;
      public int Accuracy   = 0;
      public WeaponType Group = WeaponType.standardBeam;

      // ============================================================================
      // Construction from scratch
      // ============================================================================
      public Weapon()
      {
          this.Group = WeaponType.standardBeam;
      }


      // ============================================================================
      // Construction from an ComponentProperty object
      // ============================================================================
      public Weapon(Weapon existing) 
      {
          this.Power = existing.Power;
          this.Range = existing.Range;
          this.Initiative = existing.Initiative;
          this.Accuracy = existing.Accuracy;
          this.Group = existing.Group;
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Weapon(this);
      }

      //============================================================================
      // Provide a way to add properties in the ship design.
      // only power adds, and this only makes sense if the weapons are the same
      //============================================================================
      public static Weapon operator +(Weapon op1, Weapon op2)
      {
          if (op1.Group != op2.Group)
          {
              Report.Error("Weapon.operator+ Attempted to add weapons of different Groups.");
              return op1;
          }
          Weapon sum = new Weapon(op1);
          sum.Power = op1.Power + op2.Power;
          return sum;
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      // only power adds, and this only makes sense if the weapons are the same
      //============================================================================
      public static Weapon operator *(Weapon op1, int scalar)
      {
          Weapon sum = new Weapon(op1);
          sum.Power = op1.Power * scalar;
          return sum;
      }       
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Weapon" in a Nova 
//               compenent definition file (xml document).
// ============================================================================
      public Weapon(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "power":
                          Power = int.Parse(((XmlText)subnode.FirstChild).Value);
                          break;
                      case "range":
                          Range = int.Parse(((XmlText)subnode.FirstChild).Value);
                          break;
                      case "initiative":
                          Initiative = int.Parse(((XmlText)subnode.FirstChild).Value);
                          break;
                      case "accuracy":
                          Accuracy = int.Parse(((XmlText)subnode.FirstChild).Value);
                          break;
                      case "group":
                          switch (((XmlText)subnode.FirstChild).Value.ToLower())
                          {
                              case "standardweapon":
                                  Group = WeaponType.standardBeam; break;
                              case "shieldsapper":
                                  Group = WeaponType.shieldSapper; break;
                              case "gatlinggun":
                                  Group = WeaponType.gatlingGun; break;
                              case "torpedo":
                                  Group = WeaponType.torpedo; break;
                              case "missile":
                                  Group = WeaponType.missile; break;
                          }
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
// Classify the various types as either beam or missile. Use weapon.Type to
//   determine the exact weapon type.
// ============================================================================

      public bool IsBeam
      {
          get { return (Group == WeaponType.standardBeam || Group == WeaponType.shieldSapper || Group == WeaponType.gatlingGun); }
      }

      public bool IsMissile
      {
          
                get { return (Group == WeaponType.torpedo || Group == WeaponType.missile); }
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
          // Power
          XmlElement xmlelPower = xmldoc.CreateElement("Power");
          XmlText xmltxtPower = xmldoc.CreateTextNode(this.Power.ToString());
          xmlelPower.AppendChild(xmltxtPower);
          xmlelProperty.AppendChild(xmlelPower);
          // Accuracy
          XmlElement xmlelAccuracy = xmldoc.CreateElement("Accuracy");
          XmlText xmltxtAccuracy = xmldoc.CreateTextNode(this.Accuracy.ToString());
          xmlelAccuracy.AppendChild(xmltxtAccuracy);
          xmlelProperty.AppendChild(xmlelAccuracy);
          // Range
          XmlElement xmlelRange = xmldoc.CreateElement("Range");
          XmlText xmltxtRange = xmldoc.CreateTextNode(this.Range.ToString());
          xmlelRange.AppendChild(xmltxtRange);
          xmlelProperty.AppendChild(xmlelRange);
          // Group
          XmlElement xmlelGroup = xmldoc.CreateElement("Group");
          XmlText xmltxtGroup = xmldoc.CreateTextNode(this.Group.ToString());
          xmlelGroup.AppendChild(xmltxtGroup);
          xmlelProperty.AppendChild(xmlelGroup);


          return xmlelProperty;
      }       
   }
}

