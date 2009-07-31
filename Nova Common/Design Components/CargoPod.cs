// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class defines a Cargo Pod component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Runtime.Serialization;

// ============================================================================
// CargoPod class
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public class CargoPod : ComponentProperty
   {
      public int Capacity = 0;


// ============================================================================
// Construction from scratch
// ============================================================================

      public CargoPod()
      {

      }


// ============================================================================
// Construction from an Component object
// ============================================================================

      public CargoPod(CargoPod existing)
      {
          this.Capacity = existing.Capacity;

      }

   }
}

