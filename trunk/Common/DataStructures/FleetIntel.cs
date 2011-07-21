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
    using System.Xml;
 
    using Nova.Common;
    using Nova.Common.DataStructures;

    /// <summary>
    /// This module describes what we know about each star system we have visited 
    /// or scanned.
    /// </summary>
    [Serializable]
    public class FleetIntel : Item
    {
        public int          Year    {get; set;}
        public double       Bearing {get; set;}
        public int          Speed   {get; set;}


        /// <summary>
        /// Constructor to use with LoadFromXml. Calls Fleet(0) which is a
        /// bogus ID which should be replaced during xml load.
        /// </summary>
        public FleetIntel() :
            base()
        {
            Clear();
        }
        
        /// <summary>
        /// Initializes a new instance of the FleetIntel class.
        /// </summary>
        /// <param name="fleet">The <see cref="Fleet"/> being reported</param>
        public FleetIntel(Fleet fleet, ScanLevel scan, int year) :
            base()
        {
            Clear();           
            Update(fleet, scan, year);
        }

        public FleetIntel(XmlNode xmlnode) :
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
            Owner                   = Global.NoOwner;
            Type                    = ItemType.FleetIntel;           
        }
        
        public void Update(Fleet fleet, ScanLevel scan, int year)
        {
            if (fleet == null) { return; }
            if (year < this.Year) { return; }
            
            // Information that is always available and doesn't
            // depend on scanning level. Nothing for fleets.
            
            Key         = fleet.Key;
            Name        = fleet.Name;
            Type        = ItemType.FleetIntel;
            
             
            if (scan >= ScanLevel.None)
            {            
                // We keep the information we have.
            }
            
            // If we are at least scanning with non-penetrating
            if (scan >= ScanLevel.InScan)
            {
                // We can at least see it, so set age to current.
                Year = year;
                
                Position  = fleet.Position;
                Bearing   = fleet.Bearing;
                Speed     = fleet.Speed;               
            }
            
            // You can't orbit a fleet!
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

            }    
        }
        
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelFleetIntel = xmldoc.CreateElement("FleetIntel");
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            // include inherited Item properties
            xmlelFleetIntel.AppendChild(base.ToXml(xmldoc));

            return xmlelFleetIntel;   
        }
    }
}
