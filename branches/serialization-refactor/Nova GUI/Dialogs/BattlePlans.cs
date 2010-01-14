// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Dialog to manage battle plans.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nova
{
   public partial class BattlePlans : Form
   {

// ============================================================================
// Construction.
// ============================================================================

      public BattlePlans()
      {
         InitializeComponent();

         foreach (BattlePlan plan in GuiState.Data.BattlePlans.Values) {
            PlanList.Items.Add(plan.Name);
         }
         
         PlanList.SelectedIndex = 0;
         UpdatePlanDetails();
      }


// ============================================================================
// Update the details of the selected plan
// ============================================================================

      private void UpdatePlanDetails()
      {
         Hashtable  battlePlans = GuiState.Data.BattlePlans;
         string     selection   = PlanList.SelectedItem as string;
         BattlePlan plan        = battlePlans[selection] as BattlePlan;

         PlanName.Text        = plan.Name;
         PrimaryTarget.Text   = plan.PrimaryTarget;
         SecondaryTarget.Text = plan.SecondaryTarget;
         Tactic.Text          = plan.Tactic;
         Attack.Text          = plan.Attack;
      }


// ============================================================================
// Done button pressed.
// ============================================================================

      private void DoneButton_Click(object sender, EventArgs e)
      {
         Close();
      }
   }
}
