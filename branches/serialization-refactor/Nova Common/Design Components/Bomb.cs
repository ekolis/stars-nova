// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class defines a bomb component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Xml.Schema;

// ============================================================================
// Bomb class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class Bomb : ComponentProperty
   {
      public int    Installations  = 0;
      public double PopKill        = 0;
      public int    MinimumKill    = 0;
      public bool   IsSmart        = false;


// ============================================================================
// Construction from scratch
// ============================================================================

      public Bomb()
      {
      }


// ============================================================================
// Construction from an Component object
// ============================================================================

      public Bomb(Bomb existing) 
      {
          this.Installations = existing.Installations;
          this.PopKill = existing.PopKill;
          this.MinimumKill = existing.MinimumKill;
          this.IsSmart = existing.IsSmart;
      }

      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Bomb(this);
      }

       //============================================================================
      // Provide a way to add properties in the ship design.
      // Bombs do add, but you can't and smart and normal bombs and get a value 
      // that can be represented by one Bomb property.
      //============================================================================
      public static Bomb operator +(Bomb op1, Bomb op2)
      {
          if (op1.IsSmart != op2.IsSmart)
          {
              Report.Error("Bomb.operator+ attempted to combine conventional and smart bombs.");
              return op1;
          }

          Bomb sum = new Bomb(op1);
          if (sum.IsSmart)
          {
              sum.Installations = 0;
              sum.MinimumKill = 0;
              sum.PopKill = (1 - ((1 - op1.PopKill) * (1 - op2.PopKill)));
          }
          else
          {
              sum.Installations = op1.Installations + op2.Installations;
              sum.MinimumKill = op1.MinimumKill + op2.MinimumKill;
              sum.PopKill = op1.PopKill + op2.PopKill;
          }

          return sum;
      }


      //============================================================================
      /// <summary>
      /// Provide a way to scale (multiply) bombs.
      /// </summary><param name="bomb">
      /// The bomb property to be scaled (multiplied).
      /// </param> <param name="bombCount">
      /// The scalar (number of bombs) to multiply by.
      /// </param>
      //============================================================================
      public static Bomb operator *(Bomb bomb, int bombCount)
      {
          Bomb sum = new Bomb(bomb);
          if (sum.IsSmart)
          {
              sum.Installations = 0;
              sum.MinimumKill = 0;
              sum.PopKill = (1 - ( Math.Pow(1 - bomb.PopKill, bombCount) ));
          }
          else
          {
              sum.Installations = bomb.Installations * bombCount;
              sum.MinimumKill = bomb.MinimumKill * bombCount;
              sum.PopKill = bomb.PopKill * bombCount;
          }

          return sum;
      }

      /// <summary>
      /// Initialising Constructor from an xml node.
      /// </summary>
      /// <param name="node">
      /// node is a "Property" node with Type=="Bomb" in a Nova 
      /// compenent definition file (xml document).</param>
      public Bomb(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "installations")
                  {
                      Installations = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "popkill")
                  {
                      PopKill = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "minimumkill")
                  {
                      MinimumKill = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "issmart")
                  {
                      IsSmart = bool.Parse(((XmlText)subnode.FirstChild).Value);
                  }
              }
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
          }
      }

      public override void ReadXml(XmlReader reader)
      {
          throw new NotImplementedException(); // TODO XML deserialization of Bomb
      }

      public override void WriteXml(XmlWriter writer)
      {
          writer.WriteElementString("Installations", Installations.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("PopKill", PopKill.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("MinimumKill", MinimumKill.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("IsSmart", IsSmart.ToString(System.Globalization.CultureInfo.InvariantCulture));
      }
   }
}

