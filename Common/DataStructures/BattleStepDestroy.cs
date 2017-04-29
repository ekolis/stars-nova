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
    /// A class to destroy a ship in a given stack.
    /// </summary>
    [Serializable]
    public class BattleStepDestroy : BattleStep
    {
        public long StackKey 
        {
            get; 
            set;
        }
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepDestroy()
        {
            Type = "Destroy";
        }

        /// <summary>
        /// Load: initializing Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleStepDestroy"/> XmlNode from a Nova save file (xml document). </param>
        public BattleStepDestroy(XmlNode node)
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
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Step - Destroy : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }         
        }

        /// <summary>
        /// Generate an XmlElement representation of the BattleStepDestroy for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the BattleStepDestroy.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepDestroy = xmldoc.CreateElement("BattleStepDestroy");

            xmlelBattleStepDestroy.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepDestroy, "StackKey", StackKey.ToString("X"));

            return xmlelBattleStepDestroy;
        }
    }
}