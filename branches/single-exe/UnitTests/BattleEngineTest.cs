// ============================================================================
// Nova. (c) 2007 Ken Reed
//
// Tests for the battle engine
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Collections;
using System.Drawing;
using NUnit.Framework;

using NovaCommon;
using NovaServer;
using Nova.Console;

namespace Nova.UnitTests 
{


// ============================================================================
// Unit tests for the battle engine. Note that these tests must run in sequence
// as they simulate a complete run of the engine. Each test is a prerequisit
// for the following one. NUnit runs tests in alphabetical order, hence the
// format of the test names.
//
// First, define our initial test data. We start with 4 fleets, each with one
// ship. Two fleets are different races (they will be the ones that will engage
// in combat and two fleets belong to the same, but separate races. They ensure
// that co-located single race groupings do not engage in combat.
//
// There are some uninitialised variables to receive the results of various
// steps and provide the input to the next step.
// ============================================================================

   [TestFixture]
   public class BattleEngineTest
   {
      Fleet fleet1 = new Fleet("fleet1", "Tom",  new Point(100,200));
      Fleet fleet2 = new Fleet("fleet2", "Dick", new Point(100,200));
      Fleet fleet3 = new Fleet("fleet3", "Harry",new Point(300,400));
      Fleet fleet4 = new Fleet("fleet4", "Harry",new Point(300,400));

      ShipDesign cruiser = new ShipDesign();
      ShipDesign frigate = new ShipDesign();

      Ship ship1;
      Ship ship2;
      Ship ship3;
      Ship ship4;

      // Outputs and subsequent inputs to the various routines. These have the
      // same name as the equivalent variables in the actual code.

      ArrayList fleetPositions;
      ArrayList battlePositions;
      ArrayList zoneStacks;

// ============================================================================
// Construction (test data initialisation). Create 4 test fleets and add them
// into the "all fleets" list. Make one design more powerful than the other
// so that the weaker one gets destroyed in the battle.
// ============================================================================

      public BattleEngineTest()
      {
         Resources cost = new Resources(10, 20, 30, 40);

         Component shipHull = new Component();
         Hull hull = new Hull();
         hull.FuelCapacity = 100;
         hull.Modules = new ArrayList();
         shipHull.Properties.Add("Hull", hull);
         shipHull.Properties.Add("Battle Movement", new DoubleProperty(1.0));

         cruiser.ShipHull = shipHull;
         cruiser.Mass = 5000;
         cruiser.Cost = cost;
         frigate.ShipHull = shipHull;
         frigate.Mass = 5000;
         frigate.Cost = cost;

         ship1             = new Ship(cruiser);
         ship2             = new Ship(frigate);
         ship3             = new Ship(cruiser);
         ship4             = new Ship(frigate);

         ship1.Design.Name = "Cruiser";
         ship2.Design.Name = "Frigate";
         ship3.Design.Name = "Cruiser";
         ship4.Design.Name = "Frigate";

         ship1.Armor      = 100;
         ship2.Armor      = 200;
         ship3.Armor      = 100;
         ship4.Armor      = 200;

         ship1.Cost        = cost;
         ship2.Cost        = cost;
         ship3.Cost        = cost;
         ship4.Cost        = cost;

         fleet1.FleetShips.Add(ship1);
         fleet2.FleetShips.Add(ship2);
         fleet3.FleetShips.Add(ship3);
         fleet4.FleetShips.Add(ship4);

         ServerState.Data.AllFleets["fleet1"] = fleet1;
         ServerState.Data.AllFleets["fleet2"] = fleet2;
         ServerState.Data.AllFleets["fleet3"] = fleet3;
         ServerState.Data.AllFleets["fleet4"] = fleet4;
      }


// ============================================================================
// Test for DetermineCoLocatedFleets. The test data has one set of two fleets
// at the one location and the other set of two fleets at another location so
// the result should be 2.
// ============================================================================

      [Test]
      public void Test1DetermineCoLocatedFleets()
      {
         fleetPositions = BattleEngine.DetermineCoLocatedFleets();
         Assert.AreEqual(2, fleetPositions.Count);
       }


// ============================================================================
// Test for EliminateSingleRaces
// ============================================================================

      [Test]
      public void Test2EliminateSingleRaces()
      {
         battlePositions = BattleEngine.EliminateSingleRaces(fleetPositions);
         Assert.AreEqual(1, battlePositions.Count);
      }


// ============================================================================
// Test for GenerateStacks
// ============================================================================

      [Test]
      public void Test3GenerateStacks()
      {
         ArrayList combatZone = battlePositions[0] as ArrayList;
         zoneStacks = BattleEngine.GenerateStacks(combatZone);
         Assert.AreEqual(2, zoneStacks.Count);
      }


// ============================================================================
// Test for SelectTargets. In addition to submitting the stacks to the routine
// we must also set up enough of an environment so that the races (tom and
// dick) are enemies with an attack plan.
// ============================================================================

      [Test]
      public void Test4SelectTargets()
      {
         RaceData raceData = new RaceData();

         raceData.PlayerRelations["Dick"]      = "Enemy"; 
         raceData.BattlePlans["Default"]       = new BattlePlan();
         ServerState.Data.AllRaceData["Tom"]  = raceData;

         raceData.PlayerRelations["Tom"]       = "Enemy"; 
         ServerState.Data.AllRaceData["Dick"] = raceData;

         int numberOfTargets = BattleEngine.SelectTargets(zoneStacks);

         Fleet stackA = zoneStacks[0] as Fleet;
         Fleet stackB = zoneStacks[1] as Fleet;
          
         Assert.AreEqual(2, numberOfTargets);
         Assert.AreEqual(stackA.Target.Name, stackB.Name);
         Assert.AreEqual(stackB.Target.Name, stackA.Name);
      }


// ============================================================================
// Test for PositionStacks. 
// ============================================================================

      [Test]
      public void Test5PositionStacks()
      {
         BattleEngine.PositionStacks(zoneStacks);
          
         int    separation = Global.MaxWeaponRange - 1;
         double distance   = 0;

         Fleet stackA = zoneStacks[0] as Fleet;
         Fleet stackB = zoneStacks[1] as Fleet;
          
         distance = PointUtilities.Distance(stackA.Position, stackB.Position);
         Assert.Greater((int) distance, separation);
      }


// ============================================================================
// Test for DoBattle
// ============================================================================

      [Test]
      public void Test6DoBattle()
      {
         Fleet stackA = zoneStacks[0] as Fleet;
         Fleet stackB = zoneStacks[1] as Fleet;

         double distanceS, distanceE;

         distanceS = PointUtilities.Distance(stackA.Position, stackB.Position);

         BattleEngine.DoBattle(zoneStacks);

         distanceE = PointUtilities.Distance(stackA.Position, stackB.Position);

         // Check that the fleets have moved towards each other.

         Assert.Greater(distanceS, distanceE);
      }

/*	   

// ============================================================================
// Test for FireWeapons
// ============================================================================

      [Test]
      public void Test7FireWeapons()
      {
         // FireWeapons(zoneStacks)
         
      }
*/
   }
}
