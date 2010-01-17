// ============================================================================
// (c) Daniel Vale 2010
//
// This object is an example of how a single object that controls the conversion 
// of objects to/from Xml could be written. It starts with the code to write out
// the component definition file refactored from AllComponents, and includes
// the top level ToXml function in that chain (from the Component object). 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
using System;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NovaCommon
{
    static class XmlConversion
    {
        static String SaveFilePath;

        // a refactoring of AllComponents.Save
        public static bool WriteComponentDefinition()
        {
            try
            {
                // Setup the save location, stream and compression.
                if (GetPath() == null && GetNewSaveFile() == null)
                {
                    throw (new System.IO.FileNotFoundException());
                }
                FileStream saveFile = new FileStream(SaveFilePath, FileMode.Create);
                GZipStream compressionStream = new GZipStream(saveFile, CompressionMode.Compress);

                // Setup the XML document
                XmlDocument xmldoc;
                XmlNode xmlnode;
                XmlElement xmlRoot;
                xmldoc = new XmlDocument();
                // TODO (priority 4) - see if byte order marks emitted by .NET interfere with Mono parsing of XML files
                xmlnode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmldoc.AppendChild(xmlnode);
                xmlRoot = xmldoc.CreateElement("", "ROOT", "");
                xmldoc.AppendChild(xmlRoot);

                // add the components to the document
                foreach (NovaCommon.Component thing in AllComponents.Data.Components.Values)
                {
                    xmldoc.ChildNodes.Item(1).AppendChild(ComponentToXml(xmldoc, thing));
                }

                // You can comment/uncomment the following lines to turn compression on/off if you are doing a lot of 
                // manual inspection of the save file. Generally though it can be opened by any archiving tool that
                // reads gzip format.
                // xmldoc.Save(compressionStream); compressionStream.Close();    //   compressed 
                //       or
                xmldoc.Save(saveFile);                                           //  not compressed

                saveFile.Close();

                Report.Information("Component data has been saved to " + SaveFilePath);
                return true;
            }
            catch (System.IO.FileNotFoundException)
            {

                Report.Error("Error: File path not specified.");
                return false;

            }
            catch (Exception e)
            {
                Report.Error("Error: Failed to save component definition file. " + e.Message);
                return false;
            }
        }

        // ============================================================================
        // A refactoring of AllComponents.GetPath
        // Extract the path to the component file from the registry
        // FIXME - The GUI and Console programs used this version but expect the GetPathOrDie() behaviour. Need to upadate their calls.
        // ============================================================================
        public static string GetPath()
      {
          RegistryKey regKey = Registry.CurrentUser;
          RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
          SaveFilePath = subKey.GetValue
                                  (Global.ComponentFolderKey, "?").ToString();

          if (SaveFilePath == "?" || SaveFilePath == "")
          {
              SaveFilePath = null;
          }
          else if (File.Exists(SaveFilePath) == false)
          {
              SaveFilePath = null;
          }

          return SaveFilePath;
      }//GetPath



      // ============================================================================
      // A refactoring of AllComponents.GetNewSaveFile
      // Ask the user for a location to save the file.
      // ============================================================================
      public static string GetNewSaveFile()
      {
          SaveFileDialog fd = new SaveFileDialog();
          fd.Title = "Save component definition file";

          DialogResult result = fd.ShowDialog();

          if (result == DialogResult.OK && fd.FileName != null)
          {
              AllComponents.ComponentFile = fd.FileName;  // store FileName and set registry key
              Report.Debug("AllComponents.cs: GetNewSaveFile() - Saving to: " + AllComponents.ComponentFile);

              return fd.FileName;
          }
          return null;

      }// Get New Save File

      // The following is the first object in the component hirachy that would need to be
      // refactored. To have all xml writing placed here, all the ToXml functions for all 
      // objects need to be reafactored in this way.

      // A refactoring of Component.ToXml. Note the dependent object ToXml calls still 
      // need to be replaced with similar functions.
      /// <summary>
      /// Save: Return an XmlElement representation of a Component
      /// </summary>
      /// <param name="xmldoc">The parent XmlDocument</param>
      /// <param name="component">The Component to be converted to xml (saved)</param>
      /// <returns>An XmlElement representation of component.</returns>
      public static XmlElement ComponentToXml(XmlDocument xmldoc, Component component)
      {
          XmlElement xmlelComponent = xmldoc.CreateElement("Component");
          // Name
          XmlElement xmlelName = xmldoc.CreateElement("Name");
          XmlText xmltxtName = xmldoc.CreateTextNode(component.Name);
          xmlelName.AppendChild(xmltxtName);
          xmlelComponent.AppendChild(xmlelName);
          // Type
          XmlElement xmlelType = xmldoc.CreateElement("Type");
          XmlText xmltxtType = xmldoc.CreateTextNode(component.Type);
          xmlelType.AppendChild(xmltxtType);
          xmlelComponent.AppendChild(xmlelType);
          // Mass
          XmlElement xmlelMass = xmldoc.CreateElement("Mass");
          XmlText xmltxtMass = xmldoc.CreateTextNode(component.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));
          xmlelMass.AppendChild(xmltxtMass);
          xmlelComponent.AppendChild(xmlelMass);
          // Resource
          xmlelComponent.AppendChild(component.Cost.ToXml(xmldoc));
          // Tech
          xmlelComponent.AppendChild(component.RequiredTech.ToXml(xmldoc));
          // Description
          if (component.Description != null) Global.SaveData(xmldoc, xmlelComponent, "Description", component.Description);
          // Race Restrictions
          xmlelComponent.AppendChild(component.Restrictions.ToXml(xmldoc));
          // Image - convert the ImageFile to a relative path, so this program runs in other locations
          XmlElement xmlelImage = xmldoc.CreateElement("Image");
          // Paths are always stored in external files using forward slashes.
          XmlText xmltxtImage = xmldoc.CreateTextNode(Global.EvaluateRelativePath(AllComponents.Graphics, component.ImageFile).Replace(Path.DirectorySeparatorChar, '/'));
          xmlelImage.AppendChild(xmltxtImage);
          xmlelComponent.AppendChild(xmlelImage);

          // Properties
          foreach (String key in component.Properties.Keys)
          {
              XmlElement xmlelPropertyType = xmldoc.CreateElement("Type");
              XmlText xmltxtPropertyType = xmldoc.CreateTextNode(key);
              xmlelPropertyType.AppendChild(xmltxtPropertyType);

              XmlElement xmlelProperty = component.Properties[key].ToXml(xmldoc);
              xmlelProperty.AppendChild(xmlelPropertyType);

              xmlelComponent.AppendChild(xmlelProperty);
          }

          return xmlelComponent;
      } // ComponentToXml


      // Add the other 23 ToXml functions here...

    }// class XmlConversion
}// namespace NovaCommon
