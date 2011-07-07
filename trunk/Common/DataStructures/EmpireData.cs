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
        private int empireId;
        
        public int TurnYear = Global.StartingYear; // The year that corresponds to this data
        
        private Race race = new Race(); // This empire's race.
        
        public int          ResearchBudget          = 10; // % of resources allocated to research
        public TechLevel    ResearchLevels          = new TechLevel(); // current levels of technology
        public TechLevel    ResearchResources       = new TechLevel(); // current cumulative resources on technologies
        public TechLevel    ResearchTopics          = new TechLevel(); // order or researching
        public TechLevel    ResearchLevelsGained    = new TechLevel(); // research level increases, reset per turn
        
        public StarIntelList                    StarReports     = new StarIntelList();
        public FleetIntelList                   FleetReports    = new FleetIntelList();
        public Dictionary<int, EmpireIntel>     EmpireReports   = new Dictionary<int, EmpireIntel>();
        
        public Dictionary<string, BattlePlan>   BattlePlans     = new Dictionary<string, BattlePlan>();
        
        // See associated properties.
        private int FleetCounter             = 0;
        private int DesignCounter            = 0;
        
        public Race Race
        {
            get
            {
                return this.race;
            }
            
            set
            {
                if (value != null)
                {
                    race = value;
                }
            }
        }
        
        /// <summary>
        /// Sets or gets this empires unique integer Id.
        /// </summary>
        public int Id
        {
            get
            {
                return empireId & 0x7F000000;
            }
            
            set
            {
                // Empire Id should only be set on game creation, from a simple 0-127 int.
                if (value > 127)    { throw new ArgumentException("EmpireId out of range"); }                
                empireId = (value <<= 24);
            }
        }
        
        /// <summary>
        /// Gets the next available FleetId from the internal FleetCounter.
        /// </summary>
        public int NextFleetId
        {
            get
            {
                return (++FleetCounter | empireId);
            }
        }
        
        /// <summary>
        /// Gets the next available DesignId from the internal DesignCounter.
        /// </summary>
        public int NextDesignId
        {
            get
            {
                return (++DesignCounter | empireId);
            }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public EmpireData() 
        {
            BattlePlans.Add("Default", new BattlePlan());
        }

        /// <summary>
        /// Determine if this empire wishes to treat lamb as an enemy.
        /// </summary>
        /// <param name="lamb">The id of the empire who may be attacked.</param>
        /// <returns>true if lamb is one of this empire's enemies, otherwise false.</returns>
        public bool IsEnemy(int lamb)
        {
            return EmpireReports[lamb].Relation == PlayerRelation.Enemy;
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
                        case "id":
                            empireId = int.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                        case "fleetcounter":
                            FleetCounter = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "designcounter":
                            DesignCounter = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "turnyear":
                            TurnYear = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "race":
                            race = new Race();
                            Race.LoadRaceFromXml(subnode);
                            break;
                        case "researchbudget":
                            ResearchBudget = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
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
                                StarIntel report = new StarIntel();
                                report = report.LoadFromXml(tNode);
                                StarReports.Add(report);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        case "fleetreports":
                            tNode = subnode.FirstChild;
                            while (tNode != null)
                            {
                                FleetIntel report = new FleetIntel();
                                report = report.LoadFromXml(tNode);
                                FleetReports.Add(report);
                                tNode = tNode.NextSibling;
                            }
                            break;
                        case "otherempires":
                            tNode = subnode.FirstChild;
                            while (tNode != null)
                            {
                                EmpireIntel report = new EmpireIntel(tNode);
                                EmpireReports.Add(report.Id, report);
                                tNode = tNode.NextSibling;
                            }
                            break;
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
            
            xmlelEmpireData.AppendChild(race.ToXml(xmldoc));
            
            Global.SaveData(xmldoc, xmlelEmpireData, "Id", empireId.ToString("X"));
            
            Global.SaveData(xmldoc, xmlelEmpireData, "FleetCounter", FleetCounter.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelEmpireData, "DesignCounter", DesignCounter.ToString(System.Globalization.CultureInfo.InvariantCulture));

            
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
            
            XmlElement xmlelFleetReports = xmldoc.CreateElement("FleetReports");            
            foreach (FleetIntel report in FleetReports.Values)
            {
                xmlelFleetReports.AppendChild(report.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelFleetReports);
            
            XmlElement xmlelEnemyIntel = xmldoc.CreateElement("OtherEmpires");            
            foreach (EmpireIntel report in EmpireReports.Values)
            {
                xmlelEnemyIntel.AppendChild(report.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelEnemyIntel);
            
            foreach (string key in BattlePlans.Keys)
            {
                xmlelEmpireData.AppendChild(BattlePlans[key].ToXml(xmldoc));
            }
            
            return xmlelEmpireData;
        }
        
        public void Clear()
        {
            TurnYear = Global.StartingYear;
        
            Race = new Race();
            
            ResearchBudget = 10;
            ResearchLevels          = new TechLevel();
            ResearchResources       = new TechLevel();
            ResearchTopics          = new TechLevel();
            ResearchLevelsGained    = new TechLevel();
            
            StarReports.Clear();
            FleetReports.Clear();
            
            BattlePlans.Clear();
        }
    }
}


