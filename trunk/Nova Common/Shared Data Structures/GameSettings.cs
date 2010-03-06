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

#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

#endregion

namespace NovaCommon
{
    [Serializable]
    public sealed class GameSettings
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
        public String GameName = "Feel the Nova";

        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static GameSettings instance = null;
        private static Object padlock = new Object();


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

        #region Methods
        //-------------------------------------------------------------------
        /// <summary>
        /// Restore the persistent data.
        /// </summary>
        //-------------------------------------------------------------------
      public static void Restore()
      {
          string fileName = Data.SettingsPathName;
          if (File.Exists(fileName))
          {
              using (FileStream state = new FileStream(fileName, FileMode.Open))
              {
                  Data = Serializer.Deserialize(state) as GameSettings;
              }
          }
      }

        //-------------------------------------------------------------------
        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        //-------------------------------------------------------------------
        public static void Save()
        {
            if( Data.SettingsPathName == null )
            {
                // TODO (priority 4) add the nicities. Update the game files location.
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Choose a location to save the game settings.";

                DialogResult result = fd.ShowDialog();
                if( result == DialogResult.OK )
                {
                    Data.SettingsPathName = fd.FileName;
                }
                else
                {
                    throw new System.IO.IOException( "File dialog cancelled." );
                }

            }
            using( Stream stream = new FileStream( Data.SettingsPathName, FileMode.Create ) )
            {
                Serializer.Serialize( stream, GameSettings.Data );
            }
        }
        #endregion
    }
}
