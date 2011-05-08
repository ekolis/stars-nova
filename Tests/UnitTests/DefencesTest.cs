#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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

#region Module Description
// ===========================================================================
// Test of Defense coverage.
// ===========================================================================
#endregion

using Nova.Common;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Unit test for Defense coverage calculations
    /// </summary>
    /// ----------------------------------------------------------------------------
    [TestFixture]
    public class DefensesTest
    {
        /// <summary>
        /// Defense coverage test.
        /// </summary>
        [Test]
        public void DefenseCoverageTest()
        {
            const int InitialDefenseCount = 100;

            Star star = new Star();
            star.DefenseType = "Neutron";
            star.Defenses = InitialDefenseCount;

            Defenses.ComputeDefenseCoverage(star);

            Assert.AreEqual(0.9791, Defenses.PopulationCoverage, 0.001);
            Assert.AreEqual(0.8524, Defenses.SmartBombCoverage, 0.001);
        }
    }
}

