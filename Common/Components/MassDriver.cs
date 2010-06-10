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
// This class defines an integer property used for any int value property.
// Possible uses are shield, armor, cargoPod, Mining Robot.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Runtime.Serialization;

// ============================================================================
// Simple Property Class
// ============================================================================

namespace Nova.Common.Components
{
    [Serializable]
    public class MassDriver : ComponentProperty
    {
        public int Value = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public MassDriver() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing"></param>
        /// ----------------------------------------------------------------------------
        public MassDriver(MassDriver existing)
        {
            this.Value = existing.Value;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="existing"></param>
        /// ----------------------------------------------------------------------------
        public MassDriver(int existing)
        {
            this.Value = existing;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new MassDriver(this);
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand.</param>
        /// <param name="op2">RHS operand</param>
        /// <returns>
        /// A <see cref="MassDriver"/> representing the sum of two mass drivers. 
        /// This is the best of the two or one warp higher if the same.
        /// </returns>
        /// ----------------------------------------------------------------------------
        public static MassDriver operator +(MassDriver op1, MassDriver op2)
        {
            if (op1.Value == op2.Value)
                return new MassDriver(op1.Value + 1);
            else
                return new MassDriver(Math.Max(op1.Value, op2.Value));
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// Mass Driver doesn't scale
        /// </summary>
        /// <param name="op1">The <see cref="MassDriver"/> to scale.</param>
        /// <param name="scalar">The number of mass drivers in the stack.</param>
        /// <returns>A mass driver representing the stack. +1 warp speed if more than one.</returns>
        /// ----------------------------------------------------------------------------
        public static MassDriver operator *(MassDriver op1, int scalar)
        {
            if (scalar >= 1) return new MassDriver(op1.Value + 1);
            return new MassDriver(op1.Value);
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
        public MassDriver(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "value")
                    {
                        Value = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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

            // store the value
            XmlElement xmlelValue = xmldoc.CreateElement("Value");
            XmlText xmltxtValue = xmldoc.CreateTextNode(this.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelValue.AppendChild(xmltxtValue);
            xmlelProperty.AppendChild(xmlelValue);

            return xmlelProperty;
        }

        #endregion

    }
}

