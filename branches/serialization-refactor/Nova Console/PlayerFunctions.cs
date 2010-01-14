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
using System.Xml;
using System.IO.Compression;

namespace NovaConsole 
{

   public static class Players
   {
      private static BinaryFormatter Formatter = new BinaryFormatter();
      private static ConsoleState    StateData = ConsoleState.Data;


// ============================================================================
// Identify the players. This is done by enumerating the race files present and
// loading each race definition.
// This is ok for a default setup, but it should be possible to add or remove
// races when creating a new game. TODO (priority 3)
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
