#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// Dialog for displaying current research levels and allocating resources to
// further research.
// ===========================================================================
#endregion

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Components;

    #region Delegates
    
    /// <Summary>
    /// This is the hook to listen for changes in research budget.
    /// Objects who subscribe to this should respond to this by
    /// udpating their related values. We don't specify arguments
    /// because relevant data can be read on ClientState.
    /// </Summary>
    public delegate bool ResearchAllocationChanged();
    
    #endregion
    
    /// <Summary>
    /// Research Dialog
    /// </Summary>
    public partial class ResearchDialog : Form
    {
        /// <Summary>
        /// This event should be fired when the global research budget is changed.
        /// Note that it's more apropiate to fire this when all changes are done,
        /// for example, on closing the Research Dialog instead of each Point change.
        /// </Summary>
        public event ResearchAllocationChanged ResearchAllocationChangedEvent;

        private readonly Dictionary<string, RadioButton> buttons = new Dictionary<string, RadioButton>();
        private readonly TechLevel currentLevel;
        private readonly int availableEnergy;
        private readonly ClientState stateData;
        private readonly bool dialogInitialised;
        private TechLevel.ResearchField targetArea;

        #region Construction

        /// <Summary>
        /// Initializes a new instance of the ResearchDialog class.
        /// </Summary>
        public ResearchDialog(ClientState stateData)
        {
            InitializeComponent();

            this.stateData = stateData;
            this.currentLevel = this.stateData.EmpireState.ResearchLevels;

            // Provide a convienient way of getting a button from it's name.
            this.buttons.Add("Energy", this.energyButton);
            this.buttons.Add("Weapons", this.weaponsButton);
            this.buttons.Add("Propulsion", this.propulsionButton);
            this.buttons.Add("Construction", this.constructionButton);
            this.buttons.Add("Electronics", this.electronicsButton);
            this.buttons.Add("Biotechnology", this.biotechButton);

            // Set the currently attained research levels
            this.energyLevel.Text = this.currentLevel[TechLevel.ResearchField.Energy].ToString();
            this.weaponsLevel.Text = this.currentLevel[TechLevel.ResearchField.Weapons].ToString();
            this.propulsionLevel.Text = this.currentLevel[TechLevel.ResearchField.Propulsion].ToString();
            this.constructionLevel.Text = this.currentLevel[TechLevel.ResearchField.Construction].ToString();
            this.electronicsLevel.Text = this.currentLevel[TechLevel.ResearchField.Electronics].ToString();
            this.biotechLevel.Text = this.currentLevel[TechLevel.ResearchField.Biotechnology].ToString();

            // Ensure that the correct RadioButton is checked to reflect the
            // current research selection and the budget up-down control is
            // initialised with the correct value.
            
            // Find the first research priority
            // TODO: Implement a proper hierarchy of research ("next research field") system.
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {
                if (this.stateData.EmpireState.ResearchTopics[area] == 1)
                {
                    this.targetArea = area;
                    break;        
                }
            }

            RadioButton button = this.buttons[Enum.GetName(typeof(TechLevel.ResearchField), this.targetArea)];

            button.Checked = true;

            this.availableEnergy = CountEnergy();
            this.availableResources.Text = this.availableEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.resourceBudget.Value = this.stateData.EmpireState.ResearchBudget;
            this.dialogInitialised = true;

            ParameterChanged(null, null);
        }

        #endregion

        #region Event Methods

        /// <Summary>
        /// A new area has been selected for research. Make a note of where the research
        /// resources are now going to be spent.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void CheckChanged(object sender, EventArgs e)
        {
            if (this.dialogInitialised == false)
            {
                return;
            }

            RadioButton button = sender as RadioButton;

            if (button.Checked == true)
            {
                try
                {
                    stateData.EmpireState.ResearchTopics[this.targetArea] = 0;
                    this.targetArea = (TechLevel.ResearchField)Enum.Parse(typeof(TechLevel.ResearchField), button.Text, true);
                    stateData.EmpireState.ResearchTopics[this.targetArea] = 1;
                }
                catch (System.ArgumentException)
                {
                    Report.Error("ResearchDialog.cs : CheckChanged() - unrecognised field of research.");
                }
            }
            
            // Populate the expected research benefits list
            Dictionary<string, Component> allComponents = AllComponents.Data.Components;
            TechLevel oldResearchLevel = this.stateData.EmpireState.ResearchLevels;
            TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

            newResearchLevel[this.targetArea] = oldResearchLevel[this.targetArea] + 1;
            this.researchBenefits.Items.Clear();

            foreach (Nova.Common.Components.Component component in allComponents.Values)
            {
                if (component.RequiredTech > oldResearchLevel &&
                    component.RequiredTech <= newResearchLevel)
                {
                    string available = component.Name + " " + component.Type;
                    this.researchBenefits.Items.Add(available);
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
            int percentage = (int)this.resourceBudget.Value;
            int allocatedEnergy = (this.availableEnergy * percentage) / 100;

            this.stateData.EmpireState.ResearchBudget = percentage;

            int resourcesRequired = 0;
            int yearsToComplete = 0;

            TechLevel researchLevels = this.stateData.EmpireState.ResearchLevels;            

            int level = (int)researchLevels[this.targetArea];
            int target = Research.Cost(
                this.targetArea,
                this.stateData.EmpireState.EmpireRace,
                researchLevels,
                researchLevels[this.targetArea] + 1);

            resourcesRequired = target
               - (int)this.stateData.EmpireState.ResearchResources[this.targetArea];

            if (level >= 26)
            {
                this.completionResources.Text = "Maxed";
            }
            else
            {
                this.completionResources.Text = ((int)resourcesRequired).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            if (level >= 26)
            {
                this.completionResources.Text = "Maxed";
            }
            else
            {
                this.completionResources.Text = resourcesRequired.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            if (percentage != 0 &&
                allocatedEnergy > 0 &&
                level < 26)
            {
                yearsToComplete = resourcesRequired / allocatedEnergy;
                this.completionTime.Text = yearsToComplete.ToString("f1");
            }
            else
            {
                this.completionTime.Text = "Never";
            }

            this.numericResources.Text = allocatedEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);            
        }

        #endregion

        #region Utility Methods

        /// <Summary>
        /// Return the total number of energy resources available to the current race
        /// </Summary>
        /// <returns>Total energy being invested in research.</returns>
        private int CountEnergy()
        {
            double totalEnergy = 0;
            string raceName = stateData.EmpireState.EmpireRace.Name;
            Intel turnData = this.stateData.InputTurn;

            foreach (StarIntel report in stateData.EmpireState.StarReports.Values)
            {
                if (report.Owner == stateData.EmpireState.Id)
                {
                    totalEnergy += report.GetResourceRate();
                }
            }
            return (int)totalEnergy;
        }

        #endregion
    }
}
