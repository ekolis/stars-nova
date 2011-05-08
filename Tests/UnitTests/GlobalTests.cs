#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
        [Test]
        public void EvaluateRelativePath_SameDirectory_ReturnsFileName()
        {
            AssertEqualRelativePath("test.txt", @"c:\files", @"c:\files\test.txt");
        }

        [Test]
        public void EvaluateRelativePath_SameDirectoryWithTrailingBackslash_ReturnsFileName()
        {
            AssertEqualRelativePath("test.txt", @"c:\files\", @"c:\files\test.txt");
        }

        [Test]
        public void EvaluateRelativePath_BaseDirIsSubDirectoryOfTargetPath_ReturnsFileNameOneDirectoryUp()
        {
            AssertEqualRelativePath(@"..\test.txt", @"c:\files", @"c:\test.txt");
        }

        [Test]
        public void EvaluateRelativePath_TargetPathIsSubDirectoryOfBaseDir_ReturnsFileNameOneDirectoryDown()
        {
            AssertEqualRelativePath(@"files\test.txt", @"c:\", @"c:\files\test.txt");
        }

        [Test]
        public void EvaluateRelativePath_TargetPathAndBaseDirAreInDifferentDirectories_ReturnsFileNameOneDirectoryUpThenDown()
        {
            AssertEqualRelativePath(@"..\otherfiles\test.txt", @"c:\files", @"c:\otherfiles\test.txt");
        }

        [Test]
        public void EvaluateRelativePath_DifferentDiskDrives_ReturnsTargetPath()
        {
            AssertEqualRelativePath(@"d:\files\fred.txt", @"c:\files", @"d:\files\fred.txt");
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

