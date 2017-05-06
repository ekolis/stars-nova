#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.Common.Components
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// Provides access to a <see cref="ConcurrentDictionary"/>
    /// containing all <see cref="Component"/>s indexed by their names.
    /// </summary>
    public sealed class AllComponents
    {
        // Contains all components
        private static ConcurrentDictionary<string, Component> components = new ConcurrentDictionary<string, Component>();

        // Data private to this module.
        private static string saveFilePath;
        private static string graphicsFilePath;
        private static bool isLoaded = false;
        
        /// <summary>
        /// Returns an IDictionary (Compatible with Dictionary and ConcurrentDictionary)
        /// containing all game components.
        /// </summary>
        public IDictionary<string, Component> GetAll
        {
            get
            {
                return components;
            }
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="restore">If true (by default) it will also restore
        /// all components on creation.</param>
        public AllComponents(bool restore = true)
        {
            if (restore)
            {
                Restore();
            }
        }

        
        /// <summary>
        /// Check if AllComponents contains a particular Component.
        /// </summary>
        /// <param name="componentName">The Name of the Component to look for.</param>
        /// <returns>True if the component is included.</returns>
        public bool Contains(string componentName)
        {
            return components.ContainsKey(componentName);
        }

        
        /// <summary>
        /// Check if AllComponents contains a particular Component.
        /// </summary>
        /// <param name="component">The Component to look for.</param>
        /// <returns>True if the component is included.</returns>
        public bool Contains(Component component)
        {
            return Contains(component.Name);
        }

        
        /// <summary>
        /// Returns a new instance of the requested component.
        /// </summary>
        /// <param name="componentName">The desired Component's name.</param>
        /// <returns>The new requested Component, or null.</returns>
        public Component Fetch(string componentName)
        {
            if (Contains(componentName))
            {
                return new Component(components[componentName]);
            }
            
            return null;
        }
        
        
        /// <summary>
        /// Removes and returns a Component.
        /// </summary>
        /// <param name="componentName">Component name to remove.</param>
        /// <returns>The removed Component, or null.</returns>
        public Component Remove(string componentName)
        {
            Component removed = null;
            components.TryRemove(componentName, out removed);
            
            return removed;
        }
        
        
        /// <summary>
        /// Start a new component definition set. This simply wipes all components from
        /// the in memory component definitions.
        /// </summary>
        public void MakeNew()
        {
            components = new ConcurrentDictionary<string, Component>();
            
            using (Config conf = new Config())
            {
                conf.Remove(Global.ComponentFileName);
            }
            
            saveFilePath = null;
            isLoaded = false;
        }
        
        
        /// <summary>
        /// Restore the component definitions.
        /// </summary>
        /// <exception cref="System.Data.OperationAbortedException">
        /// The loading of the component definition was aborted.
        /// </exception>
        public void Restore()
        {
            // If components are already loaded, GO AWAY DAMN DIALOG -Aeglos 25 Jun 11
            if (isLoaded)
            {
                return;
            }
            
            // Ensure we have the component definition file before starting the worker thread, or die.
            if (string.IsNullOrEmpty(saveFilePath))
            {
                saveFilePath = FileSearcher.GetComponentFile();
                if (string.IsNullOrEmpty(saveFilePath))
                {
                    Report.FatalError("Unable to locate component definition file.");
                }
            }
            else
            {
                // Report.Debug("Components file to be loaded: \"" + saveFilePath + "\"");
            }
            
            ProgressDialog progress = new ProgressDialog();
            progress.Text = "Loading Components";
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadComponents), progress);
            progress.ShowDialog();
            
            if (!progress.Success)
            {
                Report.FatalError("Failed to load component file: ProgressDialog returned false.");
                throw new System.Exception();
            }
            isLoaded = true;
        }


        /// <summary>
        /// Load all the components form the component definition file, nominally components.xml.
        /// </summary>
        /// <param name="status">An <see cref="IProgressCallback"/> used for updating the progress dialog.</param>
        /// <remarks>
        /// This is run in a worker thread and therefore has no direct access to the UI/user.
        /// </remarks>
        private void LoadComponents(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            // blank the component data
            components = new ConcurrentDictionary<string, Component>();
                
            XmlDocument xmldoc = new XmlDocument();
            bool waitForFile = false;
            double waitTime = 0; // seconds
            do
            {
                try
                {
                    using (FileStream componentFileStream = new FileStream(saveFilePath, FileMode.Open, FileAccess.Read))
                    {
                        xmldoc.Load(componentFileStream);

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
                                callback.SetText(string.Format("Loading component: {0}", nodesLoaded));
                                callback.StepTo(nodesLoaded);
                                Component newComponent = new Component(xmlnode);
                                components[newComponent.Name] = newComponent;
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
                    }
                    waitForFile = false;
                   
                    callback.Success = true;
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
            } while (waitForFile);
        }

        
        /// <summary>
        /// Save the component data.
        /// </summary>
        public bool Save()
        {
            bool waitForFile = false;
            double waitTime = 0.0; // seconds
            do
            {
                try
                {
                    // Setup the save location and stream.
                    using (FileStream saveFile = new FileStream(ComponentFile, FileMode.Create))
                    {

                        // Setup the XML document
                        XmlDocument xmldoc = new XmlDocument();
                        Global.InitializeXmlDocument(xmldoc);

                        // add the components to the document
                        foreach (Component thing in components.Values)
                        {
                            xmldoc.ChildNodes.Item(1).AppendChild(thing.ToXml(xmldoc));
                        }

                        xmldoc.Save(saveFile);
                    }

                    Report.Information("Component data has been saved to " + saveFilePath);
                    waitForFile = false;
                }
                catch (System.IO.FileNotFoundException)
                {
                    Report.Error("Error: File path not specified.");
                    return false;
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
                catch (Exception e)
                {
                    Report.Error("Error: Failed to save component definition file. " + e.Message);
                    return false;
                }
            } while (waitForFile);

            return true;
        }


        /// <summary>
        /// Get the path where the graphics files are stored.
        /// </summary>
        public string Graphics
        {
            get
            {
                if (!Directory.Exists(graphicsFilePath))
                {
                    graphicsFilePath = FileSearcher.GetGraphicsPath();
                    if (!string.IsNullOrEmpty(graphicsFilePath))
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
        public string ComponentFile
        {
            get
            {
                if (!Directory.Exists(saveFilePath))
                {
                    saveFilePath = FileSearcher.GetComponentFile();
                    if (!string.IsNullOrEmpty(saveFilePath))
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
    }
}
