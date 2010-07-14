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

using System;
using System.Collections;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;


namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Research Dialog
    /// </summary>
    public partial class ResearchDialog : Form
    {
        private readonly Hashtable buttons = new Hashtable();
        private readonly TechLevel currentLevel;
        private readonly int availableEnergy;
        private readonly ClientState stateData;
        private readonly bool dialogInitialised;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the ResearchDialog class.
        /// </summary>
        public ResearchDialog()
        {
            InitializeComponent();

            this.stateData = ClientState.Data;
            this.currentLevel = this.stateData.ResearchLevel;

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

            RadioButton button = this.buttons[Enum.GetName(typeof(TechLevel.ResearchField), this.stateData.ResearchTopic)] as RadioButton;

            button.Checked = true;

            this.availableEnergy = CountEnergy();
            this.availableResources.Text = this.availableEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.resourceBudget.Value = this.stateData.ResearchBudget;
            this.dialogInitialised = true;

            ParameterChanged(null, null);
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A new area has been selected for research. Make a note of where the research
        /// resources are now going to be spent.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void CheckChanged(object sender, EventArgs e)
        {
            if (this.dialogInitialised == false) return;

            RadioButton button = sender as RadioButton;

            if (button.Checked == true)
            {
                try
                {
                    ClientState.Data.ResearchTopic = (TechLevel.ResearchField)Enum.Parse(typeof(TechLevel.ResearchField), button.Text, true);
                }
                catch (System.ArgumentException)
                {
                    Report.Error("ResearchDialog.cs : CheckChanged() - unrecognised field of research.");
                }

            }

            ParameterChanged(null, null);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The OK button has been pressed. Just exit the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OKClicked(object sender, EventArgs e)
        {
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The resource budget has been changed. Update all relevant fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ParameterChanged(object sender, EventArgs e)
        {
            int percentage = (int)this.resourceBudget.Value;
            int allocatedEnergy = ((int)this.availableEnergy * percentage) / 100;

            this.stateData.ResearchBudget = percentage;
            this.stateData.ResearchAllocation = allocatedEnergy;

            double resourcesRequired = 0;
            double yearsToComplete = 0;

            TechLevel researchLevels = this.stateData.ResearchLevel;
            TechLevel researchResources = this.stateData.ResearchResources;
            TechLevel.ResearchField researchArea = this.stateData.ResearchTopic;

            int current = (int)researchResources[researchArea];
            int level = (int)researchLevels[researchArea];
            int target = Research.Cost(level + 1);

            resourcesRequired = target
               - (int)this.stateData.ResearchResources[researchArea];

            this.completionResources.Text = ((int)resourcesRequired).ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (percentage != 0)
            {
                yearsToComplete = resourcesRequired / allocatedEnergy;
                this.completionTime.Text = yearsToComplete.ToString("f1");
            }
            else
            {
                this.completionTime.Text = "Never";
            }

            this.numericResources.Text = allocatedEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);

            // Populate the expected research benefits list

            Hashtable allComponents = AllComponents.Data.Components;
            TechLevel oldResearchLevel = this.stateData.ResearchLevel;
            TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

            newResearchLevel[researchArea] = level + 1;
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

        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the total number of energy resources available to the current race
        /// </summary>
        /// <returns>Total energy being invested in research.</returns>
        /// ----------------------------------------------------------------------------
        private int CountEnergy()
        {
            double totalEnergy = 0;
            string raceName = this.stateData.RaceName;
            Intel turnData = this.stateData.InputTurn;

            foreach (Star star in turnData.AllStars.Values)
            {
                if (star.Owner == raceName)
                {
                    totalEnergy += star.ResourcesOnHand.Energy;
                }
            }
            return (int)totalEnergy;
        }

        #endregion

    }
}
