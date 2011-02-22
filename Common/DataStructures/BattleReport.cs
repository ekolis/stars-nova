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
// This module contains the definition of a report on a battle;
// ===========================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
#if (DEBUG)
    using System.IO;
    using System.Reflection;
    using System.Resources;
#endif    
    using System.Xml;

    using Nova.Common.DataStructures;

#if (DEBUG)
    using NUnit.Framework;
#endif

    [Serializable]
    public class BattleStep
    {
        public string Type = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStep()
        {
        }

        #region Save Load Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising constructor from an XmlNode representing the BattleStep (from a save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the BattleStep</param>
        /// ----------------------------------------------------------------------------
        public BattleStep(XmlNode node)
        {
            if (node == null)
            {
                Report.FatalError("BattleReport.cs: BattleStep(XmlNode node) - node is null - no Battle Step found.");
                return;
            }

            // A BattleStep should not be loaded directly but rather by calling the base constructor 
            // from one of the derived types.
            XmlNode battleStepNode = node.SelectSingleNode("BattleStep");
            if (battleStepNode == null)
            {
                Report.FatalError("BattleStep.cs: BattleStep(XmlNode node) - could not find BattleStep node, input file may be corrupt.");
                return;
            }


            XmlNode subnode = battleStepNode.FirstChild;

            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "type":
                            Type = ((XmlText)subnode.FirstChild).Value;
                            break;
                    }
                }

                catch (Exception e)
                {
                    Report.Error(e.Message + " \n Details: \n " + e.ToString());
                }

                subnode = subnode.NextSibling;
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Return an XmlElement representation of the <see cref="BattleStep"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the BattleStep</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStep = xmldoc.CreateElement("BattleStep");

            if (Type != null) Global.SaveData(xmldoc, xmlelBattleStep, "Type", Type);

            return xmlelBattleStep;
        }

        #endregion

    }

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// A class to record a new stack position.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class BattleStepMovement : BattleStep
    {
        
        public string StackName = null;
        public NovaPoint Position = new NovaPoint();

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public BattleStepMovement()
        {
            Type = "Movement";
        }


        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepTarget"/> XmlNode from a Nova save file (xml document). </param>
        /// ----------------------------------------------------------------------------
        public BattleStepMovement(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {

                        case "stackname":
                            StackName = subnode.FirstChild.Value;
                            break;

                        case "point":
                            Position = new NovaPoint(subnode);
                            break;

                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Movement : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the xmlelBattleStepMovement for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the xmlelBattleStepMovement</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepMovement = xmldoc.CreateElement("BattleStepMovement");

            xmlelBattleStepMovement.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepMovement, "StackName", StackName);
            xmlelBattleStepMovement.AppendChild(Position.ToXml(xmldoc));
            return xmlelBattleStepMovement;
        }

        #endregion
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// A class to record a new target.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class BattleStepTarget : BattleStep
    {

        public string TargetShip = null;

        public BattleStepTarget()
        {
            Type = "Target";
        }


        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepTarget"/> XmlNode from a Nova save file (xml document). </param>
        /// ----------------------------------------------------------------------------
        public BattleStepTarget(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {

                        case "targetship":
                            TargetShip = subnode.FirstChild.Value;
                            break;

                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Target : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the BattleStepTarget for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the BattleStepTarget</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepTarget = xmldoc.CreateElement("BattleStepTarget");

            xmlelBattleStepTarget.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepTarget, "TargetShip", TargetShip);

            return xmlelBattleStepTarget;
        }

        #endregion

    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// A class to destroy a ship in a given stack.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class BattleStepDestroy : BattleStep
    {

        public string ShipName = null; // ship in the real fleet
        public string StackName = null; // stack in the battle engine

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepDestroy()
        {
            Type = "Destroy";
        }


        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepDestroy"/> XmlNode from a Nova save file (xml document). </param>
        /// ----------------------------------------------------------------------------
        public BattleStepDestroy(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {

                        case "shipname":
                            ShipName = subnode.FirstChild.Value;
                            break;

                        case "stackname":
                            StackName = subnode.FirstChild.Value;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Destroy : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the BattleStepDestroy for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the BattleStepDestroy</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepDestroy = xmldoc.CreateElement("BattleStepDestroy");

            xmlelBattleStepDestroy.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepDestroy, "ShipName", ShipName);
            Global.SaveData(xmldoc, xmlelBattleStepDestroy, "StackName", StackName);

            return xmlelBattleStepDestroy;
        }

        #endregion
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// A class to record weapons being fired.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class BattleStepWeapons : BattleStep
    {
        
        public double HitPower = 0;
        public string Targeting = null;
        public BattleStepTarget WeaponTarget = new BattleStepTarget();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepWeapons()
        {
            Type = "Weapons";
        }


        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepWeapons"/> XmlNode from a Nova save file (xml document). </param>
        /// ----------------------------------------------------------------------------
        public BattleStepWeapons(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "type":
                            Type = subnode.FirstChild.Value;
                            break;

                        case "hitpower":
                            HitPower = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "targeting":
                            Targeting = subnode.FirstChild.Value;
                            break;

                        case "weapontarget":
                            WeaponTarget = new BattleStepTarget(subnode);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Weapons : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the BattleStepWeapons for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the ShipDesign</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepWeapons = xmldoc.CreateElement("BattleStepWeapons");

            xmlelBattleStepWeapons.AppendChild(base.ToXml(xmldoc));

            // hitpower
            Global.SaveData(xmldoc, xmlelBattleStepWeapons, "HitPower", HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // targeting
            if (Targeting != null) Global.SaveData(xmldoc, xmlelBattleStepWeapons, "Targeting", Targeting);

            // weapontarget
            xmlelBattleStepWeapons.AppendChild(WeaponTarget.ToXml(xmldoc));

            return xmlelBattleStepWeapons;
        }


        #endregion

    }


    [Serializable]
    public class BattleReport
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Main battle report components.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string Location  = null;
        public int SpaceSize    = 0;
        public int Year = 0;
        public string Key { get { return (Location == null) ? "" : Year.ToString() + Location; } }
        public ArrayList Steps  = new ArrayList();
        public Hashtable Stacks = new Hashtable();
        public Dictionary<string, int> Losses = new Dictionary<string, int>(); // raceName, lossCount
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleReport()
        {
        }

        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleReport"/> XmlNode from a Nova save file (xml document)</param>
        /// ----------------------------------------------------------------------------
        public BattleReport(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {

                    switch (subnode.Name.ToLower())
                    {
                        case "root":
                            subnode = subnode.FirstChild;
                            continue;
                        case "battlereport":
                            subnode = subnode.FirstChild;
                            continue;

                        case "location":
                            Location = subnode.FirstChild.Value;
                            break;

                        case "spacesize":
                            SpaceSize = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "year":
                            Year = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                             
                        case "battlesteptarget":
                            BattleStepTarget target = new BattleStepTarget(subnode);
                            Steps.Add(target);
                            break;

                        case "battlestepdestroy":
                            BattleStepDestroy destroy = new BattleStepDestroy(subnode);
                            Steps.Add(destroy);
                            break;

                        case "battlestepweapons":
                            BattleStepWeapons weapons = new BattleStepWeapons(subnode);
                            Steps.Add(weapons);
                            break;

                        case "battlestepmovement":
                            BattleStepMovement movement = new BattleStepMovement(subnode);
                            Steps.Add(movement);
                            break;

                        case "losses":
                            string raceName = subnode.SelectSingleNode("Race").FirstChild.Value;
                            int lossCount = int.Parse(subnode.SelectSingleNode("Count").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Losses.Add(raceName, lossCount);
                            break;

                        case "fleet": 
                            Fleet newStack = new Fleet(subnode);
                            Stacks.Add(newStack.Key, newStack);
                            break;


                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Report : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }
            
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the ShipDesign for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the BattleReport</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            
            XmlElement xmlelBattleReport = xmldoc.CreateElement("BattleReport");
            
            if (Location != null) Global.SaveData(xmldoc, xmlelBattleReport, "Location", Location);
            Global.SaveData(xmldoc, xmlelBattleReport, "SpaceSize", SpaceSize.ToString(System.Globalization.CultureInfo.InvariantCulture));

            Global.SaveData(xmldoc, xmlelBattleReport, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // public ArrayList Steps  = new ArrayList();
            foreach (BattleStep step in Steps)
            {
                switch (step.Type)
                
                {
                    case "Movement":
                        xmlelBattleReport.AppendChild(((BattleStepMovement)step).ToXml(xmldoc));
                        break;

                    case "Weapons":
                        xmlelBattleReport.AppendChild(((BattleStepWeapons)step).ToXml(xmldoc));
                        break;

                    case "Target":
                        xmlelBattleReport.AppendChild(((BattleStepTarget)step).ToXml(xmldoc));
                        break;

                    case "Destroy":
                        xmlelBattleReport.AppendChild(((BattleStepDestroy)step).ToXml(xmldoc));
                        break;

                    default:
                        xmlelBattleReport.AppendChild(step.ToXml(xmldoc));
                        break;


                }

            }

            // public Hashtable Stacks = new Hashtable();
            foreach (Fleet fleet in Stacks.Values)
            {
                xmlelBattleReport.AppendChild(fleet.ToXml(xmldoc));
            }

            // Losses< raceName, lossCount>
            if (Losses.Count > 0)
            {
                foreach (KeyValuePair<string, int> de in Losses)
                {
                    XmlElement xmlelLosses = xmldoc.CreateElement("Losses");
                    Global.SaveData(xmldoc, xmlelLosses, "Race", de.Key);
                    Global.SaveData(xmldoc, xmlelLosses, "Count", de.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    xmlelBattleReport.AppendChild(xmlelLosses);
                }
            }
            return xmlelBattleReport;

        }


        #endregion

        #region Unit Tests
#if (DEBUG)
        [Test]

        public void SerialisationTestEmptyBattleReport()
       {
           StringWriter stringStream = new StringWriter();
           try
           {

               // setup a test object
               BattleReport testData = new BattleReport();

               // setup the file name
               string saveFileName = "BattleReportTest.xml";

               // setup the streams
               MemoryStream memoryStream = new MemoryStream();
               
               // Setup the XML document
               XmlDocument xmldoc = new XmlDocument();
               Global.InitializeXmlDocument(xmldoc);

               // add the BattleReport to the document
               xmldoc.ChildNodes.Item(1).AppendChild(testData.ToXml(xmldoc));

               xmldoc.Save(memoryStream);
               xmldoc.Save(stringStream);

               // serialise to file
               if (TestControls.CreateFiles)
               {
                   using (Stream fileStream = new FileStream(saveFileName, FileMode.Create))
                   {
                       xmldoc.Save(fileStream);
                   }
               }

               // deserialize 
               BattleReport loadedData;
               xmldoc.RemoveAll();

               if (TestControls.CreateFiles)
               {
                   using (FileStream saveFileStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read))
                   {
                       xmldoc.Load(saveFileStream);
                       loadedData = new BattleReport(xmldoc);
                   }
               }
               else
               {
                   // move to start of memory stream
                   memoryStream.Seek(0, SeekOrigin.Begin);
                   xmldoc.Load(memoryStream);
                   loadedData = new BattleReport(xmldoc);
               }

               // test if it worked
               // CheckTestIntel(loadedData);
           }
           catch (Exception e)
           {
               System.Windows.Forms.MessageBox.Show("BattleReport.cs SerialisationTestEmptyBattleReport() failed:" + Environment.NewLine + e.Message + Environment.NewLine + stringStream.ToString()); 
               throw e; // fail the test
           }
       }

       /// <summary>
       /// This is a test of the BattleReport's serialisation with initialized data.
       /// </summary>
       [Test]
       public void SerialisationTestBattleReport()
       {
           StringWriter stringStream = new StringWriter();
           try
           {

               // setup a test object
               BattleReport testData = new BattleReport();

               // setup the test data
               testData.Location = "Orion";
               testData.SpaceSize = 10;
               testData.Year = 2123;

               // TODO (priority 4) Include these sub objects in the unit test.
                // public ArrayList Steps  = new ArrayList();
  // public Hashtable Stacks = new Hashtable();
    // public Dictionary<string,int> Losses = new Dictionary<string,int>(); // raceName, lossCount


               // setup the file name
               string saveFileName = "BattleReportTest.xml";

               // setup the streams
               MemoryStream memoryStream = new MemoryStream();
               
               // Setup the XML document
               XmlDocument xmldoc = new XmlDocument();
               Global.InitializeXmlDocument(xmldoc);

               // add the Intel to the document
               xmldoc.ChildNodes.Item(1).AppendChild(testData.ToXml(xmldoc));

               xmldoc.Save(memoryStream);
               xmldoc.Save(stringStream);

               // serialise to file
               if (TestControls.CreateFiles)
               {
                   using (Stream fileStream = new FileStream(saveFileName, FileMode.Create))
                   {
                       xmldoc.Save(fileStream);
                   }
               }

               // deserialize 
               BattleReport loadedData;
               xmldoc.RemoveAll();

               if (TestControls.CreateFiles)
               {
                   using (FileStream saveFileStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read))
                   {
                       xmldoc.Load(saveFileStream);
                       loadedData = new BattleReport(xmldoc);
                   }
               }
               else
               {
                   // move to start of memory stream
                   memoryStream.Seek(0, SeekOrigin.Begin);
                   xmldoc.Load(memoryStream);
                   loadedData = new BattleReport(xmldoc);
               }

               // test if it worked
               // Assert.IsTrue(loadedData.Year == 2123);
               Assert.IsTrue(loadedData.Location == "Orion");
               // Assert.IsTrue(loadedData.SpaceSize == 10);

           }
           catch (Exception e)
           {
               System.Windows.Forms.MessageBox.Show("BattleReport.cs SerialisationTestBattleReport() failed:" + Environment.NewLine + e.Message + Environment.NewLine + stringStream.ToString()); 
               throw e; // fail the test
           }
       }

       

#endif
        #endregion

    }
}
