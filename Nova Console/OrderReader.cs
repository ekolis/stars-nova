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
                ReadPlayerTurn(Race);
            }
        
        }
        // ============================================================================
        // Read in the player turns and update the relevant data stores with the
        // (possibly) new values. There is a special case where a player may decide not
        // to join the first turn so their will be no turn. So, check for this
        // condition and generate a sensible error report instead of just letting an
        // exception be thrown.
        // ============================================================================

        private static void ReadPlayerTurn(Race race)
        {
            string fileName = Path.Combine(StateData.GameFolder, race.Name + ".Orders");

            // Check for the special condition mentioned in the header.

            if (File.Exists(fileName) == false)
            {
                Report.FatalError("There is no turn file for the " + race.Name
                                  + " race.\n\nYou may only generate the first "
                                  + "turn of a game when all race turn files are "
                                  + "present.");
            }

            // Load from a binary serialised file.
            
            Orders playerOrders = new Orders();
            FileStream turnFile = new FileStream(fileName, FileMode.Open);
            playerOrders = Formatter.Deserialize(turnFile) as Orders;
            turnFile.Close();
            

            // Load from an xml file
            /*
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
            */

            // Regardless of how it was loaded:

            foreach (Design design in playerOrders.RaceDesigns)
            {
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

            foreach (Fleet fleet in playerOrders.RaceFleets)
            {
                StateData.AllFleets[fleet.Key] = fleet;
            }

            foreach (Star star in playerOrders.RaceStars)
            {
                StateData.AllStars[star.Name] = star;
            }

            StateData.AllRaceData[race.Name] = playerOrders.PlayerData;
            StateData.AllTechLevels[race.Name] = playerOrders.TechLevel;
        }

    }
}
