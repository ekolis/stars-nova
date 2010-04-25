#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
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
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This module deals with fleets laying mines. 
// ===========================================================================
#endregion

using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

using NovaCommon;
using NovaServer;

namespace NovaConsole
{

    /// <summary>
    /// Deal with mine layings
    /// </summary>
    public static class LayMines
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if we can lay mines here. If so, lay them. If there is no Minefield here
        /// start a new one. 
        /// </summary>
        /// <param name="fleet">A (potential) mine laying fleet.</param>
        /// ----------------------------------------------------------------------------
        public static void DoMines(Fleet fleet)
        {
            if (fleet.NumberOfMines == 0)
            {
                return;
            }

            // See if a Minefield is already here (owned by us). We allow a
            // certaintolerance in distance because it is unlikely that the
            // waypoint has been set exactly right.

            foreach (Minefield Minefield in ServerState.Data.AllMinefields.Values)
            {
                if (PointUtilities.IsNear(fleet.Position, Minefield.Position))
                {
                    if (Minefield.Owner == fleet.Owner)
                    {
                        Minefield.NumberOfMines += fleet.NumberOfMines;
                        return;
                    }
                }
            }

            // No Minefield found. Start a new one.

            Minefield newField = new Minefield();

            newField.Position = fleet.Position;
            newField.Owner = fleet.Owner;
            newField.NumberOfMines = fleet.NumberOfMines;

            ServerState.Data.AllMinefields[newField] = newField;
        }


    }
}

