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

namespace NovaCommon
{
   [Serializable]
   public class RaceData
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

       /// <summary>
       /// Save: Generate an XmlElement representation of the RaceData
       /// </summary>
       /// <param name="xmldoc">The parent XmlDocument</param>
       /// <returns>An XmlElement reprsenting the RaceData (to be written to file)</returns>
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelRaceData = xmldoc.CreateElement("RaceData");
          Global.SaveData(xmldoc, xmlelRaceData, "TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
          foreach (string key in PlayerRelations.Keys)
          {
              XmlElement xmlelRelation = xmldoc.CreateElement("Relation");
              Global.SaveData(xmldoc, xmlelRelation, "Race", key);
              Global.SaveData(xmldoc, xmlelRelation, "Status", PlayerRelations[key] as String);
              xmlelRaceData.AppendChild(xmlelRelation);
          }
          foreach (string key in BattlePlans.Keys)
          {
              xmlelRaceData.AppendChild((BattlePlans[key] as BattlePlan).ToXml(xmldoc));
          }

          return xmlelRaceData;
      }
   }
}


