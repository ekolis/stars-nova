// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module will initialise all the data item required by the Nova GUI
// (mostly data read from save files).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using Microsoft.Win32;
using NovaCommon;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System;

namespace Nova
{

    // ============================================================================
    // Initialise all game data
    // ============================================================================

    public static class GUIdata
    {
        private static BinaryFormatter Formatter = new BinaryFormatter();

        // ============================================================================
        // Initialise the data needed for the GUI to run.
        // On first run a password is required , on any additional turnsb no password 
        // is required.
        // ============================================================================

        public static void Load(bool bPasswordNeeded)
        {
            //GUIstate.Data = GUIstate.Data;

            // ----------------------------------------------------------------------------
            // Get the name of the folder where all the game files will be stored. This
            // is placed in the Registry by the Race Generator and we cache a copy in the
            // GUI state data store.
            //
            // Note if the same folder on the same machine is chosen for all Nova GUI and
            // Nova Console files it is possible to play a game with multiple races using
            // the same log-in name. This is a useful debugging aid.
            // ----------------------------------------------------------------------------

            RegistryKey regKey = Registry.CurrentUser;
            RegistryKey regSubKey = regKey.CreateSubKey(Global.RootRegistryKey);
            string gameFolder = "none";
            try
            {
                gameFolder = regSubKey.GetValue(Global.ClientFolderKey).ToString();
            }
            catch
            {
                Report.FatalError("An expected registry entry is missing\n" +
                                  "Have you ran the Race Designer and \n" +
                                  "Nova Console?");
            }

            // ----------------------------------------------------------------------------
            // Load the game persistent data and all component details and check the
            // password.
            // ----------------------------------------------------------------------------
            if (bPasswordNeeded) //i.e. the first time the gui loads select a race..
            {
                GUIstate.Restore(gameFolder);
            }
            else
            {
                GUIstate.Restore(gameFolder, GUIstate.Data.RaceName);
            }

            if (AllComponents.Restore() == false)
            {
                Report.FatalError("Could not find component definition file");
            }

            if (GUIstate.Data.FirstTurn)
            {
                GUIstate.Data.BattlePlans.Add("Default", new BattlePlan());
                ProcessRaceDefinition();
            }

            // ----------------------------------------------------------------------------
            // Check the password for access to this race's data
            // ----------------------------------------------------------------------------

            ControlLibrary.CheckPassword password =
               new ControlLibrary.CheckPassword(GUIstate.Data.RaceData);

            if (bPasswordNeeded)
            {
                password.ShowDialog();
                password.Dispose();

                if (password.DialogResult == DialogResult.Cancel)
                {
                    System.Threading.Thread.CurrentThread.Abort();
                }
            }

            LoadTurnFile();

            if (GUIstate.Data.FirstTurn)
            {
                foreach (string raceName in GUIstate.Data.InputTurn.AllRaceNames)
                {
                    GUIstate.Data.PlayerRelations[raceName] = "Neutral";
                }
            }

            GUIstate.Data.FirstTurn = false;
        }


        // ============================================================================
        // Read and process the Global Turn file (Nova.turn) generated by the Nova
        // Console. This file must be present before the GUI will run (since it
        // contains all sorts of important data such as a list of all stars which is
        // needed to draw the star map). This is also saved in the GUI state data
        // store.
        //
        // Note that this file is not read again after the first time a new turn is
        // received until a new turn arrives. This is because we will update some of
        // the Global Turn data during the preparation of the next turn.
        // ============================================================================

        public static void LoadTurnFile()
        {
            string turnFileName = Path.Combine(GUIstate.Data.GameFolder, "Nova.turn");

            if (File.Exists(turnFileName) == false)
            {
                Report.FatalError
                ("The Nova GUI cannot start unless a turn file is present");
            }

            FileStream turnFile = new FileStream(turnFileName, FileMode.Open);
            int turnYearInFile = (int)Formatter.Deserialize(turnFile);

            if (turnYearInFile != GUIstate.Data.TurnYear)
            {
                GUIstate.Data.InputTurn = Formatter.Deserialize(turnFile)
                                          as GlobalTurn;
                turnFile.Close();

                GUIstate.Data.TurnYear = turnYearInFile;
                GUIstate.Data.DeletedFleets.Clear();
                GUIstate.Data.DeletedDesigns.Clear();

                InputTurnData.Process();
            }
        }


        // ============================================================================
        // Read the race definition file into the persistent data store. If this is the
        // very first turn of a new game then process it's content to set up initial
        // race parameters (e.g. initial technology levels, etc.).
        // ============================================================================

        private static void ProcessRaceDefinition()
        {
            string raceFileName = Path.Combine(GUIstate.Data.GameFolder,
                                               GUIstate.Data.RaceName + ".race");

            GUIstate.Data.RaceData = new Race(raceFileName);

            MainWindow.nova.Text = "Nova - " + GUIstate.Data.RaceData.PluralName;

            ProcessPrimaryTraits();
            ProcessSecondaryTraits();
        }


        // ============================================================================
        // Determine the components available to this race according to the starting
        // tech levels. Race-specific adjustments will be made in ProcessPrimaryTraits.
        // This processing is only performed once per game.
        // ============================================================================

        public static void DetermineAvailableComponents()
        {
            foreach (NovaCommon.Component component in
                     AllComponents.Data.Components.Values)
            {
                if (GUIstate.Data.ResearchLevel >= component.RequiredTech)
                {
                    RaceComponents.Add(component, false);
                }
            }
        }


        // ============================================================================
        // Process the Primary Traits for this race.
        // ============================================================================

        private static void ProcessPrimaryTraits()
        {
            switch (GUIstate.Data.RaceData.Type)
            {
                case "JackOfAllTrades":
                    GUIstate.Data.ResearchLevel = new TechLevel(3);//set all to 3
                    //GUIstate.Data.ResearchLevel = new TechLevel(26); //set all to 26
                    //AddComponent("Chameleon Scanner");//obsolete but will be rejected now...
                    break;

                case "HyperExpansion":
                    //GUIstate.Data.ResearchLevel = new TechLevel(26);
                    GUIstate.Data.ResearchLevel = new TechLevel(1); // set all to 1
                    AddComponent("Settler's Delight");
                    AddComponent("Mini-Colony Ship");
                    break;
            }
#if (DEBUG)
            // Just for testing
            GUIstate.Data.ResearchLevel = new TechLevel(26);
#endif

            DetermineAvailableComponents();
        }


        // ============================================================================
        // Add a named component, will now check if it exists
        // ============================================================================

        private static void AddComponent(string componentName)
        {
            if (ComponentExists(componentName))
            {
                GUIstate.Data.AvailableComponents[componentName] = AllComponents.Data.Components[componentName];
            }
            else
            {
                string s = "Error: The " + componentName + " component does not exist!";
                MessageBox.Show(s);
            }
        }

        private static bool ComponentExists(string componentName)
        {
            return AllComponents.Data.Components.ContainsKey(componentName);
        }

        // ============================================================================
        // Process the Secondary Traits for this race.
        // ============================================================================

        private static void ProcessSecondaryTraits()
        {
            if (GUIstate.Data.RaceData.Traits.Contains("IFE"))
            {
                int level = (int)GUIstate.Data.ResearchLevel.TechValues["Propulsion"];
                level++;
                GUIstate.Data.ResearchLevel.TechValues["Propulsion"] = level;
            }
        }

    }
}
