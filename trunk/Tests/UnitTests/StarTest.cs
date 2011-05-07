#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
//
// This file is part of Stars-Nova.
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

using Nova.Common;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{
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

        [Test]
        public void HabitalValue_AllImmune()
        {
            star.Radiation = 100;
            star.Gravity = 1;
            star.Temperature = 17;
            race.RadiationTolerance.Immune = true;
            race.TemperatureTolerance.Immune = true;
            race.GravityTolerance.Immune = true;
            habitalValue = star.HabitalValue(race);
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
            habitalValue = star.HabitalValue(race);
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
            habitalValue = star.HabitalValue(race);
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
            habitalValue = star.HabitalValue(race);
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
            habitalValue = star.HabitalValue(race);
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
            habitalValue = star.HabitalValue(race);
            Assert.AreEqual(-0.10, habitalValue);
        }
    }
}
