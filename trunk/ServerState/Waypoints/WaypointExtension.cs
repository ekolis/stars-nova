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
    
    using Nova.Common.Waypoints;
    
    /// <summary>
    /// Includes server side functionality to waypoints
    /// </summary>
    public static class WaypointExtension
    {        
        public static IWaypointTaskWorker LoadWorker(this Waypoint waypoint)
        {
            switch(waypoint.Task.Name)
            {
                case "Colonise":
                    waypoint.Task = new ColoniseTaskWorker(waypoint.Task);
                    break;
                    
                case "Invade":
                    waypoint.Task = new InvadeTaskWorker(waypoint.Task);
                    break;
                    
                case "Lay Mines":
                    waypoint.Task = new LayMinesTaskWorker(waypoint.Task);
                    break;
                    
                case "Scrap":
                    waypoint.Task = new ScrapTaskWorker(waypoint.Task);
                    break;
                    
                case "Load":
                    waypoint.Task = new LoadTaskWorker(waypoint.Task);
                    break;
                    
                case "Unload":
                    waypoint.Task = new UnloadTaskWorker(waypoint.Task);
                    break;

                default:
                    waypoint.Task = new NoTaskWorker();
                    break;
            }
            
            return waypoint.Task as IWaypointTaskWorker;
        }    
    }
}
