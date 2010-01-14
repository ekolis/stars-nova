// ============================================================================
// Nova. (c) 2008 Ken Reed
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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{


// ============================================================================
// Design base class (characteristics common to all designs)
// A Design with a Type of "Ship" or "Starbase" is a ShipDesign type.
// (Type is inherited from Item)
// Other Designs include "Mine", "Factory" and "Defense".
// ??? Seems odd that Design inherits from Item, rather than the other way around. 
// A design is not yet an item, but an Item has all the properties of its Design. 
// Does it make sense for a Design to have a Position? It currently does because 
// it inherits one from Item. 
// TODO (priority 3) investigate refactoring this to reverse the current inheritance. - Dan 10 Jan 10
// ============================================================================

   [Serializable]
   public class Design : Item
   {
       /// <summary>
       /// default constructor
       /// </summary>
       public Design() : base() { }

       /// <summary>
       /// Load: Initialising constructor from an XmlNode representation.
       /// </summary>
       /// <param name="node">An XmlNode representing a Design</param>
       public Design(XmlNode node)
           : base(node)
       {
           // nothing to do but load the base class Item.
       }

       public override void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Design
       }

       public override void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Design");

           base.WriteXml(writer);

           writer.WriteEndElement();
       }
   }
}
