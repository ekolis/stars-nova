#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// StarList: 
// Defines a colletion of Star objects using DictionaryBase.
// Allows these to be accessed by the star's name (which is a unique key),
// as well as maintaining a sorted list for next/previous functionality.
// ===========================================================================
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Nova.Common
{
    [Serializable]
    public class StarList : DictionaryBase
    {
        #region Constuction

        /// ----------------------------------------------------------------------------
        /// /// <summary>
        /// default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public StarList()
        {
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a new star to the StarList
        /// </summary>
        /// <param name="star">The Star to be added to the StarList</param>
        /// ----------------------------------------------------------------------------
        public void Add(Star star)
        {
            Dictionary.Add(star.Name, star);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Remove a Star from the StarList
        /// </summary>
        /// <param name="star">The star to remove.</param>
        /// ----------------------------------------------------------------------------
        public void Remove(Star star)
        {
            Dictionary.Remove(star.Name);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Remove a star from the StarList
        /// </summary>
        /// <param name="starName">The name of the star.</param>
        /// ----------------------------------------------------------------------------
        public void Remove(string starName)
        {
            if (Dictionary.Contains(starName))
            {
                Dictionary.Remove(starName);
            }

        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="star">The Star to check for</param>
        /// <returns>true if star is in the StarList</returns>
        /// ----------------------------------------------------------------------------
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="starName">The name of a star.</param>
        /// <returns>true if starName is the name of one of the stars in the StarList</returns>
        /// ----------------------------------------------------------------------------
        public bool Contains(string starName)
        {
            return Dictionary.Contains(starName);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Allow array type indexing to a StarList.
        /// </summary>
        /// <param name="index">The name of the star.</param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public Star this[string index]
        {
            get
            {
                if (!Dictionary.Contains(index))
                {
                    return null;
                }
                return Dictionary[index] as Star;
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the next star in the list.
        /// </summary>
        /// <param name="star">The current star.</param>
        /// <returns>The next star, or the current star if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        /// ----------------------------------------------------------------------------
        public Star GetNext(Star star)
        {
            if (star == null)
            {
                throw new ArgumentNullException("star");
            }
            if (Dictionary.Count <= 1)
            {
                return star;
            }

            ArrayList keyList = new ArrayList();
            keyList.AddRange(Dictionary.Keys);
            keyList.Sort();
            int nextIndex = keyList.IndexOf(star.Name) + 1;
            if (nextIndex >= keyList.Count)
            {
                nextIndex = 0;
            }
            return Dictionary[keyList[nextIndex]] as Star;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the previous star in the list.
        /// </summary>
        /// <param name="star">The current star.</param>
        /// <returns>The previous star, or the current star if there is only one.</returns>
        /// <exception cref="NullReferenceException"> if star is null.</exception>
        /// ----------------------------------------------------------------------------
        public Star GetPrevious(Star star)
        {
            if (star == null)
            {
                throw new ArgumentNullException("star");
            }
            if (Dictionary.Count <= 1)
            {
                return star;
            }

            ArrayList keyList = new ArrayList();
            keyList.AddRange(Dictionary.Keys);
            keyList.Sort();
            int nextIndex = keyList.IndexOf(star.Name) - 1;
            if (nextIndex < 0)
            {
                nextIndex = keyList.Count - 1;
            }
            return Dictionary[keyList[nextIndex]] as Star;

        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the internal collection of values.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ICollection Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        #endregion

    }
}
