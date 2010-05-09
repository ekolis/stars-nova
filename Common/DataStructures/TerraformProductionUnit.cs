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
// This class is used for "constructing" terraform 1%.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Common
{
    /// <summary>
    /// 
    /// </summary>
    class TerraformProductionUnit : ProductionUnit
    {
        private Star star;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor
        /// </summary>
        /// <param name="star">The star that is producing this item</param>
        /// ----------------------------------------------------------------------------
        public TerraformProductionUnit(Star star)
        {
            this.star = star;
        }

        #region ProductionUnit Members

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Returns true if this production item is to be skipped this year.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsSkipped()
        {
            throw new NotImplementedException();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// "Construct" a 1% terraform.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Construct()
        {
            throw new NotImplementedException();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the Resources needed for this production item.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Resources NeededResources()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
