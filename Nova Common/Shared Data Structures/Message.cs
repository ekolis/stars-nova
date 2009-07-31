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
      public string Text      = null;
      public string Audience  = null;
      public Object Event     = null; 
   }
}
