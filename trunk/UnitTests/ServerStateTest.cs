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

namespace Nova.UnitTests
{
    using Nova.Common;
    using Nova.Server;

    using NUnit.Framework;

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
        /// </summary>
        [Test]
        public void SerialisationTest()
        {
            // Setup the initial state
            ServerState.Data.TurnYear = 2101;
            ServerState.Data.GameInProgress = true;
            ServerState.Data.GameFolder = "dummy_value";
            ServerState.Data.StatePathName = "unit_test.sstate";

            // serialise
            ServerState.Save();

            // change the value to ensure it is restored.
            ServerState.Data.TurnYear = 2102;
            ServerState.Data.GameInProgress = false;
            ServerState.Data.GameFolder = "foo_bar";

            // deserialise
            ServerState.Restore();

            // test
            Assert.AreEqual(ServerState.Data.TurnYear, 2101);
            Assert.AreEqual(ServerState.Data.GameInProgress, true);
            Assert.AreEqual(ServerState.Data.GameFolder, "dummy_value");
        }
    }
}

