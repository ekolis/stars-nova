// ============================================================================
// Nova. (c) 2008 Ken Reed, 2010 Daniel Vale
//
// Definition of a battle plan.
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
   public class BattlePlan
   {
      public string Name            = "Default";
      public string PrimaryTarget   = "Armed Ships";
      public string SecondaryTarget = "Any";
      public string Tactic          = "Maximise Damage";
      public string Attack          = "Enemies";


       /// <summary>
       /// default constructor
       /// </summary>
      public BattlePlan() { }

       /// <summary>
       /// Load: Initialising constructor from an XmlNode
       /// </summary>
       /// <param name="node">An XmlNode representing a BattlePlan</param>
      public BattlePlan(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "name":
                          Name = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "primarytarget":
                          PrimaryTarget = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "secondarytarget":
                          SecondaryTarget = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "tactic":
                          Tactic = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "attack":
                          Attack = ((XmlText)subnode.FirstChild).Value;
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
       /// Save: Generate an XmlElement representation of a battle plan for saving.
       /// </summary>
       /// <param name="xmldoc">The parent XmlDocument</param>
       /// <returns>An XmlElement representaion of the BattlePlan</returns>
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelBattlePlan = xmldoc.CreateElement("BattlePlan");

          Global.SaveData(xmldoc, xmlelBattlePlan, "Name", Name);
          Global.SaveData(xmldoc, xmlelBattlePlan, "PrimaryTarget", PrimaryTarget);
          Global.SaveData(xmldoc, xmlelBattlePlan, "SecondaryTarget", SecondaryTarget);
          Global.SaveData(xmldoc, xmlelBattlePlan, "Tactic", Tactic);
          Global.SaveData(xmldoc, xmlelBattlePlan, "Attack", Attack);

          return xmlelBattlePlan;
      }
   }
}
