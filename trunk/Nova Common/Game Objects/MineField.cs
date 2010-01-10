// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Definition of a Minefield. Note that it over-rides the Key method to provide
// a simple incrementing number each time a new minefield is created. This
// ensures that each minefield can be used in hash tables without having to
// specify a "name" for the minefield.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Text;
using System.Drawing;

namespace NovaCommon
{
   [Serializable]
   public class Minefield : Item
   {
      public         int NumberOfMines = 0;
      public         int SafeSpeed     = 4;
      private static int KeyID         = 0;


// ============================================================================
// Constructor
// ============================================================================

      public Minefield()
      {
         KeyID++;
      }


// ============================================================================
// Determine the spacial radius of a Minefield. (To be improved.)
// ============================================================================

      public int Radius {
         get {
            return NumberOfMines;
         }
         
      }

// ============================================================================
// Override the Key method of Item
// ============================================================================

      public override string Key
      {
          get { return KeyID.ToString(System.Globalization.CultureInfo.InvariantCulture); }
      }
   }
 
}
