// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
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
// ============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    [Serializable]
    public class TraitList : DictionaryBase
    {

        /// <summary>
        /// default constructor
        /// </summary>
        public TraitList()
        {
        }

        /// <summary>
        /// Add a new trait to the race's collection of traits.
        /// </summary>
        /// <param name="NewTrait">A TraitEntry, such as those in SecondaryTraits.</param>
        public void Add(TraitEntry new_trait)
        {
            Dictionary.Add(new_trait.Code, new_trait);
        }

        /// <summary>
        /// Add a new trait to the race's collection of traits.
        /// </summary>
        /// <param name="NewTrait">The code or short name of a trait such as IS for Improved Starbases. These are defined in AllTraits</param>
        public void Add(String NewTrait)
        {

            foreach (DictionaryEntry de in AllTraits.Data.Secondary)
            {
                TraitEntry trait = de.Value as TraitEntry;
                if (trait.Code == NewTrait)
                {
                    Dictionary.Add(trait.Code, trait);
                }
            }
        }

        /// <summary>
        /// Remove a trait from the race's collection of traits.
        /// </summary>
        /// <param name="TraitToRemove">The trait to remove.</param>
        public void Remove(TraitEntry TraitToRemove)
        {
            Dictionary.Remove(TraitToRemove.Code);
        }

        /// <summary>
        /// Remove a trait from the race's collection of traits.
        /// </summary>
        /// <param name="TraitToRemove">The code (short name) for the trait to remove, as defined in AllTraits.</param>
        public void Remove(String TraitToRemove)
        {
            Dictionary.Remove(TraitToRemove);
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="trait"></param>
        /// <returns></returns>
        public bool Contains(String trait)
        {
            return Dictionary.Contains(trait);
        }

        /// <summary>
        /// Allow array type indexing to a TraitList.
        /// </summary>
        /// <param name="index">The code (short name) for the trait, as defined in AllTraits.</param>
        /// <returns></returns>
        public TraitEntry this[String index]
        {
            get
            {
                return Dictionary[index] as TraitEntry;
            }
        }

    }
}
