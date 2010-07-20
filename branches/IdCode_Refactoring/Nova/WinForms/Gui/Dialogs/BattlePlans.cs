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
// Dialog to manage battle plans.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Dialog to manage battle plans.
    /// </summary>
    public partial class BattlePlans : Form
    {

        /// <summary>
        /// Initializes a new instance of the BattlePlans class.
        /// </summary>
        public BattlePlans()
        {
            InitializeComponent();

            foreach (BattlePlan plan in ClientState.Data.BattlePlans.Values)
            {
                this.planList.Items.Add(plan.Name);
            }

            this.planList.SelectedIndex = 0;
            UpdatePlanDetails();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update the details of the selected plan
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void UpdatePlanDetails()
        {
            Hashtable battlePlans = ClientState.Data.BattlePlans;
            string selection = this.planList.SelectedItem as string;
            BattlePlan plan = battlePlans[selection] as BattlePlan;

            this.planName.Text = plan.Name;
            this.primaryTarget.Text = plan.PrimaryTarget;
            this.secondaryTarget.Text = plan.SecondaryTarget;
            this.tactic.Text = plan.Tactic;
            this.attack.Text = plan.Attack;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Done button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
