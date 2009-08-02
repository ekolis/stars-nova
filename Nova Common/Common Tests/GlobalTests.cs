// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2009 Jan Magne Tjensvold
//
// Test of global definition code.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.IO;
using NUnit.Framework;
using NovaCommon;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace NovaCommon
{

    // ============================================================================
    // Unit test for global definition code.
    // ============================================================================

    [TestFixture]
    public class GlobalTests
    {
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

        private static void AssertEqualRelativePath(string expected, string baseDir, string targetPath)
        {
            expected = expected.Replace('\\', Path.DirectorySeparatorChar);
            baseDir = baseDir.Replace('\\', Path.DirectorySeparatorChar);
            targetPath = targetPath.Replace('\\', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, Global.EvaluateRelativePath(baseDir, targetPath));
        }
    }
}

