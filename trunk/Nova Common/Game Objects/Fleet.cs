// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Fleet class. A fleet is a container for one or more ships (which may be of
// different designs). Ship instances do not exist by themselves, they are
// always part of a fleet (even if they are the only ship in the fleet).
// A fleet may be a starbase.
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


// ============================================================================
// Fleet class.
// ============================================================================

   [Serializable]
   public class Fleet : Item
   {
      public  ArrayList  FleetShips     = new ArrayList();
      public  ArrayList  Waypoints      = new ArrayList();
      public  Cargo      Cargo          = new Cargo(); // FIXME - Cargo should be tracked by either the fleet or the ship, not both.
      public  Fleet      Target         = null;
      public  Star       InOrbit        = null;
      public  double     BattleSpeed    = 0; // used by a stack on the battle board
      public  double     Bearing        = 0;
      public  double     Cloaked        = 0;
      public  double     FuelAvailable  = 0;
      public  double     FuelCapacity   = 0;
      public  double     TargetDistance = 100;
      public  int        CargoCapacity  = 0;
      public  string     BattlePlan     = "Default";
      public  int        FleetID        = 0;
      
      public  enum       TravelStatus {Arrived, InTransit}

// ============================================================================
// Fleet construction for unit testing and stack creation during a battle.
// ============================================================================

      public Fleet(string n, string o, Point p) 
      {
         Name     = n;
         Owner    = o;
         Position = p;
      }


// ============================================================================
// Copy constructor. This is only used by the battle engine so only the fields
// used by it in creating stacks need to be copied. Note that we copy the
// ships as well. Be careful when using the copy. It is a different object.
// ============================================================================
      
      public Fleet(Fleet copy) : base(copy)
      {
         this.BattleSpeed = copy.BattleSpeed;
         this.BattlePlan  = copy.BattlePlan;

         foreach (Ship ship in copy.FleetShips) {
            this.FleetShips.Add(new Ship(ship));
         }
      }


// ============================================================================
// Fleet construction based on a ship and some parameters from a star (this is
// the usual case for most fleets when a new ship is manufactured at a star).
// ===========================================================================

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

         Waypoints.Add(w);

         // Inititialise the fleet elements that come from the star.

         Position     = star.Position;       
         InOrbit      = star;                
         Owner        = star.Owner;
      }

      // ============================================================================
      // Save: Return an XmlElement representation of the Fleet
      // ============================================================================
       
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelFleet = xmldoc.CreateElement("Fleet");

          Global.SaveData(xmldoc, xmlelFleet, "FleetID", this.FleetID.ToString());
          if (Target != null) Global.SaveData(xmldoc, xmlelFleet, "TargetID", Target.FleetID.ToString());
          else Global.SaveData(xmldoc, xmlelFleet, "TravelStatus", "InTransit");
          if (InOrbit != null) Global.SaveData(xmldoc, xmlelFleet, "InOrbit", InOrbit.Name);

          if (BattleSpeed != 0) NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "BattleSpeed", this.BattleSpeed.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "Bearing", this.Bearing.ToString());
          if (Cloaked != 0) NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "Cloaked", this.Cloaked.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "FuelAvailable", this.FuelAvailable.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "FuelCapacity", this.FuelCapacity.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "TargetDistance", this.TargetDistance.ToString());
          if (Cargo.Mass > 0) NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "CargoCapacity", this.CargoCapacity.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelFleet, "BattlePlan", this.BattlePlan);

          xmlelFleet.AppendChild(this.Cargo.ToXml(xmldoc));

          foreach (Waypoint waypoint in this.Waypoints)
          {
              xmlelFleet.AppendChild(waypoint.ToXml(xmldoc));
          }
          /* TODO
          foreach (Ship ship in this.FleetShips)
          {
              xmlelFleet.AppendChild(ship.ToXml(xmldoc));
          }
           * */

          return xmlelFleet;
      }
        

// ============================================================================
// Move the fleet towards the waypoint at the top of the list. Fuel is consumed
// at the rate of the sum of each of the individual ships (i.e. available fuel
// is automatically "pooled" between the ships).
// ===========================================================================

      public TravelStatus Move(ref double availableTime, Race race)
      {
         Waypoint target = Waypoints[0] as Waypoint;

         if (Position  == target.Position) return TravelStatus.Arrived;

         InOrbit = null;
      
         double legDistance = PointUtilities.Distance
                             (Position, target.Position);

         int    warpFactor = target.WarpFactor;
         int    speed      = warpFactor * warpFactor;
         double targetTime = legDistance / speed;
         double fuelTime   = FuelAvailable / FuelConsumption(warpFactor, race);
         double travelTime = targetTime;

         // Determine just how long we have available to travel towards the
         // waypoint target. This will be the smaller of target time (the ideal
         // case, we get there) available time (didn't get there but still can
         // move towards there next turn) and fuel time.

         TravelStatus arrived = TravelStatus.Arrived;

         if (travelTime > availableTime) {
            travelTime  = availableTime;
            arrived     = TravelStatus.InTransit;
         }
         
         if (travelTime > fuelTime) {
            travelTime  = fuelTime ;
            arrived     = TravelStatus.InTransit;
         }

         // If we have arrived then the new fleet position is the waypoint
         // target. Otherwise the position is determined by how far we got
         // in the time or fuel available.

         if (arrived == TravelStatus.Arrived) {
            Position          = target.Position;
            target.WarpFactor = 0;
         }
         else {
            double travelled  = speed * travelTime;
            Position          = PointUtilities.MoveTo
                               (Position, target.Position, travelled);
         }

         // Update the travel time left for this year and the total fuel we
         // now have available.

         availableTime -= travelTime;
         FuelAvailable -= FuelConsumption(target.WarpFactor, race) * travelTime;
      
         return arrived;
      }



// ============================================================================
// Return the fuel consumption (mg per year) of the fleet at the specified
// warp factor.
// ============================================================================

      public double FuelConsumption(int warpFactor, Race race)
      {
         double fuelConsumption = 0;

         foreach (Ship ship in FleetShips) {
            fuelConsumption += ship.FuelConsumption(warpFactor, race);
         }

         return fuelConsumption;
      }



// ============================================================================
// Return the long range scan capability of the fleet.
// ============================================================================

      public int LongRangeScan
      {
         get {
            int scanRange = 0;

            foreach (Ship ship in FleetShips) {
               if (ship.Design.NormalScan > scanRange) {
                   scanRange = ship.Design.NormalScan;
               }
            }
            return scanRange;
         }
      }


// ============================================================================
// Return the short range scan capability of the fleet.
// ============================================================================

      public int ShortRangeScan
      {
         get {
            int scanRange = 0;

            foreach (Ship ship in FleetShips) {
               if (ship.Design.NormalScan > scanRange) {
                   scanRange = ship.Design.NormalScan;
               }
            }
            return scanRange;
         }
      }


// ============================================================================
// Return the composition of a fleet (ship design and number of ships of that
// design).
// ============================================================================

      public Hashtable Composition 
      {
         get {
            Hashtable fleetComposition = new Hashtable();

            foreach (Ship ship in FleetShips) {
               fleetComposition[ship.Design.Name] = 0;
            }

            foreach (Ship ship in FleetShips) {
               int count = (int) fleetComposition[ship.Design.Name];
               
               count++;
               fleetComposition[ship.Design.Name] = count;
            }

            return fleetComposition;
         }
      }


// ============================================================================
// Return the mass of a fleet.
// ============================================================================

      public int TotalMass
      {
         get {
            double totalMass = 0;
            
            foreach (Ship ship in FleetShips) {
               totalMass += ship.TotalMass;
            }

            return (int) totalMass;
         }
      }


// ============================================================================
// Return the cost of a fleet.
// ============================================================================

      public Resources TotalCost
      {
         get {
            Resources cost = new Resources();
            
            foreach (Ship ship in FleetShips) {
               cost = cost + ship.Design.Cost;
            }

            return cost;
         }
      }


// ============================================================================
// Return the current speed of the fleet
// ============================================================================

      public int Speed
      {
         get {
            Waypoint target = Waypoints[0] as Waypoint;
            return target.WarpFactor;
         }

         set {
            Waypoint target   = Waypoints[0] as Waypoint;
            target.WarpFactor = 0;
         }
            
      }
            

// ============================================================================
// Report if a fleet is armed
// ============================================================================

      public bool IsArmed
      {
         get {
            foreach (Ship ship in FleetShips) {
               if (ship.HasWeapons) {
                  return true;
               }
            }
            return false;
         }
      }

// ============================================================================
// Return the current Defense capability of a fleet 
// ============================================================================

      public double Defenses
      {
         get {
            double totalDefenses = 0;

            foreach (Ship ship in FleetShips) {
               totalDefenses += ship.Armor;
               totalDefenses += ship.Shields;
            }
            return totalDefenses;
         }
      }


// ============================================================================
// Choose an image from one of the ships in the fleet
// ============================================================================

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


// ============================================================================
// Determine if the fleet has bombers
// ============================================================================

      public bool HasBombers
      {
         get {
            bool bombers = false;
            foreach (Ship ship in FleetShips) {
               if (ship.IsBomber) {
                  bombers = true;
               }
            }
            return bombers;
         }
      }


// ============================================================================
// Return the total normal bombing capability
// ============================================================================

      public Bomb BombCapability
      {
         get {
            Bomb totalBombs = new Bomb();
            foreach (Ship ship in FleetShips) {
               Bomb bomb = ship.BombCapability;
               totalBombs.PopKill        += bomb.PopKill;
               totalBombs.Installations += bomb.Installations;
               totalBombs.MinimumKill += bomb.MinimumKill;
            }
            return totalBombs;
         }
      }


// ============================================================================
// Return the the number of mines this fleet can lay.
// ============================================================================

      public int NumberOfMines
      {
         get {
            int mineCount = 0;

            foreach (Ship ship in FleetShips) {
               mineCount += ship.MineCount;
            }

            return mineCount;
         }
      }


// ============================================================================
// Return the total amour strength of the fleet
// ============================================================================

      public double ArmorStrength
      {
         get {
            double Armor = 0;

            foreach (Ship ship in FleetShips) {
               Armor += ship.Armor;
            }

            return Armor;
         }
      }

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




   }
}
