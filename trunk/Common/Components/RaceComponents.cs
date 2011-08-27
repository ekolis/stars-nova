#region Copyright Notice
// ============================================================================
// Copyright (C) 2010, 2011 The Stars-Nova Project
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

namespace Nova.Common.Components
{
    using System;
    using System.Collections.Generic;
    
    using Nova.Common;

    /// <summary>
    /// Defines a colletion of Component objects using Dictionary to represent
    /// the components which are available to a given race.
    /// Allows these to be accessed as either Strings or Component objects, with 
    /// Component.Name acting as the dictionary key. A Component can be added or
    /// removed from RaceComponents using its Name or by using the
    /// AllComponents reference.
    /// </summary>
    [Serializable]
    public class RaceComponents : Dictionary<string, Component>
    {
        private Race race = null;
        private TechLevel tech = null;

        /// <summary>
        /// Default Constructor. Use this when loading from XML and adding components
        /// from there one by one.
        /// </summary>
        public RaceComponents()
        {
        }
        
        /// <summary>
        /// Constructor. Generates the list of available components at construction for the 
        /// given race from AllComponents.
        /// </summary>
        /// <param name="newRace">The race these RaceComponents are available too.</param>
        /// <param name="newTech">The current tech level of the race.</param>
        public RaceComponents(Race newRace, TechLevel newTech)
        {
            DetermineRaceComponents(newRace, newTech);
        }

        /// <summary>
        /// Updates the collection for the given race and tech level.
        /// Note this does not remove any existing components from the collection.
        /// </summary>
        /// <param name="newRace"></param>
        /// <param name="newTech"></param>
        public void DetermineRaceComponents(Race newRace, TechLevel newTech)
        {
            race = newRace;
            tech = newTech.Clone();
            if (race == null)
            {
                throw new System.NullReferenceException();
            }
            if (tech == null)
            {
                throw new System.NullReferenceException();
            }

            // go through the AllCompoents list
            foreach (Component component in AllComponents.Data.Components.Values)
            {
                // first check the required tech level
                if (tech < component.RequiredTech)
                {
                    continue;
                }
                if (Contains(component.Name))
                {
                    continue;
                }

                // check if the component is restricted by this race's Primary or Secondary traits.
                bool restricted = false;
                foreach (string trait in AllTraits.TraitKeys)
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
                }

                if (!restricted)
                {
                    Add(component.Name, component);
                }
            }
        }

        /// <summary>
        /// Add a componenent to the list of available components.
        /// </summary>
        /// <param name="newComponent">The Component to add.</param>
        public void Add(Component newComponent)
        {
            if (!Contains(newComponent.Name))
            {
                Add(newComponent.Name, newComponent);
            }
            else
            {
                Component current = this[newComponent.Name] as Component;
                if (current != newComponent)
                {
                    Report.Error("RaceComponents.cs : Add() - attempted to add a new component with the same name as an existing component.");
                }
                // else, they are the same component and can be safely ignored
            }
        }

        /// <summary>
        /// Add a Component to the list of available components.
        /// </summary>
        /// <param name="componentName">The Name of the Component to add.</param>
        public void Add(string componentName)
        {
            if (AllComponents.Data.Contains(componentName))
            {
                Component c = AllComponents.Data.Components[componentName];

                Add(c.Name, c);
            }
            else
            {
                string s = "Error: The " + componentName + " component does not exist!";
                Report.Error(s);
            }
        }

        /// <summary>
        /// Remove a Component from the race's RaceComponents list.
        /// </summary>
        /// <param name="componentToRemove">The Component to remove.</param>
        public void Remove(TraitEntry componentToRemove)
        {
            Remove(componentToRemove.Name);
        }

        /// <summary>
        /// Check if the race's RaceComponents contains a particular Component.
        /// </summary>
        /// <param name="componentName">The Name of the Component to look for.</param>
        /// <returns></returns>
        public bool Contains(string componentName)
        {
            return ContainsKey(componentName);
        }
    }
}
