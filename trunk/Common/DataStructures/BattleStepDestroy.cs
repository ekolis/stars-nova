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
    /// A class to destroy a ship in a given stack.
    /// </summary>
    [Serializable]
    public class BattleStepDestroy : BattleStep
    {

        public string ShipName = null; // ship in the real fleet
        public string StackName = null; // stack in the battle engine

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStepDestroy()
        {
            Type = "Destroy";
        }

        #region Save Load Xml

        /// <summary>
        /// Load: Initialising Constructor from an xml node.
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

                        case "shipname":
                            ShipName = subnode.FirstChild.Value;
                            break;

                        case "stackname":
                            StackName = subnode.FirstChild.Value;
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
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the BattleStepDestroy</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStepDestroy = xmldoc.CreateElement("BattleStepDestroy");

            xmlelBattleStepDestroy.AppendChild(base.ToXml(xmldoc));
            Global.SaveData(xmldoc, xmlelBattleStepDestroy, "ShipName", ShipName);
            Global.SaveData(xmldoc, xmlelBattleStepDestroy, "StackName", StackName);

            return xmlelBattleStepDestroy;
        }

        #endregion
    }
}