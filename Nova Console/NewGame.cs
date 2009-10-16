// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module will start a new game (create stars, identify players, etc.)
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

   public class NewGame
   {
      static Random              random       = new Random();
      static BinaryFormatter     formatter    = new BinaryFormatter();


// ============================================================================
// Start a new game. First we pop up the victory conditions dialog for the
// game. Providing cancel isn't pressed we then go on to generate all of the
// initial information, stars, initial designs, etc. and place them in the
// console state store. Then we build the actual turn that will reference some
// things from the state store and add other turn specific information,
// e.g. the game welcome message.
//=============================================================================

      public static bool Start()
      {
         // Establish the victory conditions:

         VictoryConditions victoryConditions = new VictoryConditions();
         DialogResult result = victoryConditions.ShowDialog();
         
         // Check for cancel being pressed.

         if (result == DialogResult.Cancel) {
            return false;
         }

         // OK, generate the new game.

         GenerateStars();
         InitialisePlayerData();
         Turn.BuildAndSave();

         return true;
      }


// ===========================================================================
// Generate all the stars. We have two helper classes to assist in doing this:
// a name generator to allocate star names and a space generator to ensure
// stars have a reasonable separation. Beyond that, all we need to do is
// allocate some random mineral concentrations.
// ===========================================================================

      private static void GenerateStars()
      {
         NameGenerator  nameGenerator  = new NameGenerator();
         int            numberOfStars  = Global.NumberOfStars;
         SpaceAllocator spaceAllocator = new SpaceAllocator(numberOfStars);
      
         spaceAllocator.AllocateSpace(Global.UniverseSize);

         for (int count = 0; count < numberOfStars; count++) {
            Star star = new Star();

            Rectangle area = spaceAllocator.GetBox();
            star.Position  = PointUtilities.GetPositionInBox(area, 10);

            star.Name                           = nameGenerator.NextName;
            star.MineralConcentration.Boranium  = random.Next(1, 99);
            star.MineralConcentration.Ironium   = random.Next(1, 99);
            star.MineralConcentration.Germanium = random.Next(1, 99);

            // The following values are percentages of the permissable range of
            // each environment parameter expressed as a percentage.

            star.Radiation   = random.Next(1, 99);
            star.Gravity     = random.Next(1, 99);
            star.Temperature = random.Next(1, 99);

            ConsoleState.Data.AllStars[star.Name] = star;
         }
      }


// ===========================================================================
// Initialise the general game data for each player. E,g, picking a home
// planet, allocating initial resources, etc.
// ===========================================================================

      private static void InitialisePlayerData()
      {
         SpaceAllocator spaceAllocator = new SpaceAllocator
                        (ConsoleState.Data.AllRaces.Count);

         spaceAllocator.AllocateSpace(Global.UniverseSize);

         foreach (Race race in ConsoleState.Data.AllRaces.Values) {
            string player  = race.Name;

            Design mine    = new Design();
            Design factory = new Design();
            Design Defense = new Design();

            mine.Cost      = new NovaCommon.Resources(0, 0, 0, race.MineBuildCost);
            mine.Name      = "Mine";
            mine.Type      = "Mine";
            mine.Owner     = player;

            // If we have the secondary racial trait Cheap Factories they need 1K
            // less germanium to build.
            int factoryBuildCostGerm = (race.HasTrait("CF") ? 3 : 4);
            factory.Cost   = new NovaCommon.Resources(0, 0, factoryBuildCostGerm, race.FactoryBuildCost);
            factory.Name   = "Factory";
            factory.Type   = "Factory";
            factory.Owner  = player;

            Defense.Cost   = new NovaCommon.Resources(5, 5, 5, 15);
            Defense.Name   = "Defenses";
            Defense.Type   = "Defenses";
            Defense.Owner  = player;

            ConsoleState.Data.AllDesigns[player + "/Mine"]     = mine;
            ConsoleState.Data.AllDesigns[player + "/Factory"]  = factory; 
            ConsoleState.Data.AllDesigns[player + "/Defenses"] = Defense;

            InitialiseHomeStar(race, spaceAllocator);
         }
         
         NovaCommon.Message welcome = new NovaCommon.Message();
         welcome.Text     = "Your race is ready to explore the universe.";
         welcome.Audience = "*";

         GlobalTurn.Data.Messages.Add(welcome);
         GlobalTurn.Data.TurnYear = 2100;
      }


// ============================================================================
// Allocate a "home" star system for each player giving it some colonists and
// initial resources. We use the space allocater helper class to ensure that
// the home systems for each race are not too close together.
// ============================================================================

      private static void InitialiseHomeStar(Race           race, 
                                             SpaceAllocator spaceAllocator)
      {
         ConsoleState stateData = ConsoleState.Data;

         Rectangle box = spaceAllocator.GetBox(); 
         foreach (Star star in stateData.AllStars.Values) {
            if (PointUtilities.InBox(star.Position, box)) {
               AllocateHomeStarResources(star, race);
               return;
            }
         }

         Report.FatalError("Could not allocate home star");
      }


// ============================================================================
// Allocate an initial set of resources to a player's "home" star system. for
// each player giving it some colonists and initial resources. 
// ============================================================================

      private static void AllocateHomeStarResources(Star star, Race race)
      {
         star.ResourcesOnHand.Boranium       = random.Next(300, 500);
         star.ResourcesOnHand.Ironium        = random.Next(300, 500);
         star.ResourcesOnHand.Germanium      = random.Next(300, 500);
         star.ResourcesOnHand.Energy         = 25;

         star.MineralConcentration.Boranium  = random.Next(50, 100);
         star.MineralConcentration.Ironium   = random.Next(50, 100);
         star.MineralConcentration.Germanium = random.Next(50, 100);

         star.Owner                          = race.Name;
         star.ScannerType                    = "Scoper 150"; // TODO get from component list
         star.DefenseType                    = "SDI"; // TODO get from component list
         star.ScanRange                      = 50; // TODO get from component list
         star.Mines                          = 10;
         star.Factories                      = 10;

         // Set the habital values for this star to the optimum for each race.
         // This should result in a planet value of 100% for this race's home
         // world.

         star.Radiation   = race.OptimumRadiationLevel;
         star.Temperature = race.OptimumTemperatureLevel;
         star.Gravity     = race.OptimumGravityLevel;

         if (race.HasTrait("LSP"))
         {
            star.Colonists = 17500;
         }
         else {
            star.Colonists = 25000;
         }
      }

   }
}
