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
    
    /// <summary>
    /// Reports basic information about ship groups in fleet reports.
    /// </summary>
    public class ShipIntel
    {
        /// <summary>
        /// Ship/Design name, for displaying on GUI.
        /// </summary>
        public string   Name    { get; set; }
        
        /// <summary>
        /// Design Key, for lookup in known enemy designs. Note that
        /// until fought the design is unknown so it may not exist
        /// on the known enemy designs collection. Nonetheless this Key is
        /// always set for consistency.
        /// </summary>
        public long     Design  { get; set; }
        
        /// <summary>
        /// Amount of Ships under this same design.
        /// </summary>
        public int      Count   { get; set; }
        
        /// <summary>
        /// Total mass of the Fleet
        /// </summary>
        public int      Mass    { get; set; }
        
        /// <summary>
        /// Default constructor. Sets sensible but meaningless default values for this report.
        /// </summary>
        public ShipIntel()
        {
            Clear();
        }
        
        /// <summary>
        /// Load: Initialising constructor to read in a Ship report from an XmlNode (from a saved file).
        /// </summary>
        /// <param name="xmlnode">An XmlNode representing a Ship report.</param>
        public ShipIntel(XmlNode xmlnode)
        {
            XmlNode node = xmlnode.FirstChild;
            
            while (node != null)
            {
                try
                {
                    switch (node.Name.ToLower())
                    {
                    case "name":
                        Name = node.FirstChild.Value;
                        break;
                        
                    case "design":
                        Design = long.Parse(node.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                        break;
                        
                    case "count":
                        Count = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                        
                    case "mass":
                        Mass = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
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
            Name    = String.Empty;
            Design  = Global.Unset;
            Count   = Global.Unset;            
        }
        
        /// <summary>
        /// Create an XmlElement representation of the ship report for saving.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representation of the report.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelShipIntel = xmldoc.CreateElement("ShipIntel");
                        
            Global.SaveData(xmldoc, xmlelShipIntel, "Name", Name);
            Global.SaveData(xmldoc, xmlelShipIntel, "Design", Design.ToString("X"));
            Global.SaveData(xmldoc, xmlelShipIntel, "Count", Count.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelShipIntel, "Mass", Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelShipIntel;   
        }
    }
}
