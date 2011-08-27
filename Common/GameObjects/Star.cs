#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

using System.Globalization;

namespace Nova.Common
{
    using System;
    using System.Xml;
    
    using Nova.Common;

    /// <summary>
    /// This object represents a Star system, the basic unit of stars-nova settlement/expansion.
    /// </summary>
    [Serializable]
    public class Star : Item
    {
        public bool HasFleetsInOrbit;
        public ProductionQueue ManufacturingQueue;
        public Resources MineralConcentration;
        public Resources ResourcesOnHand;
        public Fleet Starbase;

        /// <summary>
        /// The number of colonists as reported on a planet. Divide by GlobalDefinitions.ColonistsPerKiloton to convert to cargo units.
        /// </summary>
        public int Colonists;
        private int defenses;
        public bool OnlyLeftover = false;

        public int Factories;
        public int Mines;
        public int ResearchAllocation;
        public int ScanRange;
        public string DefenseType = "None";
        public string ScannerType = "None";

        // The following values are percentages of the permissable range of each
        // environment parameter (between 0 and 100).

        public int Gravity = 0;
        public int Radiation = 0;
        public int Temperature = 0;
        public int OriginalGravity = 0;
        public int OriginalRadiation = 0;
        public int OriginalTemperature = 0;

        /// <summary>
        /// A reference the the race information for the owner of this star.
        /// This is a convinience for the server. It will be null for races other than the player's race in the client.
        /// </summary>
        public Race ThisRace = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Star()
        {
            this.Starbase = null;
            this.ManufacturingQueue = new ProductionQueue();
            this.MineralConcentration = new Resources();
            this.ResourcesOnHand = new Resources();
            Type = ItemType.Star;
        }

        /// <summary>
        /// Determine the number of factories that can be operated.
        /// </summary>
        /// <returns>The number of factories that can be operated.</returns>
        public int GetOperableFactories()
        {           
             if (ThisRace == null)
             {
                // I have removed this exception so as to enable the use of this Method
                // through the program as it is highly useful. The exception was raised
                // for all unhabited stars during ProcessTurn. Returning zero operable
                // Factories or Mines feels as a more graceful solution too. -Aeglos
                
                // throw new InvalidOperationException("no owning race found for the star");
                return 0;
             }

            return (int)((double)Colonists / Global.ColonistsPerOperableFactoryUnit * ThisRace.OperableFactories);
        }
        
        /// <summary>
        /// Determine the number of factories that can be operated next turn
        /// considering growth.
        /// </summary>
        /// <returns>The number of factories that can be operated next turn.</returns>
        public int GetFutureOperableFactories()
        {
            if (ThisRace == null)
            {
                return 0;
            }
            
            int expectedGrowth = CalculateGrowth(ThisRace);
            
            return (int)(((double)Colonists + expectedGrowth) / Global.ColonistsPerOperableFactoryUnit
                         * ThisRace.OperableFactories);
        }

        /// <summary>
        /// Calculate the number of mines that can be operated.
        /// </summary>
        /// <returns>The number of mines that can be operated.</returns>
        public int GetOperableMines()
        {            
             if (ThisRace == null)
             {
                // See GetOperableFactories() above.
                // throw new InvalidOperationException("no owning race found for the star");
                return 0;
             }

            return (int)((double)Colonists / Global.ColonistsPerOperableMiningUnit * ThisRace.OperableMines);
        }
        
        /// <summary>
        /// Determine the number of mines that can be operated next turn
        /// considering growth.
        /// </summary>
        /// <returns>The number of mines that can be operated next turn.</returns>
        public int GetFutureOperableMines()
        {
            if (ThisRace == null)
            {
                return 0;
            }
            
            int expectedGrowth = CalculateGrowth(ThisRace);
            
            return (int)(((double)Colonists + expectedGrowth) / Global.ColonistsPerOperableMiningUnit
                         * ThisRace.OperableMines);
        }
        
        /// <summary>
        /// Calculate the number of factories currently operated.
        /// </summary>
        /// <returns>The number of factories currently in operation.</returns>
        public int GetFactoriesInUse()
        {
            int potentialFactories = GetOperableFactories();
            return Math.Min(Factories, potentialFactories);
        }
        
        /// <summary>
        /// Calculate the number of mines currently operated.
        /// </summary>
        /// <returns>The number of mines currently in operated.</returns>
        public int GetMinesInUse()
        {
            int potentialMines = GetOperableMines();
            return Math.Min(Mines, potentialMines);
        }
        
        /// <summary>
        /// Calculate the amount of resources currently generated.
        /// </summary>
        /// <returns>The resources generated.</returns>
        public int GetResourceRate()
        {
            if (ThisRace == null || Colonists <= 0)
            {
                return 0;
            }
            
            int factoriesInUse = GetFactoriesInUse();
            
            int rate = (int)((double)Colonists / ThisRace.ColonistsPerResource);
            rate += (int)(((double)factoriesInUse / Global.FactoriesPerFactoryProductionUnit) * ThisRace.FactoryProduction);
            
            return rate;
        }
        
        /// <summary>
        /// Calculate the amount of resources generated next turn accounting growth and
        /// factory production.
        /// </summary>
        /// <returns>The resources generated next turn.</returns>
        public int GetFutureResourceRate(int extraFactories)
        {
            if (ThisRace == null || Colonists <= 0)
            {
                return 0;
            }
            
            int potentialFactories = GetFutureOperableFactories();
            int expectedGrowth = CalculateGrowth(ThisRace);
            int factoriesInUse = Math.Min(Factories + extraFactories, potentialFactories);
            
            int rate = (int)(((double)Colonists + expectedGrowth) / ThisRace.ColonistsPerResource);
            rate += (int)(((double)factoriesInUse / Global.FactoriesPerFactoryProductionUnit) * ThisRace.FactoryProduction);
            
            return rate;
        }
        
        /// <summary>
        /// Calculate the amount of kT of minerals that can currently be mined.
        /// </summary>
        /// <returns>The mining rate in kT.</returns>
        public int GetMiningRate(int concentration)
        {
            if (ThisRace == null)
            {
                return 0;
            }
             
            int minesInUse = GetMinesInUse();
            
            // This operation needs to be done with implicit float converstion (the 100.0 value)
            // and then casted to int, otherwise the normalized concentration is always zero
            // and no mining occurs. -Aeglos
            int rate = (int)(((minesInUse / Global.MinesPerMineProductionUnit) * ThisRace.MineProductionRate)
                              * (concentration / 100.0));
            return rate;
        }
        
        /// <summary>
        /// Calculate the amount of kT of minerals that can be mined considering additional
        /// mines, for example in production ones.
        /// </summary>
        /// <returns>The potential mining rate in kT.</returns>
        public int GetFutureMiningRate(int concentration, int extraMines)
        {  
            if (ThisRace == null)
            {
                return 0;
            }
            
            int potentialMines = GetFutureOperableMines();
            int minesInUse = Math.Min(Mines + extraMines, potentialMines);
            
            // This operation needs to be done with implicit float converstion (the 100.0 value)
            // and then casted to int, otherwise the normalized concentration is always zero
            // and no mining occurs. -Aeglos
            int rate = (int)(((minesInUse / Global.MinesPerMineProductionUnit) * ThisRace.MineProductionRate)
                              * (concentration / 100.0));
            return rate;
        }
        
        /// <summary>
        /// Calculate the utilised capacity (as a percentage).
        /// </summary>
        /// <param name="race"></param>
        /// <returns>Capacity in the range 1 - 100 (%).</returns>
        public int Capacity(Race race)
        {
            double maxPopulation = race.MaxPopulation;
            
            if (race.HasTrait("HyperExpansion"))
            {
                maxPopulation *= Global.PopulationFactorHyperExpansion;
            }

            double capacity = (Colonists / maxPopulation) * 100;
            
            return (int)Math.Ceiling(capacity);
        }
        
        /// <summary>
        /// Calculates the growth for the star.
        /// </summary>
        /// <param name="race"></param>
        /// <returns>The amount of colonists the star will gain on update.</returns>
        /// <remarks>
        /// See Upadate()
        /// </remarks>
        public int CalculateGrowth(Race race)
        {
            double habitalValue = race.HabitalValue(this);
            double growthRate = race.GrowthRate;

            if (race.HasTrait("HyperExpansion"))
            {
                growthRate *= Global.GrowthFactorHyperExpansion;
            }

            double populationGrowth = Colonists;
            populationGrowth *= growthRate / 100.0;
            populationGrowth *= habitalValue;
            
            double capacity = Capacity(race) / 100.0;

            if (capacity > 0.25)
            {
                double crowdingFactor = Global.BaseCrowdingFactor;
                crowdingFactor *= (1.0 - capacity) * (1.0 - capacity);
                populationGrowth *= crowdingFactor;
            }
            
            // As per vanilla Stars! the minimal colonist growth unit
            // is set as 100 colonists. A planet does not track colonists
            // by the tens. While visually this does not matter much,
            // the compounding effect of growth can make those extra tens of
            // colonists matter in the long run and mismatch the behaviour
            // of Stars and Nova.
            int finalGrowth = (int)populationGrowth;
            finalGrowth /= 100;
            finalGrowth *= 100;
            
            return finalGrowth;
        } 
        
        /// <summary>
        /// Update the population of a star system.
        /// </summary>
        /// <param name="race"></param>
        /// <remarks>
        /// See Upadate()
        /// </remarks>
        public void UpdatePopulation(Race race)
        {
            Colonists += CalculateGrowth(race);
        }
  
        /// <summary>
        /// Updates the research allocation for the star.
        /// </summary>
        /// <param name="budget">The new budget (0-100).</param>
        public void UpdateResearch(int budget)
        {
            if (OnlyLeftover == false)
            {
                if (budget >= 0 && budget <= 100)
                {
                    this.ResearchAllocation = (this.GetResourceRate() * budget) / 100;
                }
            }
            else
            {
                this.ResearchAllocation = 0;
            }
        }

        /// <summary>
        /// Update the resources available to a star system.
        /// </summary>
        /// <remarks>
        /// See UpadateMinerals()
        /// </remarks>
        public void UpdateResources()
        {
            // A certain number of colonists will generate a resource each year.
            // This has a default of 1000 colonists per resource but can be
            // channged on a per race basis in the Race Designer.

            // In addition, resources are generated by factories that are capable
            // of being manned. Again this is set in the Race Deigner with a
            // default of 1k colonists needed to man each factory. Note that the
            // actual number of existing factories may be less than the number
            // that are capable of being manned.
            // 
            // UPDATE: The Stars! default is 10k per 10 factories. This calculation
            // has been refactored away. -Aeglos
            // UPDATE2: This 

            this.ResourcesOnHand.Energy = this.GetResourceRate();
            this.ResourcesOnHand.Energy -= this.ResearchAllocation;
        }

        /// <summary>
        /// Update the minerals available on a star system.
        /// </summary>
        /// <remarks>
        /// See UpadateResources()
        /// </remarks>
        public void UpdateMinerals()
        {            
            this.ResourcesOnHand.Ironium += this.Mine(ref this.MineralConcentration.Ironium);
            this.ResourcesOnHand.Boranium += this.Mine(ref this.MineralConcentration.Boranium);
            this.ResourcesOnHand.Germanium += this.Mine(ref this.MineralConcentration.Germanium);
        }

        /// <summary>
        /// Mine minerals.
        /// </summary>
        /// <param name="concentration">The mineral concentraion in this system, (1.0 = 100%). 
        /// Mining alters the concentration of minerals.</param>
        /// <returns>The number of minerals mined.</returns>
        /// <remarks>
        /// Mining rate = Number of Mines * Efficiency * Mineral Concentration %.
        ///
        /// Mining efficiency is a race parameter (MineProductionRate per 10 mines)
        /// Concentration is in % and is normalised so that 1.0 = 100%
        /// 
        /// Note also that this method does not actually modify the Star's minerals. It
        /// merely returns the amount mined and decreases concentration.
        /// </remarks>
        private int Mine(ref int concentration)
        {
            // As with factories, mines must be manned to be able to produce.
            // Again this is set in the Race Deigner with a default of 1k
            // colonists needed to man each mine and, as with factories, the
            // actual number of existing mines may be less than the number that
            // are capable of being manned.
            //
            // The potential number of mines that might be capable of being used
            // is a race parameter which determines how many mines may be
            // operated by 10K colonists.

            int mined = GetMiningRate(concentration);

            // Reduce the mineral concentration. This is just an approximation of
            // the Stars! algorithm for now. Concentration will drop by 1 point
            // after 12500/concentration kT have been mined. So we just reduce the
            // concentration by a proportion according to how much has been mined
            // this year.
            // TODO (priority 3) - implement the Stars! algorithm for concentration reduction.
            // TODO - A better approach would be to store the cumulative value of minerals mined
            // and drop the concentration when the 12500/concentration threshold is breached. Also,
            // The partial algorithm from 99 to 27 is amount/(12500/concentration)*mine efficiency.
            // Below 27 the curve is linear with around 1000 until 2 and 2000 for the last point.

            concentration -= mined / (12500 / concentration);

            if (concentration < 1)
            {
                concentration = 1;
            }

            return mined;
        }

        public int Defenses
        {
            set
            {
                if (value > Global.MaxDefenses)
                {
                    Report.Debug("Max defenses exceeded.");
                    defenses = Global.MaxDefenses;
                }
                else
                {
                    defenses = value;
                }
            }
            get
            {
                if (defenses <= Global.MaxDefenses)
                {
                    return defenses;
                }
                else
                {
                    Report.Debug("Max defenses exceeded.");
                    return Global.MaxDefenses;
                }
            }
        }

        /// <summary>
        /// Override the inherited property Item.Key. Stars use there Name as a unique key as their owner does not affect their unique identity like fleets/ships/minefields.
        /// </summary>
        public new string Key
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Load: Initialising constructor to read in a Star from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="node">An XmlNode representing a Star.</param>
        public Star(XmlNode node)
            : base(node)
        {
            this.Starbase = null;

            XmlNode subnode = node.FirstChild;

            // Read the node
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "hasfleetsinorbit":
                            HasFleetsInOrbit = bool.Parse(subnode.FirstChild.Value);
                            break;
                        case "productionqueue":
                            ManufacturingQueue = new ProductionQueue(subnode);
                            break;
                        case "mineralconcentration":
                            MineralConcentration = new Resources(subnode.FirstChild);
                            break;
                        case "resourcesonhand":
                            ResourcesOnHand = new Resources(subnode.FirstChild);
                            break;
                        case "colonists":
                            Colonists = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "defenses":
                            Defenses = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "factories":
                            Factories = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "mines":
                            Mines = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "researchallocation":
                            ResearchAllocation = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "onlyleftover":
                            OnlyLeftover = bool.Parse(subnode.FirstChild.Value);
                            break;
                        case "scanrange":
                            ScanRange = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "defensetype":
                            DefenseType = subnode.FirstChild.Value;
                            break;
                        case "scannertype":
                            ScannerType = subnode.FirstChild.Value;
                            break;
                        case "gravity":
                            Gravity = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "radiation":
                            Radiation = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "temperature":
                            Temperature = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "originalgravity":
                            OriginalGravity = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "originalradiation":
                            OriginalRadiation = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "originaltemperature":
                            OriginalTemperature = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        // These are placeholder objects that will be linked to the real objects once 
                        // loading from the file is complete (as they may not exist yet, and cannot be 
                        // referenced from here). The node will only hold enough information to identify 
                        // the referenced object. 

                        // ThisRace will point to the Race that owns the Star, 
                        // for now create a placeholder Race and load its Name
                        case "thisrace":
                            ThisRace = new Race();
                            ThisRace.Name = subnode.FirstChild.Value;
                            break;

                        // Starbase will point to the Fleet that is this planet's starbase (if any), 
                        // for now create a placeholder Fleet and load its FleetID
                        case "starbase":
                            Starbase = new Fleet(long.Parse(subnode.FirstChild.Value, NumberStyles.HexNumber));
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Create an XmlElement representation of the star for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the star.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelStar = xmldoc.CreateElement("Star");

            // include inherited Item properties
            xmlelStar.AppendChild(base.ToXml(xmldoc));

            xmlelStar.AppendChild(ManufacturingQueue.ToXml(xmldoc));

            // MineralConcentraion and ResourcesOnHand need wrapper nodes so we can tell what they are (other than Resources) when we read them back in.
            XmlElement xmlelMineralConcentration = xmldoc.CreateElement("MineralConcentration");
            xmlelMineralConcentration.AppendChild(MineralConcentration.ToXml(xmldoc));
            xmlelStar.AppendChild(xmlelMineralConcentration);

            XmlElement xmlelResourcesOnHand = xmldoc.CreateElement("ResourcesOnHand");
            xmlelResourcesOnHand.AppendChild(ResourcesOnHand.ToXml(xmldoc));
            xmlelStar.AppendChild(xmlelResourcesOnHand);

            Global.SaveData(xmldoc, xmlelStar, "HasFleetsInOrbit", HasFleetsInOrbit.ToString());
  
            // Starbase and ThisRace are stored as references only (just the name is saved).
            if (Starbase != null)
            {
                Global.SaveData(xmldoc, xmlelStar, "Starbase", Starbase.Key.ToString("X"));
            }
            
            if (ThisRace != null)
            {
                Global.SaveData(xmldoc, xmlelStar, "ThisRace", ThisRace.Name);
            }

            if (Colonists != 0)
            {
                Global.SaveData(xmldoc, xmlelStar, "Colonists", Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            if (Defenses != 0)
            {
                Global.SaveData(xmldoc, xmlelStar, "Defenses", Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            if (Factories != 0)
            {
                Global.SaveData(xmldoc, xmlelStar, "Factories", Factories.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            if (Mines != 0)
            { 
                Global.SaveData(xmldoc, xmlelStar, "Mines", Mines.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            if (ResearchAllocation != 0)
            { 
                Global.SaveData(xmldoc, xmlelStar, "ResearchAllocation", ResearchAllocation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            Global.SaveData(xmldoc, xmlelStar, "OnlyLeftover", OnlyLeftover.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        
            if (ScanRange != 0)
            {
                Global.SaveData(xmldoc, xmlelStar, "ScanRange", ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            Global.SaveData(xmldoc, xmlelStar, "DefenseType", DefenseType);
            Global.SaveData(xmldoc, xmlelStar, "ScannerType", ScannerType);
            Global.SaveData(xmldoc, xmlelStar, "Gravity", Gravity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "Radiation", Radiation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "Temperature", Temperature.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "OriginalGravity", OriginalGravity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "OriginalRadiation", OriginalRadiation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "OriginalTemperature", OriginalTemperature.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelStar;
        }

        public override string ToString()
        {
            return "Star: " + Name;
        }
        
        public StarIntel GenerateReport(ScanLevel scan, int year)
        {
            StarIntel report = new StarIntel(this, scan, year);
            
            return report;
        }
    }
}
