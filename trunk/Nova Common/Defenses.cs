// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module returns details of a planet's Defenses
// TODO (priority 3) - Transfer this to the Defense component property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace NovaCommon
{

// ============================================================================
// Definition of Defense technology capabilities
// ============================================================================

   public static class Defenses
   {
      public static Hashtable DefenseTypes       = new Hashtable();
      public static double    PopulationCoverage = 0;
      public static double    BuildingCoverage   = 0;
      public static double    InvasionCoverage   = 0;
      public static double    SmartBombCoverage  = 0;
      public static int       SummaryCoverage    = 0;

      static Defenses()
      {
         DefenseTypes["SDI"]     = 0.0099;   // 0.99%
         DefenseTypes["Missile"] = 0.0199;   // 1.99%
         DefenseTypes["Laser"]   = 0.0239;   // 2.39%
         DefenseTypes["Planet"]  = 0.0299;   // 2.99%
         DefenseTypes["Neutron"] = 0.0379;   // 3.79%
      }


// ============================================================================
// Determine the Defenses of a planet. Note: results are normalised so that
// 100% = 1.0.
// ============================================================================

      public static void ComputeDefenseCoverage(Star star)
      {
         if (star.DefenseType == "None") {
            PopulationCoverage = 0;
            BuildingCoverage   = 0;
            InvasionCoverage   = 0;
            SmartBombCoverage  = 0;
            SummaryCoverage    = 0;
            return;
         }

         double baseLevel = (double) DefenseTypes[star.DefenseType];

         PopulationCoverage = 1.0 - Math.Pow((1.0 - baseLevel), star.Defenses);
         BuildingCoverage   = PopulationCoverage * 0.5;
         InvasionCoverage   = PopulationCoverage * 0.75;

         // Coverage against smart bombs is calculated slightly differently

         baseLevel *= 0.5;
         SmartBombCoverage = 1.0 - Math.Pow((1.0 - baseLevel), star.Defenses);

         double summary = ((BuildingCoverage + PopulationCoverage
                           + InvasionCoverage) / 3) * 100;

         SummaryCoverage = (int) summary;
      }
   }
}     
      
