#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 stars-nova
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
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Implementation of ProductionUnit for constructing one mine.
    /// </summary>
    public class MineProductionUnit : IProductionUnit
    {
        private Star star;

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="star">Star on which the mine is to be constructed.</param>
        public MineProductionUnit(Star star)
        {
            this.star = star;
        }

        /// <summary>
        /// Return true if this item will be skipped in production.
        /// </summary>
        public bool IsSkipped()
        {
            if (star.Mines >= star.GetOperableMines())
            {
                return true;
            }

            if (!(star.ResourcesOnHand >= NeededResources()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Produce the mine.
        /// </summary>
        public void Construct()
        {
            if (IsSkipped())
            {
                return;
            }

            star.ResourcesOnHand = star.ResourcesOnHand - NeededResources();
            star.Mines++;
        }

        /// <summary>
        /// Returns the <see cref="Resources"/> needed to build this Mine.
        /// </summary>
        public Resources NeededResources()
        {
            return star.ThisRace.GetMineResources();
        }
    }
}
