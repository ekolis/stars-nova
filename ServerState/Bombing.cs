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
// This module deals with fleets bombing a planet. 
// ===========================================================================
#endregion

using System;

using Nova.Common;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Deal with bombing
    /// </summary>
    public class Bombing
    {
        private ServerState stateData;
        
        public Bombing(ServerState serverState)
        {
            this.stateData = serverState;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if we can bomb the planet.
        /// </summary>
        /// <param name="fleet">Potential bombing fleet.</param>
        /// <param name="star">Potential bombing target.</param>
        /// ----------------------------------------------------------------------------
        public void Bomb(Fleet fleet, Star star)
        {
            // The fleet is in orbit around a planet. If it has no colonists or
            // has a starbase then to do nothing here (we'll leave anything else
            // to the battle engine.

            if (star.Colonists == 0 || star.Starbase != null)
            {
                return;
            }

            // See if this is an enemy planet. If not, leave it alone.

            if (!(stateData.AllRaceData[fleet.Owner] as RaceData).IsEnemy(star.Owner))
            {
                return;
            }

            // If we don't have bombers then there is nothing more to do here

            if (fleet.HasBombers == false)
            {
                return;
            }

            // Get the summary information
            Defenses.ComputeDefenseCoverage(star);
            Bomb totalBombs = fleet.BombCapability;


            // Bomb colonists
            double killFactor = totalBombs.PopKill / 100.0;
            double defenseFactor = 1.0 - Defenses.PopulationCoverage;
            double populationKill = killFactor * defenseFactor;
            double killed = (double)star.Colonists * populationKill;

            double minKilled = totalBombs.MinimumKill
                                  * (1 - Defenses.PopulationCoverage);

            int dead = (int)Math.Max(killed, minKilled);
            star.Colonists -= dead;


            // Get installation details
            double totalBuildings = star.Mines + star.Factories + star.Defenses;

            double buildingKills = totalBombs.Installations * (1 - Defenses.BuildingCoverage);
            double damagePercent = buildingKills / totalBuildings;

            if (damagePercent > 1)
            {
                damagePercent = 1;
            }

            // We now have the percentage of each building type to destroy (which
            // has been clamped at a maximum of 100% (normalised so that 100% =
            // 1). Let's apply that percentage to each building type in
            // turn. First Defenses:

            // Defenses
            int defensesDestroyed = (int)((double)star.Defenses * damagePercent);
            star.Defenses -= defensesDestroyed;

            // Now Factories
            double factories = (double)star.Factories;
            int factoriesDestroyed = (int)(factories * damagePercent);
            star.Factories -= factoriesDestroyed;

            // Now Mines
            double mines = (double)star.Mines;
            int minesDestroyed = (int)(mines * damagePercent);
            star.Mines -= minesDestroyed;

            // Build message


            string messageText = "Fleet " + fleet.Name + " has bombed " + star.Name;


            if (star.Colonists > 0)
            {
                messageText += " killing " + dead.ToString(System.Globalization.CultureInfo.InvariantCulture) +
                               " of the colonists and destroying " +
                               defensesDestroyed + " defenses, " +
                               factoriesDestroyed + " factories, and " +
                               minesDestroyed + " mines.";
            }
            else
            {
                messageText += " killing all of the colonists.";

                // clear out the colony
                star.ManufacturingQueue.Queue.Clear();
                star.Colonists = 0;
                star.Mines = 0;
                star.Factories = 0;
                star.Owner = null;
            }

            Message lamb = new Message();
            lamb.Text = messageText;
            lamb.Audience = star.Owner;
            stateData.AllMessages.Add(lamb);

            Message wolf = new Message();
            wolf.Text = messageText;
            wolf.Audience = fleet.Owner;
            stateData.AllMessages.Add(wolf);

        }

    }
}

