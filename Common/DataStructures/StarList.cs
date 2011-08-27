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
    /// Defines a colletion of Star objects using Dictionary.
    /// Allows these to be accessed by the star's name (which is a unique key),
    /// as well as maintaining a sorted list for next/previous functionality. 
    /// </summary>
    [Serializable]
    public class StarList : Dictionary<string, Star>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public StarList()
        {
        }

        /// <summary>
        /// Add a new star to the StarList.
        /// </summary>
        /// <param name="star">The Star to be added to the StarList.</param>
        public void Add(Star star)
        {
            Add(star.Name, star);
        }

        /// <summary>
        /// Remove a Star from the StarList.
        /// </summary>
        /// <param name="star">The star to remove.</param>
        public void Remove(Star star)
        {
            Remove(star.Name);
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="star">The Star to check for.</param>
        /// <returns>true if star is in the StarList.</returns>
        public bool Contains(Star star)
        {
            if (star == null)
            {
                return false;
            }
            if (star.Name == null)
            {
                return false;
            }
            
            return ContainsKey(star.Name);
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="starName">The name of a star.</param>
        /// <returns>true if starName is the name of one of the stars in the StarList.</returns>
        public bool Contains(string starName)
        {
            return ContainsKey(starName);
        }

        /// <summary>
        /// Get the next star in the list.
        /// </summary>
        /// <param name="star">The current star.</param>
        /// <returns>The next star, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public Star GetNext(Star star)
        {
            if (star == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Count <= 1)
            {
                return star;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(star.Name) + 1;
            if (nextIndex >= keyList.Count)
            {
                nextIndex = 0;
            }
            return this[keyList[nextIndex]] as Star;
        }      
        
        /// <summary>
        /// Get the previous star in the list.
        /// </summary>
        /// <param name="star">The current star.</param>
        /// <returns>The previous star, or the current star if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public Star GetPrevious(Star star)
        {
            if (star == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Count <= 1)
            {
                return star;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(star.Name) - 1;
            if (nextIndex < 0)
            {
                nextIndex = keyList.Count - 1;
            }
            return this[keyList[nextIndex]] as Star;
        }
    }
}
