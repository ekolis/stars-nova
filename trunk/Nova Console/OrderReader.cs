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
            Orders inputTurn = new Orders();
            string fileName = Path.Combine(StateData.GameFolder, race.Name + ".Orders");

            // Check for the special condition mentioned in the header.

            if (File.Exists(fileName) == false)
            {
                Report.FatalError("There is no turn file for the " + race.Name
                                  + " race.\n\nYou may only generate the first "
                                  + "turn of a game when all race turn files are "
                                  + "present.");
            }

            FileStream turnFile = new FileStream(fileName, FileMode.Open);

            inputTurn = Formatter.Deserialize(turnFile) as Orders;
            turnFile.Close();

            foreach (Design design in inputTurn.RaceDesigns)
            {
                StateData.AllDesigns[design.Key] = design;
            }

            foreach (string fleetKey in inputTurn.DeletedFleets)
            {
                StateData.AllFleets.Remove(fleetKey);
            }

            foreach (string designKey in inputTurn.DeletedDesigns)
            {
                StateData.AllDesigns.Remove(designKey);
            }

            foreach (Fleet fleet in inputTurn.RaceFleets)
            {
                StateData.AllFleets[fleet.Key] = fleet;
            }

            foreach (Star star in inputTurn.RaceStars)
            {
                StateData.AllStars[star.Name] = star;
            }

            StateData.AllRaceData[race.Name] = inputTurn.PlayerData;
            StateData.AllTechLevels[race.Name] = inputTurn.TechLevel;
        }

    }
}
