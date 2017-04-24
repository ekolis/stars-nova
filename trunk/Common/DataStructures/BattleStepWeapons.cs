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

namespace Nova.Common.DataStructures
{
    #region Using Statements
    using System;
    using System.Xml;
    #endregion

    /// <summary>
    /// A class to record weapons being fired.
    /// </summary>
    [Serializable]
    public class BattleStepWeapons : BattleStep
    {
        public enum TokenDefence
        {
            Shields = 0, Armor = 1
        }
        public double Damage = 0;
        public TokenDefence Targeting = TokenDefence.Shields;
        public BattleStepTarget WeaponTarget = new BattleStepTarget();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepWeapons()
        {
            Type = "Weapons";
        }

        #region Load Save Xml

        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepWeapons"/> XmlNode from a Nova save file (xml document). </param>
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

                        case "damage":
                            Damage = double.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "targeting":
                            if (subnode.FirstChild.Value.ToLower() == "shields")
                            {
                                Targeting = BattleStepWeapons.TokenDefence.Shields;
                            }
                            else if (subnode.FirstChild.Value.ToLower() == "armor")
                            {
                                Targeting = BattleStepWeapons.TokenDefence.Armor;
                            }
                            else
                            {
                                Report.Error("Unable to read Target type \"" + subnode.FirstChild.Value + "\" in BattleStepWeapons.cs LoadXml()");
                                Targeting = BattleStepWeapons.TokenDefence.Armor; // take a guess so we can continue
                            }
                            break;

                        case "battlesteptarget":
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

        /// <summary>
        /// Generate an XmlElement representation of the BattleStepWeapons for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the ShipDesign.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepWeapons = xmldoc.CreateElement("BattleStepWeapons");

            xmlelBattleStepWeapons.AppendChild(base.ToXml(xmldoc));

            // hitpower
            Global.SaveData(xmldoc, xmlelBattleStepWeapons, "Damage", Damage.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // targeting
            if (Targeting == TokenDefence.Shields)
            {
                Global.SaveData(xmldoc, xmlelBattleStepWeapons, "Targeting", "Shields");
            }
            else
            {
                Global.SaveData(xmldoc, xmlelBattleStepWeapons, "Targeting", "Armor"); // only other option
            }

            // weapontarget
            xmlelBattleStepWeapons.AppendChild(WeaponTarget.ToXml(xmldoc));

            return xmlelBattleStepWeapons;
        }

        #endregion
    }
}