#region Copyright Notice
// ============================================================================
// Copyright (C) 2009 - 2017 stars-nova
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

namespace Nova.Server.TurnSteps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Nova.Common;
    using Nova.Common.Waypoints;
    
    public class ScrapFleetStep : ITurnStep
    {
        public void Process(ServerData serverState)
        {
            foreach (Fleet fleet in serverState.IterateAllFleets())
            {
                if (fleet.Waypoints.Count > 0)
                {
                    Waypoint waypointZero = fleet.Waypoints[0];
                    if (waypointZero.Task is ScrapTask && waypointZero.Task.IsValid(fleet, null, null))
                    {
                        Star targetStar = null;
                        serverState.AllStars.TryGetValue(waypointZero.Destination, out targetStar);
                        if (targetStar != null)
                        {
                            fleet.InOrbit = targetStar;
                        }

                        EmpireData sender = serverState.AllEmpires[fleet.Owner];

                        waypointZero.Task.Perform(fleet, targetStar, sender);
                    }
                }
            }

            serverState.CleanupFleets();
        }
    }
}
