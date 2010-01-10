// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Battle Viewer Dialog
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NovaCommon;

namespace Nova
{


// ============================================================================
// Dialog for viewing battle progress and outcome.
// ============================================================================

   public partial class BattleViewer : Form
   {
      private BattleReport TheBattle  = null;
      private int          EventCount = 0;
      private Hashtable    MyStacks   = new Hashtable();


// ============================================================================
// Construction
// ============================================================================

      public BattleViewer(NovaCommon.BattleReport thisBattle)
      {
         InitializeComponent();
         TheBattle  = thisBattle;
         EventCount = 0;

         // Take a copy of all of the stacks so that we can mess with them
         // without disturbing the master copy in the global turn file.

          foreach(Fleet stack in TheBattle.Stacks.Values) {
             MyStacks[stack.Name] = new Fleet(stack);
          }
      }


// ============================================================================
// Initialisation performed on a load of the whole dialog.
// ============================================================================

       private void OnLoad(object sender, EventArgs e)
       {
          BattleLocation.Text = TheBattle.Location;

          BattlePanel.BackgroundImage       = Nova.Resources.Plasma;
          BattlePanel.BackgroundImageLayout = ImageLayout.Stretch;
          SetStepNumber();
       }


// ============================================================================
// Draw the battle panel by placing the images for the stacks in the
// appropriate position.
// ============================================================================

       private void OnPaint(object sender, PaintEventArgs e)
       {
          base.OnPaint(e); //added

          Graphics graphics      = e.Graphics;
          Size     panelSize     = BattlePanel.Size;
          float    scalingFactor = (float) panelSize.Width /
                                   (float) Global.MaxWeaponRange;

          graphics.PageScale = scalingFactor;
          graphics.ScaleTransform(0.5F, 0.5F);

          foreach(Fleet stack in MyStacks.Values) {
             graphics.DrawImage(stack.Image, stack.Position);
          }
       }


// ============================================================================
// Step through each battle event.
// ============================================================================

       private void NextStep_Click(object sender, EventArgs e)
       {
          Object thisStep = TheBattle.Steps[EventCount];
          SetStepNumber();

          if (thisStep is BattleReport.Movement) {
             UpdateMovement(thisStep as BattleReport.Movement);
          }
          else if (thisStep is BattleReport.Target) {
             UpdateTarget(thisStep as BattleReport.Target);
          }
          else if (thisStep is BattleReport.Weapons) {
             UpdateWeapons(thisStep as BattleReport.Weapons);
          }
          else if (thisStep is BattleReport.Destroy) {
             UpdateDestroy(thisStep as BattleReport.Destroy);
          }

          if (EventCount < TheBattle.Steps.Count - 1) {
             EventCount++;
          }
          else {
             NextStep.Enabled = false;
          }

       }


// ============================================================================
// Update the movement of a stack.
// ============================================================================

      private void UpdateMovement(BattleReport.Movement movement)
      {
         Fleet stack     = MyStacks[movement.StackName] as Fleet;
         MovedTo.Text    = movement.Position.ToString();
         StackOwner.Text = stack.Owner;
         stack.Position  = movement.Position;

         // We have moved, clear out the other fields as they may change.

         UpdateTarget(null);
         UpdateWeapons(null);

         BattlePanel.Invalidate();
      }


// ============================================================================
// Update the current target details.
// ============================================================================

      private void UpdateTarget(BattleReport.Target target)
      {
         if (target == null) {
            TargetName.Text    = "";
            TargetOwner.Text   = "";
            TargetShields.Text = "";
            TargetArmor.Text  = "";
            return;
         }

         TargetName.Text    = target.TargetShip.Name;
         TargetOwner.Text   = target.TargetShip.Owner;
         TargetShields.Text = target.TargetShip.Shields.ToString(System.Globalization.CultureInfo.InvariantCulture);
         TargetArmor.Text  = target.TargetShip.Armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }


// ============================================================================
// Deal with weapons being fired.
// ============================================================================

      private void UpdateWeapons(BattleReport.Weapons weapons)
      {

         if (weapons == null) {
            WeaponPower.Text     = "";
            ComponentTarget.Text = "";
            Damage.Text          = "";
            return;
         }

         WeaponPower.Text     = weapons.HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture);
         ComponentTarget.Text = weapons.Targeting;
         Damage.Text          = "Ship damaged";

         UpdateTarget(weapons.WeaponTarget);
      }


// ============================================================================
// Deal with a ship being destroyed. Remove it from the containing stack and,
// if the stack fleet count drops to zero, destroy the whole stack.
// ============================================================================

      private void UpdateDestroy(BattleReport.Destroy destroy)
      {
         Damage.Text = "Ship destroyed";

         Fleet  stack    = MyStacks[destroy.StackName] as Fleet;
         string shipName = stack.Owner + "/" + destroy.ShipName;

         if (stack.FleetShips.Contains(shipName)) {
            stack.FleetShips.Remove(shipName);
         }

         if (stack.FleetShips.Count == 0) {
            MyStacks.Remove(stack.Name);
            BattlePanel.Invalidate();
         }
      }


// ============================================================================
// Just display the currrent step number in the battle replay control panel.
// ============================================================================

      private void SetStepNumber()
      {
         StringBuilder title = new StringBuilder();

         title.AppendFormat("Step {0} of {1}",
                            EventCount + 1, TheBattle.Steps.Count);
         
         StepNumber.Text = title.ToString();
      }

   }
}
