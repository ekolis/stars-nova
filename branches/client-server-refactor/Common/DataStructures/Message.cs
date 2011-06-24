#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
// This class defines the format of messages sent to one or more players.
// ===========================================================================
#endregion

namespace Nova.Common
{
    #region Using Statements
    using System;
    using System.Xml;
    using Nova.Common.DataStructures;
    #endregion

    /// <summary>
    /// Player messages.
    /// </summary>
    [Serializable]
    public class Message
    {
        public string Text;      // The text to display in the message box.
        public string Audience;  // A string representing the destination of the message. Either a race name or an asterix. 
        public string Type;      // Text that indicates the type of event that generated the message.
        public object Event;     // An object used with the Goto button to display more information to the player. See Messages.GotoButton_Click
        // Ensure when adding new message types to add code to the Xml functions to handle your object type.

        #region Construction

        /// <summary>
        /// default constructor
        /// </summary>
        public Message() 
        { 
        }

        /// <summary>
        /// Initialising constructor
        /// </summary>
        /// <param name="audience">A string representing the destination of the message. Either a race name or and asterix.</param>
        /// <param name="messageEvent">An object used with the Goto button to display more information to the player. See Messages.GotoButton_Click</param>
        /// <param name="text">The text to display in the message box.</param>
        public Message(string audience, string text, string messageType, object messageEvent)
        {
            Audience = audience;
            Text     = text;
            Type     = messageType;
            Event    = messageEvent;
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load: Initialising constructor to read in a Star from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> representing a Star..
        /// </param>
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
                            if (subnode.FirstChild != null)
                            {
                                Text = ((XmlText)subnode.FirstChild).Value;
                            }
                            break;
                        case "audience":
                            if (subnode.FirstChild != null)
                            {
                                Audience = ((XmlText)subnode.FirstChild).Value;
                            }
                            break;
                        case "type":
                            if (subnode.FirstChild != null)
                            {
                                Type = subnode.FirstChild.Value;
                            }
                            break;
                        case "event":
                            Event = (object)subnode.FirstChild.Value;
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

        /// <summary>
        /// Save: Serialise this Message to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Message</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelMessage = xmldoc.CreateElement("Message");
            if (Text != null)
            {
                Global.SaveData(xmldoc, xmlelMessage, "Text", Text);
            }
            if (Audience != null)
            {
                Global.SaveData(xmldoc, xmlelMessage, "Audience", Audience);
            }
            if (Type != null)
            {
                Global.SaveData(xmldoc, xmlelMessage, "Type", Type);
            }

            
            if (Event != null)
            {
                switch (Type)
                {
                    case "TechAdvance":
                    case "NewComponent":
                        // No object reference required to be saved.
                        break;

                    case "Minefield":
                        Global.SaveData(xmldoc, xmlelMessage, "Event", ((Minefield)Event).Key);
                        break;

                    case "BattleReport":
                        Global.SaveData(xmldoc, xmlelMessage, "Event", ((BattleReport)Event).Key);
                        break;

                    default:
                        Report.Error("Message.ToXml() - Unable to convert Message.Event of type " + Event.ToString());
                        break;
                }
            }

            return xmlelMessage;
        }

        #endregion
    }
}