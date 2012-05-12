#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.Common.Waypoints
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.DataStructures;
    
    /// <summary>
    /// Performs Star Colonisation.
    /// </summary>
    public class InvadeTask : IWaypointTask
    {
        private List<Message> messages = new List<Message>();
        
        public List<Message> Messages
        {
            get{ return messages;}
        }
        
        public string Name
        {
            get{return "Invade";}
        }
        
        public InvadeTask()
        {
             
        }
        
        /// <summary>
        /// Load: Read in a ColoniseTask from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public InvadeTask(XmlNode node)
        {
            if (node == null)
            {
                return;
            }    
        }
        
        public bool isValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Message message = new Message();
            Messages.Add(message);
            
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Name + " has waypoint orders to invade ";
            
            // First check that we are actuallly in orbit around a planet.

            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                message.Text += "but the target is not a planet.";
                return false;
            }

            // and that we have troops.

            if (fleet.Cargo.ColonistsInKilotons == 0)
            {
                message.Text += "but there are no troops on board.";
                return false;
            }
    
            Star star = (Star)target;
            
            // Consider the diplomatic situation
            if (fleet.Owner == star.Owner)
            {
                // already own this planet, so colonists can beam down safely
                star.Colonists += fleet.Cargo.ColonistNumbers;
                fleet.Cargo.ColonistsInKilotons = 0;
                
                message.Text += star.Name + " but it is already ours. Troops have joined the local populace.";
                return false;
            }
            
            if (star.Owner == Global.Nobody)
            {
                // This star has not been colonised. Can't invade.
                message.Text += star.Name + " but it is not colonised. You must send a ship with a colony module and orders to colonise to take this system.";
                return false;
            }
            
            PlayerRelation relation = sender.EmpireReports[star.Owner].Relation;
            
            switch (relation)
            {
                case PlayerRelation.Friend:
                case PlayerRelation.Neutral:
                    {
                        message.Text += star.Name + " but the " + sender.EmpireReports[star.Owner].RaceName + " are not our enemies. Order has been cancelled.";
                        return false;
                    }
                case PlayerRelation.Enemy:
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
                message.Text += star.Name + " but the starbase at " + star.Name + " would kill all invading troops. Order has been cancelled.";
                return false;
            }

            return true;          
        }
        
        public bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = (Star)target;
            
            // The troops are now committed to take the star or die trying
            int troops = fleet.Cargo.ColonistNumbers; 
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
            if (sender.Race.HasTrait("WM"))
            {
                attackerBonus *= 1.5;
            }

            double defenderBonus = 1.0;
            if (reciever.Race.HasTrait("IS"))
            {
                defenderBonus *= 2.0;
            }

            int defenderStrength = (int)(star.Colonists * defenderBonus);
            int attackerStrength = (int)(troopsOnGround * attackerBonus);
            int survivorStrength = defenderStrength - attackerStrength; // will be negative if attacker wins

            string messageText = fleet.Owner + "'s fleet " + fleet.Name + " attacked " +
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
                Messages.Add(wolfMessage);

                lambMessage.Text = messageText;
                Messages.Add(lambMessage);
            }
            else if (survivorStrength < 0)
            {
                // attacker wins
                star.ManufacturingQueue.Clear();
                int remainingAttackers = (int)(-survivorStrength / attackerBonus);
                remainingAttackers = Math.Max(remainingAttackers, Global.ColonistsPerKiloton);
                int attackersKilled = troops - remainingAttackers;
                star.Colonists = remainingAttackers;
                
                reciever.OwnedStars.Remove(star);
                star.Owner = fleet.Owner;
                sender.OwnedStars.Add(star);
                sender.StarReports[star.Key].Update(star, ScanLevel.Owned, sender.TurnYear);
                
                messageText += "The defenders were slain but "
                            + attackersKilled +
                            " troops were killed in the attack.";

                wolfMessage.Text = messageText;
                Messages.Add(wolfMessage);

                lambMessage.Text = messageText;
                Messages.Add(lambMessage);
            }
            else
            {
                // no survivors!
                messageText += "Both sides fought to the last and none were left to claim the planet!";

                wolfMessage.Text = messageText;
                Messages.Add(wolfMessage);

                lambMessage.Text = messageText;
                Messages.Add(lambMessage);

                // clear out the colony
                star.ManufacturingQueue.Clear();
                star.Colonists = 0;
                star.Mines = 0;
                star.Factories = 0;
                star.Owner = Global.Nobody;
            }
            
            return true; 
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("InvadeTask");
            
            return xmlelTask;
        }
    }
}
