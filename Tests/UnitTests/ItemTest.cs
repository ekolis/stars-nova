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
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

using Nova.Common;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{
    [TestFixture]
    public class ItemTest
    {
        Item testItem1 = new Item();
        Item testItem2 = new Item();

        [SetUp]
        public void Init()
        {
            testItem1.PureId = 1;
            testItem1.Owner = 4;
            testItem2.PureId = 2;
            testItem2.Owner = 6;
        }

        [Test]
        public void Item1PureId()
        {
            Assert.AreEqual(1, testItem1.PureId);
        }

        [Test]
        public void Item2PureId()
        {
            Assert.AreEqual(2, testItem2.PureId);
        }

        [Test]
        public void Item1Owner()
        {
            Assert.AreEqual(4, testItem1.Owner);
        }

        [Test]
        public void Item2Owner()
        {
            Assert.AreEqual(6, testItem2.Owner);
        }
    }
}
