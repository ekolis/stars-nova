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
    public class FleetIntel : Fleet
    {
        public int          Age;      
        public Fleet        Fleet; // Consider a diferent name!
        public IntelLevel   IntelAmount;
        
        public const int UNSEEN = -1;

        /// <summary>
        /// Initializes a new instance of the FleetIntel class.
        /// </summary>
        /// <param name="fleet">The <see cref="Fleet"/> being reported</param>
        public FleetIntel(Fleet fleet, IntelLevel intelAmount) :
            base(fleet)
        {
            Age         = UNSEEN;            
            IntelAmount = IntelLevel.None;
           
            Update(fleet, intelAmount);
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
                
                Position  = fleet.Position;
                Type      = fleet.Type;
                Bearing   = fleet.Bearing;
                Speed     = fleet.Speed;

            }
            
            // You can't orbit a fleet!
            if (IntelAmount >= IntelLevel.InOrbit)
            {
                    
            }
            
            // Pen scans do nothing for fleets.
            if (IntelAmount >= IntelLevel.InDeepScan)
            {                
   
            }
            
            // If the fleet is ours.
            if (IntelAmount >= IntelLevel.Owned)
            {                

            }    
        }
        
        public FleetIntel LoadFromXml(XmlNode xmlnode)
        {
            FleetIntel intel;
            
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
            
            intel = new FleetIntel(Fleet, IntelAmount);
            
            return intel;            
        }
        
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelFleetIntel = xmldoc.CreateElement("FleetIntel");
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "Age", Age.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelFleetIntel, "IntelAmount", IntelAmount.ToString());

            xmlelFleetIntel.AppendChild(base.ToXml(xmldoc));

            return xmlelFleetIntel;   
        }
    }
}
