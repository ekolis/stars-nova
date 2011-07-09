#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
    using System.ComponentModel;
    using System.Xml;

    using Nova.Common.DataStructures;

    /// <summary>
    /// This module describes what we know about each star system in the game.
    /// Owned stars have full information, while scanned might have only some
    /// and unscanned barely anything.
    /// </summary>
    [Serializable]
    public class StarIntel : Star
    {

        public int          Year;      
        public IntelLevel   IntelAmount;
        
        public StarIntel() :
            base()
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the StarReport class.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> being reported</param>
        public StarIntel(Star star, IntelLevel intelAmount, int year) :
            base()
        {   
            Year        = 0;            
            IntelAmount = IntelLevel.None;           
            
            Update(star, intelAmount, year);            
        } 

        public void Update(Star star, IntelLevel intelAmount, int year)
        {
            if (star == null)
            {
                return;
            }
            
            if (year < this.Year)
            {
                return;
            }
            
            // This controls what we update for this report.
            IntelAmount = intelAmount;
            
            // Information that is always available and doesn't
            // depend on scanning level.
            Name     = star.Name; 
            Type     = star.Type;
            Position = star.Position; // Can this change? Random Events?
             
            if (IntelAmount >= IntelLevel.None)
            {            
                // We do nothing.
            }
            
            // If we are at least scanning with non-penetrating
            if (IntelAmount >= IntelLevel.InScan)
            {
               // Non-pen scanners are useless for stars.
            }
            
            // If we are at least currently in orbit of the star
            // with no scanners.
            if (IntelAmount >= IntelLevel.InPlace)
            {
                // We can at least see it, so set age to current.
                this.Year = year;
                
                Owner                = star.Owner;                
                MineralConcentration = star.MineralConcentration;
                Gravity              = star.Gravity;
                Radiation            = star.Radiation;
                Temperature          = star.Temperature;                    
            }
            
            // If we are have Pen-Scanners, or we are
            // in orbit with scanners.
            if (IntelAmount >= IntelLevel.InDeepScan)
            {                
                Colonists = star.Colonists;    
            }
            
            // If the star is ours.
            if (IntelAmount >= IntelLevel.Owned)
            {                
                OriginalGravity      = star.OriginalGravity;
                OriginalRadiation    = star.OriginalRadiation;
                OriginalTemperature  = star.OriginalTemperature;
                Factories            = star.Factories;
                Mines                = star.Mines;
                ResourcesOnHand      = star.ResourcesOnHand;
                ResearchAllocation   = star.ResearchAllocation;
                ManufacturingQueue   = star.ManufacturingQueue;
                OnlyLeftover         = star.OnlyLeftover;
                Defenses             = star.Defenses;
                DefenseType          = star.DefenseType;
                ScanRange            = star.ScanRange;
                ScannerType          = star.ScannerType;
                ThisRace             = star.ThisRace;
                Starbase             = star.Starbase;
            }
        }
        
        public StarIntel LoadFromXml(XmlNode xmlnode)
        {
            StarIntel intel;
            Star star = null;
            
            XmlNode node = xmlnode.FirstChild;
            while (node != null)
            {
                switch (node.Name.ToLower())
                {
                    case "year":
                        Year = int.Parse(node.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "intelamount":
                        IntelAmount = (IntelLevel)Enum.Parse(typeof(IntelLevel), node.FirstChild.Value, true);
                        break;
                    case "star":
                        star = new Star(node);
                        break;
                }
                
                node = node.NextSibling;
            }
            intel = new StarIntel(star, IntelAmount, Year);
            return intel;            
        }
        
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelStarIntel = xmldoc.CreateElement("StarIntel");
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Year", Year.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelStarIntel, "IntelAmount", IntelAmount.ToString());

            xmlelStarIntel.AppendChild(base.ToXml(xmldoc));

            return xmlelStarIntel;   
        }
    }
}
