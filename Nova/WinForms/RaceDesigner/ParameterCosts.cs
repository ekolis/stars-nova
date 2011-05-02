#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// This file holds the costs of all the various parameters defined by the
// numeric up/down counters in the tabbed forms.
// ===========================================================================
#endregion

namespace Nova.WinForms.RaceDesigner
{
    /// <summary>
    /// Static definition of all the advantage point costs for each parameter
    /// </summary>
    public static class ParameterCosts
    {
        // Mines that can be operated per 10k colonists.
        private static readonly int[] OperableMines = new[]
            {
                 10,   0,   0,   0,   0,
                -66, -53, -39, -26, -13,
                  0,  12,  23,  35,  47,
                 58,  70,  82,  93, 105,
                117, 128, 140, 152, 163,
                175
            };

        // Number of resources required to build a mine.
        private static readonly int[] ResourcesPerMine = new[]
            {
                   5,    0,  190,   43,   22,
                   0,  -21,  -43,  -64,  -86,
                -108, -129, -151, -173, -194,
                -216
            };

        // Mineral production (x10kT) for every 10 mines.
        private static readonly int[] MineralProduction = new[]
            {
                  10,    0,   0,   0,   0,
                -166, -133, -99, -66, -33,
                   0,   56, 113, 169, 225,
                 282,  338, 394, 451, 507,
                 563,  620, 676, 732, 789,
                 845
            };

        // Number of factories that can be operated by 10k colonists.
        private static readonly int[] OperableFactories = new[]
            {
                 10,   0,   0,   0,   0,
                -66, -53, -39, -26, -13,
                  0,  14,  27,  39,  52,
                 65,  78, 100, 123, 146,
                169, 192, 229, 257, 285,
                313
            };

        // Number of resources required to build each factory.
        private static readonly int[] FactoryBuildCost = new[]
            {
                  10,    0,    0,    0,    0,
                 500,  320,  180,   80,   20,
                   0,  -18,  -36,  -54,  -73,
                 -91, -109, -128, -146, -164,
                -183, -201, -219, -234, -240,
                -246
            };

        // Number of resources produced by each set of 10 factories.
        private static readonly int[] ResourceProduction = new[]
            {
                  10,    0,   0,   0,   0,
                -166, -133, -99, -66, -33,
                   0,   42,  84, 145, 207,
                 268
            };

        // Number of colonists required to generate one resource. We increment in
        // steps of 100 and start at 700 hence the blank values in the cost array.
        private static readonly int[] ColonistProduction = new[]
            {
                1000,    0,    0,    0,    0,
                   0,    0,  800,  420,  200,
                   0,  -39,  -79, -119, -159,
                -199, -239, -279, -319, -359,
                -399, -439, -479, -519, -559,
                -599
            };

        // Growth Rate.
        private static readonly int[] MaxGrowth = new[]
            {
                   15, -7594, -6171, -4748, -3325,
                -1902, -1656, -1161,  -565,  -394,
                 -274,  -228,  -182,  -136,   -68,
                    0,    69,   137,   206,   274,
                  412
            };

        public static readonly ParameterEntry[] Parameters = new[] 
            {
                new ParameterEntry("maxGrowth", MaxGrowth),
                new ParameterEntry("colonistProduction", ColonistProduction),
                new ParameterEntry("resourceProduction", ResourceProduction),
                new ParameterEntry("factoryBuildCost", FactoryBuildCost),
                new ParameterEntry("operableFactories", OperableFactories),
                new ParameterEntry("mineralProduction", MineralProduction),
                new ParameterEntry("resourcesPerMine", ResourcesPerMine),
                new ParameterEntry("operableMines", OperableMines)
            };
    }
}

