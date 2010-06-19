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
// Ship class. 
// Note that ships never exist in isolation, they are always part
// of a fleet. Consequently, all the movement attributes can be found in the
// fleet class. 
// Note that Cargo is the amount of cargo the ship is
// actually carrying (this is usually only relevant when a ship is transferred
// to another fleet or is destroyed. CargoCapacity, the maximum cargo it can
// carry, is a property of the Design.
// ===========================================================================
#endregion


using System;
using System.Collections;
using System.Xml;
using Nova.Common.Components;

namespace Nova.Common
{

    /// <summary>
    /// Ship class. 
    /// </summary>
    [Serializable]
    public class Ship : Item
    {
        // public Cargo Cargo = new Cargo(); // Cargo being carried.
        public ShipDesign Design = null;
        private bool summaryUpdated = false;

        // These are the current shield / armor values, modified by damage.
        public double Shields = 0;
        public double Armor = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Create a ship of a specified design.
        /// </summary>
        /// <param name="shipDesign"></param>
        /// ----------------------------------------------------------------------------
        public Ship(ShipDesign shipDesign)
        {
            Design = shipDesign;
            Design.Update(); // ensure summary properties have been calculated

            Shields = shipDesign.Shield;
            Armor = shipDesign.Armor;

            // Initialise inherited fields.
            Mass = shipDesign.Mass;
            Cost = shipDesign.Cost;
            Name = shipDesign.Name;
            Owner = shipDesign.Owner;
            Type = shipDesign.Type;

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor. This is only used by the battle engine so only the fields
        /// used by it in creating stacks need to be copied.  Be careful when using the
        /// copy. It is a different object to the original.
        /// </summary>
        /// <param name="copy"></param>
        /// ----------------------------------------------------------------------------
        public Ship(Ship copy)
            : base(copy)
        {
            Design = copy.Design;
            Design.Update(); // ensure summary properties are calculated
            Shields = copy.Shields;
            Armor = copy.Armor;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the summary statistics for the ship
        /// </summary>
        public void Update()
        {
            if (!summaryUpdated)
            {
                Design.Update();
                summaryUpdated = true;
            }
        }

        /// ----------------------------------------------------------------------------
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
        // fuel consumption is 15% less than advertised.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public double FuelConsumption(int warp, Race race, int cargoMass)
        {
            if (warp == 0) return 0;
            if (Design.Engine == null) return 0; // may be a star base

            double fuelFactor = Design.Engine.FuelConsumption[warp - 1];
            double efficiency = fuelFactor / 100.0;
            double speed = warp * warp;

            double fuelConsumption = (Mass + cargoMass) * efficiency * speed / 200.0;

            if (race.HasTrait("IFE"))
            {
                fuelConsumption *= 0.85;
            }

            return fuelConsumption;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get total bomb capability. 
        /// </summary>
        /// <remarks>
        /// TODO (priority 6) Whatever code uses this seems to be ignoring smart bombs?
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public Bomb BombCapability
        {
            get
            {
                Update();
                return Design.ConventionalBombs;
            }
        }


        /// <summary>
        /// Determine if this <see cref="Ship"/> can refuel.
        /// </summary>
        public bool CanRefuel
        {
            get
            {
                return Design.CanRefuel;
            }
        }

        /// <summary>
        /// The Cargo Capacity of the ship.
        /// </summary>
        public int CargoCapacity
        {
            get
            {
                Update();
                return Design.CargoCapacity;
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
                return Design.DockCapacity;
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
                return Design.FuelCapacity;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get if this ship has weapons.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool HasWeapons
        {
            get
            {
                Update();
                if (Design.Weapons == null)
                {
                    return false;
                }
                return true;
            }
        }



        /// <summary>
        /// The icon for this ship.
        /// </summary>
        public System.Drawing.Image Image
        {
            get
            {
                return Design.ShipHull.ComponentImage;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get if the ship is a bomber
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsBomber
        {
            get
            {
                Update();
                if (Design.ConventionalBombs.PopKill == 0 && Design.SmartBombs.PopKill == 0)
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
                return Design.IsStarbase;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get total mine laying capacity for this ship
        /// </summary>
        /// <remarks>
        /// TODO (priority 6) Client code must handle heavy and speed trap mines too.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public int MineCount
        {
            get
            {
                Update();
                return Design.StandardMines.LayerRate;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the power rating of this ship - stub: TODO (priority 6)
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int PowerRating
        {
            get
            {
                Update(); 
                return 0;
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
                return Design.NormalScan;
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
                return Design.PenetratingScan;
            }
        }



        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising Constructor from an xml node.
        /// Precondition: node is a "Ship" node Nova save file (xml document).
        /// </summary>
        /// <param name="node">The XmlNode of the parent.</param>
        /// ----------------------------------------------------------------------------
        public Ship(XmlNode node)
            : base(node)
        {
            try
            {

                XmlNode subnode = node.FirstChild;
                while (subnode != null)
                {

                    switch (subnode.Name.ToLower())
                    {
                        case "design":
                            Design = new ShipDesign();
                            Design.Name = ((XmlText)subnode.FirstChild).Value;
                            break;
                        case "owner":
                            Owner = ((XmlText)subnode.FirstChild).Value;
                            break;
                        case "mass":
                            Mass = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "shields":
                            Shields = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "armor":
                            Armor = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "cost":
                            Cost = new Resources(subnode);
                            break;

                    }

                    subnode = subnode.NextSibling;
                }
                Design.Update(); // ensure summary properties are calculated
            }

            catch (Exception e)
            {
                Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an xml representation of the ship for saving
        /// </summary>
        /// <param name="xmldoc">The master XmlDocument</param>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelShip = xmldoc.CreateElement("Ship");

            // Design
            Global.SaveData(xmldoc, xmlelShip, "Design", this.Design.Name);

            // Item base class
            xmlelShip.AppendChild(base.ToXml(xmldoc));

            Global.SaveData(xmldoc, xmlelShip, "Shields", this.Shields.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelShip, "Armor", this.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelShip;
        }

        #endregion

    }
}
