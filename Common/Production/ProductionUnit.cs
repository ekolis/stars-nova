﻿#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 stars-nova
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

namespace Nova.Common
{
    using System;
    using System.Xml;

    /// <summary>
    /// This interface is to be used in ProductionOrder for specifying what is
    /// the result of construction (a ship, a factory and so on).
    /// Generic interface for any single production unit: 1 ship, 1 factory, 
    /// 1 mine, 1% terraform, 1 alchemy and so on.
    /// The implementation should contain all the needed information 
    /// in order to perform actual construction (creating/changing game 
    /// objects).
    /// </summary>
    public interface IProductionUnit
    {
        /// <summary>
        /// The total Cost of this unit.
        /// TODO (priority 6): Maybe this could be removed. It's convenient to have
        /// this data here, but it leads to extra data on the XML files and can lead
        /// to potential exploits. -Aeglos 13 Mar 12
        /// </summary>
        Resources Cost {get;}
        
        /// <summary>
        /// Resources still needed to complete this unit
        /// </summary>
        Resources RemainingCost {get;}
        
        /// <summary>
        /// Returns this unit's name, for display on the GUI.
        /// </summary>
        string Name {get;}
        
        /// <summary>
        /// Method which checks whether another one unit can be constructed.
        /// The unit cannot be constructed either because of lack 
        /// of minerals/resources or because of other game restrictions
        /// (for example another factory cannot be constructed if maximum
        /// factory number limit is reached).
        /// </summary>
        /// <returns>Returns true in case unit can be constructed, false otherwise.</returns>
        bool IsSkipped(Star star);

        /// <summary>
        /// Method which performs actual construction.
        /// </summary>
        /// <returns>Returns true if the unit is done constructing, false otherwise</returns>
        bool Construct(Star star);
        
        /// <summary>
        /// Save: Generate an XmlElement representation of the ProductionUnit for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the ProductionQueue.Item.</returns>
        XmlElement ToXml(XmlDocument xmldoc);
    }
}
