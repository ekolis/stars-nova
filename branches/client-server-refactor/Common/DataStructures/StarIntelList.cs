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
    /// Defines a colletion of StarIntel objects using DictionaryBase.
    /// Allows these to be accessed by the star's name (which is a unique key),
    /// as well as maintaining a sorted list for next/previous functionality. 
    /// </summary>
    [Serializable]
    public class StarIntelList : DictionaryBase
    {
        /// /// <summary>
        /// default constructor
        /// </summary>
        public StarIntelList()
        {
        }

        /// <summary>
        /// Add a new star to the StarList
        /// </summary>
        /// <param name="report">The Star to be added to the StarList</param>
        public void Add(StarIntel report)
        {
            Dictionary.Add(report.Name, report);
        }

        /// <summary>
        /// Remove a Star from the StarList
        /// </summary>
        /// <param name="report">The star to remove.</param>
        public void Remove(StarIntel report)
        {
            Dictionary.Remove(report.Name);
        }

        /// <summary>
        /// Remove a star from the StarList
        /// </summary>
        /// <param name="starName">The name of the star.</param>
        public void Remove(string starName)
        {
            if (Dictionary.Contains(starName))
            {
                Dictionary.Remove(starName);
            }

        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="star">The Star to check for</param>
        /// <returns>true if star is in the StarList</returns>
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
            
            return Dictionary.Contains(star.Name);
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="starName">The name of a star.</param>
        /// <returns>true if starName is the name of one of the stars in the StarList</returns>
        public bool Contains(string starName)
        {
            return Dictionary.Contains(starName);
        }

        /// <summary>
        /// Allow array type indexing to a StarList.
        /// </summary>
        /// <param name="index">The name of the star.</param>
        /// <returns></returns>
        public StarIntel this[string index]
        {
            get
            {
                if (!Dictionary.Contains(index))
                {
                    return null;
                }
                return Dictionary[index] as StarIntel;
            }
            set
            {
                if (!Dictionary.Contains(index))
                {
                    return;
                }
                Dictionary[index] = value;
            }
        }

        /// <summary>
        /// Get the next star report in the list.
        /// </summary>
        /// <param name="report">The current report.</param>
        /// <returns>The next star report, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public StarIntel GetNext(StarIntel report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Dictionary.Count <= 1)
            {
                return report;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Dictionary.Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(report.Name) + 1;
            if (nextIndex >= keyList.Count)
            {
                nextIndex = 0;
            }
            return Dictionary[keyList[nextIndex]] as StarIntel;
        }      
        
        /// <summary>
        /// Get the next star report in the list that belongs to the same owner.
        /// </summary>
        /// <param name="report">The current report.</param>
        /// <returns>The next star report, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public StarIntel GetNextOwned(StarIntel report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Dictionary.Count <= 1)
            {
                return report;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Dictionary.Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(report.Name);
            
            do
            {
                nextIndex = nextIndex + 1;
                if (nextIndex >= keyList.Count)
                {
                    nextIndex = 0;
                }
            }
            while ((Dictionary[keyList[nextIndex]] as StarIntel).Owner != report.Owner);
            
            return Dictionary[keyList[nextIndex]] as StarIntel;
        }
        
        /// <summary>
        /// Get the previous star in the list.
        /// </summary>
        /// <param name="report">The current star.</param>
        /// <returns>The previous star, or the current star if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public StarIntel GetPrevious(StarIntel report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Dictionary.Count <= 1)
            {
                return report;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Dictionary.Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(report.Name) - 1;
            if (nextIndex < 0)
            {
                nextIndex = keyList.Count - 1;
            }
            return Dictionary[keyList[nextIndex]] as StarIntel;

        }
        
        /// <summary>
        /// Get the previous star report in the list that belongs to same owner.
        /// </summary>
        /// <param name="report">The current report.</param>
        /// <returns>The next star report, or the current if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        public StarIntel GetPreviousOwned(StarIntel report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (Dictionary.Count <= 1)
            {
                return report;
            }

            List<string> keyList = new List<string>();
            keyList.AddRange(Enumerable.Cast<string>(Dictionary.Keys));
            keyList.Sort();
            int nextIndex = keyList.IndexOf(report.Name);
            
            do
            {
                nextIndex = nextIndex - 1;
                if (nextIndex < 0)
                {
                    nextIndex = keyList.Count - 1;
                }
            }
            while ((Dictionary[keyList[nextIndex]] as StarIntel).Owner != report.Owner);
            
            return Dictionary[keyList[nextIndex]] as StarIntel;
        }

        /// <summary>
        /// Get the internal collection of values.
        /// </summary>
        public ICollection Values
        {
            get
            {
                return Dictionary.Values;
            }
        }
        
        /// <summary>
        /// Returns the amount of stars owned by someone. 
        /// </summary>
        /// <param name="raceName">A <see cref="System.String"/> indicating the owner to look for </param>
        /// <returns> The amount of stars property of the specified owner. </returns>
        public int Owned(string raceName)
        {
            int q = 0;
            foreach (StarIntel report in Dictionary.Values)
            {
                if (report.Owner == raceName) { q++; }
            }
            return q;
        }
    }
}
