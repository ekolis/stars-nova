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

    [Serializable]
    public class BattleStep
    {
        public string Type;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleStep()
        {
        }

        #region Save Load Xml

        /// <summary>
        /// Load: initializing constructor from an XmlNode representing the BattleStep (from a save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the BattleStep.</param>
        public BattleStep(XmlNode node)
        {
            if (node == null)
            {
                Report.FatalError("BattleReport.cs: BattleStep(XmlNode node) - node is null - no Battle Step found.");
                return;
            }

            // A BattleStep should not be loaded directly but rather by calling the base constructor 
            // from one of the derived types.
            XmlNode battleStepNode = node.SelectSingleNode("BattleStep");
            if (battleStepNode == null)
            {
                Report.FatalError("BattleStep.cs: BattleStep(XmlNode node) - could not find BattleStep node, input file may be corrupt.");
                return;
            }


            XmlNode subnode = battleStepNode.FirstChild;

            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "type":
                            Type = ((XmlText)subnode.FirstChild).Value;
                            break;
                    }
                }

                catch (Exception e)
                {
                    Report.Error(e.Message + " \n Details: \n " + e.ToString());
                }

                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Return an XmlElement representation of the <see cref="BattleStep"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the BattleStep.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleStep = xmldoc.CreateElement("BattleStep");

            if (Type != null)
            {
                Global.SaveData(xmldoc, xmlelBattleStep, "Type", Type);
            }

            return xmlelBattleStep;
        }

        #endregion
    }
}