#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using Nova.Common;
    using Nova.Common.DataStructures;
    using Nova.Common.Components;

    /// <summary>
    /// This module converts the console's state into Intel and saves it, thereby 
    /// generating the next turn to be played.
    /// </summary>
    public class IntelWriter
    {
        private readonly ServerData serverState;
        private readonly Scores Scores;
        private Intel turnData;
        
        public IntelWriter(ServerData serverState, Scores scores)
        {
            this.serverState = serverState;
            this.Scores = scores;
        }


        /// <summary>
        /// Save the turn data.
        /// </summary>
        /// <remarks>
        /// We have to be very careful that we have a consistent and self-contained data set in the turn file.
        /// For example, we write out "AllStars" but turnData.AllStars is not the same as stateData.AllStars.
        /// So make sure any pointers to AllStars refer to the copy in turnData otherwise we'll get
        /// duplicated (but separate) star objects.
        /// </remarks>
        public void WriteIntel()
        {
            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {
                turnData = new Intel();
                turnData.AllMinefields = serverState.AllMinefields;
                turnData.EmpireState = serverState.AllEmpires[empire.Id];
                
                
                // Copy any messages
                foreach (Message message in serverState.AllMessages)
                {
                    if (message.Audience == Global.Everyone || message.Audience == empire.Id)
                    {
                        turnData.Messages.Add(message);
                    }
                }

                // Don't try and generate a scores report on the very start of a new
                // game.

                if (serverState.TurnYear > Global.StartingYear)
                {
                    turnData.AllScores = Scores.GetScores();
                }
                else
                {
                    turnData.AllScores = new List<ScoreRecord>();
                }

                serverState.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                if (serverState.GameFolder == null)
                {
                    Report.Error("Intel Writer: WriteIntel() - Unable to create file \"Nova.intel\".");
                    return;
                }
                string turnFileName = Path.Combine(serverState.GameFolder, empire.Race.Name + Global.IntelExtension);

                // Write out the intel file, as xml
                bool waitForFile = false;
                double waitTime = 0.0; // seconds
                do
                {
                    try
                    {
                        using (Stream turnFile = new FileStream(turnFileName /*+ ".xml"*/, FileMode.Create))
                        {
                            // Setup the XML document
                            XmlDocument xmldoc = new XmlDocument();
                            Global.InitializeXmlDocument(xmldoc);

                            // add the Intel to the document
                            xmldoc.ChildNodes.Item(1).AppendChild(turnData.ToXml(xmldoc));

                            xmldoc.Save(turnFile);
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

        }

    }
}


