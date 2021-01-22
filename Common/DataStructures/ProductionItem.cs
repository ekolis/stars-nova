#region Copyright Notice
// ============================================================================
// Copyright (C) 2010, 2011 stars-nova
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
    using System.Globalization;
    using System.Xml;

    using Nova.Common.Components;

    /// <summary>
    /// Details of a design in the queue.
    /// </summary>
    [Serializable]
    public class ProductionItem 
    {
        public long Key;              // Design Key
        public string Name;           // Design Name
        public int Quantity;          // Number to build
        public Resources BuildState;  // Resources need to complete construction of current item (for tracking partial construction). Should be set to the cost of one item when first added to the queue.
        // Should be removed in favor of Unit.ResourcesNeeded * Quantity
        public bool Autobuild;
        private IProductionUnit unit;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProductionItem() 
        { 
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="quantity">The number of items to produce.</param>
        /// <param name="design">The <see cref="ShipDesign"/> to build.</param>
        public ProductionItem(int quantity, ShipDesign design)
        {
            Name = design.Name;
            Quantity = quantity;
            BuildState = design.Cost;
            Key = design.Key;
        }

        /// <summary>
        /// Return the resources needed to.
        /// </summary>
        /// <returns></returns>
        public Resources NeededResources()
        {
      Resources unitResources = this.unit.RemainingCost;
            return new Resources(
                (int)unitResources.Ironium * Quantity,
                (int)unitResources.Boranium * Quantity,
                (int)unitResources.Germanium * Quantity,
                (int)unitResources.Energy * Quantity);
        }

        #region Load Save Xml

        /// <summary>
        /// Load: Read in a ProductionQueue.Item from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionQueue.Item.</param>
        public ProductionItem(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "key":
                            Key = long.Parse(subnode.FirstChild.Value, NumberStyles.HexNumber);
                            break;
                        case "name":
                            Name = subnode.FirstChild.Value;
                            break;
                        case "quantity":
                            Quantity = Int32.Parse(subnode.FirstChild.Value, CultureInfo.InvariantCulture);
                            break;
                        case "resource":
                            BuildState = new Resources(subnode);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Generate an XmlElement representation of the ProductionQueue.Item for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the ProductionQueue.Item.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProductionOrder = xmldoc.CreateElement("ProductionOrder");

            Global.SaveData(xmldoc, xmlelProductionOrder, "Key", Key.ToString("X"));
            Global.SaveData(xmldoc, xmlelProductionOrder, "Name", Name);
            Global.SaveData(xmldoc, xmlelProductionOrder, "Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
            xmlelProductionOrder.AppendChild(BuildState.ToXml(xmldoc));

            return xmlelProductionOrder;
        }

        #endregion
    }
}