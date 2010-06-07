#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// Class to identify weapon capability and their targets which is sortable by
// weapon system initiative.
// ===========================================================================
#endregion

using Nova.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Nova.Common.Components;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Class to identify weapon capability and their targets which is sortable by
    /// weapon system initiative.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public class WeaponDetails : IComparable
    {
        public Fleet TargetStack = null;
        public Fleet SourceStack = null;
        public Weapon Weapon = null;

        public int CompareTo(Object rightHandSide)
        {
            WeaponDetails rhs = (WeaponDetails)rightHandSide;
            return this.Weapon.Initiative.CompareTo(rhs.Weapon.Initiative);
        }

    }
}

