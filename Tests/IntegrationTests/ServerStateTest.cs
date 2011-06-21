#region Copyright Notice
// ============================================================================
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
// Unit tests for ServerState.
// ===========================================================================
#endregion

using System.Xml;

using Nova.Server;
using NUnit.Framework;

namespace Nova.Tests.IntegrationTests
{
    /// ----------------------------------------------------------------------------
    /// <Summary>
    /// Unit tests for ServerState. 
    /// </Summary>
    /// ----------------------------------------------------------------------------
    [TestFixture]
    public class ServerStateTest
    {
        /// <Summary>
        /// Test the (de)serialising of the ServerState.
        /// 
        /// TODO Magic numbers (2101, 2102 is a magic number) in tests is not recommended.
        /// </Summary>
        [Test]
        public void SerialisationTest()
        {
            ServerState stateData = new ServerState();
            // Setup the initial state
            stateData.TurnYear = 2101;
            stateData.GameInProgress = true;
            stateData.GameFolder = "dummy_value";
            stateData.StatePathName = "unit_test.sstate";
            stateData.AllTechLevels["foo"] = 10;
            stateData.AllTechLevels["bar"] = 5;

            // serialise
            stateData.Save();

            // change the value to ensure it is restored.
            stateData.TurnYear = 2102;
            stateData.GameInProgress = false;
            stateData.GameFolder = "foo_bar";
            stateData.AllTechLevels["foo"] = 2;
            stateData.AllTechLevels["bar"] = 7;

            // deserialise
            stateData = stateData.Restore();

            // test
            Assert.AreEqual(2101, stateData.TurnYear);            
            Assert.AreEqual("dummy_value", stateData.GameFolder);
            Assert.AreEqual(10, stateData.AllTechLevels["foo"]);
            Assert.AreEqual(5, stateData.AllTechLevels["bar"]);
            Assert.AreEqual(true, stateData.GameInProgress);
        }
    }
}

