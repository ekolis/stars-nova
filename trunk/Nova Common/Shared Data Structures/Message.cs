// This file needs -*- c++ -*- mode
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

namespace NovaCommon
{


// ============================================================================
// Player messages.
// ============================================================================

   [Serializable]
   public class Message
   {
       public string Text = null; //The text to display in the message box.
       public string Audience = null; // A string representing the destination of the message. Either a race name or and asterix. 
       public Object Event = null; //An object used with the Goto button to display more information to the player. See Messages.GotoButton_Click

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
   }
}
