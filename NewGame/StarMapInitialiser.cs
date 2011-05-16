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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using Nova.Common;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.NewGame
{
    /// <summary>
    /// This object contains static methods to initialise the star map. 
    /// Note that SarsMapGenerator handles positioning of the stars.
    /// </summary>
    public class StarMapInitialiser
    {
        private ServerState stateData;
        
        public StarMapInitialiser(ServerState serverState)
        {
            this.stateData = serverState;
        }

        #region Initialisation

        /// ----------------------------------------------------------------------------
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
        /// ----------------------------------------------------------------------------
        public void GenerateStars()
        {
            NameGenerator nameGenerator = new NameGenerator();

            StarsMapGenerator map = new StarsMapGenerator(GameSettings.Data.MapWidth, GameSettings.Data.MapHeight, GameSettings.Data.StarSeparation, GameSettings.Data.StarDensity, GameSettings.Data.StarUniformity);
            List<int[]> allStarPositions = map.Generate();

            Random random = new Random(); // NB: do this outside the loop so that random is seeded only once.
            foreach (int[] starPosition in allStarPositions)
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

                stateData.AllStars[star.Name] = star;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialise the general game data for each player. E,g, picking a home
        /// planet, allocating initial resources, etc.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void InitialisePlayerData()
        {
            SpaceAllocator spaceAllocator = new SpaceAllocator(stateData.AllRaces.Count);

            // FIXME (priority 8) does not handle rectangular maps correctly
            spaceAllocator.AllocateSpace(Math.Min(GameSettings.Data.MapWidth, GameSettings.Data.MapHeight)); // attempt to not crash with a rectangular star map (slight improvement). Need to handle rectangular map properly.
            // spaceAllocator.AllocateSpace(GameSettings.Data.MapWidth); - old version

            foreach (Race race in stateData.AllRaces.Values)
            {
                string player = race.Name;

                Design mine = new Design();
                Design factory = new Design();
                Design defense = new Design();

                mine.Cost = new Nova.Common.Resources(0, 0, 0, race.MineBuildCost);
                mine.Name = "Mine";
                mine.Type = "Mine";
                mine.Owner = player;

                // If we have the secondary racial trait Cheap Factories they need 1K
                // less germanium to build.
                int factoryBuildCostGerm = race.HasTrait("CF") ? 3 : 4;
                factory.Cost = new Nova.Common.Resources(0, 0, factoryBuildCostGerm, race.FactoryBuildCost);
                factory.Name = "Factory";
                factory.Type = "Factory";
                factory.Owner = player;

                defense.Cost = new Nova.Common.Resources(5, 5, 5, 15);
                defense.Name = "Defenses";
                defense.Type = "Defenses";
                defense.Owner = player;
                stateData.AllDesigns[player + "/Mine"] = mine;
                stateData.AllDesigns[player + "/Factory"] = factory;
                stateData.AllDesigns[player + "/Defenses"] = defense;
                PrepareDesigns(race, player);
                InitialiseHomeStar(race, spaceAllocator, player);
            }

            Nova.Common.Message welcome = new Nova.Common.Message();
            welcome.Text = "Your race is ready to explore the universe.";
            welcome.Audience = "*";

            stateData.AllMessages.Add(welcome);
        }

        private void PrepareDesigns(Race race, string player)
        {
            // Read components data and create some basic stuff
            AllComponents.Restore();
            Hashtable components = AllComponents.Data.Components;
            Component colonyShipHull = null, scoutHull = null, starbaseHull = null;
            Component engine = null, colonyShipEngine = null;
            Component colonizer = null;

            engine = components["Quick Jump 5"] as Component;
            if (race.Traits.Primary.Code != "HE")
            {
                colonyShipHull = components["Colony Ship"] as Component; // (components["Colony Ship"] as Component).Properties["Hull"] as Hull;
            }
            else
            {
                colonyShipEngine = components["Settler's Delight"] as Component;
                colonyShipHull = components["Mini-Colony Ship"] as Component;
            }
            scoutHull = components["Scout"] as Component; // (components["Scout"] as Component).Properties["Hull"] as Hull;

            starbaseHull = components["Space Dock"] as Component; // (components["Space Dock"] as Component).Properties["Hull"] as Hull;

            if (race.HasTrait("AR") == true)
            {
                colonizer = components["Orbital Construction Module"] as Component; // (components["Orbital Construction Module"] as Component).Properties["Colonizer"] as Colonizer;
            }
            else
            {
                colonizer = components["Colonization Module"] as Component; // (components["Colonization Module"] as Component).Properties["Colonizer"] as Colonizer; ;
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
                    module.AllocatedComponent = engine;
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
            cs.Owner = player;

            stateData.AllDesigns[player + "/cs"] = cs;
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
                    raceData.ResearchLevel[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    raceData.ResearchLevel[TechLevel.ResearchField.Propulsion] = 5;
                    raceData.ResearchLevel[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    raceData.ResearchLevel[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;
            */

        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Allocate a "home" star system for each player giving it some colonists and
        /// initial resources. We use the space allocater helper class to ensure that
        /// the home systems for each race are not too close together.
        /// </summary>
        /// <param name="race"><see cref="Race"/> to be positioned.</param>
        /// <param name="spaceAllocator">The <see cref="SpaceAllocator"/> being used to allocate positions.</param>
        /// ----------------------------------------------------------------------------
        private void InitialiseHomeStar(Race race, SpaceAllocator spaceAllocator, string player)
        {
            Rectangle box = spaceAllocator.GetBox();
            foreach (Star star in stateData.AllStars.Values)
            {
                if (PointUtilities.InBox(star.Position, box))
                {
                    AllocateHomeStarResources(star, race);
                    AllocateHomeStarOrbitalInstallations(star, race, player);
                    return;
                }
            }

            Report.FatalError("Could not allocate home star");
        }

        /// <summary>
        /// Allocate an initial set of resources to a player's "home" star system. for
        /// each player giving it some colonists and initial resources.         
        /// </summary>
        /// <param name="star"></param>
        /// <param name="race"></param>
        private void AllocateHomeStarOrbitalInstallations(Star star, Race race, string player)
        {
            ShipDesign colonyShipDesign = stateData.AllDesigns[player + "/cs"] as ShipDesign;
            if (race.Traits.Primary.Code != "HE")
            {
                Ship cs = new Ship(colonyShipDesign);
                Fleet fleet1 = new Fleet(cs, star);
                fleet1.Name = "CSFleet1";
                stateData.AllFleets[player + "/" + fleet1.FleetID.ToString()] = fleet1;
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    Ship cs = new Ship(colonyShipDesign);
                    Fleet fleet = new Fleet(cs, star);
                    fleet.FleetID = i;
                    fleet.Name = "CSFleet" + i.ToString();
                    stateData.AllFleets[player + "/" + fleet.FleetID.ToString()] = fleet;
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Allocate an initial set of resources to a player's "home" star system. for
        /// each player giving it some colonists and initial resources. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AllocateHomeStarResources(Star star, Race race)
        {
            Random random = new Random();

            // Set the owner of the home star in order to obtain proper
            // starting resources.
            star.Owner = race.Name;
            star.ThisRace = race;

            // Set the habital values for this star to the optimum for each race.
            // This should result in a planet value of 100% for this race's home
            // world.            
            star.Radiation = race.RadiationTolerance.OptimumLevel;
            star.Temperature = race.TemperatureTolerance.OptimumLevel;
            star.Gravity = race.GravityTolerance.OptimumLevel;
            star.Colonists = race.GetStartingPopulation();

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
