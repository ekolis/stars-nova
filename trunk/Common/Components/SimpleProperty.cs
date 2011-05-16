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
// This class defines a simple property used for any property which has no 
// value - it simply exists or not. For example 'Transport Ships Only' or
// 'Unarmed Ships Only' or 'Doubles Minelayer Efficiency'.
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common.Components
{
    /// <summary>
    /// Simple Property Class
    /// </summary>
    [Serializable]
    public class SimpleProperty : ComponentProperty
    {
        // This property has no data!

        #region Construction

        /// ----------------------------------------------------------------------------
        /// /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public SimpleProperty() 
        { 
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="existing">Existing property to copy.</param>
        /// ----------------------------------------------------------------------------
        public SimpleProperty(SimpleProperty existing) 
        { 
        }

        #endregion

        #region Interface ICloneable


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new SimpleProperty();
        }

        #endregion

        #region Operators

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            return;
        }

        /// <summary>
        /// Polymorphic multiplication of properties.
        /// </summary>
        /// <param name="scalar"></param>
        public override void Scale(int scalar)
        {
            return;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operator.</param>
        /// <param name="op2">RHS operator.</param>
        /// <returns>op1</returns>
        /// ----------------------------------------------------------------------------
        public static SimpleProperty operator +(SimpleProperty op1, SimpleProperty op2)
        {
            return op1;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property to scale.</param>
        /// <param name="scalar">Number of instances of this property.</param>
        /// <returns>A single property that represents all these instances.</returns>
        /// ----------------------------------------------------------------------------
        public static SimpleProperty operator *(SimpleProperty op1, int scalar)
        {
            return op1;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public SimpleProperty(XmlNode node) 
        { 
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            return xmlelProperty;
        }

        #endregion
    }
}

