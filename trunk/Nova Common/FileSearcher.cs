﻿// ============================================================================
// Nova. (c) 2010 Daniel Vale
//
// The FileSearch object is used to find a file that is part of Nova. It uses
// the following stratergy:
// First, check if the file path is defined by a registry key, else
// see if the file is in the expected relative path location, else
// see if the file can be found by searching the nova installation directory, else
// ask the user to locate the file, else
// let the calling function know that we can't find the file and have it decide 
// what to do.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace NovaCommon
{
    public static class FileSearcher
    {

        // ============================================================================
        // 
        // ============================================================================

        #region Methods
        /// <summary>
        /// Identify the player race's. 
        // This is done by enumerating the race files present and
        // loading each race definition.
        /// </summary>
        /// <returns>An Hashtable containing all the races</returns>
        public static Hashtable GetAvailableRaces()
        {
            Hashtable AllRaces = new Hashtable();

            String RaceFolder = GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            DirectoryInfo directory = new DirectoryInfo
                                           (RaceFolder);

            FileInfo[] raceFiles = directory.GetFiles("*.race");

            if (raceFiles.Length == 0)
            {
                Report.Error("No race files found in game folder");

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
            GetFile(Global.NovaConsoleKey, false, Global.NovaConsolePath_Development, Global.NovaConsolePath_Deployed, "Nova Console.exe", false);
            GetFile(Global.RaceDesignerKey, false, Global.RaceDesignerPath_Development, Global.RaceDesignerPath_Deployed, "RaceDesigner.exe", false);
            GetFile(Global.NewGameKey, false, Global.NewGamePath_Development, Global.NewGamePath_Deployed, "NewGame.exe", false);
            GetFile(Global.NovaGuiKey, false, Global.NovaGuiPath_Development, Global.NovaGuiPath_Deployed, "NewGame.exe", false);

            GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
            GetFolder(Global.ClientFolderKey, Global.ClientFolderName);
            GetFolder(Global.RaceFolderKey, Global.RaceFolderName);
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
        public static String GetFile
            (String registryKey, bool pathOnly, String developmentPath, String deployedPath, String fileName, bool askUser)
        {
            // Tempory storage for building the absolute path reference
            String AbsoluteReference = null;
            bool registryOK = true; // assume it is until we prove otherwise

            if (registryKey != null)
            {
                // Try the registry key
                AbsoluteReference = GetRegistryValue(registryKey);
                if (pathOnly) AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            // did we find it?
            if (AbsoluteReference == null || !File.Exists(AbsoluteReference))
            {
                registryOK = false;
                // Try the deployed path
                
                AbsoluteReference = Path.Combine(Directory.GetCurrentDirectory(), deployedPath.Replace('/', Path.DirectorySeparatorChar));
                AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            if (AbsoluteReference == null || !File.Exists(AbsoluteReference))
            {
                registryOK = false;
                // Try the development path
                AbsoluteReference = Path.Combine(System.IO.Directory.GetCurrentDirectory(), developmentPath.Replace('/', Path.DirectorySeparatorChar));
                AbsoluteReference = Path.Combine(AbsoluteReference, fileName);
            }

            // Try searching the nova tree (brute force)
            // TODO (priority 3)

            if (AbsoluteReference == null || !File.Exists(AbsoluteReference))
            {
                // Ask the user for help
                if (askUser) AbsoluteReference = AskUserForFile(fileName);
            }

            // Finish up
            if (AbsoluteReference == null || !File.Exists(AbsoluteReference))
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
        public static String GetFolder(String registryKey, String defaultFolder)
        {
            // Tempory storage for building the absolute path reference
            String FolderPath = null;
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
                String NovaRoot = GetNovaRoot();

                FolderPath = Path.Combine(NovaRoot, defaultFolder);
            }

            // If the default folder doesn't exist, create it.
            if (FolderPath == null || !Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch
                {
                    FolderPath = null;
                }
            }

            // Finish Up
            if (FolderPath == null || !Directory.Exists(FolderPath))
            {
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
        public static void SetNovaRegistryValue(String registryKey, String value)
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.CreateSubKey(Global.RootRegistryKey);
            subKey.SetValue(registryKey, value);
        }


// --------------------- Private Implementation Functions -------------------------------------------------

        /// <summary>
        /// Look up a registry key and get its value, if any.
        /// </summary>
        /// <param name="registryKey">The key to look up.</param>
        /// <returns>The value of the key, or null if invalid / not set.</returns>
        private static string GetRegistryValue(String registryKey)
        {
            string path = null;
            using( RegistryKey regKey = Registry.CurrentUser.CreateSubKey( Global.RootRegistryKey ) )
            {
                object obj = regKey.GetValue( registryKey );
                if( null != obj )
                {
                    path = obj.ToString();
                }
            }
            return path;

        }//GetRegistryValue


        /// <summary>
        /// Try to locate the nova root directory
        /// </summary>
        /// <returns></returns>
        private static String GetNovaRoot()
        {
            String NovaRoot = null;

            // as usual, try the registry first
            NovaRoot = GetRegistryValue(Global.NovaFolderKey);

            if (NovaRoot == null || ! Directory.Exists(NovaRoot))
            {
                // try working upward from the working directory
                // two likely structure - the installation structure (deployed) (e.g. Program Files/nova/AppName/AppName.exe)
                // or the development structure (development) (e.g. stars-nova/trunk/AppName/bin/debug/AppName.exe)
                String WorkingDirectory = Directory.GetCurrentDirectory();
                string[] folders = WorkingDirectory.Split(Path.DirectorySeparatorChar);
                String upThree = "../../..";
                upThree = upThree.Replace('/', Path.DirectorySeparatorChar);
                if (folders[folders.Length - 1].ToLower() == "debug")
                {
                    NovaRoot = Path.Combine(WorkingDirectory, upThree);
                }
                else if (folders[folders.Length - 1].ToLower() == "release")
                {
                    NovaRoot = Path.Combine(WorkingDirectory, upThree);
                }
                else
                {
                    NovaRoot = Path.Combine(WorkingDirectory, "..");
                }
                
            }

            if (!Directory.Exists(NovaRoot)) NovaRoot = null;

            return NovaRoot;
        }

        private static String AskUserForFile(String fileName)
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
