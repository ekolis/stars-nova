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
// This class adds the properties that are specific to ship-based scanners.
// ===========================================================================
#endregion

namespace Nova.Common.Components
{
    using System;
    using System.Xml;

    /// <summary>
    /// Scanner class (a component property).
    /// </summary>
    [Serializable]
    public class Scanner : ComponentProperty
    {
        public int PenetratingScan = 0;
        public int NormalScan      = 0;
        
        #region Construction

        /// <summary>
        /// Initializes a new instance of the Scanner class.
        /// </summary>
        public Scanner()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Scanner class.
        /// </summary>
        /// <param name="existing">An existing Scanner to copy.</param>
        public Scanner(Scanner existing)
        {
            this.PenetratingScan = existing.PenetratingScan;
            this.NormalScan = existing.NormalScan;
        }

        #endregion

        #region Interface ICloneable

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>Clone of this object.</returns>
        public override object Clone()
        {
            return new Scanner(this);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            Scanner temp = this + (Scanner)op2;
            PenetratingScan = temp.PenetratingScan;
            NormalScan = temp.NormalScan;
        }

        /// <summary>
        /// Polymorphic multiplication of properties.
        /// </summary>
        /// <param name="scalar"></param>
        public override void Scale(int scalar)
        {
            Scanner temp = this * scalar;
            PenetratingScan = temp.PenetratingScan;
            NormalScan = temp.NormalScan;
        }

        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operand.</param>
        /// <param name="op2">RHS operand.</param>
        /// <returns>A single Scanner property with the scanning ability of both op1 and op2 scanners.</returns>
        public static Scanner operator +(Scanner op1, Scanner op2)
        {
            Scanner sum = new Scanner(op1);
            // 4th root of the sum of 4th powers (Manual section 9-7)
            sum.NormalScan = (int)Math.Pow(Math.Pow(op1.NormalScan, 4) + Math.Pow(op2.NormalScan, 4), 0.25);
            sum.PenetratingScan = (int)Math.Pow(Math.Pow(op1.PenetratingScan, 4) + Math.Pow(op2.PenetratingScan, 4), 0.25);
            return sum;
        }

        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// </summary>
        /// <param name="op1">Scanner property to scale.</param>
        /// <param name="scalar">Number of items in the stack.</param>
        /// <returns>A single Scanner property with the scanning ability of the stack.</returns>
        public static Scanner operator *(Scanner op1, int scalar)
        {
            Scanner sum = new Scanner(op1);
            // 4th root of the sum of 4th powers (Manual section 9-7)
            sum.NormalScan = (int)Math.Pow(Math.Pow(op1.NormalScan, 4) * scalar, 0.25);
            sum.PenetratingScan = (int)Math.Pow(Math.Pow(op1.PenetratingScan, 4) * scalar, 0.25);
            return sum;
        }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Initializes a new instance of the Scanner class.
        /// Loads from XML.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova component definition file (xml document).
        /// </param>
        public Scanner(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "penetratingscan")
                    {
                        PenetratingScan = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "normalscan")
                    {
                        NormalScan = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialize this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // PenetratingScan
            XmlElement xmlelPenetratingScan = xmldoc.CreateElement("PenetratingScan");
            XmlText xmltxtPenetratingScan = xmldoc.CreateTextNode(this.PenetratingScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelPenetratingScan.AppendChild(xmltxtPenetratingScan);
            xmlelProperty.AppendChild(xmlelPenetratingScan);
            // NormalScan
            XmlElement xmlelNormalScan = xmldoc.CreateElement("NormalScan");
            XmlText xmltxtNormalScan = xmldoc.CreateTextNode(this.NormalScan.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelNormalScan.AppendChild(xmltxtNormalScan);
            xmlelProperty.AppendChild(xmlelNormalScan);

            return xmlelProperty;
        }

        #endregion
    }
}
