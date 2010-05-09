using System;
using System.Linq;
using NovaCommon;

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
            args = args.Skip(1).ToArray();
            switch (firstArgument)
            {
                case CommandArguments.Option.ConsoleSwitch: WinForms.Console.NovaConsoleMain.Main(); break;
                case CommandArguments.Option.ComponentEditorSwitch: WinForms.ComponentEditor.Program.Main(); break;
                case CommandArguments.Option.RaceDesignerSwitch: WinForms.RaceDesigner.RaceDesignerForm.Main(); break;
                case CommandArguments.Option.GuiSwitch: WinForms.Gui.NovaGUI.Main(args); break;
                case CommandArguments.Option.NewGameSwitch: WinForms.NewGame.NewGame.Main(); break;
                case CommandArguments.Option.AiSwitch: Ai.Program.Main(args); break;
                default: WinForms.Launcher.Program.Main(); break;
            }
        }
    }
}
