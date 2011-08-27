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
// This class defines a Gate Property.
// ===========================================================================
#endregion

namespace Nova.Common.Components
{
    using System;
    using System.Xml;

    /// <summary>
    /// Gate class.
    /// </summary>
    [Serializable]
    public class Gate : ComponentProperty
    {
        public double SafeHullMass = 0;
        public double SafeRange = 0;

        #region Construction

         /// <summary>
        /// Default constructor.
        /// </summary>
        public Gate()
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">An existing <see cref="Gate"/> to copy.</param>
        public Gate(Gate existing)
        {
            this.SafeHullMass = existing.SafeHullMass;
            this.SafeRange = existing.SafeRange;
        }

        #endregion

        #region Interface IColneable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A copy of this <see cref="Gate"/>.</returns>
        public override object Clone()
        {
            return new Gate(this);
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

        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// (Gates are not addative, see the remarks).
        /// </summary>
        /// <param name="op1">LHS operand.</param>
        /// <param name="op2">RHS operand.</param>
        /// <returns>LHS operand.</returns>
        /// <remarks>
        /// Whilst it is possible to have more than one gate on a hull, and it could
        /// make sense to use the best capabilities of each gate, this is not how
        /// Stars! works with gates. One gate gets precedence and the other is ignored.
        /// TODO (priority 2) check that the same orbital slot takes precedence as
        /// in Stars!
        /// TODO (priority 3) add a game option to modify this behaviour so gates
        /// can add in a ?meaningful? way (or allow the best gate for the 
        /// circumstances to be used).
        /// </remarks>
        public static Gate operator +(Gate op1, Gate op2)
        {
            return op1;
        }

        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// Gates are not cumulative, see the remarks for the operator+.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Gate operator *(Gate op1, int scalar)
        {
            return op1;
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova compenent definition file (xml document).
        /// </param>
        public Gate(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "safehullmass")
                    {
                        SafeHullMass = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "saferange")
                    {
                        SafeRange = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // SafeHullMass
            XmlElement xmlelSafeHullMass = xmldoc.CreateElement("SafeHullMass");
            XmlText xmltxtSafeHullMass = xmldoc.CreateTextNode(this.SafeHullMass.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelSafeHullMass.AppendChild(xmltxtSafeHullMass);
            xmlelProperty.AppendChild(xmlelSafeHullMass);
            // SafeRange
            XmlElement xmlelSafeRange = xmldoc.CreateElement("SafeRange");
            XmlText xmltxtSafeRange = xmldoc.CreateTextNode(this.SafeRange.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelSafeRange.AppendChild(xmltxtSafeRange);
            xmlelProperty.AppendChild(xmlelSafeRange);

            return xmlelProperty;
        }

        #endregion
    }
}

