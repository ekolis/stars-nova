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
        List<Message> Messages {get;}
        
        /// <summary>
        /// Returns this Task's name (i.e. "Colonise" or "Lay Mines").
        /// </summary>
        string Name {get;}  
        
        /// <summary>
        /// Save: Generate an XmlElement representation of the ProductionUnit for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the ProductionQueue.Item.</returns>
        XmlElement ToXml(XmlDocument xmldoc);
    }
}
