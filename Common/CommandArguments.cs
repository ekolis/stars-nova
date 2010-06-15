#region Copyright Notice
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
//   [-[-]option [argument] [-[-]option [argument]]...]
//   where option is a single character
//     e.g.: -r MyRace -t 2055
//       or: --components
// * Some unimplented functions will throw a System.NotImplementedException.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Nova.Common
{
    public class CommandArguments : DictionaryBase
    {
        private readonly List<Argument> argumentList = new List<Argument>();

        private class Argument
        {
            public Argument(string option, string value)
            {
                Option = option;
                Value = value;
            }
            public string Option { get; private set; }
            public string Value { get; private set; }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Value))
                {
                    return Option;
                }
                if (Value.IndexOf(' ') < 0)
                {
                    return Option + " " + Value;
                }
                // Only quote value if it contains space(s).
                return Option + " \"" + Value + "\"";
            }
        }

        // 'enumerate' some common options
        public static class Option 
        {
            public const string AiSwitch = "--ai";
            public const string ConsoleSwitch = "--console";
            public const string ComponentEditorSwitch = "--components";
            public const string RaceDesignerSwitch = "--race";
            public const string GuiSwitch = "--gui";
            public const string NewGameSwitch = "--new";
            public const string LauncherSwitch = "--launch";
            public const string HelpSwitch = "--help";

            public static string IntelFileName = "-i";
            public static string Password = "-p";
            public static string RaceName = "-r";
            public static string StateFileName = "-s";
            public static string Turn = "-t";
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommandArguments() { }

        /// <summary>
        /// Constuctor taking a string array, usually used by main() to create a CommandArguments object from its arguments.
        /// </summary>
        /// <param name="args">an array of string arguments, the format normally recived by a main function.</param>
        public CommandArguments(string[] args) 
        {
            // process the argument list
            Parse(args);
        }

        /// <summary>
        /// WARNING : not implemented, will throw exception.
        /// </summary>
        /// <param name="args"></param>
        public CommandArguments(string args) 
        {
            Parse(args);
        }

        /// <summary>
        /// Add an option without a value (single option flag).
        /// </summary>
        /// <param name="argument"></param>
        public void Add(string argument)
        {
            AddOption(argument, "");
        }

        /// <summary>
        /// Add an option/argument pair.
        /// </summary>
        /// <param name="option">of the form -[:alphanum:], or as defined by CommandArguments.Option</param>
        /// <param name="argument">any string, such as a file name or race name</param>
        public void Add(string option, string argument)
        {
            AddOption(option, argument);
        }

        /// <summary>
        /// Add an option/argument pair.
        /// </summary>
        /// <param name="option">of the form -[:alpha:], or as defined by CommandArguments.Option</param>
        /// <param name="argument">any integer argument, such as a turn year</param>
        public void Add(string option, int argument)
        {
            AddOption(option, argument.ToString());
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
        /// <param name="option">The option, as defined in CommandArguments.Option</param>
        /// <returns></returns>
        public string this[string option]
        {
            get
            {
                return Dictionary[option] as string;
            }
        }

        /// <summary>
        /// Convert the CommandArguments to a single string as they would appear on the command line. Use for parsing the options&arguments to Process.Start()
        /// The arguments are returned in the same order that they were added.
        /// </summary>
        /// <returns>a single string representing the option&argument pairs</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int argumentCount = argumentList.Count;
            for (int i = 0; i < argumentCount; i++)
            {
                sb.Append(argumentList[i]);
                if (i + 1 < argumentCount) sb.Append(' ');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert the CommandArguments to an array of strings as they would be recieved by a main function.
        /// The arguments are returned in the same order that they were added.
        /// </summary>
        /// <returns>an array of strings containing all the option&argument pairs</returns>
        public string[] ToArray()
        {
            List<string> commandLine = new List<string>();
            foreach (Argument argument in argumentList)
            {
                commandLine.Add(argument.Option);
                commandLine.Add(argument.Value);
            }
            return commandLine.ToArray();
        }

        protected override void OnClear()
        {
            // Ensure that the ordered argument list is also cleared.
            argumentList.Clear();
        }

        protected override void OnRemoveComplete(object key, object value)
        {
            // Ensure that the ordered argument list is also updated.
            string option = key as string;
            string optionValue = value as string;
            Argument argument = argumentList.Find(x => x.Option == option && x.Value == optionValue);
            Trace.Assert(argument != null, "argument == null");
            argumentList.Remove(argument);
        }

        private void AddOption(string option, string value)
        {
            if (Dictionary.Contains(option))
            {
                // Duplicated option: Last value overwrites previous.
                Dictionary[option] = value;
            }
            else
            {
                Dictionary.Add(option, value);
            }
            argumentList.Add(new Argument(option, value));
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
            Queue<string> arguments = new Queue<string>(args);
            while (arguments.Count > 0)
            {
                string option = arguments.Dequeue();
                if (!option.StartsWith("-")) continue;

                string value;
                if (arguments.Count > 0)
                {
                    value = arguments.Peek();
                    if (value.StartsWith("-"))
                    {
                        AddOption(option, "");
                    }
                    else
                    {
                        arguments.Dequeue();
                        AddOption(option, value);
                    }
                }
                else
                {
                    AddOption(option, "");
                }
            }
        }

    }
}
