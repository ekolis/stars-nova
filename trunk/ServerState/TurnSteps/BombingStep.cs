using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Server.TurnSteps
{
    using Nova.Common;
    using Nova.Common.Waypoints;

    class BombingStep : ITurnStep
    {
        public void Process(ServerData serverState)
        {
            Bombing bombing = new Bombing(serverState);

            foreach (Fleet fleet in serverState.IterateAllFleets())
            {
                if (fleet.InOrbit != null && fleet.HasBombers)
                {
                    bombing.Bomb(fleet, serverState.AllStars[fleet.InOrbit.Name]);
                }
            }
        }
    }
}
