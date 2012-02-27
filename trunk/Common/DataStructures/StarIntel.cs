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
    using System.Xml;

    using Nova.Common.DataStructures;

    /// <summary>
    /// This module describes the basic things we can
    /// know about a Star system.
    /// </summary>
    [Serializable]
    public class StarIntel : Item
    {
        public int          Year                    { get; set; }
        public Resources    MineralConcentration    { get; set; }
        public int          Gravity                 { get; set; }
        public int          Radiation               { get; set; }
        public int          Temperature             { get; set; }
        public int          Colonists               { get; set; }
        public bool         HasFleetsInOrbit        { get; set; }
        public Fleet        Starbase                { get; set; }

        /// <summary>
        /// Default constructor. Sets sensible but meaningless default values for this report.
        /// </summary>
        public StarIntel() :
            base()
        {
            Clear();
        }
        
        /// <summary>
        /// Creates a star report from a star.
        /// </summary>
        /// <param name="star">Star to report.</param>
        /// <param name="scan">Amount of Knowledge to set.</param>
        /// <param name="year">Year of the data.</param>
        public StarIntel(Star star, ScanLevel scan, int year) :
            base()
        {
            Clear();
            Update(star, scan, year);
        }
        
        /// <summary>
        /// Load: Initialising constructor to read in a Star report from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="xmlnode">An XmlNode representing a Star report.</param>
        public StarIntel(XmlNode xmlnode) :
            base(xmlnode)
        {
            XmlNode node = xmlnode.FirstChild;
            
            while (node != null)
            {
                try
                {
                    switch (node.Name.ToLower())
                    {
                    case "year":
                        Year = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "mineralconcentration":
                        MineralConcentration = new Resources(node.FirstChild);
                        break;
                    case "gravity":
                        Gravity = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "radiation":
                        Radiation = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "temperature":
                        Temperature = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "colonists":
                        Colonists = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "hasfleetsinorbit":
                        HasFleetsInOrbit = bool.Parse(node.FirstChild.Value);
                        break;
                    case "starbase":
                        Starbase = new Fleet(long.Parse(node.FirstChild.Value, System.Globalization.NumberStyles.HexNumber));
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
            Name                    = String.Empty;
            Position                = new NovaPoint();
            Owner                   = Global.Nobody;
            Type                    = ItemType.StarIntel;
            MineralConcentration    = new Resources();
            Gravity                 = Global.Unset;
            Radiation               = Global.Unset;
            Temperature             = Global.Unset;
            Colonists               = Global.Unset;
            HasFleetsInOrbit        = false;
            Starbase                = null;            
        }
                
        /// <summary>
        /// Returns the name of the Star.
        /// </summary>
        /// <returns>A string with the format "Star: [name]".</returns>
        public override string ToString()
        {
            return "Star: " + Name;
        }
        
        /// <summary>
        /// Updates the report with data from a star.
        /// </summary>
        /// <param name="star">Star to report.</param>
        /// <param name="scan">Amount of Knowledge to set.</param>
        /// <param name="year">Year of the udpated data.</param>
        public void Update(Star star, ScanLevel scan, int year)
        {
            Clear();
            
            if (star == null) 
            {
                return;
            }

            if (year < this.Year) 
            { 
                return; 
            }
            
            // Information that is always available and doesn't
            // depend on scanning level.
            Name     = star.Name; 
            Position = star.Position; // Can this change? Random Events?
             
            if (scan >= ScanLevel.None)
            {            
                // We can't see this star.
            }
            
            // If we are at least scanning with non-penetrating
            if (scan >= ScanLevel.InScan)
            {
               // Non-pen scanners are useless for stars.
            }
            
            // If we are at least currently in orbit of the star
            // with no scanners.
            if (scan >= ScanLevel.InPlace)
            {
                // We can at least see it, so set age to current.
                Year = year;
                
                Owner                   = star.Owner;                
                MineralConcentration    = star.MineralConcentration;
                Gravity                 = star.Gravity;
                Radiation               = star.Radiation;
                Temperature             = star.Temperature;
                Starbase                = star.Starbase;
                HasFleetsInOrbit        = star.HasFleetsInOrbit;                
            }
            
            // If we are have Pen-Scanners, or we are
            // in orbit with scanners.
            if (scan >= ScanLevel.InDeepScan)
            {                
                Colonists = star.Colonists;    
            }
            
            // If the star is ours.
            if (scan >= ScanLevel.Owned)
            {                
                // We do nothing, as owned Stars are handled
                // elsewhere.
            }
        }
        
        /// <summary>
        /// Create an XmlElement representation of the star report for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the report.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelStarIntel = xmldoc.CreateElement("StarIntel");
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            // include inherited Item properties
            xmlelStarIntel.AppendChild(base.ToXml(xmldoc));
            
            XmlElement xmlelMineralConcentration = xmldoc.CreateElement("MineralConcentration");
            xmlelMineralConcentration.AppendChild(MineralConcentration.ToXml(xmldoc));
            xmlelStarIntel.AppendChild(xmlelMineralConcentration);
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Gravity", Gravity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStarIntel, "Radiation", Radiation.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelStarIntel, "Temperature", Temperature.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Colonists", Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelStarIntel, "HasFleetsInOrbit", HasFleetsInOrbit.ToString());
            
            if (Starbase != null)
            {
                Global.SaveData(xmldoc, xmlelStarIntel, "Starbase", Starbase.Key.ToString("X"));
            }

            return xmlelStarIntel;   
        }
    }
}
