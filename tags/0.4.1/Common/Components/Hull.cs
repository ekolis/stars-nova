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
// This file defines the hull component. Always take a copy of a hull before
// populating it in the ship designer (otherwise the "master" version will end
// up getting modified).
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Xml;

namespace Nova.Common.Components
{

    /// <summary>
    /// The definition of a hull object.
    /// </summary>
    [Serializable]
    public class Hull : ComponentProperty
    {
        // Note: several hull properties _could_ be made by adding other properties
        // e.g. fuel / armor. However as all hulls (or many in the case of cargo)
        // have these properties it improves
        // the interface to include them here. Values supplied in additional property
        // tabs in the component edditor will be in addition to those in the base hull.
        public ArrayList Modules = null;
        public int FuelCapacity = 0;
        public int DockCapacity = 0;
        public int BaseCargo = 0; // Basic Cargo capacity of the empty hull (no pods)
        public int ARMaxPop = 0;
        public int ArmorStrength = 0;
        public int BattleInitiative = 0;

        #region Construction Initialisation

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Hull() { }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy constructor for the hull.
        /// </summary>
        /// <param name="existing">The <see cref="Hull"/> to be copied.</param>
        /// ----------------------------------------------------------------------------
        public Hull(Hull existing)
        {

            FuelCapacity     = existing.FuelCapacity;
            DockCapacity     = existing.DockCapacity;
            BaseCargo        = existing.BaseCargo;
            ARMaxPop         = existing.ARMaxPop;
            ArmorStrength    = existing.ArmorStrength;
            BattleInitiative = existing.BattleInitiative;

            Modules = new ArrayList();

            foreach (HullModule module in existing.Modules)
            {
                Modules.Add(module.Clone());
            }
        }

        #endregion

        #region ICloneable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public override object Clone()
        {
            return new Hull(this);
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
        /// Has no meaning in the context of a Hull.
        /// </summary>
        /// <param name="op1">First operand.</param>
        /// <param name="op2">Second operand.</param>
        /// <returns>The first operand.</returns>
        /// ----------------------------------------------------------------------------
        public static Hull operator +(Hull op1, Hull op2)
        {
            return op1;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// Has no meaning in the context of a Hull.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="scalar"></param>
        /// <returns>The first operand.</returns>
        /// ----------------------------------------------------------------------------
        public static Hull operator *(Hull op1, int scalar)
        {
            return op1;
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if this is a starbase hull.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool IsStarbase
        {
            get { return FuelCapacity == 0; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Determine if this is a starbase that can refuel.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool CanRefuel
        {
            get { return FuelCapacity == 0 && DockCapacity > 0; }
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
        public Hull(XmlNode node)
        {
            Modules = new ArrayList();
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "fuelcapacity")
                    {
                        FuelCapacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "dockcapacity")
                    {
                        DockCapacity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "basecargo")
                    {
                        BaseCargo = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "armaxpop")
                    {
                        ARMaxPop = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "armorstrength")
                    {
                        ArmorStrength = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "battleinitiative")
                    {
                        BattleInitiative = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (subnode.Name.ToLower() == "module")
                    {
                        HullModule module = new HullModule(subnode);
                        Modules.Add(module);
                    }

                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        /// ----------------------------------------------------------------------------
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // FuelCapacity
            XmlElement xmlelFuelCapacity = xmldoc.CreateElement("FuelCapacity");
            XmlText xmltxtFuelCapacity = xmldoc.CreateTextNode(this.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelFuelCapacity.AppendChild(xmltxtFuelCapacity);
            xmlelProperty.AppendChild(xmlelFuelCapacity);
            // DockCapacity
            XmlElement xmlelDockCapacity = xmldoc.CreateElement("DockCapacity");
            XmlText xmltxtDockCapacity = xmldoc.CreateTextNode(this.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelDockCapacity.AppendChild(xmltxtDockCapacity);
            xmlelProperty.AppendChild(xmlelDockCapacity);
            // ARMaxPop
            XmlElement xmlelARMaxPop = xmldoc.CreateElement("ARMaxPop");
            XmlText xmltxtARMaxPop = xmldoc.CreateTextNode(this.ARMaxPop.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelARMaxPop.AppendChild(xmltxtARMaxPop);
            xmlelProperty.AppendChild(xmlelARMaxPop);
            // BaseCargo
            XmlElement xmlelBaseCargo = xmldoc.CreateElement("BaseCargo");
            XmlText xmltxtBaseCargo = xmldoc.CreateTextNode(this.BaseCargo.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelBaseCargo.AppendChild(xmltxtBaseCargo);
            xmlelProperty.AppendChild(xmlelBaseCargo);
            // ArmorStrength
            XmlElement xmlelArmorStrength = xmldoc.CreateElement("ArmorStrength");
            XmlText xmltxtArmorStrength = xmldoc.CreateTextNode(this.ArmorStrength.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelArmorStrength.AppendChild(xmltxtArmorStrength);
            xmlelProperty.AppendChild(xmlelArmorStrength);
            // BattleInitiative
            XmlElement xmlelBattleInitiative = xmldoc.CreateElement("BattleInitiative");
            XmlText xmltxtBattleInitiative = xmldoc.CreateTextNode(this.BattleInitiative.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelBattleInitiative.AppendChild(xmltxtBattleInitiative);
            xmlelProperty.AppendChild(xmlelBattleInitiative);
            // Modules
            foreach (HullModule module in this.Modules)
            {
                xmlelProperty.AppendChild(module.ToXml(xmldoc));
            }

            return xmlelProperty;
        }

        #endregion

    }
}

