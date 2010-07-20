#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 stars-nova
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
// The FileSearcher object is used to find a file that is part of Nova. It uses
// the following stratergy:
// First, check if the file path is defined by a key in nova.conf, else
// see if the file is in the expected relative path location, else
// see if the file can be found by searching the nova installation directory, else
// ask the user to locate the file, else
// let the calling function know that we can't find the file and have it decide 
// what to do.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Nova.Common
{
    public static class FileSearcher
    {
        private static bool disableComponentGraphics; // if we can't find them the first time, stop asking.

        #region Public Methods

        /// <summary>
        /// Identify the player race's. 
        /// This is done by enumerating the race files present and
        /// loading each race definition.
        /// </summary>
        /// <returns>An Hashtable containing all the races</returns>
        public static Hashtable GetAvailableRaces()
        {
            Hashtable allRaces = new Hashtable();

            string raceFolder = GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
            DirectoryInfo directory = new DirectoryInfo(raceFolder);

            FileInfo[] raceFiles = directory.GetFiles("*" + Global.RaceExtension);

            if (raceFiles.Length == 0)
            {
                Report.Error("No race files found in game folder: \"" + raceFolder  + "\"");

            }
            else
            {
                foreach (FileInfo file in raceFiles)
                {
                    Race race = new Race(file.FullName);
                    allRaces[race.Name] = race;
                }
            }
            return allRaces;
        }


        /// <summary>
        /// Get the game settings file.
        /// </summary>
        /// <returns>The full path&name of the game settings file or null.</returns>
        public static string GetSettingsFile()
        {
            string settings;

            settings = GetFile(Global.SettingsKey, false, "", "", "", false);

            if (!File.Exists(settings))
            {
                // if the settings file itself is not registered, look for any settings file in the ServerFolder
                string serverFolder = GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                DirectoryInfo serverFolderInfo = new DirectoryInfo(serverFolder);
                foreach (FileInfo file in serverFolderInfo.GetFiles())
                {
                    if (file.Extension == Global.SettingsExtension)
                    {
                        settings = file.FullName;
                        break;
                    }
                }
            }

            if (!File.Exists(settings))
            {
                // if all else fails, ask the user
                settings = AskUserForFile("Your Game Name.settings");

            }

            return settings;
        }

        public static string GetComponentFile()
        {
            string components;
            bool updateConf = false;

            // try the config file
            using (Config conf = new Config())
            {
                components = conf[Global.ComponentFileKey];


                // or try the usual location NovaRoot\components.xml
                if (!File.Exists(components))
                {
                    updateConf = true;
                    components = Path.Combine(GetNovaRoot(), Global.ComponentFileName);
                }

                // if all else fails, ask the user
                if (!File.Exists(components))
                {
                    components = AskUserForFile(Global.ComponentFileName);
                }

                if (updateConf && File.Exists(components))
                {
                    conf[Global.ComponentFileKey] = components;
                }
            }

            return components;
        }

        /// <summary>
        /// Get the game settings file.
        /// </summary>
        /// <returns>The full path&name of the nova.config file or null.</returns>
        public static string GetConfigFile()
        {
            string config;

            // File should be NovaRoot\nova.config
            config = Path.Combine(GetNovaRoot(), Global.ConfigFileName);
            return config;
        }
        
        public static string GetGraphicsPath()
        {
            string graphicsPath;
            using (Config conf = new Config())
            {
                bool updateConf = false;

                if (disableComponentGraphics) return null;


                // Try the config file
                graphicsPath = conf[Global.GraphicsFolderKey];

                // did we find it?
                if (graphicsPath == null || !Directory.Exists(graphicsPath))
                {
                    updateConf = true;
                    // Try the default folder
                    string novaRoot = GetNovaRoot();
                    graphicsPath = Path.Combine(novaRoot, Global.GraphicsFolderName);
                }

                if (!Directory.Exists(graphicsPath))
                {
                    // if all else fails, ask the user
                    FolderBrowserDialog graphicsFolderBrowser = new FolderBrowserDialog();

                    graphicsFolderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
                    graphicsFolderBrowser.SelectedPath = GetNovaRoot();
                    graphicsFolderBrowser.Description = "Locate the Stars! Nova \"Graphics\" folder.";
                    DialogResult gameFolderBrowserResult = graphicsFolderBrowser.ShowDialog();

                    // Check for cancel being pressed (in the new game save file dialog).
                    if (gameFolderBrowserResult == DialogResult.OK)
                    {
                        graphicsPath = graphicsFolderBrowser.SelectedPath;
                    }

                }

                if (Directory.Exists(graphicsPath) && updateConf)
                {
                    conf[Global.GraphicsFolderKey] = graphicsPath;
                }
                else
                {
                    disableComponentGraphics = true;
                }
            }
            return graphicsPath;
        }

        /// <summary>
        /// Find the file 'fileName'. 
        /// </summary>
        /// <param name="configKey">The config file key we would like to store the file path in. If this key is not set and we find the file, then it will be set to save searching next time. Ideally this key is set when the application is installed.</param>
        /// <param name="pathOnly">true if the config file should contain only the path. False if it is the path+file name</param>
        /// <param name="relativePath">The expected path relative to the running application.</param>
        /// <param name="fileName">The name of the file we are looing for.</param>
        /// <returns>The absolute path, including the file name. null if the file can't be found.</returns>
        public static string GetFile(string configKey, bool pathOnly, string developmentPath, string deployedPath, string fileName, bool askUser)
        {
            // Tempory storage for building the absolute path reference
            string absoluteReference = null;
            using (Config conf = new Config())
            {
                bool confOk = true; // assume it is until we prove otherwise

                if (configKey != null)
                {
                    // Try the config file
                    absoluteReference = conf[configKey];
                    if (pathOnly) absoluteReference = Path.Combine(absoluteReference, fileName);
                }

                // did we find it?
                if (!File.Exists(absoluteReference))
                {
                    confOk = false;
                    // Try the deployed path

                    absoluteReference = Path.Combine(Directory.GetCurrentDirectory(), deployedPath.Replace('/', Path.DirectorySeparatorChar));
                    absoluteReference = Path.Combine(absoluteReference, fileName);
                }

                if (!File.Exists(absoluteReference))
                {
                    confOk = false;
                    // Try the development path
                    absoluteReference = Path.Combine(System.IO.Directory.GetCurrentDirectory(), developmentPath.Replace('/', Path.DirectorySeparatorChar));
                    absoluteReference = Path.Combine(absoluteReference, fileName);
                }

                // Try searching the nova tree (brute force) - TODO (priority 4)

                if (!File.Exists(absoluteReference))
                {
                    // Ask the user for help
                    if (askUser) absoluteReference = AskUserForFile(fileName);
                }

                // Finish up
                if (!File.Exists(absoluteReference))
                {
                    absoluteReference = null;
                }
                else
                {
                    if (!confOk && configKey != null)
                    {

                        conf[configKey] = absoluteReference;
                    }
                }
            }
            return absoluteReference;
        }

        /// <summary>
        /// Find a folder. Use this if you are going to store something there. If you intend to open or use a file, use GetFile() instead.
        /// </summary>
        /// <param name="configKey">The config file key we would like the folder path to be stored in.</param>
        /// <param name="relativePath">The default folder name, as in NovaRoot\defaultFolder, to use if the key is not set.</param>
        /// <returns>The path to the folder, being either the folder defined by the key, or Path.Combine(NovaRoot, defaultFolder). Will create the folder if neccessary</returns>
        public static string GetFolder(string configKey, string defaultFolder)
        {
            // Tempory storage for building the absolute path reference
            string folderPath = null;
            using (Config conf = new Config())
            {
                bool configOk = true; // assume it is until we prove otherwise

                if (configKey != null)
                {
                    // Try the config file
                    folderPath = conf[configKey];
                }

                // did we find it?
                if (folderPath == null || !Directory.Exists(folderPath))
                {
                    configOk = false;
                    // Try the default folder
                    string novaRoot = GetNovaRoot();

                    folderPath = Path.Combine(novaRoot, defaultFolder);
                }

                // If the default folder doesn't exist, create it.
                if (!Directory.Exists(folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    catch
                    {
                        Report.Error("Unable to create directory: " + folderPath);
                        folderPath = null;
                    }
                }

                // Finish Up
                if (folderPath == null || !Directory.Exists(folderPath))
                {
                    Report.Error("Folder does not exist: " + folderPath);
                    folderPath = null;
                }
                else
                {
                    if (!configOk && configKey != null)
                    {
                        conf[configKey] = folderPath;
                    }
                }
            }
            return folderPath;
        }

        #endregion

        // --------------------- Private Implementation Functions -------------------------------------------------
        #region Private Methods

        /// <summary>
        /// Try to locate the nova root directory
        /// </summary>
        /// <returns></returns>
        private static string GetNovaRoot()
        {
            string novaRoot;

            // try working upward from the application directory
            // two likely structures - the installation structure (deployed) (e.g. Program Files/Stars! Nova/Nova.exe)
            // or the development structure (development) (e.g. stars-nova/trunk/AppName/bin/<debug|release>/AppName.exe)
            string applicationDirectory = (new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location)).DirectoryName;

            string[] folders = applicationDirectory.Split(Path.DirectorySeparatorChar);
            string upThree = "../../..";
            upThree = upThree.Replace('/', Path.DirectorySeparatorChar);
            if (folders[folders.Length - 1].ToLower() == "debug")
            {
                novaRoot = Path.Combine(applicationDirectory, upThree);
            }
            else if (folders[folders.Length - 1].ToLower() == "release")
            {
                novaRoot = Path.Combine(applicationDirectory, upThree);
            }
            else
            {
                novaRoot = applicationDirectory;
            }

            if (!Directory.Exists(novaRoot))
            {
                Report.FatalError("Unable to locate the installation path to Nova.");
            }


            return novaRoot;
        }

        /// <summary>
        /// Ask the user to locate a file.
        /// </summary>
        /// <param name="fileName">The name of the file to loacate.</param>
        /// <returns>The path & filename given or null.</returns>
        private static string AskUserForFile(string fileName)
        {
            Report.Information("Please locate the file \"" + fileName + "\".");
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = fileName;

            fileDialog.Title = "Please locate the file \"" + fileName + "\".";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.Cancel) return null;
            
            return fileDialog.FileName;
        }

        #endregion
    }
}
