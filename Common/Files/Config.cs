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

#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Serialization;


#endregion

namespace Nova.Common
{
    [Serializable]
    public sealed class Config : IDisposable, IXmlSerializable
    {
        #region Data

        private readonly Hashtable settings = new Hashtable(); // string key, string value
        private bool initialised;

        #endregion

        #region Construction

        /// <summary>
        /// Initialises a new instance of the Config class.
        /// </summary>
        public Config()
        {
        }

        #endregion

        #region Methods

        //-------------------------------------------------------------------
        /// <summary>
        /// Restore the configuration settings from nova.conf
        /// </summary>
        /// <remarks>
        /// If there is no configuration file yet it will be created when the
        /// settings are saved for the first time.
        /// </remarks>
        //-------------------------------------------------------------------
        public void Restore()
        {
            string fileName = FileSearcher.GetConfigFile();

            if (File.Exists(fileName))
            {
                using (FileStream confFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {

                    // Data = Serializer.Deserialize(state) as GameSettings;
                    XmlSerializer s = new XmlSerializer(typeof(Config));
                    Config data = new Config();
                    data = (Config)s.Deserialize(confFile);

                    foreach (DictionaryEntry setting in data.settings)
                    {
                        this.settings.Add(setting.Key, setting.Value);
                    }
                }
            }

            this.initialised = true;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        //-------------------------------------------------------------------
        public void Save()
        {
            string fileName = FileSearcher.GetConfigFile();

            if (fileName == null)
            {
                // TODO (priority 5) add the nicities. Update the config files location.
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Choose a location to save the nova.config file.";

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = fd.FileName;
                }
                else
                {
                    throw new System.IO.IOException("File dialog cancelled.");
                }

            }
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer s = new XmlSerializer(typeof(Config));
                s.Serialize(stream, this);
            }

        }

        /// <summary>
        /// Allow array type indexing of the Config.
        /// </summary>
        /// <param name="key">The particular setting to set or get.</param>
        /// <returns>The value of a setting.</returns>
        public string this[string key]
        {
            get
            {
                if (!this.initialised) 
                    Restore();

                if (String.IsNullOrEmpty(key)) 
                    return null;

                object setting = this.settings[key];
                if (setting == null) return null;
                return setting as string;
            }

            set
            {
                if (!this.initialised)
                    Restore();

                if (String.IsNullOrEmpty(key)) 
                    return;
                this.settings[key] = value;
            }
        }

        /// <summary>
        /// Remove a setting from the config file.
        /// </summary>
        /// <param name="setting">The key of the setting to remove.</param>
        public void Remove(string setting)
        {
            if (! this.initialised) Restore();
            this.settings.Remove(setting);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            if (this.settings.Count > 0)
            {
                Save();
            }
        }

        #endregion

        #region IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (DictionaryEntry setting in this.settings)
            {
                writer.WriteStartElement("Setting");
                writer.WriteElementString("Key", (string)setting.Key);
                writer.WriteElementString("Value", (string)setting.Value);
                writer.WriteEndElement();
            }
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();
            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement();

                string key = reader.ReadElementContentAsString();
                string value = reader.ReadElementContentAsString();

                this.settings.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        #endregion
    }
}
