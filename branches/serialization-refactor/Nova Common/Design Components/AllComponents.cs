// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module maintains a (singleton) list of all components.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Win32;
using NovaCommon;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System;
using System.Xml;
using System.Windows.Forms;



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
      private static String SaveFilePath           = null;
      private static String GraphicsFilePath       = null;
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

      private void DoSomeWork(object status)
      {
          IProgressCallback callback = status as IProgressCallback;


          try
          {



              // blank the component data
              AllComponents.Data = new AllComponents();

              XmlDocument xmldoc = new XmlDocument();
              FileStream fileStream = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
              GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);


              xmldoc.Load(SaveFilePath);  // uncompressed
              //xmldoc.Load(compressionStream); // compressed

              XmlNode xmlnode = (XmlNode)xmldoc.DocumentElement;



              while (xmlnode != null)
              {
                  int nodesLoaded = 0;

                  if (callback.IsAborting)
                  {
                      return;
                  }


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

                      //System.Threading.Thread.Sleep(10);
                  }
                  else
                  {
                      xmlnode = xmlnode.NextSibling;
                  }

                  if (callback.IsAborting)
                  {
                      return;
                  }

                  //result = true; // looks like we made it
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
      }

// ============================================================================
// Restore the data. 
// ============================================================================

      public static bool Restore()
      {
          bool result = false;
          GetPath();
          if (SaveFilePath == null || SaveFilePath == "?" || SaveFilePath == "")
          {
              return false;
          }
          else
          {
              
              ProgressDialog progress = new ProgressDialog();
              progress.Text = "Work";
              System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(AllComponents.Data.DoSomeWork), progress);
              progress.ShowDialog();
             

              //return result;
              return true; //FIXME need to get a result for the worker thread.
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
              FileStream saveFile = new FileStream(SaveFilePath, FileMode.Create);
              GZipStream compressionStream = new GZipStream(saveFile, CompressionMode.Compress);

              ComponentList components = new ComponentList(Data.Components.Values);
              XmlSerializer serializer = new XmlSerializer(typeof(ComponentList));
              serializer.Serialize(saveFile, components);
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

       public class ComponentList : IXmlSerializable
       {
           private readonly IEnumerable components;

           public ComponentList(IEnumerable components)
           {
               this.components = components;
           }

           public XmlSchema GetSchema()
           {
               return null;
           }

           public void ReadXml(XmlReader reader)
           {
               throw new NotImplementedException(); // TODO XML deserialization of ComponentList
           }

           public void WriteXml(XmlWriter writer)
           {
               writer.WriteStartElement("", "ROOT", "");

               // add the components to the document
               foreach (Component thing in components)
               {
                   thing.WriteXml(writer);
               }
               writer.WriteEndElement();
           }
       }

// Save


// ============================================================================
// Reset the location of the component definition file.
// equivelent to ComponentFile = ""
// ============================================================================
      public static void ResetPath()
      {
         SaveFilePath = "";
         RegistryKey regKey = Registry.CurrentUser;
         RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
         subKey.SetValue(Global.ComponentFolderKey, "");
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

// ============================================================================
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
// Extract the path to the component file from the registry, failing that
// from the user and failing that terminate the program.
// FIXME - doesn't actually check that the user specified a valid file, which 
// may cause an unexpected termination.
// ============================================================================
      public static void GetPathOrDie()
      {
          RegistryKey regKey = Registry.CurrentUser;
          RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
          SaveFilePath = subKey.GetValue
                                  (Global.ComponentFolderKey, "?").ToString();

          bool askForFolder = false;

          if (SaveFilePath == "?" || SaveFilePath == "")
          {
              askForFolder = true;
          }
          else if (File.Exists(SaveFilePath) == false)
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

              SaveFilePath = Path.Combine(folderDialog.SelectedPath, "components.xml");
              subKey.SetValue(Global.ComponentFolderKey, SaveFilePath);
          }

      }//GetPathOrDie

// ============================================================================
// Access to the path where the component data is stored.
// ============================================================================
      public static string ComponentFile
      {
          get { GetPath(); return SaveFilePath; }

          // ----------------------------------------------------------------------------

          set
          {
              SaveFilePath = value;
              RegistryKey key = Registry.CurrentUser;
              RegistryKey subKey = key.CreateSubKey(Global.RootRegistryKey);
              subKey.SetValue(Global.ComponentFolderKey, SaveFilePath);
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

          return GraphicsFilePath;
      }// GetNewGraphicsPath

// ============================================================================
// Extract the path to the component graphics from the registry
// ============================================================================
      public static string GetGraphicsPath()
      {
          if (DisableComponentGraphics) return "";

          RegistryKey regKey = Registry.CurrentUser;
          RegistryKey subKey = regKey.CreateSubKey(Global.RootRegistryKey);
          GraphicsFilePath = subKey.GetValue
                                  (Global.GraphicsFolderKey, "?").ToString();

          if (GraphicsFilePath == "?" || GraphicsFilePath == "")
          {
              GraphicsFilePath = GetNewGraphicsPath();
              if (GraphicsFilePath == "?" || GraphicsFilePath == "")
              {
                  // In case we are in a loop loading lots of component images, don't keep trying endlessly.
                  Report.Error("Unable to locate component graphics. All component graphics will be dissabled.");
                  DisableComponentGraphics = true;
              }
          }

          return GraphicsFilePath;

      }//GetPath

// ============================================================================
// Access to the path where the graphics files are stored.
// ============================================================================
      public static string Graphics
      {
          get { GetGraphicsPath(); return GraphicsFilePath; }

          // ----------------------------------------------------------------------------

          set
          {
              GraphicsFilePath = value;
              RegistryKey key = Registry.CurrentUser;
              RegistryKey subKey = key.CreateSubKey(Global.RootRegistryKey);
              subKey.SetValue(Global.GraphicsFolderKey, GraphicsFilePath);
              // Report.Debug("Registry key set to: " + SaveFilePath);
          }
      }
   }
}
