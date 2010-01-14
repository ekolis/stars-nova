// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale 2009
//
// This class defines a mine-layer property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;

// ============================================================================
// MineLayer property class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class MineLayer : ComponentProperty
   {
       // Only three types of mine layers are recognised, and they are distinguised by their HitChance:
      public static double HeavyHitChance = 1.0;
      public static double SpeedTrapHitChance = 3.5;
      public static double StandardHitChance = 0.3;

       // Standar Mine Stats
      public int      LayerRate          = 50;
      public int      SafeSpeed          = 4;
      public double   HitChance          = 0.3;
      public int      DamagePerEngine    = 100;
      public int      DamagePerRamScoop  = 125;
      public int      MinFleetDamage     = 500;
      public int      MinRamScoopDamage  = 600;


// ============================================================================
// Construction from scratch
// ============================================================================

      public MineLayer()
      {
         
      }


// ============================================================================
// Construction from an Component object
// ============================================================================

      public MineLayer(MineLayer existing)
      {
          this.LayerRate = existing.LayerRate;
          this.SafeSpeed = existing.SafeSpeed;
          this.HitChance = existing.HitChance;
          this.DamagePerEngine = existing.DamagePerEngine;
          this.DamagePerRamScoop = existing.DamagePerRamScoop;
          this.MinFleetDamage = existing.MinFleetDamage;
          this.MinRamScoopDamage = existing.MinRamScoopDamage;

      }
      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new MineLayer(this);
      }

       //============================================================================
      // Provide a way to add properties in the ship design.
      // Only laying rates sum up, and this only makes sense if the mine layers produce
      // the same mines. For Stars! like mines they are the same if HitChance is the 
      // same
      //============================================================================
      public static MineLayer operator +(MineLayer op1, MineLayer op2)
      {
          if (op1.HitChance != op2.HitChance)
          {
              Report.Error("MineLayer.operator+ Attempted to add together differnet types of mine layers.");
              return op1;
          }
          MineLayer sum = new MineLayer(op1);
          sum.LayerRate = op1.LayerRate + op2.LayerRate;
          return sum;
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      // Only laying rates scale, and this only makes sense if the mine layers produce
      // the same mines. For Stars! like mines they are the same if HitChance is the 
      // same
      //============================================================================
      public static MineLayer operator *(MineLayer op1, int scalar)
      {
          MineLayer sum = new MineLayer(op1);
          sum.LayerRate = op1.LayerRate * scalar;
          return sum;
      }
       
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="MineLayer" in a Nova 
//               compenent definition file (xml document).
// ============================================================================
      public MineLayer(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "layerrate")
                  {
                      LayerRate = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "safespeed")
                  {
                      SafeSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "hitchance")
                  {
                      HitChance = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "damageperengine")
                  {
                      DamagePerEngine = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "damageperramscoop")
                  {
                      DamagePerRamScoop = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "minfleetdamage")
                  {
                      MinFleetDamage = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "minramscoopdamage")
                  {
                      MinRamScoopDamage = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
          throw new NotImplementedException(); // TODO XML deserialization of MineLayer
      }

      public override void WriteXml(XmlWriter writer)
      {
          writer.WriteElementString("LayerRate", LayerRate.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("SafeSpeed", SafeSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("HitChance", HitChance.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("DamagePerEngine", DamagePerEngine.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("DamagePerRamScoop", DamagePerRamScoop.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("MinFleetDamage", MinFleetDamage.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("MinRamScoopDamage", MinRamScoopDamage.ToString(System.Globalization.CultureInfo.InvariantCulture));
      }
   }
}

