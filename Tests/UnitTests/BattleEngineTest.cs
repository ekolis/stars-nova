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
        private const int Player1Id = 1;
        private const int Player2Id = 2;
        private const int Player3Id = 3;

        private ServerData serverState = new ServerData();
        private BattleEngine battleEngine;

        private Fleet fleet1 = new Fleet("fleet1", Player1Id, 1, new Point(100, 200));
        private Fleet fleet2 = new Fleet("fleet2", Player2Id, 1, new Point(100, 200));
        private Fleet fleet3 = new Fleet("fleet3", Player3Id, 1, new Point(300, 400));
        private Fleet fleet4 = new Fleet("fleet4", Player3Id, 2, new Point(300, 400));

        private ShipDesign cruiser = new ShipDesign(12345);
        private ShipDesign frigate = new ShipDesign(67890);

        private ShipToken token1;
        private ShipToken token2;
        private ShipToken token3;
        private ShipToken token4;

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
            battleEngine = new BattleEngine(serverState, new BattleReport());
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

            serverState.AllEmpires[empireData1.Id] = empireData1;
            serverState.AllEmpires[empireData2.Id] = empireData2;
            serverState.AllEmpires[empireData3.Id] = empireData3;

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

            cruiser.Blueprint = shipHull;
            cruiser.Name = "Cruiser";

            frigate.Blueprint = shipHull;
            frigate.Name = "Frigate";

            token1 = new ShipToken(cruiser, 1);
            token2 = new ShipToken(frigate, 1);
            token3 = new ShipToken(cruiser, 1);
            token4 = new ShipToken(frigate, 1);

            token1.Armor = 100;
            token2.Armor = 200;
            token3.Armor = 100;
            token4.Armor = 200;

            fleet1.Tokens.Add(token1);
            fleet2.Tokens.Add(token2);
            fleet3.Tokens.Add(token3);
            fleet4.Tokens.Add(token4);

            serverState.AllEmpires[Player1Id].OwnedFleets[fleet1.Key] = fleet1;
            serverState.AllEmpires[Player2Id].OwnedFleets[fleet2.Key] = fleet2;
            serverState.AllEmpires[Player3Id].OwnedFleets[fleet3.Key] = fleet3;
            serverState.AllEmpires[Player3Id].OwnedFleets[fleet4.Key] = fleet4;
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
