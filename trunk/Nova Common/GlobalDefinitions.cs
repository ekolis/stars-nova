// This file needs -*- c++ -*- mode
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
   public class Global
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
      /// <param name="mainDirPath">The path from which the relative path will start.</param>
      /// <param name="absoluteFilePath">The absolute path to be converted to a relative path.</param>
      // Based on code posted by Marcin Grzębski
      // http://bytes.com/groups/net-c/260727-absolute-path-relative-path-conversion
      // ============================================================================
        public static string EvaluateRelativePath(string mainDirPath, string absoluteFilePath) 
        {
            if (mainDirPath == null) return absoluteFilePath;
            if (absoluteFilePath == null) return null;

            string[] firstPathParts=mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] secondPathParts=absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar );
            int sameCounter=0;
            
            for(int i=0; i<Math.Min(firstPathParts.Length, secondPathParts.Length); i++) 
            {
                if(!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()) ) 
                {
                    break;
                }
                sameCounter++;
            }
            if( sameCounter==0 ) 
            {
                return absoluteFilePath;
            }

            string newPath=String.Empty;
            for(int i=sameCounter; i<firstPathParts.Length; i++) 
            {
                if( i>sameCounter ) 
                {
                    newPath+=Path.DirectorySeparatorChar;
                }
                newPath+="..";
            }
            if( newPath.Length==0 ) 
            {
                //newPath="."; // don't want this bit
            }
            for(int i=sameCounter; i<secondPathParts.Length; i++) 
            {
                newPath+=Path.DirectorySeparatorChar;
                newPath+=secondPathParts[i];
            }

            return newPath;
        }


   }
}

