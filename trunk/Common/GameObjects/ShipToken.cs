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
    /// Describes a Token of Quantity ships, all of the same design. A Token forms part of a Fleet (or in battle, a Stack). Damage is tracked at the Token level. 
    /// </summary>
    [Serializable]
    public class ShipToken
    {
        /// <summary>
        /// Gets or sets the design of this token.
        /// </summary>
        public ShipDesign Design
        {
            set;
            get;
        }
        
        /// <summary>
        /// Tokens are Keyed by Design.Key, so return that.
        /// </summary>
        public long Key
        {
            get {return Design.Key;}
        }
        
        /// <summary>
        /// Gets or sets the amount of ships of the same design
        /// in this token.
        /// </summary>
        public int Quantity
        {
            get;
            set;
        }
        
        /// <summary>
        /// Returns the percentage of damage done to the token of ships.
        /// </summary>
        public double Damage
        {
            get 
            { 
                return 100 * (this.Design.Armor - this.Armor) / this.Design.Armor; 
            }
        }
        
        /// <summary>
        /// The current total shield strength of all ships in the ShipToken. 
        /// </summary>
        /// <remarks>Placed here for consistency with Armor</remarks>
        public double Shields
        {
            get;
            set;
        }
        
        /// <summary>
        /// Sets or Gets the amount of armor the Token has remaining.
        /// This is the total number of Armor points remaining for the whole Token (of Quantity ships), including Hull. Each ship has Armor / Quantity.
        /// Note: Stars! stores armor damage as a percentage (with an 8 bit number) which causes some wierd rounding errors we do not want to replicate.
        /// </summary>
        public double Armor
        {
            get;
            set;
        }
        
        /// <summary>
        /// Creates a Token of ships
        /// </summary>
        /// <param name="design">The ship design of this token</param>
        /// <param name="quantity">The amount of ships in this token</param>
        /// <param name="armor">The amount of armor remaining in this token</param>
        public ShipToken(ShipDesign design, int quantity, double armor) :
            this(design, quantity)
        {
            Armor = armor;
        }
        
        /// <summary>
        /// Creates a token of ships
        /// </summary>
        /// <param name="newDesign">The ship design of this token</param>
        /// <param name="quantity">The amount of ships in this token</param>
        public ShipToken(ShipDesign newDesign, int quantity)
        {
            Design = newDesign;
            Quantity = quantity;
            Armor = newDesign.Armor * quantity;
            Shields = newDesign.Shield;
        }
        
        
        /// <summary>
        /// Load from XML: initializing constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within
        /// a Nova component definition file (xml document).
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
                        Armor = double.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                    break;                    
                }
            
                mainNode = mainNode.NextSibling;
            }   
        }
        
        /// <summary>
        /// Save: Serialize this property to an <see cref="XmlElement"/>.
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
