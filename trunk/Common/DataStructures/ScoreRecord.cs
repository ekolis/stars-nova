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
// Keeps details of the data needed for the Score report
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;


namespace Nova.Common
{
    /// <summary>
    /// Keeps details of the data needed for the Score report
    /// </summary>
    [Serializable]
    public sealed class ScoreRecord : IComparable
    {
        public string Race;
        public int Rank;
        public int Score;
        public int Planets;
        public int Starbases;
        public int UnarmedShips;
        public int EscortShips;
        public int CapitalShips;
        public int TechLevel;
        public int Resources;


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a sort by rank function
        /// </summary>
        /// <param name="rightHandSide"></param>
        /// ----------------------------------------------------------------------------
        public int CompareTo(Object rightHandSide)
        {
            ScoreRecord rhs = (ScoreRecord)rightHandSide;
            return rhs.Score.CompareTo(this.Score);
        }
    }
}
