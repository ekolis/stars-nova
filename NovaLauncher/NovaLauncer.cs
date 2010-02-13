// ============================================================================
// Nova. (c) 2010 Daniel Vale
//
// The splash screen and launcher application for starting Nova. Nova should
// normally be started by running the NovaLauncher application.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using NovaCommon;

namespace NovaLauncher
{
    public partial class NovaLauncher : Form
    {
        String ServerStateFile = null;
        String ClientStateFile = null;

        public NovaLauncher()
        {
            InitializeComponent();

            // ensure registry keys are initialised
            FileSearcher.SetKeys();

            // look for a game in progress
            ServerStateFile = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
            ClientStateFile = FileSearcher.GetFile(Global.ClientStateKey, false, "", "", "", false);
            if (ServerStateFile == null && ClientStateFile == null)
                continueGameButton.Enabled = false;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void raceDesignerButton_Click(object sender, EventArgs e)
        {
            String RaceDesigner;
            RaceDesigner = FileSearcher.GetFile(Global.RaceDesignerKey, false, Global.RaceDesignerPath_Development, Global.RaceDesignerPath_Deployed, "RaceDesigner.exe", true);
            try
            {
                Process.Start(RaceDesigner);
                Application.Exit();
            }
            catch
            {
                Report.Error("Failed to launch Nova Race Designer.");
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            String NewGameApp;
            NewGameApp = FileSearcher.GetFile(Global.NewGameKey, false, Global.NewGamePath_Development, Global.NewGamePath_Deployed, "NewGame.exe", true);
            try
            {
                Process.Start(NewGameApp);
                Application.Exit();
            }
            catch
            {
                Report.Error("Failed to launch \"NewGame.exe\".");
            }
        }

        private void openGameButton_Click(object sender, EventArgs e)
        {
            String NovaGuiApp;
            NovaGuiApp = FileSearcher.GetFile(Global.NovaGuiKey, false, Global.NovaGuiPath_Development, Global.NovaGuiPath_Deployed, "Nova GUI.exe", true);
            String IntelFile = "";

            // have the user identify the game to open
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Open Game";
                fd.FileName = "*.intel";
                DialogResult result = fd.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                IntelFile = fd.FileName;
            }
            catch
            {
                Report.FatalError("Unable to open a game.");
            }

            CommandArguments args = new CommandArguments();
            args.Add(CommandArguments.Option.IntelFileName, IntelFile);

            try
            {
                Process.Start(NovaGuiApp, args.ToString());
                Application.Exit();
            }
            catch
            {
                Report.Error("NovaLauncher.cs: openGameButton_Click() - Failed to launch \"Nova GUI.exe\".");
            }
        }

        private void continueGameButton_Click(object sender, EventArgs e)
        {
            // start the GUI
            if (ClientStateFile != null)
            {
                String NovaGuiApp;

                NovaGuiApp = FileSearcher.GetFile(Global.NovaGuiKey, false, Global.NovaGuiPath_Development, Global.NovaGuiPath_Deployed, "Nova GUI.exe", true);
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.StateFileName, ClientStateFile);
                try
                {
                    Process.Start(NovaGuiApp, args.ToString());
                    Application.Exit();
                }
                catch
                {
                    Report.Error("Failed to launch \"Nova GUI.exe\".");
                }
            }

            // start the server
            if (ServerStateFile != null)
            {
                String NovaConsole = FileSearcher.GetFile(Global.NovaConsoleKey, false, Global.NovaConsolePath_Development, Global.NovaConsolePath_Deployed, "Nova Console.exe", false);
                try
                {
                    Process.Start(NovaConsole);
                    Application.Exit();
                    return;
                }
                catch
                {
                    Report.FatalError("Unable to launch \"Nova Console.exe\".");
                }
            }
        }
    }
}
