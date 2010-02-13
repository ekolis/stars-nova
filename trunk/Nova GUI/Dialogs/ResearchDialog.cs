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

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System;

using NovaCommon;
using NovaClient;

// ============================================================================
// Research Dialog
// ============================================================================

namespace Nova
{
   public partial class ResearchDialog : Form
   {
      private Hashtable Buttons           = new Hashtable();
      private TechLevel CurrentLevel      = null;
      private int       AvailableEnergy   = 0;
      private ClientState  StateData         = null;
      private bool      DialogInitialised = false;


// ============================================================================
// Research dialog construction and initialisation.
// ============================================================================

      public ResearchDialog()
      {
         InitializeComponent();

         StateData    = ClientState.Data;
         CurrentLevel = StateData.ResearchLevel;

         // Provide a convienient way of getting a button from it's name.

         Buttons.Add("Energy"        , EnergyButton);
         Buttons.Add("Weapons"       , WeaponsButton);
         Buttons.Add("Propulsion"    , PropulsionButton);
         Buttons.Add("Construction"  , ConstructionButton);
         Buttons.Add("Electronics"   , ElectronicsButton);
         Buttons.Add("Biotechnology" , BiotechButton);

         // Set the currently attained research levels

         EnergyLevel.Text       = CurrentLevel[TechLevel.ResearchField.Energy]       .ToString();
         WeaponsLevel.Text      = CurrentLevel[TechLevel.ResearchField.Weapons]      .ToString();
         PropulsionLevel.Text   = CurrentLevel[TechLevel.ResearchField.Propulsion]   .ToString();
         ConstructionLevel.Text = CurrentLevel[TechLevel.ResearchField.Construction] .ToString();
         ElectronicsLevel.Text  = CurrentLevel[TechLevel.ResearchField.Electronics]  .ToString();
         BiotechLevel.Text      = CurrentLevel[TechLevel.ResearchField.Biotechnology].ToString();

         // Ensure that the correct RadioButton is checked to reflect the
         // current research selection and the budget up-down control is
         // initialised with the correct value.

         RadioButton button = Buttons[Enum.GetName(typeof(TechLevel.ResearchField), StateData.ResearchTopic)] as RadioButton;
         
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

         TechLevel researchLevels    = StateData.ResearchLevel;
         TechLevel researchResources = StateData.ResearchResources;
         TechLevel.ResearchField    researchArea      = StateData.ResearchTopic;

         int current = (int) researchResources[researchArea];
         int level   = (int) researchLevels[researchArea];
         int target  = Research.Cost(level+1);

         resourcesRequired = target
            - (int) StateData.ResearchResources[researchArea];

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

         newResearchLevel[researchArea] = level + 1;
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
