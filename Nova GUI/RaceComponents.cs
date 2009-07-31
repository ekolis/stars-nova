// This file needs -*- c++ -*- mode
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
         Race race = GUIstate.Data.RaceData;
         string name = component.Name;


         if (component.CheckAvailability(race)) 
         {
            GUIstate.Data.AvailableComponents[component.Name] = component;
       
            if (report) {
               GUIstate.Data.Messages.Add("You now have available the "
                                      + component.Name + " " + component.Type
                                      + " component");
            }
         }
      }


// ===========================================================================
// Check if this engine is available to this race
// TODO - Check if this function is still required
// ===========================================================================

      private static bool CheckEngine(Engine engine)
      {
          /* disabled for refactoring of components - Daniel Apr 09. Race restrictions to be dynamic
         if (engine.RamScoop && GUIstate.Data.RaceData.NoRamScoopEngines) {
            return false;
         }

         if (engine.RequiresFuelEfficiency &&
            GUIstate.Data.RaceData.ImprovedEfficiency == false) {
            return false;
         }

         if (engine.Name == "Interspace 10") {
            if (GUIstate.Data.RaceData.NoRamScoopEngines) {
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
// TODO - Check if this function is still required
// ===========================================================================

      private static bool CheckHull(Hull hull)
      {
          /* FIXME - race restrictions
         if (hull.Name == "Meta-Morph") {
            if (GUIstate.Data.RaceData.Type == "HyperExpansion") {
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
