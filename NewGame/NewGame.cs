using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using NovaCommon;
using NovaServer;

namespace NewGame
{
    static class NewGame
    {
        static Random random = new Random();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Find files we may need
            FileSearcher.SetKeys();

            // Establish the victory conditions:

            NewGameWizard newGameWizard = new NewGameWizard();

            do
            {
                DialogResult result = newGameWizard.ShowDialog();

                // Check for cancel being pressed.
                if (result != DialogResult.OK)
                {
                    // return to the game launcher
                    String NovaLauncherApp;
                    NovaLauncherApp = FileSearcher.GetFile(Global.NovaLauncherKey, false, Global.NovaLauncherPath_Development, Global.NovaLauncherPath_Deployed, "NovaLauncher.exe", true);
                    try
                    {
                        Process.Start(NovaLauncherApp);
                    }
                    catch
                    {
                        // there was a problem returning to the launcher and the user doesn't want the new game dialog open. Just quit.
                        Report.FatalError("Could not return to the Nova Launcher.");
                    }
                    Application.Exit();
                    return; // need this as well to prevent execution of the rest of the loop, which will restart the NewGame dialog.
                }

                // New game dialog was OK, create a game
                SaveFileDialog gameNameDialog = new SaveFileDialog();
                gameNameDialog.Title = "Choose New Game Name";
                gameNameDialog.FileName = "NovaServer.state"; // give it a default name
                DialogResult gameNameDialogResult = gameNameDialog.ShowDialog();

                // Check for cancel being pressed (in the new game save file dialog).
                if (gameNameDialogResult != DialogResult.OK)
                {
                    // return to the new game wizard
                    continue;
                }

                ServerState.Data.StatePathName = gameNameDialog.FileName;
                FileSearcher.SetNovaRegistryValue(Global.ServerStateKey, gameNameDialog.FileName);
                System.IO.FileInfo newGameFile = new System.IO.FileInfo(gameNameDialog.FileName);
                ServerState.Data.GameFolder = newGameFile.DirectoryName;

                //ServerState.Data.AllRaces 
                ServerState.Data.AllPlayers = newGameWizard.Players;
                foreach (PlayerSettings settings in ServerState.Data.AllPlayers)
                {
                    // TODO (priority 4) - need to decide how to handle two races of the same name. If they are the same, fine. If they are different, problem! Maybe the race name is a poor key???
                    if ( ! ServerState.Data.AllRaces.Contains(settings.RaceName))
                        ServerState.Data.AllRaces.Add(settings.RaceName, newGameWizard.KnownRaces[settings.RaceName]);
                }


                GenerateStars();
                InitialisePlayerData();
                try
                {
                    IntelWriter.WriteIntel();
                    ServerState.Save();
                }
                catch
                {
                    Report.Error("Creation of new game failed.");
                    continue;
                }
                // start the server
                String NovaConsole = FileSearcher.GetFile(Global.NovaConsoleKey, false, Global.NovaConsolePath_Development, Global.NovaConsolePath_Deployed, "Nova Console.exe", false);
                try
                {
                    Process.Start(NovaConsole);
                    Application.Exit();
                    return;
                }
                catch
                {
                    Report.FatalError("Unable to launch \"Nova Console.exe\".");
                }
            } while (true);

        }

        /// <summary>
        /// Generate all the stars. We have two helper classes to assist in doing this:
        /// a name generator to allocate star names and a space generator to ensure
        /// stars have a reasonable separation. Beyond that, all we need to do is
        /// allocate some random mineral concentrations.
        /// </summary>
        private static void GenerateStars()
        {
            NameGenerator nameGenerator = new NameGenerator();
            int numberOfStars = GameSettings.Data.NumberOfStars;
            SpaceAllocator spaceAllocator = new SpaceAllocator(numberOfStars);

            // FIXME (priority 4) - ignores map Height
            spaceAllocator.AllocateSpace(GameSettings.Data.MapWidth);

            for (int count = 0; count < numberOfStars; count++)
            {
                Star star = new Star();

                Rectangle area = spaceAllocator.GetBox();
                star.Position = PointUtilities.GetPositionInBox(area, 10);

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


// ===========================================================================
// Initialise the general game data for each player. E,g, picking a home
// planet, allocating initial resources, etc.
// ===========================================================================

      private static void InitialisePlayerData()
      {
         SpaceAllocator spaceAllocator = new SpaceAllocator
                        (ServerState.Data.AllRaces.Count);

          // FIXME (priority 4) ignores map height
         spaceAllocator.AllocateSpace(GameSettings.Data.MapWidth);

         foreach (Race race in ServerState.Data.AllRaces.Values) {
            string player  = race.Name;

            Design mine    = new Design();
            Design factory = new Design();
            Design Defense = new Design();

            mine.Cost      = new NovaCommon.Resources(0, 0, 0, race.MineBuildCost);
            mine.Name      = "Mine";
            mine.Type      = "Mine";
            mine.Owner     = player;

            // If we have the secondary racial trait Cheap Factories they need 1K
            // less germanium to build.
            int factoryBuildCostGerm = (race.HasTrait("CF") ? 3 : 4);
            factory.Cost   = new NovaCommon.Resources(0, 0, factoryBuildCostGerm, race.FactoryBuildCost);
            factory.Name   = "Factory";
            factory.Type   = "Factory";
            factory.Owner  = player;

            Defense.Cost   = new NovaCommon.Resources(5, 5, 5, 15);
            Defense.Name   = "Defenses";
            Defense.Type   = "Defenses";
            Defense.Owner  = player;

            ServerState.Data.AllDesigns[player + "/Mine"]     = mine;
            ServerState.Data.AllDesigns[player + "/Factory"]  = factory; 
            ServerState.Data.AllDesigns[player + "/Defenses"] = Defense;

            InitialiseHomeStar(race, spaceAllocator);
         }
         
         NovaCommon.Message welcome = new NovaCommon.Message();
         welcome.Text     = "Your race is ready to explore the universe.";
         welcome.Audience = "*";

         Intel.Data.Messages.Add(welcome);
         Intel.Data.TurnYear = 2100;
      }


// ============================================================================
// Allocate a "home" star system for each player giving it some colonists and
// initial resources. We use the space allocater helper class to ensure that
// the home systems for each race are not too close together.
// ============================================================================

      private static void InitialiseHomeStar(Race           race, 
                                             SpaceAllocator spaceAllocator)
      {
         ServerState stateData = ServerState.Data;

         Rectangle box = spaceAllocator.GetBox(); 
         foreach (Star star in stateData.AllStars.Values) {
            if (PointUtilities.InBox(star.Position, box)) {
               AllocateHomeStarResources(star, race);
               return;
            }
         }

         Report.FatalError("Could not allocate home star");
      }


// ============================================================================
// Allocate an initial set of resources to a player's "home" star system. for
// each player giving it some colonists and initial resources. 
// ============================================================================

      private static void AllocateHomeStarResources(Star star, Race race)
      {
         star.ResourcesOnHand.Boranium       = random.Next(300, 500);
         star.ResourcesOnHand.Ironium        = random.Next(300, 500);
         star.ResourcesOnHand.Germanium      = random.Next(300, 500);
         star.ResourcesOnHand.Energy         = 25;

         star.MineralConcentration.Boranium  = random.Next(50, 100);
         star.MineralConcentration.Ironium   = random.Next(50, 100);
         star.MineralConcentration.Germanium = random.Next(50, 100);

         star.Owner                          = race.Name;
         star.ScannerType                    = "Scoper 150"; // TODO (priority 4) get from component list
         star.DefenseType                    = "SDI"; // TODO (priority 4) get from component list
         star.ScanRange                      = 50; // TODO (priority 4) get from component list
         star.Mines                          = 10;
         star.Factories                      = 10;

         // Set the habital values for this star to the optimum for each race.
         // This should result in a planet value of 100% for this race's home
         // world.

         star.Radiation   = race.OptimumRadiationLevel;
         star.Temperature = race.OptimumTemperatureLevel;
         star.Gravity     = race.OptimumGravityLevel;

         if (race.HasTrait("LSP"))
         {
            star.Colonists = 17500;
         }
         else {
            star.Colonists = 25000;
         }
      }


    }
}
