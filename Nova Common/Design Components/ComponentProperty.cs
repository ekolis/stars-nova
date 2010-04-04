// ============================================================================
// Nova. 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
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
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{

    [Serializable]
    public abstract class ComponentProperty : ICloneable
    {
        #region Construction

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ComponentProperty() 
        {
        }


        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="ComponentProperty"/> to copy.</param>
        public ComponentProperty(ComponentProperty existing)
        {
        }

        #endregion

        #region Interface IClonable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return null;
        }

        #endregion

        #region Operators

        // ??? (priority 4) - Some daughters return the LHS operator (when the operation has no effect). Is there any danger in this? Should they return a Clone?

        /// <summary>
        /// operator+ to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand</param>
        /// <param name="op2">RHS operand</param>
        /// <returns>op1</returns>
        public static ComponentProperty operator +(ComponentProperty op1, ComponentProperty op2)
        {
            return op1;
        }


        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Property to be scaled.</param>
        /// <param name="scalar">Scaling factor.</param>
        /// <returns>op1</returns>
        public static ComponentProperty operator *(ComponentProperty op1, int scalar)
        {
            return op1;
        }

        #endregion Operators

        #region Load Save Xml

        /// <summary>
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
