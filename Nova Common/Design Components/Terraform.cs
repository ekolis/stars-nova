// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This class defines a terraforming property.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using NovaCommon;

namespace NovaCommon
{
    /// <summary>
    /// Terraform class
    /// </summary>
    [Serializable]
    public class Terraform : ComponentProperty
    {
        public int MaxModifiedGravity     = 0;
        public int MaxModifiedTemperature = 0;
        public int MaxModifiedRadiation   = 0;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Terraform()
        {

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">The object to copy.</param>
        /// ----------------------------------------------------------------------------
        public Terraform(Terraform existing)
        {
            this.MaxModifiedGravity = existing.MaxModifiedGravity;
            this.MaxModifiedTemperature = existing.MaxModifiedTemperature;
            this.MaxModifiedRadiation = existing.MaxModifiedRadiation;
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
            return new Terraform(this);
        }

        #endregion

        #region Operators


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// </summary>
        /// <param name="op1">LHS operator</param>
        /// <param name="op2">RHS operator</param>
        /// <returns>A single terraform property that represents the stack.</returns>
        /// ----------------------------------------------------------------------------
        public static Terraform operator +(Terraform op1, Terraform op2)
        {
            Terraform sum = new Terraform(op1);
            sum.MaxModifiedGravity = Math.Max(op1.MaxModifiedGravity, op2.MaxModifiedGravity);
            sum.MaxModifiedRadiation = Math.Max(op1.MaxModifiedRadiation, op2.MaxModifiedRadiation);
            sum.MaxModifiedTemperature = Math.Max(op1.MaxModifiedTemperature, op2.MaxModifiedTemperature);
            return sum;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// Terraformers don't scale, as the modifications represent maximums.
        /// Note this represents the terraforming capability of a component,
        /// not multiple terraforming units produced by a planet, which work differently (1% each).
        /// </summary>
        /// <param name="op1">Property to be scaled.</param>
        /// <param name="scalar">Number of components in the stack.</param>
        /// <returns>A single property that represents the stack.</returns>
        /// ----------------------------------------------------------------------------
        public static Terraform operator *(Terraform op1, int scalar)
        {
            return op1;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from Xml
        /// </summary>
        /// <param name="node">
        /// node is a "Property" node with Type=="Terraform" in a Nova 
        /// compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public Terraform(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "maxmodifiedgravity")
                    {
                        MaxModifiedGravity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "maxmodifiedtemperature")
                    {
                        MaxModifiedTemperature = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "maxmodifiedradiation")
                    {
                        MaxModifiedRadiation = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Unable to load terraforming property: " + Environment.NewLine + e.Message);
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

            // MaxModifiedGravity
            XmlElement xmlelMaxModifiedGravity = xmldoc.CreateElement("MaxModifiedGravity");
            XmlText xmltxtMaxModifiedGravity = xmldoc.CreateTextNode(this.MaxModifiedGravity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMaxModifiedGravity.AppendChild(xmltxtMaxModifiedGravity);
            xmlelProperty.AppendChild(xmlelMaxModifiedGravity);
            // MaxModifiedTemperature
            XmlElement xmlelMaxModifiedTemperature = xmldoc.CreateElement("MaxModifiedTemperature");
            XmlText xmltxtMaxModifiedTemperature = xmldoc.CreateTextNode(this.MaxModifiedTemperature.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMaxModifiedTemperature.AppendChild(xmltxtMaxModifiedTemperature);
            xmlelProperty.AppendChild(xmlelMaxModifiedTemperature);
            // MaxModifiedRadiation
            XmlElement xmlelMaxModifiedRadiation = xmldoc.CreateElement("MaxModifiedRadiation");
            XmlText xmltxtMaxModifiedRadiation = xmldoc.CreateTextNode(this.MaxModifiedRadiation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMaxModifiedRadiation.AppendChild(xmltxtMaxModifiedRadiation);
            xmlelProperty.AppendChild(xmlelMaxModifiedRadiation);

            return xmlelProperty;
        }

        #endregion
    }
}

