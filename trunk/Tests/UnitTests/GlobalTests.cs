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
// Test of global definition code.
// ===========================================================================
#endregion

using System.IO;
using Nova.Common;
using NUnit.Framework;

namespace Nova.Tests.UnitTests
{

    /// <summary>
    /// Unit test for global definition code.
    /// </summary>
    [TestFixture]
    public class GlobalTests
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Relative path test.
        /// 
        /// TODO Clean up unit test. Asserting on many things in a single test is not recommended.
        /// This indicates that the test probably should be broken into multiple tests.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Test]
        public void RelativePathTest()
        {
            AssertEqualRelativePath("fred.txt", @"C:\", @"c:\fred.txt");
            AssertEqualRelativePath("fred robson.txt", @"C:\", @"c:\fred robson.txt");
            AssertEqualRelativePath("fred%20robson.txt", @"c:\", @"c:\fred%20robson.txt");
            AssertEqualRelativePath("fred.txt", @"c:\fred\Tom\Bill", @"c:\fred\Tom\Bill\fred.txt");
            AssertEqualRelativePath("fred.txt", @"c:\fred\Tom\Bill\", @"c:\fred\Tom\Bill\fred.txt");
            AssertEqualRelativePath(@"..\fred.txt", @"c:\fred\Tom\Bill", @"c:\fred\Tom\fred.txt");
            AssertEqualRelativePath(@"..\..\fred.txt", @"c:\fred\Tom\Bill", @"c:\fred\fred.txt");
            AssertEqualRelativePath(@"..\..\..\fred.txt", @"c:\fred\Tom\Bill", @"c:\fred.txt");
            AssertEqualRelativePath(@"Tom\Bill\fred.txt", @"c:\fred", @"c:\fred\Tom\Bill\fred.txt");
            AssertEqualRelativePath(@"Bill\fred.txt", @"c:\fred\Tom", @"c:\fred\Tom\Bill\fred.txt");
            AssertEqualRelativePath(@"..\..\..\Harry\Tom\Bill\fred.txt", @"c:\fred\Tom\Bill", @"c:\Harry\Tom\Bill\fred.txt");
            AssertEqualRelativePath(@"c:\Harry\Tom\Bill\fred.txt", @"d:\fred\Tom\Bill\", @"c:\Harry\Tom\Bill\fred.txt");
            AssertEqualRelativePath(@"d:\Harry\Tom\Bill\fred.txt", @"c:\fred\Tom\Bill", @"d:\Harry\Tom\Bill\fred.txt");
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Assert that the expected relative path is generated from a base directory and a target file.
        /// </summary>
        /// <param name="expected">Expected relative path.</param>
        /// <param name="baseDir">Starting directory.</param>
        /// <param name="targetPath">Target.</param>
        /// ----------------------------------------------------------------------------
        private static void AssertEqualRelativePath(string expected, string baseDir, string targetPath)
        {
            expected = expected.Replace('\\', Path.DirectorySeparatorChar);
            baseDir = baseDir.Replace('\\', Path.DirectorySeparatorChar);
            targetPath = targetPath.Replace('\\', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, Global.EvaluateRelativePath(baseDir, targetPath));
        }
    }
}

