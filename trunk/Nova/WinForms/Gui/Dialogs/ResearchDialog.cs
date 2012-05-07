#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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
// ============================================================================
#endregion


namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;    
    
    /// <Summary>
    /// This is the hook to listen for changes in research budget.
    /// Objects who subscribe to this should respond by
    /// udpating their related values. We don't specify arguments
    /// because relevant data can be read on ClientState.
    /// </Summary>
    public delegate bool ResearchAllocationChanged();
    
    /// <Summary>
    /// Dialog for displaying current research levels and allocating resources to
    /// further research.
    /// </Summary>
    public partial class ResearchDialog : Form
    {
        /// <Summary>
        /// This event should be fired when the global research budget is changed.
        /// Note that it's more apropiate to fire this when all changes are done,
        /// for example, on closing the Research Dialog instead of each Point change.
        /// </Summary>
        public event ResearchAllocationChanged ResearchAllocationChangedEvent;

        private readonly bool dialogInitialised;
        
        private readonly ClientData clientState;

        private readonly Dictionary<string, RadioButton> buttons = new Dictionary<string, RadioButton>();
        private readonly TechLevel currentLevel;
        private TechLevel.ResearchField targetArea;
        private readonly int availableEnergy;         

        /// <Summary>
        /// Initializes a new instance of the ResearchDialog class.
        /// </Summary>
        public ResearchDialog(ClientData clientState)
        {
            InitializeComponent();

            this.clientState = clientState;
            currentLevel = this.clientState.EmpireState.ResearchLevels;

            // Provide a convienient way of getting a button from it's name.
            buttons.Add("Energy", energyButton);
            buttons.Add("Weapons", weaponsButton);
            buttons.Add("Propulsion", propulsionButton);
            buttons.Add("Construction", constructionButton);
            buttons.Add("Electronics", electronicsButton);
            buttons.Add("Biotechnology", biotechButton);

            // Set the currently attained research levels
            energyLevel.Text        = currentLevel[TechLevel.ResearchField.Energy].ToString();
            weaponsLevel.Text       = currentLevel[TechLevel.ResearchField.Weapons].ToString();
            propulsionLevel.Text    = currentLevel[TechLevel.ResearchField.Propulsion].ToString();
            constructionLevel.Text  = currentLevel[TechLevel.ResearchField.Construction].ToString();
            electronicsLevel.Text   = currentLevel[TechLevel.ResearchField.Electronics].ToString();
            biotechLevel.Text       = currentLevel[TechLevel.ResearchField.Biotechnology].ToString();

            // Ensure that the correct RadioButton is checked to reflect the
            // current research selection and the budget up-down control is
            // initialised with the correct value.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (this.clientState.EmpireState.ResearchTopics[area] == 1)
                {
                    targetArea = area;
                    break;        
                }
            }

            RadioButton button = buttons[Enum.GetName(typeof(TechLevel.ResearchField), targetArea)];

            button.Checked = true;

            availableEnergy = CountEnergy();
            availableResources.Text = this.availableEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);
            budgetPercentage.Value = this.clientState.EmpireState.ResearchBudget;
            dialogInitialised = true;

            ParameterChanged(null, null);
        }


        /// <Summary>
        /// A new area has been selected for research. Make a note of where the research
        /// resources are now going to be spent.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void CheckChanged(object sender, EventArgs e)
        {
            if (dialogInitialised == false)
            {
                return;
            }

            RadioButton button = sender as RadioButton;

            if (button.Checked == true)
            {
                try
                {
                    clientState.EmpireState.ResearchTopics[targetArea] = 0;
                    targetArea = (TechLevel.ResearchField)Enum.Parse(typeof(TechLevel.ResearchField), button.Text, true);
                    clientState.EmpireState.ResearchTopics[targetArea] = 1;
                }
                catch (System.ArgumentException)
                {
                    Report.Error("ResearchDialog.cs : CheckChanged() - unrecognised field of research.");
                }
            }
            
            // Populate the expected research benefits list
            AllComponents allComponents = new AllComponents();

            TechLevel oldResearchLevel = currentLevel;
            TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

            newResearchLevel[targetArea] = oldResearchLevel[targetArea] + 1;
            researchBenefits.Items.Clear();

            foreach (Nova.Common.Components.Component component in allComponents.GetAll.Values)
            {
                if (component.RequiredTech > oldResearchLevel &&
                    component.RequiredTech <= newResearchLevel)
                {
                    string available = component.Name + " " + component.Type;
                    researchBenefits.Items.Add(available);
                }
            }

            ParameterChanged(null, null);
        }


        /// <Summary>
        /// The OK button has been pressed. Just exit the dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OKClicked(object sender, EventArgs e)
        {
            //Generate a research command to describe the changes.
            ResearchCommand command = new ResearchCommand();
            command.Budget = (int)budgetPercentage.Value;
            command.Topics.Zero();
            command.Topics[targetArea] = 1;    
            
            if (command.isValid(clientState.EmpireState))
            {
                clientState.Commands.Push(command);
                command.ApplyToState(clientState.EmpireState);
            }
            
            // This is done for synchronization. We wait for the event handlers
            // to return something (and thus complete) before closing the Form and invalidating
            // all event handlers and delegates thus crashing everything into the fiery void of despair.
            if (ResearchAllocationChangedEvent())
            {
            }
            
            Close();
        }
        

        /// <Summary>
        /// The resource budget has been changed. Update all relevant fields.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ParameterChanged(object sender, EventArgs e)
        {
        	int resourcesRequired = 0;
            int yearsToComplete = 0;

            //stateData.EmpireState.ResearchBudget = (int)resourceBudget.Value;
            int budgetedEnergy = (availableEnergy * (int)budgetPercentage.Value) / 100;                   
            
            int targetCost = Research.Cost(targetArea, clientState.EmpireState.Race, currentLevel, currentLevel[targetArea] + 1);

            resourcesRequired = targetCost - currentLevel[targetArea];

            if (currentLevel[targetArea] >= 26)
            {
                completionResources.Text = "Maxed";
            }
            else
            {
                completionResources.Text = resourcesRequired.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            if (budgetPercentage.Value != 0 &&
                budgetedEnergy > 0 &&
                currentLevel[targetArea] < 26)
            {
                yearsToComplete = (int)Math.Ceiling((double)resourcesRequired / budgetedEnergy);
                completionTime.Text = yearsToComplete.ToString();
            }
            else
            {
                completionTime.Text = "Never";
            }

            numericResources.Text = budgetedEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);            
        }


        /// <Summary>
        /// Return the total number of energy resources available to the current race
        /// </Summary>
        /// <returns>Total energy being invested in research.</returns>
        private int CountEnergy()
        {
            int totalEnergy = 0;

            foreach (Star star in clientState.EmpireState.OwnedStars.Values)
            {
                if (star.Owner == clientState.EmpireState.Id)
                {
                    totalEnergy += star.GetResourceRate();
                }
            }
            return totalEnergy;
        }
    }
}
