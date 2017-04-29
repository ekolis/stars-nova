#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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
    using System.Collections.Generic;
    using System.Xml;
 
    using Nova.Common.DataStructures;

    /// <summary>
    /// This module describes basic things we can
    /// know about a Fleet.
    /// </summary>
    [Serializable]
    public class FleetIntel : Mappable
    {
        public int                          Year        { get; set; }
        public ShipIcon                     Icon        { get; set; }
        public double                       Bearing     { get; set; }
        public int                          Speed       { get; set; }
        public bool                         InOrbit     { get; set; }
        public bool                         IsStarbase  { get; set; }
        public Dictionary<long, ShipToken>  Composition { get; set; }
        
        /// <summary>
        /// The Fleet's total ship count.
        /// </summary>
        public int Count
        {
            get
            {
                int qty = 0;
                foreach (ShipToken token in Composition.Values)
                {
                    qty += token.Quantity;
                }
                
                return qty;
            }
        }
        
        /// <summary>
        /// The Fleet's total mass in kTs.
        /// </summary>
        public int Mass
        {
            get
            {
                int mass = 0;
                foreach (ShipToken token in Composition.Values)
                {
                    mass += token.Design.Mass;
                }
                
                return mass;   
            }
        }
        
        /// <summary>
        /// Default constructor. Sets sensible but meaningless default values for this report.
        /// </summary>
        public FleetIntel() :
            base()
        {
            Clear();
        }
        
        /// <summary>
        /// Creates a fleet report from a fleet.
        /// </summary>
        /// <param name="fleet">Fleet to report.</param>
        /// <param name="scan">Amount of Knowledge to set.</param>
        /// <param name="year">Year of the data.</param>
        public FleetIntel(Fleet fleet, ScanLevel scan, int year) :
            base(fleet)
        {
            Clear();           
            Update(fleet, scan, year);
        }

        /// <summary>
        /// Load: initializing constructor to read in a Fleet report from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="xmlnode">An XmlNode representing a Fleet report.</param>
        public FleetIntel(XmlNode xmlnode) :
            base(xmlnode)
        {            
            XmlNode node = xmlnode.FirstChild;
            XmlNode subNode;
            
            while (node != null)
            {
                try
                {
                    switch (node.Name.ToLower())
                    {
                        case "year":
                            Year = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "icon":
                            string iconSource = node.FirstChild.Value;
                            Icon = AllShipIcons.Data.GetIconBySource(iconSource);
                            break;
                        case "bearing":
                            Bearing = double.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "speed":
                            Speed = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "inorbit":
                            InOrbit = bool.Parse(node.FirstChild.Value);
                            break;
                        case "isstarbase":
                            IsStarbase = bool.Parse(node.FirstChild.Value);
                            break;
                        case "composition":
                            // We can't call Clear() or we'll erase data set by base(xmlnode), so initialize collection here.
                            Composition = new Dictionary<long, ShipToken>();                            
                            subNode = node.FirstChild;
                            ShipToken token;
                            while (subNode != null)
                            {
                                token = new ShipToken(subNode);
                                Composition.Add(token.Key, token);
                                subNode = subNode.NextSibling;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }

                node = node.NextSibling;
            }           
        }        
        
        /// <summary>
        /// Resets all values to default.
        /// </summary>
        public void Clear()
        {
            Year                    = Global.Unset;
            Name                    = string.Empty;
            Position                = new NovaPoint();
            Owner                   = Global.Nobody;
            Type                    = ItemType.FleetIntel;
            Bearing                 = Global.Unset;
            Speed                   = Global.Unset;
            Composition             = new Dictionary<long, ShipToken>();
        }
        
        /// <summary>
        /// Updates the report with data from a fleet.
        /// </summary>
        /// <param name="fleet">Fleet to report.</param>
        /// <param name="scan">Amount of Knowledge to set.</param>
        /// <param name="year">Year of the updated data.</param>
        public void Update(Fleet fleet, ScanLevel scan, int year)
        {
            if (fleet == null) 
            { 
                return; 
            }
            
            // Information that is always available and doesn't
            // depend on scanning level.
            
            Key         = fleet.Key;
            Name        = fleet.Name;
            Icon        = fleet.Icon;
            Type        = ItemType.FleetIntel;
            
            if (year < this.Year) 
            { 
                return; 
            }
             
            if (scan >= ScanLevel.None)
            {            
                // We keep the information we have.
            }
            
            // If we are at least scanning with non-penetrating
            if (scan >= ScanLevel.InScan)
            {
                // We can at least see it, so set age to current.
                Year = year;
                
                Position    = fleet.Position;
                Bearing     = fleet.Bearing;
                Speed       = fleet.Speed;
                InOrbit     = (fleet.InOrbit == null) ? false : true;
                IsStarbase  = fleet.IsStarbase;
                Composition = fleet.Composition;                                
            }
            
            // If in the same position.
            if (scan >= ScanLevel.InPlace)
            {
            }
            
            // Pen scans do nothing for fleets.
            if (scan >= ScanLevel.InDeepScan)
            {                
            }
            
            // If the fleet is ours.
            if (scan >= ScanLevel.Owned)
            {
                // Owned fleets are handled elsewhere.
            }    
        }
        
        /// <summary>
        /// Create an XmlElement representation of the fleet report for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the report.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {            
            XmlElement xmlelFleetIntel = xmldoc.CreateElement("FleetIntel");
            
            // include inherited Item properties
            xmlelFleetIntel.AppendChild(base.ToXml(xmldoc));
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Icon", Icon.Source);
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Bearing", Bearing.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleetIntel, "Speed", Speed.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelFleetIntel, "InOrbit", InOrbit.ToString());
            Global.SaveData(xmldoc, xmlelFleetIntel, "IsStarbase", IsStarbase.ToString());
            
            // Store the composition
            XmlElement xmlelComposition = xmldoc.CreateElement("Composition");
            foreach (ShipToken token in Composition.Values)
            {
                xmlelComposition.AppendChild(token.ToXml(xmldoc));
            }
            xmlelFleetIntel.AppendChild(xmlelComposition);

            return xmlelFleetIntel;   
        }
    }
}
