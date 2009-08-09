// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Player-related functions, load turns, etc.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using Microsoft.Win32;
using NovaCommon;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System;

namespace NovaConsole 
{

   public static class Players
   {
      private static BinaryFormatter Formatter = new BinaryFormatter();
      private static ConsoleState    StateData = ConsoleState.Data;


// ============================================================================
// Identify the players. This is done by enumerating the race files present and
// loading each race definition.
// ============================================================================

      public static void Identify()
      {
         DirectoryInfo directory = new DirectoryInfo
                                        (ConsoleState.Data.GameFolder);
         
         FileInfo[] raceFiles = directory.GetFiles("*.race");

          if (raceFiles.Length == 0) {
             Report.Error("No race files found in game folder");
             return;
          }

          foreach (FileInfo file in raceFiles) {
             Race race = new Race(file.FullName); 
             ConsoleState.Data.AllRaces[race.Name] = race;
          }
      }


// ============================================================================
// Read all player turn files.
// ============================================================================

      public static void ReadData()
      {
         foreach (Race r in ConsoleState.Data.AllRaces.Values) {
            ReadPlayerData(r);
         }
      }


// ============================================================================
// Read in the player data from a turn file. If no player file is present then
// an empty player data turn structure is created.
// ============================================================================

      private static void ReadPlayerData(Race race)
      {
         RaceTurn    inputTurn = new RaceTurn();
         string      fileName  = Path.Combine(StateData.GameFolder, race.Name + ".turn");

         if (File.Exists(fileName)) {
            FileStream  turnFile  = new FileStream(fileName, FileMode.Open);
            inputTurn = Formatter.Deserialize(turnFile) as RaceTurn;
            turnFile.Close();
         }

         StateData.AllRaceData[race.Name] = inputTurn.PlayerData;
      }


// ============================================================================
// Determine if two players are enemies
// ============================================================================

      public static bool AreEnemies(string wolf, string lamb)
      {
         RaceData wolfData   = ConsoleState.Data.AllRaceData[wolf] as RaceData;
         string lambRelation = wolfData.PlayerRelations[lamb] as string;

         if (lambRelation == "Enemy") {
            return true;
         }

         return false;
      }

   }
}
