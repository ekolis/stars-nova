#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
#endregion
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


#region Module Description
// ===========================================================================
// Race specific data that may change from year-to-year that must be passed to
// the Nova console.
// ===========================================================================
#endregion

namespace Nova.Common
{
    #region Using Statements
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;
    #endregion

    public enum PlayerRelation
    {
        Enemy,
        Neutral,
        Friend
    }

    [Serializable]
    public class RaceData
    {
        public int TurnYear;
        public int ResearchPercentage;
        public TechLevel ResearchLevels = new TechLevel(); // current level of technology
        public TechLevel ResearchResources = new TechLevel(); // current cumulative resources on research
        public TechLevel ResearchTopics = new TechLevel(); // order or research
        public TechLevel ResearchLevelsGained = new TechLevel(); // research level increases, reset per turn.        
        public Dictionary<string, PlayerRelation> PlayerRelations = new Dictionary<string, PlayerRelation>();
        public Dictionary<string, BattlePlan> BattlePlans = new Dictionary<string, BattlePlan>();
        

        /// <summary>
        /// default constructor
        /// </summary>
        public RaceData() 
        { 
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

        #region Load Save Xml

        /// <summary>
        /// Load: constructor to load RaceData from an XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a RaceData representation (from a save file)</param>
        public RaceData(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "turnyear":
                            TurnYear = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "research":
                            ResearchPercentage = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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
                    BattlePlans.Add("Default", new BattlePlan());

                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Generate an XmlElement representation of the RaceData
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement reprsenting the RaceData (to be written to file)</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelRaceData = xmldoc.CreateElement("RaceData");
            Global.SaveData(xmldoc, xmlelRaceData, "TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelRaceData, "Research", ResearchPercentage.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            xmlelRaceData.AppendChild(ResearchLevelsGained.ToXml(xmldoc, "ResearchLevelsGained"));
            xmlelRaceData.AppendChild(ResearchLevels.ToXml(xmldoc, "ResearchLevels"));
            xmlelRaceData.AppendChild(ResearchResources.ToXml(xmldoc, "ResearchResources"));
            xmlelRaceData.AppendChild(ResearchTopics.ToXml(xmldoc, "ResearchTopics"));
            
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
                xmlelRaceData.AppendChild(xmlelRelation);
            }
            foreach (string key in BattlePlans.Keys)
            {
                xmlelRaceData.AppendChild(BattlePlans[key].ToXml(xmldoc));
            }

            return xmlelRaceData;
        }

        #endregion
    }
}


