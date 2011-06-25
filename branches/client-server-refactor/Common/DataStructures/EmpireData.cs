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
// ============================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    public enum PlayerRelation
    {
        Enemy,
        Neutral,
        Friend
    }
 
    /// <summary>
    /// Race specific data that may change from year-to-year that must be passed to
    /// the Nova console. 
    /// </summary>
    [Serializable]
    public class EmpireData
    {
        public int TurnYear = Global.StartingYear; // The year that corresponds to this data
        
        public Race EmpireRace = new Race(); // This empire's race.
        
        public int          ResearchBudget          = 10; // % of resources allocated to research
        public TechLevel    ResearchLevels          = new TechLevel(); // current levels of technology
        public TechLevel    ResearchResources       = new TechLevel(); // current cumulative resources on technologies
        public TechLevel    ResearchTopics          = new TechLevel(); // order or researching
        public TechLevel    ResearchLevelsGained    = new TechLevel(); // research level increases, reset per turn.
        
        public StarIntelList                        StarReports     = new StarIntelList();
        public Dictionary<string, FleetIntel>       FleetReports    = new Dictionary<string, FleetIntel>();
        
        public Dictionary<string, PlayerRelation>   PlayerRelations = new Dictionary<string, PlayerRelation>();
        public Dictionary<string, BattlePlan>       BattlePlans     = new Dictionary<string, BattlePlan>();
        

        /// <summary>
        /// default constructor
        /// </summary>
        public EmpireData() 
        {
            // If no orders have ever been turned in then ensure battle plans contain at least the default
            if (BattlePlans.Count == 0)
            {
                BattlePlans.Add("Default", new BattlePlan());
            }
        }

        /// <summary>
        /// Determine if this race wishes to treat lamb as an enemy.
        /// </summary>
        /// <param name="lamb">The name of the race who may be attacked.</param>
        /// <returns>true if lamb is one of this race's enemies, otherwise false.</returns>
        public bool IsEnemy(string lamb)
        {
            return PlayerRelations[lamb] == PlayerRelation.Enemy;
        }

        /// <summary>
        /// Load: constructor to load EmpireData from an XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a EmpireData representation (from a save file)</param>
        public EmpireData(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            XmlNode tNode;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "turnyear":
                            TurnYear = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "race":
                            EmpireRace = new Race();
                            EmpireRace.LoadRaceFromXml(subnode);
                            break;
                        case "researchbudget":
                            ResearchBudget = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "researchlevelsgained":
                            ResearchLevelsGained = new TechLevel(subnode);
                            break;
                        case "researchlevels":
                            ResearchLevels = new TechLevel(subnode);
                            break;
                        case "researchresources":
                            ResearchResources = new TechLevel(subnode);
                            break;
                        case "researchtopics":
                            ResearchTopics = new TechLevel(subnode);
                            break;
                        case "starreports":
                            tNode = subnode.FirstChild;
                            while (tNode != null)
                            {
                                StarIntel report = new StarIntel(tNode);
                                StarReports.Add(report);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        case "relation":
                            {
                                string key = ((XmlText)subnode.SelectSingleNode("Race").FirstChild).Value;
                                string value = ((XmlText)subnode.SelectSingleNode("Status").FirstChild).Value;
                                PlayerRelation rel = value.Equals("Enemy", StringComparison.InvariantCultureIgnoreCase) ? PlayerRelation.Enemy :
                                                     value.Equals("Friend", StringComparison.InvariantCultureIgnoreCase) ? PlayerRelation.Friend :
                                                     PlayerRelation.Neutral;
                                PlayerRelations.Add(key, rel);
                                break;
                            }
                        case "battleplan":
                            BattlePlan plan = new BattlePlan(subnode);
                            BattlePlans[plan.Name] = plan;
                            break;
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }

                // If no orders have ever been turned in then ensure battle plans contain at least the default
                if (BattlePlans.Count == 0)
                {
                    BattlePlans.Add("Default", new BattlePlan());
                }

                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Generate an XmlElement representation of the EmpireData
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement reprsenting the EmpireData (to be written to file)</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelEmpireData = xmldoc.CreateElement("EmpireData");
            
            xmlelEmpireData.AppendChild(EmpireRace.ToXml(xmldoc));
            
            Global.SaveData(xmldoc, xmlelEmpireData, "TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelEmpireData, "ResearchBudget", ResearchBudget.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            xmlelEmpireData.AppendChild(ResearchLevelsGained.ToXml(xmldoc, "ResearchLevelsGained"));
            xmlelEmpireData.AppendChild(ResearchLevels.ToXml(xmldoc, "ResearchLevels"));
            xmlelEmpireData.AppendChild(ResearchResources.ToXml(xmldoc, "ResearchResources"));
            xmlelEmpireData.AppendChild(ResearchTopics.ToXml(xmldoc, "ResearchTopics"));
            
            XmlElement xmlelStarReports = xmldoc.CreateElement("StarReports");            
            foreach (StarIntel report in StarReports.Values)
            {
                xmlelStarReports.AppendChild(report.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelStarReports);
            
            foreach (string key in PlayerRelations.Keys)
            {
                XmlElement xmlelRelation = xmldoc.CreateElement("Relation");
                Global.SaveData(xmldoc, xmlelRelation, "Race", key);
                string rel;
                switch( PlayerRelations[key])
                {
                    case PlayerRelation.Enemy: rel = "Enemy"; break;
                    case PlayerRelation.Friend: rel = "Friend"; break;
                    default: rel = "Neutral"; break;
                }
                Global.SaveData(xmldoc, xmlelRelation, "Status", rel);
                xmlelEmpireData.AppendChild(xmlelRelation);
            }
            foreach (string key in BattlePlans.Keys)
            {
                xmlelEmpireData.AppendChild(BattlePlans[key].ToXml(xmldoc));
            }

            return xmlelEmpireData;
        }
        
        public void Clear()
        {
            TurnYear = Global.StartingYear;
        
            EmpireRace = new Race();
            
            ResearchBudget = 10;
            ResearchLevels          = new TechLevel();
            ResearchResources       = new TechLevel();
            ResearchTopics          = new TechLevel();
            ResearchLevelsGained    = new TechLevel();
            
            StarReports.Clear();
            FleetReports.Clear();
            
            PlayerRelations.Clear();
            BattlePlans.Clear();
        }
    }
}


