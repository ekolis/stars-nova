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

using System.Collections.Generic;

namespace Nova.Common
{
    using System;
    using System.Linq;
    using System.Xml;

    using Nova.Common.Components;
    using Nova.Common.DataStructures;

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
        /// Holds the ship tokens in the format "ShipDesign, Quantity, Damage%"
        /// </summary>
        public List<ShipToken> Tokens  = new List<ShipToken>();
        public List<Waypoint>   Waypoints   = new List<Waypoint>();

        /// <summary>
        /// The cargo carried by the entire fleet. 
        /// To avoid issues with duplication cargo is traked at the fleet level only.
        /// </summary>
        public Cargo Cargo = new Cargo(); 

        public Fleet Target = null;
        public Mappable InOrbit = null;
        public double BattleSpeed = 0; // used by a stack on the battle board
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
                foreach (ShipToken token in Tokens)
                {
                    Bomb bomb = (token.Design.BombCapability * token.Quantity);
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
                foreach (ShipToken token in Tokens)
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
                foreach (ShipToken token in Tokens)
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
                foreach (ShipToken token in Tokens)
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
        public Dictionary<ShipDesign, int> Composition
        {
            get
            {
                Dictionary<ShipDesign, int> fleetComposition = new Dictionary<ShipDesign, int>();

                foreach (ShipToken token in Tokens)
                {
                    int count;
                    if (!fleetComposition.TryGetValue(token.Design, out count))
                    {
                        count = 0;
                    }
                    count += token.Quantity;
                    fleetComposition[token.Design] = count;
                }

                return fleetComposition;
            }
        }
        
        public Dictionary<long, ShipIntel> CompositionReport
        {
            get
            {
                Dictionary<long, ShipIntel> compositionReport = new Dictionary<long, ShipIntel>();
                
                foreach (ShipToken token in Tokens)
                {
                    compositionReport.Add(token.Design.Key, token.GenerateReport());
                }
                
                return compositionReport;
            }
        }

        /// <summary>
        /// Return the current Defense capability of a fleet.
        /// </summary>
        public double Defenses
        {
            get
            {
                double totalDefenses = 0;

                foreach (ShipToken token in Tokens)
                {
                    totalDefenses += (token.Design.Armor * token.Quantity);
                    totalDefenses += (token.Design.Shield * token.Quantity);
                }
                return totalDefenses;
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
                foreach (ShipToken token in Tokens)
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
                foreach (ShipToken token in Tokens)
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
                    ShipToken token = Tokens[0];
                    return token.Design.Icon;
                }
                catch
                {
                    Report.Error("Fleet.cs Fleet.Image (get): unable to get ship image.");
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
                foreach (ShipToken token in Tokens)
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
                foreach (ShipToken token in Tokens)
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

                foreach (ShipToken token in Tokens)
                {
                    totalMass += (token.Design.Mass * token.Quantity);
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

                foreach (ShipToken token in Tokens)
                {
                    mineCount += (token.Design.MineCount * token.Quantity);
                }

                return mineCount;
            }
        }
        
        /// <summary>
        /// Return the penetrating range scan capability of the fleet.
        /// FIXME (priority 4) - scanning capability can be addative (but the formula is non-linear).
        /// </summary>
        public int PenScanRange
        {
            get
            {
                int penRange = 0;
                
                foreach (ShipToken token in Tokens)
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
        /// FIXME (priority 4) - scanning capability can be addative (but the formula is non-linear).
        /// </summary>
        public int ScanRange
        {
            get
            {
                int scanRange = 0;
                
                foreach (ShipToken token in Tokens)
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
        /// Return the total amour strength of the fleet.
        /// </summary>
        public double TotalArmorStrength
        {
            get
            {
                double armor = 0;

                foreach (ShipToken token in Tokens)
                {
                    armor += (token.Design.Armor * token.Quantity);
                }

                return armor;
            }
        }

        /// <summary>
        /// Find the total cargo capacity of the fleet.
        /// </summary>
        public int TotalCargoCapacity
        {
            get
            {
                return Tokens.Sum(token => token.Design.CargoCapacity);
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

                foreach (ShipToken token in Tokens)
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
                return Tokens.Sum(token => token.Design.DockCapacity);
            }
        }

        /// <summary>
        /// Find the total fuel capacity of all ships in the fleet.
        /// </summary>
        public int TotalFuelCapacity
        {
            get
            {
                return Tokens.Sum(token => token.Design.FuelCapacity);
            }
        }

        /// <summary>
        /// Return the total shield strength of the fleet.
        /// </summary>
        public double TotalShieldStrength
        {
            get
            {
                return Tokens.Sum(token => token.Design.Shield);
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

        
        /// <summary>
        /// Copy constructor. This is only used by the battle engine so only the fields
        /// used by it in creating stacks need to be copied. Note that we copy the
        /// ships as well. Be careful when using the copy. It is a different object.
        /// </summary>
        /// <param name="copy">The fleet to copy.</param>
        public Fleet(Fleet copy)
            : base(copy)
        {
            BattleSpeed = copy.BattleSpeed;
            BattlePlan = copy.BattlePlan;
            Key = copy.Key;  // FIXME (priority 5): Why are we copying fleets? copying this ID worries me..
                             // We are copying fleets as a step toward making battle stacks. 
                             // It should not really be a copy as a stack contains only one ship design. 
                             // Need to work out what happens when we have a fleet contianing multiple designs and then form stacks for the battle engine. 
                             // To the best of my knowledge this has never happend in nova because merging fleets was not previously possible. Not sure if it is yet...
                             // -- Dan 09 Jul 11
            Target = copy.Target;
            InOrbit = copy.InOrbit;
       
            foreach (ShipToken token in copy.Tokens)
            {
                this.Tokens.Add(token);
            }
        }

        
        /// <summary>
        /// Fleet construction based on a ShipToken and some parameters from a star (this is
        /// the usual case for most fleets when a new ship is manufactured at a star).
        /// </summary>
        /// <param name="ship">The ShipToken being constructed.</param>
        /// <param name="star">The star constructing the ship.</param>
        public Fleet(ShipToken token, Star star, long newKey)
        {
            Tokens.Add(token);

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
            Waypoint target = Waypoints[0];

            if (Position == target.Position)
            {
                return TravelStatus.Arrived;
            }

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


            foreach (ShipToken token in Tokens)
            {
                fuelConsumption += token.Design.FuelConsumption(warpFactor, race, (int)(token.Design.CargoCapacity * cargoFullness)); 
            }

            return fuelConsumption;
        }
        

        /// <summary>
        /// Load: Initialising constructor to load a fleet from an XmlNode (save file).
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
                        case "targetid":
                            Target = new Fleet(long.Parse(mainNode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber));
                            break;
                        case "cargo":
                            Cargo = new Cargo(mainNode);
                            break;
                        case "inorbit":
                            InOrbit = new Star();
                            InOrbit.Name = mainNode.FirstChild.Value;
                            break;
                        case "battlespeed":
                            BattleSpeed = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
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
                            while (subNode != null)
                            {
                                Tokens.Add(new ShipToken(subNode));
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
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelFleet = xmldoc.CreateElement("Fleet");

            xmlelFleet.AppendChild(base.ToXml(xmldoc));

            if (Target != null)
            {
                Global.SaveData(xmldoc, xmlelFleet, "TargetID", Target.Key.ToString("X"));
            }
            else
            {
                Global.SaveData(xmldoc, xmlelFleet, "TravelStatus", "InTransit");
            }
            if (InOrbit != null)
            {
                Global.SaveData(xmldoc, xmlelFleet, "InOrbit", InOrbit.Name);
            }

            if (BattleSpeed != 0)
            {
                Global.SaveData(xmldoc, xmlelFleet, "BattleSpeed", this.BattleSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
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
            foreach (ShipToken token in Tokens)
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
