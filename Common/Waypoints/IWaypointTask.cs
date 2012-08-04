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
    /// Interface for common Waypoint Tasks functionality
    /// </summary>
    public interface IWaypointTask
    {
        /// <summary>
        /// Returns the messages generated during validation and execution
        /// of the Task.
        /// </summary>
        List<Message> Messages {get;}
        
        /// <summary>
        /// Returns this Task's name (i.e. "Colonise" or "Lay Mines").
        /// </summary>
        string Name {get;}  
        
        /// <summary>
        /// Checks if this task can be legally performed
        /// </summary>
        /// <param name="fleet">The fleet that carries out the Task</param>
        /// <param name="target">The Mappable (Fleet/Star) target of the Task</param>
        /// <param name="sender">The EmpireData that owns the fleet</param>
        /// <param name="receiver">The EmpireData (if any) that owns the target</param>
        /// <returns>True if the task can be performed</returns>
        bool isValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver = null);
        
        /// <summary>
        /// Performs the Waypoint Task at hand.
        /// </summary>
        /// <param name="fleet">The fleet that carries out the Task</param>
        /// <param name="target">The Mappable (Fleet/Star) target of the Task</param>
        /// <param name="sender">The EmpireData that owns the fleet</param>
        /// <param name="receiver">The EmpireData (if any) that owns the target</param>
        /// <returns>True if the task was succesful</returns>
        bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver = null);   
        
        /// <summary>
        /// Save: Generate an XmlElement representation of this object for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of this object.</returns>
        XmlElement ToXml(XmlDocument xmldoc);
    }
}
