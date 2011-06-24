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
// TraitList: 
// Defines a colletion of TraitEntry objects using DictionaryBase.
// Allows these to be accessed as either Strings or TraitEnty objects, with 
// TraitEntry.Code acting as the dictionary key. A trait can be added or
// removed from a Trait list using only its mnemonic Code by using the
// PrimaryTraits and SecondaryTraits lists for reference.
//
// See PrimaryTraits and SecondaryTraits for descriptions of these traits.
//
// Design notes:
// Intention is to make working with traits intuitive without worying about
// when they are String or TraitEntry objects.
// ===========================================================================
#endregion

using System;
using System.Collections;

namespace Nova.Common
{
    /// <summary>
    /// Maintains a collection of TraitEntry objects.
    /// </summary>
    [Serializable]
    public class TraitList : DictionaryBase
    {
        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a new trait to the race's collection of traits.
        /// </summary>
        /// <param name="new_trait">A TraitEntry, such as those in SecondaryTraits.</param>
        /// ----------------------------------------------------------------------------
        public void Add(TraitEntry new_trait)
        {
            Dictionary.Add(new_trait.Code, new_trait);
        }

        /// <summary>
        /// Add a new trait to the race's collection of traits.
        /// </summary>
        /// <param name="newTrait">The code or short name of a trait such as IS for Improved Starbases. These are defined in AllTraits</param>
        public void Add(string newTrait)
        {
            foreach (DictionaryEntry de in AllTraits.Data.Secondary)
            {
                TraitEntry trait = de.Value as TraitEntry;
                if (trait.Code == newTrait)
                {
                    Dictionary.Add(trait.Code, trait);
                }
            }
        }

        /// <summary>
        /// Remove a trait from the race's collection of traits.
        /// </summary>
        /// <param name="traitToRemove">The trait to remove.</param>
        public void Remove(TraitEntry traitToRemove)
        {
            Dictionary.Remove(traitToRemove.Code);
        }

        /// <summary>
        /// Remove a trait from the race's collection of traits.
        /// </summary>
        /// <param name="traitToRemove">The code (short name) for the trait to remove, as defined in AllTraits.</param>
        public void Remove(string traitToRemove)
        {
            Dictionary.Remove(traitToRemove);
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="trait"></param>
        /// <returns></returns>
        public bool Contains(string trait)
        {
            return Dictionary.Contains(trait);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Allow array type indexing to a TraitList.
        /// </summary>
        /// <param name="index">The code (short name) for the trait, as defined in AllTraits.</param>
        /// <returns>The TraitEntry for the given trait code.</returns>
        public TraitEntry this[string index]
        {
            get
            {
                return Dictionary[index] as TraitEntry;
            }
        }

        #endregion
    }
}
