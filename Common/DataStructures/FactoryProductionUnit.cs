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

#region Module Description
// ===========================================================================
// This class is used for constructing 1 factory.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Common
{
    /// <summary>
    /// Implementation of ProductionUnit for factory building.
    /// </summary>
    class FactoryProductionUnit : ProductionUnit
    {
        private Star star;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor
        /// </summary>
        /// <param name="star">Star on which the factory is to be constructed</param>
        /// ----------------------------------------------------------------------------
        public FactoryProductionUnit(Star star)
        {
            this.star = star;
        }
        
        #endregion

        #region ProductionUnit Members

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Returns true if this production item will be skipped.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsSkipped()
        {
            if (star.Factories >= star.GetOperableFactories())
            {
                return true;
            }

            if (!(star.ResourcesOnHand >= NeededResources()))
            {
                return true;
            }

            return false;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construct one factory.
        /// FIXME (priority 4) - doesn't account for partial construction.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Construct()
        {
            if (IsSkipped())
            {
                return;
            }

            star.ResourcesOnHand = star.ResourcesOnHand - NeededResources();
            star.Factories++;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Returns the Resources needed to construct this factory.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public Resources NeededResources()
        {
            return star.ThisRace.GetFactoryResources();
        }

        #endregion
    }
}
