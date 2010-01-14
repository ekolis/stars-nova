// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Class for a star's production queue.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{

   [Serializable]
   public class ProductionQueue : IXmlSerializable
   {

// ============================================================================
// Details of a design in the queue.
// ============================================================================

      [Serializable]
      public class Item : IXmlSerializable // FIXME - Seems like a bad name as there is already an Item type in the NovaCommon namespace
      {
         public string    Name;              // Design name, e.g. "Space Dock"
         public int       Quantity;          // Number to build
         public Resources BuildState;        // Resources need to build item // ??? just the next 1 or the whole lot? - Dan 10 Jan 10

          /// <summary>
          /// default constructor
          /// </summary>
         public Item() { }

         /// <summary>
         /// Load: Read in a ProductionQueue.Item from and XmlNode representation.
         /// </summary>
         /// <param name="xmlnode">An XmlNode containing a representation of a ProductionQueue.Item</param>
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

          public XmlSchema GetSchema()
          {
              return null;
          }

          public void ReadXml(XmlReader reader)
          {
              throw new NotImplementedException(); // TODO XML deserialization of ProductionQueue.Item
          }

          public void WriteXml(XmlWriter writer)
          {
              writer.WriteStartElement("ProductionOrder");

              writer.WriteElementString("Name", Name);
              writer.WriteElementString("Quantity", Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
              BuildState.WriteXml(writer);

              writer.WriteEndElement();
          }
      }      


// ============================================================================
// The production queue itself.
// ============================================================================

      public ArrayList Queue = new ArrayList();

       /// <summary>
       /// default constructor
       /// </summary>
      public ProductionQueue() { }

       /// <summary>
       /// Read in a ProductionQueue from an XmlElement representation
       /// </summary>
       /// <param name="xmlel">A ProductionQueue XmlNode, normally read from a nova data file.</param>
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
                      if (order != null) Queue.Add(order); // TODO (priority 4) ensure they load in the correct order.
                  }
                 
              }
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
          }
      }

       public XmlSchema GetSchema()
       {
           return null;
       }

       public void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of ProductionQueue
       }

       public void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("ProductionQueue");
           foreach (Item item in Queue)
           {
               item.WriteXml(writer);
           }
           writer.WriteEndElement();
       }
   }
}
