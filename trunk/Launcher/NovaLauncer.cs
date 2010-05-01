﻿#region Copyright Notice
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
// The splash screen and launcher application for starting Nova. Nova should
// normally be started by running the NovaLauncher application.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

using NovaCommon;

namespace NovaLauncher
{
    /// <summary>
    /// The Stars! Nova - Launcher <see cref="Form"/>
    /// </summary>
    public partial class NovaLauncher : Form
    {
        String ServerStateFile = null;
        String ClientStateFile = null;

        #region Initialisation

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction and initialisation
        /// </summary>
        /// ----------------------------------------------------------------------------
        public NovaLauncher()
        {
            InitializeComponent();

            // Show the Nova version
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            AssemblyName myAssemblyName = myAssembly.GetName();
            versionNumber.Text = myAssemblyName.Version.ToString();
            string version = Assembly.GetCallingAssembly().FullName.Split(',')[1];
            DateTime start = new DateTime(2000, 1, 1);
            int buildNumber = Convert.ToInt32(version.Split('.')[2]);
            int revision = Convert.ToInt32(version.Split('.')[3]);
            DateTime buildDate = start.Add(new TimeSpan(buildNumber, 0, 0, 2 * revision, 0));

            versionNumber.Text += " " + buildDate.ToShortDateString();

            // ensure registry keys are initialised
            FileSearcher.SetKeys();

            // look for a game in progress
            ServerStateFile = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
            ClientStateFile = FileSearcher.GetFile(Global.ClientStateKey, false, "", "", "", false);
            if (ServerStateFile == null && ClientStateFile == null)
                continueGameButton.Enabled = false;
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'exit' button is pressed, terminate the Nova Launcher
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Race Designer' button is pressed, launch the Race Designer application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'New Game' button is pressed, launch the New Game Wizard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Open Game' button is pressed, open a file browser to locate the game and open it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Continue Game' button is pressed, continue the last opened game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// When the 'Nova Website' link is clicked, go to the nova website with the default browser, if allowed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void webLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch 
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Support funtion to open the Nova Website
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited 
            // to true.
            webLink.LinkVisited = true;
            //Call the Process.Start method to open the default browser 
            //with a URL:
            System.Diagnostics.Process.Start(Global.NovaWebSite);
        }

        #endregion

    }//NovaLauncher

}//namespace
