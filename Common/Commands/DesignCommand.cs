 #region Copyright Notice
 // ============================================================================
 // Copyright (C) 2012 The Stars-Nova Project
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
    using System.Collections.Generic;
    using System.Xml;
    
    using Nova.Common.Components;
    
    /// <summary>
    /// Description of WaypointCommand.
    /// </summary>
    public class DesignCommand : ICommand
    {        
        public ShipDesign Design
        {
            private set;
            get;
        }
        
        public CommandMode Mode
        {
            private set;
            get;
        }
        
        
        // Create a blank design command.
        public DesignCommand()
        {
            Design = new ShipDesign(Global.None);
            Mode = CommandMode.Add;
        }
        
        /// <summary>
        /// Creaets a design command with a design key. Useful to delete designs without
        /// bloating the orders file when all that is needed is the numeric Key instead of
        /// the full design.
        /// </summary>
        public DesignCommand(CommandMode mode, long designKey)
        {
            Design = new ShipDesign(designKey);
            Mode = mode;
        }
        

        /// <summary>
        /// Creates a design command by providing a full design object. Use when adding or
        /// modifying designs.
        /// </summary>
        public DesignCommand(CommandMode mode, ShipDesign design)
        {
            Design = design;
            Mode = mode;
        }
                
        
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within
        /// a Nova compenent definition file (xml document).
        /// </param>
        public DesignCommand(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            
            while (mainNode != null)
            {
                switch (mainNode.Name.ToLower())
                {
                    case "mode":
                        Mode = (CommandMode)Enum.Parse(typeof(CommandMode), mainNode.FirstChild.Value);
                    break;                   
                    
                    case "design":
                        Design = new ShipDesign(mainNode);
                    break;
                    
                    case "shipdesign":
                        Design = new ShipDesign(mainNode);
                    break;
                }
            
                mainNode = mainNode.NextSibling;
            }   
        }
        
        
        
        public bool isValid(EmpireData empire)
        {           
            switch(Mode)
            {
                case CommandMode.Add:
                    if (empire.Designs.ContainsKey(Design.Key))
                    {
                        // Cant re-add same design.
                        return false;
                    }
                break;
                case CommandMode.Delete: // Botch cases check for existing design before editing/deleting.
                case CommandMode.Edit:
                    if (!empire.Designs.ContainsKey(Design.Key))
                    {
                        return false;
                    }
                break;
            }
            
            return true;
        }
        
        
        
        public void ApplyToState(EmpireData empire)
        {
            switch(Mode)
            {
                case CommandMode.Add:
                    empire.Designs.Add(Design.Key, Design);
                break;
                case CommandMode.Delete:
                    empire.Designs.Remove(Design.Key);                
                    UpdateFleetCompositions(empire);
                break;
                case CommandMode.Edit:
                    empire.Designs.Remove(Design.Key);
                    UpdateFleetCompositions(empire);
                    empire.Designs.Add(Design.Key, Design);
                break;
            }
            
        }
        
        
        /// <summary>
        /// Handle destroying ships of the deleted/edited design.
        /// </summary>
        private void UpdateFleetCompositions(EmpireData empire)
        {
            // Note that we are not allowed to delete the ships or fleets on the
            // iteration as that is not allowed (it
            // destroys the validity of the iterator). Consequently we identify
            // anything that needs deleting and remove them separately from their
            // identification.
            List<Fleet> fleetsToRemove = new List<Fleet>();
            
            foreach (Fleet fleet in empire.OwnedFleets.Values)
            {
                List<ShipToken> tokensToRemove = new List<ShipToken>();
    
                foreach (ShipToken token in fleet.Composition.Values)
                {
                    if (token.Design.Key == Design.Key)
                    {
                        tokensToRemove.Add(token);
                    }
                }
    
                foreach (ShipToken token in tokensToRemove)
                {
                    fleet.Composition.Remove(token.Design.Key);
                }
    
                if (fleet.Composition.Count == 0)
                {
                    fleetsToRemove.Add(fleet);
                }
            }
    
            foreach (Fleet fleet in fleetsToRemove)
            {
                empire.OwnedFleets.Remove(fleet.Key);
                empire.FleetReports.Remove(fleet.Key);
            }
            
        }
        
        
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCom = xmldoc.CreateElement("Command");
            xmlelCom.SetAttribute("Type", "Design");
            Global.SaveData(xmldoc, xmlelCom, "Mode", Mode.ToString());
            xmlelCom.AppendChild(Design.ToXml(xmldoc));
            
            return xmlelCom;    
        }
    }
}
