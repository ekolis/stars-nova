// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module will generate the player turn file to be sent to the Nova
// Console so that the turn for the next year can be generated. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System;


// ============================================================================
// Generate the player's turn.
// ============================================================================

namespace Nova
{
   public class PlayerTurn
   {

// ---------------------------------------------------------------------------
// Class private data. The turn data itself is stored in the class defined in
// NovaCommon RaceTurn.cs
// ---------------------------------------------------------------------------

      private static BinaryFormatter Formatter  = new BinaryFormatter();
      private static GUIstate        StateData  = null;
      private static GlobalTurn      InputTurn  = null;
      private static string          RaceName   = null;


// ============================================================================
// Generate the player's turn. We don't bother checking what's changed we just
// send the details of everything owned by the player's race and the Nova
// Console will update its master copy of everything.
// ============================================================================

      public static void Generate()
      {
         StateData  = GUIstate.Data;
         InputTurn  = StateData.InputTurn;
         RaceName   = StateData.RaceName;

         RaceTurn outputTurn = new RaceTurn();
         RaceData playerData = new RaceData();
         
         playerData.TurnYear        = StateData.TurnYear;
         playerData.PlayerRelations = StateData.PlayerRelations;
         playerData.BattlePlans     = StateData.BattlePlans;
         outputTurn.PlayerData      = playerData;
         outputTurn.TechLevel       = CountTechLevels();

         foreach (Fleet fleet in InputTurn.AllFleets.Values) {
            if (fleet.Owner == RaceName) {
               outputTurn.RaceFleets.Add(fleet);
            }
         }

         // While adding the details of the stars owned by the player's race
         // tell the star how many of its resources have been allocated to be
         // used for research (used in the Star.Update method). We also keep a
         // local count so that we can "do" the research on the arrival of the
         // next turn.

         StateData.ResearchAllocation = 0;

         foreach (Star star in InputTurn.AllStars.Values) {
            if (star.Owner == RaceName) {
               star.ResearchAllocation = (int) ((star.ResourcesOnHand.Energy
                                          *  StateData.ResearchBudget) / 100);

               StateData.ResearchAllocation += star.ResearchAllocation;
               outputTurn.RaceStars.Add(star);
            }
         }

         foreach (Design design in InputTurn.AllDesigns.Values) {
            if (design.Owner == RaceName) {
               outputTurn.RaceDesigns.Add(design);
            }
         }
         
         foreach (string fleetName in StateData.DeletedFleets) {
            outputTurn.DeletedFleets.Add(fleetName);
         }

         foreach (string designKey in StateData.DeletedDesigns)
         {
            outputTurn.DeletedFleets.Add(designKey);
         }

         string turnFileName = Path.Combine(StateData.GameFolder, RaceName + ".turn");

         FileStream turnFile=new FileStream(turnFileName, FileMode.Create);
         Formatter.Serialize(turnFile, outputTurn);
         turnFile.Close();
      }


// ============================================================================
// Return the sum of the levels reached in all research areas
// ============================================================================

      private static int CountTechLevels()
      {
         StateData = GUIstate.Data;

         int       total     = 0;
         Hashtable techLevel = StateData.ResearchLevel.TechValues;
         
         total += (int) techLevel["Energy"];
         total += (int) techLevel["Weapons"];
         total += (int) techLevel["Propulsion"];
         total += (int) techLevel["Construction"];
         total += (int) techLevel["Electronics"];
         total += (int) techLevel["Biotechnology"];

         return total;
      }

   }
}
