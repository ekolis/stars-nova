using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Nova.Common;

namespace Nova.UnitTests
{
    [TestFixture] 
    public class StarTest
    {
        Star star = new Star();
        Race race = new Race();
        double habitalValue;

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
