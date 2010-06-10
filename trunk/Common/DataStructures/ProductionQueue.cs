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
// Class for a star's production queue.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Collections;

namespace Nova.Common
{

    [Serializable]
    public class ProductionQueue
    {

        /// <summary>
        /// Details of a design in the queue.
        /// </summary>
        [Serializable]
        public class Item // FIXME (priority 5) - Seems like a bad name as there is already an Item type in the Nova.Common namespace
        {
            public string Name;           // Design name, e.g. "Space Dock"
            public int Quantity;          // Number to build
            public Resources BuildState;  // Resources need to build item // ??? (priority 6) just the next 1 or the whole lot? - Dan 10 Jan 10
            // Should be removed in favor of Unit.ResourcesNeeded * Quantity
            public bool Autobuild;
            private IProductionUnit Unit;

            /// ----------------------------------------------------------------------------
            /// <summary>
            /// default constructor
            /// </summary>
            /// ----------------------------------------------------------------------------
            public Item() { }

            /// ----------------------------------------------------------------------------
            /// <summary>
            /// Return the resources needed to 
            /// </summary>
            /// <returns></returns>
            /// ----------------------------------------------------------------------------
            public Resources NeededResources()
            {
                Resources unitResources = Unit.NeededResources();
                return new Resources((int)unitResources.Ironium * Quantity,
                                                 (int)unitResources.Boranium * Quantity,
                                                 (int)unitResources.Germanium * Quantity,
                                                 (int)unitResources.Energy * Quantity);
            }

            #region Load Save Xml

            /// ----------------------------------------------------------------------------
            /// <summary>
            /// Load: Read in a ProductionQueue.Item from and XmlNode representation.
            /// </summary>
            /// <param name="xmlnode">An XmlNode containing a representation of a ProductionQueue.Item</param>
            /// ----------------------------------------------------------------------------
            public Item(XmlNode node)
            {
                XmlNode subnode = node.FirstChild;
                while (subnode != null)
                {
                    try
                    {
                        switch (subnode.Name.ToLower())
                        {
                            case "name":
                                Name = ((XmlText)subnode.FirstChild).Value;
                                break;
                            case "quantity":
                                Quantity = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "resource":
                                BuildState = new Resources(subnode);
                                break;
                        }

                    }
                    catch
                    {
                        // ignore incomplete or unset values
                    }
                    subnode = subnode.NextSibling;
                }
            }


            /// ----------------------------------------------------------------------------
            /// <summary>
            /// Save: Generate an XmlElement representation of the ProductionQueue.Item for saving.
            /// </summary>
            /// <param name="xmldoc">The parent XmlDocument</param>
            /// <returns>An XmlElement representation of the ProductionQueue.Item</returns>
            /// ----------------------------------------------------------------------------
            public XmlElement ToXml(XmlDocument xmldoc)
            {
                XmlElement xmlelProductionOrder = xmldoc.CreateElement("ProductionOrder");

                Global.SaveData(xmldoc, xmlelProductionOrder, "Name", Name);
                Global.SaveData(xmldoc, xmlelProductionOrder, "Quantity", Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelProductionOrder.AppendChild(BuildState.ToXml(xmldoc));

                return xmlelProductionOrder;
            }

            #endregion
        }


        // ============================================================================
        // The production queue itself.
        // ============================================================================

        public ArrayList Queue = new ArrayList();

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// default constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ProductionQueue() { }

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Read in a ProductionQueue from an XmlElement representation
        /// </summary>
        /// <param name="xmlel">A ProductionQueue XmlNode, normally read from a nova data file.</param>
        /// ----------------------------------------------------------------------------
        public ProductionQueue(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "productionorder")
                    {
                        ProductionQueue.Item order = new ProductionQueue.Item(subnode);
                        if (order != null) Queue.Add(order); // TODO (priority 6) ensure they load in the correct order.
                    }

                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Generate an XmlElement representation of the ProductionQueue to save to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the ProductionQueue</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProductionQueue = xmldoc.CreateElement("ProductionQueue");
            foreach (ProductionQueue.Item item in Queue)
            {
                xmlelProductionQueue.AppendChild(item.ToXml(xmldoc));
            }
            return xmlelProductionQueue;
        }

        #endregion
    }
}
