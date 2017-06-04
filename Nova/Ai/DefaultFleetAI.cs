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
    /// A sub AI to manage a <see cref="Fleet"/>.
    /// </summary>
    public class DefaultFleetAI
    {
        /// <summary>
        /// The <see cref="Fleet"/> managed by this AI.
        /// </summary>
        private Fleet fleet;

        private ClientData clientState;

        public FleetList FuelStations = null;

        /// <summary>
        /// Initializing constructor.
        /// </summary>
        /// <param name="newFleet">The Fleet to be managed.</param>
        /// <param name="newState">The AbstractAI.clientState.</param>
        public DefaultFleetAI(Fleet newFleet, ClientData newState, FleetList newFuelStations)
        {
            fleet = newFleet;
            clientState = newState;
            FuelStations = newFuelStations;
        }

        public StarIntel Scout(List<StarIntel> excludedStars)
        {
            bool missionAccepted = false;

            // Find a star to scout
            StarIntel starToScout = CloesestStar(this.fleet, excludedStars);
            if (starToScout != null)
            {
                // Do we need fuel first?
                double fuelRequired = 0.0;
                Fleet nearestFuel = ClosestFuel(fleet);
                if (!fleet.CanRefuel)
                {
                    // Can not make fuel, so how much fuel is required to scout and then refuel?
                    if (nearestFuel != null)
                    {
                        int bestWarp = 6; // FIXME (priority 4) - what speed to scout at?
                        double bestSpeed = bestWarp * bestWarp;
                        double speedSquared = bestSpeed * bestSpeed;
                        double fuelConsumption = fleet.FuelConsumption(bestWarp, clientState.EmpireState.Race);
                        double distanceSquared = PointUtilities.DistanceSquare(fleet.Position, starToScout.Position); // to the stars
                        distanceSquared += PointUtilities.DistanceSquare(starToScout.Position, nearestFuel.Position); // and back to fuel (minimum)
                        double time = Math.Sqrt(distanceSquared / speedSquared);
                        fuelRequired = time * fuelConsumption;
                    }
                    else
                    {
                        // OMG there is no fuel! - just keep scouting then?
                    }
                }


                if (fleet.FuelAvailable > fuelRequired)
                {
                    // Fuel is no problem
                    SendFleet(starToScout, fleet, new NoTask());
                    missionAccepted = true;
                }
                else
                {
                    // Refuel before scouting further
                    SendFleet(nearestFuel, fleet, new NoTask());
                }
            }

            if (missionAccepted)
            {
                return starToScout;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Send the fleet to colonise a planet.
        /// </summary>
        /// <param name="targetStar">The <see cref="StarIntel"/> for the target star to colonise.</param>
        public void Colonise(StarIntel targetStar)
        {
            // ensure we take some colonists, maybe some Germainium
            if (this.fleet.Cargo.ColonistsInKilotons < 1)
            {
                if (this.fleet.InOrbit != null && this.fleet.InOrbit.Owner == this.fleet.Owner)
                {
                    // there is one of our planets here, so try to beam up colonists
                    Star ourStar = (Star)this.fleet.InOrbit;

                    // How much germanium to load to seed factory production?
                    // 1/4 of cargo capacity, so long as we can load 100 kT of colonists
                    int germaniumToLoad = System.Math.Min(this.fleet.TotalCargoCapacity / 4, this.fleet.TotalCargoCapacity - 100);
                    // but leave at least 50 G behind
                    germaniumToLoad = System.Math.Min(germaniumToLoad, ourStar.ResourcesOnHand.Germanium - 50);
                    // do not try to load a negative number of G
                    germaniumToLoad = System.Math.Max(germaniumToLoad, 0);

                    // How many colonists to load?
                    // fill up space left after G
                    int colonistsToLoadKt = this.fleet.TotalCargoCapacity - germaniumToLoad;
                    // but do not take the Star below 250,000 (max % growth)
                    colonistsToLoadKt = System.Math.Min(colonistsToLoadKt, (ourStar.Colonists - 250000) / Nova.Common.Global.ColonistsPerKiloton);
                    // ensure we load at least 1 kT of colonists
                    colonistsToLoadKt = System.Math.Max(colonistsToLoadKt, 1);

                    // load up
                    CargoTask wpTask = new CargoTask();
                    wpTask.Mode = CargoMode.Load;
                    wpTask.Amount.ColonistsInKilotons = colonistsToLoadKt;
                    wpTask.Amount.Germanium = germaniumToLoad;

                    Waypoint wp = new Waypoint();
                    wp.Task = wpTask;
                    wp.Position = ourStar.Position;
                    wp.WarpFactor = this.fleet.FreeWarpSpeed;
                    wp.Destination = ourStar.Name;

                    WaypointCommand loadCommand = new WaypointCommand(CommandMode.Add, wp, this.fleet.Key);
                    loadCommand.ApplyToState(clientState.EmpireState);
                    clientState.Commands.Push(loadCommand);

                    SendFleet(targetStar, this.fleet, new ColoniseTask());
                }
                else
                {
                    // TODO (priority 5) - go get some colonists
                }
            }
        }

        /// <summary>
        /// Return closest star to current fleet.
        /// </summary>
        /// <param name="fleet"></param>
        /// <returns></returns>
        private StarIntel CloesestStar(Fleet fleet, List<StarIntel> excludedStars)
        {
            StarIntel target = null;
            double distance = double.MaxValue;
            foreach (StarIntel report in clientState.EmpireState.StarReports.Values)
            {
                if (excludedStars.Contains(report) != true)
                {
                    if (distance > Math.Sqrt(Math.Pow(fleet.Position.X - report.Position.X, 2) + Math.Pow(fleet.Position.Y - report.Position.Y, 2)))
                    {
                        target = report;
                        distance = Math.Sqrt(Math.Pow(fleet.Position.X - target.Position.X, 2) + Math.Pow(fleet.Position.Y - target.Position.Y, 2));
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Return the closest refuelling point.
        /// </summary>
        /// <param name="fleet">The fleet looking for fuel.</param>
        /// <returns>The closest fleet that can refuel (normally a star base).</returns>
        private Fleet ClosestFuel(Fleet customer)
        {
            if (customer == null)
            {
                return null;
            }

            // initialise the list of fuel stations, if null.
            if (FuelStations == null)
            {
                FuelStations = new FleetList();
                foreach (Fleet pump in clientState.EmpireState.OwnedFleets.Values)
                {
                    if (pump.CanRefuel)
                    {
                        FuelStations.Add(pump);
                    }
                }
            }

            // if there are still no fuel stations, bug out
            if (FuelStations.Count == 0)
            {
                return null;
            }

            Fleet closestFuelSoFar = null;
            double minRefulerDistance = double.MaxValue;

            foreach (Fleet pump in FuelStations.Values)
            {
                double distSquare = PointUtilities.DistanceSquare(pump.Position, customer.Position);
                if (distSquare < minRefulerDistance)
                {
                    minRefulerDistance = distSquare;
                    closestFuelSoFar = pump;
                }
            }

            return closestFuelSoFar;
        }


        private void SendFleet(NovaPoint position, Fleet fleet, IWaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = position;
            w.Destination = position.ToString();
            w.Task = task;

            WaypointCommand command = new WaypointCommand(CommandMode.Add, w, fleet.Key);
            command.ApplyToState(clientState.EmpireState);
            clientState.Commands.Push(command);
        }

        private void SendFleet(FleetIntel target, Fleet fleet, IWaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = target.Position;
            w.Destination = target.Name;
            w.Task = task;

            WaypointCommand command = new WaypointCommand(CommandMode.Add, w, fleet.Key);
            command.ApplyToState(clientState.EmpireState);
            clientState.Commands.Push(command);
        }

        private void SendFleet(Fleet target, Fleet fleet, IWaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = target.Position;
            w.Destination = target.Name;
            w.Task = task;

            WaypointCommand command = new WaypointCommand(CommandMode.Add, w, fleet.Key);
            command.ApplyToState(clientState.EmpireState);
            clientState.Commands.Push(command);
        }

        private void SendFleet(StarIntel star, Fleet fleet, IWaypointTask task)
        {
            Waypoint w = new Waypoint();
            w.Position = star.Position;

            w.Destination = star.Name;
            w.Task = task;

            WaypointCommand command = new WaypointCommand(CommandMode.Add, w, fleet.Key);
            command.ApplyToState(clientState.EmpireState);
            clientState.Commands.Push(command);
        }
    }
}
