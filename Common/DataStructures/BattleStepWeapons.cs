#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

using System;
using System.Xml;

namespace Nova.Common.DataStructures
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// A class to record weapons being fired.
    /// </summary>
    /// ----------------------------------------------------------------------------
    [Serializable]
    public class BattleStepWeapons : BattleStep
    {
        
        public double HitPower = 0;
        public string Targeting = null;
        public BattleStepTarget WeaponTarget = new BattleStepTarget();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepWeapons()
        {
            Type = "Weapons";
        }


        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepWeapons"/> XmlNode from a Nova save file (xml document). </param>
        /// ----------------------------------------------------------------------------
        public BattleStepWeapons(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "type":
                            Type = subnode.FirstChild.Value;
                            break;

                        case "hitpower":
                            HitPower = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "targeting":
                            Targeting = subnode.FirstChild.Value;
                            break;

                        case "weapontarget":
                            WeaponTarget = new BattleStepTarget(subnode);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Weapons : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the BattleStepWeapons for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the ShipDesign</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepWeapons = xmldoc.CreateElement("BattleStepWeapons");

            xmlelBattleStepWeapons.AppendChild(base.ToXml(xmldoc));

            // hitpower
            Global.SaveData(xmldoc, xmlelBattleStepWeapons, "HitPower", HitPower.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // targeting
            if (Targeting != null)
            {
                Global.SaveData(xmldoc, xmlelBattleStepWeapons, "Targeting", Targeting);
            }

            // weapontarget
            xmlelBattleStepWeapons.AppendChild(WeaponTarget.ToXml(xmldoc));

            return xmlelBattleStepWeapons;
        }


        #endregion

    }
}