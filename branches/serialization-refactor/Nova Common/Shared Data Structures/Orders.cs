// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module contains the data that is generated by the Nova GUI and passed
// to the Nova Console so that it can generate the turn for the next year. These
// are the orders sent to the console/server.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System;
using System.IO;
using System.IO.Compression;
using System.Xml;


// ============================================================================
// Definition of the orders that are created by the Nova GUI and read by the
// Nova Console.
// ============================================================================

namespace NovaCommon
{
   [Serializable]
   public sealed class Orders
   {
      public Hashtable   RaceFleets      = new Hashtable(); // For fleet orders
      public Hashtable   RaceDesigns     = new Hashtable(); // For any new designs
      public ArrayList   RaceStars       = new ArrayList(); // For production queues
      public ArrayList   DeletedFleets   = new ArrayList(); // To delete fleets
      public ArrayList   DeletedDesigns  = new ArrayList(); // To delete designs
      public RaceData    PlayerData      = new RaceData();  // Player relations, battle orders & turn # (turn # so we can check these orders are for the right year.)
      public int         TechLevel       = 0;               // FIXME (priority 4): should send our research orders; server should control actual player tech level ??? what does this int mean? it is not a TechLevel type.

       // private data
       private static BinaryFormatter Formatter = new BinaryFormatter();

       /// <summary>
       /// default constructor
       /// </summary>
       public Orders() { }

       public Orders(XmlNode xmlnode)
       {
           // temporary variables for reading in designs, fleets, stars
           Design design = null; 
           ShipDesign shipDesign = null;
           Fleet fleet = null;
           Star star = null;

           // Read the node
           while (xmlnode != null)
           {
               try
               {
                   switch (xmlnode.Name.ToLower())
                   {
                       case "root": xmlnode = xmlnode.FirstChild; continue;
                       case "orders": xmlnode = xmlnode.FirstChild; continue;
                       case "techlevel": TechLevel = int.Parse(((XmlText)xmlnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); break;

                           // When loading designs we need to know what type of design it is.
                           // To do this we first look ahead at the Type field of the design,
                           // then load a design of the appropriate type (currently Design or ShipDesign).
                       case "design":
                           {
                               string type = xmlnode.FirstChild.SelectSingleNode("Type").FirstChild.Value;
                               if (type.ToLower() == "ship" || type == "starbase")
                               {
                                   shipDesign = new ShipDesign(xmlnode);
                                   RaceDesigns.Add(shipDesign.Name, shipDesign);
                               }
                               else
                               {
                                   design = new Design(xmlnode);
                                   RaceDesigns.Add(design.Name, design);
                               }
                           }
                           break;

                           // Deleted designs are in their own section to seperate them from 
                           // current designs. We load this section in one loop. The comments
                           // above for designs apply equally to deleted designs.
                       case "deleteddesigns":
                           XmlNode deletedDesignsNode = xmlnode.FirstChild;
                           while (deletedDesignsNode != null)
                           {
                               string type = deletedDesignsNode.FirstChild.SelectSingleNode("Type").FirstChild.Value;
                               if (type.ToLower() == "ship" || type == "starbase")
                               {
                                   shipDesign = new ShipDesign(deletedDesignsNode);
                                   DeletedDesigns.Add(shipDesign);
                               }
                               else
                               {
                                   design = new Design(deletedDesignsNode);
                                   DeletedDesigns.Add(design);
                               }
                               deletedDesignsNode = deletedDesignsNode.NextSibling;
                           }
                           break;

                       case "star":
                           star = new Star(xmlnode);
                           RaceStars.Add(star);
                           break;

                       case "fleet":
                           fleet = new Fleet(xmlnode);
                           RaceFleets.Add(fleet.FleetID, fleet);
                           break;

                           // Deleted fleets are contained in their own section to seperate them from
                           // current fleets. We load this section in this loop.
                       case "deletedfleets":
                           XmlNode deletedFleetsNode = xmlnode.FirstChild;
                           while (deletedFleetsNode != null)
                           {
                               fleet = new Fleet(deletedFleetsNode);
                               DeletedFleets.Add(fleet);
                               deletedFleetsNode = deletedFleetsNode.NextSibling;
                           }
                           break;

                       case "racedata":
                           PlayerData = new RaceData(xmlnode);
                           break;

                       default: break;
                   }

               }
               catch (Exception e)
               {
                   Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
               }
               xmlnode = xmlnode.NextSibling;
           }
       }

       /// <summary>
       /// Write out the orders file using binary serialization
       /// </summary>
       public void ToBinary(string ordersFileName)
       {
           FileStream ordersFile = new FileStream(ordersFileName, FileMode.Create);
           Formatter.Serialize(ordersFile, this);
           ordersFile.Close();
       }

       /// <summary>
       /// Write out the orders using xml format
       /// </summary>
       /// <param name="ordersFileName">The path&filename to save the orders too.</param>
       public void ToXml(string ordersFileName)
       {
           FileStream ordersFile = new FileStream(ordersFileName, FileMode.Create);
           GZipStream compressionStream = new GZipStream(ordersFile, CompressionMode.Compress);

           // Setup the XML document
           XmlDocument xmldoc;
           XmlNode xmlnode;
           XmlElement xmlRoot;
           xmldoc = new XmlDocument();
           // TODO (priority 4) - add the encoding attribute like UTF-8 ???
           xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
           xmldoc.AppendChild(xmlnode);
           xmlRoot = xmldoc.CreateElement("", "ROOT", "");
           xmldoc.AppendChild(xmlRoot);

           // add the orders to the document
           XmlElement xmlelOrders = xmldoc.CreateElement("Orders");
           xmlRoot.AppendChild(xmlelOrders);

           // Place the turn year first, so it can be determined quickly
           Global.SaveData(xmldoc, xmlelOrders, "Turn", PlayerData.TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));

           // Store the fleets, to pass on fleet orders
           foreach (DictionaryEntry de in RaceFleets)
           {
               Fleet fleet = de.Value as Fleet;
               xmlelOrders.AppendChild(fleet.ToXml(xmldoc));
           }
           // store the designs, for any new designs
           foreach (DictionaryEntry de in RaceDesigns)
           {
               Design design = de.Value as Design;
               if (design.Type == "Ship" || design.Type == "Starbase")
                   xmlelOrders.AppendChild(((ShipDesign)design).ToXml(xmldoc));
               else
                   xmlelOrders.AppendChild(design.ToXml(xmldoc));
           }
           // store the stars, so we can pass production orders
           foreach (Star star in RaceStars)
           {
               xmlelOrders.AppendChild(star.ToXml(xmldoc));
           }

           // Deleted fleets and designs are wrapped in a section node
           // so they can be told appart from current designs and fleets when
           // loaded.
           XmlElement xmlelDeletedFleets = xmldoc.CreateElement("DeletedFleets");
           foreach (Fleet fleet in DeletedFleets)
           {
               xmlelDeletedFleets.AppendChild(fleet.ToXml(xmldoc));
           }
           xmlelOrders.AppendChild(xmlelDeletedFleets);

           XmlElement xmlelDeletedDesigns = xmldoc.CreateElement("DeletedDesigns");
           foreach (Design design in DeletedDesigns)
           {
               if (design.Type == "Ship" || design.Type == "Starbase")
                   xmlelDeletedDesigns.AppendChild(((ShipDesign)design).ToXml(xmldoc));
               else
                   xmlelDeletedDesigns.AppendChild(design.ToXml(xmldoc)); 
           }
           xmlelOrders.AppendChild(xmlelDeletedDesigns);

           xmlelOrders.AppendChild(PlayerData.ToXml(xmldoc));
           Global.SaveData(xmldoc, xmlelOrders, "TechLevel", TechLevel.ToString(System.Globalization.CultureInfo.InvariantCulture));
           

           // You can comment/uncomment the following lines to turn compression on/off if you are doing a lot of 
           // manual inspection of the save file. Generally though it can be opened by any archiving tool that
           // reads gzip format.
#if (DEBUG)
           xmldoc.Save(ordersFile);                                           //  not compressed
#else
           xmldoc.Save(compressionStream); compressionStream.Close();    //   compressed 
#endif

           ordersFile.Close();
       }
   }
}
