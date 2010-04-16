// ============================================================================
// Nova. (c) 2009, 2010, stars-nova
//
// A simple AI harness.
// This runs as a command line application to have the computer play a nova
// turn.
//
// TODO (priority 3) - suggest placing wrappers around OrderWriter.WriteOrders and GuiState.Initialize 
//        incase these interfaces change when file formats are reworked.
//
// TODO (priority 3) - suggest refactor this to better seperate the AI itself from the program that 
//        interfaces with the console and data files.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;

using NovaCommon;
using NovaClient;

namespace Nova_AI
{
    class Program
    {
        static void Main(string[] args)
        {
            String RaceName = null;
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
                // TODO (priority 4) - bypass password entry for AI.
                // Note: passwords have currently been disabled completely, awaiting a new more effective implementation - Dan 02 Mar 10
                ClientState.Initialize(commandArguments.ToArray()); 
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error reading its intel.");
                return;
            }

            // play turn
              // currently does nothing: This is where the AI propper should do its work.

            // save turn
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
