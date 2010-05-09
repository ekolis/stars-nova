using System;
using System.Linq;
using NovaCommon;
using NewGameNs=NewGame;

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
                case CommandArguments.Option.ConsoleSwitch: NovaConsole.NovaConsoleMain.Main(); break;
                case CommandArguments.Option.ComponentEditorSwitch: ComponentEditor.Program.Main(); break;
                case CommandArguments.Option.RaceDesignerSwitch: RaceDesigner.RaceDesignerForm.Main(); break;
                case CommandArguments.Option.GuiSwitch: NovaGUI.Main(args); break;
                case CommandArguments.Option.NewGameSwitch: NewGameNs.NewGame.Main(); break;
                case CommandArguments.Option.AiSwitch: Nova_AI.Program.Main(args); break;
                default: NovaLauncher.Program.Main(); break;
            }
        }
    }
}
