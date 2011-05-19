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
    public class Invade
    {
        private ServerState stateData;
        
        public Invade(ServerState serverState)
        {
            this.stateData = serverState;
        }
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// invade a star system.
        /// </summary>
        /// <param name="fleet">The invading fleet.</param>
        /// ----------------------------------------------------------------------------
        public void Planet(Fleet fleet)
        {
            // First check that we are actuallly in orbit around a planet.

            if (fleet.InOrbit == null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade but the waypoint is not a planet";
                stateData.AllMessages.Add(message);
                return;
            }

            // and that we have troops.

            int troops = fleet.Cargo.ColonistsInKilotons * Global.ColonistsPerKiloton;
            Star star = fleet.InOrbit;

            if (troops == 0)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade " + star.Name + " but there are no troops on board";
                stateData.AllMessages.Add(message);
                return;
            }

            // Consider the diplomatic situation
            if (fleet.Owner == star.Owner)
            {
                // already own this planet, so colonists can beam down safely
                star.Colonists += troops;
                fleet.Cargo.ColonistsInKilotons = 0;
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                               "invade " + star.Name +
                               " but it is already ours. Troops have joined the local populace.";
                stateData.AllMessages.Add(message);
                return;
            }
            string relation = (stateData.AllRaceData[fleet.Owner] as RaceData).PlayerRelations[star.Owner];
            switch (relation)
            {
                case "Friend":
                case "Neutral":
                    {
                        Message message = new Message();
                        message.Audience = fleet.Owner;
                        message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                                       "invade " + star.Name +
                                       " but the " + star.Owner + " are not our enemies. Order has been cancelled.";
                        stateData.AllMessages.Add(message);
                        return;
                    }
                case "Enemy":
                    {
                        // continue with the invasion
                        break;
                    }
                default:
                    {
                        Report.Error("An unrecognised relationship \"" + relation + "\" was encountered. Invasion of " + star.Name + " has been cancelled.");
                        break;
                    }

            }

            // check for starbase
            if (star.Starbase != null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to " +
                               "invade " + star.Name +
                               " but the starbase at " + star.Name + " would kill all invading troops. Order has been cancelled.";
                stateData.AllMessages.Add(message);
                return;
            }

            // The troops are now committed to take the star or die trying
            fleet.Cargo.ColonistsInKilotons = 0; 

            // Set up the message recipients before the star (potentially) changes hands.
            Message wolfMessage = new Message();
            wolfMessage.Audience = fleet.Owner;
            Message lambMessage = new Message();
            lambMessage.Audience = star.Owner;

            // Take into account the Defenses
            Defenses.ComputeDefenseCoverage(star);
            int troopsOnGround = (int)(troops * (1.0 - Defenses.InvasionCoverage));

            // Apply defender and attacker bonuses
            double attackerBonus = 1.1;
            if ((stateData.AllRaces[fleet.Owner] as Race).HasTrait("WM"))
            {
                attackerBonus *= 1.5;
            }

            double defenderBonus = 1.0;
            if ((stateData.AllRaces[fleet.Owner] as Race).HasTrait("IS"))
            {
                defenderBonus *= 2.0;
            }

            int defenderStrength = (int)(star.Colonists * defenderBonus);
            int attackerStrength = (int)(troopsOnGround * attackerBonus);
            int survivorStrength = defenderStrength - attackerStrength; // will be negative if attacker wins

            string messageText = fleet.Owner + " fleet " + fleet.Name + " attacked " +
                                 star.Name + " with " + troops + " troops. ";

            if (survivorStrength > 0)
            {
                // defenders win
                int remainingDefenders = (int)(survivorStrength / defenderBonus);
                remainingDefenders = Math.Max(remainingDefenders, Global.ColonistsPerKiloton);
                int defendersKilled = star.Colonists - remainingDefenders;
                star.Colonists = remainingDefenders;

                messageText += "The attackers were slain but "
                            + defendersKilled +
                            " colonists were killed in the attack.";

                wolfMessage.Text = messageText;
                stateData.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                stateData.AllMessages.Add(lambMessage);
            }
            else if (survivorStrength < 0)
            {
                // attacker wins
                star.ManufacturingQueue.Queue.Clear();
                int remainingAttackers = (int)(-survivorStrength / attackerBonus);
                remainingAttackers = Math.Max(remainingAttackers, Global.ColonistsPerKiloton);
                int attackersKilled = troops - remainingAttackers;
                star.Colonists = remainingAttackers;
                // star.Owner = fleet.Owner; // FIXME (priority 4) - This doesn't work. Is star a copy?
                (stateData.AllStars[star.Name] as Star).Owner = fleet.Owner;

                messageText += "The defenders were slain but "
                            + attackersKilled +
                            " troops were killed in the attack.";

                wolfMessage.Text = messageText;
                stateData.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                stateData.AllMessages.Add(lambMessage);
            }
            else
            {
                // no survivors!
                messageText += "Both sides fought to the last and none were left to claim the planet!";

                wolfMessage.Text = messageText;
                stateData.AllMessages.Add(wolfMessage);

                lambMessage.Text = messageText;
                stateData.AllMessages.Add(lambMessage);

                // clear out the colony
                star.ManufacturingQueue.Queue.Clear();
                star.Colonists = 0;
                star.Mines = 0;
                star.Factories = 0;
                star.Owner = null;
            }

        }

    }
}


