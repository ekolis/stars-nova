// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This object represents a Star system, the basic unit of stars-nova settlement/expansion.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Drawing;

namespace NovaCommon
{

    /// <summary>
    /// Star Class
    /// </summary>
    [Serializable]
    public class Star : Item
    {
        public bool            OrbitingFleets       = false;
        public ProductionQueue ManufacturingQueue   = new ProductionQueue();
        public Resources       MineralConcentration = new Resources();
        public Resources       ResourcesOnHand      = new Resources();
        public Fleet           Starbase             = null;
        public int             Colonists            = 0;
        public int             Defenses             = 0;
        public int             Factories            = 0;
        public int             Mines                = 0;
        public int             ResearchAllocation   = 0;
        public int             ScanRange            = 0;
        public string          DefenseType          = "None";
        public string          ScannerType          = "None";

        // The following values are percentages of the permissable range of each
        // environment parameter (between 0 and 100).

        public int Gravity     = 0;
        public int Radiation   = 0;
        public int Temperature = 0;

        // the current owner of the star, if any.
        public Race ThisRace   = null;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Star() { }

        #endregion

        // Methods that access or calculate values without changing the Star system.
        #region Information Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine the number of factories that can be operated.
        /// </summary>
        /// <returns>the number of factories that can be operated</returns>
        /// ----------------------------------------------------------------------------
        public int GetOperableFactories()
        {
            if (ThisRace == null)
            {
                throw new InvalidOperationException("no owning race found for the star");
            }

            return Convert.ToInt32(Colonists * ThisRace.OperableFactories);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the number of mines that can be operated.
        /// </summary>
        /// <returns>the number of mines that can be operated</returns>
        /// ----------------------------------------------------------------------------
        public int GetOperableMines()
        {
            if (ThisRace == null)
            {
                throw new InvalidOperationException("no owning race found for the star");
            }

            return Convert.ToInt32(Colonists * ThisRace.OperableMines);
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the utilised capacity (as a percentage).
        /// </summary>
        /// <param name="race"></param>
        /// <returns>Capacity in the range 1 - 100 (%)</returns>
        /// ----------------------------------------------------------------------------
        public int Capacity(Race race)
        {
            double maxPopulation = race.MaxPopulation;
            if (race.HasTrait("HyperExpansion"))
            {
                maxPopulation /= 2;
            }

            double capacity = (Colonists / maxPopulation) * 100;
            return (int)Math.Ceiling(capacity);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate this star's Habitability for a given race.
        /// </summary>
        /// <param name="race">The race for which the Habitability is being determined.</param>
        /// <returns>The normalised habitability of this star (-1 to +1).</returns>
        /// <remarks>
        /// This algorithm is taken from the Stars! Technical FAQ:
        // http://www.starsfaq.com/advfaq/contents.htm
        //
        // Return the habital value of this star for the specified race (in the range
        // -1 to +1 where 1 = 100%). Note that the star environment values are
        // percentages of the total range.
        //
        // The full equation (from the Stars! Technical FAQ) is: 
        //
        // Hab% = SQRT[(1-g)^2+(1-t)^2+(1-r)^2]*(1-x)*(1-y)*(1-z)/SQRT[3] 
        //
        // Where g, t,and r (stand for gravity, temperature, and radiation)are given
        // by Clicks_from_center/Total_clicks_from_center_to_edge and where x,y, and z
        // are:
        //
        // x=g-1/2 for g>1/2
        // x=0 for g<1/2 
        // y=t-1/2 for t>1/2
        // y=0 for t<1/2 
        // z=r-1/2 for r>1/2
        // z=0 for r<1/2 
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public double HabitalValue(Race race)
        {
            // Calculate the minimum and maximum values of the tolerance ranges
            // expressed as a percentage of the total range. (Radiation is in the
            // range 0-100 so the values already can be thought of as a
            // percentage.

            double rMinimum = race.RadiationTolerance.Minimum;
            double rMaximum = race.RadiationTolerance.Maximum;

            // Get the span of the range and its centre.

            double rSpan = rMaximum - rMinimum;
            double rCentre = rMinimum + (rSpan / 2);

            // Find out how far away the star value is from the ideal.

            double rDistance = Math.Abs(rCentre - Radiation);

            // Normalisize the distance to be in the range 0 to 1.

            double r = rDistance / rSpan;

            // Now do the same for the other two parameters. Gravity is in the
            // range 0 to 10.

            double gMinimum = race.GravityTolerance.Minimum * 10;
            double gMaximum = race.GravityTolerance.Maximum * 10;
            double gSpan = gMaximum - gMinimum;
            double gCentre = gMinimum + (gSpan / 2);
            double gDistance = Math.Abs(gCentre - Gravity);
            double g = gDistance / gSpan;

            // Temperature is in the range -200 to 200.

            double tMinimum = (200 + race.TemperatureTolerance.Minimum) / 4;
            double tMaximum = (200 + race.TemperatureTolerance.Maximum) / 4;
            double tSpan = tMaximum - tMinimum;
            double tCentre = tMinimum + (tSpan / 2);
            double tDistance = Math.Abs(tCentre - Temperature);
            double t = tDistance / tSpan;

            double x = 0;
            double y = 0;
            double z = 0;

            if (g > 0.5) x = g - 0.5;
            if (t > 0.5) y = t - 0.5;
            if (r > 0.5) z = r - 0.5;

            double h = Math.Sqrt(
                            ((1 - g) * (1 - g)) + ((1 - t) * (1 - t)) + ((1 - r) * (1 - r))
                                 ) * (1 - x) * (1 - y) * (1 - z)
                                 / Math.Sqrt(3.0);
            return h;
        }

        #endregion

        // These methods change the star system. They should only be called server side.
        #region Action Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update a star to take into account the passing of a year.
        /// </summary>
        /// <param name="race"></param>
        /// <remarks>
        /// FIXME (priority 3) why pass the race in? The race occupying this star system is this.ThisRace.
        /// 
        /// Update a star to take into account the passing of a year.
        /// FIXME (priority 4) - this should not be here as it means the GUI has access to methods 
        /// that increase pop and resources - which is probably the source of the bug
        /// that causes this to happen when a star is clicked in the GUI.
        /// ... Turns out that bug was calling this function when it should not have been,
        /// still the above comment applies. Suggest refactor into ProcessTurn. - Dan 16 Jan 10
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public void Update(Race race)
        {
            UpdatePopulation(race);
            UpdateResources(race);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the population of a star system.
        /// </summary>
        /// <param name="race"></param>
        /// <remarks>
        /// See Upadate()
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void UpdatePopulation(Race race)
        {
            double habitalValue = HabitalValue(race);
            double growthRate = race.GrowthRate / 100;

            if (race.HasTrait("HyperExpansion"))
            {
                growthRate *= 2;
            }

            double populationGrowth = Colonists * growthRate * habitalValue;
            double capacity = Colonists / race.MaxPopulation;

            if (race.HasTrait("HyperExpansion"))
            {
                capacity /= 2;
            }

            if (capacity > 0.25)
            {
                double crowdingFactor = 1.75;
                crowdingFactor *= (1.0 - capacity) * (1.0 - capacity);
                populationGrowth *= crowdingFactor;
            }

            Colonists += (int)populationGrowth;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the resources available to a star system.
        /// </summary>
        /// <param name="race"></param>
        /// <remarks>
        /// See Upadate()
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void UpdateResources(Race race)
        {
            // A certain number of colonists will generate a resource each year.
            // This has a default of 1000 colonists per resource but can be
            // channged on a per race basis in the Race Designer.

            ResourcesOnHand.Energy = Colonists / race.ColonistsPerResource;

            // In addition, resources are generated by factories that are capable
            // of being manned. Again this is set in the Race Deigner with a
            // default of 1k colonists needed to man each factory. Note that the
            // actual number of existing factories may be less than the number
            // that are capable of being manned.

            double potentialFactories = Colonists / race.OperableFactories;
            double factoriesInUse = Math.Min(Factories, potentialFactories);

            ResourcesOnHand.Energy += factoriesInUse * race.FactoryProduction / Global.FactoriesPerFactoryProductionUnit;

            ThisRace = race;
            ResourcesOnHand.Ironium += Mine(ref MineralConcentration.Ironium);
            ResourcesOnHand.Boranium += Mine(ref MineralConcentration.Boranium);
            ResourcesOnHand.Germanium += Mine(ref MineralConcentration.Germanium);
            ResourcesOnHand.Energy -= ResearchAllocation;
        }


        /// ----------------------------------------------------------------------------
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
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private double Mine(ref double concentration)
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


            double potentialMines = (Colonists / Global.ColonistsPerOperableMiningUnit) * ThisRace.OperableMines;
            double minesInUse = Math.Min(Mines, potentialMines);

            double mined = ((minesInUse / Global.MinesPerMineProductionUnit) * ThisRace.MineProductionRate)
                         * (concentration / 100);

            // Reduce the mineral concentration. This is just an approximation of
            // the Stars! algorithm for now. Concentration will drop by 1 point
            // after 12500/concentration kT have been mined. So we just reduce the
            // concentration by a proportion according to how much has been mined
            // this year.
            // TODO (priority 3) - implement the Stars! algorithm for concentration reduction.

            concentration -= mined / (12500.0 / concentration);

            if (concentration < 1)
            {
                concentration = 1;
            }

            return mined;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising constructor to read in a Star from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="node">An XmlNode representing a Star.</param>
        /// ----------------------------------------------------------------------------
        public Star(XmlNode node)
            : base(node)
        {

            XmlNode subnode = node.FirstChild;

            // Read the node
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "orbitingfleets":
                            OrbitingFleets = bool.Parse(((XmlText)subnode.FirstChild).Value);
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
                            Colonists = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "defenses":
                            Defenses = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "factories":
                            Factories = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "mines":
                            Mines = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "researchallocation":
                            ResearchAllocation = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "scanrange":
                            ScanRange = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "defensetype":
                            DefenseType = ((XmlText)subnode.FirstChild).Value;
                            break;
                        case "scannertype":
                            ScannerType = ((XmlText)subnode.FirstChild).Value;
                            break;
                        case "gravity":
                            Gravity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "radiation":
                            Radiation = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "temperature":
                            Temperature = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        // These are placeholder objects that will be linked to the real objects once 
                        // loading from the file is complete (as they may not exist yet, and cannot be 
                        // referenced from here). The node will only hold enough information to identify 
                        // the referenced object. 

                        // ThisRace will point to the Race that owns the Star, 
                        // for now create a placeholder Race and load its Name
                        case "thisrace":
                            ThisRace = new Race(); ThisRace.Name = ((XmlText)subnode.FirstChild).Value;
                            break;

                        // Starbase will point to the Fleet that is this planet's starbase (if any), 
                        // for now create a placeholder Fleet and load its FleetID
                        case "starbase":
                            Starbase = new Fleet(int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture));
                            break;

                        default: break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }
                subnode = subnode.NextSibling;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Create an XmlElement representation of the star for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representation of the star.</returns>
        /// ----------------------------------------------------------------------------
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

            // Starbase and ThisRace are stored as references only (just the name is saved).
            if (Starbase != null) Global.SaveData(xmldoc, xmlelStar, "Starbase", Starbase.FleetID.ToString());
            if (ThisRace != null) Global.SaveData(xmldoc, xmlelStar, "ThisRace", ThisRace.Name);

            if (Colonists != 0) Global.SaveData(xmldoc, xmlelStar, "Colonists", Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Defenses != 0) Global.SaveData(xmldoc, xmlelStar, "Defenses", Defenses.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Factories != 0) Global.SaveData(xmldoc, xmlelStar, "Factories", Factories.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Mines != 0) Global.SaveData(xmldoc, xmlelStar, "Mines", Mines.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (ResearchAllocation != 0) Global.SaveData(xmldoc, xmlelStar, "ResearchAllocation", ResearchAllocation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (ScanRange != 0) Global.SaveData(xmldoc, xmlelStar, "ScanRange", ScanRange.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "DefenseType", DefenseType);
            Global.SaveData(xmldoc, xmlelStar, "ScannerType", ScannerType);
            Global.SaveData(xmldoc, xmlelStar, "Gravity", Gravity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "Radiation", Radiation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStar, "Temperature", Temperature.ToString(System.Globalization.CultureInfo.InvariantCulture));


            return xmlelStar;
        }

        #endregion

    }
}
