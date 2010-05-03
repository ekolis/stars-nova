using System;
using System.Windows.Forms;
using Nova.Launcher;
using Nova.Console;
using Nova.ComponentEditor;
using Nova.Gui;
using Nova.NewGame;
using Nova.RaceDesigner;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Find files we may need
            FileSearcher.SetKeys();

            Form mainForm = FindMainForm(args);
            Application.Run(mainForm);
        }

        private static Form FindMainForm(string[] args)
        {
            string firstArgument = (args.Length == 0) ? null : args[0];
            switch (firstArgument)
            {
                case "--console": return new NovaConsoleMain();
                case "--components": return new ComponentEditorWindow();
                case "--races": return new RaceDesignerForm();
                case "--gui": return new NovaGUI();
                case "--newgame": return new NewGameWizard();
                default: return new NovaLauncher();
            }
        }
    }
}
