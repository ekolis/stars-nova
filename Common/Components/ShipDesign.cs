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

namespace Nova.Common.Components
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// This module defines the potential design of a ship. Details of the actual
    /// design are only available once the hull modules have been populated.
    /// </summary>
    [Serializable]
    public class ShipDesign : Item
    {
        // This is the component that contains the Hull property, to which all other ships components attach.
        public Component Blueprint 
        {
            get; 
            set;
        }

        // Note there are get properties for: Armor, Shield, FuelCapcity, CargoCapacity, etc

        // The Summary is a 'super' component with properties representing the sum of all 
        // components added to the ship. 
        public Component Summary = new Component();

        // The following items can't be fully sumarised, as their properties can't be simply added.
        // For example each weapon stack will fire separately at its own initiative.
        public List<Weapon> Weapons = new List<Weapon>();

        // The bombing capability of a ship can be summarised by the sum of its 
        // Conventional bombs and the sum of its smart bombs.
        public Bomb ConventionalBombs = new Bomb(0, 0, 0, false);
        public Bomb SmartBombs = new Bomb(0, 0, 0, true);

        // Mine layers which create different types of minefields.
        // Note we assume that there will be three types of minefields: standard, heavy and speed bump
        // and they can be distinguised by the % chance of collision. (0.3, 1.0 and 3.5 respectivly).
        public MineLayer StandardMines = new MineLayer();
        public MineLayer HeavyMines = new MineLayer();
        public MineLayer SpeedBumbMines = new MineLayer();

        /// <summary>
        /// The image assigned to this ship design, which may be different from the default hull module component image. 
        /// The ship image shall be selectable when the ship is designed.
        /// </summary>
        public ShipIcon Icon = null;
        
        /// <summary>
        // Returns the Hull directly for easy Module access.
        /// </summary>
        public Hull Hull
        {
            get
            {
                return Blueprint.Properties["Hull"] as Hull;
            }
        }
        
        /// <summary>
        /// Returns the total Mass (No cargo) of this design.
        /// </summary>
        public int Mass
        {
            get
            {
                return Summary.Mass;   
            }
        }
        
        /// <summary>
        /// Returns the total Cost of this design.
        /// </summary>
        public Resources Cost
        {
            get
            {
                return Summary.Cost;
            }
        }
        
        /// <summary>
        /// Get the total shield value of this ShipDesign.
        /// </summary>
        public int Shield
        {
            get
            {
                if (Summary.Properties.ContainsKey("Shield"))
                {
                    return ((IntegerProperty)Summary.Properties["Shield"]).Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the total Armor value of this ShipDesign.
        /// </summary>
        public int Armor
        {
            get
            {
                if (Summary.Properties.ContainsKey("Armor"))
                {
                    return ((IntegerProperty)Summary.Properties["Armor"]).Value;
                }
                else
                {
                    return 0;
                }
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
        /// Get the total FuelCapacity of this ShipDesign.
        /// </summary>
        public int FuelCapacity
        {
            get
            {
                if (Summary.Properties.Count == 0)
                {
                    Update();
                }
                if (Summary.Properties.ContainsKey("Fuel"))
                {
                    return ((Fuel)Summary.Properties["Fuel"]).Capacity;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the total cargo capacity of this design.
        /// </summary>
        public int CargoCapacity
        {
            get
            {
                if (Summary.Properties.ContainsKey("Cargo"))
                {
                    return ((IntegerProperty)Summary.Properties["Cargo"]).Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the dock capacity of this ShipDesign (0 if none).
        /// </summary>
        public int DockCapacity
        {
            get
            {
                if (Blueprint.Properties.ContainsKey("Hull"))
                {
                    return ((Hull)Blueprint.Properties["Hull"]).DockCapacity;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// True if the ship design includes a scanner.
        /// </summary>
        public bool CanScan
        {
            get
            {
                if (Summary.Properties.ContainsKey("Scanner"))
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
        /// Get the normal scanner capability of this ShipDesign (0 if none).
        /// </summary>
        public int NormalScan
        {
            get
            {
                if (Summary.Properties.ContainsKey("Scanner"))
                {
                    return ((Scanner)Summary.Properties["Scanner"]).NormalScan;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the penetrating scanner ability of this ShipDesign (0 if none).
        /// </summary>
        public int PenetratingScan
        {
            get
            {
                if (Summary.Properties.ContainsKey("Scanner"))
                {
                    return ((Scanner)Summary.Properties["Scanner"]).PenetratingScan;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the engine component fitted to this ShipDesign (null if none).
        /// </summary>
        public Engine Engine
        {
            get
            {
                if (Summary.Properties.ContainsKey("Engine"))
                {
                    return (Engine)Summary.Properties["Engine"];
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// Get the highest speed the ship can travel for 0 fuel.
        /// </summary>
        public int FreeWarpSpeed
        {
            get
            {
                return Engine.FreeWarpSpeed; 
            }
        }

        /// <summary>
        /// Get this design's battle speed (0.0 if it can't move, i.e. star-base).
        /// </summary>
        public double BattleSpeed
        {
            get
            {
                if (IsStarbase)
                {
                    return 0.0;
                }

                // From the manual: Movement = (Ideal_Speed_of_Engine - 4) / 4 - (weight / 70 /4 / Number_of_Engines) + (Number_ofManeuvering_Jets / 4) + (Num_Overthrusters / 2)
                double speed = 0;
                if (Summary.Properties.ContainsKey("Engine"))
                {
                    Engine engine = (Engine)Summary.Properties["Engine"];
                    speed = (((double)engine.OptimalSpeed) - 4.0) / 4.0;
                    speed -= Summary.Mass / 70 / 4 / Number_of_Engines;
                }
                if (Summary.Properties.ContainsKey("Battle Movement"))
                {
                    speed += ((DoubleProperty)Summary.Properties["Battle Movement"]).Value;
                }
                // ship speed is always between 0.5 and 2.5 in increments of 0.25
                if (speed < 0.5)
                {
                    speed = 0.5; // Set a minimum ship speed.
                }
                if (speed > 2.5)
                {
                    speed = 2.5;
                }
                speed = ((double)((int)((speed * 4.0) + 0.5))) / 4.0;
                return speed;
            }
        }

        /// <summary>
        /// Get the total beam deflection capability.
        /// </summary>
        public double BeamDeflectors
        {
            get
            {
                if (Summary.Properties.ContainsKey("Deflector"))
                {
                    return ((ProbabilityProperty)Summary.Properties["Deflector"]).Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get a count of the number of engines. Assumes there is only one engine stack.
        /// </summary>
        public int Number_of_Engines
        {
            get
            {
                if (Blueprint.Properties.ContainsKey("Hull"))
                {
                    foreach (HullModule module in Hull.Modules)
                    {
                        if (module.AllocatedComponent != null && module.AllocatedComponent.Type == ItemType.Engine)
                        {
                            return module.ComponentCount;
                        }
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// Determine if this is a starbase hull.
        /// </summary>
        public bool IsStarbase
        {
            get
            {
                if (Blueprint.Properties.ContainsKey("Hull"))
                {
                    return Hull.IsStarbase;
                }
                // It doesn't even have a Hull!
                Report.Error("ShipDesign.IsStarbase called on a design with no hull.");
                return false;
            }
        }

        /// <summary>
        /// Get if this is a starbase that can provide unlimited fuel.
        /// </summary>
        public bool CanRefuel
        {
            get
            {
                if (Blueprint.Properties.ContainsKey("Hull"))
                {
                    return Hull.CanRefuel;
                }
                // It doesn't even have a Hull!
                Report.Error("ShipDesign.CanRefuel called on a design with no hull.");
                return false;
            }
        }

        /// <summary>
        /// Get the initiative of the ShipDesign, including computers but not weapon initiative.
        /// </summary>
        public int Initiative
        {
            get
            {
                int initiative = 0;
                if (Blueprint.Properties.ContainsKey("Hull"))
                {
                    initiative += ((Hull)Blueprint.Properties["Hull"]).BattleInitiative;
                }
                if (Summary.Properties.ContainsKey("Computer"))
                {
                    initiative += ((Computer)Summary.Properties["Computer"]).Initiative;
                }
                return initiative;
            }
        }        
        
        /// <summary>
        /// Get total bomb capability. 
        /// </summary>
        /// <remarks>
        /// TODO (priority 6) Whatever code uses this seems to be ignoring smart bombs.
        /// </remarks>
        public Bomb BombCapability
        {
            get
            {
                Update();
                return ConventionalBombs;
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
                if (ConventionalBombs.PopKill == 0 && SmartBombs.PopKill == 0)
                {
                    return false;
                }
                return true;
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
                return StandardMines.LayerRate;
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
                if (Weapons == null)
                {
                    return false;
                }
                return true;
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
                return NormalScan;
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
                return PenetratingScan;
            }
        }
        
        /// <summary>
        /// Checks if ship can colonize.
        /// </summary>
        public bool CanColonize
        {
            get
            {
                return Summary.Properties.ContainsKey("Colonizer");
            }
        }
        
        /// <summary>
        /// Parametric Constructors. Stores just the
        /// Design Key for later lookup.
        /// </summary>
        /// <param name="designkey"></param>
        public ShipDesign(long designkey)
            : base(designkey)
        {
            Key = designkey;
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">ShipDesign to copy.</param>
        public ShipDesign(ShipDesign copy)
            : base(copy)
        {
            Icon = (ShipIcon)copy.Icon.Clone();
            Blueprint = new Component(copy.Blueprint);
            Update();
        }

        /// <summary>
        /// The ship design object has all information that could be found from a scan
        /// of the the ship hull modules. However scanning these for a particular piece
        /// of information is inefficient. This method reorganizes the information
        /// to save other routines from having to do this.
        /// </summary>
        public void Update()
        {
            if (Blueprint == null)
            {
                return; // not much of a ship yet
            }

            if ( ! Blueprint.Properties.ContainsKey("Hull"))
            {
                return; // still not much of a ship.
            }

            // Start by copying the basic properties of the hull
            Summary = new Component(Blueprint);

            // Add those properties which are included with the hull

            IntegerProperty armor = new IntegerProperty(Hull.ArmorStrength);
            Summary.Properties.Add("Armor", armor);
            IntegerProperty cargo = new IntegerProperty(Hull.BaseCargo);
            Summary.Properties.Add("Cargo", cargo);

            if ( ! Summary.Properties.ContainsKey("Fuel"))
            {
                Fuel fuel = new Fuel(Hull.FuelCapacity, 0);
                Summary.Properties.Add("Fuel", fuel);
            }
            // Check any non Hull properties of the ShipHull
            foreach (string key in Blueprint.Properties.Keys)
            {
                if (key != "Hull")
                {
                SumProperty(Blueprint.Properties[key], key, 1);
                }
            }

            // Then add all of the components fitted to the hull modules.
            foreach (HullModule module in Hull.Modules)
            {
                if (module.AllocatedComponent != null)
                {
                    // Sumarise the mass & cost
                    Summary.Mass += module.AllocatedComponent.Mass;
                    Summary.Cost += module.AllocatedComponent.Cost;
                    // Summarise the properties
                    foreach (string key in module.AllocatedComponent.Properties.Keys)
                    {
                        SumProperty(module.AllocatedComponent.Properties[key], key, module.ComponentCount);
                    }
                }
            }
        }

        /// <summary>
        /// Add a property to the ShipDesign.Summary.
        /// </summary>
        /// <param name="property">
        /// The property to be added to the ShipDesign.Summary.
        /// </param><param name="type">
        /// The type of the property: one of Component.propertyKeys, normally 
        /// the key used to obtain it from a Properties dictionary.
        /// </param>
        private void SumProperty(ComponentProperty property, string type, int componentCount)
        {
            switch (type)
            {
                // properties that can be summed up to a single property
                case "Armor":
                case "Capacitor":
                case "Cargo":
                case "Cloak":
                case "Computer":
                case "Defense":
                case "Driver":
                case "Fuel":
                case "Jammer":
                case "Movement":
                case "Orbital Adjuster":
                case "Radiation":
                case "Robot":
                case "Scanner":
                case "Shield":
                case "Terraforming":
                    if (Summary.Properties.ContainsKey(type))
                    {
                        ComponentProperty toAdd = property.Clone() as ComponentProperty; // create a copy so scaling doesn't mess it up.
                        toAdd.Scale(componentCount);
                        Summary.Properties[type].Add(toAdd);
                    }
                    else
                    {
                        ComponentProperty toAdd = property.Clone() as ComponentProperty; // create a copy so scaling doesn't mess it up.
                        toAdd.Scale(componentCount);
                        Summary.Properties.Add(type, toAdd);
                    }
                    break;

                // sum up the components in the slot, but keep a separate entry for 'different components'<-- has different meaning for each of these
                case "Bomb":
                    Bomb bomb = property as Bomb;
                    if (bomb.IsSmart)
                    {
                        SmartBombs += bomb * componentCount;
                    }
                    else
                    {
                        ConventionalBombs += bomb * componentCount;
                    }
                    break;
                case "Mine Layer":
                    MineLayer layer = property as MineLayer;
                    if (layer.HitChance == MineLayer.HeavyHitChance)
                    {
                        HeavyMines += layer * componentCount;
                    }
                    else if (layer.HitChance == MineLayer.SpeedTrapHitChance)
                    {
                        SpeedBumbMines += layer * componentCount;
                    }
                    else
                    {
                        StandardMines += layer * componentCount;
                    }
                    break;

                case "Weapon":
                    Weapon weapon = property as Weapon;
                    Weapons.Add(weapon * componentCount);
                    break;

                // keep one of each type only - TODO (priority 2) keep the right one
                case "Colonizer":
                case "Engine":
                case "Gate":
                case "Hull":
                case "Mine Layer Efficiency":
                    if (Summary.Properties.ContainsKey(type))
                    {
                        break;
                    }
                    else
                    {
                        Summary.Properties.Add(type, property);
                    }
                    break;

                // Ignore in this context
                case "Hull Affinity":
                case "Transport Ships Only":
                    break;
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
            if (Engine == null)
            {
                return 0; // may be a star base
            }

            double fuelFactor = Engine.FuelConsumption[warp - 1];
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
        /// Removes all allocated components on this design,
        /// but keeps the Hull.
        /// </summary>
        public void ClearAllocated()
        {
            foreach (HullModule module in Hull.Modules)
            {
                module.Empty();
            }
        }
        
        
        /// <summary>
        /// Generate an XmlElement representation of the ShipDesign for saving to file.
        /// Note this uses the minimal approach of storing the ship hull object 
        /// (and recursing through all components). All figured values will need to be 
        /// recalculated on loading.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the ShipDesign.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelShipDesign = xmldoc.CreateElement("ShipDesign");
            xmlelShipDesign.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelShipDesign, "Icon", Icon.Source);
            xmlelShipDesign.AppendChild(Blueprint.ToXml(xmldoc));            
            return xmlelShipDesign;
        }

        /// <summary>
        /// Load: initializing Constructor from an xml node.
        /// </summary>
        /// <param name="node">A "ShipDesign" node Nova save file (xml document).</param>
        public ShipDesign(XmlNode node)
            : base(node)
        {
            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "component":
                            Blueprint = new Component(mainNode);
                            break;
                        case "icon":
                            string iconSource = mainNode.FirstChild.Value;
                            Icon = AllShipIcons.Data.GetIconBySource(iconSource);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Ship Design : " + e.Message);
                }
                mainNode = mainNode.NextSibling;
            }
        }
    }
}

