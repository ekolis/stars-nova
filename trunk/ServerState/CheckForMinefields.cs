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
// Check to see if a fleet is in an enemy Minefield and inflict appropriate
// damage.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Nova.Common;
using Nova.Common.DataStructures;
using Nova.Server;

namespace Nova.WinForms.Console
{
    /// <summary>
    /// Handle fleets hitting minefields.
    /// </summary>
    public class CheckForMinefields
    {
        private readonly Random random = new Random();
        private ServerState stateData;
        
        public CheckForMinefields(ServerState serverState)
        {
            this.stateData = serverState;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Do a check for minefileds.
        /// </summary>
        /// <param name="fleet">A moving fleet.</param>
        /// <returns>false</returns>
        /// ----------------------------------------------------------------------------
        public bool Check(Fleet fleet)
        {
            foreach (Minefield minefield in
                     stateData.AllMinefields.Values)
            {

                if (IsInField(fleet, minefield))
                {
                    bool hit = CheckForHit(fleet, minefield);
                    if (hit)
                    {
                        InflictDamage(fleet, minefield);
                    }
                }

                // Minefields dedcay 1% each year. Fields of less than 10 mines are
                // just not worth bothering about.
                // FIXME (priority 5) - Minefiled decay has nothing to do with moving fleets and should be processed seperately.
                // FIXME (priority 5) - Minefield decay rates depend on what is in the field (stars).
                minefield.NumberOfMines -= minefield.NumberOfMines / 100;
                if (minefield.NumberOfMines <= 10)
                {
                    stateData.AllMinefields.Remove(minefield);
                }
            }

            // FIXME (priority 6) - always returns false.
            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if a fleet is within a Minefield. The fleet is inside the
        /// circle if the distance between the field and the center of the field is
        /// less than the radius of the field.
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="minefield"></param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        private bool IsInField(Fleet fleet, Minefield minefield)
        {

            // If we are travelling at a "safe" speed we can just pretend we are
            // not in a Minefield.

            if (fleet.Speed <= minefield.SafeSpeed) return false;

            double distance = PointUtilities.Distance(fleet.Position, minefield.Position);

            if (distance < minefield.Radius)
            {
                return true;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if the fleet hits the minefiled.
        /// </summary>
        /// <remarks>
        /// The probability of hitting a mine is 0.3% per light year traveled for each
        /// warp over the safe speed.
        /// TODO (priority 3) - reference required.
        ///
        /// Example: A fleet traveling at Warp 9 has a 1.5% chance per light year
        /// traveled in a turn.  Traveling 10 light years through the Minefield that
        /// turn, the fleet has a 10.5% chance of triggering a mine.
        /// </remarks>
        /// <param name="fleet">The moving fleet.</param>
        /// <param name="minefield">The minefield being traversed.</param>
        /// <returns>true if the minefield is hit.</returns>
        /// ----------------------------------------------------------------------------
        private bool CheckForHit(Fleet fleet, Minefield minefield)
        {
            // Calculate how long we are going to be in the Minefield. This is the
            // lesser of the distance to the next waypoint and the radius of the
            // field.

            NovaPoint currentPosition = fleet.Position;
            Waypoint targetWaypoint = fleet.Waypoints[0] as Waypoint;
            NovaPoint targetPosition = targetWaypoint.Position;

            double travelDistance = PointUtilities.Distance(currentPosition, targetPosition);
            if (minefield.Radius > (int)travelDistance)
            {
                travelDistance = minefield.Radius;
            }

            double speeding = fleet.Speed - minefield.SafeSpeed;
            double probability = (0.03 * travelDistance * speeding) * 100;
            double dice = random.Next(0, 100);

            if (dice < probability)
            {
                return true;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// We've hit a mine. Inflict appropriate damage to the fleet and bring it to a
        /// stop. If all ships are gone destroy the fleet.
        ///
        /// Let's start with the simplest algoritm:
        ///
        /// 5 destroyers take 500dp damage = 100dp each = 50dp to armor, 50dp to shields
        /// (absorbed)
        /// </summary>
        /// <param name="fleet">The fleet that hit the minefield.</param>
        /// <param name="minefield">The minefield being impacted.</param>
        /// ----------------------------------------------------------------------------
        private void InflictDamage(Fleet fleet, Minefield minefield)
        {
            int shipDamage = 100 / 2;
            int shipsLost = 0;
            fleet.Speed = 0;

            ArrayList shipsToRemove = new ArrayList();

            foreach (Ship ship in fleet.FleetShips)
            {
                ship.Armor -= shipDamage;

                if (ship.Armor < 0)
                {
                    shipsToRemove.Add(ship);
                    shipsLost++;
                }
            }

            foreach (Ship removeShip in shipsToRemove)
            {
                fleet.FleetShips.Remove(removeShip);
            }

            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Event = "Minefield";
            message.Text = "Fleet " + fleet.Name
               + " has hit a Minefield." + "\n\n";

            if (shipsLost == 0)
            {
                message.Text += "None of your ships were destroyed.";
                fleet.Speed = 0;
            }
            else if (fleet.FleetShips.Count != 0)
            {
                message.Text += shipsLost.ToString(System.Globalization.CultureInfo.InvariantCulture)
                   + " of your ships were destroyed.\n";
            }
            else
            {
                message.Text += "All of your ships were destroyed.\n";
                message.Text += "You lost this fleet.";
                stateData.AllFleets.Remove(fleet.Key);
            }

            stateData.AllMessages.Add(message);
        }
    }
}
