// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Class for a star's production queue.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;

namespace NovaCommon
{

   [Serializable]
   public class ProductionQueue
   {

// ============================================================================
// Details of a design in the queue.
// ============================================================================

      [Serializable]
      public class Item
      {
         public string    Name;              // Design name, e.g. "Space Dock"
         public int       Quantity;          // Number to build
         public Resources BuildState;        // Resources need to build item
      }      


// ============================================================================
// The production queue itself.
// ============================================================================

      public ArrayList Queue = new ArrayList();

   }
}
