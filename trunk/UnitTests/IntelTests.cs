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

namespace Nova.UnitTests
{
    [TestFixture]
    public class IntelTests
    {
        /// <summary>
        /// This is a test of the Intel's serialisation, with empty Intel.
        /// 
        /// TODO Move test into separate integration test project.
        /// The test attempts to write to disk. Accessing external resources greatly
        /// reduces performance of the test suite and disrupts the unit test flow making
        /// this an integration test and not a unit test. Doing exception handling and
        /// using logic (if-statements) is also not recommended.
        /// </summary>
        [Ignore("Not a unit test, but an integration test. Should be moved to different project.")]
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

        /// <summary>
        /// This is a test of the Intel's serialisation with initialized data.
        /// 
        /// TODO Move test into separate integration test project.
        /// The test attempts to write to disk. Accessing external resources greatly
        /// reduces performance of the test suite and disrupts the unit test flow making
        /// this an integration test and not a unit test. Doing exception handling and
        /// using logic (if-statements) is also not recommended.
        /// </summary>
        [Ignore("Not a unit test, but an integration test. Should be moved to different project.")]
        [Test]
        public void SerialisationTestIntel()
        {
            StringWriter stringStream = new StringWriter();
            try
            {

                // setup a test object
                Intel testData = new Intel();

                // setup the test data
                testData.TurnYear = 3500;
                Star testStar = new Star();
                testStar.Name = "Pluto";
                testStar.Colonists = 25000;
                testData.AllStars.Add("Pluto", testStar);


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
                Assert.IsTrue(loadedData.TurnYear == 3500);
                Assert.IsTrue(loadedData.AllStars.Contains("Pluto") && ((Star)loadedData.AllStars["Pluto"]).Name == "Pluto");
                Assert.IsTrue(loadedData.AllStars.Contains("Pluto") && ((Star)loadedData.AllStars["Pluto"]).Colonists == 25000);

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Intel.cs SerialisationTestIntel() failed:" + Environment.NewLine + e.Message + Environment.NewLine + stringStream.ToString());
                throw e; // fail the test
            }
        }
    }
}