#region Copyright Notice
// ============================================================================
// Copyright (C) 2007, 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.Tests.UnitTests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Nova.Server;
    using Nova.WinForms.Console;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the battle engine. Note that these tests must run in sequence
    /// as they simulate a complete run of the engine. Each test is a prerequisit
    /// for the following one. NUnit runs tests in alphabetical order, hence the
    /// format of the test names.
    ///
    /// First, define our initial test data. We start with 4 fleets, each with one
    /// ship. Two fleets are different races (they will be the ones that will engage
    /// in combat and two fleets belong to the same, but separate races. They ensure
    /// that co-located single race groupings do not engage in combat.
    ///
    /// There are some uninitialised variables to receive the results of various
    /// steps and provide the input to the next step.
    ///
    /// TODO (priority 5) Eliminate the requirement that the tests are run in a specific sequence.
    /// Unit tests should be able to run individually and not depend on other tests.
    /// </summary>
    [TestFixture]
    public class BattleEngineTest
    {
        private ServerState stateData = new ServerState();
        private BattleEngine battleEngine;

        private const int Player1Id = 1;
        private const int Player2Id = 2;
        private const int Player3Id = 3;

        private Fleet fleet1 = new Fleet("fleet1", Player1Id, 1, new Point(100, 200));
        private Fleet fleet2 = new Fleet("fleet2", Player2Id, 1, new Point(100, 200));
        private Fleet fleet3 = new Fleet("fleet3", Player3Id, 1, new Point(300, 400));
        private Fleet fleet4 = new Fleet("fleet4", Player3Id, 2, new Point(300, 400));

        private ShipDesign cruiser = new ShipDesign();
        private ShipDesign frigate = new ShipDesign();

        private Ship ship1;
        private Ship ship2;
        private Ship ship3;
        private Ship ship4;

        // Outputs and subsequent inputs to the various routines. These have the
        // same name as the equivalent variables in the actual code.

        private List<List<Fleet>> fleetPositions;
        private List<List<Fleet>> battlePositions;
        private List<Fleet> zoneStacks;

        /// <Summary>
        /// Initializes a new instance of the BattleEngineTest class.
        /// Create 4 test fleets and add them
        /// into the "all fleets" list. Make one design more powerful than the other
        /// so that the weaker one gets destroyed in the battle.
        /// </Summary>
        public BattleEngineTest()
        {
            battleEngine = new BattleEngine(stateData, new BattleReport());
            Resources cost = new Resources(10, 20, 30, 40);

            // Initialize empires 
            EmpireData empireData1 = new EmpireData();
            EmpireData empireData2 = new EmpireData();
            EmpireData empireData3 = new EmpireData();
            empireData1.Id = 1;
            empireData2.Id = 2;
            empireData3.Id = 3;
            empireData1.Race.Name = "Tom";
            empireData2.Race.Name = "Dick";
            empireData2.Race.Name = "Harry";

            stateData.AllEmpires[empireData1.Id] = empireData1;
            stateData.AllEmpires[empireData2.Id] = empireData2;
            stateData.AllEmpires[empireData3.Id] = empireData3;

            empireData1.BattlePlans["Default"] = new BattlePlan();
            empireData2.BattlePlans["Default"] = new BattlePlan();

            empireData1.EmpireReports.Add(2, new EmpireIntel(empireData2));
            empireData2.EmpireReports.Add(1, new EmpireIntel(empireData1));

            empireData1.EmpireReports[2].Relation = PlayerRelation.Enemy;
            empireData2.EmpireReports[1].Relation = PlayerRelation.Enemy;

            Component shipHull = new Component();
            Hull hull = new Hull();
            hull.FuelCapacity = 100;
            hull.Modules = new List<HullModule>();
            shipHull.Cost = cost;
            shipHull.Mass = 5000;
            shipHull.Properties.Add("Hull", hull);
            shipHull.Properties.Add("Battle Movement", new DoubleProperty(1.0));

            cruiser.ShipHull = shipHull;
            // cruiser.Mass = 5000;
            // cruiser.Cost = cost;
            cruiser.Name = "Cruiser";

            frigate.ShipHull = shipHull;
            // frigate.Mass = 5000;
            // frigate.Cost = cost;
            frigate.Name = "Frigate";

            ship1 = new Ship(cruiser, empireData1.GetNextShipKey());
            ship2 = new Ship(frigate, empireData2.GetNextShipKey());
            ship3 = new Ship(cruiser, empireData3.GetNextShipKey());
            ship4 = new Ship(frigate, empireData3.GetNextShipKey());

            ship1.Armor = 100;
            ship2.Armor = 200;
            ship3.Armor = 100;
            ship4.Armor = 200;

            ship1.Cost = cost;
            ship2.Cost = cost;
            ship3.Cost = cost;
            ship4.Cost = cost;

            fleet1.FleetShips.Add(ship1);
            fleet2.FleetShips.Add(ship2);
            fleet3.FleetShips.Add(ship3);
            fleet4.FleetShips.Add(ship4);

            stateData.AllFleets[fleet1.Key] = fleet1;
            stateData.AllFleets[fleet2.Key] = fleet2;
            stateData.AllFleets[fleet3.Key] = fleet3;
            stateData.AllFleets[fleet4.Key] = fleet4;
        }

        /// <Summary>
        /// Test for DetermineCoLocatedFleets. The test data has one set of two fleets
        /// at the one location and the other set of two fleets at another location so
        /// the allTurnedIn should be 2.
        /// </Summary>
        [Test]
        public void Test1DetermineCoLocatedFleets()
        {
            fleetPositions = battleEngine.DetermineCoLocatedFleets();
            Assert.AreEqual(2, fleetPositions.Count);
        }

        /// <Summary>
        /// Test for EliminateSingleRaces
        /// </Summary>
        [Test]
        public void Test2EliminateSingleRaces()
        {
            battlePositions = battleEngine.EliminateSingleRaces(fleetPositions);
            Assert.AreEqual(1, battlePositions.Count);
        }

        /// <Summary>
        /// Test for GenerateStacks
        /// </Summary>
        [Test]
        public void Test3GenerateStacks()
        {
            List<Fleet> combatZone = battlePositions[0];
            zoneStacks = battleEngine.GenerateStacks(combatZone);
            Assert.AreEqual(2, zoneStacks.Count);
        }

        /// <Summary>
        /// Test for SelectTargets. In addition to submitting the stacks to the routine
        /// we must also set up enough of an environment so that the empires (1 "Tom" 
        /// & 2 "Dick") are enemies with an attack plan.
        /// </Summary>
        [Test]
        public void Test4SelectTargets()
        {
            int numberOfTargets = battleEngine.SelectTargets(zoneStacks);

            Fleet stackA = zoneStacks[0] as Fleet;
            Fleet stackB = zoneStacks[1] as Fleet;

            Assert.AreEqual(2, numberOfTargets);
            Assert.AreEqual(stackA.Target.Name, stackB.Name);
            Assert.AreEqual(stackB.Target.Name, stackA.Name);
        }

        /// <Summary>
        /// Test for PositionStacks. 
        /// </Summary>
        [Test]
        public void Test5PositionStacks()
        {
            battleEngine.PositionStacks(zoneStacks);

            double distance = 0;

            Fleet stackA = zoneStacks[0] as Fleet;
            Fleet stackB = zoneStacks[1] as Fleet;

            distance = PointUtilities.Distance(stackA.Position, stackB.Position);
            Assert.Greater((int)distance, 3); // changed from Global.MaxRange -1 to 3 due to reduction in battle board size from Ken's 20x20 to Stars! 10x10 - Dan 07 Jul 11
        }

        /// <Summary>
        /// Test for DoBattle
        /// </Summary>
        [Test]
        public void Test6DoBattle()
        {
            Fleet stackA = zoneStacks[0] as Fleet;
            Fleet stackB = zoneStacks[1] as Fleet;

            double distanceS, distanceE;

            distanceS = PointUtilities.Distance(stackA.Position, stackB.Position);

            battleEngine.DoBattle(zoneStacks);

            distanceE = PointUtilities.Distance(stackA.Position, stackB.Position);

            // Check that the fleets have moved towards each other.

            Assert.Greater(distanceS, distanceE);
        }
    }
}
