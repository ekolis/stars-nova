// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file contains data that are persistent across multiple invokations of
// Nova Console. (It also holds the odd item that doesn't need to be persistent
// but it's just convenient to keep all "global" data in one place.
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
using Microsoft.Win32;
using ControlLibrary;


// ============================================================================
// Manipulation of data that is persistent across muliple invocations of the
// Nova Console.
// ============================================================================

namespace NovaConsole
{

// ============================================================================
// Data used by the Console that will be persistent across multiple program
// invocations (although it may be updated each year).
// ============================================================================

   [Serializable]
   public sealed class ConsoleState
   {
      public ArrayList AllBattles     = new ArrayList();
      public Hashtable AllTechLevels  = new Hashtable();
      public Hashtable AllDesigns     = new Hashtable();
      public Hashtable AllFleets      = new Hashtable();
      public Hashtable AllRaceData    = new Hashtable(); // Data about the race (traits etc) 
      public Hashtable AllRaces       = new Hashtable(); // Data about the race's relations and battle plans, see RaceData
      public Hashtable AllStars       = new Hashtable();
      public Hashtable AllMinefields  = new Hashtable();

      public bool      GameInProgress = false;
      public int       FleetID        = 1;
      public int       TurnYear       = 2100;
      public string    GameFolder     = null;
      public string    StatePathName  = null;

      // Victory conditions (with initial default values)

      public EnabledValue PlanetsOwned       = new EnabledValue(true, 60);
      public EnabledValue TechLevels         = new EnabledValue(false, 22);
      public EnabledValue NumberOfFields     = new EnabledValue(false, 4);
      public EnabledValue TotalScore         = new EnabledValue(false, 1000);
      public EnabledValue SecondPlaceScore   = new EnabledValue(false, 0);
      public EnabledValue ProductionCapacity = new EnabledValue(false, 1000);
      public EnabledValue CapitalShips       = new EnabledValue(false, 100);
      public EnabledValue HighestScore       = new EnabledValue(false, 100);
      public int          TargetsToMeet      = 1;
      public int          MinimumGameTime    = 50;

// ============================================================================
// Data private to this module.
// ============================================================================

      private static ConsoleState    instance      = null;
      private static Object          padlock       = new Object();
      private static BinaryFormatter formatter     = new BinaryFormatter();


// ============================================================================
// Private constructor to prevent anyone else creating instances of this class.
// ============================================================================

      private ConsoleState() {}


// ============================================================================
// Provide a mechanism of accessing the single instance of this class that we
// will create locally. Creation of the data is thread-safe.
// ============================================================================

      public static ConsoleState Data
      {
         get {
            if (instance == null) {
               lock(padlock) {
                  if (instance == null) {
                     instance = new ConsoleState();
                  }
               }
            }
            return instance;
         }

// ----------------------------------------------------------------------------

         set {
            instance = value;
         }
      }


// ============================================================================
// Restore the persistent data. 
// ============================================================================

      public static void Restore()
      {
         string fileName = Data.StatePathName;
         if (File.Exists(fileName)) {
            FileStream state  = new FileStream(fileName, FileMode.Open);
            ConsoleState.Data = formatter.Deserialize(state) as ConsoleState;
            state.Close();
         }
      }


// ============================================================================
// Save the console persistent data.
// ============================================================================

      public static void Save()
      {
         FileStream state = new FileStream(Data.StatePathName,FileMode.Create);
         formatter.Serialize(state, ConsoleState.Data);
         state.Close();
      }


// ============================================================================
// Reset all values to the defaults
// ============================================================================

      public static void Clear()
      {
         Data.GameFolder     = null;
         Data.AllRaces       = new Hashtable();
         Data.AllRaceData    = new Hashtable();
         Data.AllStars       = new Hashtable();
         Data.AllDesigns     = new Hashtable();
         Data.AllFleets      = new Hashtable();
         Data.AllTechLevels  = new Hashtable();
         Data.TurnYear       = 2100;
         Data.FleetID        = 1;
         Data.StatePathName  = null;
         Data.GameInProgress = false;
      }

   }
}
