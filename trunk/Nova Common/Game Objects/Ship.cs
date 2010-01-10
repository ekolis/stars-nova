// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Ship class. Note that ships never exist in isolation, they are always part
// of a fleet. Consequently, all the movement attributes can be found in the
// fleet class.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Xml;

namespace NovaCommon
{

// ============================================================================
// Ship class. Note that CargoCapacity is the amount of cargo the ship is
// actually carrying (this is usually only relevant when a ship is transferred
// to another fleet or is destroyed. CargoCapacity, the maximum cargo it can
// carry, is burried deep in the design but replicated here just
// for convenience of access.
// ============================================================================

   [Serializable]
   public class Ship : Item
   {
      public Cargo      Cargo         = new Cargo(); // Cargo being carried.
      public ShipDesign Design        = null;

       // These are the current shield / armor values, modified by damage.
      public double     Shields       = 0;
      public double     Armor         = 0;


// ============================================================================
// Create a ship of a specified design.
// ===========================================================================

      public Ship(ShipDesign shipDesign)
      {
         Design = shipDesign;

         Shields       = shipDesign.Shield;
         Armor         = shipDesign.Armor;
         Cost          = shipDesign.Cost;
        
      }


// ============================================================================
// Copy constructor. This is only used by the battle engine so only the fields
// used by it in creating stacks need to be copied.  Be careful when using the
// copy. It is a different object to the original.
// ============================================================================

      public Ship(Ship copy) : base (copy)
      {
         this.Design  = copy.Design;
         this.Shields = copy.Shields;
         this.Armor  = copy.Armor;
         this.Cargo   = new Cargo(copy.Cargo);
      }

      /// <summary>
      /// Initialising Constructor from an xml node.
      /// Precondition: node is a "Ship" node Nova save file (xml document).
      /// </summary>
      /// <param name="node">The XmlNode of the parent.</param>
      public Ship(XmlNode node)
      {
          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {

                  switch (subnode.Name.ToLower())
                  {
                      case "design":
                          // FIXME - need to reference the actual design, this is just a placeholder for the name loaded from the file.
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
              }
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
          }
      }


// ============================================================================
// Return the ship fuel consumption (mg per year) at a specified warp factor.
//
// Ship_fuel_usage = ship_mass x efficiency x distance / 200
//
// As distance = speed * time, and we are setting time to 1 year, then we can
// just drop speed into the above equation and end up with mg per year. 
//
// If the secondary racial trait "improved fuel efficiency" is set then
// fuel consumption is 15% less than advertised.
// ============================================================================

      public double FuelConsumption(int warp, Race race)
      {
         if (warp == 0) return 0;

         double fuelFactor      = Design.Engine.FuelConsumption[warp - 1];
         double efficiency      = fuelFactor / 100.0;
         double speed           = warp * warp;

         double fuelConsumption = (TotalMass * efficiency * speed) / 200.0; 

         if (race.HasTrait("IFE")) 
         {
            fuelConsumption *= 0.85;
         }

         return fuelConsumption;
      }


// ============================================================================
// Return the mass of the ship design plus the mass of any cargo.
// ============================================================================

      public double TotalMass {
         get { return (double) (Design.Mass + Cargo.Mass); }
      }


// ============================================================================
      // Return the power rating of this ship (stub - TODO) 
// ============================================================================

      public int PowerRating {
         get { return 0; }
      }


// ============================================================================
// Report if this ship has weapons.
// ============================================================================

      public bool HasWeapons
      {
         get {
            if (Design.Weapons == null) {
               return false;
            }
            return true;
         }
      }


// ============================================================================
// Report if the ship is a bomber
// ============================================================================

      public bool IsBomber
      {
         get {
            if (Design.ConventionalBombs.PopKill == 0 && Design.SmartBombs.PopKill == 0) 
            {
               return false;
            }
            return true;
         }
      }


// ============================================================================
// Return total  bomb capability. 
// TODO (low priority) Whatever code uses this seems to be ignoring smart bombs???
// ============================================================================

      public Bomb BombCapability
      {
         get 
         {
            return Design.ConventionalBombs;
         }
      }


// ============================================================================
// Return total mine laying capacity for this ship
// TODO (low priority) Client code must handle heavy and speed trap mines too.
// ============================================================================

      public int MineCount
      {
         get {
            return Design.StandardMines.LayerRate;
         }
      }

       /// <summary>
       /// Generate an xml representation of the ship for saving
       /// </summary>
       /// <param name="xmldoc">The master XmlDocument</param>
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelShip = xmldoc.CreateElement("Ship");

          NovaCommon.Global.SaveData(xmldoc, xmlelShip, "Design", this.Design.Name);
          NovaCommon.Global.SaveData(xmldoc, xmlelShip, "Owner", this.Owner);
          NovaCommon.Global.SaveData(xmldoc, xmlelShip, "Mass", this.Mass.ToString());
          xmlelShip.AppendChild(Cost.ToXml(xmldoc));

          NovaCommon.Global.SaveData(xmldoc, xmlelShip, "Shields", this.Shields.ToString());
          NovaCommon.Global.SaveData(xmldoc, xmlelShip, "Armor", this.Armor.ToString());
          if (Cargo.Mass > 0) xmlelShip.AppendChild(Cargo.ToXml(xmldoc));

          /* These fields inherited from Item are ignored.
           * public  string    Type     = null;
           * public  Point     Position = new Point(0, 0);
           */

          return xmlelShip;
      }
   }
}
