// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class defines a beam armour component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Runtime.Serialization;

// ============================================================================
// Beam armour class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class Armor : ComponentProperty
   {
      public int Strength = 0;
      public int Shields  = 0;


// ============================================================================
// Construction from scratch
// ============================================================================

      public Armor()
      {

      }


// ============================================================================
// Construction from an existing Armor object
// ============================================================================

      public Armor(Armor existing) 
      {
          this.Strength = existing.Strength;
          this.Shields = existing.Shields;

      }

   }
}

