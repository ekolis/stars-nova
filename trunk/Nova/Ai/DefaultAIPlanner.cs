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

namespace Nova.Ai
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Nova.Common.Waypoints;

 

    /// <summary>
    /// An AI sub-component to manage planning AI moves. 
    /// </summary>
    /// <remarks>
    /// The default AI is stateless - it does not persist any information between turns other than what is in an ordinary player's state.
    /// </remarks>
    public class DefaultAIPlanner
    {
        public const int EarlyScouts = 5;
        public const int LowProduction = 100;

        /// <summary>
        /// Minimum hab value for colonizing a planet.
        /// </summary>
        public const double MinHabValue = 0.05;

        public int ScoutCount = 0;
        public int ColonizerCount = 0;
        public int TransportCount = 0;
        public int BomberCount = 0;
        public int WarfleetCount = 0;

        private ClientData clientState = null;

        /// <summary>
        /// The ShipDesign to use for building scouts.
        /// </summary>
        private ShipDesign scoutDesign = null;

        /// <summary>
        /// The ShipDesign to use for building colonizers.
        /// </summary>
        private ShipDesign colonizerDesign = null;

        /// <summary>
        /// The number of scouted, unowned planets with > 5% habitability.
        /// </summary>
        /// <remarks>
        /// Initialized to -1 to flag it has not been calculated. 
        /// </remarks>
        private int planetsToColonize = -1;
        
        /// <summary>
        /// The current design to be used for building scouts.
        /// </summary>
        public ShipDesign ScoutDesign
        {
            get
            {
                if (scoutDesign == null)
                {
                    foreach (ShipDesign design in clientState.EmpireState.Designs.Values)
                    {
                        if (design.Name.Contains("Scout"))
                        {
                            scoutDesign = design;
                        }
                    }
                }
                return scoutDesign;
            }
        }

        /// <summary>
        /// The current design to be used for building colonizers.
        /// </summary>
        public ShipDesign ColonizerDesign
        {
            get
            {
                if (colonizerDesign == null)
                {
                    foreach (ShipDesign design in clientState.EmpireState.Designs.Values)
                    {
                        if (design.Name.Contains("Santa Maria"))
                        {
                            colonizerDesign = design;
                        }
                    }
                }
                return colonizerDesign;
            }
        }

        /// <summary>
        /// Track the number of known planets suitable for sending a colonizer too.
        /// </summary>
        public int PlanetsToColonize
        {
            get
            {
                if (planetsToColonize < 0)
                {
                    planetsToColonize = 0;
                    foreach (StarIntel star in clientState.EmpireState.StarReports.Values)
                    {
                        if (star.Owner == Global.Nobody && clientState.EmpireState.Race.HabitalValue(star) > DefaultAIPlanner.MinHabValue)
                        {
                            planetsToColonize++;
                        }
                    }
                }
                return planetsToColonize;
            }
        }

        /// <summary>
        /// Initializing constructor.
        /// </summary>
        public DefaultAIPlanner(ClientData newClientState)
        {
            clientState = newClientState;
        }

        /// <summary>
        /// Count and classify owned fleets.
        /// </summary>
        /// <param name="fleet"></param>
        public void CountFleet(Fleet fleet)
        {
            // Work out what we have
            if (fleet.CanColonize)
            {
                this.ColonizerCount++;
            }
            else if (fleet.Name.Contains("Scout"))
            {
                this.ScoutCount++;
            }
            else if (fleet.HasBombers)
            {
                this.BomberCount++;
            }
            else if (fleet.TotalCargoCapacity > 0)
            {
                this.TransportCount++;
            }
            else if (fleet.IsArmed)
            {
                this.WarfleetCount++;
            }
        }
    }
}
