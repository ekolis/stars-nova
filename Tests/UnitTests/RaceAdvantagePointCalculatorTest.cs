using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Nova.Common;
using Nova.Common.RaceDefinition;

namespace Nova.Tests.UnitTests
{
    [TestFixture]
    public class RaceAdvantagePointCalculatorTest
    {
        private Race race;
        private RaceAdvantagePointCalculator raceAdvantagePointCalculator;
        private int result1;

        [SetUp]
        public void Init()
        {
            raceAdvantagePointCalculator = new RaceAdvantagePointCalculator();
            
            race = new Race();

            race.Traits.SetPrimary("JOAT");

            // race.Traits.Add(); // no LRTs

            race.GravityTolerance.MinimumValue = 15;
            race.GravityTolerance.MaximumValue = 85;
            race.GravityTolerance.Immune = false;
            race.TemperatureTolerance.MinimumValue = 15;
            race.TemperatureTolerance.MaximumValue = 85;
            race.TemperatureTolerance.Immune = false;
            race.RadiationTolerance.MinimumValue = 15;
            race.RadiationTolerance.MaximumValue = 85;
            race.RadiationTolerance.Immune = false;
            race.GrowthRate = 15;

            race.ColonistsPerResource = 1000;
            race.FactoryProduction = 10;
            race.OperableFactories = 10;
            race.FactoryBuildCost = 10;
            // factories less ger
            race.MineProductionRate = 10;
            race.OperableMines = 10;
            race.MineBuildCost = 5;

            race.ResearchCosts[TechLevel.ResearchField.Energy] = 100;
            race.ResearchCosts[TechLevel.ResearchField.Weapons] = 100;
            race.ResearchCosts[TechLevel.ResearchField.Propulsion] = 100;
            race.ResearchCosts[TechLevel.ResearchField.Construction] = 100;
            race.ResearchCosts[TechLevel.ResearchField.Electronics] = 100;
            race.ResearchCosts[TechLevel.ResearchField.Biotechnology] = 100;

            result1 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
        }

        [Test]
        public void calculateAdvantagePoints()
        {
            int result2 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.AreEqual(result1, result2, "two runs with same race are not equal");

            // ColonistsPerResource 1000->700
            race.ColonistsPerResource = 700;
            int result3 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs(result1 - (result3 + (2400 / 3))) <= 1, "Colonists per resource = 700, with rounding");
            race.ColonistsPerResource = 1000;

            // Cheap Factories
            race.Traits.Add("CF"); // Cheap Factories
            int result4 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs(result1 - (result4 + (175 / 3))) <= 1);
            race.Traits.Remove("CF");

            // PRT JOAT->IT
            race.Traits.SetPrimary("IT");
            int result5 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs((result1 - 66/3) - (result5 + 180 / 3)) <= 1);
            race.Traits.SetPrimary("JOAT");
        }

         [Test]
        public void calculateAdvantagePointsForExpensiveTech()
        {
            // Expensive Techs start at 3
            race.Traits.Add("ExtraTech");
            int result6 = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs(result1 - (result6 + 180 / 3)) <= 1, "Extra Tech normal: " + result1 + " expected: " + (result1 - 180 / 3) + " was: " + result6);
        }

        [Test]
        public void calculateAdvantagePointsForLrtIFE()
        {
            race.Traits.Add("IFE");
            int resultX = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs(result1 - (resultX + 235 / 3)) <= 1, "LRT Test for IFE: normal: " + result1 + " expected: " + (result1 - (235 / 3)).ToString() + " was: " + resultX.ToString());
        }

        [Test]
        public void calculateAdvantagePointsForStandardJoat()
        {
            Assert.IsTrue(Math.Abs(result1 - 25) <= 1, "was: " + result1 + " expected: 25");
        }

        [Test]
        public void calculateAdvantagePointsFor3ImmuneJoat()
        {
            race.GravityTolerance.Immune = true;
            race.TemperatureTolerance.Immune = true;
            race.RadiationTolerance.Immune = true;
            int resultY = raceAdvantagePointCalculator.calculateAdvantagePoints(race);
            Assert.IsTrue(Math.Abs(resultY + 3900) <= 1, "normal:" + result1 + ", freestars: " + resultY + " expected: -3900");
        }
    }
}
