﻿#region Copyright Notice
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
using System.IO;
using System.IO.Compression;
using System.Xml;
using Nova.Common;
using Nova.Common.Components;
#endregion

namespace Nova.Server
{
    public class OrderReader
    {
        private readonly ServerState stateData;
        private ServerState turnData;
  
        public OrderReader(ServerState stateData)
        {
            this.stateData = stateData;    
        }
        
        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///  Read in all the race orders
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ServerState ReadOrders()
        {
            // We need a copy of the server state to operate on
            // since we want to return a tentative new state
            // which the server will then double check for errors
            // or cheats. Instead of cloning we can use the already
            // existing serialization techniques; serialize the state
            // and then deserialize into a new objetct.
            this.stateData.Save();
            this.turnData = this.stateData.Restore();
            
            this.turnData.AllTechLevels.Clear();

            foreach (Race race in this.turnData.AllRaces.Values)
            {
                // TODO (priority 4) only load those that are not yet turned in.
                this.ReadPlayerTurn(race);
            }
            
            return this.stateData;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Read in the player turns and update the relevant data stores with the
        /// (possibly) new values. 
        /// </summary>
        /// <param name="race">The race who's orders are to be loaded.</param>
        /// ----------------------------------------------------------------------------
        private void ReadPlayerTurn(Race race)
        {
            Orders playerOrders;
            try
            {
                string fileName = Path.Combine(this.stateData.GameFolder, race.Name + Global.OrdersExtension);

                if (!File.Exists(fileName))
                {
                    return;
                }

                // Load from an xml file
                XmlDocument xmldoc = new XmlDocument();
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
                    xmldoc.Load(compressionStream);

                    // check these orders are for the right turn
                    int ordersTurn = int.Parse(xmldoc.SelectSingleNode("ROOT/Orders/Turn").InnerText);
                    if (ordersTurn != this.stateData.TurnYear)
                    {
                        return;
                    }

                    playerOrders = new Orders(xmldoc.DocumentElement);
                }
            }
            catch (Exception e)
            {
                Report.Error(Environment.NewLine + "There was a problem reading in the orders for " + race.Name + Environment.NewLine + "Details: " + e.Message);
                return;
            }
            try
            {
                // Regardless of how it was loaded:
                this.LinkOrderReferences(playerOrders);

                // Load the information from the orders

                // TODO (priority 4) - check we are on the right turn before processing, and perhaps flag when we have processed the orders
                // TODO (priority 5) - fine tune so the client can't modify things like a star's position, i.e. treat the data as orders only.
                foreach (Design design in playerOrders.RaceDesigns.Values)
                {
                    this.stateData.AllDesigns[design.Key] = design;
                }

                foreach (string fleetKey in playerOrders.DeletedFleets)
                {
                    this.stateData.AllFleets.Remove(fleetKey);
                }

                foreach (string designKey in playerOrders.DeletedDesigns)
                {
                    this.stateData.AllDesigns.Remove(designKey);
                }

                foreach (Fleet fleet in playerOrders.RaceFleets.Values)
                {
                    this.stateData.AllFleets[fleet.Key] = fleet;
                }

                // load the orders for each star. 
                foreach (Star star in playerOrders.RaceStars)
                {
                    this.stateData.AllStars[star.Name] = star;
                }

                this.stateData.AllEmpires[race.Name] = playerOrders.EmpireStatus;
                this.stateData.AllTechLevels[race.Name] = playerOrders.TechLevel;
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
        private void LinkOrderReferences(Orders playerOrders)
        {
            // Fleet reference to Star
            foreach (Fleet fleet in playerOrders.RaceFleets.Values)
            {
                if (fleet.InOrbit != null)
                {
                    fleet.InOrbit = this.stateData.AllStars[fleet.InOrbit.Name];
                }
                // Ship reference to Design
                foreach (Ship ship in fleet.FleetShips)
                {
                    ship.DesignUpdate(playerOrders.RaceDesigns[ship.DesignName] as ShipDesign);
                }
            }
            // Star reference to Race
            // Star reference to Fleet (starbase)
            foreach (Star star in playerOrders.RaceStars)
            {
                if (star.ThisRace != null)
                {
                    star.ThisRace = this.stateData.AllRaces[star.ThisRace.Name];
                }
                if (star.Starbase != null)
                {
                    star.Starbase = playerOrders.RaceFleets[star.Starbase.FleetID];
                }
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
                        {
                            AllComponents.Data.Components.TryGetValue(module.AllocatedComponent.Name, out module.AllocatedComponent);
                        }
                    }
                }
            }
        }

        #endregion

    }
}
