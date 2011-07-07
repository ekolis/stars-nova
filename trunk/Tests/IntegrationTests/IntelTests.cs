#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

using System;
using System.IO;
using System.Xml;
using Nova.Common;
using NUnit.Framework;

namespace Nova.Tests.IntegrationTests
{
    [TestFixture]
    public class IntelTests
    {
        /// <Summary>
        /// This is a test of the Intel's serialisation, with empty Intel.
        /// 
        /// TODO Exception handling and logic (if-statements) in tests is not recommended.
        /// </Summary>
        [Test]
        public void SerialisationTestEmptyIntel()
        {
            StringWriter stringStream = new StringWriter();
            try
            {
                // setup a test object
                Intel testData = new Intel();

                // setup the file name
                string saveFileName = "IntelTest.xml";

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
                Intel loadedData;
                xmldoc.RemoveAll();

                if (TestControls.CreateFiles)
                {
                    using (FileStream saveFileStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read))
                    {
                        xmldoc.Load(saveFileStream);
                        loadedData = new Intel(xmldoc);
                    }
                }
                else
                {
                    // move to start of memory stream
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    xmldoc.Load(memoryStream);
                    loadedData = new Intel(xmldoc);
                }

                // test if it worked
                // CheckTestIntel(loadedData);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Intel.cs SerialisationTestEmptyIntel() failed:" + Environment.NewLine + e.Message + Environment.NewLine + stringStream.ToString());
                throw e; // fail the test
            }
        }

        /// <Summary>
        /// This is a test of the Intel's serialisation with initialized data.
        /// 
        /// TODO Exception handling and logic (if-statements) in tests is not recommended.
        /// </Summary>
        [Test]
        public void SerialisationTestIntel()
        {
            StringWriter stringStream = new StringWriter();
            try
            {

                // setup a test object
                Intel testData = new Intel();

                // setup the test data
                testData.EmpireState.TurnYear = 3500;
                Star testStar = new Star();
                testStar.Name = "Pluto";
                testStar.Colonists = 25000;
                testData.EmpireState.StarReports.Add(new StarIntel(testStar, IntelLevel.Owned, testData.EmpireState.TurnYear));


                // setup the file name
                string saveFileName = "IntelTest.xml";

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
                Intel loadedData;
                xmldoc.RemoveAll();

                if (TestControls.CreateFiles)
                {
                    using (FileStream saveFileStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read))
                    {
                        xmldoc.Load(saveFileStream);
                        loadedData = new Intel(xmldoc);
                    }
                }
                else
                {
                    // move to start of memory stream
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    xmldoc.Load(memoryStream);
                    loadedData = new Intel(xmldoc);
                }

                // test if it worked
                Assert.IsTrue(loadedData.EmpireState.TurnYear == 3500);
                Assert.IsTrue(loadedData.EmpireState.StarReports.Contains("Pluto") &&
                              loadedData.EmpireState.StarReports["Pluto"].Name == "Pluto");
                Assert.IsTrue(loadedData.EmpireState.StarReports.Contains("Pluto") &&
                              loadedData.EmpireState.StarReports["Pluto"].Colonists == 25000);

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Intel.cs SerialisationTestIntel() failed:" + Environment.NewLine + e.Message + Environment.NewLine + stringStream.ToString());
                throw e; // fail the test
            }
        }
    }
}