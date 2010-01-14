// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class adds the properties that are specific to ship-based scanners.
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

namespace NovaCommon
{

// ============================================================================
// Scanner class.
// ============================================================================

   [Serializable]
   public class Scanner : ComponentProperty
   {
      public int PenetratingScan = 0;
      public int NormalScan  = 0;


// ============================================================================
// Construction
// ============================================================================

      public Scanner ()
      {

      }


// ============================================================================
// Construction from an Component object
// ============================================================================

      public Scanner(Scanner existing)
      {
          this.PenetratingScan = existing.PenetratingScan;
          this.NormalScan = existing.NormalScan;
      }
      //============================================================================
      // Implement the ICloneable interface so properties can be cloned.
      //============================================================================
      public override object Clone()
      {
          return new Scanner(this);
      }

       //============================================================================
      // Provide a way to add properties in the ship design.
      //============================================================================
      public static Scanner operator +(Scanner op1, Scanner op2)
      {
          Scanner sum = new Scanner(op1);
          // 4th root of the sum of 4th powers (Manual section 9-7)
          sum.NormalScan = (int)Math.Pow(Math.Pow(op1.NormalScan, 4) + Math.Pow(op2.NormalScan, 4), 0.25);
          sum.PenetratingScan = (int) Math.Pow(Math.Pow(op1.PenetratingScan, 4) + Math.Pow(op2.PenetratingScan, 4), 0.25);
          return sum;
      }

      //============================================================================
      // Operator* to scale (multiply) properties in the ship design.
      //============================================================================
      public static Scanner operator *(Scanner op1, int scalar)
      {
          Scanner sum = new Scanner(op1);
          // 4th root of the sum of 4th powers (Manual section 9-7)
          sum.NormalScan = (int)Math.Pow(Math.Pow(op1.NormalScan, 4) * scalar, 0.25);
          sum.PenetratingScan = (int)Math.Pow(Math.Pow(op1.PenetratingScan, 4) * scalar, 0.25);
          return sum;
      }
// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "Property" node with Type=="Scanner" in a Nova 
//               compenent definition file (xml document).
// ============================================================================
      public Scanner(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  if (subnode.Name.ToLower() == "penetratingscan")
                  {
                      PenetratingScan = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                  }
                  if (subnode.Name.ToLower() == "normalscan")
                  {
                      NormalScan = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
          throw new NotImplementedException(); // TODO XML deserialization of Scanner
      }

      public override void WriteXml(XmlWriter writer)
      {
          writer.WriteElementString("PenetratingScan", PenetratingScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
          writer.WriteElementString("NormalScan", NormalScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
      }

   }
}
