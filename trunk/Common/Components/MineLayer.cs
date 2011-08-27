#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 stars-nova
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

namespace Nova.Common.Components
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class defines a mine-layer property.
    /// </summary>
    [Serializable]
    public class MineLayer : ComponentProperty
    {
        // Only three types of mine layers are recognised, and they are distinguised by their HitChance:
        public static double HeavyHitChance = 1.0;
        public static double SpeedTrapHitChance = 3.5;
        public static double StandardHitChance = 0.3;

        // Standar Mine Stats
        public int LayerRate = 50;
        public int SafeSpeed = 4;
        public double HitChance = 0.3;
        public int DamagePerEngine = 100;
        public int DamagePerRamScoop = 125;
        public int MinFleetDamage = 500;
        public int MinRamScoopDamage = 600;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MineLayer()
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing"><see cref="MineLayer"/> to copy.</param>
        public MineLayer(MineLayer existing)
        {
            this.LayerRate         = existing.LayerRate;
            this.SafeSpeed         = existing.SafeSpeed;
            this.HitChance         = existing.HitChance;
            this.DamagePerEngine   = existing.DamagePerEngine;
            this.DamagePerRamScoop = existing.DamagePerRamScoop;
            this.MinFleetDamage    = existing.MinFleetDamage;
            this.MinRamScoopDamage = existing.MinRamScoopDamage;
        }

        /// <summary>
        /// Implement the ICloneable interface so properties can be cloned.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override object Clone()
        {
            return new MineLayer(this);
        }

        /// <summary>
        /// Polymorphic addition of properties.
        /// </summary>
        /// <param name="op2"></param>
        public override void Add(ComponentProperty op2)
        {
            if (HitChance != ((MineLayer)op2).HitChance)
            {
                Report.Error("MineLayer.operator+ Attempted to add together different types of mine layers.");
                return;
            }
            LayerRate += ((MineLayer)op2).LayerRate;
        }

        /// <summary>
        /// Polymorphic multiplication of properties.
        /// </summary>
        /// <param name="scalar"></param>
        public override void Scale(int scalar)
        {
            LayerRate *= scalar;
        }

        /// <summary>
        /// Provide a way to add properties in the ship design.
        /// Only laying rates sum up, and this only makes sense if the mine layers produce
        /// the same mines. 
        /// </summary> 
        /// <remarks>
        /// In Stars! mines are the same if HitChance is the 
        /// same, and that test is used here.
        /// </remarks>
        /// <param name="op1">LHS operator.</param>
        /// <param name="op2">RHS operator.</param>
        /// <returns>A single <see cref="MineLayer"/> property with a laying rate equal to the total of op1 and op2.</returns>
        public static MineLayer operator +(MineLayer op1, MineLayer op2)
        {
            if (op1.HitChance != op2.HitChance)
            {
                Report.Error("MineLayer.operator+ Attempted to add together different types of mine layers.");
                return op1;
            }
            MineLayer sum = new MineLayer(op1);
            sum.LayerRate = op1.LayerRate + op2.LayerRate;
            return sum;
        }

        /// <summary>
        /// Operator* to scale (multiply) properties in the ship design.
        /// Only laying rates scale, and this only makes sense if the mine layers produce
        /// the same mines. For Stars! like mines they are the same if HitChance is the 
        /// same.
        /// </summary>
        /// <param name="op1"><see cref="MineLayer"/> to be scaled.</param>
        /// <param name="scalar">Number of <see cref="MineLayer"/>s in the stack.</param>
        /// <returns></returns>
        public static MineLayer operator *(MineLayer op1, int scalar)
        {
            MineLayer sum = new MineLayer(op1);
            sum.LayerRate = op1.LayerRate * scalar;
            return sum;
        }

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">
        /// The node is a "Property" node with Type=="MineLayer" in a Nova 
        /// compenent definition file (xml document).
        /// </param>
        public MineLayer(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "layerrate")
                    {
                        LayerRate = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "safespeed")
                    {
                        SafeSpeed = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "hitchance")
                    {
                        HitChance = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "damageperengine")
                    {
                        DamagePerEngine = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "damageperramscoop")
                    {
                        DamagePerRamScoop = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "minfleetdamage")
                    {
                        MinFleetDamage = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (subnode.Name.ToLower() == "minramscoopdamage")
                    {
                        MinRamScoopDamage = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public override XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProperty = xmldoc.CreateElement("Property");

            // LayerRate
            XmlElement xmlelLayerRate = xmldoc.CreateElement("LayerRate");
            XmlText xmltxtLayerRate = xmldoc.CreateTextNode(this.LayerRate.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelLayerRate.AppendChild(xmltxtLayerRate);
            xmlelProperty.AppendChild(xmlelLayerRate);
            // SafeSpeed
            XmlElement xmlelSafeSpeed = xmldoc.CreateElement("SafeSpeed");
            XmlText xmltxtSafeSpeed = xmldoc.CreateTextNode(this.SafeSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelSafeSpeed.AppendChild(xmltxtSafeSpeed);
            xmlelProperty.AppendChild(xmlelSafeSpeed);
            // HitChance
            XmlElement xmlelHitChance = xmldoc.CreateElement("HitChance");
            XmlText xmltxtHitChance = xmldoc.CreateTextNode(this.HitChance.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelHitChance.AppendChild(xmltxtHitChance);
            xmlelProperty.AppendChild(xmlelHitChance);
            // DamagePerEngine
            XmlElement xmlelDamagePerEngine = xmldoc.CreateElement("DamagePerEngine");
            XmlText xmltxtDamagePerEngine = xmldoc.CreateTextNode(this.DamagePerEngine.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelDamagePerEngine.AppendChild(xmltxtDamagePerEngine);
            xmlelProperty.AppendChild(xmlelDamagePerEngine);
            // DamagePerRamScoop
            XmlElement xmlelDamagePerRamScoop = xmldoc.CreateElement("DamagePerRamScoop");
            XmlText xmltxtDamagePerRamScoop = xmldoc.CreateTextNode(this.DamagePerRamScoop.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelDamagePerRamScoop.AppendChild(xmltxtDamagePerRamScoop);
            xmlelProperty.AppendChild(xmlelDamagePerRamScoop);
            // MinFleetDamage
            XmlElement xmlelMinFleetDamage = xmldoc.CreateElement("MinFleetDamage");
            XmlText xmltxtMinFleetDamage = xmldoc.CreateTextNode(this.MinFleetDamage.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMinFleetDamage.AppendChild(xmltxtMinFleetDamage);
            xmlelProperty.AppendChild(xmlelMinFleetDamage);
            // MinRamScoopDamage
            XmlElement xmlelMinRamScoopDamage = xmldoc.CreateElement("MinRamScoopDamage");
            XmlText xmltxtMinRamScoopDamage = xmldoc.CreateTextNode(this.MinRamScoopDamage.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelMinRamScoopDamage.AppendChild(xmltxtMinRamScoopDamage);
            xmlelProperty.AppendChild(xmlelMinRamScoopDamage);

            return xmlelProperty;
        }
    }
}

