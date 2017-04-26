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
                DoBattleStepMovement(thisStep as BattleStepMovement);
            }
            else if (thisStep is BattleStepTarget)
            {
                DoBattleStepTarget(thisStep as BattleStepTarget);
            }
            else if (thisStep is BattleStepWeapons)
            {
                DoBattleStepFireWeapon(thisStep as BattleStepWeapons);
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
        /// <param name="battleStep">Movement to display.</param>
        private void DoBattleStepMovement(BattleStepMovement battleStep)
        {
            Stack stack = null;
            theBattle.Stacks.TryGetValue(battleStep.StackKey, out stack);

            if (stack != null)
            {
                UpdateStackDetails(stack);
                stack.Position = battleStep.Position; // move the icon
            }
            else
            {
                ClearStackDetails();
            }

            movedFrom.Text = stack.Position.ToString();
            movedTo.Text = battleStep.Position.ToString();
            stack.Position = battleStep.Position;

            // We have moved, clear out the other fields as they are not relevant to this step.
            ClearTargetDetails();
            ClearWeapons();

            battlePanel.Invalidate();
        }

        /// <Summary>
        /// Update the current target (and stack) details.
        /// </Summary>
        /// <param name="target">Target ship to display.</param>
        private void DoBattleStepTarget(BattleStepTarget battleStep)
        {
            if (battleStep == null)
            {
                Report.Error("BattleViewer.cs DoBattleStepTarget(): battleStep is null.");
                ClearTargetDetails();
            }
            else
            {
                Stack lamb = null;
                Stack wolf = null;

                theBattle.Stacks.TryGetValue(battleStep.TargetKey, out lamb);
                theBattle.Stacks.TryGetValue(battleStep.StackKey, out wolf);

                UpdateStackDetails(wolf);
                ClearMovementDetails();
                ClearWeapons();
                UpdateTargetDetails(lamb);
            }
        }

        /// <Summary>
        /// Deal with weapons being fired.
        /// </Summary>
        /// <param name="weapons">Weapon to display.</param>
        private void DoBattleStepFireWeapon(BattleStepWeapons weapons)
        {
            if (weapons == null)
            {
                Report.Error("BattleViewer.cs DoBattleStepFireWeapon() weapons is null.");
                ClearWeapons();
            }
            else
            {
                BattleStepTarget target = weapons.WeaponTarget;

                Stack lamb = null;
                Stack wolf = null;

                theBattle.Stacks.TryGetValue(target.TargetKey, out lamb);
                theBattle.Stacks.TryGetValue(target.StackKey, out wolf);

                UpdateStackDetails(wolf);
                UpdateTargetDetails(lamb);
                ClearMovementDetails();

                // damge taken
                weaponPower.Text = weapons.Damage.ToString(System.Globalization.CultureInfo.InvariantCulture);

                // "Damage to shields" or "Damage to armor"
                if (weapons.Targeting == BattleStepWeapons.TokenDefence.Shields)
                {
                    componentTarget.Text = "Damage to shields";
                    damage.Text = weaponPower.Text + " " + componentTarget.Text;
                    lamb.Token.Shields -= weapons.Damage;
                    UpdateTargetDetails(lamb);
                }
                else
                {
                    componentTarget.Text = "Damage to armor";
                    damage.Text = weaponPower.Text + " " + componentTarget.Text;
                    lamb.Token.Armor -= weapons.Damage;
                    UpdateTargetDetails(lamb);
                }

                
            }
        }

        /// <summary>
        /// Clear the details of the stack in the BattleViewer->BattleDetails->Stack
        /// </summary>
        private void ClearStackDetails()
        {
            stackKey.Text = "";
            stackOwner.Text = "";
            stackDesign.Text = "";
            stackShields.Text = "";
            topTokenArmor.Text = "";

        }

        /// <summary>
        /// Write out the Battle viewer stack details
        /// </summary>
        /// <param name="wolf"></param>
        private void UpdateStackDetails(Stack wolf)
        {
            if (wolf != null)
            {
                stackKey.Text = wolf.Key.ToString("X");
                stackOwner.Text = wolf.Owner.ToString("X");
                stackQuantity.Text = wolf.Token.Quantity.ToString();
                stackDesign.Text = wolf.Composition.First().Value.Design.Name;
                stackShields.Text = wolf.TotalShieldStrength.ToString();
                topTokenArmor.Text = wolf.TotalArmorStrength.ToString();
            }
            else
            {
                ClearStackDetails();
            }
        }

        /// <summary>
        /// Write out the target details
        /// </summary>
        /// <param name="lamb"></param>
        private void UpdateTargetDetails(Stack lamb)
        {
            if (lamb != null)
            {
                targetDesign.Text = lamb.Name;
                targetOwner.Text = lamb.Owner.ToString("X");

                targetShields.Text = lamb.TotalShieldStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
                targetArmor.Text = lamb.TotalArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                ClearTargetDetails();
            }
        }
        
        /// <Summary>
        /// Set the details for the target to "" on the UI.
        /// </Summary>
        private void ClearTargetDetails()
        {
            targetDesign.Text = "";
            targetOwner.Text = "";
            targetShields.Text = "";
            targetArmor.Text = "";
        }

        
        /// <summary>
        /// Clear the BattleViewer weapon details.
        /// </summary>
        private void ClearWeapons()
        {
            weaponPower.Text = "";
            componentTarget.Text = "";
            damage.Text = "";
        }

        /// <summary>
        /// Clear the BattleViewer movement details.
        /// </summary>
        private void ClearMovementDetails()
        {
            movedFrom.Text = "";
            movedTo.Text = "";
               
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
            // Not sure they do - see ShipToken.Quantity - Dan 17 Apr 17

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
