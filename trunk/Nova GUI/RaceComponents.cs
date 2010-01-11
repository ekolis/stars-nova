// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Maintain components according to race characteristics
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Drawing;
using NovaCommon;

namespace Nova
{


// ============================================================================
// Maintain race components
// ============================================================================

   public static class RaceComponents
   {


// ===========================================================================
// Add a component if the race characteristics allow it
// ===========================================================================

      public static void Add(NovaCommon.Component component, bool report)
      {
         Race race = GuiState.Data.RaceData;
         string name = component.Name;


         if (component.CheckAvailability(race)) 
         {
            GuiState.Data.AvailableComponents[component.Name] = component;
       
            if (report) 
            {
                Message newComponentMessage = 
                    new Message(GuiState.Data.RaceName, null, "You now have available the "
                    + component.Name + " " + component.Type + " component");
               GuiState.Data.Messages.Add(newComponentMessage);
            }
         }
      }


// ===========================================================================
// Check if this engine is available to this race
// TODO (priority 4) - Check if this function is still required
// ===========================================================================

      private static bool CheckEngine(Engine engine)
      {
          /* disabled for refactoring of components - Daniel Apr 09. Race restrictions to be dynamic
         if (engine.RamScoop && GuiState.Data.RaceData.NoRamScoopEngines) {
            return false;
         }

         if (engine.RequiresFuelEfficiency &&
            GuiState.Data.RaceData.ImprovedEfficiency == false) {
            return false;
         }

         if (engine.Name == "Interspace 10") {
            if (GuiState.Data.RaceData.NoRamScoopEngines) {
               return true;
            }
            else {
               return false;
            }
         }
          */
         return true;
      }


// ===========================================================================
// Check if this hull is available to this race
// TODO (priority 4) - Check if this function is still required
// ===========================================================================

      private static bool CheckHull(Hull hull)
      {
          /* FIXME - race restrictions
         if (hull.Name == "Meta-Morph") {
            if (GuiState.Data.RaceData.Type == "HyperExpansion") {
               return true;
            }
            else {
               return false;
            }
         }
          */
         return true;
      }
   }
}
