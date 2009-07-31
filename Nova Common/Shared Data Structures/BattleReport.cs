// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module contains the definition of a report on a battle;
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;
using System;

namespace NovaCommon
{
   [Serializable]
   public class BattleReport
   {

// ============================================================================
// A class to record a new stack position
// ============================================================================

      [Serializable]
      public class Movement
      {
         public string StackName = null;
         public Point  Position  = new Point();
      }


// ============================================================================
// A class to record a new target.
// ============================================================================

      [Serializable]
      public class Target
      {
         public Ship TargetShip  = null;
      }


// ============================================================================
// A class to destroy a ship in a given stack.
// ============================================================================

      [Serializable]
      public class Destroy
      {
         public string ShipName  = null;
         public string StackName = null;
      }


// ============================================================================
// A class to record weapons being fired.
// ============================================================================

      [Serializable]
      public class Weapons
      {
         public double HitPower     = 0;
         public string Targeting    = null;
         public Target WeaponTarget = new Target();
      }


// ============================================================================
// Main battle report components
// ============================================================================
 
      public string    Location  = null;
      public int       SpaceSize = 0;
      public ArrayList Steps     = new ArrayList();
      public Hashtable Stacks    = new Hashtable();
      public Hashtable Losses    = new Hashtable();

   }
}
