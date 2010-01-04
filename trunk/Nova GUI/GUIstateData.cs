// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file contains data that are persistent across multiple invokations of
// dialogs and even muliple inovcations of the GUI itself. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System;


// ============================================================================
// Manipulation of data that is persistent across muliple invocations of the
// GUI. Some things don't need to be persistent but it's convenient to keep all
// "global" data in one place.
// ============================================================================

namespace Nova
{
   [Serializable]
   public sealed class GUIstate
   {


// ============================================================================
// Data used by the GUI that will be persistent across multiple program
// invocations.
// ============================================================================

      public ArrayList    DeletedDesigns           = new ArrayList();
      public ArrayList    DeletedFleets            = new ArrayList();
      public ArrayList    Messages                 = new ArrayList();
      public GlobalTurn   InputTurn                = null;
      public Hashtable    BattlePlans              = new Hashtable();
      public Hashtable    KnownEnemyDesigns        = new Hashtable();
      public Hashtable    PlayerRelations          = new Hashtable();
      public Hashtable    StarReports              = new Hashtable();
      public Hashtable    AvailableComponents      = new Hashtable();
      public List<Fleet>  PlayerFleets             = new List<Fleet>();
      public List<Star>   PlayerStars              = new List<Star>();
      public Race         RaceData                 = new Race();
      public TechLevel    ResearchLevel            = new TechLevel();
      public TechLevel    ResearchResources        = new TechLevel();
      public bool         FirstTurn                = true;
      public double       ResearchAllocation       = 0;
      public int          ResearchBudget           = 10;
      public int          TurnYear                 = 0; 
      public string       GameFolder               = null;
      public string       RaceName                 = null;
      public string       ResearchTopic            = "Energy";


// ============================================================================
// Data private to this module.
// ============================================================================

      private static GUIstate        Instance      = null;
      private static BinaryFormatter Formatter     = new BinaryFormatter();
      private static string          StatePathName = null;


// ============================================================================
// Private constructor to prevent anyone else creating instances of this class.
// ============================================================================

      private GUIstate() {}


// ============================================================================
// Provide a mechanism of accessing the single instance of this class that we
// will create locally. Access to the data is thread-safe.
// ============================================================================

      public static GUIstate Data
      {
         get {
            Object padlock = new Object();

            lock(padlock) {
               if (Instance == null) {
                  Instance = new GUIstate();
               }
            }
            return Instance;
         }

         set {
            Instance = value;
         }
      }


// ============================================================================
// Restore the GUI persistent data if the state store file exists (it typically
// will not on the very first turn of a new game). Later on, when we read the
// file Nova.turn we will reset the persistent data fields if the turn file
// indicates the first turn of a new game.
// ============================================================================

      public static void Restore(string gameFolder)
      {

// ----------------------------------------------------------------------------
// Scan the game directory for .race files. If only one is present then that is
// the race we will use (single race test bed or remote server). If more than one is
// present then display a dialog asking the player which race he wants to use
// (multiplayer game with all players playing from a single game directory).
// ----------------------------------------------------------------------------

         DirectoryInfo directory = new DirectoryInfo(gameFolder);
         FileInfo[]    raceFiles = directory.GetFiles("*.race");

         if (raceFiles.Length == 0) {
            Report.FatalError
            ("The Nova GUI cannot start unless a race file is present");
          }

         string raceName = null;

         if (raceFiles.Length > 1) {
            raceName = SelectRace(raceFiles);
         }
         else {
            string pathName = raceFiles[0].FullName;
            raceName = Path.GetFileNameWithoutExtension(pathName);
         }

         StatePathName = Path.Combine(gameFolder, raceName + ".state");

         if (File.Exists(StatePathName)) {
            FileStream stateFile = new FileStream(StatePathName,FileMode.Open);
            GUIstate.Data        = Formatter.Deserialize(stateFile)as GUIstate;
            stateFile.Close();
         }

         // Copy the race and game folder names into the state data store. This
         // is just a convenient way of making them globally available.

         Data.RaceName   = raceName;
         Data.GameFolder = gameFolder;

         MainWindow.nova.Text = "Nova - " + Data.RaceData.PluralName;
      }


      // ============================================================================
      // Restore the GUI persistent data if the state store file exists (it typically
      // will not on the very first turn of a new game). Later on, when we read the
      // file Nova.turn we will reset the persistent data fields if the turn file
      // indicates the first turn of a new game.
      // ============================================================================

      public static void Restore(string gameFolder, string raceName)
      {
         StatePathName = Path.Combine(gameFolder, raceName + ".state");

         if (File.Exists(StatePathName))
         {
            FileStream stateFile = new FileStream(StatePathName, FileMode.Open);
            GUIstate.Data = Formatter.Deserialize(stateFile) as GUIstate;
            stateFile.Close();
         }

         // Copy the race and game folder names into the state data store. This
         // is just a convenient way of making them globally available.

         Data.RaceName = raceName;
         Data.GameFolder = gameFolder;

         MainWindow.nova.Text = "Nova - " + Data.RaceData.PluralName;
      }

// ============================================================================
// Save the GUI global data and flag that we should now be able to restore it.
// ============================================================================

      public static void Save()
      {
         FileStream stateFile = new FileStream(StatePathName,FileMode.Create);
         Formatter.Serialize(stateFile, GUIstate.Data);
         stateFile.Close();
      }


// ============================================================================
// Pop up a dialog to select the race to play
// ============================================================================

      private static string SelectRace(FileInfo[] raceFiles)
      {
         SelectRaceDialog raceDialog = new SelectRaceDialog();

         foreach (FileInfo file in raceFiles) {
            string pathName = file.FullName;;
            string raceName = Path.GetFileNameWithoutExtension(pathName);
            raceDialog.RaceList.Items.Add(raceName);
         }

         raceDialog.RaceList.SelectedIndex = 0;
            
         DialogResult result = raceDialog.ShowDialog();
         if (result == DialogResult.Cancel) {
            Report.FatalError
            ("The Nova GUI cannot start unless a race has been selected");
         }

         string selectedRace = raceDialog.RaceList.SelectedItem as string;

         raceDialog.Dispose();
         return selectedRace;
      }

   }
}

