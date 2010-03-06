// ===========================================================================
// Nova. (c) 2008 Ken Reed
//
// This class defines the format of messages sent to one or more players.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.Xml;

namespace NovaCommon
{


// ============================================================================
// Player messages.
// ============================================================================

   [Serializable]
   public class Message
   {
       public string Text = null; // The text to display in the message box.
       public string Audience = null; // A string representing the destination of the message. Either a race name or and asterix. 
       public Object Event = null; // An object used with the Goto button to display more information to the player. See Messages.GotoButton_Click
                                   // Ensure when adding new message types to add code to the Xml functions to handle your object type.

       /// <summary>
       /// default constructor
       /// </summary>
       public Message() { }

       /// <summary>
       /// Initialising constructor
       /// </summary>
       /// <param name="audience">A string representing the destination of the message. Either a race name or and asterix.</param>
       /// <param name="messageEvent">An object used with the Goto button to display more information to the player. See Messages.GotoButton_Click</param>
       /// <param name="text">The text to display in the message box.</param>
       public Message(string audience, Object messageEvent, string text)
       {
           Audience = audience;
           Event = messageEvent;
           Text = text;

       }

      /// <summary>
      /// Load: Initialising constructor to read in a Star from an XmlNode (from a saved file).
      /// </summary>
      /// <param name="node">An XmlNode representing a Star.</param>
      public Message(XmlNode node) 
      {

          XmlNode subnode = node.FirstChild;

          // Read the node
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "text": 
                          Text = ((XmlText)subnode.FirstChild).Value; 
                          break;
                      case "audience":
                          Audience = ((XmlText)subnode.FirstChild).Value; 
                          break;
                      case "BattleReport":
                          // TODO (priority 5) - when messages are being saved as xml, this will need to be converted into a link to the actual battle report.
                          Event = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "Minefield":
                          Event = ((XmlText)subnode.FirstChild).Value; 
                          break;
                     
                      default: break;
                  }
              }
              catch (Exception e)
              {
                  Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
              }
              subnode = subnode.NextSibling;
          }
      }

       public XmlElement ToXml(XmlDocument xmldoc)
       {
           XmlElement xmlelMessage = xmldoc.CreateElement("Message");
           Global.SaveData(xmldoc, xmlelMessage, "Text", Text);
           Global.SaveData(xmldoc, xmlelMessage, "Audience", Audience);
           
           if (Event != null)
           {
               if (Event is BattleReport)
               {
                   // save just a reference to the full report
                   BattleReport battle = Event as BattleReport;
                   if (battle.Location != null)
                       Global.SaveData(xmldoc, xmlelMessage, "BattleReport", battle.Location);
               }
               if (Event is string)
               {
                   string eventString = Event as string;
                   if (eventString == "Minefield")
                   {
                       Global.SaveData(xmldoc, xmlelMessage, "Minefield", "Minefield");
                   }
               }
               else
               {
                   Report.Error("Message.ToXml() - Unable to convert Message.Event of type " + Event.ToString());
               }
           }

           return xmlelMessage;

       }

   }
}
