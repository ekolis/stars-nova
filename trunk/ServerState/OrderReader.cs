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
// This module processes the reading of race orders.
//
// This is a static helper object that reads Orders and does processing on ConsoleState.
// ===========================================================================
#endregion

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
using Nova.Common;
using Nova.Common.Components;

#endregion

namespace Nova.Server
{
    public static class OrderReader
    {
        static ServerState StateData = ServerState.Data;

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///  Read in all the race orders
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void ReadOrders()
        {
            
            StateData.AllTechLevels.Clear();

            foreach (Race Race in StateData.AllRaces.Values)
            {
                // TODO (priority 4) only load those that are not yet turned in.
                ReadPlayerTurn(Race);
            }
        
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Read in the player turns and update the relevant data stores with the
        /// (possibly) new values. 
        /// </summary>
        /// <param name="race">The race who's orders are to be loaded.</param>
        /// ----------------------------------------------------------------------------
        private static void ReadPlayerTurn(Race race)
        {
            Orders playerOrders;
            try
            {
                string fileName = Path.Combine(StateData.GameFolder, race.Name + Global.OrdersExtension);

                if (!File.Exists(fileName))
                {
                    return;
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
#if (DEBUG)
                xmldoc.Load(fileName);
                playerOrders = new Orders(xmldoc.DocumentElement);
#else
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
                xmldoc.Load(compressionStream);
                playerOrders = new Orders(xmldoc.DocumentElement);
                fileStream.Close();
#endif

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
                // TODO (priority 5) fine tune so the client can't modify things like a star's position, i.e. treat the data as orders only.
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
                Report.FatalError(Environment.NewLine + "There was a problem processing the orders for " + race.Name + Environment.NewLine + "Details: " + e.Message);
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When orders are loaded from file, objects may contain references to other objects.
        /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
        /// up these references once the file is fully loaded and all objects exist.
        /// In most cases a placeholder object has been created with the Name set from the file,
        /// and we need to find the actual reference using this Name.
        /// Objects can't do this themselves as they don't have access to the state data, 
        /// so we do it here.
        /// </summary>
        /// <param name="playerOrders">The <see cref="Orders"/> object to undergo post load linking.</param>
        /// ----------------------------------------------------------------------------
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
                        if (module.AllocatedComponent != null && module.AllocatedComponent.Name != null) 
                            module.AllocatedComponent = AllComponents.Data.Components[module.AllocatedComponent.Name] as Component;
                    }
                }
            }


        }

        #endregion

    }//OrderReader
}//namespace
