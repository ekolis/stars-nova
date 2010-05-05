// ============================================================================
// Nova (c) 2009, 2010 stars-nova
// See https://sourceforge.net/projects/stars-nova/
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
using System.Reflection;
using Nova.Gui;
using Nova.RaceDesigner;
using NovaCommon;
using Nova.NewGame;
using Nova.Console;

namespace Nova.Launcher
{
    public partial class NovaLauncher : Form
    {
        String ServerStateFile = null;
        String ClientStateFile = null;

        #region Initialisation

        /// <summary>
        /// Construction and initialisation
        /// </summary>
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

        /// <summary>
        /// When the 'exit' button is pressed, terminate the Nova Launcher
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// When the 'Race Designer' button is pressed, launch the Race Designer application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void raceDesignerButton_Click(object sender, EventArgs e)
        {
            Form raceDesigner = new RaceDesignerForm();
            Hide();
            raceDesigner.ShowDialog(null);
            Application.Exit();
        }


        /// <summary>
        /// When the 'New Game' button is pressed, launch the New Game Wizard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            Form newGame = new NewGameWizard();
            Hide();
            newGame.ShowDialog(null);
            Application.Exit();
        }

        /// <summary>
        /// When the 'Open Game' button is pressed, open a file browser to locate the game and open it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void openGameButton_Click(object sender, EventArgs e)
        {
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

            Hide();
            NovaGUI.LaunchModal(args);
            Application.Exit();
        }


        /// <summary>
        /// When the 'Continue Game' button is pressed, continue the last opened game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void continueGameButton_Click(object sender, EventArgs e)
        {
            if (ClientStateFile == null && ServerStateFile == null) return;
            Hide();

            // start the GUI
            if (ClientStateFile != null)
            {
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.StateFileName, ClientStateFile);
                try
                {
                    NovaGUI.LaunchModal(args);
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
                Form console = new NovaConsoleMain();
                console.Show();
            }
        }


        /// <summary>
        /// When the 'Nova Website' link is clicked, go to the nova website with the default browser, if allowed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <summary>
        /// Support funtion to open the Nova Website
        /// </summary>
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
