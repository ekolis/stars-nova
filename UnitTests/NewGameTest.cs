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
// Test of game generation code.
// This test was created to capture issue #3029446 caused by attempting to 
// find a home star in a rectangular map.
// ===========================================================================
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Nova.Common;
using Nova.NewGame;
using Nova.Server;

using NUnit.Framework;

namespace Nova.UnitTests
{

    /// <summary>
    /// Test for game generation
    /// </summary>
    [TestFixture]
    public class NewGameTest
    {
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test rectangular map generation.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Test]
        public void Map800x400Test()
        {
            // Generate the map
            ServerState stateData = ServerState.Data;
            try
            {
                // some inital data
                

                stateData.AllRaces.Clear();
                Race race = new Race();

                for (int i = 0; i < 7; i++)
                {
                    race.Name = "foo" + i;
                    stateData.AllRaces.Add(race.Name, race);
                }

                for (int attempts = 0; attempts < 100; ++attempts)
                {
                    stateData.AllStars.Clear();

                    // make a map
                    GameSettings.Data.MapHeight = 800;
                    GameSettings.Data.MapWidth = 400;
                    GameSettings.Data.StarDensity = 60;
                    GameSettings.Data.StarSeparation = 10;
                    GameSettings.Data.StarUniformity = 60;

                    StarMapInitialiser.GenerateStars();
                    StarMapInitialiser.InitialisePlayerData();
                }
            }
            catch
            {
                // fail on any exception
                // MessageBox.Show("Number of stars: " + stateData.AllStars.Count);
                Assert.Fail();
            }

            
        }
        
    }
}

