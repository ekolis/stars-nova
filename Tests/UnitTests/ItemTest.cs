﻿#region Copyright Notice
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
using NUnit;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{
    /// <summary>
    /// Unit testing for <see cref="Item"/>.
    /// </summary>
    [TestFixture]
    public class ItemTest
    {
        /// <summary>
        /// Test that valid Owner values can be set a read for an <see cref="Item"/>.
        /// </summary>
        /// <param name="testOwner">The value to be set as the Owner of the <see cref="Item"/>.</param>
        /// <returns>The value read back from the Owner property of the <see cref="Item"/>.</returns>
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(255, ExpectedResult = 255)]
        public ushort Test1ValidOwner(int testOwner)
        {
            Item testItem = new Item();
            testItem.Owner = (ushort) testOwner;
            return testItem.Owner;
        }

        /// <summary>
        /// Test that valid Id values can be set a read for an <see cref="Item"/>.
        /// </summary>
        /// <param name="testId">The value to be set as the Id of the <see cref="Item"/>.</param>
        /// <returns>The value read back from the Id property of the <see cref="Item"/>.</returns>
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(0xFFFFFFFF, ExpectedResult = 0xFFFFFFFF)]
        public uint Test2ValidId(long testId)
        {
            Item testItem = new Item();
            testItem.Id = (uint) testId;
            return testItem.Id;
        }

        /// <summary>
        /// Test that valid key values can be set and read.
        /// </summary>
        /// <param name="testOwner">The value to be set as the Owner of the <see cref="Item"/>.</param>
        /// <param name="testId">The value to be set as the Id of the <see cref="Item"/>.</param>
        /// <returns>The value read back from the Key property of the <see cref="Item"/>.</returns>
        [TestCase(1, 1, ExpectedResult = 0x0100000001)]
        [TestCase(255, 1, ExpectedResult = 0xFF00000001)]
        [TestCase(1, 0xFFFFFFFF, ExpectedResult = 0x01FFFFFFFF)]
        [TestCase(255, 0xFFFFFFFF, ExpectedResult = 0xFFFFFFFFFF)]
        public long Test3ValidKey(int testOwner, long testId)
        {
            Item testItem = new Item();
            testItem.Owner = (ushort) testOwner;
            testItem.Id = (uint) testId;

            return testItem.Key;
        }
    }
}
