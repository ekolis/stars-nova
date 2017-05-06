#region Copyright Notice
// ============================================================================
// Copyright (C) 2010-2012 The Stars-Nova Project
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


namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Schema;
    using System.Xml.Serialization; 

    /// <summary>
    /// Class to manage the application configuration settings. 
    /// </summary>
    [Serializable]
    public sealed class Config : IDisposable, IXmlSerializable
    {
        private readonly Dictionary<string, string> settings = new Dictionary<string, string>(); // string key, string value
        private bool initialized;

        /// <summary>
        /// Initializes a new instance of the Config class.
        /// </summary>
        public Config()
        {
        }

        /// <summary>
        /// Restore the configuration settings from nova.conf.
        /// </summary>
        /// <remarks>
        /// If there is no configuration file yet it will be created when the
        /// settings are saved for the first time.
        /// </remarks>
        public void Restore()
        {
            string fileName = FileSearcher.GetConfigFile();

            if (File.Exists(fileName))
            {
                bool waitForFile = false;
                double waitTime = 0; // seconds
                do
                {
                    try
                    {
                        using (FileStream confFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            // Data = Serializer.Deserialize(state) as GameSettings;
                            XmlSerializer s = new XmlSerializer(typeof(Config));
                            Config data = new Config();
                            data = (Config)s.Deserialize(confFile);

                            foreach (KeyValuePair<string, string> setting in data.settings)
                            {
                                if (this.settings.ContainsKey(setting.Key))
                                {
                                    this.settings[setting.Key] = setting.Value.ToString();
                                }
                                else
                                {
                                    this.settings.Add(setting.Key, setting.Value);
                                }
                            }
                            waitForFile = false;
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        // IOException. Is the file locked? Try waiting.
                        if (waitTime < Global.TotalFileWaitTime)
                        {
                            waitForFile = true;
                            System.Threading.Thread.Sleep(Global.FileWaitRetryTime); 
                            waitTime += 0.1;
                        }
                        else
                        {
                            // Give up, maybe something else is wrong?
                            throw; 
                        }
                    }
                } while (waitForFile);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Save the console persistent data.
        /// </summary>
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

            bool waitForFile = false;
            double waitTime = 0.0; // seconds
            do
            {
                try
                {
                    using (Stream stream = new FileStream(fileName, FileMode.Create))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(Config));
                        s.Serialize(stream, this);
                    }
                    waitForFile = false;
                }
                catch (System.IO.IOException)
                {
                    // IOException. Is the file locked? Try waiting.
                    if (waitTime < Global.TotalFileWaitTime)
                    {
                        waitForFile = true;
                        System.Threading.Thread.Sleep(Global.FileWaitRetryTime);
                        waitTime += 0.1;
                    }
                    else
                    {
                        // Give up, maybe something else is wrong?
                        throw;
                    }
                }
            } while (waitForFile);
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
                if (!this.initialized)
                {
                    Restore();
                }

                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                string setting;
                settings.TryGetValue(key, out setting);

                return setting;
            }

            set
            {
                if (!this.initialized)
                {
                    Restore();
                }

                if (string.IsNullOrEmpty(key))
                {
                    return;
                }
                this.settings[key] = value;
            }
        }

        /// <summary>
        /// Remove a setting from the config file.
        /// </summary>
        /// <param name="setting">The key of the setting to remove.</param>
        public void Remove(string setting)
        {
            if (!this.initialized)
            {
                Restore();
            }
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

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (KeyValuePair<string, string> setting in this.settings)
            {
                writer.WriteStartElement("Setting");
                writer.WriteElementString("Key", setting.Key);
                writer.WriteElementString("Value", setting.Value);
                writer.WriteEndElement();
            }
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();
            if (wasEmpty)
            {
                return;
            }

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
    }
}
