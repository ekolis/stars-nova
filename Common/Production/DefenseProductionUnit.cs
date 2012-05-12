#region Copyright Notice
// ============================================================================
// COpyright (C) 2010 Pavel Kazlou
// Copyright (C) 2011, 2012 The Stars-Nova Project
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
    /// Class for constructing 1 defense unit.
    /// </summary>
    public class DefenseProductionUnit : IProductionUnit
    {
        public Resources Cost
        {
            private set;
            get;
        }
                
        public Resources RemainingCost
        {
            private set;
            get;
        }
        
        public string Name
        {
            get {return "Defenses";}
        }
        
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> to create the defense on.</param>
        public DefenseProductionUnit()
        {
            Cost = RemainingCost = new Resources(0, 0, 0, Global.DefenceEnergyCost);
        }

        /// <summary>
        /// Return true if this production item is to be skipped.
        /// </summary>
        public bool IsSkipped(Star star)
        {
            throw new NotImplementedException();
        }

        public bool Construct(Star star)
        {
            throw new NotImplementedException();
        }
                
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            throw new NotImplementedException();
        }
    }
}
