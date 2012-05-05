using System.Linq;
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
    
    /// <summary>
    /// Performs Star Colonisation.
    /// </summary>
    public class ColoniseTask : IWaypointTask
    {
        private List<Message> messages = new List<Message>();
        
        public List<Message> Messages
        {
            get{ return messages;}
        }
        
        public string Name
        {
            get{return "Colonise";}
        }
        
        public ColoniseTask()
        {
             
        }
        
        /// <summary>
        /// Load: Read in a ColoniseTask from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public ColoniseTask(XmlNode node)
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
            message.Text = fleet.Name + " attempted to colonise ";
            
            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                message.Text += "something that is not a star.";
                return false;
            }
            
            Star star = (Star)target;
            message.Text += target.Name;
            
            if (star.Colonists != 0)
            {
                message.Text += " but it is already occupied.";
                return false;
            }
            
            if (fleet.Cargo.Colonists == 0)
            {
                message.Text += " but no colonists were on board.";
                return false;
            }
            
            if (fleet.CanColonize == false)
            {
                message.Text += " but no ships with colonization module were present.";
                return false;
            }
            
            Messages.Clear();
            return true;           
        }
        
        public bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = target as Star;
            
            Message message = new Message();            
            message.Audience = fleet.Owner;            
            message.Text = " You have colonised " + star.Name + ".";
            Messages.Add(message);

            star.ResourcesOnHand = fleet.Cargo.ToResource();
            star.Colonists = fleet.Cargo.ColonistNumbers;
            star.Owner = fleet.Owner;
            
            fleet.TotalCost.Energy = 0;            
            star.ResourcesOnHand += fleet.TotalCost * 0.75;
            
            fleet.Tokens.Clear();
            
            sender.OwnedStars.Add(star);
            sender.StarReports[star.Name].Update(star, ScanLevel.Owned, sender.TurnYear);
            
            return true;
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("ColoniseTask");
            
            return xmlelTask;
        }
    }
}
