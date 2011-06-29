#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
// This module holds definitions that are global across all Nova application
// programs. 
// ===========================================================================
#endregion

using System;
using System.IO;
using System.Xml;

namespace Nova.Common
{
   public static class Global
   {
       public const string NovaWebSite = "https://sourceforge.net/apps/mediawiki/stars-nova/index.php?title=Main_Page";

       // Note: Client/SeverState.GameFolder vs ServerFolder vs ClientFolder
       // In a single player game the ServerFolder == ClientFolder == "GameFiles"
       // The only reason to save seperate keys for ServerFolder and ClientFolder
       // is so that we can simulate a network game on a single PC. When this
       // is done it will be necessary to transfer the game files from one
       // folder to the other. In Stars! this is done manually. Nova may (eventually)
       // implement an automated solution (e.g. using TCP/IP sockets to conect the
       // client and server.)

       #region Nova Resources

       // These config file keys are used to loacte files and folders where Nova stores game data.
       #region Files and Folders
       public const string ComponentFileKey     = "ComponentFile"; // where components.xml is (possibly re-named), installation relative path is '.'. Should only change for modified game play.
       public const string GraphicsFolderKey    = "GraphicsFolder"; // where pictures are stored, installation relative path is './Graphics'. Should only be changed for modding the game interface.
       public const string ClientFolderKey      = "ClientFolder"; // where client side game files are, nominally './GameFiles'. Likely to be different for each active game.
       public const string ServerFolderKey      = "ServerFolder"; // where server side game files are, nominally './GameFiles'. May be a network path.
       public const string RaceFolderKey        = "RaceFolder";  // where player race files are stored, nominally './GameFiles'. Players may save races elswhere to create a reusable library.
       public const string ServerStateKey       = "ServerStateFile"; // the server's saved data from the most recent game, if any. Nominally ./GameFiles/Constole.state. Players may save game files anywhere, e.g. My Document\Saved Games\Nova\A Bare Foot Jay-Walk\
       public const string ClientStateKey       = "ClientStateFile"; // the client's saved data from the most recent game, if any. Nominally ./GameFiles/RaceName.state. Players may save game files anywhere, e.g. My Document\Saved Games\Nova\A Bare Foot Jay-Walk\
       public const string SettingsKey          = "GameSettingsFile"; // where the game settings are stored (on the server, but a copy should be avaialble to the client).
       #endregion Files and Folders

       #region File Extensions
       public const string ClientStateExtension = ".cstate";
       public const string ServerStateExtension = ".sstate";
       public const string OrdersExtension      = ".orders";
       public const string RaceExtension        = ".race";
       public const string IntelExtension       = ".intel";
       public const string SettingsExtension    = ".settings";
       #endregion

       // default folder and file names, used with FileSearcher.GetFolder(). 
       #region Default Folders
       // Follows the above naming conventions
       public const string NovaFolderName           = ".";
       public const string ComponentFolderName      = ".";
       public const string ComponentFileName        = "components.xml";
       public const string ConfigFileName           = "nova.conf";
       public const string GraphicsFolderName       = "Graphics";
       public const string ClientFolderName         = "GameFiles";
       public const string ServerFolderName         = "GameFiles";
       public static readonly string RaceFolderName = "DefaultRaces";
       #endregion Default Folders

       #endregion Nova Resources

       #region Numeric Constants

       // Colonists
       public const int     ColonistsPerKiloton                     = 100;
       public const double  LowStartingPopulationFactor             = 0.7;
       public const double  BaseCrowdingFactor                      = 16 / 9; // Taken from the Stars technical faq.
       public const int     StartingColonists                       = 25000;
       public const int     StartingColonistsAcceleratedBBS         = 100000;
       public const int     NominalMaximumPlanetaryPopulation       = 1000000; // use Race.MaxPopulation to get the maximum for a particular race.
       public const double  PopulationFactorHyperExpansion          = 0.5;
       public const double  GrowthFactorHyperExpansion              = 2;
       public const double  PopulationFactorJackOfAllTrades         = 1.2;
       public const double  PopulationFactorOnlyBasicRemoteMining   = 1.1;

       // Combat
       public const int MaxWeaponRange  = 10;
       public const int MaxDefenses     = 100;
       
       // Environment
       public const double GravityMinimum       = 0; // FIXME (priority 3) - Stars! gravity range is 0.2 - 6.0 with 1.0 in the middle! Will need to revise all current race builds once changed.
       public const double GravityMaximum       = 8;
       public const double RadiationMinimum     = 0;
       public const double RadiationMaximum     = 100;
       public const double TemperatureMinimum   = -200;
       public const double TemperatureMaximum   = 200;

       // Production constants
       public const int ColonistsPerOperableFactoryUnit     = 10000;
       public const int FactoriesPerFactoryProductionUnit   = 10;
       public const int ColonistsPerOperableMiningUnit      = 10000;
       public const int MinesPerMineProductionUnit          = 10;
        
       // Research constants
       public const int DefaultResearchPercentage = 10;

       // Format
       public const int ShipIconNumberingLength = 4;
        
       // Turn data
       public const int StartingYear = 2100;
       public const int DiscardFleetReportAge = 1;
       
       // Limits
       public const int MaxFleetAmount              = 512;
       public const int MaxDesignsAmount            = 16;
       public const int MaxStarbaseDesignsAmount    = 10;

       #endregion

       #region Methods

       #region Xml

       /// <summary>
       /// Do some common setup work for creating a new xml document.
       /// </summary>
       /// <param name="xmldoc">An XmlDocument variable, may be null, which will be the new document.</param>
       /// <returns>An XmlElement that is the root node of xmldoc.</returns>
       public static XmlElement InitializeXmlDocument(XmlDocument xmldoc)
       {
           if (xmldoc == null)
           {
               xmldoc = new XmlDocument();
           }

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
       public static void SaveData(XmlDocument xmldoc, XmlElement parent, string tag, string value)
       {
           XmlElement xmlelData = xmldoc.CreateElement(tag);
           XmlText xmltxtData = xmldoc.CreateTextNode(value);
           xmlelData.AppendChild(xmltxtData);
           parent.AppendChild(xmlelData);
       }

       public static void SaveData(XmlDocument xmldoc, XmlElement parent, string tag, double value)
       {
           Global.SaveData(xmldoc, parent, tag, value.ToString(System.Globalization.CultureInfo.InvariantCulture));
       }

       public static double ParseDoubleSubnode(XmlNode node, string tag)
       {
           XmlNode subnode = (XmlText)node.SelectSingleNode("descendant::" + tag).FirstChild;
           return double.Parse(subnode.Value, System.Globalization.CultureInfo.InvariantCulture);
       }

       #endregion Xml

       #region Paths

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

       #endregion Paths

       #endregion Methods
   }
}

