#region Copyright Notice
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

#region Module Description
// ===========================================================================
// RaceComponents: 
// Defines a colletion of Component objects using DictionaryBase to represent
// the components which are available to a given race.
// Allows these to be accessed as either Strings or Component objects, with 
// Component.Name acting as the dictionary key. A Component can be added or
// removed from RaceComponents using its Name or by using the
// AllComponents reference.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Nova.Common.Components
{
    /// <summary>
    /// RaceComponents: 
    /// Defines a colletion of Component objects using DictionaryBase to represent
    /// the components which are available to a given race.
    /// </summary>
    [Serializable]
    public class RaceComponents : DictionaryBase
    {
        private Race race = null;
        private TechLevel tech = null;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor. Generates the list of available components at construction for the 
        /// given race from AllComponents.
        /// </summary>
        /// <param name="newRace">The race these RaceComponents are available too.</param>
        /// <param name="newTech">The current tech level of the race.</param>
        /// ----------------------------------------------------------------------------
        public RaceComponents(Race newRace, TechLevel newTech)
        {
            DetermineRaceComponents(newRace, newTech);
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Updates the collection for the given race and tech level.
        /// Note this does not remove any existing components from the collection.
        /// </summary>
        /// <param name="newRace"></param>
        /// <param name="newTech"></param>
        /// ----------------------------------------------------------------------------
        public void DetermineRaceComponents(Race newRace, TechLevel newTech)
        {
            race = newRace;
            tech = newTech.Clone();
            if (race == null) throw new System.NullReferenceException();
            if (tech == null) throw new System.NullReferenceException();

            // go through the AllCompoents list
            foreach (Component component in AllComponents.Data.Components.Values)
            {
                // first check the required tech level
                if (tech < component.RequiredTech)
                    continue;
                if (Dictionary.Contains(component.Name))
                    continue;

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
                    Dictionary.Add(component.Name, component);
            }
        }

        #endregion

        #region Interfaces

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a componenent to the list of available components.
        /// </summary>
        /// <param name="newComponent">The Component to add.</param>
        /// ----------------------------------------------------------------------------
        public void Add(Component newComponent)
        {
            if (!Dictionary.Contains(newComponent.Name))
            {
                Dictionary.Add(newComponent.Name, newComponent);
            }
            else
            {
                Component current = Dictionary[newComponent.Name] as Component;
                if (current != newComponent)
                {
                    Report.Error("RaceComponents.cs : Add() - attempted to add a new component with the same name as an existing component.");
                }
                // else, they are the same component and can be safely ignored
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a Component to the list of available components.
        /// </summary>
        /// <param name="NewTrait">The Name of the Component to add.</param>
        /// ----------------------------------------------------------------------------
        public void Add(string ComponentName)
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Remove a Component from the race's RaceComponents list.
        /// </summary>
        /// <param name="ComponentToRemove">The Component to remove.</param>
        /// ----------------------------------------------------------------------------
        public void Remove(TraitEntry ComponentToRemove)
        {
            Dictionary.Remove(ComponentToRemove.Name);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Remove a Component from the race's RaceComponents list.
        /// </summary>
        /// <param name="TraitToRemove">The Name of the Component to remove.</param>
        /// ----------------------------------------------------------------------------
        public void Remove(string ComponentToRemove)
        {
            Dictionary.Remove(ComponentToRemove);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check if the race's RaceComponents contains a particular Component.
        /// </summary>
        /// <param name="ComponentName">The Name of the Component to look for.</param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public bool Contains(string ComponentName)
        {
            return Dictionary.Contains(ComponentName);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Allow array type indexing to an RaceComponents list.
        /// </summary>
        /// <param name="index">The Name of the Component</param>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public Component this[string index]
        {
            get
            {
                return Dictionary[index] as Component;
            }
            
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the Values in the collection.
        /// <para>
        /// Use: <c>foreach (Component c in this.Values)</c>
        /// </para>
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
