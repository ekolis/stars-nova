#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// This module maintains a (singleton) list of all components.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using Nova.Common;
#endregion

namespace Nova.Common.Components
{
    /// <summary>
    /// Provides singleton access (via AllComponents.Data) to a <see cref="Hashtable"/> containing all <see cref="Component"/>s indexed on the component's name.
    /// </summary>
    [Serializable]
    public sealed class AllComponents
    {
        #region Singleton Setup

        private static readonly object Padlock = new object();
        private static AllComponents instance;

        // ============================================================================
        // All component data
        // ============================================================================

        public Hashtable Components = new Hashtable();


        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static string saveFilePath;
        private static string graphicsFilePath;

        /// <summary>
        /// Prevents a default instance of the AllComponents class from being created.
        /// </summary>
        private AllComponents()
        {
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a mechanism of accessing the single instance of this class that we
        /// will create locally. Creation of the data is thread-safe.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static AllComponents Data
        {
            get
            {
                if (instance == null)
                {
                    lock (Padlock)
                    {
                        if (instance == null)
                        {
                            instance = new AllComponents();
                        }
                    }
                }
                return instance;
            }

            // ----------------------------------------------------------------------------

            set
            {
                instance = value;
            }
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if AllComponents contains a particular Component.
        /// </summary>
        /// <param name="componentName">The Name of the Component to look for.</param>
        /// <returns>True if the component is included.</returns>
        /// ----------------------------------------------------------------------------
        public bool Contains(string componentName)
        {
            return Data.Components.ContainsKey(componentName);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if AllComponents contains a particular Component.
        /// </summary>
        /// <param name="component">The Component to look for.</param>
        /// <returns>True if the component is included.</returns>
        /// ----------------------------------------------------------------------------
        public bool Contains(Component component)
        {
            return Data.Components.ContainsValue(component);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Restore the component definitions.
        /// </summary>
        /// <exception cref="System.Data.OperationAbortedException">
        /// The loading of the component definition was aborted.
        /// </exception>
        /// ----------------------------------------------------------------------------
        public static void Restore()
        {
            // Ensure we have the component definition file before starting the worker thread, or die.
            if (String.IsNullOrEmpty(saveFilePath))
            {
                saveFilePath = FileSearcher.GetComponentFile();
                if (String.IsNullOrEmpty(saveFilePath))
                {
                    Report.FatalError("Unable to locate component definition file.");
                }
            }
            ProgressDialog progress = new ProgressDialog();
            progress.Text = "Work";
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(AllComponents.Data.LoadComponents), progress);
            progress.ShowDialog();
            if (!progress.Success)
            {
                throw new System.Exception();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Start a new component definition set. This simply wipes all components from
        /// the in memory component definitions.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void MakeNew()
        {
            lock (Padlock)
            {
                if (instance == null)
                {
                    instance = new AllComponents();
                }
            }
            AllComponents.instance.Components = new Hashtable();
            using (Config conf = new Config())
            {
                conf.Remove(Global.ComponentFileName);
            }
            saveFilePath = null;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load all the components form the component definition file, nominally components.xml.
        /// </summary>
        /// <param name="status">An <see cref="IProgressCallback"/> used for updating the progress dialog.</param>
        /// <remarks>
        /// This is run in a worker thread and therefore has no direct access to the UI/user.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void LoadComponents(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            try
            {

                // blank the component data
                Data = new AllComponents();

                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(saveFilePath);

                XmlNode xmlnode = xmldoc.DocumentElement;

                int nodesLoaded = 0;
                while (xmlnode != null)
                {

                    // Report.Information("node name = '" + xmlnode.Name + "'");
                    if (xmlnode.Name == "ROOT")
                    {
                        callback.Begin(0, xmlnode.ChildNodes.Count);

                        xmlnode = xmlnode.FirstChild;
                    }
                    else if (xmlnode.Name == "Component")
                    {
                        ++nodesLoaded;
                        callback.SetText(String.Format("Loading component: {0}", nodesLoaded));
                        callback.StepTo(nodesLoaded);
                        Component newComponent = new Component(xmlnode);
                        AllComponents.Data.Components[newComponent.Name] = newComponent;
                        xmlnode = xmlnode.NextSibling;
                    }
                    else
                    {
                        xmlnode = xmlnode.NextSibling;
                    }

                    // check for user Cancel
                    if (callback.IsAborting)
                    {
                        return;
                    }

                }
                callback.Success = true;

            }
            catch (System.Threading.ThreadAbortException)
            {
                // We want to exit gracefully here (if we're lucky)
                Report.Error("AllComponents: LoadComponents() - Thread Abort Exception.");
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                // And here, if we can
                Report.Error("AllComponents: LoadComponents() - Thread Interrupted Exception.");
            }

            catch (Exception e)
            {
                Report.Error("Failed to load file: \r\n" + e.Message);
            }

            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary> Save the component data. </summary>
        /// ----------------------------------------------------------------------------
        public static bool Save()
        {
            try
            {
                // Setup the save location and stream.
                FileStream saveFile = new FileStream(ComponentFile, FileMode.Create);

                // Setup the XML document
                XmlDocument xmldoc = new XmlDocument();
                Global.InitializeXmlDocument(xmldoc);

                // add the components to the document
                foreach (Component thing in AllComponents.Data.Components.Values)
                {
                    xmldoc.ChildNodes.Item(1).AppendChild(thing.ToXml(xmldoc));
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

        } // Save

        #endregion

        #region File Paths

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the path where the graphics files are stored.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static string Graphics
        {
            get
            {
                if (!Directory.Exists(graphicsFilePath))
                {
                    graphicsFilePath = FileSearcher.GetGraphicsPath();
                    if (!String.IsNullOrEmpty(graphicsFilePath))
                    {
                        using (Config conf = new Config())
                        {
                            conf[Global.GraphicsFolderKey] = graphicsFilePath;
                        }
                    }
                }
                return graphicsFilePath;
            }
        }

        /// <summary>
        /// Path and file name of the component definition file, automatically located and persisted, or null.
        /// </summary>
        public static string ComponentFile
        {
            get
            {
                if (!Directory.Exists(saveFilePath))
                {

                    saveFilePath = FileSearcher.GetComponentFile();
                    if (!String.IsNullOrEmpty(saveFilePath))
                    {
                        using (Config conf = new Config())
                        {
                            conf[Global.ComponentFileName] = saveFilePath;
                        }
                    }
                }
                return saveFilePath;
            }
        }

        #endregion
    }
}
