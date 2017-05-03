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
    using System.Linq;
    using System.Xml;

    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Nova.Common.Waypoints;

    /// <summary>
    /// Fleet class. A fleet is a container for one or more ships (which may be of
    /// different designs). Ship instances do not exist by themselves, they are
    /// always part of a fleet (even if they are the only ship in the fleet).
    /// A fleet may be a starbase.
    /// </summary>
    [Serializable]
    public class Fleet : Mappable
    {   
        /// <summary>
        /// Holds the ship tokens in the format "ShipDesign, Quantity, Damage%".
        /// </summary>
        private Dictionary<long, ShipToken> tokens  = new Dictionary<long, ShipToken>();
        public List<Waypoint> Waypoints   = new List<Waypoint>();

        /// <summary>
        /// The cargo carried by the entire fleet. 
        /// To avoid issues with duplication cargo is tracked at the fleet level only.
        /// </summary>
        public Cargo Cargo = new Cargo(); 
        
        public Mappable InOrbit = null;
        public double Bearing = 0;
        public double Cloaked = 0;
        public double FuelAvailable = 0;
        public double TargetDistance = 100;
        public string BattlePlan = "Default";
       
        public enum TravelStatus 
        { 
            Arrived, InTransit 
        }
        
        /// <summary>
        /// Return the total normal bombing capability.
        /// </summary>
        public Bomb BombCapability
        {
            get
            {
                Bomb totalBombs = new Bomb();
                foreach (ShipToken token in tokens.Values)
                {
                    Bomb bomb = token.Design.BombCapability * token.Quantity;
                    totalBombs.PopKill += bomb.PopKill;
                    totalBombs.Installations += bomb.Installations;
                    totalBombs.MinimumKill += bomb.MinimumKill;
                }
                return totalBombs;
            }
        }

        /// <summary>
        /// Check if any of the ships has colonization module.
        /// </summary>
        public bool CanColonize
        {
            get
            {
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.CanColonize)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Property to determine if a fleet can re-fuel.
        /// </summary>
        public bool CanRefuel
        {
            get
            {
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.CanRefuel)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// This property is true if the fleet has at least one ship with a scanner.
        /// </summary>
        public bool CanScan
        {
            get
            {
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.CanScan)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Return the composition of a fleet (ship design and number of ships of that
        /// design).
        /// </summary>
        public Dictionary<long, ShipToken> Composition
        {
            get
            {
                return tokens;
            }
        }
        
        
        /// <summary>
        /// Return Free Warp speed for fleet.
        /// </summary>
        public int FreeWarpSpeed
        {
            get
            {
                int speed = 10;
                foreach (ShipToken token in tokens.Values)
                {
                    speed = Math.Min(speed, token.Design.FreeWarpSpeed);
                }

                return speed;
            }
        }

        /// <summary>
        /// Determine if the fleet has bombers.
        /// </summary>
        public bool HasBombers
        {
            get
            {
                bool bombers = false;
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.IsBomber)
                    {
                        bombers = true;
                    }
                }
                return bombers;
            }
        }

        /// <summary>
        /// Choose an image from one of the ships in the fleet.
        /// </summary>
        public ShipIcon Icon
        {
            get
            {
                try
                {
                    ShipToken token = tokens.Values.First();
                    return token.Design.Icon;
                }
                catch
                {
                    Report.Error("Fleet.cs Fleet.Icon (get): unable to get ship image.");
                }
                return null;
            }
        }

        /// <summary>
        /// Report if a fleet is armed.
        /// </summary>
        public bool IsArmed
        {
            get
            {
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.HasWeapons)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Property to determine if a fleet is a starbase.
        /// </summary>
        public bool IsStarbase
        {
            get
            {
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.IsStarbase)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        
        /// <summary>
        /// Return the mass of a fleet.
        /// </summary>
        public int Mass
        {
            get
            {
                int totalMass = 0;

                foreach (ShipToken token in tokens.Values)
                {
                    totalMass += token.Design.Mass * token.Quantity;
                }
                totalMass += Cargo.Mass;

                return totalMass;
            }
        }

        /// <summary>
        /// Return the the number of mines this fleet can lay.
        /// </summary>
        public int NumberOfMines
        {
            get
            {
                int mineCount = 0;

                foreach (ShipToken token in tokens.Values)
                {
                    mineCount += token.Design.MineCount * token.Quantity;
                }

                return mineCount;
            }
        }
        
        /// <summary>
        /// Return the penetrating range scan capability of the fleet.
        /// FIXME (priority 4) - scanning capability can be additive (but the formula is non-linear).
        /// </summary>
        public int PenScanRange
        {
            get
            {
                int penRange = 0;
                
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.ScanRangePenetrating > penRange)
                    {
                        penRange = token.Design.ScanRangePenetrating;
                    }
                }
                return penRange;
            }
        }
        
        /// <summary>
        /// Return the non penetrating range scan capability of the fleet.
        /// FIXME (priority 4) - scanning capability can be additive (but the formula is non-linear).
        /// </summary>
        public int ScanRange
        {
            get
            {
                int scanRange = 0;
                
                foreach (ShipToken token in tokens.Values)
                {
                    if (token.Design.ScanRangeNormal > scanRange)
                    {
                        scanRange = token.Design.ScanRangeNormal;
                    }
                }
                return scanRange;
            }
        }

        /// <summary>
        /// Return the current speed of the fleet.
        /// </summary>
        public int Speed
        {
            get
            {
                Waypoint target = Waypoints[0];
                return target.WarpFactor;
            }

            set
            {
                Waypoint target = Waypoints[0];
                target.WarpFactor = 0;
            }

        }

        /// <summary>
        /// Return the current total amour strength of the fleet.
        /// </summary>
        public double TotalArmorStrength
        {
            get
            {
                return tokens.Values.Sum(token => token.Armor);
            }
        }

        /// <summary>
        /// Find the total cargo capacity of the fleet.
        /// </summary>
        public int TotalCargoCapacity
        {
            get
            {
                return tokens.Values.Sum(token => token.Design.CargoCapacity);
            }
        }

        /// <summary>
        /// Return the cost of a fleet. 
        /// </summary>
        public Resources TotalCost
        {
            get
            {
                Resources cost = new Resources();

                foreach (ShipToken token in tokens.Values)
                {
                    cost += token.Design.Cost;
                }

                return cost;
            }
        }

        /// <summary>
        /// Find the total dock capacity of the fleet.
        /// </summary>
        public int TotalDockCapacity
        {
            get
            {
                return tokens.Values.Sum(token => token.Design.DockCapacity);
            }
        }

        /// <summary>
        /// Find the total fuel capacity of all ships in the fleet.
        /// </summary>
        public int TotalFuelCapacity
        {
            get
            {
                return tokens.Values.Sum(token => token.Design.FuelCapacity);
            }
        }

        /// <summary>
        /// Return the total shield strength of the fleet.
        /// </summary>
        public double TotalShieldStrength
        {
            get
            {
                return tokens.Values.Sum(token => token.Shields);
            }
        }

        
        /// <summary>
        /// Placeholder constructor - Fleet should be replaced by a reference to the fleet with the same Key.
        /// </summary>
        public Fleet(long newKey) 
        { 
            Key = newKey; 
        }

        
        /// <summary>
        /// Fleet construction for unit testing and stack creation during a battle.
        /// </summary>
        /// <param name="name">The fleet name.</param>
        /// <param name="id">The fleet id.</param>
        /// <param name="position">The fleet position.</param>
        public Fleet(string name, ushort owner, uint id, NovaPoint position)
        {
            Name = name;
            Owner = owner;
            Id = id;
            Position = position;
        }
        
        public Fleet(Fleet copy)
            : base(copy)
        {
            
        }
        
        /// <summary>
        /// Fleet construction based on a ShipToken and some parameters from a star (this is
        /// the usual case for most fleets when a new ship is manufactured at a star).
        /// </summary>
        /// <param name="ship">The ShipToken being constructed.</param>
        /// <param name="star">The star constructing the ship.</param>
        public Fleet(ShipToken token, Star star, long newKey)
        {
            tokens.Add(token.Key, token);

            FuelAvailable = TotalFuelCapacity;
            Type          = ItemType.Fleet;

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
            Key          = newKey;    
        }
        
        
        /// <summary>
        /// Fleet construction based on a ship and some parameters from a star (this is
        /// the usual case for most fleets when a new ship is manufactured at a star).
        /// </summary>
        /// <param name="ship">The ship being constructed.</param>
        /// <param name="star">The star constructing the ship.</param>
        public Fleet(ShipDesign design, int quantity, Star star, long newKey) :
            this(new ShipToken(design, quantity), star, newKey)
        {
            
        }


        public TravelStatus GetTravelStatus()
        {
            Waypoint target = Waypoints[0];
            if (Position == target.Position)
            {
                return TravelStatus.Arrived;
            }
            else
            {
                return TravelStatus.InTransit;
            }
        }

        
        /// <summary>
        /// Move the fleet towards the waypoint at the top of the list. Fuel is consumed
        /// at the rate of the sum of each of the individual ships (i.e. available fuel
        /// is automatically "pooled" between the ships).
        /// </summary>
        /// <param name="availableTime">The portion of a year left for travel.</param>
        /// <param name="race">The race this fleet belongs to.</param>
        /// <returns>A TravelStatus indicating arrival or in-transit.</returns>
        public TravelStatus Move(ref double availableTime, Race race)
        {
            if (GetTravelStatus() == TravelStatus.Arrived)
            {
                return TravelStatus.Arrived;
            }

            Waypoint target = Waypoints[0];

            InOrbit = null;

            double legDistance = PointUtilities.Distance(Position, target.Position);

            int warpFactor = target.WarpFactor;
            int speed = warpFactor * warpFactor;
            double targetTime = legDistance / speed;
            double fuelConsumptionRate = FuelConsumption(warpFactor, race);
            double fuelTime = FuelAvailable / fuelConsumptionRate;
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

            if (travelTime >= fuelTime)
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
                Position = PointUtilities.MoveTo(Position, target.Position, travelled);
            }

            // Update the travel time left for this year and the total fuel we
            // now have available.

            availableTime -= travelTime;
            int fuelUsed = (int)(fuelConsumptionRate * travelTime);
            FuelAvailable -= fuelUsed;

            // Added check if fleet run out of full it's speed will be changed 
            // to free warp speed.
            if (arrived == TravelStatus.InTransit && fuelConsumptionRate > this.FuelAvailable)
            {
                target.WarpFactor = this.FreeWarpSpeed;
            }
            return arrived;
        }

        /// <summary>
        /// Return the fuel consumption (mg per year) of the fleet at the specified
        /// warp factor.
        /// </summary>
        /// <param name="warpFactor">The warp speed of the fleet.</param>
        /// <param name="race">The race this fleet belongs too.</param>
        /// <returns>The rate of fuel consumptionin mg / year.</returns>
        public double FuelConsumption(int warpFactor, Race race)
        {
            double fuelConsumption = 0;

            // Work out how full of cargo the fleet is.
            double cargoFullness;
            if (TotalCargoCapacity == 0)
            {
                cargoFullness = 0;
            }
            else
            {
                cargoFullness = ((double)Cargo.Mass) / ((double)TotalCargoCapacity);
            }


            foreach (ShipToken token in tokens.Values)
            {
                fuelConsumption += token.Design.FuelConsumption(warpFactor, race, (int)(token.Design.CargoCapacity * cargoFullness)); 
            }

            return fuelConsumption;
        }
        

        /// <summary>
        /// Load: initializing constructor to load a fleet from an XmlNode (save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the fleet.</param>
        public Fleet(XmlNode node)
            : base(node)
        {
            // Read the node
            XmlNode mainNode = node.FirstChild;
            try
            {
                while (mainNode != null)
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "fleetid":
                            Id = uint.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "cargo":
                            Cargo = new Cargo(mainNode);
                            break;
                        case "inorbit":
                            InOrbit = new Star();
                            InOrbit.Name = mainNode.FirstChild.Value;
                            break;
                        case "bearing":
                            Bearing = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "cloaked":
                            Cloaked = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "fuelavailable":
                            FuelAvailable = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "targetdistance":
                            TargetDistance = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "battleplan":
                            BattlePlan = mainNode.FirstChild.Value;
                            break;
                        case "tokens":
                            XmlNode subNode = mainNode.FirstChild;
                            ShipToken token;
                            while (subNode != null)
                            {
                                token = new ShipToken(subNode);
                                tokens.Add(token.Key, token);
                                subNode = subNode.NextSibling;
                            }
                            break;
                        case "waypoint":
                            Waypoint waypoint = new Waypoint(mainNode);
                            Waypoints.Add(waypoint);
                            break;

                        default: break;
                    }


                    mainNode = mainNode.NextSibling;
                }
            }
            catch (Exception e)
            {
                Report.Error("Error loading fleet:" + Environment.NewLine + e.Message);
                throw e;
            }
        }


        /// <summary>
        /// Save: Return an XmlElement representation of the Fleet.
        /// </summary>
        /// <param name="xmldoc">The parent xml document.</param>
        /// <returns>An XmlElement representation of the Fleet.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc, string nodeName = "Fleet")
        {
            XmlElement xmlelFleet = xmldoc.CreateElement(nodeName);

            xmlelFleet.AppendChild(base.ToXml(xmldoc));
            
            if (InOrbit != null)
            {
                Global.SaveData(xmldoc, xmlelFleet, "InOrbit", InOrbit.Name);
            }

            Global.SaveData(xmldoc, xmlelFleet, "Bearing", this.Bearing.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            if (Cloaked != 0)
            {
                Global.SaveData(xmldoc, xmlelFleet, "Cloaked", this.Cloaked.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            Global.SaveData(xmldoc, xmlelFleet, "FuelAvailable", this.FuelAvailable.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "FuelCapacity", this.TotalFuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleet, "TargetDistance", this.TargetDistance.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            if (Cargo.Mass > 0)
            {
                Global.SaveData(xmldoc, xmlelFleet, "CargoCapacity", this.TotalCargoCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            
            Global.SaveData(xmldoc, xmlelFleet, "BattlePlan", this.BattlePlan);

            xmlelFleet.AppendChild(Cargo.ToXml(xmldoc));

            foreach (Waypoint waypoint in Waypoints)
            {
                xmlelFleet.AppendChild(waypoint.ToXml(xmldoc));
            }

            XmlElement xmlelTokens = xmldoc.CreateElement("Tokens");
            foreach (ShipToken token in tokens.Values)
            {
                xmlelTokens.AppendChild(token.ToXml(xmldoc));
            }            
            xmlelFleet.AppendChild(xmlelTokens);

            return xmlelFleet;
        }
        
        public FleetIntel GenerateReport(ScanLevel scan, int year)
        {
            FleetIntel report = new FleetIntel(this, scan, year);
            
            return report;
        }
    }
}
