#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary> 
    /// Defines a collection of FleetIntel objects using DictionaryBase.
    /// Allows these to be accessed by the fleet's key (which is a unique key),
    /// as well as maintaining a sorted list for next/previous functionality. 
    /// </summary>
    [Serializable]
    public class FleetList : Dictionary<long, Fleet>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FleetList()
        {
        }

        /// <summary>
        /// Add a new fleet to the FleetList.
        /// </summary>
        /// <param name="fleet">The Fleet to be added to the FleetList.</param>
        public void Add(Fleet fleet)
        {
            Add(fleet.Key, fleet);
        }

        /// <summary>
        /// Remove a Fleet from the FleetList.
        /// </summary>
        /// <param name="fleet">The fleet to remove.</param>
        public void Remove(Fleet fleet)
        {
            Remove(fleet.Key);
        }

        /// <summary>
        /// Check if the fleet list contains a particular trait.
        /// </summary>
        /// <param name="fleet">The Fleet to check for.</param>
        /// <returns>true if fleet is in the FleetList.</returns>
        public bool Contains(Fleet fleet)
        {
            if (fleet == null)
            {
                return false;
            }
            if (fleet.Name == null)
            {
                return false;
            }
            
            return ContainsKey(fleet.Key);
        }

        /// <summary>
        /// Check if the fleet list contains a particular fleet.
        /// </summary>
        /// <param name="key">The name of a fleet.</param>
        /// <returns>true if key is the key of one of the fleets in the FleetList.</returns>
        public bool Contains(long key)
        {
            return ContainsKey(key);
        }

        
        /// <summary>
        /// Get the next fleet report in the list that belongs to the same owner.
        /// </summary>
        /// <param name="fleet">The current report.</param>
        /// <returns>The next fleet report, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException">If fleet is null.</exception>
        public Fleet GetNext(Fleet fleet)
        {
            if (fleet == null)
            {
                throw new ArgumentNullException("fleet");
            }
            if (Count <= 1)
            {
                return fleet;
            }

            List<long> keyList = new List<long>();
            keyList.AddRange(Keys);
            keyList.Sort();
            int nextIndex = keyList.IndexOf(fleet.Key);
            
            do
            {
                nextIndex = nextIndex + 1;
                if (nextIndex >= keyList.Count)
                {
                    nextIndex = 0;
                }
            }
            while (this[keyList[nextIndex]].Owner != fleet.Owner);
            
            return this[keyList[nextIndex]];
        }
        
        
        /// <summary>
        /// Get the previous fleet report in the list that belongs to same owner.
        /// </summary>
        /// <param name="fleet">The current report.</param>
        /// <returns>The next fleet report, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException">If fleet is null.</exception>
        public Fleet GetPrevious(Fleet fleet)
        {
            if (fleet == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Count <= 1)
            {
                return fleet;
            }

            List<long> keyList = new List<long>();
            keyList.AddRange(Keys);
            keyList.Sort();
            int nextIndex = keyList.IndexOf(fleet.Key);
            
            do
            {
                nextIndex = nextIndex - 1;
                if (nextIndex < 0)
                {
                    nextIndex = keyList.Count - 1;
                }
            }
            while (this[keyList[nextIndex]].Owner != fleet.Owner);
            
            return this[keyList[nextIndex]];
        }
    }
}
