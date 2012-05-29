#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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
    using System;
    using System.Xml;

    /// <summary>
    /// A class to record a new target.
    /// </summary>
    [Serializable]
    public class BattleStepTarget : BattleStep
    {
        public long StackKey {get; set;}
        public long TargetKey {get; set;}

        public BattleStepTarget()
        {
            Type = "Target";
        }

        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepTarget"/> XmlNode from a Nova save file (xml document). </param>
        public BattleStepTarget(XmlNode node)
            : base(node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "stackkey":
                            StackKey = long.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                        case "targetkey":
                            TargetKey = long.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Target : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }

        /// <summary>
        /// Generate an XmlElement representation of the BattleStepTarget for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the BattleStepTarget.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepTarget = xmldoc.CreateElement("BattleStepTarget");

            xmlelBattleStepTarget.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepTarget, "StackKey", StackKey.ToString("X"));
            Global.SaveData(xmldoc, xmlelBattleStepTarget, "TargetKey", TargetKey.ToString("X"));

            return xmlelBattleStepTarget;
        }
    }
}