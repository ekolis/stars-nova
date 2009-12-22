// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// A simple AI harness.
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
            Console.WriteLine("Nova AI");
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Nova_AI <race_name> <turn_number>");
                return;
            }

            RaceName = args[0];
            TurnNumber = int.Parse(args[1]);

            // read in race data
            Console.WriteLine("Playing turn {0} for race \"{1}\".", TurnNumber, RaceName);
            try
            {
                GUIdata.Load(false);
            }
            catch
            {
                Console.WriteLine("Nova_AI encountered an error.");
            }
            // play turn

            // save turn
            return;
        }
    }
}
