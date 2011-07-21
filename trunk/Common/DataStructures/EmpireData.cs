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
        private ushort      empireId;
        
        /// <summary>
        /// The year that corresponds to this data. Normally the current game year.
        /// </summary>
        public int          TurnYear                = Global.StartingYear;  

        /// <summary>
        /// Set to true when submit turn is selected in the client. Indicates when orders are ready for processing by the server.
        /// </summary>
        public bool         TurnSubmitted           = false;

        /// <summary>
        /// The last game year for which a turn was submitted. Should be the previous game year until the current year is submitted. May be several years previous if turns were skipped. 
        /// </summary>
        public int          LastTurnSubmitted       = 0;             

        private Race        race                    = new Race(); // This empire's race.
        
        public int          ResearchBudget          = 10; // % of resources allocated to research
        public TechLevel    ResearchLevels          = new TechLevel(); // current levels of technology
        public TechLevel    ResearchResources       = new TechLevel(); // current cumulative resources on technologies
        public TechLevel    ResearchTopics          = new TechLevel(); // order or researching
        public TechLevel    ResearchLevelsGained    = new TechLevel(); // research level increases, reset per turn
        
        public StarList OwnedStars = new StarList();
        public Dictionary<string, StarIntel> StarReports  = new Dictionary<string, StarIntel>();
        
        public FleetList OwnedFleets = new FleetList();
        public Dictionary<long, FleetIntel> FleetReports  = new Dictionary<long, FleetIntel>();
        
        public Dictionary<ushort, EmpireIntel>  EmpireReports   = new Dictionary<ushort, EmpireIntel>();
        
        public Dictionary<string, BattlePlan>   BattlePlans     = new Dictionary<string, BattlePlan>();
        
        // See associated properties.
        private long        FleetCounter             = 0;
        private long        DesignCounter            = 0;
        private long        ShipCounter              = 0;
        
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
        public ushort Id
        {
            get
            {
                return empireId;
            }
            
            set
            {
                // Empire Id should only be set on game creation, from a simple 0-127 int.
                if (value > 127)    { throw new ArgumentException("EmpireId out of range"); }                
                empireId = value;
            }
        }
        
        /// <summary>
        /// Gets the next available Fleet Key from the internal FleetCounter.
        /// </summary>
        public long GetNextFleetKey()
        {
            ++FleetCounter;
            return ((long)FleetCounter | ((long)empireId << 32));
        }

        /// <summary>
        /// Gets the next available Key for the empire.
        /// </summary>
        public long GetNextDesignKey()
        {
            ++DesignCounter;
            return ((long)DesignCounter | ((long)empireId << 32));
        }

        public long GetNextShipKey()
        {
            ++ShipCounter;
            return ((long)ShipCounter | ((long)empireId << 32));
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
        public bool IsEnemy(ushort lamb)
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
                switch (subnode.Name.ToLower())
                {
                    case "id":
                        empireId = ushort.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                        break;
                    case "fleetcounter":
                        FleetCounter = long.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "designcounter":
                        DesignCounter = long.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "shipcounter":
                        ShipCounter = long.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "turnyear":
                        TurnYear = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "turnsubmitted":
                        TurnSubmitted = bool.Parse(subnode.FirstChild.Value);
                        break;
                    case "lastturnsubmitted":
                        LastTurnSubmitted = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
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
                            StarIntel report = new StarIntel(tNode);
                            StarReports.Add(report.Name, report);
                            tNode = tNode.NextSibling;
                        }
                        break;
                    case "ownedstars":
                        tNode = subnode.FirstChild;
                        while (tNode != null)
                        {
                            Star star = new Star(tNode);
                            OwnedStars.Add(star);
                            tNode = tNode.NextSibling;
                        }
                        break;
                    case "fleetreports":
                        tNode = subnode.FirstChild;
                        while (tNode != null)
                        {
                            FleetIntel report = new FleetIntel(tNode);
                            FleetReports.Add(report.Key, report);
                            tNode = tNode.NextSibling;
                        }
                        break;
                    case "ownedfleets":
                        tNode = subnode.FirstChild;
                        while (tNode != null)
                        {
                            Fleet fleet = new Fleet(tNode);
                            OwnedFleets.Add(fleet);
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
            Global.SaveData(xmldoc, xmlelEmpireData, "ShipCounter", ShipCounter.ToString(System.Globalization.CultureInfo.InvariantCulture));

            
            Global.SaveData(xmldoc, xmlelEmpireData, "TurnYear", TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelEmpireData, "TurnSubmitted", TurnSubmitted.ToString());
            Global.SaveData(xmldoc, xmlelEmpireData, "LastTurnSubmitted", LastTurnSubmitted.ToString(System.Globalization.CultureInfo.InvariantCulture));
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
            
            XmlElement xmlelOwnedStars = xmldoc.CreateElement("OwnedStars");            
            foreach (Star star in OwnedStars.Values)
            {
                xmlelOwnedStars.AppendChild(star.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelOwnedStars);
            
            XmlElement xmlelFleetReports = xmldoc.CreateElement("FleetReports");            
            foreach (FleetIntel report in FleetReports.Values)
            {
                xmlelFleetReports.AppendChild(report.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelFleetReports);
            
            XmlElement xmlelOnedFleets = xmldoc.CreateElement("OwnedFleets");            
            foreach (Fleet fleet in OwnedFleets.Values)
            {
                xmlelOnedFleets.AppendChild(fleet.ToXml(xmldoc));    
            }
            xmlelEmpireData.AppendChild(xmlelOnedFleets);
            
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
            
            OwnedStars.Clear();
            OwnedFleets.Clear();
            
            BattlePlans.Clear();
        }
    }
}


