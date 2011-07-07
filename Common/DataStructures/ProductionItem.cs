using System;
using System.Globalization;
using System.Xml;

namespace Nova.Common
{
    /// <summary>
    /// Details of a design in the queue.
    /// </summary>
    [Serializable]
    public class ProductionItem 
    {
        public int Id;                // Design id
        public string Name;           // Design Name
        public int Quantity;          // Number to build
        public Resources BuildState;  // Resources need to build item // ??? (priority 6) just the next 1 or the whole lot? - Dan 10 Jan 10
        // Should be removed in favor of Unit.ResourcesNeeded * Quantity
        public bool Autobuild;
        private IProductionUnit unit;

        /// <summary>
        /// default constructor
        /// </summary>
        public ProductionItem() 
        { 
        }

        /// <summary>
        /// Return the resources needed to 
        /// </summary>
        /// <returns></returns>
        public Resources NeededResources()
        {
            Resources unitResources = this.unit.NeededResources();
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
        /// <param name="node">An XmlNode containing a representation of a ProductionQueue.Item</param>
        public ProductionItem(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "id":
                            Id = Int32.Parse(subnode.FirstChild.Value, NumberStyles.HexNumber);
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
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representation of the ProductionQueue.Item</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelProductionOrder = xmldoc.CreateElement("ProductionOrder");

            Global.SaveData(xmldoc, xmlelProductionOrder, "Id", Id.ToString("X"));
            Global.SaveData(xmldoc, xmlelProductionOrder, "Name", Name);
            Global.SaveData(xmldoc, xmlelProductionOrder, "Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
            xmlelProductionOrder.AppendChild(BuildState.ToXml(xmldoc));

            return xmlelProductionOrder;
        }

        #endregion
    }
}