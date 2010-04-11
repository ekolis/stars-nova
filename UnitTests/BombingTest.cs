// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2007 Ken Reed
//
// Test for the bombing code.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NUnit.Framework;
using NovaCommon;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace Nova.UnitTests
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
// a	0.025 * 10	0.25        10 Cherry bombs
// b	0.012 * 5	0.06        5 M-70 bombs
// c	a + b		0.31        Total kill factor
// d	1 - 0.97	0.0208      1 - defence factor for 100 neutron defences
// e    c * d           0.006448    Total kill factor
// f    pop * c         64.48       Total colonists killed
//
// Minimum kill:
//
// a 10*300 + 5*300  4500   
// b 1 - 0.97	     0.0208   1 - defence factor for 100 neutron defences
// c a *b            156      Total minimum kill
// ============================================================================

   [TestFixture]
   public class BombingTest
   {
      [Test]
      public void BombingEffect()
      {
         const double initial = 10000;
         Star star        = new Star();
         star.DefenseType = "Neutron";
         star.Defenses    = 100;
         star.Colonists   = (int) initial;

         Defenses.ComputeDefenseCoverage(star);

         // In line with the example in the FAQ bomb with 10 Cherry bombs and 5
         // M70s

         AllComponents.Restore();
         Hashtable allComponents = NovaCommon.AllComponents.Data.Components;
         Bomb      totalBombs    = new Bomb();
         Component cherryComponent = allComponents["Cherry Bomb"] as Component;
         Bomb cherry = cherryComponent.Properties["Bomb"] as Bomb;

         for (int i = 0; i < 10; i++) {
            totalBombs.PopKill       += cherry.PopKill;
            totalBombs.Installations += cherry.Installations;
            totalBombs.MinimumKill   += cherry.MinimumKill;
         }

         Component m70Component = allComponents["M-70 Bomb"] as Component;
         Bomb M70 = m70Component.Properties["Bomb"] as Bomb;

         for (int i = 0; i < 5; i++) {
            totalBombs.PopKill       += M70.PopKill;
            totalBombs.Installations += M70.Installations;
            totalBombs.MinimumKill   += M70.MinimumKill;
         }

         // Just verify the algorithm, not the whole routine

         double killFactor     = totalBombs.PopKill / 100;
         double defenceFactor  = 1.0 - Defenses.PopulationCoverage;
         double populationKill = killFactor * defenceFactor;
         double killed         = (double) star.Colonists * populationKill;

         double minKilled      = totalBombs.MinimumKill 
                               * (1 - Defenses.PopulationCoverage);

         int dead              = (int) Math.Max(killed, minKilled);

         Assert.AreEqual(0.006448, populationKill, 0.01);
         Assert.AreEqual(64.48,    killed,         1);
         Assert.AreEqual(94,       minKilled,      1);
         Assert.AreEqual(94,       dead,           1);
      }
   }
}     
      
