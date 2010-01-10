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
// This differs from OrderReader.ReadOrders() in that the orders are kept seperate
// and no further processing is done.
// TODO (priority 4) - refactor this method into OrderReader to remove duplication.
// ============================================================================

      private static void ReadPlayerData(Race race)
      {
          string fileName = Path.Combine(StateData.GameFolder, race.Name + ".Orders");


          Orders playerOrders = new Orders();

          if (File.Exists(fileName))
          {
              // Load from a binary serialised file.
              FileStream turnFile = new FileStream(fileName, FileMode.Open);
              playerOrders = Formatter.Deserialize(turnFile) as Orders;
              turnFile.Close();


              // Load from an xml file
              /*
              XmlDocument xmldoc = new XmlDocument();
              FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
              GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
    #if (DEBUG)
              xmldoc.Load(fileName);  // uncompressed
    #else
                xmldoc.Load(compressionStream); // compressed
    #endif
              playerOrders = new Orders(xmldoc.DocumentElement);
              fileStream.Close();
               */
          }

          // regardless of how playerOrders was loaded
          StateData.AllRaceData[race.Name] = playerOrders.PlayerData;
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
