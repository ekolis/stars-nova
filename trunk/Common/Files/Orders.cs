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

namespace Nova.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;
    using Nova.Common.Components;

    /// <summary>
    /// This object defines the data that is generated by the Nova GUI and passed
    /// to the Nova Console so that it can generate the turn for the next year. These
    /// are the orders sent to the console/server.
    /// </summary>
    [Serializable]
    public sealed class Orders
    {
        public Dictionary<long, Design> RaceDesigns = new Dictionary<long, Design>();     // For any new designs
        
        /// <summary>
        /// List of fleets (Fleet.Key) to delete.
        /// </summary>
        public List<long> DeletedFleets = new List<long>();

        /// <summary>
        /// List of designs (Design.Key) to delete.
        /// </summary>
        public List<long> DeletedDesigns = new List<long>(); 

        public EmpireData EmpireStatus = new EmpireData();        // Player relations, battle orders & turn # (turn # so we can check these orders are for the right year.) and research %
        public int TechLevel;                               // This is the sum of all the player's tech levels, used for victory checks.
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Orders() 
        { 
        }

        /// <summary>
        /// Read in a set of orders.
        /// </summary>
        /// <param name="xmlnode">An <see cref="XmlNode"/> containing orders.</param>
        public Orders(XmlNode xmlnode)
        {
            // temporary variables for reading in designs, fleets, stars
            Design design = null;
            ShipDesign shipDesign = null;

            // Read the node
            while (xmlnode != null)
            {
                try
                {
                    switch (xmlnode.Name.ToLower())
                    {
                        case "root":
                            xmlnode = xmlnode.FirstChild;
                            continue;
                        case "orders":
                            xmlnode = xmlnode.FirstChild;
                            continue;

                        // When loading designs we need to know what type of design it is.
                        // To do this we first look ahead at the Type field of the design,
                        // then load a design of the appropriate type (currently Design or ShipDesign).
                        case "design":
                            {
                                string type = xmlnode.FirstChild.SelectSingleNode("Type").FirstChild.Value;
                                if (type.ToLower() == "ship" || type == "starbase")
                                {
                                    shipDesign = new ShipDesign(xmlnode);
                                    RaceDesigns.Add(shipDesign.Key, shipDesign);
                                }
                                else
                                {
                                    design = new Design(xmlnode);
                                    RaceDesigns.Add(design.Key, design);
                                }
                            }
                            break;

                        case "shipdesign":
                            {
                                shipDesign = new ShipDesign(xmlnode);
                                RaceDesigns.Add(shipDesign.Key, shipDesign);
                            }
                            break;

                        // Deleted designs are in their own section to seperate them from 
                        // current designs. We load this section in one loop. The comments
                        // above for designs apply equally to deleted designs.
                        case "deleteddesigns":
                            XmlNode deletedDesignsNode = xmlnode.FirstChild;
                            while (deletedDesignsNode != null)
                            {
                                // only the deletedDesignKey is stored in the xml file
                                long deletedDesignKey = long.Parse(deletedDesignsNode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                                DeletedDesigns.Add(deletedDesignKey);
                                deletedDesignsNode = deletedDesignsNode.NextSibling;
                            }
                            break;
                            
                        // Deleted fleets are contained in their own section to seperate them from
                        // current fleets. We load this section in this loop.
                        case "deletedfleets":
                            XmlNode deletedFleetsNode = xmlnode.FirstChild;
                            while (deletedFleetsNode != null)
                            {
                                // only the fleet.key is stored in the xml file
                                long key = long.Parse(deletedFleetsNode.FirstChild.Value);
                                DeletedFleets.Add(key);
                                deletedFleetsNode = deletedFleetsNode.NextSibling;
                            }
                            break;

                        case "empiredata":
                            EmpireStatus = new EmpireData(xmlnode);
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
        /// Write out the orders using xml format.
        /// </summary>
        /// <param name="ordersFileName">The path and filename to save the orders too.</param>
        public void ToXml(string ordersFileName)
        {
            // Setup the XML document
            XmlDocument xmldoc = new XmlDocument();
            XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);

            // add the orders to the document
            XmlElement xmlelOrders = xmldoc.CreateElement("Orders");
            xmlRoot.AppendChild(xmlelOrders);

            // Place the turn year first, so it can be determined quickly
            Global.SaveData(xmldoc, xmlelOrders, "Turn", EmpireStatus.TurnYear.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            // store the designs, for any new designs
            foreach (Design design in RaceDesigns.Values)
            {
                if (design.Type == ItemType.Starbase || design.Type == ItemType.Ship)
                {
                    xmlelOrders.AppendChild(((ShipDesign)design).ToXml(xmldoc));
                }
                else
                {
                    xmlelOrders.AppendChild(design.ToXml(xmldoc));
                }
            }

            // Deleted fleets and designs are wrapped in a section node
            // so they can be told appart from current designs and fleets when
            // loaded.
            XmlElement xmlelDeletedFleets = xmldoc.CreateElement("DeletedFleets");
            foreach (int fleetKey in DeletedFleets)
            {
                // only need to store enough data to find the deleted fleet.
                Global.SaveData(xmldoc, xmlelDeletedFleets, "FleetKey", fleetKey.ToString("X"));
            }
            xmlelOrders.AppendChild(xmlelDeletedFleets);

            XmlElement xmlelDeletedDesigns = xmldoc.CreateElement("DeletedDesigns");
            foreach (int designKey in DeletedDesigns)
            {
                Global.SaveData(xmldoc, xmlelDeletedDesigns, "DesignKey", designKey);
            }
            xmlelOrders.AppendChild(xmlelDeletedDesigns);

            xmlelOrders.AppendChild(EmpireStatus.ToXml(xmldoc));

            Stream output = new FileStream(ordersFileName, FileMode.Create);
            // output = new GZipStream(output, CompressionMode.Compress);

            xmldoc.Save(output);
            output.Close();

            output.Close();
        }
    }
}
