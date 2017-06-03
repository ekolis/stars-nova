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
    using Nova.Common.Commands;
    using Nova.Common.Components;

    /// <summary>
    /// This class processes the reading of race orders files.
    /// </summary>
    public class OrderReader
    {
        private readonly string gameFolder;
        private readonly int turnYear;
        private readonly Dictionary<int, EmpireData> allEmpires;
        private Dictionary<int, Stack<ICommand>> allCommands;
        public OrderReader(ServerData serverState)
        {
            gameFolder = serverState.GameFolder;            
            turnYear = serverState.TurnYear;
            allEmpires = serverState.AllEmpires;
            allCommands = serverState.AllCommands;
        }

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

            string fileName = Path.Combine(gameFolder, empire.Race.Name + Global.OrdersExtension);

            if (!File.Exists(fileName))
            {
                return;
            }

            // Load from an xml file
            XmlDocument xmldoc = new XmlDocument();
            bool waitForFile = false;
            double waitTime = 0; // seconds
            do
            {
                try
                {
                    using (Stream disposeMe = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        Stream input = disposeMe;
                        // input = new GZipStream(input, CompressionMode.Decompress);
                        xmldoc.Load(input);

                        // check these orders are for the right turn
                        int ordersTurn = int.Parse(xmldoc.SelectSingleNode("ROOT/Turn").InnerText, System.Globalization.CultureInfo.InvariantCulture);
                        int empireId = int.Parse(xmldoc.SelectSingleNode("ROOT/Id").InnerText, System.Globalization.CultureInfo.InvariantCulture);

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

                        // Note that this assembles the command stack reversed with respect to the client's stack;
                        // The file contains the newest commands first, and the oldest last. Thus the server's
                        // stack for the turn pops the oldest commands first, thus applying them in the correct order.                    
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

                                case "design":
                                    commands.Push(new DesignCommand(subnode));
                                    break;

                                case "production":
                                    commands.Push(new ProductionCommand(subnode));
                                    break;
                                    
                                case "renamefleet":
                                    commands.Push(new RenameFleetCommand(subnode));
                                    break;
            
                                default:
                                    Report.Error("The command \"" + subnode.Attributes["Type"].Value.ToString() + "\" was not recognised by the console.");
                                    Report.Debug("Unrecognised Command in OrderReader.cs ReadPlayerTurn().");
                                    break;
                            }
                            subnode = subnode.NextSibling;
                        }
                    }
                    waitForFile = false;
                }
                catch (System.IO.IOException)
                {
                    // IOException. Is the file locked? Try waiting.
                    if (waitTime < Global.TotalFileWaitTime)
                    {
                        waitForFile = true;
                        System.Threading.Thread.Sleep(Global.FileWaitRetryTime);
                        waitTime += 0.1;
                    }
                    else
                    {
                        // Give up, maybe something else is wrong?
                        throw;
                    }
                }
                catch (Exception e)
                {
                    Report.Error(Environment.NewLine + "There was a problem reading in the orders for " + empire.Race.Name + Environment.NewLine + "Details: " + e.Message);
                    return;
                }
            } 
            while (waitForFile);

            allEmpires[empire.Id].LastTurnSubmitted = turnYear;
            allEmpires[empire.Id].TurnSubmitted = true;
            allCommands[empire.Id] = commands;
        }
    }
}
