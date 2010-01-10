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

namespace NovaCommon
{

   [Serializable]
   public class ProductionQueue
   {

// ============================================================================
// Details of a design in the queue.
// ============================================================================

      [Serializable]
      public class Item // FIXME - Seems like a bad name as there is already an Item type in the NovaCommon namespace
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



          /// <summary>
          /// Save: Generate an XmlElement representation of the ProductionQueue.Item for saving.
          /// </summary>
          /// <param name="xmldoc">The parent XmlDocument</param>
          /// <returns>An XmlElement representation of the ProductionQueue.Item</returns>
         public XmlElement ToXml(XmlDocument xmldoc)
         {
             XmlElement xmlelProductionOrder = xmldoc.CreateElement("ProductionOrder");

             Global.SaveData(xmldoc, xmlelProductionOrder, "Name", Name);
             Global.SaveData(xmldoc, xmlelProductionOrder, "Quantity", Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
             xmlelProductionOrder.AppendChild(BuildState.ToXml(xmldoc));

             return xmlelProductionOrder;
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
          Queue = new ArrayList(); // ensure the queue is initialised as an empty ArrayList
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

       /// <summary>
       /// Save: Generate an XmlElement representation of the ProductionQueue to save to file.
       /// </summary>
       /// <param name="xmldoc">The parent XmlDocument</param>
       /// <returns>An XmlElement representing the ProductionQueue</returns>
      public XmlElement ToXml(XmlDocument xmldoc)
      {
          XmlElement xmlelProductionQueue = xmldoc.CreateElement("ProductionQueue");
          foreach (ProductionQueue.Item item in Queue)
          {
              xmlelProductionQueue.AppendChild(item.ToXml(xmldoc));
          }
          return xmlelProductionQueue;
      }
   }
}
