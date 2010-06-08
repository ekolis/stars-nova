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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
        private BattleReport TheBattle = null;
        private int EventCount = 0;
        private Hashtable MyStacks = new Hashtable();


        #region Construction and Initialisation

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thisBattle">The <see cref="BattleReport"/> to be displayed.</param>
        public BattleViewer(Nova.Common.BattleReport thisBattle)
        {
            InitializeComponent();
            TheBattle = thisBattle;
            EventCount = 0;

            // Take a copy of all of the stacks so that we can mess with them
            // without disturbing the master copy in the global turn file.

            foreach (Fleet stack in TheBattle.Stacks.Values)
            {
                MyStacks[stack.Name] = new Fleet(stack);
            }
        }


        /// <summary>
        /// Initialisation performed on a load of the whole dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            BattleLocation.Text = TheBattle.Location;

            BattlePanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            BattlePanel.BackgroundImageLayout = ImageLayout.Stretch;
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
            base.OnPaint(e); //added

            Graphics graphics = e.Graphics;
            Size panelSize = BattlePanel.Size;
            float scalingFactor = (float)panelSize.Width /
                                     (float)Global.MaxWeaponRange;

            graphics.PageScale = scalingFactor;
            graphics.ScaleTransform(0.5F, 0.5F);

            foreach (Fleet stack in MyStacks.Values)
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
            Object thisStep = TheBattle.Steps[EventCount];
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

            if (EventCount < TheBattle.Steps.Count - 1)
            {
                EventCount++;
            }
            else
            {
                NextStep.Enabled = false;
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
            Fleet stack = MyStacks[movement.StackName] as Fleet;
            MovedTo.Text = movement.Position.ToString();
            StackOwner.Text = stack.Owner;
            stack.Position = movement.Position;

            // We have moved, clear out the other fields as they may change.

            UpdateTarget(null);
            UpdateWeapons(null);

            BattlePanel.Invalidate();
        }


        /// <summary>
        /// Update the current target details.
        /// </summary>
        /// <param name="target">Target ship to display.</param>
        private void UpdateTarget(BattleReport.Target target)
        {
            if (target == null)
            {
                TargetName.Text = "";
                TargetOwner.Text = "";
                TargetShields.Text = "";
                TargetArmor.Text = "";
                return;
            }

            TargetName.Text = target.TargetShip.Name;
            TargetOwner.Text = target.TargetShip.Owner;
            TargetShields.Text = target.TargetShip.Shields.ToString(System.Globalization.CultureInfo.InvariantCulture);
            TargetArmor.Text = target.TargetShip.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Deal with weapons being fired.
        /// </summary>
        /// <param name="weapons">weapon to display</param>
        private void UpdateWeapons(BattleReport.Weapons weapons)
        {

            if (weapons == null)
            {
                WeaponPower.Text = "";
                ComponentTarget.Text = "";
                Damage.Text = "";
                return;
            }

            WeaponPower.Text = weapons.HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ComponentTarget.Text = weapons.Targeting;
            Damage.Text = "Ship damaged";

            UpdateTarget(weapons.WeaponTarget);
        }


        /// <summary>
        /// Deal with a ship being destroyed. Remove it from the containing stack and,
        /// if the stack fleet count drops to zero, destroy the whole stack.
        /// </summary>
        /// <param name="destroy"></param>
        private void UpdateDestroy(BattleReport.Destroy destroy)
        {
            Damage.Text = "Ship destroyed";

            Fleet stack = MyStacks[destroy.StackName] as Fleet;
            string shipName = stack.Owner + "/" + destroy.ShipName;

            if (stack.FleetShips.Contains(shipName))
            {
                stack.FleetShips.Remove(shipName);
            }

            if (stack.FleetShips.Count == 0)
            {
                MyStacks.Remove(stack.Name);
                BattlePanel.Invalidate();
            }
        }


        /// <summary>
        /// Just display the currrent step number in the battle replay control panel.
        /// </summary>
        private void SetStepNumber()
        {
            StringBuilder title = new StringBuilder();

            title.AppendFormat("Step {0} of {1}",
                               EventCount + 1, TheBattle.Steps.Count);

            StepNumber.Text = title.ToString();
        }

        #endregion

    }
}
