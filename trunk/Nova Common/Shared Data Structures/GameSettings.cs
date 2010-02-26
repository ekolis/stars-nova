// ===========================================================================
// Nova. (c) 2010 Daniel Vale
//
// This module holds information about the game settup as determined when a 
// new game is created. The information in this module shall only be changed
// when setting up a new game, but may be read by all applications during 
// game play.
//
// This module is implemented as a singleton.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;

namespace NovaCommon
{
    [Serializable]
    public class GameSettings
    {

        // Map settings

        public int MapWidth = 400;
        public int MapHeight = 400;

        public int NumberOfStars = 50;

        // Victory conditions (with initial default values)

        public EnabledValue PlanetsOwned = new EnabledValue(true, 60);
        public EnabledValue TechLevels = new EnabledValue(false, 22);
        public EnabledValue NumberOfFields = new EnabledValue(false, 4);
        public EnabledValue TotalScore = new EnabledValue(false, 1000);
        public EnabledValue SecondPlaceScore = new EnabledValue(false, 0);
        public EnabledValue ProductionCapacity = new EnabledValue(false, 1000);
        public EnabledValue CapitalShips = new EnabledValue(false, 100);
        public EnabledValue HighestScore = new EnabledValue(false, 100);
        public int TargetsToMeet = 1;
        public int MinimumGameTime = 50;

        public String SettingsPathName = null;

        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static GameSettings instance = null;
        private static Object padlock = new Object();
        private static BinaryFormatter formatter = new BinaryFormatter();


// ============================================================================
// Private constructor to prevent anyone else creating instances of this class.
// ============================================================================

      private GameSettings() {}


// ============================================================================
// Provide a mechanism of accessing the single instance of this class that we
// will create locally. Creation of the data is thread-safe.
// ============================================================================

      public static GameSettings Data
      {
         get {
            if (instance == null) {
               lock(padlock) {
                  if (instance == null) {
                      instance = new GameSettings();
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
          Data.SettingsPathName = FileSearcher.GetFile(Global.SettingsKey, false, null, null, null, true);

          if (File.Exists(Data.SettingsPathName))
          {
              FileStream state = new FileStream(Data.SettingsPathName, FileMode.Open);
              Data = formatter.Deserialize(state) as GameSettings;
              state.Close();
          }
      }


        
        // TODO (priority 4) - save the game settings as xml
        /// <summary>
        /// Save the game settings.
        /// </summary>
        /// <param name="gameFilename">The path&filename of the server/console.state file</param>
      public static void Save(String gameFilename)
      {
          // change the extension from .state to .settings
          int extensionIndex = gameFilename.Length - 1;
          while (gameFilename[extensionIndex] != '.' && extensionIndex > 0) extensionIndex--;

          Data.SettingsPathName = gameFilename.Substring(0, extensionIndex) + ".settings";
          FileSearcher.SetNovaRegistryValue(Global.SettingsKey, Data.SettingsPathName);

          FileStream state = new FileStream(Data.SettingsPathName, FileMode.Create);
          formatter.Serialize(state, GameSettings.Data);
          state.Close();

      }


    }
}
