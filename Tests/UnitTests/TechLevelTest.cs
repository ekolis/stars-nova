#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <https://github.com/ekolis/stars-nova>.
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

namespace Nova.Tests.UnitTests
{
	using Nova.Common;
	using NUnit.Framework;

	[TestFixture]
	public class TechLevelTest
	{
		private TechLevel target = new TechLevel(3);
		private TechLevel allLess = new TechLevel(2);
		private TechLevel allGreater = new TechLevel(4);
		private TechLevel mixed = new TechLevel(1, 2, 3, 4, 5, 6);

		public void GreaterThan()
		{
			Assert.IsFalse(target > target);
			Assert.IsFalse(allLess > target);
			Assert.IsTrue(allGreater > target);
			Assert.IsFalse(mixed > target);
		}

		public void GreaterThanOrEqual()
		{
			Assert.IsTrue(target >= target);
			Assert.IsFalse(allLess >= target);
			Assert.IsTrue(allGreater >= target);
			Assert.IsFalse(mixed >= target);
		}
		public void LessThan()
		{
			Assert.IsFalse(target < target);
			Assert.IsTrue(allLess < target);
			Assert.IsFalse(allGreater < target);
			Assert.IsTrue(mixed < target);
		}

		public void LessThanOrEqual()
		{
			Assert.IsTrue(target <= target);
			Assert.IsTrue(allLess <= target);
			Assert.IsFalse(allGreater <= target);
			Assert.IsTrue(mixed <= target);
		}
	}
}
