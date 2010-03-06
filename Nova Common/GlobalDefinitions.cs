// ===========================================================================
// Nova. (c) 2008 Ken Reed.
// Added GraphicsFolderKey and EvaluateRelativePath() - Daniel Vale, May 2009.
//
// This module holds definitions that are global across all Nova application
// programs. E.g. Registry key names, etc.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.Xml;
using System.Collections;
using System.IO;

namespace NovaCommon
{
   public static class Global
   {
       // Note: Client/SeverState.GameFolder vs ServerFolder vs ClientFolder
       // In a single player game the ServerFolder == ClientFolder == "Game Files"
       // The only reason to save seperate keys for ServerFolder and ClientFolder
       // is so that we can simulate a network game on a single PC. When this
       // is done it will be necessary to transfer the game files from one
       // folder to the other. In Stars! this is done manually. Nova may (eventually)
       // implement an automated solution (e.g. using TCP/IP sockets to conect the
       // client and server.)

       // registry keys - game resources
       public const string RootRegistryKey = "Software\\Ken Reed\\Nova"; // where the keys are
       public const string NovaFolderKey = "NovaFolderKey";              // where Nova is installed 
       public const string ComponentFolderKey = "ComponentFolder";       // where components.xml is (possibly re-named), installation relative path is '.'. Should only change for modified game play.
       public const string GraphicsFolderKey = "GraphicsFolder";         // where pictures are stored, installation relative path is './Graphics'. Should only be changed for modding the game interface.
       public const string ClientFolderKey = "ClientFolder";             // where client side game files are, nominally './GameFiles'. Likely to be different for each active game.
       public const string ServerFolderKey = "ServerFolder";             // where server side game files are, nominally './GameFiles'. May be a network path.
       public const string RaceFolderKey = "RaceFolder";                 // where player race files are stored, nominally './GameFiles'. Players may save races elswhere to create a reusable library.
       public const string ServerStateKey = "ServerStateFile";           // the server's saved data from the most recent game, if any. Nominally ./GameFiles/Constole.state. Players may save game files anywhere, e.g. My Document\Saved Games\Nova\A Bare Foot Jay-Walk\
       public const string ClientStateKey = "ClientStateFile";           // the client's saved data from the most recent game, if any. Nominally ./GameFiles/RaceName.state. Players may save game files anywhere, e.g. My Document\Saved Games\Nova\A Bare Foot Jay-Walk\
       public const string SettingsKey = "GameSettingsFile";             // where the game settings are stored (on the server, but a copy should be avaialble to the client).


       // default folder names, used with FileSearcher.GetFolder(). 
       // Follows the above naming conventions
       public const string NovaFolderName = ".";
       public const string ComponentFolderName = ".";
       public const string GraphicsFolderName = "Graphics";
       public const string ClientFolderName = "Game Files";
       public const string ServerFolderName = "Game Files";
       public const string RaceFolderName = "Game Files";
       
       // registry keys - application files 
       public const string NovaLauncherKey = "NovaLauncher";  // where the NovaLauncher application is installed
       public const string NewGameKey = "NewGame";            // where the NewGame application is installed
       public const string RaceDesignerKey = "RaceDesigner";  // where the Race Designer application is installed
       public const string NovaConsoleKey = "NovaConsole";    // where the Nova Console application is installed
       public const string NovaGuiKey = "NovaGui";            // where the NovaLauncher application is installed
       public const string NovaAiKey = "NovaAi";              // where the Nova_AI application is installed
       
       // paths, relative to the 'current' application (it doesn't actully matter which application that is, due to the way the nova directory is structured)
       // note these are only likely to be valid when the application launches, as any file/folder browser dialog will change the current working directory
       // hence the reason we call FileSearcher.GetKeys() at application launch.
       public const string NewGamePath_Development = "../../../NewGame/bin/Debug";
       public const string NewGamePath_Deployed = "../NewGame";
       public const string RaceDesignerPath_Development = "../../../Race Designer/bin/Debug";
       public const string RaceDesignerPath_Deployed = "../Race Designer";
       public const string NovaConsolePath_Development = "../../../Nova Console/bin/Debug";
       public const string NovaConsolePath_Deployed = "../Nova Console";
       public const string NovaLauncherPath_Development = "../../../NovaLauncher/bin/Debug";
       public const string NovaLauncherPath_Deployed = "../NovaLauncher";
       public const string NovaGuiPath_Development = "../../../Nova Gui/bin/Debug";
       public const string NovaGuiPath_Deployed = "../Nova Gui";
       public const string NovaAiPath_Development = "../../../Nova AI/bin/Debug";
       public const string NovaAiPath_Deployed = "../Nova AI";
       

       public const int MaxWeaponRange = 10;

       public const double GravityMinimum = 0; // FIXME (priority 3) - Stars! gravity range is 0.2 - 6.0 with 1.0 in the middle!
       public const double GravityMaximum = 8;
       public const double RadiationMinimum = 0;
       public const double RadiationMaximum = 100;
       public const double TemperatureMinimum = -200;
       public const double TemperatureMaximum = 200;

       /// <summary>
       /// Do some common setup work for creating a new xml document.
       /// </summary>
       /// <param name="xmldoc">An XmlDocument variable, may be null, which will be the new document.</param>
       /// <returns>An XmlElement that is the root node of xmldoc.</returns>
       public static XmlElement InitializeXmlDocument(XmlDocument xmldoc)
       {
           if (xmldoc == null) xmldoc = new XmlDocument();

           // Write down the XML declaration
           XmlDeclaration xmlDeclaration = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);

           // Create the root element
           XmlElement xmlRoot = xmldoc.CreateElement("ROOT");
           xmldoc.InsertBefore(xmlDeclaration, xmldoc.DocumentElement);
           xmldoc.AppendChild(xmlRoot);

           return xmlRoot;
       }

       /// <summary>Create an xml node for a save file.</summary>
       /// <param name="xmldoc">The XmlDocument data is being saved to.</param>
       /// <param name="parent">The element this data will be saved under.</param>
       /// <param name="tag">A name that describes the data, usually a variable name.</param>
       /// <param name="value">A String representation of the data, usually variable.ToString.</param>
       public static void SaveData(XmlDocument xmldoc, XmlElement parent, String tag, String value)
       {
           XmlElement xmlelData = xmldoc.CreateElement(tag);
           XmlText xmltxtData = xmldoc.CreateTextNode(value);
           xmlelData.AppendChild(xmltxtData);
           parent.AppendChild(xmlelData);
       }

       /// <summary>Derive a relative path from two absolute paths.</summary>
       /// <param name="baseDir">The path from which the relative path will start.</param>
       /// <param name="targetPath">The absolute or relative path to be converted to a relative path.</param>
       public static string EvaluateRelativePath(string baseDir, string targetPath)
       {
           Uri baseUrl = new Uri(AddDirSeparator(baseDir));
           Uri targetUri = new Uri(targetPath);
           Uri relativeUri = baseUrl.MakeRelativeUri(targetUri);
           string unescaped = Uri.UnescapeDataString(relativeUri.OriginalString);
           return unescaped.Replace('/', Path.DirectorySeparatorChar);
       }

       /// <summary>
       /// Add the local directory seperator character (\ or /) to a path, if required.
       /// </summary>
       /// <param name="path">A file path</param>
       /// <returns>path + the (local) directory seperater character added to the end, if required.</returns>
       private static string AddDirSeparator(string path)
       {
           if (!path.EndsWith(Path.DirectorySeparatorChar + ""))
           {
               return path + Path.DirectorySeparatorChar;
           }
           return path;
       }

   }
}

