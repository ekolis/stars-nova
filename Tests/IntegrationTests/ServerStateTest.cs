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

using Nova.Server;
using NUnit.Framework;

namespace Nova.Tests.IntegrationTests
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Unit tests for ServerState. 
    /// </summary>
    /// ----------------------------------------------------------------------------
    [TestFixture]
    public class ServerStateTest
    {
        /// <summary>
        /// Test the (de)serialising of the ServerState.
        /// 
        /// TODO Magic numbers (2101, 2102 is a magic number) in tests is not recommended.
        /// </summary>
        [Test]
        public void SerialisationTest()
        {
            ServerState StateData = new ServerState();
            // Setup the initial state
            StateData.TurnYear = 2101;
            StateData.GameInProgress = true;
            StateData.GameFolder = "dummy_value";
            StateData.StatePathName = "unit_test.sstate";

            // serialise
            StateData.Save();

            // change the value to ensure it is restored.
            StateData.TurnYear = 2102;
            StateData.GameInProgress = false;
            StateData.GameFolder = "foo_bar";

            // deserialise
            StateData = StateData.Restore();

            // test
            Assert.AreEqual(StateData.TurnYear, 2101);
            Assert.AreEqual(StateData.GameInProgress, true);
            Assert.AreEqual(StateData.GameFolder, "dummy_value");
        }
    }
}

