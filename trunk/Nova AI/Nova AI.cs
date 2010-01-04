// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// A simple AI harness.
// This runs as a command line application to have the computer play a nova
// turn.
// TODO - have the nova console recognise a computer player and call the AI
// to take the turn - should wait until the turn files are properly re-structured.
//
// TODO - suggest placing wrappers around PlayerTurn.Generate and GUIdata.Load 
//        incase these interfaces change when file formats are reworked.
//
// TODO - refactor this to better seperate the AI itself from the program that 
//        interfaces with the console and data files.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Nova;

namespace Nova_AI
{
    class Program
    {
        static void Main(string[] args)
        {
            String RaceName = null;
            int TurnNumber = -1;
            // read paramaters for race and turn to play
            // TODO: parse the parameters with options to make it less dependant 
            //       on order and number of parameters. e.g.: -r<race_name> -t<turn_number> -p<password>
            Console.WriteLine("Nova AI");
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Nova_AI <race_name> <turn_number>");
                return;
            }

            RaceName = args[0];
            TurnNumber = int.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture);

            // read in race data
            Console.WriteLine("Playing turn {0} for race \"{1}\".", TurnNumber, RaceName);
            try
            {
                // TODO bypass GUI race selection (multi-player) and password entry for AI
                GUIdata.Load(true); 
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error.");
            }

            // play turn
              // currently does nothing: This is where the AI propper should do its work.

            // save turn
            PlayerTurn.Generate();

            return;
        }
    }
}
