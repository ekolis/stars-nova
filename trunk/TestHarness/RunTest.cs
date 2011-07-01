﻿#region Copyright Notice
// ============================================================================
// Copyright (C) 2010, 2011 The Stars-Nova Project
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
// This module provides a means of running a test in the debugger without 
// the aid of a thrid party tool (such as ReSharper).
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nova.Common;
using Nova.Common.DataStructures;
using Nova.Tests;
using Nova.Tests.IntegrationTests;
using Nova.Tests.UnitTests;

namespace TestHarness
{
    /// <summary>
    /// This module provides a means of running a test in the debugger without 
    /// the aid of a thrid party tool (such as ReSharper).
    /// </summary>
    public class RunTest
    {
        public static void Main(string[] args)
        {
            // Run the New Game Test
            /*
            NewGameTest test = new NewGameTest();
            for (int i = 1; i < 1000; i++)
            {
                test.Map800x400Test();
            }
             */
            // Run the Intel Serialization Test
            /*
            Intel test = new Intel();
            test.SerialisationTestEmptyIntel();
            test.SerialisationTestIntel();             
             */

            // Debuging battle test
            /*
            BattleEngineTest test = new BattleEngineTest();
            test.Test1DetermineCoLocatedFleets();
             */

            // BattleReport
            /*
            BattleReport test = new BattleReport();
            test.SerialisationTestBattleReport();
             */

            // ItemTest
            ItemTest test = new ItemTest();
            test.Init();
            test.Item1Owner();
            test.Item1PureId();
            test.Item2Owner();
            test.Item2PureId();


        }
    }
}
