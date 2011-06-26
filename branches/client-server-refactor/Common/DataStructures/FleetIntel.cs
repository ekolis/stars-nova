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
    public class FleetIntel
    {
        public int          Age;      
        public Fleet        Fleet; // Consider a diferent name!
        public IntelLevel   IntelAmount;
        
        public const int UNSEEN = -1;

        /// <summary>
        /// Initializes a new instance of the FleetIntel class.
        /// </summary>
        /// <param name="star">The <see cref="Fleet"/> being reported</param>
        public FleetIntel(Fleet fleet, IntelLevel intelAmount)
        {
            Age         = UNSEEN;            
            IntelAmount = IntelLevel.None;
            Fleet       = new Fleet(-1);
           
            Update(fleet, intelAmount);
        }
        
        /// <summary>
        /// Makes conversion from FleetIntel to Fleet possible. Eases
        /// foreach loops and data access.
        /// </summary>
        /// <param name="s">A <see cref="FleetIntel"/> containing the desired Fleet.</param>
        /// <returns>The <see cref="Fleet"/> contained in this Intel.</returns>
        public static explicit operator Fleet(FleetIntel f)
        {
            return f.Fleet;
        } 
        
        public void Update(Fleet fleet, IntelLevel intelAmount)
        {
            // This controls what we update for this report.
            IntelAmount = intelAmount;
            
            // Information that is always available and doesn't
            // depend on scanning level. Nothing for fleets.
             
            if (IntelAmount >= IntelLevel.None)
            {            
                // We keep the information we have, but age it.
                if (Age != UNSEEN)
                {
                    Age++;
                }
            }
            
            // If we are at least scanning with non-penetrating
            if (IntelAmount >= IntelLevel.InScan)
            {
                // We can at least see it, so set age to current.
                Age = 0;
                
                Fleet.Position  = fleet.Position;
                Fleet.Type      = fleet.Type;
                Fleet.Bearing   = fleet.Bearing;

            }
            
            // You can't orbit a fleet!
            if (IntelAmount >= IntelLevel.InOrbit)
            {
                    
            }
            
            // If it is in range of Pen-Scanners.
            if (IntelAmount >= IntelLevel.InDeepScan)
            {                
   
            }
            
            // If the fleet is ours.
            if (IntelAmount >= IntelLevel.Owned)
            {                

            }    
        }
        
        public FleetIntel(XmlNode xmlnode)
        {
            XmlNode node = xmlnode.FirstChild;
            while (node != null)
            {
                try
                {
                    switch (node.Name.ToLower())
                    {
                        case "age":
                            Age = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "intelamount":
                            IntelAmount = (IntelLevel)Enum.Parse(typeof(IntelLevel), node.FirstChild.Value, true);
                            break;
                        case "fleet":
                            Fleet = new Fleet(node);
                            break;
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }

                node = node.NextSibling;
            }    
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelFleetIntel = xmldoc.CreateElement("FleetIntel");
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Age", Age.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "IntelAmount", IntelAmount.ToString());

            xmlelFleetIntel.AppendChild(Fleet.ToXml(xmldoc));

            return xmlelFleetIntel;   
        }
    }
}
