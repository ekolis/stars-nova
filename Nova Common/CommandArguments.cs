// ============================================================================
// Nova. 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// An object for managing creation, passing and digesting of command line
// arguments.
//
// arguments are stored as option/argument pairs in a Dictionary
// use ToString() to get the argument list as a single string (the format need for Process.Start() )
// ust ToArray() to get the argument list as a string[] (the format recieved by main() )
//
// Known Limitations:
// (functionality not currently required by Nova)
// * Currently only handles commandlines containing the form:
//   [-option argument [-option argument]...]
//   where option is a single character
//     e.g.: -r MyRace -t 2055
// * Order of option&argument pairs is not gauranteed. You will get an option 
//   then its argument, but the pairs may be re-arranged.
// * Some unimplented functions will throw a System.NotImplementedException.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    public class CommandArguments : DictionaryBase
    {
        // 'enumerate' some common options
        public class Option 
        {
            public static string IntelFileName = "-i";
            public static string Password = "-p";
            public static string RaceName = "-r";
            public static string StateFileName = "-s";
            public static string Turn = "-t";
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public CommandArguments() { }

        /// <summary>
        /// constuctor taking a string array, usually used by main() to create a CommandArguments object from its arguments.
        /// </summary>
        /// <param name="args">an array of string arguments, the format normally recived by a main function.</param>
        public CommandArguments(string[] args) 
        {
            // process the argument list
            Parse(args);
        }

        /// <summary>
        /// WARNING : not implemented, will throw exception
        /// </summary>
        /// <param name="args"></param>
        public CommandArguments(string args) 
        {
            Parse(args);
        }

        /// <summary>
        /// WARNING : not implemented, will throw exception
        /// </summary>
        /// <param name="argument"></param>
        public void Add(string argument)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Add an option/argument pair.
        /// </summary>
        /// <param name="option">of the form -[:alphanum:], or as defined by CommandArguments.Option</param>
        /// <param name="argument">any string, such as a file name or race name</param>
        public void Add(string option, string argument)
        {
            Dictionary.Add(option, "\"" + argument + "\"");
        }

        /// <summary>
        /// Add an option/argument pair.
        /// </summary>
        /// <param name="option">of the form -[:alpha:], or as defined by CommandArguments.Option</param>
        /// <param name="argument">any integer argument, such as a turn year</param>
        public void Add(string option, int argument)
        {
            Dictionary.Add(option, argument.ToString());
        }

        /// <summary>
        /// Check if the CommandArgument(s) contains a particulat option.
        /// </summary>
        /// <param name="option">From CommandArgument.Option or of the form -[:alpha:]</param>
        /// <returns></returns>
        public bool Contains(string option)
        {
            return Dictionary.Contains(option);
        }

        /// <summary>
        /// A collection containing all the options.
        /// </summary>
        public ICollection Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        /// <summary>
        /// A collection containing all the arguments.
        /// </summary>
        public ICollection Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        /// <summary>
        /// Allow array type indexing to a Command Argument.
        /// </summary>
        /// <param name="index">The option, as defined in CommandArguments.Option</param>
        /// <returns></returns>
        public String this[String index]
        {
            get
            {
                return Dictionary[index] as String;
            }
        }

        /// <summary>
        /// Convert the CommandArguments to a single string as they would appear on the command line. Use for parsing the options&arguments to Process.Start()
        /// </summary>
        /// <returns>a single string representing the option&argument pairs</returns>
        public override String ToString()
        {
            String commandLine = "";
            foreach (DictionaryEntry de in Dictionary)
            {
                commandLine += " " + de.Key as String;
                commandLine += " " + de.Value as String;
            }
            return commandLine;
        }

        /// <summary>
        /// Convert the CommandArguments to an array of strings as they would be recieved by a main function.
        /// </summary>
        /// <returns>an array of strings containing all the option&argument pairs</returns>
        public string[] ToArray()
        {
            ArrayList commandLine = new ArrayList();
            foreach (DictionaryEntry de in Dictionary)
            {
                commandLine.Add(de.Key as String);
                commandLine.Add(de.Value as String);
            }
            return commandLine.ToArray(typeof(string)) as string[];
        }



        /// <summary>
        /// WARNING : not implemented, will throw an exception.
        /// process one or more arguments passed as a string
        /// </summary>
        /// <param name="argument"></param>
        private void Parse(string argument)
        {
            throw new System.NotImplementedException();
            // if this needs to be implemented, consider quoted arguments containing spaces, e.g. "Game Folder"
        }


        /// <summary>
        /// process one or more arguments passed as a string[]
        /// </summary>
        /// <param name="argument"></param>
        private void Parse(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null) continue;
                if (args[i][0] == '-')
                {
                    // process an option
                    if (i + 1 >= args.Length)
                    {
                        // doesn't handle multiple option groups (-vfxt) 
                        // or long option formats --race-name
                        Report.Error("Error processing argument list.");
                        return;
                    }
                    else
                    {
                        Dictionary.Add(args[i], args[i+1]);
                        i++;
                        continue;
                        
                    }

                }
                else
                {
                    // doesn't handle single option flags or
                    // arguments without options
                    Report.Error("Error processing argument list.");
                    return;
                }
            }

        }


    }
}
