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
        
        /// <inheritdoc />
        public List<Message> Messages
        {
            get{return messages;}
        }
        
        /// <inheritdoc />
        public string Name
        {
            get
            {
                if (Mode == CargoMode.Load) {return "Load Cargo";}
                else {return "Unload Cargo";}
            }
        }
        
        /// <summary>
        /// Cargo object representing the amount to Load or Unload
        /// </summary>
        public Cargo Amount {get; set;}
        
        /// <summary>
        /// Load or Unload cargo. Mixed operations are represented by more than one Task.
        /// </summary>
        public CargoMode Mode {get; set;}
        
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CargoTask()
        {
            Amount = new Cargo();
            Mode = CargoMode.Unload;
        }
        
        
        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="other">CargoTask to copy</param>
        public CargoTask(CargoTask copy)
        {
            Amount = new Cargo(copy.Amount);
            Mode = copy.Mode;            
        }
        
        
        /// <summary>
        /// Load: Read an object of this class from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of this object</param>
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
        
        
        /// <inheritdoc />
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("CargoTask");
            Global.SaveData(xmldoc, xmlelTask, "Mode", Mode.ToString());
            xmlelTask.AppendChild(Amount.ToXml(xmldoc));
            
            return xmlelTask;
        }

        
        /// <inheritdoc />
        public bool isValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            if (fleet.InOrbit == null || target == null || !(target is Star))
            {
                Message message = new Message();            
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name +" attempted to unload cargo while not in orbit.";
                Messages.Add(message);
                return false;
            }
            
            Star star = target as Star;
            
            // Check ownership.
            if (fleet.Owner != star.Owner)
            {
                bool toReturn = false;
                
                InvadeTask invade = new InvadeTask();
                
                if (invade.isValid(fleet, target, sender, reciever))
                {
                    toReturn = invade.Perform(fleet, target, sender, reciever);
                }
                
                Messages.AddRange(invade.Messages);
                
                return toReturn;                
            }
            
            return false;           
        }
        
        
        /// <inheritdoc />
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

        
        /// <summary>
        /// Performs concrete unloading.
        /// </summary>
        private bool Unload(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = target as Star;
            
            Message message = new Message();            
            message.Text = "Fleet " + fleet.Name + " has unloaded its cargo at " + star.Name + ".";
            Messages.Add(message);

            star.ResourcesOnHand += fleet.Cargo.ToResource();
            star.Colonists += fleet.Cargo.Colonists;
            
            fleet.Cargo.Clear();
            
            return true;    
        }
        
        
        /// <summary>
        /// Performs concrete loading.
        /// </summary>
        private bool Load(Fleet fleet, Mappable target, EmpireData sender, EmpireData reciever)
        {
            Star star = target as Star;
            
            Message message = new Message();
            message.Text = "Fleet " + fleet.Name + " has loaded cargo from " + star.Name + ".";
            Messages.Add(message);            

            fleet.Cargo.Add(Amount);
            
            star.ResourcesOnHand.Ironium -= Amount.Ironium;
            star.ResourcesOnHand.Boranium -= Amount.Boranium;
            star.ResourcesOnHand.Germanium -= Amount.Germanium;
            star.Colonists -= Amount.ColonistsInKilotons * Global.ColonistsPerKiloton;
            
            return true;      
        }
    }
}
