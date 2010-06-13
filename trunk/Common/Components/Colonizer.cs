﻿#region Copyright Notice
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
// This class defines a Colonizer property. 
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common.Components
{
    /// <summary>
    /// Colonizer class
    /// </summary>
    [Serializable]
    public class Colonizer : ComponentProperty
    {
        public bool Orbital = false;


        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Colonizer()
        {

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing <see cref="Colonizer"/> property to copy.</param>
        /// ----------------------------------------------------------------------------
        public Colonizer(Colonizer existing)
        {
            this.Orbital = existing.Orbital;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>Clone of this object.</returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Colonizer(this);
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
        /// <param name="op1">LHS operand</param>
        /// <param name="op2">RHS operand</param>
        /// <returns>Sum of the properties.</returns>
        /// ----------------------------------------------------------------------------
        public static Colonizer operator +(Colonizer op1, Colonizer op2)
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
        public static Colonizer operator *(Colonizer op1, int scalar)
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
        public Colonizer(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "orbital")
                    {
                        Orbital = bool.Parse(((XmlText)subnode.FirstChild).Value);
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
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

            // Orbital
            XmlElement xmlelOrbital = xmldoc.CreateElement("Orbital");
            XmlText xmltxtOrbital = xmldoc.CreateTextNode(this.Orbital.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelOrbital.AppendChild(xmltxtOrbital);
            xmlelProperty.AppendChild(xmlelOrbital);

            return xmlelProperty;
        }

        #endregion

    }
}

