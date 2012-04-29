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

namespace Nova.Server.Waypoints
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Waypoints;
    
    /// <summary>
    /// Performs Star Colonisation.
    /// </summary>
    public class UnloadTaskWorker : UnloadTask, IWaypointTaskWorker
    {
        public UnloadTaskWorker(IWaypointTask task)
        {
            Amount = ((UnloadTask)task).Amount;
        }
        
        /// <summary>
        /// Load: Read in a ProductionUnit from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public UnloadTaskWorker(XmlNode node) : base(node)
        {
            
        }

        public bool isValid(Fleet fleet, Mappable target, ServerData serverState)
        {
            Message message = new Message();
            Messages.Add(message);
            
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Name;
            
            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                message.Text += " attempted to unload cargo while not in orbit.";
                return false;
            }
            
            Messages.Clear();
            return true;           
        }
        
        public bool Perform(Fleet fleet, Mappable target, ServerData serverState)
        {
            Star star = (Star)target;
            
            Message message = new Message();
            Messages.Add(message);
            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at " + star.Name + "";

            star.ResourcesOnHand += fleet.Cargo.ToResource();

            // check if this is normal transportation or an invasion
            if (fleet.Owner != star.Owner && fleet.Cargo.ColonistsInKilotons != 0)
            {
                IWaypointTaskWorker invade = new InvadeTaskWorker();
                EmpireData empire = serverState.AllEmpires[fleet.Owner];
                
                if (invade.isValid(fleet, target, serverState))
                {
                    invade.Perform(fleet, target, serverState);
                    Messages.AddRange(invade.Messages);
                }
                
            }
            else
            {
                star.Colonists += fleet.Cargo.Colonists;
            }
            
            fleet.Cargo.Clear();
            
            return true;
        }        
    }
}
