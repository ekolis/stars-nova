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

namespace Nova.Common.Waypoints
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    
    using Nova.Common;
    
    /// <summary>
    /// Performs Star Colonisation.
    /// </summary>
    public class LoadTask : IWaypointTask
    {   
        private List<Message> messages = new List<Message>();
        
        public List<Message> Messages
        {
            get{ return messages;}
        }
        
        public string Name
        {
            get{return "Load Cargo";}
        }
                
        public Cargo Amount {get; set;}
        
        public LoadTask()
        {
             
        }
        
        /// <summary>
        /// Load: Read in a ProductionUnit from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public LoadTask(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {                            
                        case "cargo":
                            Amount = new Cargo(mainNode);
                            break;                            
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                mainNode = mainNode.NextSibling;
            }
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("LoadCargoTask");
            xmlelTask.AppendChild(Amount.ToXml(xmldoc));
            
            return xmlelTask;
        }
    }
}
