#region Copyright Notice
// ============================================================================
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
// An abstract base class for the various properties (weapon, shield, Armor, etc.) which
// a component item may have. 
// Each component has a base property (set int the field Type), but may have 
// additional properties.
// For example Tritanium is a component with Type = "Armor" and one component
// property: Armor. Crobysharmor has Type = "Shield" with two component 
// properties: shield and armor.
// An abstract class is provided to allow all ComponentProperties to be grouped
// in the same collection within a Component object. The derived class can
// be determined from the dictionary key used to store the property.
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common.Components
{

    [Serializable]
    public abstract class ComponentProperty : ICloneable
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the ComponentProperty class.
        /// </summary>
        public ComponentProperty() { }


        /// <summary>
        /// Initializes a new instance of the ComponentProperty class.
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing <see cref="ComponentProperty"/> to copy.</param>
        public ComponentProperty(ComponentProperty existing)
        {
        }

        #endregion

        #region Interface IClonable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public virtual object Clone()
        {
            return null;
        }

        #endregion

        #region Operators

        // Note: this used to contain operators + and * which were then overriden in the daughter classes. 
        // This turned out to be a very bad idea as operator overloading and polymorphism don't mix well:
        // The base class operators were always called which didn't do what we want. To get the desired
        // result I have implemented these as abstract methods.

        /// <summary>
        /// Add the <see cref="ComponentProperty"/> op2 to the current property.
        /// </summary>
        /// <param name="op2">A <see cref="ComponentProperty"/> of the same daughter type as the calling object.</param>
        public abstract void Add(ComponentProperty op2);

        /// <summary>
        /// Scale up (multiply) this <see cref="ComponentProperty"/>, e.g. for a stack of Armor components in a <see cref="ShipDesign"/> slot.
        /// </summary>
        /// <param name="scalar">The number of components in the stack.</param>
        public abstract void Scale(int scalar);

        #endregion Operators

        #region Load Save Xml

        /// <summary>
        /// Initializes a new instance of the ComponentProperty class.
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        public ComponentProperty(XmlNode node)
        {
        }


        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        public virtual XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            return xmlelProperty;
        }

        #endregion

    }
}
