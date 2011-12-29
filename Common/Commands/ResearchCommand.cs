 #region Copyright Notice
 // ============================================================================
 // Copyright (C) 2008 Ken Reed
 // Copyright (C) 2011 The Stars-Nova Project
 //
 // This file is part of Stars-Nova.
 // See <http://sourceforge.net/projects/stars-nova/>;.
 //
 // This program is free software; you can redistribute it and/or modify
 // it under the terms of the GNU General Public License version 2 as
 // published by the Free Software Foundation.
 //
 // This program is distributed in the hope that it will be useful,
 // but WITHOUT ANY WARRANTY; without even the implied warranty of
 // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 // GNU General Public License for more details.
 //
 // You should have received a copy of the GNU General Public License
 // along with this program. If not, see <http://www.gnu.org/licenses/>;
 // ===========================================================================
 #endregion
 
namespace Nova.Common.Commands
{
    using System;
    using System.Xml;
    
    /// <summary>
    /// Command that describes the change of research state. Includes both
    /// new(if at all) budget and target.
    /// </summary>
    public class ResearchCommand : ICommand
    {
        public int Budget
        {
            get;
            set;
        }
        
        public TechLevel Topics
        {
            get;
            set;
        }
        
        public ResearchCommand()
        {
            Budget = 10;
            Topics = new TechLevel(0, 0, 1, 0, 0, 0);
        }
        
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within
        /// a Nova compenent definition file (xml document).
        /// </param>
        public ResearchCommand(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;

            while (subnode != null)
            {
                switch (subnode.Name.ToLower())
                {
                    case "budget":
                      Budget = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;

                    case "topics":
                        Topics = new TechLevel(subnode);
                    break;
                }
            
                subnode = subnode.NextSibling;
            }
        }
        
        public bool isValid(EmpireData empire)
        {
            if (Budget < 0 || Budget > 100) { return false; }
            
            return true;
        }
        
        public void ApplyToState(EmpireData empire)
        {
            empire.ResearchBudget = Budget;
            empire.ResearchTopics = Topics;
        }
        
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCom = xmldoc.CreateElement("Command");
            xmlelCom.SetAttribute("Type", "Research");
            Global.SaveData(xmldoc, xmlelCom, "Budget", Budget.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelCom.AppendChild(Topics.ToXml(xmldoc, "Topics"));            
            
            return xmlelCom;
        }
    }
}
