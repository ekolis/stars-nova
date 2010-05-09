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
// This module describes what we know about each star system we have visited 
// or scanned.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Drawing;
using Nova.Common;

namespace Nova.Client
{

    /// <summary>
    /// Report class.
    /// </summary>
    [Serializable]
    public class StarReport
    {
        public Nova.Common.Resources StarResources = null;
        public Nova.Common.Resources Concentration = null;
        public int Population;
        public int Age;
        public string StarName;
        public int Radiation;
        public int Gravity;
        public int Temperature;
        public string Owner;
        public Fleet Starbase;
        public Point Position;
        public bool OrbitingFleets;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> being reported</param>
        /// ----------------------------------------------------------------------------
        public StarReport(Star star)
        {
            StarResources  = new Nova.Common.Resources(star.ResourcesOnHand);
            Concentration  = new Nova.Common.Resources(star.MineralConcentration);
            Population     = star.Colonists;
            StarName       = star.Name;
            Radiation      = star.Radiation;
            Gravity        = star.Gravity;
            Temperature    = star.Temperature;
            Owner          = star.Owner;
            Starbase       = star.Starbase;
            Position       = star.Position;
            OrbitingFleets = star.OrbitingFleets;
        }
    }
}
