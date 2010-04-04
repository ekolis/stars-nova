// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Design base class (characteristics common to all designs)
// A Design with a Type of "Ship" or "Starbase" is a ShipDesign type.
// (Type is inherited from Item)
// Other Designs include "Mine", "Factory" and "Defense".
//
// TODO (priority 4) refactor this to reverse the current inheritance. 
// A design is not yet an item, but an Item has all the properties of its Design. 
// Does it make sense for a Design to have a Position? It currently does because 
// it inherits one from Item. 
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

namespace NovaCommon
{
    /// <summary>
    /// A design.
    /// </summary>
    [Serializable]
    public class Design : Item
    {

        #region Construction

        /// <summary>
        /// default constructor
        /// </summary>
        public Design() : base() { }

        #endregion

        #region Load Save Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within a Nova xml file (xml document).
        /// </param>
        public Design(XmlNode node)
            : base(node)
        {
            // nothing to do but load the base class Item.
        }

        /// <summary>
        /// Save: Generate an XmlElement representing the Design.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the Design</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelDesign = xmldoc.CreateElement("Design");

            xmlelDesign.AppendChild(base.ToXml(xmldoc));

            return xmlelDesign;
        }

        #endregion

    }
}
