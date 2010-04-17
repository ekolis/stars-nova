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
// TraitEntry: 
// An object containing all the data relating to a single Primary or lesser
// racial trait.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    /// <summary>
    /// Class to support the format of each trait. Static definitions of all the 
    /// primary and secondary racial traits are in PrimaryTraits.cs and SecondaryTraits.cs
    /// (both primary and lesser racial traits use this).
    /// </summary>
    [Serializable]
    public class TraitEntry
    {
        public String Name;        // e.g. "Hyper Expansion" or "Regenerating Shields" (may contain spaces)
        public String Code;        // e.g. "HE" or "RS" (must be unique, all caps, no spaces or punctuation) 
        public int    Cost;        // in advantage points, negative cost give more points to buy other things
        public String Description; // Detailed description (paragraph).

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Trait constructor. In most instances a string containing the trait code is 
        /// sufficient. Use this when it is necessary to have acess to all the details of a trait.
        /// </summary>
        /// <param name="n">Name e.g. "Hyper Expansion" or "Regenerating Shields" (may contain spaces)</param>
        /// <param name="a">Code e.g. "HE" or "RS" (must be unique, all caps, no spaces or punctuation)</param>
        /// <param name="c">Cost in advantage points, negative cost give more points to buy other things</param>
        /// <param name="d">Detailed description (paragraph).</param>
        /// ----------------------------------------------------------------------------
        public TraitEntry(String n, String a, int c, String d)
        {
            Name = n;
            Code = a;
            Cost = c;
            Description = d;
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the TraitKey for a TraitEntry
        /// </summary>
        /// ----------------------------------------------------------------------------
        public override int GetHashCode()
        {
            int hash = 1;
            foreach (String key in AllTraits.TraitKeys)
            {
                if (key == this.Code) return hash;
                else ++hash;
            }
            return hash;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a String representation of the TraitEntry
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public new String ToString()
        {
            return Name;
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>
        /// ----------------------------------------------------------------------------
        public static bool operator ==(TraitEntry a, TraitEntry b)
        {
            return a.Code == b.Code;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>
        /// ----------------------------------------------------------------------------
        public override bool Equals(object trait)
        {
            if (trait is TraitEntry)
                return (Code == ((TraitEntry)trait).Code);
            else
                return false;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>
        /// ----------------------------------------------------------------------------
        public static bool operator !=(TraitEntry a, TraitEntry b)
        {
            return a.Code != b.Code;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>       
        /// ----------------------------------------------------------------------------
        public static bool operator ==(String a, TraitEntry b)
        {
            return a == b.Code;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for in-equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>       
        /// ----------------------------------------------------------------------------
        public static bool operator !=(String a, TraitEntry b)
        {
            return a != b.Code;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for equality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>       
        /// ----------------------------------------------------------------------------
        public static bool operator ==(TraitEntry a, String b)
        {
            return a.Code == b;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Test for inequality.
        /// </summary>
        /// <param name="a">Trait to compare.</param>
        /// <param name="b">Trait to compare</param>
        /// <returns>true if the traits are the same trait.</returns>       
        /// ----------------------------------------------------------------------------
        public static bool operator !=(TraitEntry a, String b)
        {
            return a.Code != b;
        }

        #endregion

    }

}
