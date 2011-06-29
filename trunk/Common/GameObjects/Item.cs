#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// See Item summary.
// ===========================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Drawing;
    using System.Xml;

    using Nova.Common.DataStructures;

    /// <summary>
    /// Base class for most game items. 
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// This item's Id. It contains it's owner on the higher 7 bits.
        /// </summary>
        private int itemId = Global.NoOwner; // Default to no-id and no owner.
        
        /// <summary>
        /// The mass of the item (in kT).
        /// </summary>
        public int Mass;

        /// <summary>
        /// The resource cost to build (germanium, ironium, etc.).
        /// </summary>
        public Resources Cost = new Resources();

        /// <summary>
        /// The name of the derived item, for example the name of a star.
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of the derived item (e.g. "Ship", "Star", "Starbase", etc.)
        /// <remarks>
        /// FIXME (priority 5) - An enumerated type would improve data matching and avoid unneccessary errors.
        /// </remarks>
        /// </summary>
        public string Type;

        /// <summary>
        /// Position of the Item (if any)
        /// </summary>
        public NovaPoint Position = new NovaPoint();

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default Construction
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Item() 
        { 
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Copy (initialising) constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="Item"/>.</param>
        /// ----------------------------------------------------------------------------
        public Item(Item existing)
        {
            if (existing == null)
            {
                return;
            }

            this.Mass = existing.Mass;
            this.Name = existing.Name;
            this.Owner = existing.Owner;
            this.Type = existing.Type;
            this.Position = existing.Position;
            this.Cost = new Resources(existing.Cost);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Return a key for use in hash tables to locate items.
        /// </summary>
        public virtual string Key
        {
            get { return this.Owner + "/" + this.Name; }
        }
        
        /// <summary>
        /// Return this item's Id without the owner bits.
        /// </summary>
        public int PureId
        {
            get
            {
                return itemId & 0x00FFFFFF;    
            }
            
            set
            {
                if (value > 0x00FFFFFF) { throw new ArgumentException("ItemId out of range"); }
                itemId &= 0x7F000000;                
                itemId |= value;    
            }
        }
        
        /// <summary>
        /// Get or set this item's unique Id (Item.Id, Flet.Id, Ship.Id, etc).
        /// This includes the owner; use this by default.
        /// </summary>
        public int Id
        {
            get
            {
                return itemId;    
            }
            
            set
            {              
                itemId = value;    
            }
        }
        
        /// <summary>
        /// Get or set this item's owner Id. This should match the Id from the empire
        /// that owns this object.
        /// </summary>
        public int Owner
        {
            get
            {
                return itemId & 0x7F000000;
            }
            
            set
            {
                if (value < 0x01000000 && value != Global.NoOwner) { throw new ArgumentException("OwnerId out of range"); }
                itemId &= 0x00FFFFFF;
                itemId |= value;
            }
        }

        #endregion

        #region Save Load Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising constructor from an XmlNode representing the Item (from a save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the Item</param>
        /// ----------------------------------------------------------------------------
        public Item(XmlNode node)
        {
            if (node == null)
            {
                Report.FatalError("Item.cs: Item(XmlNode node) - node is null - no Item found.");
                return;
            }

            // There are two acceptable entry points to this constructor. Either node is 
            // an Item node, or it is a parent node of an Item node. The second case is allowed
            // for loading objects which inherit from Item.

            // Check if this is an Item node, or a parent of an Item
            XmlNode itemNode;
            if (node.Name.ToLower() == "item")
            {
                itemNode = node;
            }
            else
            {

                itemNode = node.SelectSingleNode("Item");
                if (itemNode == null)
                {
                    Report.FatalError("Item.cs: Item(XmlNode node) - could not find Item node, input file may be corrupt.");
                    return;
                }
            }

            XmlNode subnode = itemNode.FirstChild;

            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "itemid":
                            itemId = int.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                        case "mass":
                            Mass = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "name":
                            Name = subnode.FirstChild.Value;
                            break;
                        case "type":
                            Type = subnode.FirstChild.Value;
                            break;
                        case "position":
                            Position.X = int.Parse(subnode.SelectSingleNode("X").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Position.Y = int.Parse(subnode.SelectSingleNode("Y").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "resource":
                            Cost = new Resources(subnode);
                            break;
                    }
                }

                catch (Exception e)
                {
                    Report.Error(e.Message + " \n Details: \n " + e.ToString());
                }

                subnode = subnode.NextSibling;
            }

        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Return an XmlElement representation of the <see cref="Item"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelItem = xmldoc.CreateElement("Item");

            Global.SaveData(xmldoc, xmlelItem, "ItemId", itemId.ToString("X"));
            
            if (Name != null)
            {
                Global.SaveData(xmldoc, xmlelItem, "Name", Name);
            }
            if (Type != null)
            {
                Global.SaveData(xmldoc, xmlelItem, "Type", Type);
            }
            if (Mass != 0)
            {
                Global.SaveData(xmldoc, xmlelItem, "Mass", Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (Cost != null)
            {
                xmlelItem.AppendChild(Cost.ToXml(xmldoc));
            }

            if (Position.X != 0 || Position.Y != 0)
            {
                XmlElement xmlelPoint = xmldoc.CreateElement("Position");
                Global.SaveData(xmldoc, xmlelPoint, "X", Position.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                Global.SaveData(xmldoc, xmlelPoint, "Y", Position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelItem.AppendChild(xmlelPoint);
            }
            return xmlelItem;
        }

        #endregion

    }
}
