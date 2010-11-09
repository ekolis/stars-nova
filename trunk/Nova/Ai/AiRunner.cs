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

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.Ai
{
    public class Program
    {
        static AbstractAI AI = new DefaultAi();
        public static void Main(string[] args)
        {
            string raceName;
            int turnNumber = -1;

            // read paramaters for race and turn to play
            CommandArguments commandArguments;
            try
            {
                commandArguments = new CommandArguments(args);
                Console.WriteLine("Nova AI");
                if (commandArguments.Count < 3)
                {
                    Console.WriteLine("Usage: Nova --ai -r <race_name> -t <turn_number> -i <intel_file>");
                    return;
                }

                raceName = commandArguments[CommandArguments.Option.RaceName];
                turnNumber = int.Parse(commandArguments[CommandArguments.Option.Turn], System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Usage: Nova --ai -r <race_name> -t <turn_number> -i <intel_file>");
                return;
                
            }
            // read in race data
            Console.WriteLine("Playing turn {0} for race \"{1}\".", turnNumber, raceName);
            try
            {
                // TODO (priority 6) - bypass password entry for AI.
                // Note: passwords have currently been disabled completely, awaiting a new more effective implementation - Dan 02 Mar 10
               // ClientState.Initialize(commandArguments.ToArray()); 
                AI.Initialize(commandArguments);
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error reading its intel.");
                return;
            }

            // play turn
            AI.DoMove();

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
