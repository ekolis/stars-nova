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

#region Module Description
// ===========================================================================
// This module provides a means of un-compressing and re-compressing the 
// game files to aid in debugging.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Nova.Common;


namespace GameFileInflator
{
    /// <summary>
    /// This module provides a means of un-compressing and re-compressing the 
    /// game files to aid in debugging.
    /// </summary>
    public partial class GameFileInflator : Form
    {
        /// <summary>
        /// Initialise a GameFileInflator object.
        /// </summary>
        public GameFileInflator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialise the gameFilesLocationTextBox from the Nova.conf, if any.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void GameFileInflator_Load(object sender, EventArgs e)
        {
            string gameFiles = FileSearcher.GetFolder(Global.ClientFolderKey, Global.ClientFolderName);
            if (!String.IsNullOrEmpty(gameFiles))
            {
                gameFilesLocationTextBox.Text = gameFiles;
            }

        }

        /// <summary>
        /// Starting from the location specified by gameFilesLocationTextBox, uncompress any files found.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DecompressButton_Click(object sender, EventArgs e)
        {
            string gameFolder = gameFilesLocationTextBox.Text;
            if (String.IsNullOrEmpty(gameFolder))
            {
                Report.Error("Please provide the location of the game files to de-compress.");
                return;
            }

            DirectoryInfo source = new DirectoryInfo(gameFolder);

            // decompress each file
            FileInfo[] fileList = source.GetFiles();
            foreach (FileInfo fi in fileList)
            {
                progressBox.Text += fi.FullName;
                try
                {
                    string sourceFileName = fi.FullName;
                    string destFileName = fi.FullName + ".xml";

                    using (FileStream inStream = new FileStream(sourceFileName, FileMode.Open))
                    {
                        GZipStream foo = new GZipStream(inStream, CompressionMode.Decompress);

                        StreamReader reader = new StreamReader(foo);
                        using (StreamWriter writer = new StreamWriter(destFileName))
                        {
                            writer.Write(reader.ReadToEnd());

                            progressBox.Text += " done." + Environment.NewLine;
                        }
                    }
                }
                catch (InvalidDataException)
                {
                    FileInfo destFile = new FileInfo(fi.FullName + ".xml");
                    destFile.Delete();
                    progressBox.Text += " skipped." + Environment.NewLine;
                }
            }

            progressBox.Text += "Done." + Environment.NewLine;
        }


        /// <summary>
        /// Browse for the location of the game files.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void GameFilesBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult result = fb.ShowDialog();

            if (result == DialogResult.OK)
            {
                progressBox.Text = "";
                gameFilesLocationTextBox.Text = fb.SelectedPath;
            }
        }
    }
}
