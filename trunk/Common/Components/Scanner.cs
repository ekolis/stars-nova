#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This class adds the properties that are specific to ship-based scanners.
// ===========================================================================
#endregion

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

// ============================================================================
// Return an XmlElement representation of the Property
// ============================================================================
      public override XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelProperty = xmldoc.CreateElement("Property");

          // PenetratingScan
          XmlElement xmlelPenetratingScan = xmldoc.CreateElement("PenetratingScan");
          XmlText xmltxtPenetratingScan = xmldoc.CreateTextNode(this.PenetratingScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelPenetratingScan.AppendChild(xmltxtPenetratingScan);
          xmlelProperty.AppendChild(xmlelPenetratingScan);
          // NormalScan
          XmlElement xmlelNormalScan = xmldoc.CreateElement("NormalScan");
          XmlText xmltxtNormalScan = xmldoc.CreateTextNode(this.NormalScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelNormalScan.AppendChild(xmltxtNormalScan);
          xmlelProperty.AppendChild(xmlelNormalScan);

          return xmlelProperty;
      }

   }
}
