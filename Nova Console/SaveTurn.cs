// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module converts the console's state into a Turn and saves it, thereby 
// generating the next turn to be played.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;


// ============================================================================
// Manipulation of the turn data that is shared between the Console and GUI.
// ============================================================================

namespace NovaConsole
{
   public class Turn
   {
      private static BinaryFormatter Formatter = new BinaryFormatter();
      private static ConsoleState    StateData = null;
      private static GlobalTurn      TurnData  = null;


// ============================================================================
// Save the turn data. We have to be very careful that we have a consistent and
// self-contained data set in the turn file. For example, we write out
// "AllStars" but TurnData.AllStars is not the same as StateData.AllStars. So
// make sure any pointers to AllStars refer to the copy in TurnData otherwise
// we'll get duplicated (but separate) star objects.
// ============================================================================

      public static void BuildAndSave()
      {
         StateData = ConsoleState.Data;
         TurnData  = GlobalTurn.Data;

         TurnData.TurnYear      = StateData.TurnYear;
         TurnData.AllStars      = StateData.AllStars;
         TurnData.AllMinefields = StateData.AllMinefields;
         TurnData.AllFleets     = StateData.AllFleets;
         TurnData.AllDesigns = StateData.AllDesigns;

         foreach (Race race in StateData.AllRaces.Values) {
            TurnData.AllRaceNames.Add(race.Name);
            TurnData.RaceIcons[race.Name] = race.Icon;
         }

         foreach (BattleReport report in StateData.AllBattles) {
            TurnData.Battles.Add(report);
         }

         // Don't try and generate a scores report on the very start of a new
         // game.

         if (StateData.AllTechLevels.Count != 0) {
            TurnData.AllScores = Scores.GetScores();
         }
         else {
            TurnData.AllScores = new ArrayList();
         }

         string turnFileName = Path.Combine(ConsoleState.Data.GameFolder, "Nova.turn");
         FileStream turnFile = new FileStream(turnFileName,FileMode.Create);

         Formatter.Serialize(turnFile, GlobalTurn.Data.TurnYear);
         Formatter.Serialize(turnFile, GlobalTurn.Data);

         turnFile.Close();
      }
   }
}


