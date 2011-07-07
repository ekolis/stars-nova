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

namespace Nova.Common
{
    #region Using Statements
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;
    #endregion

    [Serializable]
    public class ProductionQueue
    {
        /// <summary>
        /// The production queue itself.
        /// </summary>
        public List<ProductionItem> Queue = new List<ProductionItem>();

        /// <summary>
        /// default constructor
        /// </summary>
        public ProductionQueue() 
        { 
        }

        #region Load Save Xml

        /// <summary>
        /// Read in a ProductionQueue from an XmlElement representation
        /// </summary>
        /// <param name="node">A ProductionQueue XmlNode, normally read from a nova data file.</param>
        public ProductionQueue(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    if (subnode.Name.ToLower() == "productionorder")
                    {
                        ProductionItem order = new ProductionItem(subnode);
                        if (order != null)
                        {
                            Queue.Add(order); // TODO (priority 6) ensure they load in the correct order.
                        }
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
        /// Save: Generate an XmlElement representation of the ProductionQueue to save to file.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the ProductionQueue</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProductionQueue = xmldoc.CreateElement("ProductionQueue");
            foreach (ProductionItem item in Queue)
            {
                xmlelProductionQueue.AppendChild(item.ToXml(xmldoc));
            }
            return xmlelProductionQueue;
        }

        #endregion
    }
}
