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
    public class CargoTask : IWaypointTask
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
        public CargoMode Mode {get; set;}
        
        public CargoTask()
        {
            Amount = new Cargo();
            Mode = CargoMode.Unload;
        }
        
        /// <summary>
        /// Load: Read in a ProductionUnit from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public CargoTask(XmlNode node)
        {
            if (node == null)
            {
                return;
            }
            
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
                        case "mode":
                            Mode = (CargoMode)Enum.Parse(typeof(CargoMode), mainNode.FirstChild.Value);
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
            XmlElement xmlelTask = xmldoc.CreateElement("CargoTask");
            Global.SaveData(xmldoc, xmlelTask, "Mode", Mode.ToString());
            xmlelTask.AppendChild(Amount.ToXml(xmldoc));
            
            return xmlelTask;
        }

        public bool isValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Message message = new Message();
            Messages.Add(message);
            
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Name;
            
            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                message.Text += " attempted to unload cargo while not in orbit.";
                return false;
            }
            
            Star star = (Star)target;
            
            if (fleet.Owner != star.Owner)
            {
                bool ret = false;
                
                IWaypointTask invade = new InvadeTask();
                
                if (invade.isValid(fleet, target, sender, reciever))
                {
                    ret = invade.Perform(fleet, target, sender, reciever);
                }
                
                Messages.AddRange(invade.Messages);
                
                return ret;                
            }
            
            Messages.Clear();
            return true;           
        }
        
        public bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {            
            switch(Mode)
            {
                case CargoMode.Load:
                    return Load(fleet, target, sender, reciever);
                case CargoMode.Unload:
                    return Unload(fleet, target, sender, reciever);
            }
            
            return false;
        }

        private bool Unload(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = (Star)target;
            
            Message message = new Message();
            Messages.Add(message);
            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at " + star.Name + "";

            star.ResourcesOnHand += fleet.Cargo.ToResource();
            star.Colonists += fleet.Cargo.Colonists;
            
            fleet.Cargo.Clear();
            
            return true;    
        }
        
        private bool Load(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = (Star)target;
            Message message = new Message();
            Messages.Add(message);
            message.Text = "Fleet " + fleet.Name + " has loaded cargo from " + star.Name + "";

            fleet.Cargo.Add(Amount);
            
            star.ResourcesOnHand.Ironium -= Amount.Ironium;
            star.ResourcesOnHand.Boranium -= Amount.Boranium;
            star.ResourcesOnHand.Germanium -= Amount.Germanium;
            star.Colonists -= Amount.ColonistsInKilotons * Global.ColonistsPerKiloton;
            
            return true;      
        }
    }
}
