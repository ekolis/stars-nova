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
    using System.Xml;

    /// <summary>
    /// This class is used for "constructing" terraform 1%.
    /// </summary>
    public class TerraformProductionUnit : IProductionUnit
    {
        private Resources cost;
        private Resources remainingCost;        
        
        public Resources Cost
        {
            get {return cost;}
        }
                
        public Resources RemainingCost
        {
            get {return remainingCost;}
        }
        
        public string Name
        {
            get {return "Terraform";}
        }
        
        /// <summary>
        /// initializing constructor.
        /// </summary>
        /// <param name="star">The star that is producing this item.</param>
        public TerraformProductionUnit()
        {
        }

        /// <summary>
        /// Returns true if this production item is to be skipped this year.
        /// </summary>
        public bool IsSkipped(Star star)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Construct a 1% terraform.
        /// </summary>
        public bool Construct(Star star)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the Resources needed for this production item.
        /// </summary>
        public Resources NeededResources()
        {
            throw new NotImplementedException();
        }
        
                
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            throw new NotImplementedException();
        }
    }
}
