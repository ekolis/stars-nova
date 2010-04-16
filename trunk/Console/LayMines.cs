// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module deals with fleets laying mines. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

using NovaCommon;
using NovaServer;

namespace NovaConsole
{


// ============================================================================
// Deal with mine layings
// ============================================================================

   public static class LayMines
   {

// ============================================================================
// See if we can lay mines here. If so, lay them. If there is no Minefield here
// start a new one. 
// ============================================================================

      public static void DoMines(Fleet fleet)
      {
         if (fleet.NumberOfMines == 0) {
            return;
         }

         // See if a Minefield is already here (owned by us). We allow a
         // certaintolerance in distance because it is unlikely that the
         // waypoint has been set exactly right.

         foreach (Minefield Minefield in ServerState.Data.AllMinefields.Values) {
            if (PointUtilities.IsNear(fleet.Position, Minefield.Position)) {
               if (Minefield.Owner == fleet.Owner) {
                  Minefield.NumberOfMines += fleet.NumberOfMines;
                  return;
               }
            }
         }

         // No Minefield found. Start a new one.

         Minefield newField = new Minefield();

         newField.Position = fleet.Position;
         newField.Owner = fleet.Owner;
         newField.NumberOfMines =fleet.NumberOfMines;

         ServerState.Data.AllMinefields[newField] = newField;
      }
     

   }
}     
      
