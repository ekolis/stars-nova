// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// Racial Traits: 
// Defines a colletion of TraitEntry objects representing the singular primary
// and zero or more lesser (secondary) racial traits. See PrimaryTraits and 
// SecondaryTraits for descriptions of these traits.
//
// Design notes:
// Inherits from TraitList and adds a PrimaryTrait field
// ============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NovaCommon
{
    /// <summary>
    ///  Class to store and compare a Race's traits.
    /// </summary>
    [Serializable]
    public class RacialTraits : TraitList
    {
        private TraitEntry PrimaryTrait = AllTraits.Data.Primary["JOAT"]; // Start with some default primary trait

        /// <summary>
        /// default constructor
        /// </summary>
        public RacialTraits()
        {
        }

        /// <summary>
        /// Loop through all of a races traits, starting with the primary trait.
        /// </summary>
        /// <returns>Each of the race's traits, begining with the PrimaryTrait, 
        /// followed by any lesser traits.</returns>
        public new IEnumerator GetEnumerator()
        {
            yield return PrimaryTrait;
            foreach (TraitEntry trait in Dictionary.Values)
                yield return (TraitEntry)trait;
        }

        /// <summary>
        /// Check if the racial traits contains a particular trait.
        /// </summary>
        /// <param name="trait"></param>
        /// <returns>True if trait is the race's PrimaryTrait or a lesser trait.</returns>
        public new bool Contains(String trait)
        {
            if (PrimaryTrait.Code == trait) return true;
            if (Dictionary == null)
            {
                Report.Error("RacialTraits: Contains() - Dictionary is null.");
                return false; // FIXME - should never be null. Should be fixed now this is based on DictionaryBase (requires testing) ---Dan 16 Oct 09
            }
            return Dictionary.Contains(trait);
        }

        // ============================================================================
        /// <summary>
        /// Control access to the primary trait. It can be read as a public property. 
        /// It can be set using the SetPrimary() accessor function passing either a 
        /// TraitEntry or a String containing one of the primary trait codes.
        /// </summary>
        /// Design note: did not use a set method as I needed to overload depending on 
        /// whether a TraitEntry or a String is used to set the Primary racial trait.
        public TraitEntry Primary
        {
            get
            {
                return PrimaryTrait;
            }
        }
        /// <summary>
        /// Control access to the primary trait. It can be read as a public property. 
        /// It can be set using the SetPrimary() accessor function passing either a 
        /// TraitEntry or a String containing one of the primary trait codes.
        /// </summary>
        /// <param name="primaryTrait">The new primary trait.</param>
        public void SetPrimary(TraitEntry primaryTrait)
        {
            PrimaryTrait = primaryTrait;
        }
        /// <summary>
        /// Control access to the primary trait. It can be read as a public property. 
        /// It can be set using the SetPrimary() accessor function passing either a 
        /// TraitEntry or a String containing one of the primary trait codes.
        /// </summary>
        /// <param name="primaryTrait">The new primary trait.</param>
        public void SetPrimary(String primaryTrait)
        {
            foreach (DictionaryEntry de in AllTraits.Data.Primary)
            {
                TraitEntry trait = de.Value as TraitEntry;
                if (trait.Code == primaryTrait || trait.Name == primaryTrait)
                {
                    PrimaryTrait = trait;
                    return;
                }
            }

            Report.Error("The primaryTrait \"" + primaryTrait + "\" is not recognised. Failed to set primary trait.");
        }

    }

}
