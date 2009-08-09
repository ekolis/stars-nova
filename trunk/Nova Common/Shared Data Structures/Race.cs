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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.IO.Compression;
using System.Web.Security;
using NovaCommon;


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

      // ============================================================================
      // Initialising Constructor from an xml node.
      // Precondition: node is a "EnvironmentTolerance" node in a Nova compenent definition file (xml document).
      // ============================================================================
      public EnvironmentTolerance(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (node.Name.ToLower())
                  {
                      case "min": Minimum = double.Parse(((XmlText)node.FirstChild).Value); break;
                      case "max": Minimum = double.Parse(((XmlText)node.FirstChild).Value); break;
                  }
              }
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
          }

      }

      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelEnvironmentTolerance = xmldoc.CreateElement("EnvironmentTolerance");

          Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Min", Minimum.ToString());
          Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Max", Maximum.ToString());
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


      public Race() { } // required for searializable class

      /// <summary>
      /// constructor for Race. 
      /// Reads all the race data in from an xml formated save file.
      /// </summary>
      /// <param name="fileName">A nova save file containing a race.</param>
      public Race(String fileName)
      {
          XmlDocument xmldoc = new XmlDocument();
          FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
          GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);

          xmldoc.Load(fileName);  // uncompressed
          //xmldoc.Load(compressionStream); // compressed

          LoadRaceFromXml(xmldoc);

          fileStream.Close();
      }

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

          // Type; // Primary Racial Trait.
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

      /// <summary>
      /// Load a Race from an xml document 
      /// </summary>
      /// <param name="xmldoc">produced using XmlDocument.Load(), see Race constructor</param>
      private void LoadRaceFromXml(XmlDocument xmldoc)
      {
          Traits = new ArrayList();
          GravityTolerance = new EnvironmentTolerance();
          RadiationTolerance = new EnvironmentTolerance();
          TemperatureTolerance = new EnvironmentTolerance();

          XmlNode xmlnode = (XmlNode)xmldoc.DocumentElement;
          while (xmlnode != null)
          {
              try
              {
                  // Report.Information("node name = '" + xmlnode.Name + "'");
                  switch (xmlnode.Name.ToLower())
                  {
                      case "root": xmlnode = xmlnode.FirstChild; continue;
                      case "race": xmlnode = xmlnode.FirstChild; continue;
                      case "gravitytolerance": this.GravityTolerance = new EnvironmentTolerance(xmlnode); break;
                      case "Radiationtolerance": this.RadiationTolerance = new EnvironmentTolerance(xmlnode); break;
                      case "temperaturetolerance": this.TemperatureTolerance = new EnvironmentTolerance(xmlnode); break;
                      case "tech": this.ResearchCosts = new TechLevel(xmlnode); break;

                      case "lrt": this.Traits.Add(((XmlText)xmlnode.FirstChild).Value); break;

                      case "minebuildcost": this.MineBuildCost = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "prt": this.Type = ((XmlText)xmlnode.FirstChild).Value; break;
                      case "pluralname": this.PluralName = ((XmlText)xmlnode.FirstChild).Value; break;
                      case "name": this.Name = ((XmlText)xmlnode.FirstChild).Value; break;
                      case "password": this.Password = ((XmlText)xmlnode.FirstChild).Value; break;

                      // TODO - load the RaceIcon
                      case "raceiconname": this.Icon.Source = ((XmlText)xmlnode.FirstChild).Value; break;


                      case "factorybuildcost": this.FactoryBuildCost = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "colonistsperresource": this.ColonistsPerResource = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "factoryproduction": this.FactoryProduction = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "operablefactories": this.OperableFactories = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "mineproductionrate": this.MineProductionRate = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "operablemines": this.OperableMines = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;
                      case "growthrate": this.GrowthRate = int.Parse(((XmlText)xmlnode.FirstChild).Value); break;

                      default: break;
                  }

              }
              catch
              {
                  // If there are blank entries null reference exemptions will be raised here. It is safe to ignore them.
              }
              xmlnode = xmlnode.NextSibling;
          }
      }



   }
}
