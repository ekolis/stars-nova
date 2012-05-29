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
// ===========================================================================
#endregion

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    
    using Nova.Common;
    using Nova.Common.DataStructures;

    /// <Summary>
    /// Dialog for viewing battle progress and outcome.
    /// </Summary>
    public partial class BattleViewer : Form
    {
        private readonly BattleReport theBattle;
        private readonly Dictionary<long, Stack> myStacks = new Dictionary<long, Stack>();
        private int eventCount;

        /// <Summary>
        /// Initializes a new instance of the BattleViewer class.
        /// </Summary>
        /// <param name="thisBattle">The <see cref="BattleReport"/> to be displayed.</param>
        public BattleViewer(BattleReport report)
        {
            InitializeComponent();
            theBattle = report;
            eventCount = 0;

            // Take a copy of all of the stacks so that we can mess with them
            // without disturbing the master copy in the global turn file.

            foreach (Stack stack in theBattle.Stacks.Values)
            {
                myStacks[stack.Key] = new Stack(stack);
            }
        }

        /// <Summary>
        /// Initialisation performed on a load of the whole dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            battleLocation.Text = theBattle.Location;

            battlePanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            battlePanel.BackgroundImageLayout = ImageLayout.Stretch;
            SetStepNumber();
        }

        /// <Summary>
        /// Draw the battle panel by placing the images for the stacks in the
        /// appropriate position.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); // added

            Graphics graphics = e.Graphics;
            Size panelSize = battlePanel.Size;
            float scalingFactor = (float)panelSize.Width /
                                     (float)Global.MaxWeaponRange;

            graphics.PageScale = scalingFactor;
            graphics.ScaleTransform(0.5F, 0.5F);

            foreach (Stack stack in myStacks.Values)
            {
                graphics.DrawImage(stack.Icon.Image, (Point)stack.Position);
            }
        }

        /// <Summary>
        /// Step through each battle event.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextStep_Click(object sender, EventArgs e)
        {
            object thisStep = theBattle.Steps[eventCount];
            SetStepNumber();

            if (thisStep is BattleStepMovement)
            {
                UpdateMovement(thisStep as BattleStepMovement);
            }
            else if (thisStep is BattleStepTarget)
            {
                UpdateTarget(thisStep as BattleStepTarget);
            }
            else if (thisStep is BattleStepWeapons)
            {
                UpdateWeapons(thisStep as BattleStepWeapons);
            }
            else if (thisStep is BattleStepDestroy)
            {
                UpdateDestroy(thisStep as BattleStepDestroy);
            }

            if (eventCount < theBattle.Steps.Count - 1)
            {
                eventCount++;
            }
            else
            {
                nextStep.Enabled = false;
            }

        }

        /// <Summary>
        /// Update the movement of a stack.
        /// </Summary>
        /// <param name="movement">Movement to display.</param>
        private void UpdateMovement(BattleStepMovement movement)
        {
            Stack stack = myStacks[movement.StackKey];
            movedTo.Text = movement.Position.ToString();
            stackOwner.Text = stack.Owner.ToString("X");
            stack.Position = movement.Position;

            // We have moved, clear out the other fields as they may change.

            UpdateTarget(null);
            UpdateWeapons(null);

            battlePanel.Invalidate();
        }

        /// <Summary>
        /// Update the current target details.
        /// </Summary>
        /// <param name="target">Target ship to display.</param>
        private void UpdateTarget(BattleStepTarget targetKey)
        {
            if (targetKey == null)
            {
                ClearTargetDetails();
            }
            else
            {
                Stack target = null;
                theBattle.Stacks.TryGetValue(targetKey.TargetKey, out target);

                if (target == null)
                {
                    ClearTargetDetails();
                }
                else
                {
                    targetName.Text = target.Name;
                    targetOwner.Text = target.Owner.ToString("X");

                    targetShields.Text = target.TotalShieldStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    targetArmor.Text = target.TotalArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        /// <Summary>
        /// Set the details for the target to "" on the UI.
        /// </Summary>
        private void ClearTargetDetails()
        {
            targetName.Text = "";
            targetOwner.Text = "";
            targetShields.Text = "";
            targetArmor.Text = "";
        }

        /// <Summary>
        /// Deal with weapons being fired.
        /// </Summary>
        /// <param name="weapons">Weapon to display.</param>
        private void UpdateWeapons(BattleStepWeapons weapons)
        {
            if (weapons == null)
            {
                weaponPower.Text = "";
                componentTarget.Text = "";
                damage.Text = "";
                return;
            }

            weaponPower.Text = weapons.HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture);
            componentTarget.Text = weapons.Targeting;
            damage.Text = "Ship damaged";

            UpdateTarget(weapons.WeaponTarget);
        }


        /// <Summary>
        /// Deal with a ship being destroyed. Remove it from the containing stack and,
        /// if the token count drops to zero, destroy the whole stack.
        /// </Summary>
        /// <param name="destroy"></param>
        private void UpdateDestroy(BattleStepDestroy destroy)
        {
            damage.Text = "Ship destroyed";

            // Stacks have 1 token, so remove the stack at once.

            myStacks.Remove(destroy.StackKey);
            battlePanel.Invalidate();
        }
        

        /// <Summary>
        /// Just display the currrent step number in the battle replay control panel.
        /// </Summary>
        private void SetStepNumber()
        {
            StringBuilder title = new StringBuilder();

            title.AppendFormat(
                "Step {0} of {1}",
                eventCount + 1,
                theBattle.Steps.Count);

            stepNumber.Text = title.ToString();
        }
    }
}
