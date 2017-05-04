#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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
    using Nova.Common;
    using NUnit.Framework;

    // TODO Clean up unit test. Using magic numbers is not recommended.
    [TestFixture]
    public class StarTest
    {
        private Star star = new Star();
        private Race race = new Race();
        private double habitalValue;

        [SetUp]
        public void Init()
        {
            race.RadiationTolerance.Immune = false;
            race.TemperatureTolerance.Immune = false;
            race.GravityTolerance.Immune = false;
            race.Traits.Remove("TT");
        }

        /// <summary>
        /// Test population growth for a negative hab planet.
        /// </summary>
        /// <returns>The amount of popluation added (may be negative).</returns>
        [Test]
        public double NegativeHabPopGrowth()
        {
            // setup the star
            star.Colonists = 100000;
            star.Gravity = 10;
            star.Radiation = 10;
            star.Temperature = 10;

            // setup the race
            race.GrowthRate = 10; // 10% growth
            race.Traits.SetPrimary("SS"); // avoid the JoAT and HE complications

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(-1500, growth);

            return growth;
        }

        // Tests for population growth
        [Test]
        public int LowPopGrowth()
        {
            // setup the star
            star.Colonists = 100000;
            star.Gravity = 50;
            star.Radiation = 50;
            star.Temperature = 50;

            // set a growth rate
            race.GrowthRate = 10; // 10% growth

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(10000, growth);

            return growth;
        }

        [Test]
        public int CrowdingPopGrowth()
        {
            // setup the star
            star.Colonists = 500000;
            star.Gravity = 50;
            star.Radiation = 50;
            star.Temperature = 50;

            // setup the race
            race.GrowthRate = 10; // 10% growth
            race.Traits.SetPrimary("SS"); // avoid the JoAT and HE complications

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(22200, growth);

            return growth;
        }

        [Test]
        public int MaxPopGrowth()
        {
            // setup the star
            star.Colonists = 1000000;
            star.Gravity = 50;
            star.Radiation = 50;
            star.Temperature = 50;

            // setup the race
            race.GrowthRate = 10; // 10% growth
            race.Traits.SetPrimary("SS"); // avoid the JoAT and HE complications

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(0, growth);

            return growth;
        }

        [Test]
        public int OvercrowdedPopGrowth()
        {
            // setup the star
            star.Colonists = 1500000;
            star.Gravity = 50;
            star.Radiation = 50;
            star.Temperature = 50;

            // setup the race
            race.GrowthRate = 10; // 10% growth
            race.Traits.SetPrimary("SS"); // avoid the JoAT and HE complications

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(-30000, growth);

            return growth;
        }

        [Test]
        public int VeryOvercrowdedPopGrowth()
        {
            // setup the star
            star.Colonists = 5000000;
            star.Gravity = 50;
            star.Radiation = 50;
            star.Temperature = 50;

            // setup the race
            race.GrowthRate = 10; // 10% growth
            race.Traits.SetPrimary("SS"); // avoid the JoAT and HE complications

            // run the growth calculation
            int growth = star.CalculateGrowth(race);

            // check the growth
            Assert.AreEqual(-600000, growth);

            return growth;
        }

        [Test]
        public void HabitalValue_AllImmune()
        {
            star.Radiation = 100;
            star.Gravity = 1;
            star.Temperature = 17;
            race.RadiationTolerance.Immune = true;
            race.TemperatureTolerance.Immune = true;
            race.GravityTolerance.Immune = true;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(1.0, habitalValue);
        }

        [Test]
        public void HabitalValue_100()
        {
            star.Radiation = 90;
            star.Gravity = 1;
            star.Temperature = 17;
            race.RadiationTolerance.MaximumValue = 91;
            race.RadiationTolerance.MinimumValue = 89;
            race.TemperatureTolerance.MaximumValue = 18;
            race.TemperatureTolerance.MinimumValue = 16;
            race.GravityTolerance.MaximumValue = 2;
            race.GravityTolerance.MinimumValue = 0;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(1.0, habitalValue);
        }

        [Test]
        public void HabitalValue_n15()
        {
            star.Radiation = 100;
            star.Gravity = 1;
            star.Temperature = 17;
            race.RadiationTolerance.MaximumValue = 3;
            race.RadiationTolerance.MinimumValue = 1;
            race.TemperatureTolerance.MaximumValue = 18;
            race.TemperatureTolerance.MinimumValue = 16;
            race.GravityTolerance.MaximumValue = 2;
            race.GravityTolerance.MinimumValue = 0;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(-0.15, habitalValue);
        }

        [Test]
        public void HabitalValue_n45()
        {
            star.Radiation = 100;
            star.Gravity = 1;
            star.Temperature = 17;
            race.RadiationTolerance.MaximumValue = 53;
            race.RadiationTolerance.MinimumValue = 51;
            race.TemperatureTolerance.MaximumValue = 58;
            race.TemperatureTolerance.MinimumValue = 56;
            race.GravityTolerance.MaximumValue = 60;
            race.GravityTolerance.MinimumValue = 50;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(-0.45, habitalValue);
        }

        [Test]
        public void HabitalValue_n90()
        {
            star.Radiation = 1;
            star.Gravity = 1;
            star.Temperature = 1;
            race.Traits.Add("TT");
            race.RadiationTolerance.MaximumValue = 70;
            race.RadiationTolerance.MinimumValue = 60;
            race.TemperatureTolerance.MaximumValue = 70;
            race.TemperatureTolerance.MinimumValue = 60;
            race.GravityTolerance.MaximumValue = 70;
            race.GravityTolerance.MinimumValue = 60;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(-0.90, habitalValue);
        }

        [Test]
        public void HabitalValue_n10()
        {
            star.Radiation = 50;
            star.Gravity = 10;
            star.Temperature = 17;
            race.RadiationTolerance.MaximumValue = 45;
            race.RadiationTolerance.MinimumValue = 43;
            race.TemperatureTolerance.MaximumValue = 18;
            race.TemperatureTolerance.MinimumValue = 16;
            race.GravityTolerance.MaximumValue = 5;
            race.GravityTolerance.MinimumValue = 0;
            habitalValue = race.HabitalValue(star);
            Assert.AreEqual(-0.10, habitalValue);
        }
    }
}
