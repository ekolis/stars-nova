// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
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
// ============================================================================

using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{

    [Serializable]
    public abstract class ComponentProperty : ICloneable, IXmlSerializable
    {


        //============================================================================
        // Default Constructor
        //============================================================================
        public ComponentProperty() 
        {
        }

        //============================================================================
        // Copy Constructor
        //============================================================================
        public ComponentProperty(ComponentProperty existing)
        {
        }

        //============================================================================
        // Implement the ICloneable interface so properties can be cloned.
        //============================================================================
        public virtual object Clone()
        {
            return null;
        }

        //============================================================================
        // operator+ to add properties in the ship design.
        //============================================================================
        public static ComponentProperty operator +(ComponentProperty op1, ComponentProperty op2)
        {
            return op1;
        }

        //============================================================================
        // Operator* to scale (multiply) properties in the ship design.
        //============================================================================
        public static ComponentProperty operator *(ComponentProperty op1, int scalar)
        {
            return op1;
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Race_Restrictions" node in a Nova compenent definition file (xml document).
        // ============================================================================
        public ComponentProperty(XmlNode node)
        {
        }

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml(XmlReader reader); // TODO XML deserialization of ComponentProperty
        public abstract void WriteXml(XmlWriter writer);
    }
}
