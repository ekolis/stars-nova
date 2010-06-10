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
// Fleet class. A fleet is a container for one or more ships (which may be of
// different designs). Ship instances do not exist by themselves, they are
// always part of a fleet (even if they are the only ship in the fleet).
// A fleet may be a starbase.
// ===========================================================================
#endregion


using System;
using System.Xml;
using System.Collections;
using System.Drawing;
using Nova.Common.Components;

namespace Nova.Common
{

    /// <summary>
    /// Fleet class.
    /// </summary>
    [Serializable]
    public class Fleet : Item
    {
        public int FleetID = 0;
        public ArrayList FleetShips = new ArrayList();
        public ArrayList Waypoints = new ArrayList();
        public Cargo Cargo = new Cargo(); // FIXME (priority 6) - Cargo should be tracked by either the fleet or the ship, not both. Need to decide on a design for cargo management and document it.
        public Fleet Target = null;
        public Star InOrbit = null;
        public double BattleSpeed = 0; // used by a stack on the battle board
        public double Bearing = 0;
        public double Cloaked = 0;
        public double FuelAvailable = 0;
        public double FuelCapacity = 0;
        public double TargetDistance = 100;
        public int CargoCapacity = 0;
        public string BattlePlan = "Default";
       
        public enum TravelStatus { Arrived, InTransit }

        #region Construction Initialisation

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// placeholder constructor - Fleet should be replaced by a reference to the fleet with the same ID
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Fleet(int id) { FleetID = id; }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Fleet construction for unit testing and stack creation during a battle.
        /// </summary>
        /// <param name="n">The fleet name.</param>
        /// <param name="o">The fleet owner.</param>
        /// <param name="p">The fleet position</param>
        /// ----------------------------------------------------------------------------
        public Fleet(string n, string o, Point p)
        {
            Name = n;
            Owner = o;
            Position = p;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor. This is only used by the battle engine so only the fields
        /// used by it in creating stacks need to be copied. Note that we copy the
        /// ships as well. Be careful when using the copy. It is a different object.
        /// </summary>
        /// <param name="copy">The fleet to copy.</param>
        /// ----------------------------------------------------------------------------
        public Fleet(Fleet copy)
            : base(copy)
        {
            this.BattleSpeed = copy.BattleSpeed;
            this.BattlePlan = copy.BattlePlan;

            foreach (Ship ship in copy.FleetShips)
            {
                this.FleetShips.Add(new Ship(ship));
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Fleet construction based on a ship and some parameters from a star (this is
        /// the usual case for most fleets when a new ship is manufactured at a star).
        /// </summary>
        /// <param name="ship">The ship being constructed.</param>
        /// <param name="star">The star constructing the ship.</param>
        /// ----------------------------------------------------------------------------
        public Fleet(Ship ship, Star star)
        {

            FleetShips.Add(ship);


            FuelCapacity          = ship.Design.FuelCapacity;
            CargoCapacity         = ship.Design.CargoCapacity;
            FuelAvailable         = FuelCapacity;
            Type                  = "Fleet";

            // Have one waypoint to reflect the fleet's current position and the
            // planet it is in orbit around.
         
            Waypoint w    = new Waypoint();      
            w.Position    = star.Position;
            w.Destination = star.Name;
            w.WarpFactor  = 0;

            Waypoints.Add(w);

            // Inititialise the fleet elements that come from the star.

            Position     = star.Position;       
            InOrbit      = star;                
            Owner        = star.Owner;
        }


        #endregion


        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Move the fleet towards the waypoint at the top of the list. Fuel is consumed
        /// at the rate of the sum of each of the individual ships (i.e. available fuel
        /// is automatically "pooled" between the ships).
        /// </summary>
        /// <param name="availableTime">The portion of a year left for travel.</param>
        /// <param name="race">The race this fleet belongs to.</param>
        /// <returns>A TravelStatus indicating arrival or in-transit.</returns>
        /// ----------------------------------------------------------------------------
        public TravelStatus Move(ref double availableTime, Race race)
        {
            Waypoint target = Waypoints[0] as Waypoint;

            if (Position == target.Position) return TravelStatus.Arrived;

            InOrbit = null;

            double legDistance = PointUtilities.Distance
                                (Position, target.Position);

            int warpFactor = target.WarpFactor;
            int speed = warpFactor * warpFactor;
            double targetTime = legDistance / speed;
            double fuelTime = FuelAvailable / FuelConsumption(warpFactor, race);
            double travelTime = targetTime;

            // Determine just how long we have available to travel towards the
            // waypoint target. This will be the smaller of target time (the ideal
            // case, we get there) available time (didn't get there but still can
            // move towards there next turn) and fuel time.

            TravelStatus arrived = TravelStatus.Arrived;

            if (travelTime > availableTime)
            {
                travelTime = availableTime;
                arrived = TravelStatus.InTransit;
            }

            if (travelTime > fuelTime)
            {
                travelTime = fuelTime;
                arrived = TravelStatus.InTransit;
            }

            // If we have arrived then the new fleet position is the waypoint
            // target. Otherwise the position is determined by how far we got
            // in the time or fuel available.

            if (arrived == TravelStatus.Arrived)
            {
                Position = target.Position;
                target.WarpFactor = 0;
            }
            else
            {
                double travelled = speed * travelTime;
                Position = PointUtilities.MoveTo
                                   (Position, target.Position, travelled);
            }

            // Update the travel time left for this year and the total fuel we
            // now have available.

            availableTime -= travelTime;
            FuelAvailable -= FuelConsumption(target.WarpFactor, race) * travelTime;

            return arrived;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the fuel consumption (mg per year) of the fleet at the specified
        /// warp factor.
        /// </summary>
        /// <param name="warpFactor">The warp speed of the fleet.</param>
        /// <param name="race">The race this fleet belongs too.</param>
        /// <returns>The mass of fuel consumed.</returns>
        /// ----------------------------------------------------------------------------
        public double FuelConsumption(int warpFactor, Race race)
        {
            double fuelConsumption = 0;

            foreach (Ship ship in FleetShips)
            {
                fuelConsumption += ship.FuelConsumption(warpFactor, race);
            }

            return fuelConsumption;
        }

        #endregion 



        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the long range scan capability of the fleet.
        /// FIXME (priority 4) - scanning capability can be addative (but the formula is non-linear)
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int LongRangeScan
        {
            get
            {
                int scanRange = 0;

                foreach (Ship ship in FleetShips)
                {
                    if (ship.Design.NormalScan > scanRange)
                    {
                        scanRange = ship.Design.NormalScan;
                    }
                }
                return scanRange;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the short range scan capability of the fleet.
        /// FIXME (priority 4) - scanning capability can be addative (but the formula is non-linear)
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int ShortRangeScan
        {
            get
            {
                int scanRange = 0;

                foreach (Ship ship in FleetShips)
                {
                    if (ship.Design.NormalScan > scanRange)
                    {
                        scanRange = ship.Design.NormalScan;
                    }
                }
                return scanRange;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// // Return the composition of a fleet (ship design and number of ships of that
        /// design).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Hashtable Composition
        {
            get
            {
                Hashtable fleetComposition = new Hashtable();

                foreach (Ship ship in FleetShips)
                {
                    fleetComposition[ship.Design.Name] = 0;
                }

                foreach (Ship ship in FleetShips)
                {
                    int count = (int)fleetComposition[ship.Design.Name];

                    count++;
                    fleetComposition[ship.Design.Name] = count;
                }

                return fleetComposition;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the mass of a fleet.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int TotalMass
        {
            get
            {
                double totalMass = 0;

                foreach (Ship ship in FleetShips)
                {
                    totalMass += ship.TotalMass;
                }

                return (int)totalMass;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the cost of a fleet. 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources TotalCost
        {
            get
            {
                Resources cost = new Resources();

                foreach (Ship ship in FleetShips)
                {
                    cost = cost + ship.Design.Cost;
                }

                return cost;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the current speed of the fleet
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Speed
        {
            get
            {
                Waypoint target = Waypoints[0] as Waypoint;
                return target.WarpFactor;
            }

            set
            {
                Waypoint target = Waypoints[0] as Waypoint;
                target.WarpFactor = 0;
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Report if a fleet is armed
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsArmed
        {
            get
            {
                foreach (Ship ship in FleetShips)
                {
                    if (ship.HasWeapons)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the current Defense capability of a fleet 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public double Defenses
        {
            get
            {
                double totalDefenses = 0;

                foreach (Ship ship in FleetShips)
                {
                    totalDefenses += ship.Armor;
                    totalDefenses += ship.Shields;
                }
                return totalDefenses;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Choose an image from one of the ships in the fleet
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Image Image
        {
            get
            {
                try
                {
                    Ship ship = FleetShips[0] as Ship;
                    return ship.Design.ShipHull.ComponentImage;
                }
                catch
                {
                    Report.Error("Fleet.cs Fleet.Image (get): unable to get ship image.");
                }
                return null;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if the fleet has bombers
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool HasBombers
        {
            get
            {
                bool bombers = false;
                foreach (Ship ship in FleetShips)
                {
                    if (ship.IsBomber)
                    {
                        bombers = true;
                    }
                }
                return bombers;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the total normal bombing capability
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Bomb BombCapability
        {
            get
            {
                Bomb totalBombs = new Bomb();
                foreach (Ship ship in FleetShips)
                {
                    Bomb bomb = ship.BombCapability;
                    totalBombs.PopKill += bomb.PopKill;
                    totalBombs.Installations += bomb.Installations;
                    totalBombs.MinimumKill += bomb.MinimumKill;
                }
                return totalBombs;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the the number of mines this fleet can lay.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int NumberOfMines
        {
            get
            {
                int mineCount = 0;

                foreach (Ship ship in FleetShips)
                {
                    mineCount += ship.MineCount;
                }

                return mineCount;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the total amour strength of the fleet
        /// </summary>
        /// ----------------------------------------------------------------------------
        public double ArmorStrength
        {
            get
            {
                double Armor = 0;

                foreach (Ship ship in FleetShips)
                {
                    Armor += ship.Armor;
                }

                return Armor;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property to determine if a fleet is a starbase.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsStarbase
        {
            get
            {
                foreach (Ship ship in FleetShips)
                {
                    if (ship.Design.IsStarbase) return true;
                }
                return false;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Property to determine if a fleet can re-fuel
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool CanRefuel
        {
            get
            {
                foreach (Ship ship in FleetShips)
                {
                    if (ship.Design.CanRefuel) return true;
                }
                return false;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the dock capacity of the fleet. This is used as a shortcut for a starbase which is normally the only 'ship' in its fleet.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int DockCapacity
        {
            get
            {
                int dockCapacity = 0;
                foreach (Ship ship in FleetShips)
                {
                    dockCapacity += ship.Design.DockCapacity;
                }
                return dockCapacity;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a key for use in hash tables to locate items.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public override string Key
        {
            get { return this.Owner + "/" + this.FleetID; }
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising constructor to load a fleet from an XmlNode (save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the fleet.</param>
        /// ----------------------------------------------------------------------------
        public Fleet(XmlNode node)
            : base(node)
        {
            // Read the node
            XmlNode subnode = node.FirstChild;

            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "fleetid": FleetID = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "targetid": Target = new Fleet(int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "cargo": Cargo = new Cargo(subnode); break;
                        case "inorbit": InOrbit = new Star(); InOrbit.Name = ((XmlText)subnode.FirstChild).Value; break;
                        case "battlespeed": BattleSpeed = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "bearing": Bearing = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "cloaked": Cloaked = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "fuelavailable": FuelAvailable = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "fuelcapacity": FuelCapacity = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "targetdistance": TargetDistance = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "cargocapacity": CargoCapacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;
                        case "battleplan": BattlePlan = ((XmlText)subnode.FirstChild).Value; break;
                        case "ship": Ship ship = new Ship(subnode); FleetShips.Add(ship); break;
                        case "waypoint": Waypoint waypoint = new Waypoint(subnode); Waypoints.Add(waypoint); break;

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
        /// Save: Return an XmlElement representation of the Fleet
        /// </summary>
        /// <param name="xmldoc">The parent xml document.</param>
        /// <returns>An XmlElement representation of the Fleet.</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelFleet = xmldoc.CreateElement("Fleet");

            xmlelFleet.AppendChild(base.ToXml(xmldoc));

            Global.SaveData(xmldoc, xmlelFleet, "FleetID", this.FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Target != null) Global.SaveData(xmldoc, xmlelFleet, "TargetID", Target.FleetID.ToString(System.Globalization.CultureInfo.InvariantCulture));
            else Global.SaveData(xmldoc, xmlelFleet, "TravelStatus", "InTransit");
            if (InOrbit != null) Global.SaveData(xmldoc, xmlelFleet, "InOrbit", InOrbit.Name);

            if (BattleSpeed != 0) Global.SaveData(xmldoc, xmlelFleet, "BattleSpeed", this.BattleSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "Bearing", this.Bearing.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Cloaked != 0) Global.SaveData(xmldoc, xmlelFleet, "Cloaked", this.Cloaked.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "FuelAvailable", this.FuelAvailable.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "FuelCapacity", this.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "TargetDistance", this.TargetDistance.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (Cargo.Mass > 0) Global.SaveData(xmldoc, xmlelFleet, "CargoCapacity", this.CargoCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "BattlePlan", this.BattlePlan);

            xmlelFleet.AppendChild(this.Cargo.ToXml(xmldoc));

            foreach (Waypoint waypoint in this.Waypoints)
            {
                xmlelFleet.AppendChild(waypoint.ToXml(xmldoc));
            }

            foreach (Ship ship in this.FleetShips)
            {
                xmlelFleet.AppendChild(ship.ToXml(xmldoc));
            }

            return xmlelFleet;
        }


        #endregion

    }
}
