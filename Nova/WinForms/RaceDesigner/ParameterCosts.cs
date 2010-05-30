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

using System;

namespace Nova.WinForms.RaceDesigner
{

    /// <summary>
    /// Class to support the format of each array entry (note that element 0
    /// of the Cost array is used to hold previous counter values).
    /// </summary>
    class ParameterEntry
    {
        public string ParameterName;
        public int[] Cost;

        public ParameterEntry(string p, int[] c)
        {
            ParameterName = p;
            Cost = c;
        }
    }


    /// <summary>
    /// Static definition of all the advantage point costs for each parameter
    /// </summary>
    class ParameterCosts
    {
        public static ParameterEntry[] Parameters = new ParameterEntry[8] 
      {

// -----------------------------------------------------------------------------
// Growth Rate.
// -----------------------------------------------------------------------------

         new ParameterEntry("MaxGrowth", new int[21] {15, 
             -7594, -6171, -4748, -3325, -1902, 
             -1656, -1161,  -565,  -394,  -274, 
             -228,  -182,  -136,   -68,     0, 
               69,   137,   206,   274,   412}),

// -----------------------------------------------------------------------------
// Number of colonists required to generate one resource. We increment in
// steps of 100 and start at 700 hence the blank values in the cost array.
// -----------------------------------------------------------------------------

         new ParameterEntry("ColonistProduction", new int[26] {1000,
               0,     0,    0,    0,    0,
               0,   800,  420,  200,    0,
             -39,   -79, -119, -159, -199,
             -239, -279, -319, -359, -399,
             -439, -479, -519, -559, -599}),

// -----------------------------------------------------------------------------
// Number of resources produced by each set of 10 factories.
// -----------------------------------------------------------------------------

         new ParameterEntry("ResourceProduction", new int[16] {10,
                0,   0,   0,   0, -166,
             -133, -99, -66, -33,    0,
               42,  84, 145, 207,  268}),

// -----------------------------------------------------------------------------
// Number of resources required to build each factory
// -----------------------------------------------------------------------------

         new ParameterEntry("FactoryBuildCost", new int[26] {10,
                0,    0,    0,    0,  500,
              320,  180,   80,   20,    0,
              -18,  -36,  -54,  -73,  -91,
             -109, -128, -146, -164, -183,
             -201, -219, -234, -240, -246}),

// -----------------------------------------------------------------------------
// Number of factories that can be operated by 10k colonists.
// -----------------------------------------------------------------------------

         new ParameterEntry("OperableFactories", new int[26] {10,
               0,   0,   0,   0, -66,
             -53, -39, -26, -13,   0,
              14,  27,  39,  52,  65,
              78, 100, 123, 146, 169,
             192, 229, 257, 285, 313}),

// -----------------------------------------------------------------------------
// Mineral production (x10kT) for every 10 mines.
// -----------------------------------------------------------------------------

         new ParameterEntry("MineralProduction", new int[26] {10,
                0,   0,   0,    0, -166,
             -133, -99, -66,  -33,    0,
               56, 113, 169,  225,  282,
              338, 394, 451,  507,  563,
              620, 676, 732,  789,  845}),

// -----------------------------------------------------------------------------
// Number of resources required to build a mine.
// -----------------------------------------------------------------------------

         new ParameterEntry("ResourcesPerMine", new int[16] {5,
                0,  190,   43,   22,    0,
              -21,  -43,  -64,  -86, -108,
             -129, -151, -173, -194, -216}),

// -----------------------------------------------------------------------------
// Mines that can be operated per 10k colonists
// -----------------------------------------------------------------------------

         new ParameterEntry("OperableMines", new int[26] {10,
               0,   0,   0,   0, -66,
             -53, -39, -26, -13,   0,
              12,  23,  35,  47,  58,
              70,  82,  93, 105, 117, 
             128, 140, 152, 163, 175})

      };

    }
}

