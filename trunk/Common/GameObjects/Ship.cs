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

namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Nova.Common.Components;

    /// <summary>
    /// Ship class. 
    /// Note that ships never exist in isolation, they are always part
    /// of a fleet. Consequently, all the movement attributes can be found in the
    /// fleet class.  
    /// </summary>
    [Serializable]
    public class Ship : Item
    {
        private ShipDesign design;
        private bool summaryUpdated;

        // These are the current shield / armor values, modified by damage.
        public double Shields;
        public double Armor;

        /// <summary>
        /// Create a ship of a specified design.
        /// </summary>
        /// <param name="shipDesign"></param>
        public Ship(ShipDesign shipDesign, long key)
        {
            Key = key;
            
            design = shipDesign;
            
            design.Update(); // ensure summary properties have been calculated

            Shields = design.Shield;
            Armor = design.Armor;

            // Initialise inherited fields.
            Mass = design.Mass;
            Cost = design.Cost;
            Name = design.Name;
            Owner = design.Owner;
            Type = design.Type;            
        }

        /// <summary>
        /// Copy constructor. This is only used by the battle engine so only the fields
        /// used by it in creating stacks need to be copied.  Be careful when using the
        /// copy. It is a different object to the original.
        /// </summary>
        /// <param name="copy"></param>
        public Ship(Ship copy)
            : base(copy)
        {
            design = copy.design;
            design.Update(); // ensure summary properties are calculated
            Shields = copy.Shields;
            Armor = copy.Armor;
            Key = copy.Key;
        }

        public ShipDesign Design
        {
            get { return design; }
        }

        /// <summary>
        /// Replace the design of the ship.
        /// </summary>
        /// <param name="design"></param>
        public void DesignUpdate(ShipDesign shipDesign)
        {
            design = shipDesign;
            design.Update(); // ensure summary properties have been calculated

            Shields = design.Shield;
            Armor = design.Armor;

            // Initialise inherited fields.
            Mass = design.Mass;
            Cost = design.Cost;
            Name = design.Name;
            Owner = design.Owner;
            Type = design.Type;
        }

        /// <summary>
        /// Update the summary statistics for the ship.
        /// </summary>
        public void Update()
        {
            if (!summaryUpdated)
            {
                design.Update();
                summaryUpdated = true;
            }
        }

        /// <summary>
        /// Calculate fuel consumption.
        /// </summary>
        /// <param name="warp">The speed the ship is travelling.</param>
        /// <param name="race">The race the ship belongs too.</param>
        /// <param name="cargoMass">The mass of any cargo carried (ship mass will be added automatically).</param>
        /// <returns>The ship fuel consumption rate in mg per year.</returns>
        /// <remarks>
        /// Ship_fuel_usage = ship_mass x efficiency x distance / 200
        ///
        /// As distance = speed * time, and we are setting time to 1 year, then we can
        /// just drop speed into the above equation and end up with mg per year. 
        ///
        /// If the secondary racial trait "improved fuel efficiency" is set then
        /// fuel consumption is 15% less than advertised.
        /// </remarks>
        public double FuelConsumption(int warp, Race race, int cargoMass)
        {
            if (warp == 0)
            {
                return 0;
            }
            if (design.Engine == null)
            {
                return 0; // may be a star base
            }

            double fuelFactor = design.Engine.FuelConsumption[warp - 1];
            double efficiency = fuelFactor / 100.0;
            double speed = warp * warp;

            double fuelConsumption = (Mass + cargoMass) * efficiency * speed / 200.0;

            if (race.HasTrait("IFE"))
            {
                fuelConsumption *= 0.85;
            }

            return fuelConsumption;
        }

        /// <summary>
        /// Get the highest speed the ship can travel for 0 fuel.
        /// </summary>
        public int FreeWarpSpeed
        {
            get
            {
                return design.Engine.FreeWarpSpeed; 
            }
        }

        /// <summary>
        /// Checks if ship can colonize.
        /// </summary>
        public bool CanColonize
        {
            get
            {
                return design.Summary.Properties.ContainsKey("Colonizer");
            }
        }

        /// <summary>
        /// The battle speed of a ship.
        /// </summary>
        public double BattleSpeed
        {
            get
            {
                return design.BattleSpeed;
            }
        }

        /// <summary>
        /// Get total bomb capability. 
        /// </summary>
        /// <remarks>
        /// TODO (priority 6) Whatever code uses this seems to be ignoring smart bombs?
        /// </remarks>
        public Bomb BombCapability
        {
            get
            {
                Update();
                return design.ConventionalBombs;
            }
        }

        /// <summary>
        /// Determine if this <see cref="Ship"/> can refuel.
        /// </summary>
        public bool CanRefuel
        {
            get
            {
                return design.CanRefuel;
            }
        }

        /// <summary>
        /// The Cargo Capacity of the ship.
        /// Note the cargo carried is tracked by the <see cref="Fleet"/>.
        /// </summary>
        public int CargoCapacity
        {
            get
            {
                Update();
                return design.CargoCapacity;
            }
        }

        /// <summary>
        /// The armor of the underlying ship's design.
        /// </summary>
        public int DesignArmor
        {
            get
            {
                return design.Armor;
            }
        }

        /// <summary>
        /// The <see cref="Resources"/> cost of the ship's underlying design.
        /// </summary>
        public Resources DesignCost
        {
            get
            {
                return design.Cost;
            }
        }
        

        /// <summary>
        /// The name of the ship's underlying design.
        /// </summary>
        public long DesignKey
        {
            get
            {
                return design.Key;
            }
        }

        /// <summary>
        /// The shield strength of the underlying design.
        /// </summary>
        public int DesignShield
        {
            get
            {
                return design.Shield;
            }
        }

        /// <summary>
        /// The maximum sized ship that can be produced.
        /// </summary>
        public int DockCapacity
        {
            get
            {
                Update();
                return design.DockCapacity;
            }
        }

        /// <summary>
        /// The fuel capacity of this ship.
        /// </summary>
        public int FuelCapacity
        {
            get
            {
                Update();
                return design.FuelCapacity;
            }
        }

        /// <summary>
        /// Get if this ship has weapons.
        /// </summary>
        public bool HasWeapons
        {
            get
            {
                Update();
                if (design.Weapons == null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// The icon for this ship.
        /// </summary>
        public ShipIcon Icon    
        {
            get
            {
                return design.Icon;
            }
        }

        /// <summary>
        /// Get if the ship is a bomber.
        /// </summary>
        public bool IsBomber
        {
            get
            {
                Update();
                if (design.ConventionalBombs.PopKill == 0 && this.design.SmartBombs.PopKill == 0)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Determine if this <see cref="Ship"/> is a starbase.
        /// </summary>
        public bool IsStarbase
        {
            get
            {
                return design.IsStarbase;
            }
        }

        /// <summary>
        /// Get total mine laying capacity for this ship.
        /// </summary>
        /// <remarks>
        /// TODO (priority 6) Client code must handle heavy and speed trap mines too.
        /// </remarks>
        public int MineCount
        {
            get
            {
                Update();
                return design.StandardMines.LayerRate;
            }
        }

        /// <summary>
        /// Get the power rating of this ship - stub: TODO (priority 6).
        /// </summary>
        public int PowerRating
        {
            get
            {
                Update(); 
                return 0;
            }
        }

        /// <summary>
        /// True if the ship has any sort of scanner.
        /// </summary>
        public bool CanScan
        {
            get
            {
                if (design.CanScan)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// The range of the ship's normal scanners.
        /// </summary>
        public int ScanRangeNormal
        {
            get
            {
                Update();
                return design.NormalScan;
            }
        }

        /// <summary>
        /// The range of the ship's penetrating scanners.
        /// </summary>
        public int ScanRangePenetrating
        {
            get
            {
                Update();
                return design.PenetratingScan;
            }
        }

        /// <summary>
        /// The ship's weapons.
        /// </summary>
        public List<Weapon> Weapons
        {
            get
            {
                return design.Weapons;
            }
        }

        /// <summary>
        /// Initialising Constructor from an xml node.
        /// Precondition: node is a "Ship" node Nova save file (xml document).
        /// </summary>
        /// <param name="node">The XmlNode of the parent.</param>
        public Ship(XmlNode node)
            : base(node)
        {
            try
            {
                XmlNode mainNode = node.FirstChild;
                while (mainNode != null)
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "design":
                            design = new ShipDesign(long.Parse(mainNode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber));
                            break;
                        case "mass":
                            Mass = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "shields":
                            Shields = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "armor":
                            Armor = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "cost":
                            Cost = new Resources(mainNode);
                            break;
                    }

                    mainNode = mainNode.NextSibling;
                }
                this.design.Update(); // ensure summary properties are calculated
            }

            catch (Exception e)
            {
                Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
            }
        }

        /// <summary>
        /// Generate an xml representation of the ship for saving.
        /// </summary>
        /// <param name="xmldoc">The master XmlDocument.</param>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelShip = xmldoc.CreateElement("Ship");

            // Design
            Global.SaveData(xmldoc, xmlelShip, "Design", this.design.Key.ToString("X"));

            // Item base class
            xmlelShip.AppendChild(base.ToXml(xmldoc));

            Global.SaveData(xmldoc, xmlelShip, "Shields", this.Shields.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelShip, "Armor", this.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelShip;
        }
        
        public ShipIntel GenerateReport()
        {
            ShipIntel report = new ShipIntel();
            report.Design   = Design.Key;
            report.Name     = Name;
            report.Count    = 1; // This has to be updated at Fleet or FleetIntel level
            
            return report;
        }
    }
}
