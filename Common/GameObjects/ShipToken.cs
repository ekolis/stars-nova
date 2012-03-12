#region Copyright Notice
// ============================================================================
// Copyright (C) 2012 The Stars-Nova Project
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
    using System.Collections.Generic;
    using System.Xml;
    
    using Nova.Common.Components;
    

    /// <summary>
    /// Describes a Token of ships of the same design.
    /// </summary>
    [Serializable]
    public class ShipToken
    {
        public ShipDesign Design
        {
            set;
            get;
        }
        
        public int Quantity
        {
            get;
            set;
        }
        
        public double Damage
        {
            get { return (100 * Armor / Design.Armor); }
        }
        
        public int Shields
        {
            get;
            set;
        }
        
        public int Armor
        {
            get;
            set;
        }
        
        public ShipToken(ShipDesign design, int quantity, int armor) :
            this(design, quantity)
        {
            Armor = armor;
        }
        
        public ShipToken(ShipDesign design, int quantity)
        {
            Design = design;
            Quantity = quantity;
            Armor = Design.Armor;
            Shields = Design.Shield;
        }
        
        
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within
        /// a Nova compenent definition file (xml document).
        /// </param>
        public ShipToken(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            
            while (mainNode != null)
            {
                switch (mainNode.Name.ToLower())
                {
                    case "design":
                        Design = new ShipDesign(long.Parse(mainNode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber));
                    break;                   
                    
                    case "quantity":
                        Quantity = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;
                    
                    case "armor":
                        Armor = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;                    
                }
            
                mainNode = mainNode.NextSibling;
            }   
        }
        
        
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCom = xmldoc.CreateElement("Token");

            Global.SaveData(xmldoc, xmlelCom, "Design", Design.Key.ToString("X"));
            Global.SaveData(xmldoc, xmlelCom, "Quantity", Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelCom, "Armor", Armor.ToString(System.Globalization.CultureInfo.InvariantCulture));            

            return xmlelCom; 
        }
    }
}
