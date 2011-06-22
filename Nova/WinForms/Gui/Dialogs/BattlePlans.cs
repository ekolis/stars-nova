#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;
    
    /// <Summary>
    /// Dialog to manage battle plans.
    /// </Summary>
    public partial class BattlePlans : Form
    {
        private Dictionary<string, BattlePlan> battlePlans;
        /// <Summary>
        /// Initializes a new instance of the BattlePlans class.
        /// </Summary>
        public BattlePlans(Dictionary<string, BattlePlan> battlePlans)
        {
            this.battlePlans = battlePlans;
            
            InitializeComponent();

            foreach (BattlePlan plan in battlePlans.Values)
            {
                this.planList.Items.Add(plan.Name);
            }

            this.planList.SelectedIndex = 0;
            UpdatePlanDetails();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Update the details of the selected plan
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void UpdatePlanDetails()
        {
            string selection = this.planList.SelectedItem as string;
            BattlePlan plan = battlePlans[selection];

            this.planName.Text = plan.Name;
            this.primaryTarget.Text = plan.PrimaryTarget;
            this.secondaryTarget.Text = plan.SecondaryTarget;
            this.tactic.Text = plan.Tactic;
            this.attack.Text = plan.Attack;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Done button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
