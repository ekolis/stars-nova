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

#region Using Statements
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
#endregion

namespace NovaServer
{
    public static class OrderReader
    {
        static ServerState StateData = ServerState.Data;

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


        /// <summary>
        /// Read in the player turns and update the relevant data stores with the
        /// (possibly) new values. 
        /// </summary>
        /// <param name="race">The race who's orders are to be loaded.</param>
        private static void ReadPlayerTurn(Race race)
        {
            Orders playerOrders;
            try
            {
                string fileName = Path.Combine(StateData.GameFolder, race.Name + ".Orders");

                if (File.Exists(fileName) == false)
                {
                    return;
                    // TODO (priority 5) move this to a warning when generating a new turn.
                    Report.Error("There is no turn file for the " + race.Name
                                      + " race.\n\nYou may only generate the first "
                                      + "turn of a game when all race turn files are "
                                      + "present.");
                }

                // Load from a binary serialised file (old format).
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
                playerOrders = new Orders(xmldoc.DocumentElement);
                fileStream.Close();

            }
            catch (Exception e)
            {
                Report.Error(Environment.NewLine + "There was a problem reading in the orders for " + race.Name + Environment.NewLine + "Details: " + e.Message);
                return;
            }
            try
            {

                // Regardless of how it was loaded:
                LinkOrderReferences(playerOrders);

                // TODO (priority 4) - check we are on the right turn before processing, and perhaps flag when we have processed the orders
                // TODO (priority 4) fine tune so the client can't modify things like a star's position, i.e. treat the data as orders only.
                foreach (Design design in playerOrders.RaceDesigns.Values)
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

                foreach (Fleet fleet in playerOrders.RaceFleets.Values)
                {
                    StateData.AllFleets[fleet.Key] = fleet;
                }

                // load the orders for each star. 
                foreach (Star star in playerOrders.RaceStars)
                {
                    StateData.AllStars[star.Name] = star;
                }

                StateData.AllRaceData[race.Name] = playerOrders.PlayerData;
                StateData.AllTechLevels[race.Name] = playerOrders.TechLevel;
            }
            catch (Exception e)
            {
                Report.Error(Environment.NewLine + "There was a problem processing the orders for " + race.Name + Environment.NewLine + "Details: " + e.Message);
            }
        }

        /// <summary>
        /// When orders are loaded from file, objects may contain references to other objects.
        /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
        /// up these references once the file is fully loaded and all objects exist.
        /// In most cases a placeholder object has been created with the Name set from the file,
        /// and we need to find the actual reference using this Name.
        /// Objects can't do this themselves as they don't have access to the state data, 
        /// so we do it here.
        /// </summary>
        private static void LinkOrderReferences(Orders playerOrders)
        {
            // Fleet reference to Star
            foreach (Fleet fleet in playerOrders.RaceFleets.Values)
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
                    star.Starbase = playerOrders.RaceFleets[star.Starbase.FleetID] as Fleet;
            }

            // HullModule reference to a component
            foreach (Design design in playerOrders.RaceDesigns.Values)
            {
                if (design.Type == "Ship")
                {
                    ShipDesign ship = design as ShipDesign;
                    foreach (HullModule module in ((Hull)ship.ShipHull.Properties["Hull"]).Modules)
                    {
                        module.AllocatedComponent = AllComponents.Data.Components[module.AllocatedComponent.Name] as Component;
                    }
                }
            }


        }

    }
}
