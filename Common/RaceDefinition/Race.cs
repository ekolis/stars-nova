#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011, 2012 The Stars-Nova Project
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

namespace Nova.Common
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;

    using Nova.Common.RaceDefinition;

    /// <summary>
    /// This Class defines all the parameters that define the characteristics of a
    /// race. These values are all set in the race designer. This object also manages
    /// the loading ans saving of race data to a file.
    /// </summary>
    [Serializable]
    public class Race
    {
        public EnvironmentTolerance GravityTolerance        = new GravityTolerance();
        public EnvironmentTolerance RadiationTolerance      = new RadiationTolerance();
        public EnvironmentTolerance TemperatureTolerance    = new TemperatureTolerance();

        public TechLevel ResearchCosts = new TechLevel(0);

        public RacialTraits Traits = new RacialTraits(); // Collection of all the race's traits, including the primary.

        public string PluralName;
        public string Name;
        public string Password;
        public RaceIcon Icon = new RaceIcon();

        // These parameters affect the production rate of each star (used in the
        // Star class Update method).
        public int FactoryBuildCost;        // defined in the Race Designer as the amount of Resourcesrequired to build one factory
        public int ColonistsPerResource;
        public int FactoryProduction;    // defined in the Race Designer as the amount of resources produced by 10 factories
        public int OperableFactories;

        public int MineBuildCost;
        public int MineProductionRate;   // defined in the Race Designer as the amount of minerals (kT) mined by every 10 mines
        public int OperableMines;

        public string LeftoverPointTarget;

        // Growth goes from 3 to 20 and is not normalized here.
        public double GrowthRate;

        // required for searializable class
        public Race() 
        { 
        }

        /// <summary>
        /// Constructor for Race. 
        /// Reads all the race data in from an xml formated save file.
        /// </summary>
        /// <param name="fileName">A nova save file containing a race.</param>
        public Race(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            xmldoc.Load(fileName);
            XmlNode xmlnode = xmldoc.DocumentElement;
            LoadRaceFromXml(xmlnode);

            fileStream.Close();
        }

        
        /// <summary>
        /// Calculate this race's Habitability for a given star.
        /// </summary>
        /// <param name="star">The star for which the Habitability is being determined.</param>
        /// <returns>The normalised habitability of the star (-1 to +1).</returns>
        /// <remarks>
        /// This algorithm is taken from the Stars! Technical FAQ:
        /// http://www.starsfaq.com/advfaq/contents.htm
        ///
        /// Return the habital value of this star for the specified race (in the range
        /// -1 to +1 where 1 = 100%). Note that the star environment values are
        /// percentages of the total range.
        ///
        /// The full equation (from the Stars! Technical FAQ) is: 
        ///
        /// Hab% = SQRT[(1-g)^2+(1-t)^2+(1-r)^2]*(1-x)*(1-y)*(1-z)/SQRT[3] 
        ///
        /// Where g, t,and r (stand for gravity, temperature, and radiation)are given
        /// by Clicks_from_center/Total_clicks_from_center_to_edge and where x,y, and z
        /// are:
        ///
        /// x=g-1/2 for g>1/2
        /// x=0 for g less than 1/2 
        /// y=t-1/2 for t>1/2
        /// y=0 for t less than 1/2 
        /// z=r-1/2 for r>1/2
        /// z=0 for r less than 1/2 
        /// </remarks>
        public double HabitalValue(Star star)
        {
            double r = NormalizeHabitalityDistance(RadiationTolerance, star.Radiation);
            double g = NormalizeHabitalityDistance(GravityTolerance, star.Gravity);
            double t = NormalizeHabitalityDistance(TemperatureTolerance, star.Temperature);

            if (r > 1 || g > 1 || t > 1)
            {
                // currently not habitable
                int result = 0;
                int maxMalus = GetMaxMalus();
                if (r > 1)
                {
                    result -= GetMalusForEnvironment(RadiationTolerance, star.Radiation, maxMalus);
                }
                if (g > 1)
                {
                    result -= GetMalusForEnvironment(GravityTolerance, star.Gravity, maxMalus);
                }
                if (t > 1)
                {
                    result -= GetMalusForEnvironment(TemperatureTolerance, star.Temperature, maxMalus);
                }
                return result / 100.0;
            }

            double x = 0;
            double y = 0;
            double z = 0;

            if (g > 0.5)
            {
                x = g - 0.5;
            }
            if (t > 0.5)
            {
                y = t - 0.5;
            }
            if (r > 0.5)
            {
                z = r - 0.5;
            }

            double h = Math.Sqrt(
                            ((1 - g) * (1 - g)) + ((1 - t) * (1 - t)) + ((1 - r) * (1 - r))) * (1 - x) * (1 - y) * (1 - z)
                                 / Math.Sqrt(3.0);
            return h;
        }

        /// <summary>
        /// Calculate this race's Habitability for a given star report.
        /// </summary>
        /// <param name="report">The star report for which the Habitability is being determined.</param>
        /// <returns>The normalised habitability of the star (-1 to +1).</returns>
        public double HabitalValue(StarIntel report)
        {
            Star star = new Star();
            star.Gravity = report.Gravity;
            star.Radiation = report.Radiation;
            star.Temperature = report.Temperature;
            
            return HabitalValue(star);
        }

        public virtual int GetAdvantagePoints()
        {
            RaceAdvantagePointCalculator calculator = new RaceAdvantagePointCalculator();
            return calculator.calculateAdvantagePoints(this);
        }

        public int GetLeftoverAdvantagePoints()
        {
            int advantagePoints = GetAdvantagePoints();
            advantagePoints = Math.Max(0, advantagePoints); // return Advantage Points only if >= 0
            advantagePoints = Math.Min(50, advantagePoints); // return not more than 50
            return advantagePoints;
        }

        private int GetMaxMalus()
        {
            int maxMalus = 15;
            if (HasTrait("TT"))
            {
                maxMalus = 30;
            }
            return maxMalus;
        }

        private int GetMalusForEnvironment(EnvironmentTolerance tolerance, int starValue, int maxMalus)
        {
            if (starValue > tolerance.MaximumValue)
            {
                return Math.Min(maxMalus, starValue - tolerance.MaximumValue);
            }
            else if (starValue < tolerance.MinimumValue)
            {
                return Math.Min(maxMalus, tolerance.MinimumValue - starValue);
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Clicks_from_center / Total_clicks_from_center_to_edge .
        /// </summary>
        /// <param name="tol"></param>
        /// <param name="starValue"></param>
        /// <returns></returns>
        private double NormalizeHabitalityDistance(EnvironmentTolerance tol, int starValue)
        {
            if (tol.Immune)
            {
                return 0.0;
            }

            int minv = tol.MinimumValue;
            int maxv = tol.MaximumValue;
            int span = Math.Abs(maxv - minv);
            double totalClicksFromCenterToEdge = span / 2;
            double centre = minv + totalClicksFromCenterToEdge;
            double clicksFromCenter = Math.Abs(centre - starValue);
            return clicksFromCenter / totalClicksFromCenterToEdge;
        }
        
        /// <summary>
        /// Calculate the number of resources this race requires to construct a factory.
        /// </summary>
        /// <returns>The number of resources this race requires to construct a factory.</returns>
        public Resources GetFactoryResources()
        {
            int factoryBuildCostGerm = HasTrait("CF") ? 3 : 4;
            return new Resources(0, 0, factoryBuildCostGerm, FactoryBuildCost);
        }

        /// <summary>
        /// Calculate the number of resources this race requires to construct a mine.
        /// </summary>
        public Resources GetMineResources()
        {
            return new Resources(0, 0, 0, MineBuildCost);
        }

        /// <summary>
        /// Determine if this race has a given trait.
        /// </summary>
        /// <param name="trait">A string representing a primary or secondary trait. 
        /// See AllTraits.TraitKeys for examples.</param>
        /// <returns>true if this race has the given trait.</returns>
        public bool HasTrait(string trait)
        {
            if (trait == Traits.Primary)
            {
                return true;
            }

            if (Traits == null)
            {
                return false;
            }
            return this.Traits.Contains(trait);
        }

        /// <summary>
        /// The maximum planetary population for this race.
        /// </summary>
        public int MaxPopulation
        {
            get
            {
                int maxPop = Global.NominalMaximumPlanetaryPopulation;
                if (HasTrait("HE"))
                {
                    maxPop = (int)(maxPop * Global.PopulationFactorHyperExpansion);
                }
                if (HasTrait("JOAT"))
                { 
                    maxPop = (int)(maxPop * Global.PopulationFactorJackOfAllTrades);
                }
                if (HasTrait("OBRM")) 
                {
                    maxPop = (int)(maxPop * Global.PopulationFactorOnlyBasicRemoteMining);
                }
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

            if (HasTrait("LSP"))
            {
                population = (int)(population * Global.LowStartingPopulationFactor);
            }

            return population;
        }

        // Quick and dirty way to clone a race but has the big advantage
        // of picking up XML changes automagically
        public Race Clone()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement ele = ToXml(doc);
            Race ret = new Race();
            ret.LoadRaceFromXml(ele);
            return ret;
        }

        /// <summary>
        /// Save: Serialize this Race to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Race.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelRace = xmldoc.CreateElement("Race");

            xmlelRace.AppendChild(GravityTolerance.ToXml(xmldoc, "GravityTolerance"));
            xmlelRace.AppendChild(RadiationTolerance.ToXml(xmldoc, "RadiationTolerance"));
            xmlelRace.AppendChild(TemperatureTolerance.ToXml(xmldoc, "TemperatureTolerance"));
            // Tech
            xmlelRace.AppendChild(ResearchCosts.ToXml(xmldoc));

            // Type; // Primary Racial Trait.
            Global.SaveData(xmldoc, xmlelRace, "PRT", Traits.Primary.Code);
            // Traits
            foreach (TraitEntry trait in Traits)
            {
                if (AllTraits.Data.Primary.Contains(trait.Code))
                {
                    continue; // Skip the PRT, just add LRTs here.
                }
                Global.SaveData(xmldoc, xmlelRace, "LRT", trait.Code);
            }

            // MineBuildCost
            Global.SaveData(xmldoc, xmlelRace, "MineBuildCost", MineBuildCost.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Plural Name
            if (!string.IsNullOrEmpty(PluralName))
            {
                Global.SaveData(xmldoc, xmlelRace, "PluralName", PluralName);
            }
            // Name
            if (!string.IsNullOrEmpty(Name))
            {
                Global.SaveData(xmldoc, xmlelRace, "Name", Name);
            }
            // Password 
            if (!string.IsNullOrEmpty(Password))
            {
                Global.SaveData(xmldoc, xmlelRace, "Password", Password);
            }
            // RaceIconName
            if (!string.IsNullOrEmpty(Icon.Source))
            {
                Global.SaveData(xmldoc, xmlelRace, "RaceIconName", Icon.Source);
            }
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

            // LeftoverPointTarget
            if ("".Equals(LeftoverPointTarget) || LeftoverPointTarget == null)
            {
                LeftoverPointTarget = "Surface minerals";
            }
            Global.SaveData(xmldoc, xmlelRace, "LeftoverPoints", LeftoverPointTarget.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelRace;
        }

        /// <summary>
        /// Load a Race from an xml document.
        /// </summary>
        /// <param name="xmlnode">An XmlNode, see Race constructor for generation.</param>
        public void LoadRaceFromXml(XmlNode xmlnode)
        {
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
                            GravityTolerance.FromXml(xmlnode);
                            break;
                        case "radiationtolerance":
                            RadiationTolerance.FromXml(xmlnode);
                            break;
                        case "temperaturetolerance":
                            TemperatureTolerance.FromXml(xmlnode);
                            break;
                        case "tech":
                            ResearchCosts = new TechLevel(xmlnode);
                            break;

                        case "lrt":
                            Traits.Add(xmlnode.FirstChild.Value);
                            break;

                        case "minebuildcost":
                            MineBuildCost = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "prt":
                            Traits.SetPrimary(xmlnode.FirstChild.Value);
                            break;
                        case "pluralname":
                            if (xmlnode.FirstChild != null)
                            {
                                PluralName = xmlnode.FirstChild.Value;
                            }
                            break;
                        case "name":
                            if (xmlnode.FirstChild != null)
                            {
                                Name = xmlnode.FirstChild.Value;
                            }
                            break;
                        case "password":
                            if (xmlnode.FirstChild != null)
                            {
                                Password = xmlnode.FirstChild.Value;
                            }
                            break;

                        // TODO (priority 5) - load the RaceIcon
                        case "raceiconname":
                            if (xmlnode.FirstChild != null)
                            {
                                Icon.Source = xmlnode.FirstChild.Value;
                            }
                            break;

                        case "factorybuildcost":
                            FactoryBuildCost = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "colonistsperresource":
                            ColonistsPerResource = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "factoryproduction":
                            FactoryProduction = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "operablefactories":
                            OperableFactories = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "mineproductionrate":
                            MineProductionRate = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "operablemines":
                            OperableMines = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "growthrate":
                            GrowthRate = int.Parse(xmlnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "leftoverpoints":
                            this.LeftoverPointTarget = xmlnode.FirstChild.Value;
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

            // if an old version of the race file is loaded and there is no leftover point target then select standard leftover point target.
            if ("".Equals(LeftoverPointTarget) || LeftoverPointTarget == null)
            {
                this.LeftoverPointTarget = "Surface minerals";
            }
        }

        public int lowerHab(int habIndex)
        {
            switch (habIndex)
            {
                case 0:
                    return GravityTolerance.MinimumValue;
                case 1:
                    return TemperatureTolerance.MinimumValue;
                case 2:
                    return RadiationTolerance.MinimumValue;
            }
            return 0;
        }

        public int upperHab(int habIndex)
        {
            switch (habIndex)
            {
                case 0:
                    return GravityTolerance.MaximumValue;
                case 1:
                    return TemperatureTolerance.MaximumValue;
                case 2:
                    return RadiationTolerance.MaximumValue;
            }
            return 0;
        }

        public int centerHab(int habIndex)
        {
            switch (habIndex)
            {
                case 0:
                    return GravityTolerance.OptimumLevel;
                case 1:
                    return TemperatureTolerance.OptimumLevel;
                case 2:
                    return RadiationTolerance.OptimumLevel;
            }
            return 0;
        }

        public bool isImmune(int habIndex)
        {
            switch (habIndex)
            {
                case 0:
                    return GravityTolerance.Immune;
                case 1:
                    return TemperatureTolerance.Immune;
                case 2:
                    return RadiationTolerance.Immune;
            }
            return false;
        }
    }
}
