#region Copyright Notice
// ============================================================================
// Copyright (C) 2010, 2011 stars-nova
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
// This class is used for constructing 1 defense unit.
// ===========================================================================
#endregion

namespace Nova.Common
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    /// <summary>
    /// Class for constructing 1 defense unit.
    /// </summary>
    public class DefenseProductionUnit : IProductionUnit
    {
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> to create the defense on.</param>
        public DefenseProductionUnit(Star star)
        {
        }

        #region ProductionUnit Members

        /// <summary>
        /// Return true if this production item is to be skipped.
        /// </summary>
        public bool IsSkipped()
        {
            throw new NotImplementedException();
        }

        public void Construct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the <see cref="Resources"/> needed to build this defense.
        /// ??? (priority 6) - is this before or after some resources have already been spent?.
        /// </summary>
        public Resources NeededResources()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
