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

#region Module Description
// ===========================================================================
// This module converts the console's state into Intel and saves it, thereby 
// generating the next turn to be played.
//
// This is a static helper object that acts on ConsoleState to produce an Intel 
// Object.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

using Nova.Common;
using Nova.Common.DataStructures;

#endregion

// ============================================================================
// Manipulation of the turn data that is shared between the Console and GUI.
// ============================================================================

namespace Nova.Server
{
    public class IntelWriter
    {
        private readonly ServerState StateData;
        private readonly Scores Scores;
        private Intel turnData;
        
        public IntelWriter(ServerState serverState, Scores scores)
        {
            this.StateData = serverState;
            this.Scores = scores;
        }

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Save the turn data.
        /// </summary>
        /// <remarks>
        /// We have to be very careful that we have a consistent and self-contained data set in the turn file.
        /// For example, we write out "AllStars" but turnData.AllStars is not the same as stateData.AllStars.
        /// So make sure any pointers to AllStars refer to the copy in turnData otherwise we'll get
        /// duplicated (but separate) star objects.
        /// </remarks>
        /// -------------------------------------------------------------------
        public void WriteIntel()
        {
            foreach (EmpireData empire in StateData.AllEmpires.Values)
            {
                turnData = new Intel();
                turnData.AllMinefields = StateData.AllMinefields;
                turnData.AllDesigns = StateData.AllDesigns;
                turnData.EmpireState = StateData.AllEmpires[empire.Id];
                
                // Copy any messages
                foreach (Message message in StateData.AllMessages)
                {
                    if (message.Audience == Global.AllEmpires || message.Audience == empire.Id)
                    {
                        turnData.Messages.Add(message);
                    }
                }

                // Copy any battle reports
                foreach (BattleReport report in StateData.AllBattles)
                {
                    turnData.Battles.Add(report);
                }

                // Don't try and generate a scores report on the very start of a new
                // game.

                if (StateData.TurnYear > Global.StartingYear)
                {
                    turnData.AllScores = Scores.GetScores();
                }
                else
                {
                    turnData.AllScores = new List<ScoreRecord>();
                }

                StateData.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                if (StateData.GameFolder == null)
                {
                    Report.Error("Intel Writer: WriteIntel() - Unable to create file \"Nova.intel\".");
                    return;
                }
                string turnFileName = Path.Combine(StateData.GameFolder, empire.EmpireRace.Name + Global.IntelExtension);

                // Write out the intel file, as xml, but also handle the case of it being locked (in use).
                bool locked = false;
               
                do
                {
                    locked = false;
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
                    }
                    catch (System.IO.IOException)
                    {
                        locked = true;
                        System.Threading.Thread.Sleep(100);
                    }
                }
                while (locked);
            }

        }

        #endregion

    }
}


