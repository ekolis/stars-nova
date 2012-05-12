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
    
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class ProductionCommand : ICommand
    {
        public ProductionOrder ProductionOrder
        {
            private get;
            set;
        }
        
        public CommandMode Mode
        {
            private get;
            set;
        }
        
        public int Index
        {
            private set;
            get;
        }
        
                
        public string StarKey
        {
            private set;
            get;
        }
        
        
        public ProductionCommand(CommandMode mode, ProductionOrder productionOrder, string starKey, int index = 0)
        {
            Mode = mode;
            ProductionOrder = productionOrder;
            StarKey = starKey;
            Index = index;            
        }
        
        
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within
        /// a Nova compenent definition file (xml document).
        /// </param>
        public ProductionCommand(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            
            while (mainNode != null)
            {
                switch (mainNode.Name.ToLower())
                {
                    case "mode":
                        Mode = (CommandMode)Enum.Parse(typeof(CommandMode), mainNode.FirstChild.Value);
                    break;                   
                    
                    case "productionorder":
                        ProductionOrder = new ProductionOrder(mainNode);
                    break;
                    
                    case "index":
                        Index = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;
                    
                    case "starkey":
                        StarKey = mainNode.FirstChild.Value;
                    break;
                }
            
                mainNode = mainNode.NextSibling;
            }   
        }
        
        
        public bool isValid(EmpireData empire)
        {
            switch (Mode)
            {
                case CommandMode.Add:
                    // When adding something to the production, it's cost can't be lower than
                    // it's actual cost.
                    if (ProductionOrder.Unit is ShipProductionUnit)
                    {
                        if (!(ProductionOrder.Unit.Cost >= empire.Designs[(ProductionOrder.Unit as ShipProductionUnit).DesignKey].Cost)) {return false;}
                    }                    
                    if (ProductionOrder.Unit is FactoryProductionUnit)
                    {
                        if (!(ProductionOrder.Unit.Cost >= empire.Race.GetFactoryResources())) { return false; }
                    }                    
                    if (ProductionOrder.Unit is MineProductionUnit)
                    {
                        if (!(ProductionOrder.Unit.Cost >= empire.Race.GetMineResources())) { return false; }
                    }                    
                    // Don't add cheated pre-built units.
                    if (!(ProductionOrder.Unit.Cost == ProductionOrder.Unit.RemainingCost)) { return false; }
                    break;
                
                case CommandMode.Edit:
                    // Check the order actually exists.

                    // FIXME (priority 5) - this causes a false positive when edditing production orders
                    // if (!empire.OwnedStars[StarKey].ManufacturingQueue.Queue.Contains(ProductionOrder)) {return false;}
                    
                    // Don't allow modification of the total cost.                    
                    if (!(ProductionOrder.Unit.RemainingCost >= empire.OwnedStars[StarKey].ManufacturingQueue.Queue[Index].Unit.RemainingCost))
                    {
                        return false;    
                    }                    
                    // Don't allow modification of the remaining cost.  
                    if (!(ProductionOrder.Unit.Cost >= empire.OwnedStars[StarKey].ManufacturingQueue.Queue[Index].Unit.Cost))
                    {
                        return false;    
                    }
                     
                    break;
                
                case CommandMode.Delete:
                    // Check the order actually exists.                    
                    if (!empire.OwnedStars[StarKey].ManufacturingQueue.Queue.Contains(ProductionOrder)) {return false;}
                    break;
            }
            
            return true;
        }
        
        
        public void ApplyToState(EmpireData empire)
        {
            switch (Mode)
            {
                case CommandMode.Add:
                    empire.OwnedStars[StarKey].ManufacturingQueue.Queue.Insert(Index, ProductionOrder);
                    break;
                case CommandMode.Edit:
                    empire.OwnedStars[StarKey].ManufacturingQueue.Queue.RemoveAt(Index);
                    empire.OwnedStars[StarKey].ManufacturingQueue.Queue.Insert(Index, ProductionOrder);
                    break;
                case CommandMode.Delete:
                    empire.OwnedStars[StarKey].ManufacturingQueue.Queue.RemoveAt(Index);
                    break;
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
            xmlelCom.SetAttribute("Type", "Production");
            Global.SaveData(xmldoc, xmlelCom, "Mode", Mode.ToString());
            Global.SaveData(xmldoc, xmlelCom, "StarKey", StarKey);
            Global.SaveData(xmldoc, xmlelCom, "Index", Index.ToString(System.Globalization.CultureInfo.InvariantCulture));            
            if (ProductionOrder != null)
            {
                xmlelCom.AppendChild(ProductionOrder.ToXml(xmldoc));
            }
            return xmlelCom; 
        }
    }
}
