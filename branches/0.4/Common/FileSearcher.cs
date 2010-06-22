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
// First, check if the file path is defined by a registry key, else
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
        #region Public Methods

        /// <summary>
        /// Identify the player race's. 
        /// This is done by enumerating the race files present and
        /// loading each race definition.
        /// </summary>
        /// <returns>An Hashtable containing all the races</returns>
        public static Hashtable GetAvailableRaces()
        {
            Hashtable AllRaces = new Hashtable();

            string RaceFolder = GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
            DirectoryInfo directory = new DirectoryInfo(RaceFolder);

            FileInfo[] raceFiles = directory.GetFiles("*" + Global.RaceExtension);

            if (raceFiles.Length == 0)
            {
                Report.Error("No race files found in game folder: \"" + RaceFolder  + "\"");

            }
            else
            {
                foreach (FileInfo file in raceFiles)
                {
                    Race race = new Race(file.FullName);
                    AllRaces[race.Name] = race;
                }
            }
            return AllRaces;
        }


        /// <summary>
        /// Find all the parts of Nova in one go, stoping short of asking the user.
        /// This should be run at application startup when we know where the working directory is.
        /// </summary>
        public static void SetKeys()
        {
            try
            {
                GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                GetFolder(Global.ClientFolderKey, Global.ClientFolderName);
                GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
            }
            catch (Exception e)
            {
                Report.FatalError("Unable to locate essential application folders: " + e.Message);
            }
        }


        /// <summary>
        /// Get the game settings file.
        /// </summary>
        /// <returns>The full path&name of the game settings file or null.</returns>
        public static string GetSettingsFile()
        {
            string settings;

            // try the registry first
            settings = GetFile(Global.SettingsKey, false, "", "", "", false);

            if (!File.Exists(settings))
            {
                // if the settings file itself is not registered, look for any settings file in the ServerFolder
                string serverFolder = GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                DirectoryInfo ServerFolderInfo = new DirectoryInfo(serverFolder);
                foreach (FileInfo file in ServerFolderInfo.GetFiles())
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

        /// <summary>
        /// Find the file 'fileName'. Use registryKey if possible, or look
        /// in the likely path, or search the nova file tree, or ask the user.
        /// </summary>
        /// <param name="registryKey">The registry key we would like to store the file path in. If this registry key is not set and we find the file, then it will be set to save searching next time. Ideally this key is set when the application is installed.</param>
        /// <param name="pathOnly">true if the registry key should contain only the path. False if it is the path+file name</param>
        /// <param name="relativePath">The expected path relative to the running application.</param>
        /// <param name="fileName">The name of the file we are looing for.</param>
        /// <returns>The absolute path, including the file name. null if the file can't be found.</returns>
        public static string GetFile(string registryKey, bool pathOnly, string developmentPath, string deployedPath, string fileName, bool askUser)
        {
            // Tempory storage for building the absolute path reference
            string AbsoluteReference = null;
            bool registryOK = true; // assume it is until we prove otherwise

            if (registryKey != null)
            {
                // Try the registry key
                AbsoluteReference = GetRegistryValue(registryKey);
                if (pathOnly) AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            // did we find it?
            if (!File.Exists(AbsoluteReference))
            {
                registryOK = false;
                // Try the deployed path
                
                AbsoluteReference = Path.Combine(Directory.GetCurrentDirectory(), deployedPath.Replace('/', Path.DirectorySeparatorChar));
                AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            if (!File.Exists(AbsoluteReference))
            {
                registryOK = false;
                // Try the development path
                AbsoluteReference = Path.Combine(System.IO.Directory.GetCurrentDirectory(), developmentPath.Replace('/', Path.DirectorySeparatorChar));
                AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            // Try searching the nova tree (brute force) - TODO (priority 4)

            if (!File.Exists(AbsoluteReference))
            {
                // Ask the user for help
                if (askUser) AbsoluteReference = AskUserForFile(fileName);
            }

            // Finish up
            if (!File.Exists(AbsoluteReference))
            {
                AbsoluteReference = null;
            }
            else
            {
                if (! registryOK && registryKey != null)
                    SetNovaRegistryValue(registryKey, AbsoluteReference);
            }

            return AbsoluteReference;
        }

        /// <summary>
        /// Find a folder. Use this if you are going to store something there. If you intend to open or use a file, use GetFile() instead.
        /// </summary>
        /// <param name="registryKey">The key we would like the folder path to be stored in.</param>
        /// <param name="relativePath">The default folder name, as in NovaRoot\defaultFolder, to use if the registry key is not set.</param>
        /// <returns>The path to the folder, being either the folder defined by the registry key, or Path.Combine(NovaRoot, defaultFolder). Will create the folder if neccessary</returns>
        public static string GetFolder(string registryKey, string defaultFolder)
        {
            // Tempory storage for building the absolute path reference
            string FolderPath = null;
            bool registryOK = true; // assume it is until we prove otherwise

            if (registryKey != null)
            {
                // Try the registry key
                FolderPath = GetRegistryValue(registryKey);
            }

            // did we find it?
            if (FolderPath == null || !Directory.Exists(FolderPath))
            {
                registryOK = false;
                // Try the default folder
                string NovaRoot = GetNovaRoot();

                FolderPath = Path.Combine(NovaRoot, defaultFolder);
            }

            // If the default folder doesn't exist, create it.
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch
                {
                    Report.Error("Unable to create directory: " + FolderPath);
                    FolderPath = null;
                }
            }

            // Finish Up
            if (FolderPath == null || !Directory.Exists(FolderPath))
            {
                Report.Error("Folder does not exist: " + FolderPath);
                FolderPath = null;
            }
            else
            {
                if (!registryOK && registryKey != null)
                    SetNovaRegistryValue(registryKey, FolderPath);
            }

            return FolderPath;
        }

        /// <summary>
        /// Store a registry key to make finding game files/folders easier.
        /// </summary>
        /// <param name="registryKey">The nova registry key to use. These are defined in the nova Global object.</param>
        /// <param name="value">The text to be associated with this key.</param>
        public static void SetNovaRegistryValue(string registryKey, string value)
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.CreateSubKey(Global.RootRegistryKey);
            subKey.SetValue(registryKey, value);
            key.Close();
            subKey.Close();
        }

        #endregion

        // --------------------- Private Implementation Functions -------------------------------------------------
        #region Private Methods

        /// <summary>
        /// Look up a registry key and get its value, if any.
        /// </summary>
        /// <param name="registryKey">The key to look up.</param>
        /// <returns>The value of the key, or null if invalid / not set.</returns>
        private static string GetRegistryValue(string registryKey)
        {
            RegistryKey regKey = Registry.CurrentUser;
            RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
            string Path = subKey.GetValue(registryKey, "?").ToString();

            if (Path == "?" || Path == "")
            {
                Path = null;
            }

            return Path;

        }


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
