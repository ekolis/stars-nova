// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Test of Defense coverage.
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
// Unit test for Defense coverage calculations
// ============================================================================

   [TestFixture]
   public class DefensesTest
   {
      [Test]
      public void DefenseCoverageTest()
      {
         Star star        = new Star();
         star.DefenseType = "Neutron";
         star.Defenses    = 100;

         Defenses.ComputeDefenseCoverage(star);

         Assert.AreEqual(0.9791, Defenses.PopulationCoverage, 0.001);
         Assert.AreEqual(0.8524, Defenses.SmartBombCoverage,  0.001);
      }
   }
}     
      
