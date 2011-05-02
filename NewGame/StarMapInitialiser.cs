#region Copyright Notice
// ============================================================================
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
// This object contains static methods to initialise the star map. 
// Note that SarsMapGenerator handles positioning of the stars.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using Nova.Common;
using Nova.Common.Components;
using Nova.Server;
using System.Collections;

namespace Nova.NewGame
{
    /// <summary>
    /// This object contains static methods to initialise the star map. 
    /// Note that SarsMapGenerator handles positioning of the stars.
    /// </summary>
    public static class StarMapInitialiser
    {

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
        public static void GenerateStars()
        {
            NameGenerator nameGenerator = new NameGenerator();

            StarsMapGenerator map = new StarsMapGenerator(GameSettings.Data.MapWidth, GameSettings.Data.MapHeight, GameSettings.Data.StarSeparation, GameSettings.Data.StarDensity, GameSettings.Data.StarUniformity);
            List<int[]> allStarPositions = map.Generate();

            Random random = new Random(); // NB: do this outside the loop so that Random is seeded only once.
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

                ServerState.Data.AllStars[star.Name] = star;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialise the general game data for each player. E,g, picking a home
        /// planet, allocating initial resources, etc.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void InitialisePlayerData()
        {
            SpaceAllocator spaceAllocator = new SpaceAllocator(ServerState.Data.AllRaces.Count);

            // FIXME (priority 8) does not handle rectangular maps correctly
            spaceAllocator.AllocateSpace(Math.Min(GameSettings.Data.MapWidth, GameSettings.Data.MapHeight)); // attempt to not crash with a rectangular star map (slight improvement). Need to handle rectangular map properly.
            // spaceAllocator.AllocateSpace(GameSettings.Data.MapWidth); - old version

            foreach (Race race in ServerState.Data.AllRaces.Values)
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
                AllComponents.Restore();
                Hashtable components = AllComponents.Data.Components;
                Hull csHull, scoutHull, starbaseHull;
                Engine engine;
                foreach (string name in components.Keys)
                {
                    if (name == "Colony Ship")
                        csHull = (components["Colony Ship"] as Component).Properties["Hull"] as Hull;
                    else if (name == "Scout")
                        scoutHull = (components["Scout"] as Component).Properties["Hull"] as Hull;
                    else if (name == "Space Dock")
                        starbaseHull = (components["Space Dock"] as Component).Properties["Hull"] as Hull;
                    else if (name == "Quick Jump 5")
                        engine = (components["Quick Jump 5"] as Component).Properties["Engine"] as Engine;
                }

                ShipDesign cs = new ShipDesign();
                cs.Name = "Santa Maria";
                cs.Owner = player;
                

                ServerState.Data.AllDesigns[player + "/Mine"] = mine;
                ServerState.Data.AllDesigns[player + "/Factory"] = factory;
                ServerState.Data.AllDesigns[player + "/Defenses"] = defense;

                InitialiseHomeStar(race, spaceAllocator);
            }

            Nova.Common.Message welcome = new Nova.Common.Message();
            welcome.Text = "Your race is ready to explore the universe.";
            welcome.Audience = "*";

            ServerState.Data.AllMessages.Add(welcome);
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
        private static void InitialiseHomeStar(Race race, SpaceAllocator spaceAllocator)
        {
            ServerState stateData = ServerState.Data;

            Rectangle box = spaceAllocator.GetBox();
            foreach (Star star in stateData.AllStars.Values)
            {
                if (PointUtilities.InBox(star.Position, box))
                {
                    AllocateHomeStarResources(star, race);
                    AllocateHomeStarOrbitalInstallations(star, race);
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
        private static void AllocateHomeStarOrbitalInstallations(Star star, Race race)
        {
            
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Allocate an initial set of resources to a player's "home" star system. for
        /// each player giving it some colonists and initial resources. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private static void AllocateHomeStarResources(Star star, Race race)
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
