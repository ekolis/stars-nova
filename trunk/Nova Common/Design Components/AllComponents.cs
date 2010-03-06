// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module maintains a (singleton) list of all components.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

#region Using Statements
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using NovaCommon;
#endregion


// ============================================================================
// Manipulation of data that is persistent across muliple invocations of the
// GUI.
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public sealed class AllComponents
   {


// ============================================================================
// All component data
// ============================================================================

      public Hashtable Components = new Hashtable();


// ============================================================================
// Data private to this module.
// ============================================================================

      private static AllComponents   Instance      = null;
      private static Object          Padlock       = new Object();
      private static String         saveFilePath;
      private static String         graphicsFilePath;
      private static bool DisableComponentGraphics = false; // if we can't find them


// ============================================================================
// Private constructor to prevent anyone else creating instances of this class.
// ============================================================================

      private AllComponents() 
      {
      }


// ============================================================================
// Provide a mechanism of accessing the single instance of this class that we
// will create locally. Creation of the data is thread-safe.
// ============================================================================

      public static AllComponents Data
      {
         get {
            if (Instance == null) {
               lock(Padlock) {
                  if (Instance == null) {
                     Instance = new AllComponents();
                  }
               }
            }
            return Instance;
         }

// ----------------------------------------------------------------------------

         set {
            Instance = value;
         }
      }



      /// <summary>
      /// Check if AllComponents contains a particular Component.
      /// </summary>
      /// <param name="ComponentName">The Name of the Component to look for.</param>
      /// <returns></returns>
      public bool Contains(String ComponentName)
      {
          return Data.Components.ContainsKey(ComponentName);
      }

      /// <summary>
      /// Check if AllComponents contains a particular Component.
      /// </summary>
      /// <param name="Component">The Component to look for.</param>
      /// <returns></returns>
      public bool Contains(Component component)
      {
          return Data.Components.ContainsValue(component);
      }

      private void LoadComponents(object status)
      {
          IProgressCallback callback = status as IProgressCallback;

          try
          {

              // blank the component data
              AllComponents.Data = new AllComponents();

              XmlDocument xmldoc = new XmlDocument();
              FileStream fileStream = new FileStream(saveFilePath, FileMode.Open, FileAccess.Read);
              GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);


              xmldoc.Load(saveFilePath);  // uncompressed
              //xmldoc.Load(compressionStream); // compressed

              XmlNode xmlnode = (XmlNode)xmldoc.DocumentElement;

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
                      NovaCommon.Component newComponent = new Component(xmlnode);
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

              }//while loading nodes
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

// ============================================================================
// Restore the data. 
       
// ============================================================================

        /// <summary>
        /// Restore the component definitions.
        /// </summary>
        /// <exception cref="System.Data.OperationAbortedException">
        /// The loading of the component definition was aborted.
        /// </exception>
        public static void Restore()
        {
            GetPath();
            if( saveFilePath == null || saveFilePath == "?" || saveFilePath == "" )
            {
                throw new Exception();
            }


            ProgressDialog progress = new ProgressDialog();
            progress.Text = "Work";
            System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( AllComponents.Data.LoadComponents ), progress );
            progress.ShowDialog();
            if( !progress.Success )
            {
                throw new System.Exception();
            }
        }




      // ============================================================================
      /// <summary> Save the component data. </summary>
      // ============================================================================
      public static bool Save()
      {
          try
          {
              // Setup the save location, stream and compression.
              if (GetPath() == null && GetNewSaveFile() == null)
              {
                  throw (new System.IO.FileNotFoundException());
              }
              FileStream saveFile = new FileStream(saveFilePath, FileMode.Create);
              GZipStream compressionStream = new GZipStream(saveFile, CompressionMode.Compress);

              // Setup the XML document
              XmlDocument xmldoc = new XmlDocument();
              XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);

              // add the components to the document
              foreach (NovaCommon.Component thing in AllComponents.Data.Components.Values)
              {
                  xmldoc.ChildNodes.Item(1).AppendChild(thing.ToXml(xmldoc));
              }

              // You can comment/uncomment the following lines to turn compression on/off if you are doing a lot of 
              // manual inspection of the save file. Generally though it can be opened by any archiving tool that
              // reads gzip format.
              // xmldoc.Save(compressionStream); compressionStream.Close();    //   compressed 
                                                                               //       or
              xmldoc.Save(saveFile);                                           //  not compressed

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

        //-------------------------------------------------------------------
        /// <summary>
        /// Reset the location of the component definition file.
        /// </summary>
        /// <remarks>
        /// Equivelent to ComponentFile = "".
        /// </remarks>
        //-------------------------------------------------------------------
        public static void ResetPath()
        {
             saveFilePath = string.Empty;
             using( RegistryKey regKey = Registry.CurrentUser.CreateSubKey( Global.RootRegistryKey ) )
             {
                 regKey.SetValue( Global.ComponentFolderKey, string.Empty );
             }
        }

// ============================================================================
// Start a new component definition set. This simply wipes all components from
// the in memory component definitions.
// ============================================================================
      public static void MakeNew()
      {
          // FIXME - Selecting File|New before loading an existing component definition file caused a null reference exception. - Dan 28 Dec 09.
          AllComponents.Instance.Components = new Hashtable();
          ResetPath();
      }

// ============================================================================
// Ask the user for a location to save the file.
// ============================================================================
      public static string GetNewSaveFile()
      {
          SaveFileDialog fd = new SaveFileDialog();
          fd.Title = "Save component definition file";

          DialogResult result = fd.ShowDialog();

          // MessageBox.Show("Result " + result.ToString(System.Globalization.CultureInfo.InvariantCulture));

          if (result == DialogResult.OK && fd.FileName != null)
          {
              AllComponents.ComponentFile = fd.FileName;  // store FileName and set registry key
              Report.Debug("AllComponents.cs: GetNewSaveFile() - Saving to: " + AllComponents.ComponentFile);

              return fd.FileName;
          }
          return null;

      }// Get New Save File

        #region Methods
        //-------------------------------------------------------------------
        /// <summary>
        /// Extract the path to the component file from the registry.
        /// FIXME - The GUI and Console programs used this version but expect the GetPathOrDie() behaviour. Need to upadate their calls.
        /// </summary>
        //-------------------------------------------------------------------
        private static string GetPath()
        {
            using( RegistryKey regKey = Registry.CurrentUser.CreateSubKey( Global.RootRegistryKey ) )
            {
                saveFilePath = regKey.GetValue( Global.ComponentFolderKey, string.Empty ).ToString();
                if( 0 == saveFilePath.Length )
                {
                    saveFilePath = null;
                }
            }

            return saveFilePath;
        }
        #endregion

        // ============================================================================
// Extract the path to the component file from the registry, failing that
// from the user and failing that terminate the program.
// FIXME - doesn't actually check that the user specified a valid file, which 
// may cause an unexpected termination.
// ============================================================================
      public static void GetPathOrDie()
      {
          RegistryKey regKey = Registry.CurrentUser;
          RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
          saveFilePath = subKey.GetValue
                                  (Global.ComponentFolderKey, "?").ToString();

          bool askForFolder = false;

          if (saveFilePath == "?" || saveFilePath == "")
          {
              askForFolder = true;
          }
          else if (File.Exists(saveFilePath) == false)
          {
              askForFolder = true;
          }

          if (askForFolder)
          {
              FolderBrowserDialog folderDialog = new FolderBrowserDialog();
              folderDialog.Description =
              "Select the folder where the component definitions are located.";

              DialogResult result = folderDialog.ShowDialog();
              if (result == DialogResult.Cancel)
              {
                  Report.FatalError("You must specify a component folder.");
              }

              saveFilePath = Path.Combine(folderDialog.SelectedPath, "components.xml");
              subKey.SetValue(Global.ComponentFolderKey, saveFilePath);
          }

      }//GetPathOrDie

// ============================================================================
// Access to the path where the component data is stored.
// ============================================================================
      public static string ComponentFile
      {
          get { GetPath(); return saveFilePath; }

          // ----------------------------------------------------------------------------

          set
          {
              saveFilePath = value;
              RegistryKey key = Registry.CurrentUser;
              RegistryKey subKey = key.CreateSubKey(Global.RootRegistryKey);
              subKey.SetValue(Global.ComponentFolderKey, saveFilePath);
              // MessageBox.Show("Registry key set to: " + SaveFilePath);
          }
      }

      // ============================================================================
      // Ask the user for the graphics directory.
      // ============================================================================
      public static string GetNewGraphicsPath()
      {
          FolderBrowserDialog folderDialog = new FolderBrowserDialog();
          folderDialog.Description =
          @"Select the folder where the component graphics images are located (normally Nova\Graphics\)";

          DialogResult result = folderDialog.ShowDialog();
          if (result == DialogResult.Cancel)
          {
              Report.Error("Unable to load images.");
              Graphics = "";
              return "";
          }

          Graphics = folderDialog.SelectedPath;

          return graphicsFilePath;
      }// GetNewGraphicsPath

// ============================================================================
// Extract the path to the component graphics from the registry
// ============================================================================
      public static string GetGraphicsPath()
      {
          if (DisableComponentGraphics) return "";

          RegistryKey regKey = Registry.CurrentUser;
          RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
          graphicsFilePath = subKey.GetValue
                                  (Global.GraphicsFolderKey, "?").ToString();

          if (graphicsFilePath == "?" || graphicsFilePath == "")
          {
              graphicsFilePath = GetNewGraphicsPath();
              if (graphicsFilePath == "?" || graphicsFilePath == "")
              {
                  // In case we are in a loop loading lots of component images, don't keep trying endlessly.
                  Report.Error("Unable to locate component graphics. All component graphics will be dissabled.");
                  DisableComponentGraphics = true;
              }
          }

          return graphicsFilePath;

      }//GetPath

        #region Properties
       //-------------------------------------------------------------------
       /// <summary>
       /// Gets or sets the path where the graphics files are stored.
       /// </summary>
       //-------------------------------------------------------------------
       public static string Graphics
       {
           get
           {
               GetGraphicsPath();
               return graphicsFilePath;
           }
           private set
           {
               graphicsFilePath = value;
               using( RegistryKey key = Registry.CurrentUser.CreateSubKey( Global.RootRegistryKey ) )
               {
                   key.SetValue( Global.GraphicsFolderKey, graphicsFilePath );
               }
              // Report.Debug("Registry key set to: " + SaveFilePath);
           }
       }
        #endregion
   }
}
