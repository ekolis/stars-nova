// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module defines the potential design of a ship. Details of the actual
// design are ony available once the hull modules have been populated.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
   [Serializable]
   public class ShipDesign : Design
   {

       // This is the component that contains the Hull property, to which all other ships components attach.
       public Component ShipHull = null;

       // The following information could be found from a scan of the the ShipHull
       // hull modules but the ship designer fills them in for us as a
       // convenience to save having to keep looking them up.

       // Note there are get properties for: Armor, Shield, FuelCapcity, CargoCapacity, etc

       // The Summary is a 'super' component with properties representing the sum of all 
       // components added to the ship. 
       public Component Summary = new Component();
       // The following items can't be fully sumarised, as their properties can't be simply added.
       // For example each weapon stack will fire separately at its own initiative.
      public List<Weapon>       Weapons       = new List<Weapon>();
       // The bombing capability of a ship can be summarised by the sum of its 
       // Conventional bombs and the sum of its smart bombs.
      public Bomb ConventionalBombs = new Bomb();
      public Bomb SmartBombs = new Bomb();
       // Mine layers which create different types of minefields.
       // Note we assume that there will be three types of minefields: standard, heavy and speed bump
       // and they can be distinguised by the % chance of collision. (0.3, 1.0 and 3.5 respectivly).
       public MineLayer StandardMines = new MineLayer();
       public MineLayer HeavyMines = new MineLayer();
       public MineLayer SpeedBumbMines = new MineLayer();


       // ============================================================================
       /// <summary>
       /// The ship design object has all information that could be found from a scan
       /// of the the ship hull modules. However scanning these for a particular piece
       /// of information is inefficient. This method reorganises the information
       /// to save other routines from having to do this.
       ///</summary>
       // ============================================================================
       public void Update()
       {
           if (ShipHull == null) return; // not much of a ship yet
           Hull hullProperties = null;
           if (ShipHull.Properties.ContainsKey("Hull"))
           {
               hullProperties = ShipHull.Properties["Hull"] as Hull;
           }
           else
               return; // still not much of a ship.

           // Start by copying the basic properties of the hull
           Summary = new Component(ShipHull);

           // Add those properties which are included with the hull
           IntegerProperty armor = new IntegerProperty(hullProperties.ArmorStrength);
           Summary.Properties.Add("Armor", armor);
           IntegerProperty cargo = new IntegerProperty(hullProperties.BaseCargo);
           Summary.Properties.Add("Cargo", cargo);
           Fuel fuel = new Fuel(hullProperties.FuelCapacity, 0);
           Summary.Properties.Add("Fuel", fuel);
        

           // Check any non Hull properties of the ShipHull
           foreach (String key in ShipHull.Properties.Keys)
           {
               if (key == "Hull") continue;
               SumProperty(ShipHull.Properties[key], key, 1);
           }

           // Then add all of the components fitted to the hull modules.
           foreach (HullModule module in hullProperties.Modules)
           {
               if (module.AllocatedComponent == null) continue;
               // Sumarise the mass & cost
               Summary.Mass += module.AllocatedComponent.Mass;
               Summary.Cost += module.AllocatedComponent.Cost;
               // Summarise the properties
               foreach (String key in module.AllocatedComponent.Properties.Keys)
               {
                   SumProperty(module.AllocatedComponent.Properties[key], key, module.ComponentCount);
               }
           }
       }


       // ============================================================================
       /// <summary>
       /// Add a property to the ShipDesign.Summary.
       /// </summary>
       /// <param name="property">
       /// The property to be added to the ShipDesign.Summary
       /// </param><param name="type">
       /// The type of the property: one of Component.propertyKeys, normally 
       /// the key used to obtain it from a Properties dictionary.
       /// </param>
       // ============================================================================
       private void SumProperty(ComponentProperty property, String type, int componentCount)
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
                       Summary.Properties[type] += property * componentCount;
                   }
                   else
                   {
                       Summary.Properties.Add(type, property * componentCount);
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

                   // keep one only - TODO (low priority) keep the right one
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

       public int Armor
       {
           get
           {
               Update(); // TODO - too much doing this every time - need a more efficient way
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
       public int FuelCapacity
       {
           get
           {
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
       public int DockCapacity
       {           
           get
           {
               if (ShipHull.Properties.ContainsKey("Hull"))
               {
                   return ((Hull)ShipHull.Properties["Hull"]).DockCapacity;
                   
               }
               else
               {
                   return 0;
               }
           }
       }
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
       public double BattleSpeed
       {
           get
           {
               if (IsStarbase) return 0.0;

               // From the manual: Movement = (Ideal_Speed_of_Engine - 4) / 4 - (weight / 70 /4 / Number_of_Engines) + (Number_ofManeuvering_Jets / 4) + (Num_Overthrusters / 2)
               double speed = 0;
               if (Summary.Properties.ContainsKey("Engine"))
               {
                   Engine engine = (Engine) Summary.Properties["Engine"];
                   speed = (((double) engine.OptimalSpeed) - 4.0) / 4.0;
                   speed -= Summary.Mass / 70 / 4 / Number_of_Engines;
               }
               if (Summary.Properties.ContainsKey("Battle Movement"))
               {
                   speed += ((DoubleProperty)Summary.Properties["Battle Movement"]).Value;
               }
               // ship speed is always between 0.5 and 2.5 in increments of 0.25
               if (speed < 0.5)
                   speed = 0.5; // Set a minimum ship speed.
               if (speed > 2.5) speed = 2.5;
               speed = ((double)((int)(speed * 4.0 + 0.5))) / 4.0;
               return speed; 

           }
       }
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
       
       public int Number_of_Engines
       {
           get
           {
               
               if (ShipHull.Properties.ContainsKey("Hull"))
               {
                   Hull hull = ShipHull.Properties["Hull"] as Hull;
                   foreach (HullModule module in hull.Modules)
                   {
                       if (module.AllocatedComponent != null && module.AllocatedComponent.Type == "Engine")
                       {
                           return module.ComponentCount;
                       }
                   }

               }
               return 0;
           }
       }

       // ============================================================================
       // Determine if this is a starbase hull
       // ============================================================================

       public bool IsStarbase
       {
           get 
           {
               if (ShipHull.Properties.ContainsKey("Hull"))
               {
                   Hull hull = ShipHull.Properties["Hull"] as Hull;
                   return hull.IsStarbase;
               }
               // It doesn't even have a Hull!
               Report.Error("ShipDesign.IsStarbase called on a design with no hull.");
               return false;
           }
       }

       // ============================================================================
       // Determine if this is a starbase that can infinite refuel
       // ============================================================================

       public bool CanRefuel
       {
           get 
           {
               if (ShipHull.Properties.ContainsKey("Hull"))
               {
                   Hull hull = ShipHull.Properties["Hull"] as Hull;
                   return hull.CanRefuel;
               }
               // It doesn't even have a Hull!
               Report.Error("ShipDesign.CanRefuel called on a design with no hull.");
               return false;
           }
       }


       // ============================================================================
       // The initiative of the ShipDesign, including computers but not weapon initiative.
       // ============================================================================
       public int Initiative
       {
           get
           {
               int initiative = 0;
               if (ShipHull.Properties.ContainsKey("Hull"))
               {
                   initiative += ((Hull)ShipHull.Properties["Hull"]).BattleInitiative;
               }
               if (Summary.Properties.ContainsKey("Computer"))
               {
                   initiative += ((Computer)Summary.Properties["Computer"]).Initiative;
               }
               return initiative;
           }
       }
   }



}

