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
    using System.Drawing;
    using System.Xml;

    using Nova.Common.DataStructures;

    /// <summary>
    /// Base class for most game items. 
    /// </summary>
    [Serializable]
    public class Item : IComparable<Item>
    {     
        /// <summary>
        /// Backing store for the game wide unique key. 
        /// First bit is for sign. Negative values are reserved for special flags.
        /// Bits 2-24 are reserved.
        /// Bits 25-32 are for the empire.Id aka Owner.
        /// Bits 33-64 are the Item.Id, which is a number generated by the client and unique for objects owned by that empire.
        /// Bit map is:
        /// S-----------------------OOOOOOOOIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
        /// IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
        /// ^                  ^                ^            ^
        /// +-- sign bit       +-- reserved     +-- owner    +-- client generated Id
        /// </summary>
        private long key = Global.NoOwner; // Default to no-id and no owner.

        private const long idMask = 0x00000000FFFFFFFF;
        private const long ownerMask = 0x000000FF00000000;

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
        /// </summary>
        public ItemType Type = ItemType.None;

        /// <summary>
        /// Position of the Item (if any)
        /// </summary>
        public NovaPoint Position = new NovaPoint();
        
        /// <summary>
        /// Distance to an arbitrary position, used to sort Items by distance.
        /// </summary>
        public double sortableDistance;

        /// <summary>
        /// Default Construction
        /// </summary>
        public Item() 
        { 
        }

        /// <summary>
        /// Copy (initialising) constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="Item"/>.</param>
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
        
        /// <summary>
        /// Property for accessing the game wide unique key.
        /// </summary>
        public long Key
        {
            get
            {
                return key;
            }
            set
            {
                if (value < 0) { throw new ArgumentException("OwnerId out of range"); }
                key = value;
            }
        }

        /// <summary>
        /// Property for accessing the object's owning empire id, stored as bits 25-32 of the key.
        /// Range is 1 to 255, with zero being reserved for no/any empire.
        /// </summary>
        public ushort Owner
        {
            get
            {
                return (ushort) ((key & ownerMask) >> 32);
            }

            set
            {
                if (value > 0xFF || value < 0) { throw new ArgumentException("OwnerId out of range"); }
                key &= idMask;
                key |= ((long)value << 32);
            }
        }

        /// <summary>
        /// Property for accessing the objects owner specific Id (eg, for use in generating the default name). 
        /// Range is 1 - 0xFFFFFFFF, with 0 reserved for undefined Id.
        /// </summary>
        public uint Id
        {
            get
            {
                return (uint) (key & idMask);
            }

            set
            {
                if (value > idMask || value < 0) { throw new ArgumentException("ItemId out of range"); }
                key &= ownerMask;
                key |= value;
            }
        }
        
        /// <summary>
        /// Compares this Item's sortableDistance with another's.
        /// </summary>
        /// <param name="other">The Item to compare this to</param>
        public int CompareTo(Item other)
        {
            // Put stars first
            if (Type == ItemType.Star && other.Type != ItemType.Star)
                return -1;

            Item rhs = (Item)other;
            return sortableDistance.CompareTo(rhs.sortableDistance);
        }
        
        /// <summary>
        /// Load: Initialising constructor from an XmlNode representing the Item (from a save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the Item</param>
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
                        case "key":
                            key = long.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                        case "mass":
                            Mass = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "name":
                            Name = subnode.FirstChild.Value;
                            break;
                        case "type":
                            Type = (ItemType)Enum.Parse(typeof(ItemType), subnode.FirstChild.Value);
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

        /// <summary>
        /// Save: Return an XmlElement representation of the <see cref="Item"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelItem = xmldoc.CreateElement("Item");

            Global.SaveData(xmldoc, xmlelItem, "Key", Key.ToString("X"));
            
            if (Name != null)
            {
                Global.SaveData(xmldoc, xmlelItem, "Name", Name);
            }
            
            Global.SaveData(xmldoc, xmlelItem, "Type", Type.ToString());

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
    }
}
