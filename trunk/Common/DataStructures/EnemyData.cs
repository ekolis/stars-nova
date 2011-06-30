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

namespace Nova.Common
{
    using System;
    using System.Xml;
    
    /// <summary>
    /// Description of EnemyIntel.
    /// </summary>
    public class EnemyData
    {
        private int empireId;
        
        public string RaceName;
        
        public PlayerRelation Relation;
        public RaceIcon Icon;
        
        public int Id
        {
            get
            {
                return empireId;
            }
            
            set
            {               
                empireId = value;
            }
        }
        
        public EnemyData(EmpireData empire)
        {
            empireId = empire.Id;
            RaceName = empire.EmpireRace.Name;
            Icon = empire.EmpireRace.Icon;
        }
        
        public EnemyData(XmlNode node)
        {
            while (node != null)
            {
                try
                {
                    switch (node.Name.ToLower())
                    {
                        case "empire":
                            node = node.FirstChild;
                            continue;
                            
                        case "racename":
                            RaceName = node.FirstChild.Value;
                            break;
                        case "id":
                            empireId = int.Parse(node.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                        case "relation":
                            Relation = (PlayerRelation)Enum.Parse(typeof(PlayerRelation), node.FirstChild.Value);
                            break;
                        case "raceicon":
                            Icon = new RaceIcon(node);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e);
                }
                
                node = node.NextSibling;
            }
        }
                
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            // create the outer element
            XmlElement xmlelEnemy = xmldoc.CreateElement("Empire");

            Global.SaveData(xmldoc, xmlelEnemy, "Id", empireId.ToString("X"));
            
            Global.SaveData(xmldoc, xmlelEnemy, "RaceName", RaceName);
            
            Global.SaveData(xmldoc, xmlelEnemy, "Relation", Relation.ToString());
            
            if (Icon != null)
            {
                xmlelEnemy.AppendChild(Icon.ToXml(xmldoc));
            }
            
            return xmlelEnemy;
        }
    }
}
