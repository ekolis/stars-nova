// ============================================================================
// Nova. (c) 2010 Daniel Vale
//
// This module processes the reading of race orders.
//
// This is a static helper object that reads Orders and does processing on ConsoleState.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
using System;
using System.Xml;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using NovaCommon;

namespace NovaConsole
{
    public static class OrderReader
    {
        static ConsoleState StateData = ConsoleState.Data;
        private static BinaryFormatter Formatter = new BinaryFormatter();

        /// <summary>
        ///  Read in all the race orders
        /// </summary>
        public static void ReadOrders()
        {
            
            StateData.AllTechLevels.Clear();

            foreach (Race Race in StateData.AllRaces.Values)
            {
                // TODO (priority 3) only load those that are not yet turned in.
                ReadPlayerTurn(Race);
            }
        
        }
        // ============================================================================
        // Read in the player turns and update the relevant data stores with the
        // (possibly) new values. There is a special case where a player may decide not
        // to join the first turn so their will be no turn. So, check for this
        // condition and generate a sensible error report instead of just letting an
        // exception be thrown.
        // What if the player misses the deadline for the first turn in a multiplayer
        // game??? TODO (priority 3) make it possible to force the generation of the 
        // turn or explain why it is impossible. What is the behaviour of Stars! in
        // this case ???
        // ============================================================================

        private static void ReadPlayerTurn(Race race)
        {
            string fileName = Path.Combine(StateData.GameFolder, race.Name + ".Orders");

            // Check for the special condition mentioned in the header.

            if (File.Exists(fileName) == false)
            {
                return;
                // TODO (priority 5) check this only before generating a new year.
                Report.FatalError("There is no turn file for the " + race.Name
                                  + " race.\n\nYou may only generate the first "
                                  + "turn of a game when all race turn files are "
                                  + "present.");
            }

            // Load from a binary serialised file (older format, still in use).
            /*
            Orders playerOrders = new Orders();
            FileStream turnFile = new FileStream(fileName, FileMode.Open);
            playerOrders = Formatter.Deserialize(turnFile) as Orders;
            turnFile.Close();
            */

            // Load from an xml file
            
            XmlDocument xmldoc = new XmlDocument();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
#if (DEBUG)
            xmldoc.Load(fileName);  // uncompressed
#else
            xmldoc.Load(compressionStream); // compressed
#endif
            Orders playerOrders = new Orders(xmldoc.DocumentElement);
            fileStream.Close();
            

            // Regardless of how it was loaded:
            LinkOrderReferences(playerOrders);

            // TODO - check we are on the right turn before processing, and perhaps flag when we have processed the orders

            foreach (DictionaryEntry de in playerOrders.RaceDesigns)
            {
                Design design = de.Value as Design;
                StateData.AllDesigns[design.Key] = design;
            }

            foreach (string fleetKey in playerOrders.DeletedFleets)
            {
                StateData.AllFleets.Remove(fleetKey);
            }

            foreach (string designKey in playerOrders.DeletedDesigns)
            {
                StateData.AllDesigns.Remove(designKey);
            }

            foreach (DictionaryEntry de in playerOrders.RaceFleets)
            {
                Fleet fleet = de.Value as Fleet;
                StateData.AllFleets[fleet.Key] = fleet;
            }

            foreach (Star star in playerOrders.RaceStars)
            {
                StateData.AllStars[star.Name] = star;
            }

            StateData.AllRaceData[race.Name] = playerOrders.PlayerData;
            StateData.AllTechLevels[race.Name] = playerOrders.TechLevel;
        }

        /// <summary>
        /// When orders are loaded from file, objects may contain references to other objects.
        /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
        /// up these references once the file is fully loaded and all objects exist.
        /// In most cases a placeholder object has been created with the Name set from the file,
        /// and we need to find the actual reference using this Name.
        /// Objects can't do this themselves as they don't have access to the state data.
        /// </summary>
        private static void LinkOrderReferences(Orders playerOrders)
        {
            // Fleet reference to Star
            foreach (Fleet fleet in playerOrders.RaceFleets)
            {
                if (fleet.InOrbit != null)
                    fleet.InOrbit = StateData.AllStars[fleet.InOrbit.Name] as Star;
                // Ship reference to Design
                foreach (Ship ship in fleet.FleetShips)
                {
                    ship.Design = playerOrders.RaceDesigns[ship.Design.Name] as ShipDesign;
                }
            }
            // Star reference to Race
            // Star reference to Fleet (starbase)
            foreach (Star star in playerOrders.RaceStars)
            {
                if (star.Owner != null)
                    star.ThisRace = StateData.AllRaces[star.Owner] as Race;
                if (star.Starbase != null)
                    star.Starbase = playerOrders.RaceFleets[star.Starbase.Name] as Fleet;
            }

        }

    }
}
