#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

using Nova.Common;
using Nova.Common.DataStructures;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{
    [TestFixture]
    public class PointUtilitiesTests
    {
        [Test]
        public void TestIsNearDoesNotChangeParams()
        {
            NovaPoint a = new NovaPoint(1, 2);
            NovaPoint b = new NovaPoint(3, 4);

            bool testNear = PointUtilities.IsNear(a, b);

            Assert.IsTrue(a.X == 1);
            Assert.IsTrue(a.Y == 2);
            Assert.IsTrue(b.X == 3);
            Assert.IsTrue(b.Y == 4);
        }
    }
}