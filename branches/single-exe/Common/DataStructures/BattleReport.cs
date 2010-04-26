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
// This module contains the definition of a report on a battle;
// ===========================================================================
#endregion

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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record a new stack position
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Movement
        {
            public string StackName = null;
            public Point Position   = new Point();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record a new target.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Target
        {
            public Ship TargetShip = null;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to destroy a ship in a given stack.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Destroy
        {
            public string ShipName  = null;
            public string StackName = null;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record weapons being fired.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Weapons
        {
            public double HitPower     = 0;
            public string Targeting    = null;
            public Target WeaponTarget = new Target();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Main battle report components
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string Location  = null;
        public int SpaceSize    = 0;
        public ArrayList Steps  = new ArrayList();
        public Hashtable Stacks = new Hashtable();
        public Hashtable Losses = new Hashtable();

    }
}
