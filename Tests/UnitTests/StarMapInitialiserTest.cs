using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Tests.UnitTests
{
    using NUnit.Framework;
    using Nova.Common;
    using Nova.Server;
    using Nova.Server.NewGame;


    class TestRace : Nova.Common.Race
    {
        public int TestAdvantagePoints = 0;

        public override int GetAdvantagePoints()
        {
            return TestAdvantagePoints;
        }
    }


    [TestFixture]
    class HomeStarLeftoverpointsAdjusterTest
    {
        private TestRace testRace;
        private Star star;

        [SetUp]
        public void TestSetUp()
        {
            testRace = new TestRace();
            star = new Star();
        }

        [Test]
        public void LeftoverAdvantagePointMinimumTest()
        {
            testRace.TestAdvantagePoints = -1;
            Race race = testRace;
            Assert.AreEqual(-1, race.GetAdvantagePoints());
            Assert.AreEqual(0, race.GetLeftoverAdvantagePoints());
        }

        [Test]
        public void LeftoverAdvantagePointMaximumTest()
        {
            testRace.TestAdvantagePoints = 51;
            Race race = testRace;
            Assert.AreEqual(51, race.GetAdvantagePoints());
            Assert.AreEqual(50, race.GetLeftoverAdvantagePoints());
        }

        [Test]
        public void LeftoverAdvantagePointExactTest()
        {
            testRace.TestAdvantagePoints = 1;
            Race race = testRace;
            Assert.AreEqual(1, race.GetAdvantagePoints());
            Assert.AreEqual(1, race.GetLeftoverAdvantagePoints());
        }

        [Test]
        public void NoMineralConcentration()
        {
            testRace.TestAdvantagePoints = 0;
            testRace.LeftoverPointTarget = "Mineral concentration";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.MineralConcentration.Boranium + star.MineralConcentration.Germanium + star.MineralConcentration.Ironium);

            testRace.TestAdvantagePoints = 2;
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.MineralConcentration.Boranium + star.MineralConcentration.Germanium + star.MineralConcentration.Ironium);
        }

        [Test]
        public void OneMineralConcentration()
        {
            testRace.TestAdvantagePoints = 3;
            testRace.LeftoverPointTarget = "Mineral concentration";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(1, star.MineralConcentration.Boranium + star.MineralConcentration.Germanium + star.MineralConcentration.Ironium);
        }

        [Test]
        public void CorrectMineralConcentration()
        {
            star.MineralConcentration.Boranium = 10;
            star.MineralConcentration.Germanium = 10;
            star.MineralConcentration.Ironium = 0;
            testRace.TestAdvantagePoints = 3;
            testRace.LeftoverPointTarget = "Mineral concentration";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(1, star.MineralConcentration.Ironium);
        }

        [Test]
        public void NoMines()
        {
            testRace.TestAdvantagePoints = 0;
            testRace.LeftoverPointTarget = "Mines";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Mines);
            testRace.TestAdvantagePoints = 1;
            testRace.LeftoverPointTarget = "Mines";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Mines);
        }

        [Test]
        public void SomeMines()
        {
            testRace.TestAdvantagePoints = 10;
            testRace.LeftoverPointTarget = "Mines";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(5, star.Mines);
        }

        [Test]
        public void NoFactories()
        {
            testRace.TestAdvantagePoints = 0;
            testRace.LeftoverPointTarget = "Factories";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Factories);
            testRace.TestAdvantagePoints = 4;
            testRace.LeftoverPointTarget = "Factories";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Factories);
        }

        [Test]
        public void SomeFactories()
        {
            testRace.TestAdvantagePoints = 11;
            testRace.LeftoverPointTarget = "Factories";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(2, star.Factories);
        }

        [Test]
        public void NoDefenses()
        {
            testRace.TestAdvantagePoints = 0;
            testRace.LeftoverPointTarget = "Defenses";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Defenses);
            testRace.TestAdvantagePoints = 9;
            testRace.LeftoverPointTarget = "Defenses";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.Defenses);
        }

        [Test]
        public void SomeDefenses()
        {
            testRace.TestAdvantagePoints = 29;
            testRace.LeftoverPointTarget = "Defenses";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(2, star.Defenses);
        }


        [Test]
        public void NoSurfaceMinerals()
        {
            testRace.TestAdvantagePoints = 0;
            testRace.LeftoverPointTarget = "Surface minerals";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(0, star.ResourcesOnHand.Boranium);
            Assert.AreEqual(0, star.ResourcesOnHand.Germanium);
            Assert.AreEqual(0, star.ResourcesOnHand.Ironium);
        }

        [Test]
        public void SomeSurfaceMinerals()
        {
            star.ResourcesOnHand.Boranium = 10;
            star.ResourcesOnHand.Germanium = 10;
            star.ResourcesOnHand.Ironium = 10;
            testRace.TestAdvantagePoints = 3;
            testRace.LeftoverPointTarget = "Surface minerals";
            HomeStarLeftoverpointsAdjuster.Adjust(star, testRace);
            Assert.AreEqual(20, star.ResourcesOnHand.Boranium, "Boranium, Sum: " + (star.ResourcesOnHand.Boranium + star.ResourcesOnHand.Germanium + star.ResourcesOnHand.Ironium));
            Assert.AreEqual(20, star.ResourcesOnHand.Germanium, "Germanium");
            Assert.AreEqual(20, star.ResourcesOnHand.Ironium, "Ironium");
        }
    }
}
