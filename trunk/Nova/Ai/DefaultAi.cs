#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// A simple AI harness.
// This runs as a command line application to have the computer play a nova
// turn.
//
// TODO (priority 5) - suggest placing wrappers around OrderWriter.WriteOrders and GuiState.Initialize 
//        incase these interfaces change when file formats are reworked.
//
// TODO (priority 5) - suggest refactor this to better seperate the AI itself from the program that 
//        interfaces with the console and data files.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;

using Nova.Common;
using Nova.Client;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.Ai
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string RaceName;
            int TurnNumber = -1;

            // ensure registry keys are initialised
            FileSearcher.SetKeys();
            
            // read paramaters for race and turn to play
            CommandArguments commandArguments = new CommandArguments(args);
            Console.WriteLine("Nova AI");
            if (commandArguments.Count < 2)
            {
                Console.WriteLine("Usage: Nova_AI <race_name> <turn_number>");
                return;
            }

            RaceName = commandArguments[CommandArguments.Option.RaceName];
            TurnNumber = int.Parse(commandArguments[CommandArguments.Option.Turn], System.Globalization.CultureInfo.InvariantCulture);

            // read in race data
            Console.WriteLine("Playing turn {0} for race \"{1}\".", TurnNumber, RaceName);
            try
            {
                // TODO (priority 6) - bypass password entry for AI.
                // Note: passwords have currently been disabled completely, awaiting a new more effective implementation - Dan 02 Mar 10
                ClientState.Initialize(commandArguments.ToArray()); 
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error reading its intel.");
                return;
            }

            // play turn
            // Currently just builds factories/mines/defenses
            try
            {
                Intel turnData = ClientState.Data.InputTurn;

                // currently does nothing: This is where the AI propper should do its work.
                foreach (Star star in ClientState.Data.PlayerStars.Values)
                {
                    star.ManufacturingQueue.Queue.Clear();
                    ProductionQueue.Item item = new ProductionQueue.Item();
                    Design design;

                    // build factories (limited by Germanium, and don't want to use it all)
                    if (star.ResourcesOnHand.Germanium > 50)
                    {
                        item.Name = "Factory";
                        item.Quantity = (int)((star.ResourcesOnHand.Germanium - 50) / 5);
                        item.Quantity = Math.Max(0, item.Quantity);

                        design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;

                        item.BuildState = design.Cost;

                        star.ManufacturingQueue.Queue.Add(item);

                    }

                    // build mines
                    item = new ProductionQueue.Item();
                    item.Name = "Mine";
                    item.Quantity = 100;
                    design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;
                    item.BuildState = design.Cost;
                    star.ManufacturingQueue.Queue.Add(item);

                    // build defenses
                    item = new ProductionQueue.Item();
                    item.Name = "Defenses";
                    item.Quantity = 100;
                    design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;
                    item.BuildState = design.Cost;
                    star.ManufacturingQueue.Queue.Add(item);
                }

            }
            catch (Exception)
            {
                Report.FatalError("AI failed to take proper actions.");
            }

            // save turn)
            try
            {
                OrderWriter.WriteOrders();
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error writing its orders.");
            }

            return;
        }
    }
}
