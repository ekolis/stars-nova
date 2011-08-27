#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project.
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

namespace Nova.WinForms.Launcher
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    
    using Nova.Common;
    
    /// <Summary>
    /// The Stars! Nova - Launcher <see cref="Form"/>
    /// </Summary>
    public partial class NovaLauncher : Form
    {
        private readonly string serverStateFile;
        private readonly string clientStateFile;

        /// <Summary>
        /// The splash screen and launcher application for starting Nova. Nova should
        /// normally be started by running the NovaLauncher.
        /// </Summary>
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

            // look for a game in progress
            this.serverStateFile = FileSearcher.GetFile(Global.ServerStateKey, false, "", "", "", false);
            this.clientStateFile = FileSearcher.GetFile(Global.ClientStateKey, false, "", "", "", false);
            if (this.serverStateFile == null && this.clientStateFile == null)
            {
                continueGameButton.Enabled = false;
            }
        }
  
        private void RunNewGameWizard()
        {
            NewGameWizard wizard = new NewGameWizard();
            wizard.FormClosing += NewGameWizardClosing;
            wizard.Show();
        }
        
        /// <Summary>
        /// When the 'exit' button is pressed, terminate the Nova Launcher
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <Summary>
        /// When the 'Race Designer' button is pressed, launch the Race Designer application.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RaceDesignerButton_Click(object sender, EventArgs e)
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


        /// <Summary>
        /// When the 'New Game' button is pressed, launch the New Game Wizard.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            RunNewGameWizard();
            
            Hide();
        }


        /// <Summary>
        /// When the 'Open Game' button is pressed, open a file browser to locate the game and open it.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OpenGameButton_Click(object sender, EventArgs e)
        {
            string intelFileName = "";
            bool gameLaunched = false;

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
                intelFileName = fd.FileName;
            }
            catch
            {
                Report.FatalError("Unable to open a game.");
            }

            // Launch the GUI
            CommandArguments args = new CommandArguments();
            args.Add(CommandArguments.Option.GuiSwitch);
            args.Add(CommandArguments.Option.IntelFileName, intelFileName);

            try
            {
                Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                gameLaunched = true;
            }
            catch
            {
                Report.Error("NovaLauncher.cs: OpenGameButton_Click() - Failed to launch GUI.");
            }

            // Launch the Console if this is a local game, i.e. if the console.state is in the same directory.
            string serverStateFileName = "";
            FileInfo intelFileInfo = new FileInfo(intelFileName);
            string gamePathName = intelFileInfo.DirectoryName;
            DirectoryInfo gameDirectoryInfo = new DirectoryInfo(gamePathName);
            FileInfo[] gameFilesInfo = gameDirectoryInfo.GetFiles();
            foreach (FileInfo file in gameFilesInfo)
            {
                if (file.Extension == Global.ServerStateExtension)
                {
                    serverStateFileName = file.FullName;
                }
            }

            if (serverStateFileName.Length > 0)
            {
                args.Clear();
                args.Add(CommandArguments.Option.ConsoleSwitch);
                args.Add(CommandArguments.Option.StateFileName, serverStateFileName);

                try
                {
                    Process.Start(Assembly.GetExecutingAssembly().Location, args.ToString());
                    gameLaunched = true;
                }
                catch
                {
                    Report.Error("NovaLauncher.cs: OpenGameButton_Click() - Failed to launch GUI.");
                }
            }

            if (gameLaunched)
            {
                Application.Exit();
            }
           
        }


        /// <Summary>
        /// When the 'Continue Game' button is pressed, continue the last opened game.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ContinueGameButton_Click(object sender, EventArgs e)
        {
            // start the GUI
            if (this.clientStateFile != null)
            {
                CommandArguments args = new CommandArguments();
                args.Add(CommandArguments.Option.GuiSwitch);
                args.Add(CommandArguments.Option.StateFileName, this.clientStateFile);
                
                try
                {
                    Nova.WinForms.Gui.NovaGUI gui = new Nova.WinForms.Gui.NovaGUI(args.ToArray());
                    gui.Show();
                }
                catch
                {
                    Report.Error("Failed to launch GUI.");
                }
            }

            // start the server
            if (this.serverStateFile != null)
            {
                try
                {
                    Nova.WinForms.Console.NovaConsoleMain novaConsole = new Nova.WinForms.Console.NovaConsoleMain();                    
                    novaConsole.Show();
                    return;
                }
                catch
                {
                    Report.FatalError("Unable to launch Console.");
                }
            }
        }


        /// <Summary>
        /// When the 'Nova Website' link is clicked, go to the nova website with the default browser, if allowed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void WebLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
  
        private void NewGameWizardClosing(object sender, FormClosingEventArgs e)
        {
            switch ((sender as Form).DialogResult)
            {
            case DialogResult.OK:
                Close();
                break;
            case DialogResult.Cancel:
                Show();
                break;
            case DialogResult.Retry:
                RunNewGameWizard();
                break;
            }    
        }

        /// <Summary>
        /// Support funtion to open the Nova Website
        /// </Summary>
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited 
            // to true.
            webLink.LinkVisited = true;
            // Call the Process.Start method to open the default browser 
            // with a URL:
            System.Diagnostics.Process.Start(Global.NovaWebSite);
        }
    }
}
