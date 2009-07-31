// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Definition of a battle plan.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Text;

namespace NovaCommon
{
   [Serializable]
   public class BattlePlan
   {
      public string Name            = "Default";
      public string PrimaryTarget   = "Armed Ships";
      public string SecondaryTarget = "Any";
      public string Tactic          = "Maximise Damage";
      public string Attack          = "Enemies";
   }
}
