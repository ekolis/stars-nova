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
// This file contains data that are persistent across multiple invokations of
// Nova Console. (It also holds the odd item that doesn't need to be persistent
// but it's just convenient to keep all "global" data in one place.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Nova.Common;
using Nova.Common.Components;
using Nova.Common.DataStructures;
using Message = Nova.Common.Message;

#endregion


namespace Nova.Server
{

    /// <summary>
    /// Object for Manipulation of data that is persistent across muliple invocations of the
    /// Nova Console.
    /// </summary>
    [Serializable]
    public sealed class ServerState
    {
        #region Data

        public List<BattleReport> AllBattles = new List<BattleReport>();
        public List<PlayerSettings> AllPlayers = new List<PlayerSettings>(); // Player number, race, ai (program name or "Default AI" or "Human")
        public Hashtable AllTechLevels  = new Hashtable(); // Sum of a player's techlevels, for scoring purposes.
        public Dictionary<string, Design> AllDesigns = new Dictionary<string, Design>();
        public Dictionary<string, Fleet> AllFleets = new Dictionary<string, Fleet>();
        public Hashtable AllRaceData    = new Hashtable(); // Data about the race's relations and battle plans, see RaceData
        public Hashtable AllRaces       = new Hashtable(); // Data about the race (traits etc)
        public Dictionary<string, Star> AllStars = new Dictionary<string, Star>();
        public Dictionary<string, Minefield> AllMinefields = new Dictionary<string, Minefield>();
        public List<Message> AllMessages = new List<Message>(); // All messages generated this turn.

        public bool GameInProgress      = false;
        public int FleetID              = 1;
        public int TurnYear             = 2100;
        public string GameFolder        = null; // The path&folder where client files are held.
        public string StatePathName     = null; // path&file name to the saved state data
        
        #endregion

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Creates a new fresh server state.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ServerState()
        { 
        }

        #region Methods

        /// -------------------------------------------------------------------
        /// <summary>
        /// Restore the persistent data. 
        /// </summary>
        /// -------------------------------------------------------------------
        public ServerState Restore()
        {
            string fileName = this.StatePathName;            
            ServerState serverState = new ServerState();
            
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {                    
                    serverState = (ServerState)Serializer.Deserialize(stream);
                }
            }
            
            return serverState;
        }


        /// -------------------------------------------------------------------
        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        /// -------------------------------------------------------------------
        public void Save()
        {
            if (this.StatePathName == null)
            {
                // TODO (priority 5) add the nicities. Update the game files location.
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Choose a location to save the game.";

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.StatePathName = fd.FileName;
                }
                else
                {
                    throw new System.IO.IOException("File dialog cancelled");
                }
            }

            using (FileStream stream = new FileStream(this.StatePathName, FileMode.Create))
            {
                Serializer.Serialize(stream, this);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reset all values to the defaults
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Clear()
        {
            this.GameFolder = null;
            this.AllRaces.Clear();
            this.AllRaceData.Clear();
            this.AllStars.Clear();
            this.AllDesigns.Clear();
            this.AllFleets.Clear();
            this.AllTechLevels.Clear();
            this.TurnYear = 2100;
            this.FleetID = 1;
            this.StatePathName = null;
            this.GameInProgress = false;
        }

        #endregion

    }
}
