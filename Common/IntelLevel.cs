#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
    /// Enumerates the different levels of knowledge amount
    /// for objects such as Stars and Fleets.
    /// </summary>
    public enum IntelLevel
    {
        None = 0, // never visited        
        InScan = 1, // in range of non-penetrating scanner - not much better than None.
        InPlace = 2, // Have a fleet in orbit
        InDeepScan = 3, // In range of a penetrating scanner.
        Owned = 4 // Best kind of star ;)
    }
}

