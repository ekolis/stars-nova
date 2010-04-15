#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
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
// This object is an example of how a single object that controls the conversion 
// of objects to/from Xml could be written. It starts with the code to write out
// the component definition file refactored from AllComponents, and includes
// the top level ToXml function in that chain (from the Component object). 
// The purpose of this example is to demonstrate why this is a bad idea.
// It is incomplete and there is probably no point in finishing it.
// ===========================================================================
#endregion

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
        #region Fields
        private static String saveFilePath;
        #endregion

        #region Methods
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
                FileStream saveFile = new FileStream(saveFilePath, FileMode.Create);

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

                xmldoc.Save(saveFile);
                saveFile.Close();

                Report.Information("Component data has been saved to " + saveFilePath);
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

        //-------------------------------------------------------------------
        /// <summary>
        /// Extract the path to the component file from the registry.
        /// </summary>
        /// <remarks>
        /// A refactoring of AllComponents.GetPath
        /// FIXME (priority 3) - The GUI and Console programs used this version but expect the GetPathOrDie() behaviour. Need to upadate their calls.
        /// </remarks>
        //-------------------------------------------------------------------
        public static string GetPath()
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(Global.RootRegistryKey))
            {
                object obj = regKey.GetValue(Global.ComponentFolderKey);
                if (null != obj)
                {
                    saveFilePath = obj.ToString();
                }
            }
            return saveFilePath;
        }



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
        #endregion

        // Add the other 23 ToXml functions here...

    }// class XmlConversion
}// namespace NovaCommon
