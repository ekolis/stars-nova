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

using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

using Nova.Common;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Deal with bombing
    /// </summary>
    public static class Bombing
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// See if we can bomb the planet.
        /// </summary>
        /// <param name="fleet">Potential bombing fleet.</param>
        /// <param name="star">Potential bombing target.</param>
        /// ----------------------------------------------------------------------------
        public static void Bomb(Fleet fleet, Star star)
        {
            // The fleet is in orbit around a planet. If it has no colonists or
            // has a starbase then to do nothing here (we'll leave anything else
            // to the battle engine.

            if (star.Colonists == 0 || star.Starbase != null)
            {
                return;
            }

            // See if this is an enemy planet. If not, leave it alone.

            if (Players.AreEnemies(fleet.Owner, star.Owner) == false)
            {
                return;
            }

            // If we don't have bombers then there is nothing more to do here

            if (fleet.HasBombers == false)
            {
                return;
            }

            Defenses.ComputeDefenseCoverage(star);
            BombColonists(fleet, star);

            if (star.Owner != null)
            {
                BombInstallations(fleet, star);
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Bomb the colonists. Algorithms:
        /// Normalpopkills = sum[bomb_kill_perc(n)*#(n)] * (1-Def(pop))
        /// Minkills = sum[bomb_kill_min(n)*#(n)] * (1-Def(pop))
        /// </summary>
        /// <param name="fleet">Bombing fleet.</param>
        /// <param name="star">Target star.</param>
        /// ----------------------------------------------------------------------------
        private static void BombColonists(Fleet fleet, Star star)
        {
            Bomb totalBombs       = fleet.BombCapability;

            double killFactor     = totalBombs.PopKill / 100.0;
            double DefenseFactor  = 1.0 - Defenses.PopulationCoverage;
            double populationKill = killFactor * DefenseFactor;
            double killed         = (double)star.Colonists * populationKill;

            double minKilled      = totalBombs.MinimumKill
                                  * (1 - Defenses.PopulationCoverage);

            double dead           = Math.Max(killed, minKilled);
            star.Colonists       -= (int)dead;

            StringBuilder text    = new StringBuilder();
            text.Append("Fleet " + fleet.Name + " has killed ");

            if (star.Colonists > 0)
            {
                text.Append(killed.ToString(System.Globalization.CultureInfo.InvariantCulture) + "of the colonists ");
            }
            else
            {
                text.Append("all of the colonists ");
            }

            text.Append("on " + star.Name);

            Message lambMessage  = new Message();
            lambMessage.Text     = text.ToString();
            lambMessage.Audience = star.Owner;
            ServerState.Data.AllMessages.Add(lambMessage);

            Message wolfMessage  = new Message();
            wolfMessage.Audience = fleet.Owner;
            wolfMessage.Text     = text.ToString();
            ServerState.Data.AllMessages.Add(wolfMessage);

            if (star.Colonists <= 0)
            {
                star.Colonists = 0;
                star.Mines     = 0;
                star.Factories = 0;
                star.Owner     = null;
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Bomb a planet's installations
        /// </summary>
        /// <param name="fleet">Bombing fleet.</param>
        /// <param name="star">Target star.</param>
        /// ----------------------------------------------------------------------------
        private static void BombInstallations(Fleet fleet, Star star)
        {
            Bomb bomb = fleet.BombCapability;

            double totalBuildings = star.Mines + star.Factories + star.Defenses;


            double buildingKills = bomb.Installations * (1 - Defenses.BuildingCoverage);
            double damagePercent = buildingKills / totalBuildings;

            if (damagePercent > 1)
            {
                damagePercent = 1;
            }

            // We now have the percentage of each building type to destroy (which
            // has been clamped at a maximum of 100% (normalised so that 100% =
            // 1). Let's apply that percentage to each building type in
            // turn. First Defenses:

            string prefix = "Fleet " + fleet.Name + " has destroyed ";
            string suffix = "on " + star.Owner;

            // Defenses

            int loss = (int)((double)star.Defenses * damagePercent);
            star.Defenses -= loss;

            StringBuilder text = new StringBuilder();
            text.Append(prefix + loss.ToString(System.Globalization.CultureInfo.InvariantCulture) + " Defenses " + suffix);

            Message lambDefenses  = new Message();
            lambDefenses.Text     = text.ToString();
            lambDefenses.Audience = star.Owner;
            ServerState.Data.AllMessages.Add(lambDefenses);

            Message wolfDefenses  = new Message();
            wolfDefenses.Audience = fleet.Owner;
            wolfDefenses.Text     = text.ToString();
            ServerState.Data.AllMessages.Add(wolfDefenses);

            // Now Factories

            double factories = (double)star.Factories;
            loss = (int)(factories * damagePercent);

            star.Factories -= loss;
            text = new StringBuilder();
            text.Append(prefix + loss.ToString(System.Globalization.CultureInfo.InvariantCulture) + " factories " + suffix);

            Message lambFactories  = new Message();
            lambFactories.Text     = text.ToString();
            lambFactories.Audience = star.Owner;
            ServerState.Data.AllMessages.Add(lambFactories);

            Message wolfFactories  = new Message();
            wolfFactories.Audience = fleet.Owner;
            wolfFactories.Text     = text.ToString();
            ServerState.Data.AllMessages.Add(wolfFactories);

            // Now Mines

            double mines = (double)star.Mines;
            loss = (int)(mines * damagePercent);

            star.Mines -= loss;
            text = new StringBuilder();
            text.Append(prefix + loss.ToString(System.Globalization.CultureInfo.InvariantCulture) + " mines " + suffix);

            Message lambMines  = new Message();
            lambMines.Text     = text.ToString();
            lambMines.Audience = star.Owner;
            ServerState.Data.AllMessages.Add(lambMines);

            Message wolfMines  = new Message();
            wolfMines.Text     = text.ToString();
            wolfMines.Audience = fleet.Owner;
            ServerState.Data.AllMessages.Add(wolfMines);
        }
    }
}

