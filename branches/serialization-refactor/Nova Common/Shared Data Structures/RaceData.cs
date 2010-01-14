// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Race specific data that may change from year-to-year that must be passed to
// the Nova console.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{
   [Serializable]
   public class RaceData : IXmlSerializable
   {
      public int       TurnYear          = 0;
      public Hashtable PlayerRelations   = new Hashtable();
      public Hashtable BattlePlans       = new Hashtable();

       /// <summary>
       /// default constructor
       /// </summary>
      public RaceData() { }

       /// <summary>
       /// Load: constructor to load RaceData from an XmlNode representation.
       /// </summary>
       /// <param name="xmldoc">An XmlNode containing a RaceData representation (from a save file)</param>
      public RaceData(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "turnyear":
                          TurnYear = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "relation":
                          {
                              string key = ((XmlText)subnode.SelectSingleNode("Race").FirstChild).Value;
                              string value = ((XmlText)subnode.SelectSingleNode("Status").FirstChild).Value;
                              PlayerRelations.Add(key, value);
                              break;
                          }
                      case "battleplan":
                          BattlePlan plan = new BattlePlan(subnode);
                          BattlePlans.Add(plan.Name, plan);
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

       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of RaceData
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("RaceData");

           writer.WriteElementString("TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
           foreach (string key in PlayerRelations.Keys)
           {
               writer.WriteStartElement("Relation");
               writer.WriteElementString("Race", key);
               writer.WriteElementString("Status", PlayerRelations[key] as String);
               writer.WriteEndElement();
           }
           foreach (string key in BattlePlans.Keys)
           {
               (BattlePlans[key] as BattlePlan).WriteXml(writer);
           }

           writer.WriteEndElement();
       }
   }
}


