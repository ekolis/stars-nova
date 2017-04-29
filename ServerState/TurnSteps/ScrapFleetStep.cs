using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Server.TurnSteps
{
    using Nova.Common;
    using Nova.Common.Waypoints;

    public class ScrapFleetStep : ITurnStep
    {
        public void Process(ServerData serverState)
        {
            foreach (Fleet fleet in serverState.IterateAllFleets())
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

            serverState.CleanupFleets();
        }
    }
}
