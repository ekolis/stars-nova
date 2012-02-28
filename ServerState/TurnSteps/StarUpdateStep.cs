#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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

namespace Nova.Server.TurnSteps
{
    using System;
    using System.Collections.Generic;
    
    using Nova.Common;
    using Nova.Common.Components;
    
    /// <summary>
    /// Updates Stars, Manufacturing and Research.
    /// </summary>
    public class StarUpdateStep : ITurnStep
    {
        private ServerData serverState;
        
        public StarUpdateStep()
        {
        }
        
        public void Process(ServerData serverState)
        {
            this.serverState = serverState;
            
            foreach (Star star in serverState.AllStars.Values)
            {                
                if (star.Owner == Global.Nobody || star.Colonists == 0)
                {
                    continue;
                }
                
                star.UpdateMinerals();
                
                // According to the allocated budget submited, update star resources.
                // Note that this sets the allocation for research to zero for all stars
                // which have "contribute only leftover resources to research". This
                // makes those stars be handled after manufacturing.
                star.UpdateResearch(serverState.AllEmpires[star.Owner].ResearchBudget);
                star.UpdateResources();
                
                ContributeAllocatedResearch(star);
                
                int initialPopulation = star.Colonists;
                star.UpdatePopulation(serverState.AllEmpires[star.Owner].Race);
                int finalPopulation = star.Colonists;
    
                if (finalPopulation < initialPopulation)
                {
                    int died = initialPopulation - finalPopulation;
                    Message message = new Message();
                    message.Audience = star.Owner;
                    message.Text = died.ToString(System.Globalization.CultureInfo.InvariantCulture)
                       + " of your colonists have been killed"
                       + " by the environment on " + star.Name;
                    serverState.AllMessages.Add(message);
                }
                
                ContributeLeftoverResearch(star);
                
                star.UpdateResearch(serverState.AllEmpires[star.Owner].ResearchBudget);
                star.UpdateResources();
            }
        }
        
        /// <summary>
        /// Contributes allocated research from the star.
        /// </summary>
        /// <param name="race">Star's owner race.</param>
        /// <param name="star">Star to process.</param>
        /// <remarks>
        /// Note that stars which contribute only leftovers are not accounted for.
        /// </remarks>
        private void ContributeAllocatedResearch(Star star)
        {   
            if (star.Owner == Global.Nobody)
            {
                return;
            }

            EmpireData empire = serverState.AllEmpires[star.Owner];

            TechLevel targetAreas = empire.ResearchTopics;
            TechLevel.ResearchField targetArea = TechLevel.ResearchField.Energy; // default to Energy.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (targetAreas[area] == 1)
                {
                    targetArea = area;
                    break;        
                }
            }
            
            // Consume resources for research for added paranoia.
            empire.ResearchResources[targetArea] = empire.ResearchResources[targetArea] + star.ResearchAllocation;
            star.ResearchAllocation = 0;            
            
            while (true)
            {
                int cost = Research.Cost(targetArea, empire.Race, empire.ResearchLevels, empire.ResearchLevels[targetArea] + 1);
                
                if (empire.ResearchResources[targetArea] >= cost)
                {
                    TechLevelUp(targetArea, empire);
                }
                else
                {
                    break;
                }
            }
        }
        
        private void ContributeLeftoverResearch(Star star)
        {
            if (star.Owner == Global.Nobody)
            {
                return;
            }
            
            EmpireData empire = serverState.AllEmpires[star.Owner];
            
            TechLevel targetAreas = empire.ResearchTopics;
            TechLevel.ResearchField targetArea = TechLevel.ResearchField.Energy; // default to Energy.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (targetAreas[area] == 1)
                {
                    targetArea = area;
                    break;        
                }
            }
            
            // Consume resources for research for added paranoia.
            empire.ResearchResources[targetArea] = empire.ResearchResources[targetArea] + star.ResourcesOnHand.Energy;
            star.ResourcesOnHand.Energy = 0;
            
            while (true)
            {
                int cost = Research.Cost(targetArea, empire.Race, empire.ResearchLevels, empire.ResearchLevels[targetArea] + 1);
                
                if (empire.ResearchResources[targetArea] >= cost)
                {
                    TechLevelUp(targetArea, empire);
                }
                else
                {
                  break;
                }
            }
        }
        
        /// <summary>
        /// Report an update in tech level and any new components that have became
        /// available.
        /// </summary>
        private void TechLevelUp(TechLevel.ResearchField area, EmpireData empire)
        {
            TechLevel oldResearchLevel = empire.ResearchLevels.Clone();
            empire.ResearchLevels[area]++;
            TechLevel newResearchLevel = empire.ResearchLevels;
            
            Message techAdvanceMessage = new Message(
                empire.Id,
                "Your race has advanced to Tech Level " + empire.ResearchLevels[area] + " in the " + area.ToString() + " field",
                "TechAdvance",
                null);
            
            serverState.AllMessages.Add(techAdvanceMessage);

            Dictionary<string, Component> allComponents = AllComponents.Data.Components;

            foreach (Component component in allComponents.Values)
            {
                if (oldResearchLevel < component.RequiredTech && newResearchLevel >= component.RequiredTech)
                {
                    empire.AvailableComponents.Add(component);                    
                    Message newComponentMessage = null;
                    
                    if (component.Properties.ContainsKey("Scanner") && component.Type == ItemType.PlanetaryInstallations)
                    {
                        newComponentMessage = new Message(
                            empire.Id,
                            null,
                            "All existing planetary scanners has been replaced by " + component.Name + " " + component.Type,
                            null);
                        
                        foreach (Star star in empire.OwnedStars.Values)
                        {
                            if (star.Owner == empire.Id &&
                                star.ScannerType != string.Empty)
                            {
                                star.ScannerType = component.Name;
                            }
                        }
                    }
                    else
                    {
                        newComponentMessage = new Message(
                           empire.Id,
                           null,
                           "You now have available the " + component.Name + " " + component.Type + " component",
                           null);
                    }
                    serverState.AllMessages.Add(newComponentMessage);
                }
            }            
        }

    }
}
