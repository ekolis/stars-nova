#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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


namespace Nova.Common
{
    using System;
    
    /// <summary>
    /// Useful methods to extract values right from the Key
    /// instead of Item.
    /// </summary>
    public static class KeyExtensions
    {
        private const long IdMask = 0x00000000FFFFFFFF;
        private const long OwnerMask = 0x000000FF00000000;
        
        public static ushort Owner(this long key)
        {
            return (ushort)((key & OwnerMask) >> 32);
        }
        
        public static uint Id(this long key)
        {
            return (uint)(key & IdMask);
        }
        
        public static long SetOwner(this long key, ushort owner)
        {
            if (owner > 0xFF || owner < 0) 
            { 
                throw new ArgumentException("OwnerId out of range"); 
            }
            key &= IdMask;
            key |= (long)owner << 32;
            
            return key;
        }
        
        public static long SetId(this long key, uint id)
        {
            if (id > IdMask || id < 0) 
            { 
                throw new ArgumentException("ItemId out of range"); 
            }
            key &= OwnerMask;
            key |= id;
            
            return key;
        }
    }
}
