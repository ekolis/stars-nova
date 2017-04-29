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
    public class LayMinesTask : IWaypointTask
    {
        private List<Message> messages = new List<Message>();
        
        public List<Message> Messages
        {
            get{ return messages;}
        } 
        
        public string Name
        {
            get{return "Lay Mines";}
        }
        
        public LayMinesTask()
        {
             
        }
        
        /// <summary>
        /// Load: Read in a ColoniseTask from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public LayMinesTask(XmlNode node)
        {
            if (node == null)
            {
                return;
            }    
        }
        
        public bool IsValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver)
        {
            Message message = new Message();
            Messages.Add(message);            
            message.Audience = fleet.Owner;
            
            
            if (fleet.NumberOfMines == 0)
            {
                message.Text = fleet.Name + " attempted to lay mines. The order has been canceled because no ship in the fleet has a mine laying pod.";
                return false;
            }
            
            Messages.Clear();
            return true;           
        }
        
        public bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver)
        {
            // See if a Minefield is already here (owned by us). We allow a
            // certaintolerance in distance because it is unlikely that the
            // waypoint has been set exactly right.
            
            //TODO: Implement per empire minefields.
            /*foreach (Minefield minefield in serverState.AllMinefields.Values)
            {
                if (PointUtilities.IsNear(fleet.Position, minefield.Position))
                {
                    if (minefield.Owner == fleet.Owner)
                    {
                        minefield.NumberOfMines += fleet.NumberOfMines;
                        return true;
                    }
                }
            }
    
            // No Minefield found. Start a new one.
    
            Minefield newField = new Minefield();
    
            newField.Position = fleet.Position;
            newField.Owner = fleet.Owner;
            newField.NumberOfMines = fleet.NumberOfMines;
    
            serverState.AllMinefields[newField.Key] = newField;*/
            return true;
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("LayMinesTask");
            
            return xmlelTask;
        }
    }
}
