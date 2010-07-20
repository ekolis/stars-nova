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
// Battle Viewer Dialog
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    // ============================================================================
    // Dialog for viewing battle progress and outcome.
    // ============================================================================
    /// <summary>
    /// 
    /// </summary>
    public partial class BattleViewer : Form
    {
        private readonly BattleReport theBattle;
        private readonly Hashtable myStacks = new Hashtable();
        private int eventCount;


        #region Construction and Initialisation

        /// <summary>
        /// Initializes a new instance of the BattleViewer class.
        /// </summary>
        /// <param name="thisBattle">The <see cref="BattleReport"/> to be displayed.</param>
        public BattleViewer(Nova.Common.BattleReport thisBattle)
        {
            InitializeComponent();
            this.theBattle = thisBattle;
            this.eventCount = 0;

            // Take a copy of all of the stacks so that we can mess with them
            // without disturbing the master copy in the global turn file.

            foreach (Fleet stack in this.theBattle.Stacks.Values)
            {
                this.myStacks[stack.Name] = new Fleet(stack);
            }
        }


        /// <summary>
        /// Initialisation performed on a load of the whole dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            this.battleLocation.Text = this.theBattle.Location;

            this.battlePanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            this.battlePanel.BackgroundImageLayout = ImageLayout.Stretch;
            SetStepNumber();
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Draw the battle panel by placing the images for the stacks in the
        /// appropriate position.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); // added

            Graphics graphics = e.Graphics;
            Size panelSize = this.battlePanel.Size;
            float scalingFactor = (float)panelSize.Width /
                                     (float)Global.MaxWeaponRange;

            graphics.PageScale = scalingFactor;
            graphics.ScaleTransform(0.5F, 0.5F);

            foreach (Fleet stack in this.myStacks.Values)
            {
                graphics.DrawImage(stack.Image, stack.Position);
            }
        }


        /// <summary>
        /// Step through each battle event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextStep_Click(object sender, EventArgs e)
        {
            object thisStep = this.theBattle.Steps[this.eventCount];
            SetStepNumber();

            if (thisStep is BattleReport.Movement)
            {
                UpdateMovement(thisStep as BattleReport.Movement);
            }
            else if (thisStep is BattleReport.Target)
            {
                UpdateTarget(thisStep as BattleReport.Target);
            }
            else if (thisStep is BattleReport.Weapons)
            {
                UpdateWeapons(thisStep as BattleReport.Weapons);
            }
            else if (thisStep is BattleReport.Destroy)
            {
                UpdateDestroy(thisStep as BattleReport.Destroy);
            }

            if (this.eventCount < this.theBattle.Steps.Count - 1)
            {
                this.eventCount++;
            }
            else
            {
                this.nextStep.Enabled = false;
            }

        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Update the movement of a stack.
        /// </summary>
        /// <param name="movement">movement to display</param>
        private void UpdateMovement(BattleReport.Movement movement)
        {
            Fleet stack = this.myStacks[movement.StackName] as Fleet;
            this.movedTo.Text = movement.Position.ToString();
            this.stackOwner.Text = stack.Owner;
            stack.Position = movement.Position;

            // We have moved, clear out the other fields as they may change.

            UpdateTarget(null);
            UpdateWeapons(null);

            this.battlePanel.Invalidate();
        }


        /// <summary>
        /// Update the current target details.
        /// </summary>
        /// <param name="target">Target ship to display.</param>
        private void UpdateTarget(BattleReport.Target target)
        {
            if (target == null)
            {
                this.targetName.Text = "";
                this.targetOwner.Text = "";
                this.targetShields.Text = "";
                this.targetArmor.Text = "";
                return;
            }

            this.targetName.Text = target.TargetShip.Name;
            this.targetOwner.Text = target.TargetShip.Owner;
            this.targetShields.Text = target.TargetShip.Shields.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.targetArmor.Text = target.TargetShip.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Deal with weapons being fired.
        /// </summary>
        /// <param name="weapons">weapon to display</param>
        private void UpdateWeapons(BattleReport.Weapons weapons)
        {

            if (weapons == null)
            {
                this.weaponPower.Text = "";
                this.componentTarget.Text = "";
                this.damage.Text = "";
                return;
            }

            this.weaponPower.Text = weapons.HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.componentTarget.Text = weapons.Targeting;
            this.damage.Text = "Ship damaged";

            UpdateTarget(weapons.WeaponTarget);
        }


        /// <summary>
        /// Deal with a ship being destroyed. Remove it from the containing stack and,
        /// if the stack fleet count drops to zero, destroy the whole stack.
        /// </summary>
        /// <param name="destroy"></param>
        private void UpdateDestroy(BattleReport.Destroy destroy)
        {
            this.damage.Text = "Ship destroyed";

            Fleet stack = this.myStacks[destroy.StackName] as Fleet;
            string shipName = stack.Owner + "/" + destroy.ShipName;

            if (stack.FleetShips.Contains(shipName))
            {
                stack.FleetShips.Remove(shipName);
            }

            if (stack.FleetShips.Count == 0)
            {
                this.myStacks.Remove(stack.Name);
                this.battlePanel.Invalidate();
            }
        }


        /// <summary>
        /// Just display the currrent step number in the battle replay control panel.
        /// </summary>
        private void SetStepNumber()
        {
            StringBuilder title = new StringBuilder();

            title.AppendFormat(
                "Step {0} of {1}",
                this.eventCount + 1,
                this.theBattle.Steps.Count);

            this.stepNumber.Text = title.ToString();
        }

        #endregion

    }
}
