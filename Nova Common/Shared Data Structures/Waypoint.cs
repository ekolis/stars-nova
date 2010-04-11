// ============================================================================
// Nova. (c) 20056 Ken Reed
//
// The details of an individual Waypoint.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Runtime.Serialization;

namespace NovaCommon
{


// ============================================================================
// Waypoints have a position (i.e. where to go), a destination description
// (e.g. a star name), a speed to go there and a task to do on arrival (e.g. 
// colonise).
// ============================================================================

   [Serializable]
   public class Waypoint
   {
      public Point  Position;
      public string Destination = null;
      public int    WarpFactor  = 6;
      public String Task        = "None";

      public Waypoint()
      {
      }
      // ============================================================================
      // Load: Initialising Constructor from an xml node.
      // Precondition: node is a "Waypoint" node Nova save file (xml document).
      // ============================================================================
      public Waypoint(XmlNode node)
      {
          XmlSerializer x = new XmlSerializer(typeof(Waypoint));
          
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "destination":
                          Destination = ((XmlText)subnode.FirstChild).Value;
                          break;

                      case "task":
                          Task = ((XmlText)subnode.FirstChild).Value;
                          break;

                      case "warpfactor":
                          WarpFactor = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;

                      case "position":
                          Position.X = int.Parse(((subnode.SelectSingleNode("X")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          Position.Y = int.Parse(((subnode.SelectSingleNode("Y")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
      // Save: Return an XmlElement representation of the Waypoint
      // ============================================================================
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelWaypoint = xmldoc.CreateElement("Waypoint");

          NovaCommon.Global.SaveData(xmldoc, xmlelWaypoint, "Destination", this.Destination);
          NovaCommon.Global.SaveData(xmldoc, xmlelWaypoint, "Task", this.Task);
          NovaCommon.Global.SaveData(xmldoc, xmlelWaypoint, "WarpFactor", this.WarpFactor.ToString(System.Globalization.CultureInfo.InvariantCulture));
          // point
          if (Position.X != 0 || Position.Y != 0)
          {
              XmlElement xmlelPoint = xmldoc.CreateElement("Position");
              Global.SaveData(xmldoc, xmlelPoint, "X", Position.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
              Global.SaveData(xmldoc, xmlelPoint, "Y", Position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
              xmlelWaypoint.AppendChild(xmlelPoint);
          }


          return xmlelWaypoint;
      }

   }
}