#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 stars-nova
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

namespace Nova.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Components;
    using Nova.Common.Commands;

    /// <summary>
    /// This class processes the reading of race orders files.
    /// </summary>
    public class OrderReader
    {
#if USE_COMMAND_ORDERS
    
        private readonly string gameFolder;
        private readonly int turnYear;
        private readonly Dictionary<int, EmpireData> allEmpires;
        private Dictionary<int, Stack<ICommand>> allCommands;
#else
        // these are only needed for the old orders system
        private readonly ServerState stateData;
        private ServerState turnData;
#endif
        public OrderReader(ServerState stateData)
        {
#if USE_COMMAND_ORDERS
            gameFolder = stateData.GameFolder;            
            turnYear = stateData.TurnYear;
            allEmpires = stateData.AllEmpires;
            allCommands = stateData.AllCommands;
#else
            this.stateData = stateData;
#endif
        }

#if USE_COMMAND_ORDERS
        /// <summary>
        ///  Read in all the race orders.
        /// </summary>
        public void ReadOrders()
        {            
            foreach (EmpireData empire in allEmpires.Values)
            {                
                // TODO (priority 4) only load those that are not yet turned in.
                ReadPlayerTurn(empire);
            }
        }

        /// <summary>
        /// Read in the player turns and update the relevant data stores with the
        /// (possibly) new values. 
        /// </summary>
        /// <param name="race">The race who's orders are to be loaded.</param>
        private void ReadPlayerTurn(EmpireData empire)
        {
            Stack<ICommand> commands = new Stack<ICommand>();

            try
            {
                string fileName = Path.Combine(gameFolder, empire.Race.Name + Global.OrdersExtension);

                if (!File.Exists(fileName))
                {
                    return;
                }

                // Load from an xml file
                XmlDocument xmldoc = new XmlDocument();
                using (Stream disposeMe = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    Stream input = disposeMe;
                    // input = new GZipStream(input, CompressionMode.Decompress);
                    xmldoc.Load(input);

                    // check these orders are for the right turn
                    int ordersTurn = int.Parse(xmldoc.SelectSingleNode("ROOT/Turn").InnerText);
                    int empireId = int.Parse(xmldoc.SelectSingleNode("ROOT/Id").InnerText);

                    // Only read current orders.
                    if (ordersTurn != turnYear)
                    {
                        return;
                    }

                    // Prevent reading orders tagged for another empire
                    if (empireId != empire.Id)
                    {
                        return;
                    }

                    XmlNode subnode = xmldoc.SelectSingleNode("ROOT/Orders").FirstChild;

                    // Note that this reads the command stack in reverse with respect to the client's stack;
                    // server reads oldest commands first so that they are applied in the correct order.                    
                    while (subnode != null)
                    {
                        switch (subnode.Attributes["Type"].Value.ToString().ToLower())
                        {
                            case "research":
                                commands.Push(new ResearchCommand(subnode));
                                break;
                            case "waypoint":
                                commands.Push(new WaypointCommand(subnode));
                            break;
                        }
                        subnode = subnode.NextSibling;
                    }
                }
            }
            catch (Exception e)
            {
                Report.Error(Environment.NewLine + "There was a problem reading in the orders for " + empire.Race.Name + Environment.NewLine + "Details: " + e.Message);
                return;
            }

            allEmpires[empire.Id].LastTurnSubmitted = turnYear;
            allEmpires[empire.Id].TurnSubmitted = true;
            allCommands[empire.Id] = commands;
        }

#else
        /// <summary>
        ///  Read in all the race orders.
        /// </summary>
        public ServerState ReadOrders()
        {
            // We need a copy of the server state to operate on
            // since we want to return a tentative new state
            // which the server will then double check for errors
            // or cheats. Instead of cloning we can use the already
            // existing serialization techniques; serialize the state
            // and then deserialize into a new objetct.
            stateData.Save();
            turnData = new ServerState();
            turnData.StatePathName = stateData.StatePathName;
            turnData.Restore();
            
            turnData.AllTechLevels.Clear();

            foreach (EmpireData empire in turnData.AllEmpires.Values)
            {
                // TODO (priority 4) only load those that are not yet turned in.
                ReadPlayerTurn(empire);
            }
            
            return stateData;
        }

        /// <summary>
        /// Read in the player turns and update the relevant data stores with the
        /// (possibly) new values. 
        /// </summary>
        /// <param name="race">The race who's orders are to be loaded.</param>
        private void ReadPlayerTurn(EmpireData empire)
        {
            Orders playerOrders;
            try
            {
                string fileName = Path.Combine(this.stateData.GameFolder, empire.Race.Name + Global.OrdersExtension);

                if (!File.Exists(fileName))
                {
                    return;
                }

                // Load from an xml file
                XmlDocument xmldoc = new XmlDocument();
                using (Stream disposeme = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    Stream input = disposeme;
                    // input = new GZipStream(input, CompressionMode.Decompress);
                    xmldoc.Load(input);

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
                Report.Error(Environment.NewLine + "There was a problem reading in the orders for " + empire.Race.Name + Environment.NewLine + "Details: " + e.Message);
                return;
            }
                // Regardless of how it was loaded:
                this.LinkOrderReferences(playerOrders);

                // Load the information from the orders

                // TODO (priority 4) - check we are on the right turn before processing, and perhaps flag when we have processed the orders
                // TODO (priority 5) - fine tune so the client can't modify things like a star's position, i.e. treat the data as orders only.
                foreach (Design design in playerOrders.RaceDesigns.Values)
                {
                    this.stateData.AllDesigns[design.Key] = design;
                }

                foreach (int fleetKey in playerOrders.DeletedFleets)
                {
                    this.stateData.AllFleets.Remove(fleetKey);
                }

                foreach (int designId in playerOrders.DeletedDesigns)
                {
                    this.stateData.AllDesigns.Remove(designId);
                }

                foreach (Fleet fleet in playerOrders.EmpireStatus.OwnedFleets.Values)
                {
                    stateData.AllEmpires[empire.Id].OwnedFleets[fleet.Key] = fleet;
                    if (fleet.Owner == empire.Id)
                    {
                        stateData.AllFleets[fleet.Key] = fleet; // TODO (priority 6) - verify validity of orders.
                    }
                }

                // load the orders for each star. 
                foreach (Star star in playerOrders.EmpireStatus.OwnedStars.Values)
                {
                    stateData.AllEmpires[empire.Id].OwnedStars[star.Name] = star;
                    if (star.Owner == empire.Id)
                    {
                        stateData.AllStars[star.Name] = star;
                    }
                }

                this.stateData.AllEmpires[empire.Id] = playerOrders.EmpireStatus;
                this.stateData.AllTechLevels[empire.Id] = playerOrders.TechLevel;
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
        /// <param name="playerOrders">The <see cref="Orders"/> object to undergo post load linking.</param>
        private void LinkOrderReferences(Orders playerOrders)
        {
            // Fleet reference to Star
            foreach (Fleet fleet in playerOrders.EmpireStatus.OwnedFleets.Values)
            {
                if (fleet.InOrbit != null)
                {
                    fleet.InOrbit = stateData.AllStars[fleet.InOrbit.Name];
                }
                // prevent attempting to link enemy fleets to own designs (crash). TODO (priority 4) - decide if they should be linked to something else - Dan 10/7/11.
                if (fleet.Owner == playerOrders.EmpireStatus.Id)
                {
                    // Ship reference to Design
                    foreach (Ship ship in fleet.FleetShips)
                    {
                        ship.DesignUpdate(playerOrders.RaceDesigns[ship.DesignKey] as ShipDesign);
                    }
                }
            }
            // Star reference to Race
            // Star reference to Fleet (starbase)
            foreach (Star star in playerOrders.EmpireStatus.OwnedStars.Values)
            {
                if (star.ThisRace != null)
                {
                    star.ThisRace = stateData.AllRaces[star.ThisRace.Name];
                }
                if (star.Starbase != null)
                {
                    star.Starbase = playerOrders.EmpireStatus.OwnedFleets[star.Starbase.Key];
                }
            }

            // HullModule reference to a component
            foreach (Design design in playerOrders.RaceDesigns.Values)
            {
                if (design.Type == ItemType.Ship)
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
#endif

    }
}
