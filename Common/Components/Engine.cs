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
// Definition of an engine object.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nova.Common.Components
{

    /// <summary>
    /// Engine property
    /// </summary>
    [Serializable]
    public class Engine : ComponentProperty
    {
        public int[] FuelConsumption = new int[10];
        public bool RamScoop;
        public int FastestSafeSpeed;
        public int OptimalSpeed;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Engine() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">The existing <see cref="Engine"/> to copy.</param>
        /// ----------------------------------------------------------------------------
        public Engine(Engine existing)
        {
            for (int i = 0; i < FuelConsumption.Length; i++)
            {
                this.FuelConsumption[i] = existing.FuelConsumption[i];
            }
            this.RamScoop = existing.RamScoop;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A copy of this <see cref="Engine"/> property.</returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Engine(this);
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
        /// <returns>op1 - engines don't add in Stars! or Nova</returns>
        /// ----------------------------------------------------------------------------
        public static Engine operator +(Engine op1, Engine op2)
        {
            return op1;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand</param>
        /// <param name="scalar">Scaling factor.</param>
        /// <returns>op1 - engines don't scale in Nova</returns>
        /// ----------------------------------------------------------------------------
        public static Engine operator *(Engine op1, int scalar)
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
        public Engine(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "ramscoop":
                            RamScoop = bool.Parse(((XmlText)subnode.FirstChild).Value);
                            break;
                        case "fastestsafespeed":
                            FastestSafeSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "optimalspeed":
                            OptimalSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "fuelconsumption":
                            {
                                // load each fuel consumption value

                                for (int warp = 0; warp < FuelConsumption.Length; warp++)
                                {

                                    FuelConsumption[warp] = int.Parse(((XmlText)subnode.SelectSingleNode("Warp" + warp.ToString(System.Globalization.CultureInfo.InvariantCulture)).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                }
                                break;
                            }
                    }

                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
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

            // RamScoop
            XmlElement xmlelRamScoop = xmldoc.CreateElement("RamScoop");
            XmlText xmltxtRamScoop = xmldoc.CreateTextNode(this.RamScoop.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelRamScoop.AppendChild(xmltxtRamScoop);
            xmlelProperty.AppendChild(xmlelRamScoop);
            // FastestSafeSpeed
            XmlElement xmlelFastestSafeSpeed = xmldoc.CreateElement("FastestSafeSpeed");
            XmlText xmltxtFastestSafeSpeed = xmldoc.CreateTextNode(this.FastestSafeSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelFastestSafeSpeed.AppendChild(xmltxtFastestSafeSpeed);
            xmlelProperty.AppendChild(xmlelFastestSafeSpeed);
            // Optimal Speed
            XmlElement xmlelOptimalSpeed = xmldoc.CreateElement("OptimalSpeed");
            XmlText xmltxtOptimalSpeed = xmldoc.CreateTextNode(this.OptimalSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelOptimalSpeed.AppendChild(xmltxtOptimalSpeed);
            xmlelProperty.AppendChild(xmlelOptimalSpeed);
            // FuelConsumption
            XmlElement xmlelFuelConsumption = xmldoc.CreateElement("FuelConsumption");
            for (int warp = 0; warp < FuelConsumption.Length; warp++)
            {
                XmlElement xmlelWarp = xmldoc.CreateElement("Warp" + warp.ToString(System.Globalization.CultureInfo.InvariantCulture));
                XmlText xmltextWarp = xmldoc.CreateTextNode(this.FuelConsumption[warp].ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelWarp.AppendChild(xmltextWarp);
                xmlelFuelConsumption.AppendChild(xmlelWarp);
            }
            xmlelProperty.AppendChild(xmlelFuelConsumption);

            return xmlelProperty;
        }

        #endregion

    }
}
