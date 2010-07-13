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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Nova.Common;
#endregion

// ============================================================================
// Manipulation of the turn data that is shared between the Console and GUI.
// ============================================================================

namespace Nova.Server
{
    public static class IntelWriter
    {
        private static ServerState StateData = null;
        private static Intel TurnData = null;


        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Save the turn data.
        /// </summary>
        /// <remarks>
        /// We have to be very careful that we have a consistent and self-contained data set in the turn file. For example, we write out "AllStars" but TurnData.AllStars is not the same as StateData.AllStars. So make sure any pointers to AllStars refer to the copy in TurnData otherwise we'll get duplicated (but separate) star objects.
        /// </remarks>
        /// -------------------------------------------------------------------
        public static void WriteIntel()
        {
            StateData = ServerState.Data;
            foreach (PlayerSettings player in StateData.AllPlayers)
            {
                TurnData = new Intel();
                TurnData.TurnYear = StateData.TurnYear;
                TurnData.MyRace = ServerState.Data.AllRaces[player.RaceName] as Race;
                TurnData.TurnYear = StateData.TurnYear;
                TurnData.AllStars = StateData.AllStars;
                TurnData.AllMinefields = StateData.AllMinefields;
                TurnData.AllFleets = StateData.AllFleets;
                TurnData.AllDesigns = StateData.AllDesigns;

                // Copy any messages
                foreach (Message message in StateData.AllMessages)
                {
                    if (message.Audience == "*" || message.Audience == player.RaceName)
                    {
                        TurnData.Messages.Add(message);
                    }
                }

                // Copy the list of player races
                foreach (Race race in StateData.AllRaces.Values)
                {
                    TurnData.AllRaceNames.Add(race.Name);
                    TurnData.RaceIcons[race.Name] = race.Icon;
                }

                // Copy any battle reports
                foreach (BattleReport report in StateData.AllBattles)
                {
                    TurnData.Battles.Add(report);
                }

                // Don't try and generate a scores report on the very start of a new
                // game.

                if (StateData.TurnYear > 2100)
                {
                    TurnData.AllScores = Scores.GetScores();
                }
                else
                {
                    TurnData.AllScores = new ArrayList();
                }

                ServerState.Data.GameFolder = FileSearcher.GetFolder(Global.ServerFolderKey, Global.ServerFolderName);
                if (ServerState.Data.GameFolder == null)
                {
                    Report.Error("Intel Writer: WriteIntel() - Unable to create file \"Nova.intel\".");
                    return;
                }
                string turnFileName = Path.Combine(ServerState.Data.GameFolder, player.RaceName + Global.IntelExtension);

                bool locked = false;
                do
                {
                    locked = false;
                    try
                    {
                        using (Stream turnFile = new FileStream(turnFileName, FileMode.Create))
                        {
                            Serializer.Serialize(turnFile, TurnData.TurnYear);
                            Serializer.Serialize(turnFile, TurnData);
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


