#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// This module defines all the parameters that define the characteristics of a
// race. These values are all set in the race designer. This object also manages
// the loading ans saving of race data to a file.
// ===========================================================================
#endregion

using System;
using System.IO;
using System.Xml;

namespace Nova.Common
{

    /// ----------------------------------------------------------------------------
    /// /// <summary>
    /// All of the race parameters
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public sealed class Race
    {
        public EnvironmentTolerance GravityTolerance = new EnvironmentTolerance();
        public EnvironmentTolerance RadiationTolerance = new EnvironmentTolerance();
        public EnvironmentTolerance TemperatureTolerance = new EnvironmentTolerance();

        public TechLevel ResearchCosts = new TechLevel(0);

        public RacialTraits Traits = new RacialTraits(); // Collection of all the race's traits, including the primary.
        public int MineBuildCost;

        public string PluralName;
        public string Name;
        public string Password;
        public RaceIcon Icon = new RaceIcon();

        // These parameters affect the production rate of each star (used in the
        // Star class Update method).
        public int FactoryBuildCost;        // defined in the Race Designer as the amount of Resourcesrequired to build one factory
        public double ColonistsPerResource;
        public double FactoryProduction;    // defined in the Race Designer as the amount of resources produced by 10 factories
        public double OperableFactories;
        public double MineProductionRate;   // defined in the Race Designer as the amount of minerals (kT) mined by every 10 mines
        public double OperableMines;

        public double GrowthRate;

        #region Construction

        // required for searializable class
        public Race() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// constructor for Race. 
        /// Reads all the race data in from an xml formated save file.
        /// </summary>
        /// <param name="fileName">A nova save file containing a race.</param>
        /// ----------------------------------------------------------------------------
        public Race(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            xmldoc.Load(fileName);
            XmlNode xmlnode = xmldoc.DocumentElement;
            LoadRaceFromXml(xmlnode);

            fileStream.Close();
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the number of resources this race requires to construct a factory.
        /// </summary>
        /// <returns>The number of resources this race requires to construct a factory.</returns>
        /// ----------------------------------------------------------------------------
        public Resources GetFactoryResources()
        {
            int factoryBuildCostGerm = HasTrait("CF") ? 3 : 4;
            return new Resources(0, 0, factoryBuildCostGerm, FactoryBuildCost);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the number of resources this race requires to construct a mine.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources GetMineResources()
        {
            return new Resources(0, 0, 0, MineBuildCost);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if this race has a given trait.
        /// </summary>
        /// <param name="trait">A string representing a primary or secondary trait. 
        /// See AllTraits.TraitKeys for examples.</param>
        /// <returns>true if this race has the given trait.</returns>
        /// ----------------------------------------------------------------------------
        public bool HasTrait(string trait)
        {
            if (trait == Traits.Primary) return true;
            if (Traits == null) return false;
            return this.Traits.Contains(trait);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the Median value of an integer range.
        /// </summary>
        /// <param name="tolerance">An <see cref="EnvironmentTolerance"/> to determine the Median of.</param>
        /// <remarks>
        /// FIXME (priority 3) - Mathematically this finds the mean, which in some
        /// circumstances is different from the Median. 
        /// TODO (priority 3) - It would make more sense for this to be a member of EnvironmentTolerance
        /// or a general purpose method.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private int Median(EnvironmentTolerance tolerance)
        {
            int result = (int)(((tolerance.Maximum - tolerance.Minimum) / 2)
                        + tolerance.Minimum);

            return result;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the optimum radiation level as a percentage for this race. This is
        /// simply the median value as radiation levels run from 0 to 100;
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int OptimumRadiationLevel
        {
            get { return Median(RadiationTolerance); }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the optimum temperature level as a percentage for this
        /// race. Temperature values range from -200 to + 200
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int OptimumTemperatureLevel
        {
            get { return (200 + Median(TemperatureTolerance)) / 4; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the optimum gravity level as a percentage for this race. Gravity
        /// values range from 0 to 10;
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int OptimumGravityLevel
        {
            get { return Median(GravityTolerance) * 10; }
        }

        /// <summary>
        /// The maximum planetary population for this race.
        /// </summary>
        public int MaxPopulation
        {
            get
            {
                int maxPop = Global.NominalMaximumPlanetaryPopulation;
                if (HasTrait("HE")) maxPop = (int)(maxPop * Global.PopulationFactorHyperExpansion);
                if (HasTrait("JOAT")) maxPop = (int)(maxPop * Global.PopulationFactorJackOfAllTrades);
                if (HasTrait("OBRM")) maxPop = (int)(maxPop * Global.PopulationFactorOnlyBasicRemoteMining);
                return maxPop;
            }
        }

        /// <summary>
        /// Get the starting population for this race.
        /// </summary>
        /// <returns>The starting population.</returns>
        /// <remarks>
        /// TODO (priority 4) - Implement starting populations for races with two starting planets.
        /// </remarks>
        public int GetStartingPopulation()
        {
            int population = Global.StartingColonists;
            
            if (GameSettings.Data.AcceleratedStart)
            {
                population = Global.StartingColonistsAcceleratedBBS;
            }     

            if (HasTrait("LSP")) population = (int)(population * Global.LowStartingPopulationFactor);

            return population;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this Race to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Race</returns>
        /// ----------------------------------------------------------------------------
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

            // Type; // Primary Racial Trait.
            Global.SaveData(xmldoc, xmlelRace, "PRT", Traits.Primary.Code);
            // Traits
            foreach (TraitEntry trait in Traits)
            {
                if (AllTraits.Data.Primary.Contains(trait.Code)) continue; // Skip the PRT, just add LRTs here.
                Global.SaveData(xmldoc, xmlelRace, "LRT", trait.Code);
            }

            // MineBuildCost
            Global.SaveData(xmldoc, xmlelRace, "MineBuildCost", MineBuildCost.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Plural Name
            if (PluralName != null && PluralName != "") Global.SaveData(xmldoc, xmlelRace, "PluralName", PluralName);
            // Name
            if (Name != null && Name != "") Global.SaveData(xmldoc, xmlelRace, "Name", Name);
            // Password 
            if (Password != null && Password != "") Global.SaveData(xmldoc, xmlelRace, "Password", Password);
            // RaceIconName
            if (Icon.Source != null && Icon.Source != "") Global.SaveData(xmldoc, xmlelRace, "RaceIconName", Icon.Source);
            // Factory Build Cost
            Global.SaveData(xmldoc, xmlelRace, "FactoryBuildCost", FactoryBuildCost.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // ColonistsPerResource
            Global.SaveData(xmldoc, xmlelRace, "ColonistsPerResource", ColonistsPerResource.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // FactoryProduction
            Global.SaveData(xmldoc, xmlelRace, "FactoryProduction", FactoryProduction.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // OperableFactories
            Global.SaveData(xmldoc, xmlelRace, "OperableFactories", OperableFactories.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // MineProductionRate
            Global.SaveData(xmldoc, xmlelRace, "MineProductionRate", MineProductionRate.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // OperableMines
            Global.SaveData(xmldoc, xmlelRace, "OperableMines", OperableMines.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // MaxPopulation
            Global.SaveData(xmldoc, xmlelRace, "MaxPopulation", MaxPopulation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // GrowthRate
            Global.SaveData(xmldoc, xmlelRace, "GrowthRate", GrowthRate.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelRace;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load a Race from an xml document 
        /// </summary>
        /// <param name="xmlnode">An XmlNode, see Race constructor for generation.</param>
        /// ----------------------------------------------------------------------------
        public void LoadRaceFromXml(XmlNode xmlnode)
        {

            GravityTolerance = new EnvironmentTolerance();
            RadiationTolerance = new EnvironmentTolerance();
            TemperatureTolerance = new EnvironmentTolerance();

            while (xmlnode != null)
            {
                try
                {
                    switch (xmlnode.Name.ToLower())
                    {
                        case "root":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        case "race":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        case "gravitytolerance":
                            this.GravityTolerance = new EnvironmentTolerance(xmlnode.FirstChild);
                            break;
                        case "radiationtolerance":
                            this.RadiationTolerance = new EnvironmentTolerance(xmlnode.FirstChild);
                            break;
                        case "temperaturetolerance":
                            this.TemperatureTolerance = new EnvironmentTolerance(xmlnode.FirstChild);
                            break;
                        case "tech":
                            this.ResearchCosts = new TechLevel(xmlnode);
                            break;

                        case "lrt":
                            this.Traits.Add(xmlnode.FirstChild.Value);
                            break;

                        case "minebuildcost":
                            this.MineBuildCost = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "prt":
                            this.Traits.SetPrimary(xmlnode.FirstChild.Value);
                            break;
                        case "pluralname":
                            if (xmlnode.FirstChild != null)
                            {
                                this.PluralName = xmlnode.FirstChild.Value;
                            }
                            break;
                        case "name":
                            if (xmlnode.FirstChild != null)
                                this.Name = xmlnode.FirstChild.Value;
                            break;
                        case "password":
                            if (xmlnode.FirstChild != null)
                                this.Password = xmlnode.FirstChild.Value;
                            break;

                        // TODO (priority 5) - load the RaceIcon
                        case "raceiconname":
                            if (xmlnode.FirstChild != null)
                                this.Icon.Source = xmlnode.FirstChild.Value;
                            break;

                        case "factorybuildcost":
                            this.FactoryBuildCost = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "colonistsperresource":
                            this.ColonistsPerResource = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "factoryproduction":
                            this.FactoryProduction = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "operablefactories":
                            this.OperableFactories = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "mineproductionrate":
                            this.MineProductionRate = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "operablemines":
                            this.OperableMines = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "growthrate":
                            this.GrowthRate = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        default: break;
                    }

                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e);
                }

                xmlnode = xmlnode.NextSibling;
            }
        }

        #endregion

    }
}
