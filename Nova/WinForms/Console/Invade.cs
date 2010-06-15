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
// This module is invoked when a planet is to be invaded
// ===========================================================================
#endregion

using System;
using Nova.Common;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class invade a planet
    /// </summary>
    public static class Invade
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Invade a star system.
        /// </summary>
        /// <param name="fleet">The invading fleet.</param>
        /// ----------------------------------------------------------------------------
        public static void Planet(Fleet fleet)
        {
            // First check that we are actuallly in orbit around a planet.

            if (fleet.InOrbit == null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade but the waypoint is not a planet";
                ServerState.Data.AllMessages.Add(message);
                return;
            }

            // and that we have troops.

            int troops = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
            fleet.Cargo.ColonistsInKilotons = 0; // unload all troops, they will fight to the death or become new colonists.
            Star star = fleet.InOrbit;

            if (troops == 0)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade " + star.Name + " but there are no troops on board";
                ServerState.Data.AllMessages.Add(message);
                return;
            }

            // Take into account the Defenses
            Defenses.ComputeDefenseCoverage(star);
            troops = (int)(troops * (1.0 - Defenses.InvasionCoverage));

            // Apply defender and attacker bonuses
            double attackerBonus = 1.1;
            if (((Race)ServerState.Data.AllRaces[fleet.Owner]).HasTrait("WM"))
                attackerBonus *= 1.5;

            double defenderBonus = 1.0;
            if (((Race)ServerState.Data.AllRaces[fleet.Owner]).HasTrait("IS"))
                defenderBonus *= 2.0;

            int defenderStrength = (int)(star.Colonists * defenderBonus);
            int attackerStrength = (int)(troops * attackerBonus);
            int survivorStrength = defenderStrength - attackerStrength; // will be negative if attacker wins

            string messageText = fleet.Owner + " fleet " + fleet.Name + " attacked " +
                                 star.Name + " with " + troops.ToString(System.Globalization.CultureInfo.InvariantCulture) + " troops. ";

            if (survivorStrength > 0)
            {
                // defenders win
                int remainingDefenders = (int)(survivorStrength / defenderBonus);
                remainingDefenders = Math.Max(remainingDefenders, Global.ColonistsPerKiloton);
                int defendersKilled = star.Colonists - remainingDefenders;
                star.Colonists = remainingDefenders;

                messageText += "The attackers were slain but "
                            + defendersKilled.ToString(System.Globalization.CultureInfo.InvariantCulture) +
                            " colonists were killed in the attack.";

                Message wolfMessage = new Message();
                wolfMessage.Audience = fleet.Owner;
                wolfMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(wolfMessage);

                Message lambMessage = new Message();
                lambMessage.Audience = star.Owner;
                lambMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(lambMessage);
            }
            else if (survivorStrength < 0)
            {
                // attacker wins
                int remainingAttackers = (int)(survivorStrength / defenderBonus);
                remainingAttackers = Math.Max(remainingAttackers, Global.ColonistsPerKiloton);
                int attackersKilled = troops - remainingAttackers;
                star.Colonists = remainingAttackers;
                star.Owner = fleet.Owner;

                messageText += "The defenders were slain but "
                            + attackersKilled.ToString(System.Globalization.CultureInfo.InvariantCulture) +
                            " troops were killed in the attack.";

                Message wolfMessage = new Message();
                wolfMessage.Audience = fleet.Owner;
                wolfMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(wolfMessage);

                Message lambMessage = new Message();
                lambMessage.Audience = star.Owner;
                lambMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(lambMessage);
            }
            else
            {
                // no survivors!
                messageText += "Both sides fought to the last and none were left to claim the planet!";

                Message wolfMessage = new Message();
                wolfMessage.Audience = fleet.Owner;
                wolfMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(wolfMessage);

                Message lambMessage = new Message();
                lambMessage.Audience = star.Owner;
                lambMessage.Text = messageText;
                ServerState.Data.AllMessages.Add(lambMessage);

                // clear out the colony
                star.Colonists = 0;
                star.Mines = 0;
                star.Factories = 0;
                star.Owner = null;
            }

        }

    }
}


