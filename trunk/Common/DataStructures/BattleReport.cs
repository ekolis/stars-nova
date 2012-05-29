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
    using System.Collections.Generic;
    using System.Xml;

    [Serializable]
    public class BattleReport
    {
        /// <summary>
        /// Main battle report components.
        /// </summary>
        public string Location  = null;
        public int SpaceSize    = 0;
        public int Year         = 0;
        public string Key 
        { 
            get 
            { 
                return (Location == null) ? "" : Year.ToString() + Location; 
            } 
        }
        public List<BattleStep> Steps = new List<BattleStep>();
        public Dictionary<long, Stack> Stacks = new Dictionary<long, Stack>();
        public Dictionary<long, int> Losses = new Dictionary<long, int>(); // empireId, lossCount
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleReport()
        {
        }

        #region Xml

        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleReport"/> XmlNode from a Nova save file (xml document).</param>
        public BattleReport(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "root":
                            subnode = subnode.FirstChild;
                            continue;
                        case "battlereport":
                            subnode = subnode.FirstChild;
                            continue;

                        case "location":
                            Location = subnode.FirstChild.Value;
                            break;

                        case "spacesize":
                            SpaceSize = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "year":
                            Year = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "battlesteptarget":
                            BattleStepTarget target = new BattleStepTarget(subnode);
                            Steps.Add(target);
                            break;

                        case "battlestepdestroy":
                            BattleStepDestroy destroy = new BattleStepDestroy(subnode);
                            Steps.Add(destroy);
                            break;

                        case "battlestepweapons":
                            BattleStepWeapons weapons = new BattleStepWeapons(subnode);
                            Steps.Add(weapons);
                            break;

                        case "battlestepmovement":
                            BattleStepMovement movement = new BattleStepMovement(subnode);
                            Steps.Add(movement);
                            break;

                        case "losses":
                            int empireId = int.Parse(subnode.SelectSingleNode("EmpireId").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            int lossCount = int.Parse(subnode.SelectSingleNode("Count").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Losses.Add(empireId, lossCount);
                            break;

                        case "fleet":
                            Stack newStack = new Stack(subnode);
                            Stacks.Add(newStack.Key, newStack);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Failed to load battle report: " + Environment.NewLine + e.Message);
                    throw e;
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Generate an XmlElement representation of the ShipDesign for saving to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the BattleReport.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelBattleReport = xmldoc.CreateElement("BattleReport");

            if (Location != null)
            {
                Global.SaveData(xmldoc, xmlelBattleReport, "Location", Location);
            }
            Global.SaveData(xmldoc, xmlelBattleReport, "SpaceSize", SpaceSize.ToString(System.Globalization.CultureInfo.InvariantCulture));

            Global.SaveData(xmldoc, xmlelBattleReport, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));

            foreach (BattleStep step in Steps)
            {
                switch (step.Type)
                
                {
                    case "Movement":
                        xmlelBattleReport.AppendChild(((BattleStepMovement)step).ToXml(xmldoc));
                        break;

                    case "Weapons":
                        xmlelBattleReport.AppendChild(((BattleStepWeapons)step).ToXml(xmldoc));
                        break;

                    case "Target":
                        xmlelBattleReport.AppendChild(((BattleStepTarget)step).ToXml(xmldoc));
                        break;

                    case "Destroy":
                        xmlelBattleReport.AppendChild(((BattleStepDestroy)step).ToXml(xmldoc));
                        break;

                    default:
                        xmlelBattleReport.AppendChild(step.ToXml(xmldoc));
                        break;
                }
            }

            // public Hashtable Stacks = new Hashtable();
            foreach (Fleet fleet in Stacks.Values)
            {
                xmlelBattleReport.AppendChild(fleet.ToXml(xmldoc));
            }

            // Losses< raceName, lossCount>
            if (Losses.Count > 0)
            {
                foreach (KeyValuePair<long, int> de in Losses)
                {
                    XmlElement xmlelLosses = xmldoc.CreateElement("Losses");
                    Global.SaveData(xmldoc, xmlelLosses, "EmpireId", de.Key);
                    Global.SaveData(xmldoc, xmlelLosses, "Count", de.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    xmlelBattleReport.AppendChild(xmlelLosses);
                }
            }
            return xmlelBattleReport;
        }

        #endregion
    }
}
