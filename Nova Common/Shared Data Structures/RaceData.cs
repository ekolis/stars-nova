// This file needs -*- c++ -*- mode
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
   }
}


