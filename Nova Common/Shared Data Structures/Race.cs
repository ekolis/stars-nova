// This file needs -*- c++ -*- mode
// ===========================================================================
// Nova. (c) 2008 Ken Reed.
//
// This module defines all the parameters that define the characteristics of a
// race. These values are all set in the race designer and saved to a file.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.Collections;
using System.Drawing;
using System.Xml;
using NovaCommon.Shared_Data_Structures;


namespace NovaCommon
{


// ===========================================================================
// Class to hold environmental tolerance details
// ===========================================================================

   [Serializable]
   public sealed class EnvironmentTolerance
   {
      public double Minimum = 0;
      public double Maximum = 0;

      public EnvironmentTolerance() { } // required for serialization
      public EnvironmentTolerance(double minv, double maxv)
      {
         Minimum = minv;
         Maximum = maxv;
      }
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelEnvironmentTolerance = xmldoc.CreateElement("EnvironmentTolerance");

          Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Min", Minimum.ToString());
          Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "max", Maximum.ToString());
          return xmlelEnvironmentTolerance;
      }
   }


// ===========================================================================
// All of the race parameters
// ===========================================================================

   [Serializable]
   public sealed class Race
   {
      public EnvironmentTolerance  GravityTolerance;
      public EnvironmentTolerance  RadiationTolerance;
      public EnvironmentTolerance  TemperatureTolerance;

      public TechLevel             ResearchCosts = new TechLevel(0);

      public ArrayList             Traits; // List of lesser racial traits: A list of TraitEntry.Code values
      public int                   MineBuildCost;

      public string                Type; // Primary racial trait.
      public string                PluralName;
      public string                Name;
      public string                Password;
      public RaceIcon              Icon; 

      // These parameters affect the production rate of each star (used in the
      // Star class Update method).
      public int                   FactoryBuildCost;
      public double                ColonistsPerResource;
      public double                FactoryProduction;
      public double                OperableFactories;
      public double                MineProductionRate;
      public double                OperableMines;
      public double                MaxPopulation = 1000000;
      public double                GrowthRate;


// ===========================================================================
// Return the median value of an integer range.
// ===========================================================================

      private int Median(EnvironmentTolerance tolerance)
      {
         int  result = (int) (((tolerance.Maximum - tolerance.Minimum) / 2) 
                     + tolerance.Minimum); 

         return result;
      }


// ===========================================================================
// Return the optimum radiation level as a percentage for this race. This is
// simply the median value as radiation levels run from 0 to 100;
// ===========================================================================

      public int OptimumRadiationLevel
      {
         get { return Median(RadiationTolerance); }
      }


// ===========================================================================
// Return the optimum temperature level as a percentage for this
// race. Temperature values range from -200 to + 200
// ===========================================================================

      public int OptimumTemperatureLevel
      {
         get { return (200 + Median(TemperatureTolerance)) / 4; }
      }


// ===========================================================================
// Return the optimum gravity level as a percentage for this race. Gravity
// values range from 0 to 10;
// ===========================================================================

      public int OptimumGravityLevel
      {
         get { return Median(GravityTolerance) * 10; }
      }

      public bool HasTrait(string trait)
      {
          if (Type == trait) return true;

          return Traits.Contains(trait);
      }

      // ============================================================================
      // Return an XmlElement representation of the Race
      // ============================================================================
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelRace = xmldoc.CreateElement("Race");

          // GravityTolerance
          XmlElement xmlelGravityTolerance = xmldoc.CreateElement("GravityTolerance");
          xmlelGravityTolerance.AppendChild(this.GravityTolerance.ToXml(xmldoc));
          xmlelRace.AppendChild(xmlelGravityTolerance);
          // RadiationTolerance
          XmlElement xmlelRadiationTolerance = xmldoc.CreateElement("RadiationTolerance");
          xmlelRadiationTolerance.AppendChild(this.RadiationTolerance.ToXml(xmldoc));
          xmlelRace.AppendChild(xmlelRadiationTolerance);
          // TemperatureTolerance
          XmlElement xmlelTemperatureTolerance = xmldoc.CreateElement("TemperatureTolerance");
          xmlelTemperatureTolerance.AppendChild(this.TemperatureTolerance.ToXml(xmldoc));
          xmlelRace.AppendChild(xmlelTemperatureTolerance);
          // Tech
          xmlelRace.AppendChild(this.ResearchCosts.ToXml(xmldoc));

          // Traits
          foreach (String trait in Traits)
          {
              Global.SaveData(xmldoc, xmlelRace, "LRT", trait);
          }

          // MineBuildCost
          Global.SaveData(xmldoc, xmlelRace, "MineBuildCost", MineBuildCost.ToString());

          // Type; // Primary racial trait.
          Global.SaveData(xmldoc, xmlelRace, "PRT", Type);
          // Plural Name
          Global.SaveData(xmldoc, xmlelRace, "PluralName", PluralName);
          // Name
          Global.SaveData(xmldoc, xmlelRace, "Name", Name);
          // Password 
          Global.SaveData(xmldoc, xmlelRace, "Password", Password);
          // RaceIconName
          Global.SaveData(xmldoc, xmlelRace, "RaceIconName", Icon.Source);
          // Factory Build Cost
          Global.SaveData(xmldoc, xmlelRace, "FactoryBuildCost", FactoryBuildCost.ToString());
          // ColonistsPerResource
          Global.SaveData(xmldoc, xmlelRace, "ColonistsPerResource", ColonistsPerResource.ToString());
          // FactoryProduction
          Global.SaveData(xmldoc, xmlelRace, "FactoryProduction", FactoryProduction.ToString());
          // OperableFactories
          Global.SaveData(xmldoc, xmlelRace, "OperableFactories", OperableFactories.ToString());
          // MineProductionRate
          Global.SaveData(xmldoc, xmlelRace, "MineProductionRate", MineProductionRate.ToString());
          // OperableMines
          Global.SaveData(xmldoc, xmlelRace, "OperableMines", OperableMines.ToString());
          // MaxPopulation
          Global.SaveData(xmldoc, xmlelRace, "MaxPopulation", MaxPopulation.ToString());
          // GrowthRate
          Global.SaveData(xmldoc, xmlelRace, "GrowthRate", GrowthRate.ToString());

          return xmlelRace;
      }
   }
}
