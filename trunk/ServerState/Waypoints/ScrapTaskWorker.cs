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
    public class ScrapTaskWorker : ScrapTask, IWaypointTaskWorker
    {
        public ScrapTaskWorker(IWaypointTask task)
        {
             
        }
        
        /// <summary>
        /// Load: Read in a ColoniseTask from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public ScrapTaskWorker(XmlNode node) : base(node)
        {
            
        }

        public bool isValid(Fleet fleet, Mappable target, ServerData serverState)
        { 
            return true;           
        }
        
        /// <summary>
        /// Scrap a fleet (fleets include starbases).
        /// </summary>
        /// <param name="fleet"></param>
        /// <param name="star"></param>
        /// <remarks>
        /// The minerals depositied are:
        /// 
        /// Colonisation                        - 75% (Handled in ColoniseWorkerTask)
        /// Scrap at a starbase                 - 80%
        /// Scrap at a planet without a stabase - 33%
        /// Scrap in space                      - 0%
        ///
        /// If the secondary trait Ulitmate Recycling has been selected when you scrap a
        /// fleet at a starbase, you recover 90% of the minerals and 70% of the
        /// resources used to produce the fleet. The resources are available or use the
        /// next year. Scrapping at a planet gives you 45% of the minerals and 35% of
        /// the resources.These resources are not strictly additive.
        /// </remarks>
        public bool Perform(Fleet fleet, Mappable target, ServerData serverState)
        {
            Message message = new Message();
            Messages.Add(message);
            message.Audience = fleet.Owner;
            message.Text = fleet.Name + " has been scrapped";

            double amount = 0;
            EmpireData empire = serverState.AllEmpires[fleet.Owner];
            double resources = 0;

            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                // TODO (priority 4) - create a scrap packet in space
                return false;
            }
            else
            {
                Star star = (Star)target;
                
                if (empire.Race.HasTrait("UR"))
                {
                    if (star.Starbase != null)
                    {
                        amount = 90;
                        resources = 70;
                    }
                    else
                    {
                        amount = 45;
                    }
                }
                else
                {
                    if (star.Starbase != null)
                    {
                        amount = 80;
                    }
                    else
                    {
                        amount = 33;
                    }
                }

                double totalResources = fleet.TotalCost.Energy * resources/100;
                Resources returned = fleet.TotalCost;
                returned *= amount/100;
                fleet.TotalCost.Energy = (int)totalResources;
                star.ResourcesOnHand += fleet.TotalCost;
            }
            
            fleet.Tokens.Clear(); // disapear the ships. The (now empty) fleet will be cleaned up latter.
            return true;
        }
    }
}
