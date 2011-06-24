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
// This class is used for constructing 1 ship.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Nova.Common.Components;

namespace Nova.Common
{
    /// <summary>
    /// ShipProductionUnit class.
    /// </summary>
    public class ShipProductionUnit : IProductionUnit
    {
        private ShipDesign shipDesign;


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="star">Star with this production queue.</param>
        /// <param name="shipDesign"><see cref="ShipDesign"/> to produce.</param>
        /// ----------------------------------------------------------------------------
        public ShipProductionUnit(Star star, ShipDesign shipDesign)
        {
            this.shipDesign = shipDesign;
        }
        
        #region ProductionUnit Members


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return true if production of this item will be skipped.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsSkipped()
        {
            throw new NotImplementedException();
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construct the ship.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Construct()
        {
            throw new NotImplementedException();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the resources needed for construction.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources NeededResources()
        {
            return shipDesign.Cost;
        }

        #endregion
    }
}
