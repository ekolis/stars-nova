// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module will identify the folder where the Console game files will be
// kept. Note that the Nova Console is expected to run on a different PC to
// that used by the players. However, it can be on the same machine and even be
// the one used for player game files as all file names from all programs are
// unique (incorporating the race name where necessry).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================


using Microsoft.Win32;
using NovaCommon;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System;

namespace NovaConsole 
{
    class GameFolder
    {

// ============================================================================
// Identify the folder that will hold all of the console game-related data.
// Check that the game folder key actually exists before we try and do anything
// with its value (if it is actually null trying to apply ToString to it will
// throw an exception). If no value has been saved in the registry then pop up
// a dialog to find out where to keep the files. Also pop up the same dialog if
// a folder has been specified in the registry but it doesn't exist.
// ============================================================================

      public static void Identify()
      {
         FolderBrowserDialog folderDialog = new FolderBrowserDialog();
         RegistryKey         regKey       = null;
         RegistryKey         regSubKey    = null;
         string              gameFolder   = null;
         Object              possibleKey  = null;
         bool                askForFolder = false;
         
         regKey      = Registry.CurrentUser;
         regSubKey   = regKey.CreateSubKey(Global.RootRegistryKey);
         possibleKey = regSubKey.GetValue(Global.ServerFolderKey);

         if (possibleKey == null || possibleKey.ToString() == "") {
            askForFolder = true;
         }
         else {
            gameFolder = possibleKey.ToString();
            if (Directory.Exists(gameFolder) == false) {
               askForFolder = true;
            }
         }

         if (askForFolder) {
             folderDialog.Description = 
             "Select the folder where the Console game files are located.";

            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.Cancel) {
               Report.FatalError("You must specify a Console game folder");
            }

            gameFolder = folderDialog.SelectedPath + "\\";
            regSubKey.SetValue(Global.ServerFolderKey, gameFolder);
         }
          
         ConsoleState.Data.GameFolder    = gameFolder;
         ConsoleState.Data.StatePathName = gameFolder + "Console.state";
         ConsoleState.Restore();
      }

   }
}
