// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// A convenience functions to hide implementation (and save typing).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using NovaCommon;

namespace Nova.Gui
{
 
// ============================================================================
// Class to deal with details of research
// ============================================================================

   public class Utilities
   {
 

// ============================================================================
// Force an update of the map display.
// ============================================================================

      public static void MapRefresh()
      {
         MainWindow.nova.MapControl.MapRefresh();
      }

   }
}
