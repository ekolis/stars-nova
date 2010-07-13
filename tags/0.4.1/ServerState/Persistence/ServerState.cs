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
// This file contains data that are persistent across multiple invokations of
// Nova Console. (It also holds the odd item that doesn't need to be persistent
// but it's just convenient to keep all "global" data in one place.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

using Nova.Common;
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

        public ArrayList AllBattles    = new ArrayList();
        public ArrayList AllPlayers    = new ArrayList(); // collection of PlayerSettings (player number, race, ai (program name or "Default AI" or "Human")
        public Hashtable AllTechLevels = new Hashtable();
        public Hashtable AllDesigns    = new Hashtable();
        public Hashtable AllFleets     = new Hashtable();
        public Hashtable AllRaceData   = new Hashtable(); // Data about the race's relations and battle plans, see RaceData
        public Hashtable AllRaces      = new Hashtable(); // Data about the race (traits etc)
        public Hashtable AllStars      = new Hashtable();
        public Hashtable AllMinefields = new Hashtable();
        public ArrayList AllMessages   = new ArrayList(); // ArrayList containing all messages generated this turn.

        public bool GameInProgress     = false;
        public int FleetID             = 1;
        public int TurnYear            = 2100;
        public string GameFolder       = null; // The path&folder where client files are held.
        public string StatePathName    = null; // path&file name to the saved state data
        
        #endregion

        #region Singleton

        // ============================================================================
        // Data private to this module.
        // ============================================================================

        private static ServerState instance = null;
        private static object padlock = new object();

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Private constructor to prevent anyone else creating instances of this class.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private ServerState() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a mechanism of accessing the single instance of this class that we
        /// will create locally. Creation of the data is thread-safe.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static ServerState Data
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ServerState();
                        }
                    }
                }
                return instance;
            }

            // ----------------------------------------------------------------------------

            set
            {
                instance = value;
            }
        }

        #endregion

        #region Methods

        /// -------------------------------------------------------------------
        /// <summary>
        /// Restore the persistent data. 
        /// </summary>
        /// -------------------------------------------------------------------
        public static void Restore()
        {
            string fileName = Data.StatePathName;
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    ServerState.Data = (ServerState)Serializer.Deserialize(stream);
                }
            }
        }


        /// -------------------------------------------------------------------
        /// <summary>
        /// Save the console persistent data.
        /// </summary>
        /// -------------------------------------------------------------------
        public static void Save()
        {
            if (Data.StatePathName == null)
            {
                // TODO (priority 5) add the nicities. Update the game files location.
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Choose a location to save the game.";

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Data.StatePathName = fd.FileName;
                }
                else
                {
                    throw new System.IO.IOException("File dialog cancelled");
                }
            }

            using (FileStream stream = new FileStream(Data.StatePathName, FileMode.Create))
            {
                Serializer.Serialize(stream, ServerState.Data);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reset all values to the defaults
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void Clear()
        {
            Data.GameFolder = null;
            Data.AllRaces = new Hashtable();
            Data.AllRaceData = new Hashtable();
            Data.AllStars = new Hashtable();
            Data.AllDesigns = new Hashtable();
            Data.AllFleets = new Hashtable();
            Data.AllTechLevels = new Hashtable();
            Data.TurnYear = 2100;
            Data.FleetID = 1;
            Data.StatePathName = null;
            Data.GameInProgress = false;
        }

        #endregion

    }
}
