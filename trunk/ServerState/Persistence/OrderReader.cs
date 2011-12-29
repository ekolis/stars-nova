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
        private readonly string gameFolder;
        private readonly int turnYear;
        private readonly Dictionary<int, EmpireData> allEmpires;
        private Dictionary<int, Stack<ICommand>> allCommands;
        
        public OrderReader(ServerState stateData)
        {
            gameFolder = stateData.GameFolder;            
            turnYear = stateData.TurnYear;
            allEmpires = stateData.AllEmpires;
            allCommands = stateData.AllCommands;
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
                        switch(subnode.Attributes["Type"].Value.ToString().ToLower())
                        {
                            case "research":
                                commands.Push(new ResearchCommand(subnode));
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
    }
}
