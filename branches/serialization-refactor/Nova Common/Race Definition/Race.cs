// ===========================================================================
// Nova. (c) 2008 Ken Reed.
//
// This module defines all the parameters that define the characteristics of a
// race. These values are all set in the race designer. This object also manages
// the loading ans saving of race data to a file.
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
using System.Xml.Schema;
using NovaCommon;
using System.Xml.Serialization;


namespace NovaCommon
{



// ===========================================================================
// All of the race parameters
// ===========================================================================

   [Serializable]
   public sealed class Race : IXmlSerializable
   {
      public EnvironmentTolerance  GravityTolerance = new EnvironmentTolerance();
      public EnvironmentTolerance  RadiationTolerance = new EnvironmentTolerance();
      public EnvironmentTolerance  TemperatureTolerance = new EnvironmentTolerance();

      public TechLevel             ResearchCosts = new TechLevel(0);

      // public string                Type; // use this.Traits.Primary or this.Traits.SetPrimary()
      public RacialTraits Traits = new RacialTraits(); // Collection of all the race's traits, including the primary.
      public int                   MineBuildCost;

      public string                PluralName;
      public string                Name;
      public string                Password;
      public RaceIcon              Icon = new RaceIcon(); 

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

      // required for searializable class
      public Race() {} 

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

      public bool HasTrait(string trait)
      {
          if (trait == Traits.Primary) return true;
          if (Traits == null) return false;
          return this.Traits.Contains(trait);
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

      /// <summary>
      /// Load a Race from an xml document 
      /// </summary>
      /// <param name="xmldoc">produced using XmlDocument.Load(), see Race constructor</param>
      private void LoadRaceFromXml(XmlDocument xmldoc)
      {
          
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
                      case "gravitytolerance": this.GravityTolerance = new EnvironmentTolerance(xmlnode.FirstChild); break;
                      case "radiationtolerance": this.RadiationTolerance = new EnvironmentTolerance(xmlnode.FirstChild); break;
                      case "temperaturetolerance": this.TemperatureTolerance = new EnvironmentTolerance(xmlnode.FirstChild); break;
                      case "tech": this.ResearchCosts = new TechLevel(xmlnode); break;

                      case "lrt": this.Traits.Add(((XmlText)xmlnode.FirstChild).Value); break;

                      case "minebuildcost": this.MineBuildCost = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "prt": this.Traits.SetPrimary(((XmlText)xmlnode.FirstChild).Value); break;
                      case "pluralname": this.PluralName = ((XmlText)xmlnode.FirstChild).Value; break;
                      case "name": this.Name = ((XmlText)xmlnode.FirstChild).Value; break;
                      case "password": this.Password = ((XmlText)xmlnode.FirstChild).Value; break;

                      // TODO (priority 3) - load the RaceIcon
                      case "raceiconname": this.Icon.Source = ((XmlText)xmlnode.FirstChild).Value; break;


                      case "factorybuildcost": this.FactoryBuildCost = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "colonistsperresource": this.ColonistsPerResource = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "factoryproduction": this.FactoryProduction = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "operablefactories": this.OperableFactories = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "mineproductionrate": this.MineProductionRate = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "operablemines": this.OperableMines = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                      case "growthrate": this.GrowthRate = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;

                      default: break;
                  }

              }
              catch (Exception e)
              {
                  Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
              }

              xmlnode = xmlnode.NextSibling;
          }
      }


       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Race
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Race");

           // GravityTolerance
           writer.WriteStartElement("GravityTolerance");
           GravityTolerance.WriteXml(writer);
           writer.WriteEndElement();
           // RadiationTolerance
           writer.WriteStartElement("RadiationTolerance");
           RadiationTolerance.WriteXml(writer);
           writer.WriteEndElement();
           // TemperatureTolerance
           writer.WriteStartElement("TemperatureTolerance");
           TemperatureTolerance.WriteXml(writer);
           writer.WriteEndElement();
           // Tech
           ResearchCosts.WriteXml(writer);

           // Type; // Primary Racial Trait.
           writer.WriteElementString("PRT", Traits.Primary.Code); // TODO (priority 5) check the PRT is saved/loaded properly into Traits
           // Traits
           foreach (TraitEntry trait in Traits)
           {
               if (AllTraits.Data.Primary.Contains(trait.Code)) continue; // Skip the PRT, just add LRTs here.
               writer.WriteElementString("LRT", trait.Code);
           }

           writer.WriteElementString("MineBuildCost", MineBuildCost.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("PluralName", PluralName);
           writer.WriteElementString("Name", Name);
           writer.WriteElementString("Password", Password);
           writer.WriteElementString("RaceIconName", Icon.Source);
           writer.WriteElementString("FactoryBuildCost", FactoryBuildCost.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("ColonistsPerResource", ColonistsPerResource.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("FactoryProduction", FactoryProduction.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("OperableFactories", OperableFactories.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("MineProductionRate", MineProductionRate.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("OperableMines", OperableMines.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("MaxPopulation", MaxPopulation.ToString(System.Globalization.CultureInfo.InvariantCulture));
           writer.WriteElementString("GrowthRate", GrowthRate.ToString(System.Globalization.CultureInfo.InvariantCulture));

           writer.WriteEndElement();
       }
   }
}
