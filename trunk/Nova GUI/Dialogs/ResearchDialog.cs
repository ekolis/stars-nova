// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// 
// Dialog for displaying current research levels and allocating resources to
// further research.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System;


// ============================================================================
// Research Dialog
// ============================================================================

namespace Nova
{
   public partial class ResearchDialog : Form
   {
      private Hashtable Buttons           = new Hashtable();
      private Hashtable CurrentLevel      = null;
      private int       AvailableEnergy   = 0;
      private GuiState  StateData         = null;
      private bool      DialogInitialised = false;


// ============================================================================
// Research dialog construction and initialisation.
// ============================================================================

      public ResearchDialog()
      {
         InitializeComponent();

         StateData    = GuiState.Data;
         CurrentLevel = StateData.ResearchLevel.TechValues;

         // Provide a convienient way of getting a button from it's name.

         Buttons.Add("Energy"        , EnergyButton);
         Buttons.Add("Weapons"       , WeaponsButton);
         Buttons.Add("Propulsion"    , PropulsionButton);
         Buttons.Add("Construction"  , ConstructionButton);
         Buttons.Add("Electronics"   , ElectronicsButton);
         Buttons.Add("Biotechnology" , BiotechButton);

         // Set the currently attained research levels
         
         EnergyLevel.Text       = CurrentLevel["Energy"]       .ToString();
         WeaponsLevel.Text      = CurrentLevel["Weapons"]      .ToString();
         PropulsionLevel.Text   = CurrentLevel["Propulsion"]   .ToString();
         ConstructionLevel.Text = CurrentLevel["Construction"] .ToString();
         ElectronicsLevel.Text  = CurrentLevel["Electronics"]  .ToString();
         BiotechLevel.Text      = CurrentLevel["Biotechnology"].ToString();

         // Ensure that the correct RadioButton is checked to reflect the
         // current research selection and the budget up-down control is
         // initialised with the correct value.

         RadioButton button = Buttons[StateData.ResearchTopic] as RadioButton;
         button.Checked     = true;

         AvailableEnergy         = CountEnergy();
         AvailableResources.Text = AvailableEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture); 
         ResourceBudget.Value    = StateData.ResearchBudget;
         DialogInitialised       = true;

         ParameterChanged(null, null);
      }


// ============================================================================
// A new area has been selected for research. Make a note of where the research
// resources are now going to be spent.
// ============================================================================

      private void CheckChanged(object sender, EventArgs e)
      {
         if (DialogInitialised == false) return;

         RadioButton button = sender as RadioButton;

         if (button.Checked == true) {
            GuiState.Data.ResearchTopic = button.Text;
         }

         ParameterChanged(null, null);
      }


// ============================================================================
// The OK button has been pressed. Just exit the dialog.
// ============================================================================

      private void OKClicked(object sender, EventArgs e)
      {
         Close();
      }


// ============================================================================
// The resource budget has been changed. Update all relevant fields.
// ============================================================================

      private void ParameterChanged(object sender, EventArgs e)
      {
         int percentage       = (int) ResourceBudget.Value;
         int allocatedEnergy  = ((int) AvailableEnergy * percentage) / 100;

         StateData.ResearchBudget     = percentage;
         StateData.ResearchAllocation = allocatedEnergy;

         double resourcesRequired = 0;
         double yearsToComplete   = 0;

         Hashtable researchLevels    = StateData.ResearchLevel.TechValues;
         Hashtable researchResources = StateData.ResearchResources.TechValues;
         string    researchArea      = StateData.ResearchTopic;

         int current = (int) researchResources[researchArea];
         int level   = (int) researchLevels[researchArea];
         int target  = Research.Cost(level+1);

         resourcesRequired = target
            - (int) StateData.ResearchResources.TechValues[researchArea];

         CompletionResources.Text = ((int) resourcesRequired).ToString(System.Globalization.CultureInfo.InvariantCulture);

         if (percentage != 0) {
            yearsToComplete     = resourcesRequired / allocatedEnergy;
            CompletionTime.Text = yearsToComplete.ToString("f1");
         }
         else {
            CompletionTime.Text = "Never";
         }

         NumericResources.Text = allocatedEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture);

         // Populate the expected research benefits list

         Hashtable allComponents    = AllComponents.Data.Components;
         TechLevel oldResearchLevel = StateData.ResearchLevel;
         TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

         newResearchLevel.TechValues[researchArea] = level + 1;
         ResearchBenefits.Items.Clear();

         foreach (NovaCommon.Component component in allComponents.Values) {
            if (component.RequiredTech >  oldResearchLevel &&
                component.RequiredTech <= newResearchLevel) {

               string available = component.Name + " " + component.Type;
               ResearchBenefits.Items.Add(available);
            }
         }

      }


// ============================================================================
// Return the total number of energy resources available to the current race
// ============================================================================

      private int CountEnergy()
      {
         double     totalEnergy = 0;
         string     raceName    = StateData.RaceName;
         Intel turnData    = StateData.InputTurn;    

         foreach (Star star in turnData.AllStars.Values) {
            if (star.Owner == raceName) {
               totalEnergy += star.ResourcesOnHand.Energy;
            }
         }
         return (int) totalEnergy;
      }
   }
}
