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
// find a home Star in a rectangular map.
// ===========================================================================
#endregion

using Nova.Common;
using Nova.Server;
using Nova.Server.NewGame;
using NUnit.Framework;

namespace Nova.Tests.IntegrationTests
{
    /// <Summary>
    /// Test for game generation
    /// </Summary>
    [TestFixture]
    public class NewGameTest
    {
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Test rectangular map generation.
        /// 
        /// TODO Exception handling logic (if-statements) in tests is not recommended.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        [Test]
        public void Map800x400Test()
        {
            const int NUM_ATTEMPTS = 1; 

            // Generate the map
            ServerState stateData = new ServerState();
            try
            {
                // some inital data
                

                stateData.AllRaces.Clear();
                Race race = new Race();

                for (int i = 0; i < 7; i++)
                {
                    race.Name = "foo" + i;
                    stateData.AllRaces.Add(race.Name, race);
                    stateData.AllPlayers.Add(new PlayerSettings());
                }      

                for (int attempts = 0; attempts < NUM_ATTEMPTS; ++attempts)
                {
                    stateData.AllStars.Clear();

                    // make a map
                    GameSettings.Data.MapHeight = 800;
                    GameSettings.Data.MapWidth = 400;
                    GameSettings.Data.StarDensity = 60;
                    GameSettings.Data.StarSeparation = 10;
                    GameSettings.Data.StarUniformity = 60;
     
                    StarMapInitialiser starMapInitializer = new StarMapInitialiser(stateData);
                    
                    starMapInitializer.GenerateStars();
                    starMapInitializer.GeneratePlayerAssets();
                }
            }
            catch
            {
                // fail on any exception
                System.Windows.Forms.MessageBox.Show("Number of stars: " + stateData.AllStars.Count); // keep this line for debugging - Dan 01 Jul 11
                Assert.Fail();
            }
        }
    }
}

