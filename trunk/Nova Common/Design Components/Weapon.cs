// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova
//
// This class defines a weapon component.
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
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Enumeration of weapon types.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public enum WeaponType
    {
        standardBeam,
        shieldSapper,
        gatlingGun,
        torpedo,
        missile
    }

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Weapon class
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class Weapon : ComponentProperty
    {
        public int Power = 0;
        public int Range = 0;
        public int Initiative = 0;
        public int Accuracy = 0;
        public WeaponType Group = WeaponType.standardBeam;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Weapon()
        {
            this.Group = WeaponType.standardBeam;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="Weapon"/> compoenent.</param>
        /// ----------------------------------------------------------------------------
        public Weapon(Weapon existing)
        {
            this.Power = existing.Power;
            this.Range = existing.Range;
            this.Initiative = existing.Initiative;
            this.Accuracy = existing.Accuracy;
            this.Group = existing.Group;
        }

        #endregion

        #region Interface ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A clone of this <see cref="Weapon"/></returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Weapon(this);
        }

        #endregion

        #region Operators

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// only power adds, and this only makes sense if the weapons are the same
        /// </summary>
        /// <param name="op1">LHS operator.</param>
        /// <param name="op2">RHS operator.</param>
        /// <returns>A single <see cref="Weapon"/> object that represents the stack.</returns>
        /// ----------------------------------------------------------------------------
        public static Weapon operator +(Weapon op1, Weapon op2)
        {
            if (op1.Group != op2.Group)
            {
                Report.Error("Weapon.operator+ Attempted to add weapons of different Groups.");
                return op1;
            }
            Weapon sum = new Weapon(op1);
            sum.Power = op1.Power + op2.Power;
            return sum;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// only power adds, and this only makes sense if the weapons are the same
        /// </summary>
        /// <param name="op1">The <see cref="Weapon"/> to be scaled.</param>
        /// <param name="scalar">The number of components in the stack.</param>
        /// <returns>A single <see cref="Weapon"/> that represents the stack.</returns>
        /// ----------------------------------------------------------------------------
        public static Weapon operator *(Weapon op1, int scalar)
        {
            Weapon sum = new Weapon(op1);
            sum.Power = op1.Power * scalar;
            return sum;
        }

        #endregion

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from Xml.
        /// </summary>
        /// <param name="node">
        /// node is a "Property" node with Type=="Weapon" in a Nova 
        /// compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public Weapon(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "power":
                            Power = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "range":
                            Range = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "initiative":
                            Initiative = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "accuracy":
                            Accuracy = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "group":
                            switch (((XmlText)subnode.FirstChild).Value.ToLower())
                            {
                                case "standardweapon":
                                    Group = WeaponType.standardBeam; break;
                                case "shieldsapper":
                                    Group = WeaponType.shieldSapper; break;
                                case "gatlinggun":
                                    Group = WeaponType.gatlingGun; break;
                                case "torpedo":
                                    Group = WeaponType.torpedo; break;
                                case "missile":
                                    Group = WeaponType.missile; break;
                            }
                            break;
                    }

                }
                catch (Exception e)
                {
                    Report.Error("Error loading weapon component" + Environment.NewLine + e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }


        /// ----------------------------------------------------------------------------
        /// /// <summary>
        /// Save as Xml
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>an XmlElement representation of the Property</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // Initiative
            XmlElement xmlelInitiative = xmldoc.CreateElement("Initiative");
            XmlText xmltxtInitiative = xmldoc.CreateTextNode(this.Initiative.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelInitiative.AppendChild(xmltxtInitiative);
            xmlelProperty.AppendChild(xmlelInitiative);
            // Power
            XmlElement xmlelPower = xmldoc.CreateElement("Power");
            XmlText xmltxtPower = xmldoc.CreateTextNode(this.Power.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelPower.AppendChild(xmltxtPower);
            xmlelProperty.AppendChild(xmlelPower);
            // Accuracy
            XmlElement xmlelAccuracy = xmldoc.CreateElement("Accuracy");
            XmlText xmltxtAccuracy = xmldoc.CreateTextNode(this.Accuracy.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelAccuracy.AppendChild(xmltxtAccuracy);
            xmlelProperty.AppendChild(xmlelAccuracy);
            // Range
            XmlElement xmlelRange = xmldoc.CreateElement("Range");
            XmlText xmltxtRange = xmldoc.CreateTextNode(this.Range.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelRange.AppendChild(xmltxtRange);
            xmlelProperty.AppendChild(xmlelRange);
            // Group
            XmlElement xmlelGroup = xmldoc.CreateElement("Group");
            XmlText xmltxtGroup = xmldoc.CreateTextNode(this.Group.ToString());
            xmlelGroup.AppendChild(xmltxtGroup);
            xmlelProperty.AppendChild(xmlelGroup);


            return xmlelProperty;
        }

        #endregion

        #region Properties

        // ============================================================================
        // Classify the various types as either beam or missile. Use weapon.Type to
        //   determine the exact weapon type.
        // ============================================================================

        public bool IsBeam
        {
            get { return (Group == WeaponType.standardBeam || Group == WeaponType.shieldSapper || Group == WeaponType.gatlingGun); }
        }

        public bool IsMissile
        {

            get { return (Group == WeaponType.torpedo || Group == WeaponType.missile); }
        }

        #endregion
    }
}

