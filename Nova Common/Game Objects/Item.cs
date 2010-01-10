// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Base class for most game items.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Drawing;
using System.Xml;
using System.Runtime.Serialization;

namespace NovaCommon
{

// ===========================================================================
// Item class. It is used as the base class for most game items. It consists
// of the following members:
// 
// Cost     The resource cost to build (germanium, ironium, etc.).
// Mass     The mass of the item (in kT).
// Name     The name of the derived item, for example the name of a star.
// Owner    The race name of the owner of this item (null if no owner). 
// Type     The type of the derived item (e.g. "ship", "star", etc.
// Position Position of the Item (if any)
// ===========================================================================

   [Serializable]
   public class Item
   {
      public  int       Mass     = 0;
      public  Resources Cost     = new Resources();
      public  string    Name     = null;
      public  string    Owner    = null;
      public  string    Type     = null;
      public  Point     Position = new Point(0, 0);
   

// ===========================================================================
// Default Construction
// ===========================================================================

      public Item()
      {
      }


// ===========================================================================
// Copy (initialising) constructor
// ===========================================================================

      public Item(Item existing)
      {
         if (existing == null) return;

         this.Mass     = existing.Mass;
         this.Name     = existing.Name;
         this.Owner    = existing.Owner;
         this.Type     = existing.Type;
         this.Position = existing.Position;
         this.Cost     = new Resources(existing.Cost);
      }


// ============================================================================
// Return a key for use in hash tables to locate items.
// ============================================================================

      public virtual string Key 
      {
         get { return this.Owner + "/" + this.Name; }
      }


   }
}
