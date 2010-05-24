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
// The splash screen and launcher application for starting Nova. Nova should
// normally be started by running the NovaLauncher application.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

using Nova.Common;
#endregion

namespace Nova.WinForms.Launcher
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
            string version = Application.ProductVersion;
            string[] versionParts = version.Split('.');
            string productVersion = string.Join(".", versionParts, 0, 3);

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            int buildNumber = assemblyName.Version.Build;
            int revision = assemblyName.Version.Revision;
            DateTime start = new DateTime(2000, 1, 1);
            DateTime buildDate = start.Add(new TimeSpan(buildNumber, 0, 0, 2 * revision, 0));

            versionNumber.Text = string.Format("{0}  -  {1}", productVersion, buildDate.ToShortDateString());

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
            try
            {
                Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.RaceDesignerSwitch);
                Application.Exit();
            }
            catch
            {
                Report.Error("Failed to launch Race Designer.");
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
            try
            {
                Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.NewGameSwitch);
                Application.Exit();
            }
            catch
            {
                Report.Error("Failed to launch New Game Wizard.");
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
            String IntelFileName = "";
            bool GameLaunched = false;

            // have the user identify the game to open
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Open Game";
                fd.FileName = "*" + Global.IntelExtension;
                DialogResult result = fd.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                IntelFileName = fd.FileName;
            }
            catch
            {
                Report.FatalError("Unable to open a game.");
            }

            // Launch the GUI
            CommandArguments args = new CommandArguments();
            args.Add(CommandArguments.Option.GuiSwitch);
            args.Add(CommandArguments.Option.IntelFileName, IntelFileName);

            try
            {
                Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                GameLaunched = true;
            }
            catch
            {
                Report.Error("NovaLauncher.cs: openGameButton_Click() - Failed to launch GUI.");
            }

            // Launch the Console if this is a local game, i.e. if the console.state is in the same directory.
            String ServerStateFileName = "";
            FileInfo IntelFileInfo = new FileInfo(IntelFileName);
            String GamePathName = IntelFileInfo.DirectoryName;
            DirectoryInfo GameDirectoryInfo = new DirectoryInfo(GamePathName);
            FileInfo[] GameFilesInfo = GameDirectoryInfo.GetFiles();
            foreach (FileInfo file in GameFilesInfo)
            {
                if (file.Extension == Global.ServerStateExtension)
                {
                    ServerStateFileName = file.FullName;
                }
            }

            if (ServerStateFileName != "")
            {
                args.Clear();
                args.Add(CommandArguments.Option.ConsoleSwitch);
                args.Add(CommandArguments.Option.StateFileName, ServerStateFileName);

                try
                {
                    Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                    GameLaunched = true;
                }
                catch
                {
                    Report.Error("NovaLauncher.cs: openGameButton_Click() - Failed to launch GUI.");
                }
            }

            if (GameLaunched)
            {
                Application.Exit();
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
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.GuiSwitch);
                args.Add(CommandArguments.Option.StateFileName, ClientStateFile);
                try
                {
                    Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                    Application.Exit();
                }
                catch
                {
                    Report.Error("Failed to launch GUI.");
                }
            }

            // start the server
            if (ServerStateFile != null)
            {
                try
                {
                    Process.Start(Assembly.GetExecutingAssembly().Location, CommandArguments.Option.ConsoleSwitch);
                    Application.Exit();
                    return;
                }
                catch
                {
                    Report.FatalError("Unable to launch Console.");
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
