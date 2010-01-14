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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{
   [Serializable]
   public class BattlePlan : IXmlSerializable
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
       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of BattlePlan
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("BattlePlan");

           writer.WriteElementString("Name", Name);
           writer.WriteElementString("PrimaryTarget", PrimaryTarget);
           writer.WriteElementString("SecondaryTarget", SecondaryTarget);
           writer.WriteElementString("Tactic", Tactic);
           writer.WriteElementString("Attack", Attack);

           writer.WriteEndElement();
       }
   }
}
