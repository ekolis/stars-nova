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

using Nova.Common;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Deal with mine layings
    /// </summary>
    public class LayMines
    {
        private ServerState StateData;
        
        public LayMines(ServerState serverState)
        {
            this.StateData = serverState;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if we can lay mines here. If so, lay them. If there is no Minefield here
        /// start a new one. 
        /// </summary>
        /// <param name="fleet">A (potential) mine laying fleet.</param>
        /// ----------------------------------------------------------------------------
        public void DoMines(Fleet fleet)
        {
            if (fleet.NumberOfMines == 0)
            {
                return;
            }

            // See if a Minefield is already here (owned by us). We allow a
            // certaintolerance in distance because it is unlikely that the
            // waypoint has been set exactly right.

            foreach (Minefield minefield in StateData.AllMinefields.Values)
            {
                if (PointUtilities.IsNear(fleet.Position, minefield.Position))
                {
                    if (minefield.Owner == fleet.Owner)
                    {
                        minefield.NumberOfMines += fleet.NumberOfMines;
                        return;
                    }
                }
            }

            // No Minefield found. Start a new one.

            Minefield newField = new Minefield();

            newField.Position = fleet.Position;
            newField.Owner = fleet.Owner;
            newField.NumberOfMines = fleet.NumberOfMines;

            StateData.AllMinefields[newField] = newField;
        }


    }
}

