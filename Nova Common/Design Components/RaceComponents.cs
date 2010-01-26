// ============================================================================
// Nova. (c) 2010 Daniel Vale
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// RaceComponents: 
// Defines a colletion of Component objects using DictionaryBase to represent
// the components which are available to a given race.
// Allows these to be accessed as either Strings or Component objects, with 
// Component.Name acting as the dictionary key. A Component can be added or
// removed from RaceComponents using its Name or by using the
// AllComponents reference.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NovaCommon
{
    [Serializable]
    public class RaceComponents : DictionaryBase
    {
        private Race race = null;
        private TechLevel tech = null;

        /// <summary>
        /// Constructor. Generates the list of available components at construction for the 
        /// given race from AllComponents.
        /// </summary>
        /// <param name="newRace">The race these RaceComponents are available too.</param>
        /// <param name="newTech">The current tech level of the race.</param>
        public RaceComponents(Race newRace, TechLevel newTech)
        {
        }// RaceComponents constructor

        public void DetermineRaceComponents(Race newRace, TechLevel newTech)
        {
            race = newRace;
            tech = newTech.Clone();
            if (race == null) throw new System.NullReferenceException();
            if (tech == null) throw new System.NullReferenceException();

            // go through the AllCompoents list
            foreach (NovaCommon.Component component in AllComponents.Data.Components.Values)
            {
                // first check the required tech level
                if (tech < component.RequiredTech)
                    continue;

                // check if the component is restricted by this race's Primary or Secondary traits.
                bool restricted = false;
                foreach (String trait in AllTraits.TraitKeys)
                {
                    bool hasTrait = race.HasTrait(trait);
                    RaceAvailability availability = component.Restrictions.Availability(trait);
                    if (availability == RaceAvailability.not_available && hasTrait)
                    {
                        restricted = true;
                        break;
                    }
                    if (availability == RaceAvailability.required && !hasTrait)
                    {
                        restricted = true;
                        break;
                    }
                }// foreach trait

                if (!restricted)
                    Dictionary.Add(component.Name, component);
            }
        }//DetermineRaceComponents

        /// <summary>
        /// Add a componenent to the list of available components.
        /// </summary>
        /// <param name="newComponent">The Component to add.</param>
        public void Add(Component newComponent)
        {
            Dictionary.Add(newComponent.Name, newComponent);
        }

       
        /// <summary>
        /// Add a Component to the list of available components.
        /// </summary>
        /// <param name="NewTrait">The Name of the Component to add.</param>
        public void Add(String ComponentName)
        {
            if (AllComponents.Data.Contains(ComponentName))
            {
                Component c = AllComponents.Data.Components[ComponentName] as Component;

                Dictionary.Add(c.Name, c);
            }
            else
            {
                string s = "Error: The " + ComponentName + " component does not exist!";
                Report.Error(s);
            }

        }

        /// <summary>
        /// Remove a Component from the race's RaceComponents list.
        /// </summary>
        /// <param name="ComponentToRemove">The Component to remove.</param>
        public void Remove(TraitEntry ComponentToRemove)
        {
            Dictionary.Remove(ComponentToRemove.Name);
        }

        /// <summary>
        /// Remove a Component from the race's RaceComponents list.
        /// </summary>
        /// <param name="TraitToRemove">The Name of the Component to remove.</param>
        public void Remove(String ComponentToRemove)
        {
            Dictionary.Remove(ComponentToRemove);
        }

        /// <summary>
        /// Check if the race's RaceComponents contains a particular Component.
        /// </summary>
        /// <param name="ComponentName">The Name of the Component to look for.</param>
        /// <returns></returns>
        public bool Contains(String ComponentName)
        {
            return Dictionary.Contains(ComponentName);
        }

        /// <summary>
        /// Allow array type indexing to an RaceComponents list.
        /// </summary>
        /// <param name="index">The Name of the Component</param>
        /// <returns></returns>
        public Component this[String index]
        {
            get
            {
                return Dictionary[index] as Component;
            }
        }

        public ICollection Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

    }
}
