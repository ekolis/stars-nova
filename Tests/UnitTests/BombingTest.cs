#region Copyright Notice
// ============================================================================
// Copyright (C) 2007, 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// Test for the bombing code.
// ===========================================================================
#endregion

using System;
using Nova.Common;
using Nova.Common.Components;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{


    // ============================================================================
    // Algorithms:
    // Normalpopkills = sum[bomb_kill_perc(n)*#(n)] * (1-Def(pop))
    // Minkills = sum[bomb_kill_min(n)*#(n)] * (1-Def(pop))
    //
    // Unit test for bombing calculations. The figures used are based on the FAQ
    // configuration:
    //
    // 10 Cherry and 5 M-70 bombing vs 100 Neutron Defs (97.92%) 
    //
    // The calculations are, population kill:
    //
    // a    0.025 * 10  0.25        10 Cherry bombs
    // b    0.012 * 5   0.06        5 M-70 bombs
    // c    a + b       0.31        Total kill factor
    // d    1 - 0.97    0.0208      1 - defence factor for 100 neutron defences
    // e    c * d           0.006448    Total kill factor
    // f    pop * c         64.48       Total colonists killed
    //
    // Minimum kill:
    //
    // a 10*300 + 5*300  4500   
    // b 1 - 0.97        0.0208   1 - defence factor for 100 neutron defences
    // c a *b            156      Total minimum kill
    // ============================================================================

    [TestFixture]
    public class BombingTest
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Bombing Test
        /// 
        /// TODO Clean up unit test. Using logic (for-loops and Math.Max()) and magic numbers in tests is not recommended.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Test]
        public void BombingEffect()
        {
            const double Initial = 10000;
            Star star = new Star();
            star.DefenseType = "Neutron";
            star.Defenses = 100;
            star.Colonists = (int)Initial;

            Defenses.ComputeDefenseCoverage(star);

            // In line with the example in the FAQ bomb with 10 Cherry bombs and 5
            // M70s

            Bomb totalBombs = new Bomb();


            for (int i = 0; i < 10; i++)
            {
                totalBombs.PopKill += 2.5;
                totalBombs.Installations += 10;
                totalBombs.MinimumKill += 300;
            }


            for (int i = 0; i < 5; i++)
            {
                totalBombs.PopKill += 1.2;
                totalBombs.Installations += 6;
                totalBombs.MinimumKill += 300;
            }

            // Just verify the algorithm, not the whole routine

            double killFactor = totalBombs.PopKill / 100;
            double defenceFactor = 1.0 - Defenses.PopulationCoverage;
            double populationKill = killFactor * defenceFactor;
            double killed = (double)star.Colonists * populationKill;

            double minKilled = totalBombs.MinimumKill
                                  * (1 - Defenses.PopulationCoverage);

            int dead = (int)Math.Max(killed, minKilled);

            Assert.AreEqual(0.006448, populationKill, 0.01);
            Assert.AreEqual(64.48, killed, 1);
            Assert.AreEqual(94, minKilled, 1);
            Assert.AreEqual(94, dead, 1);
        }
    }
}

