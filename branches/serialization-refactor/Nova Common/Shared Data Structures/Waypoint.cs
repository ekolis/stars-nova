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
using System.Xml.Schema;
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
   public class Waypoint : IXmlSerializable
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

                      case "point":
                          Position.X = int.Parse((((XmlText)subnode.SelectSingleNode("X")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          Position.Y = int.Parse((((XmlText)subnode.SelectSingleNode("Y")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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

       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Waypoint
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Waypoint");

           writer.WriteElementString("Destination", Destination);
           writer.WriteElementString("Task", Task);
           writer.WriteElementString("WarpFactor", WarpFactor.ToString(System.Globalization.CultureInfo.InvariantCulture));

           writer.WriteEndElement();
       }
   }
}
