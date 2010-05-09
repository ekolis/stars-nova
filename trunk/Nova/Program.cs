#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 The Stars-Nova Project
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

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Common;
using System.Reflection;

namespace Nova
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            RunApplication(args);
        }

        private static void RunApplication(string[] args)
        {
            string firstArgument = args.FirstOrDefault();
            string[] coreArgs = args.Skip(1).ToArray();
            switch (firstArgument)
            {
                case CommandArguments.Option.ConsoleSwitch:
                    WinForms.Console.NovaConsoleMain.Main(); break;
                case CommandArguments.Option.ComponentEditorSwitch:
                    WinForms.ComponentEditor.Program.Main(); break;
                case CommandArguments.Option.RaceDesignerSwitch:
                    WinForms.RaceDesigner.RaceDesignerForm.Main(); break;
                case CommandArguments.Option.GuiSwitch:
                    WinForms.Gui.NovaGUI.Main(coreArgs); break;
                case CommandArguments.Option.NewGameSwitch:
                    WinForms.NewGame.NewGame.Main(); break;
                case CommandArguments.Option.AiSwitch:
                    Ai.Program.Main(coreArgs); break;
                case CommandArguments.Option.LauncherSwitch:
                case null:
                    WinForms.Launcher.Program.Main(); break;
                case CommandArguments.Option.HelpSwitch:
                    ShowHelpDialog(null, false); break;
                default:
                    ShowErrorDialog(args); break;
            }
        }

        private static void ShowErrorDialog(string[] args)
        {
            string commandArguments = CommandArgumentsToString(args);
            string fileName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);

            string message = string.Format(
                "Invalid command line arguments{0}{1} {2}",
                Environment.NewLine, fileName, commandArguments);

            ShowHelpDialog(message, true);
        }

        private static void ShowHelpDialog(string message, bool error)
        {
            if (!string.IsNullOrEmpty(message))
            {
                // Add error message at the top.
                message += Environment.NewLine + Environment.NewLine;
            }

            message += string.Format(
                "Supported command line arguments{0}" + 
                "===================={0}" +
                "Start Launcher{0}" +
                "    [{1}]{0}" +
                "Start New Game Wizard{0}" +
                "    {2}{0}" +
                "Start Race Designer{0}" +
                "    {3}{0}" +
                "Start Component Editor{0}" +
                "    {4}{0}" +
                "Start Console{0}" +
                "    {5}{0}" +
                "Start GUI{0}" +
                "    {6} {9} <race> {10} <turn> {11} <intel file> {12} <state file>{0}" +
                "Run AI{0}" +
                "    {7} {9} <race> {10} <turn> {11} <intel file>{0}" +
                "Display this help screen{0}" +
                "    {8}",
                Environment.NewLine,
                CommandArguments.Option.LauncherSwitch,
                CommandArguments.Option.NewGameSwitch,
                CommandArguments.Option.RaceDesignerSwitch,
                CommandArguments.Option.ComponentEditorSwitch,
                CommandArguments.Option.ConsoleSwitch,
                CommandArguments.Option.GuiSwitch,
                CommandArguments.Option.AiSwitch,
                CommandArguments.Option.HelpSwitch,
                CommandArguments.Option.RaceName, CommandArguments.Option.Turn,
                CommandArguments.Option.IntelFileName, CommandArguments.Option.StateFileName);

            MessageBox.Show(message, "Stars! Nova " + (error ? "Error" : "Information"), MessageBoxButtons.OK,
                (error ? MessageBoxIcon.Error : MessageBoxIcon.Information),
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static string CommandArgumentsToString(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string argument in args)
            {
                if (argument.IndexOf(' ') < 0)
                {
                    sb.Append(argument);
                    sb.Append(" ");
                }
                else
                {
                    sb.Append("\"");
                    sb.Append(argument);
                    sb.Append("\" ");
                }
            }
            return sb.ToString();
        }
    }
}
