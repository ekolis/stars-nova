#region Copyright Notice
// ============================================================================
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

#region Module Description
// ===========================================================================
// This object contains static methods to initialise the star map. 
// Note that SarsMapGenerator handles positioning of the stars.
// ===========================================================================
#endregion

namespace Nova.NewGame
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Server;

    /// <summary>
    /// This object contains static methods to initialise the star map. 
    /// Note that SarsMapGenerator handles positioning of the stars.
    /// </summary>
    public class StarMapInitialiser
    {
        private ServerState stateData;
        private StarsMapGenerator map;
        private NameGenerator nameGenerator = new NameGenerator();
        
        public StarMapInitialiser(ServerState serverState)
        {
            this.stateData = serverState;
            this.map = new StarsMapGenerator(GameSettings.Data.MapWidth,
                                             GameSettings.Data.MapHeight,
                                             GameSettings.Data.StarSeparation,
                                             GameSettings.Data.StarDensity,
                                             GameSettings.Data.StarUniformity,
                                             serverState.AllPlayers.Count);
        }

        #region Initialisation

        /// <summary>
        /// Generate all the stars. We have two helper classes to assist in doing this:
        /// a name generator to allocate star names and a space generator to ensure
        /// stars have a reasonable separation. Beyond that, all we need to do is
        /// allocate some random mineral concentrations.
        /// </summary>
        /// <remarks>
        /// FIXME (priority 3) This method is public so that it can be called from the test fixture in NewGameTest.cs.
        /// That is the only method outside this object that should call this method.
        /// </remarks>
        public void GenerateStars()
        {
            map.Generate();

            Random random = new Random(); // NB: do this outside the loop so that random is seeded only once.
            foreach (int[] starPosition in map.Stars)
            {
                Star star = new Star();

                star.Position.X = starPosition[0];
                star.Position.Y = starPosition[1];

                star.Name = nameGenerator.NextName;

                star.MineralConcentration.Boranium = random.Next(1, 99);
                star.MineralConcentration.Ironium = random.Next(1, 99);
                star.MineralConcentration.Germanium = random.Next(1, 99);

                // The following values are percentages of the permissable range of
                // each environment parameter expressed as a percentage.
                star.Radiation = random.Next(1, 99);
                star.Gravity = random.Next(1, 99);
                star.Temperature = random.Next(1, 99);
                
                star.OriginalRadiation = star.Radiation;
                star.OriginalGravity = star.Gravity;
                star.OriginalTemperature = star.Temperature;

                stateData.AllStars[star.Name] = star;
            }
        }

        /// <summary>
        /// Initialise the general game data for each player. E,g, picking a home
        /// planet, allocating initial resources, etc.
        /// </summary>
        public void InitialisePlayerData()
        {
            foreach (EmpireData empire in stateData.AllEmpires.Values)
            {
                string player = empire.EmpireRace.Name;
                
                Design mine = new Design();
                Design factory = new Design();
                Design defense = new Design();

                mine.Cost = new Nova.Common.Resources(0, 0, 0, empire.EmpireRace.MineBuildCost);
                mine.Name = "Mine";
                mine.Type = "Mine";
                mine.Owner = empire.Id;

                // If we have the secondary racial trait Cheap Factories they need 1K
                // less germanium to build.
                int factoryBuildCostGerm = empire.EmpireRace.HasTrait("CF") ? 3 : 4;
                factory.Cost = new Nova.Common.Resources(0, 0, factoryBuildCostGerm, empire.EmpireRace.FactoryBuildCost);
                factory.Name = "Factory";
                factory.Type = "Factory";
                factory.Owner = empire.Id;

                defense.Cost = new Nova.Common.Resources(5, 5, 5, 15);
                defense.Name = "Defenses";
                defense.Type = "Defenses";
                defense.Owner = empire.Id;
                stateData.AllDesigns[player + "/Mine"] = mine;
                stateData.AllDesigns[player + "/Factory"] = factory;
                stateData.AllDesigns[player + "/Defenses"] = defense;
                PrepareDesigns(empire, player);
                InitialiseHomeStar(empire, player);
            }

            Nova.Common.Message welcome = new Nova.Common.Message();
            welcome.Text = "Your race is ready to explore the universe.";
            welcome.Audience = Global.AllEmpires;

            stateData.AllMessages.Add(welcome);
        }

        /// <summary>
        /// Initialise some starting designs.
        /// </summary>
        /// <param name="race">The <see cref="Race"/> of the player being initialised.</param>
        /// <param name="player">The player being initialised.</param>
        private void PrepareDesigns(EmpireData empire, string player)
        {
            // Read components data and create some basic stuff
            AllComponents.Restore();
            Dictionary<string, Component> components = AllComponents.Data.Components;
            Component colonyShipHull = null, scoutHull = null, starbaseHull = null;
            Component engine = null, colonyShipEngine = null;
            Component colonizer = null;
            Component scaner = components["Bat Scanner"];
            Component armor = components["Tritanium"];
            Component shield = components["Mole-skin Shield"];
            Component laser = components["Laser"];
            Component torpedo = components["Alpha Torpedo"];

            starbaseHull = components["Space Station"];
            engine = components["Quick Jump 5"];

            if (empire.EmpireRace.Traits.Primary.Code != "HE")
            {
                colonyShipHull = components["Colony Ship"]; // (components["Colony Ship"] as Component).Properties["Hull"] as Hull;
            }
            else
            {
                colonyShipEngine = components["Settler's Delight"];
                colonyShipHull = components["Mini-Colony Ship"];
            }
            scoutHull = components["Scout"]; // (components["Scout"] as Component).Properties["Hull"] as Hull;

            starbaseHull = components["Space Dock"]; // (components["Space Dock"] as Component).Properties["Hull"] as Hull;

            if (empire.EmpireRace.HasTrait("AR") == true)
            {
                colonizer = components["Orbital Construction Module"]; // (components["Orbital Construction Module"] as Component).Properties["Colonizer"] as Colonizer;
            }
            else
            {
                colonizer = components["Colonization Module"]; // (components["Colonization Module"] as Component).Properties["Colonizer"] as Colonizer; ;
            }


            if (colonyShipEngine == null)
            {
                colonyShipEngine = engine;
            }

            ShipDesign cs = new ShipDesign();
            cs.ShipHull = colonyShipHull;
            foreach (HullModule module in (cs.ShipHull.Properties["Hull"] as Hull).Modules)
            {
                if (module.ComponentType == "Engine")
                {
                    module.AllocatedComponent = colonyShipEngine;
                    module.ComponentCount = 1;
                }
                else if (module.ComponentType == "Mechanical")
                {
                    module.AllocatedComponent = colonizer;
                    module.ComponentCount = 1;
                }
            }
            cs.Icon = new ShipIcon(colonyShipHull.ImageFile, (Bitmap)colonyShipHull.ComponentImage);

            cs.Type = "Ship";
            cs.Name = "Santa Maria";
            cs.Owner = empire.Id;
            cs.Update();

            ShipDesign scout = new ShipDesign();
            scout.ShipHull = scoutHull;
            foreach (HullModule module in (scout.ShipHull.Properties["Hull"] as Hull).Modules)
            {
                if (module.ComponentType == "Engine")
                {
                    module.AllocatedComponent = engine;
                    module.ComponentCount = 1;
                }
                else if (module.ComponentType == "Scanner")
                {
                    module.AllocatedComponent = scaner;
                    module.ComponentCount = 1;
                }
            }
            scout.Icon = new ShipIcon(scoutHull.ImageFile, (Bitmap)scoutHull.ComponentImage);

            scout.Type = "Ship";
            scout.Name = "Scout";
            scout.Owner = empire.Id;
            scout.Update();

            ShipDesign starbase = new ShipDesign();
            starbase.Name = "Starbase";
            starbase.Owner = empire.Id;

            starbase.ShipHull = starbaseHull;
            starbase.Type = "Starbase";
            starbase.Icon = new ShipIcon(starbaseHull.ImageFile, (Bitmap)starbaseHull.ComponentImage);
            bool weaponSwitcher = false; // start with laser
            bool armorSwitcher = false; // start with armor
            foreach (HullModule module in (starbase.ShipHull.Properties["Hull"] as Hull).Modules)
            {
                if (module.ComponentType == "Weapon")
                {
                    if (weaponSwitcher == false)
                    {
                        module.AllocatedComponent = laser;
                    }
                    else
                    {
                        module.AllocatedComponent = torpedo;
                    }
                    weaponSwitcher = !weaponSwitcher;
                    module.ComponentCount = 8;
                }
                if (module.ComponentType == "Shield")
                {
                    module.AllocatedComponent = shield;
                    module.ComponentCount = 8;
                }
                if (module.ComponentType == "Shield or Armor")
                {
                    if (armorSwitcher == false)
                    {
                        module.AllocatedComponent = armor;
                    }
                    else
                    {
                        module.AllocatedComponent = shield;
                    }
                    module.ComponentCount = 8;
                    armorSwitcher = !armorSwitcher;
                }
            }

            starbase.Update();

            stateData.AllDesigns[player + "/" + starbase.Name] = starbase;
            stateData.AllDesigns[player + "/" + cs.Name] = cs;
            stateData.AllDesigns[player + "/" + scout.Name] = scout;
            /*
            switch (race.Traits.Primary.Code)
            {
                case "HE":
                    // Start with one armed scout + 3 mini-colony ships
                case "SS":
                    // Start with one scout + one colony ship.
                case "WM":
                    // Start with one armed scout + one colony ship.
                    break;

                case "CA":
                    // Start with an orbital terraforming ship
                    break;

                case "IS":
                    // Start with one scout and one colony ship
                    break;

                case "SD":
                    // Start with one scout, one colony ship, Two mine layers (one standard, one speed trap)
                    break;

                case "PP":
                    empireData.ResearchLevel[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    empireData.ResearchLevel[TechLevel.ResearchField.Propulsion] = 5;
                    empireData.ResearchLevel[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    empireData.ResearchLevel[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;
            */
        }

        /// <summary>
        /// Allocate a "home" star system for each player giving it some colonists and
        /// initial resources. We use the space allocater helper class to ensure that
        /// the home systems for each race are not too close together.
        /// </summary>
        /// <param name="race"><see cref="Race"/> to be positioned.</param>
        /// <param name="spaceAllocator">The <see cref="SpaceAllocator"/> being used to allocate positions.</param>
        private void InitialiseHomeStar(EmpireData empire, string player)
        {
            if (map.Homeworlds.Count > 0)
            {
                
                Random random = new Random();
                
                int[] starPosition = map.Homeworlds[random.Next(map.Homeworlds.Count)];   
                
                Star star = new Star();
                
                star.Position.X = starPosition[0];
                star.Position.Y = starPosition[1];
                
                map.Homeworlds.Remove(starPosition);

                star.Name = nameGenerator.NextName;

                AllocateHomeStarResources(star, empire);
                AllocateHomeStarOrbitalInstallations(star, empire, player);
                
                stateData.AllStars[star.Name] = star;                
                
                return;
            }
            else
            {
                Report.FatalError("Could not allocate home star");
            }
        }

        /// <summary>
        /// Allocate an initial set of resources to a player's "home" star system. for
        /// each player giving it some colonists and initial resources.         
        /// </summary>
        /// <param name="star"></param>
        /// <param name="race"></param>
        private void AllocateHomeStarOrbitalInstallations(Star star, EmpireData empire, string player)
        {
            ShipDesign colonyShipDesign = stateData.AllDesigns[player + "/Santa Maria"] as ShipDesign;
            if (empire.EmpireRace.Traits.Primary.Code != "HE")
            {
                Ship cs = new Ship(colonyShipDesign);
                Fleet fleet1 = new Fleet(cs, star);                               
                fleet1.Id = empire.NextFleetId;
                fleet1.Name = colonyShipDesign.Name + " #1";
                stateData.AllFleets[fleet1.Id] = fleet1;
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    Ship cs = new Ship(colonyShipDesign);
                    Fleet fleet = new Fleet(cs, star);                    
                    fleet.Id = empire.NextFleetId;
                    fleet.Name = String.Format("{0} #{1}", colonyShipDesign.Name, i);                    
                    stateData.AllFleets[fleet.Id] = fleet;
                }
            }

            ShipDesign scoutDesign = stateData.AllDesigns[player + "/Scout"] as ShipDesign;
            Ship scout = new Ship(scoutDesign);
            Fleet scoutFleet = new Fleet(scout, star);
            scoutFleet.Id = empire.NextFleetId;
            scoutFleet.Name = "Scout #1";       
            stateData.AllFleets[scoutFleet.Id] = scoutFleet;
 
            ShipDesign starbaseDesign = stateData.AllDesigns[player + "/Starbase"] as ShipDesign;
            Ship starbase = new Ship(starbaseDesign);
            Fleet starbaseFleet = new Fleet(starbase, star);            
            starbaseFleet.Id = empire.NextFleetId;
            starbaseFleet.Name = star.Name + " Starbase";
            stateData.AllFleets[starbaseFleet.Id] = starbaseFleet;
            star.Starbase = starbaseFleet;
        }

        /// <summary>
        /// Allocate an initial set of resources to a player's "home" star system. for
        /// each player giving it some colonists and initial resources. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void AllocateHomeStarResources(Star star, EmpireData empire)
        {
            Random random = new Random();

            // Set the owner of the home star in order to obtain proper
            // starting resources.
            star.Owner = empire.Id;
            star.ThisRace = empire.EmpireRace;

            // Set the habital values for this star to the optimum for each race.
            // This should result in a planet value of 100% for this race's home
            // world.            
            star.Radiation = empire.EmpireRace.RadiationTolerance.OptimumLevel;
            star.Temperature = empire.EmpireRace.TemperatureTolerance.OptimumLevel;
            star.Gravity = empire.EmpireRace.GravityTolerance.OptimumLevel;
            
            star.OriginalRadiation = star.Radiation;
            star.OriginalGravity = star.Gravity;
            star.OriginalTemperature = star.Temperature;
            
            star.Colonists = empire.EmpireRace.GetStartingPopulation();

            star.ResourcesOnHand.Boranium = random.Next(300, 500);
            star.ResourcesOnHand.Ironium = random.Next(300, 500);
            star.ResourcesOnHand.Germanium = random.Next(300, 500);
            star.Mines = 10;
            star.Factories = 10;
            star.ResourcesOnHand.Energy = star.GetResourceRate();

            star.MineralConcentration.Boranium = random.Next(50, 100);
            star.MineralConcentration.Ironium = random.Next(50, 100);
            star.MineralConcentration.Germanium = random.Next(50, 100);


            star.ScannerType = "Scoper 150"; // TODO (priority 4) get from component list
            star.DefenseType = "SDI"; // TODO (priority 4) get from component list
            star.ScanRange = 50; // TODO (priority 4) get from component list
        }

        #endregion
    }
}
