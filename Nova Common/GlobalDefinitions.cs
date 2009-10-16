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
      public const string RootRegistryKey    = "Software\\Ken Reed\\Nova";
      public const string NovaFolderKey      = "NaveFolderKey";
      public const string ComponentFolderKey = "ComponentFolder";
      public const string GraphicsFolderKey  = "GraphicsFolder";
      public const string ClientFolderKey    = "GameFilesFolder";
      public const string ServerFolderKey    = "ConsoleGameFolder";
      public const string RaceFolderKey      = "RaceFolder";
       

      public const int    UniverseSize       =   400;
      public const int    NumberOfStars      =   50;
      public const int    MaxWeaponRange     =   10;

      public const double GravityMinimum     =    0;
      public const double GravityMaximum     =    8;
      public const double RadiationMinimum   =    0;
      public const double RadiationMaximum   =  100;
      public const double TemperatureMinimum = -200;
      public const double TemperatureMaximum =  200;


      // ============================================================================
      /// <summary>Create an xml node for a save file.</summary>
      /// <param name="xmldoc">The XmlDocument data is being saved to.</param>
      /// <param name="parent">The element this data will be saved under.</param>
      /// <param name="tag">A name that describes the data, usually a variable name.</param>
      /// <param name="value">A String representation of the data, usually variable.ToString.</param>
      // ============================================================================
      public static void SaveData(XmlDocument xmldoc, XmlElement parent, String tag, String value)
      {
          XmlElement xmlelData = xmldoc.CreateElement(tag);
          XmlText xmltxtData = xmldoc.CreateTextNode(value);
          xmlelData.AppendChild(xmltxtData);
          parent.AppendChild(xmlelData);
      }

      // ============================================================================
      /// <summary>Derive a relative path from two absolute paths.</summary>
      /// <param name="baseDir">The path from which the relative path will start.</param>
      /// <param name="targetPath">The absolute or relative path to be converted to a relative path.</param>
      // ============================================================================
      public static string EvaluateRelativePath(string baseDir, string targetPath)
      {
          Uri baseUrl = new Uri(AddDirSeparator(baseDir));
          Uri targetUri = new Uri(targetPath);
          Uri relativeUri = baseUrl.MakeRelativeUri(targetUri);
          string unescaped = Uri.UnescapeDataString(relativeUri.OriginalString);
          return unescaped.Replace('/', Path.DirectorySeparatorChar);
      }

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

