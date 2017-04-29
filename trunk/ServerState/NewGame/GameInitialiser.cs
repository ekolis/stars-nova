#region Copyright Notice
// ============================================================================
// Copyright (C) 2011-2012 The Stars-Nova Project
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
// ============================================================================
#endregion

namespace Nova.Server.NewGame
{
    using System.Collections.Generic;
    using System.IO;
    
    using Nova.Common;
    using Nova.Common.Components;
    
    /// <summary>
    /// Creates a game from scratch.
    /// </summary>
    public class Gameinitializer
    {
        private ServerData serverState;
        private StarMapinitializer starMapinitializer;
        private NameGenerator nameGenerator = new NameGenerator();
        
        public ServerData ServerState
        {
            get
            {
                return serverState;
            }
        }


        public static void Initialize(string gameFolderPath, List<PlayerSettings> players, Dictionary<string, Race> knownRaces)
        {
            Gameinitializer game = new Gameinitializer(gameFolderPath);
            game.GenerateEmpires(players, knownRaces);
            game.GenerateStarMap();
            game.GenerateAssets();
            game.GenerateIntel();
            game.ServerState.Save();
        } 

        
        private Gameinitializer(string gameFolderPath)
        {
            serverState = new ServerData();
            
            // store the updated Game Folder information
            using (Config conf = new Config())
            {
                conf[Global.ServerFolderKey] = gameFolderPath;
                
                serverState.GameFolder = gameFolderPath;
                // Don't set ClientFolderKey in case we want to simulate a LAN game 
                // on one PC for testing. 
                // Should be set when the ClientState is initialized. If that is by 
                // launching Nova GUI from the console then the GameFolder will be 
                // passed as the path to the .intel and the ClientFolderKey will then 
                // be updated.

                // Construct appropriate state and settings file names
                serverState.StatePathName = gameFolderPath + Path.DirectorySeparatorChar +
                                          GameSettings.Data.GameName + Global.ServerStateExtension;
                
                GameSettings.Data.SettingsPathName = gameFolderPath + Path.DirectorySeparatorChar +
                                                     GameSettings.Data.GameName + Global.SettingsExtension;
                
                conf[Global.ServerStateKey] = serverState.StatePathName;
            }
            
            starMapinitializer = new StarMapinitializer(serverState);
        }


        private void GenerateEmpires(List<PlayerSettings> players, Dictionary<string, Race> knownRaces)
        {
            // Copy the player & race data to the ServerState
            serverState.AllPlayers = players;

            // Assemble empires.
            foreach (PlayerSettings settings in serverState.AllPlayers)
            {
                if (serverState.AllRaces.ContainsKey(settings.RaceName))
                {
                    // Copy the race to a new key and change the setting to de-duplicate the race.
                    // This may mean a 2nd copy of the race in the data, but it's not much data and
                    // a copy of it doesn't really matter
                    Race cloneRace = serverState.AllRaces[settings.RaceName].Clone();
                    cloneRace.Name = nameGenerator.GetNextRaceName(settings.RaceName);
                    cloneRace.PluralName = cloneRace.Name + "s";
                    settings.RaceName = cloneRace.Name;

                    serverState.AllRaces.Add(cloneRace.Name, cloneRace);
                }
                else
                {
                    serverState.AllRaces.Add(settings.RaceName, knownRaces[settings.RaceName]);    
                }                

                // Initialize clean data for them. 
                EmpireData empireData = new EmpireData();
                empireData.Id = settings.PlayerNumber;
                empireData.Race = serverState.AllRaces[settings.RaceName];

                serverState.AllEmpires[empireData.Id] = empireData;

                // Add initial state to the intel files.
                ProcessPrimaryTraits(empireData);
                ProcessSecondaryTraits(empireData);
                
                // TODO: (priority 6) Set spent resources according to initial levels, instead of zero.
                
                // Load components!
                empireData.AvailableComponents = new RaceComponents(empireData.Race, empireData.ResearchLevels);
            }
            
            // Create initial relations.
            foreach (EmpireData wolf in serverState.AllEmpires.Values)
            {
                foreach (EmpireData lamb in serverState.AllEmpires.Values)
                {
                    if (wolf.Id != lamb.Id)
                    {
                        wolf.EmpireReports.Add(lamb.Id, new EmpireIntel(lamb));
                        wolf.EmpireReports[lamb.Id].Relation = PlayerRelation.Enemy;
                    }
                }

            }
        }


        private void GenerateStarMap()
        {
            starMapinitializer.GenerateStars();   
        }


        private void GenerateAssets()
        {
            starMapinitializer.GeneratePlayerAssets();   
        }


        private void GenerateIntel()
        {
            TurnGenerator firstTurn = new TurnGenerator(serverState);
                
            firstTurn.AssembleEmpireData();
                            
            Scores scores = new Scores(serverState);
            IntelWriter intelWriter = new IntelWriter(serverState, scores);
            intelWriter.WriteIntel();
        }
        
        
        /// <Summary>
        /// Process the Primary Traits for this race.
        /// </Summary>
        private void ProcessPrimaryTraits(EmpireData empire)
        {
            // TODO (priority 4) Special Components
            // Races are granted access to components currently based on tech level and primary/secondary traits (not tested).
            // Need to grant special access in a few circumstances
            // 1. JOAT Hulls with pen scans. (either make a different hull with a built in pen scan, of the same name and layout; or modify scanning and scan display functions)
            // 2. Mystery Trader Items - probably need to implement the idea of 'hidden' technology to cover this.

            // TODO (priority 4) Starting Tech
            // Need to specify starting tech levels. These must be checked by the server/console. Started below - Dan 26 Jan 10

            // TODO (priority 4) Implement Starting Items
   
            empire.ResearchLevels = new TechLevel(0);

            switch (empire.Race.Traits.Primary.Code)
            {
                case "HE":
                    // Start with one armed scout + 3 mini-colony ships
                    break;

                case "SS":
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] = 5;
                    // Start with one scout + one colony ship.
                    break;

                case "WM":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    // Start with one armed scout + one colony ship.
                    break;

                case "CA":
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 6;
                    // Start with an orbital terraforming ship
                    break;

                case "IS":
                    // Start with one scout and one colony ship
                    break;

                case "SD":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 2;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 2;
                    // Start with one scout, one colony ship, Two mine layers (one standard, one speed trap)
                    break;

                case "PP":
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 4;
                    // Two shielded scouts, one colony ship, two starting planets in a non-tiny universe
                    break;

                case "IT":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 5;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] = 5;
                    // one scout, one colony ship, one destroyer, one privateer, 2 planets with 100/250 stargates (in non-tiny universe)
                    break;

                case "AR":
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 1;

                    // starts with one scout, one orbital construction colony ship
                    break;

                case "JOAT":
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] = 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] = 3;
                    // two scouts, one colony ship, one medium freighter, one mini miner, one destroyer
                    break;

                default:
                    Report.Error("NewGameWizard.cs - ProcessPrimaryTraits() - Unknown Primary Trait \"" + empire.Race.Traits.Primary.Code + "\"");
                    break;
            } // switch on PRT
        }
        
        
        /// <Summary>
        /// Read the Secondary Traits for this race.
        /// </Summary>
        private void ProcessSecondaryTraits(EmpireData empire)
        {
            // TODO (priority 4) finish the rest of the LRTs.
            // Not all of these properties are fully implemented here, as they may require changes elsewhere in the game engine.
            // Where a trait is listed as 'TODO ??? (priority 4)' this means it first needs to be checked if it has been implemented elsewhere.
                        
            if (empire.Race.Traits.Contains("IFE"))
            {
                // Ships burn 15% less fuel : TODO ??? (priority 4)

                // Fuel Mizer and Galaxy Scoop engines available : Implemented in component definitions.

                // propulsion tech starts one level higher
                empire.ResearchLevels[TechLevel.ResearchField.Propulsion]++;
            }
            if (empire.Race.Traits.Contains("TT"))
            {
                // Begin the game able to adjust each environment attribute up to 3%
                // Higher levels of terraforming are available : implemented in component definitions.
                // Total Terraforming requires 30% fewer resources : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("ARM"))
            {
                // Grants access to three additional mining hulls and two new robots : implemented in component definitions.
                // Start the game with two midget miners : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("ISB"))
            {
                // Two additional starbase designs (space dock & ultra station) : implemented in component definitions.
                // Starbases have built in 20% cloacking : TODO ??? (priority 4)

                // Improved Starbases gives a 20% discount to starbase hulls.
                /*
                foreach (Component component in ClientState.Data.AvailableComponents.Values)
                {
                    // TODO (priority 3) - work out why it sometimes is null.
                    if (component == null || component.Type != "Hull") continue;
                    Hull hull = component.Properties["Hull"] as Hull;
                    if (hull == null || !hull.IsStarbase) continue;

                    Resources cost = component.Cost;
                    cost *= 0.8;
                }
                */
            }

            if (empire.Race.Traits.Contains("GR"))
            {
                // 50% resources go to selected research field. 15% to each other field. 115% total. TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("UR"))
            {
                // Affects minerals and resources returned due to scrapping. TODO ??? (priority 4).
            }
            if (empire.Race.Traits.Contains("MA"))
            {
                // One instance of mineral alchemy costs 25 resources instead of 100. TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("NRSE"))
            {
                // affects which engines are available : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("OBRM"))
            {
                // affects which mining robots will be available : implemented in component definitions.
            }
            if (empire.Race.Traits.Contains("CE"))
            {
                // Engines cost 50% less TODO (priority 4)
                // Engines have a 10% chance of not engaging above warp 6 : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("NAS"))
            {
                // No access to standard penetrating scanners : implemented in component definitions.
                // Ranges of conventional scanners are doubled : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("LSP"))
            {
                // Starting population is 17500 instead of 25000 : TODO ??? (priority 4)
            }
            if (empire.Race.Traits.Contains("BET"))
            {
                // TODO ??? (priority 4)
                // New technologies initially cost twice as much to build. 
                // Once all tech requirements are exceeded cost is normal. 
                // Miniaturization occurs at 5% per level up to 80% (instead of 4% per level up to 75%)
            }
            if (empire.Race.Traits.Contains("RS"))
            {
                // TODO ??? (priority 4)
                // All shields are 40% stronger than the listed rating.
                // Shields regenrate at 10% of max strength each round of combat.
                // All armors are 50% of their rated strength.
            }
            if (empire.Race.Traits.Contains("ExtraTech"))
            {
                // All extra technologies start on level 3 or 4 with JOAT
                if (empire.Race.Traits.Primary.Code == "JOAT")
                {
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] += 1;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] += 1;
                }
                else
                {
                    empire.ResearchLevels[TechLevel.ResearchField.Propulsion] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Construction] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Biotechnology] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Electronics] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Energy] += 3;
                    empire.ResearchLevels[TechLevel.ResearchField.Weapons] += 3;
                }
            }
        }
        
        
        private string AddKnownRace(Race race)
        {
            string name = race.Name;
            // De-dupe race names here. Not the ideal solution but for now it'll have to do.
            race.Name = nameGenerator.GetNextRaceName(name);

            if (race.Name != name)
            {
                race.PluralName = race.Name + "s";                
            }                            
            // KnownRaces[race.Name] = race;
            return race.Name;
        }
    }
}

