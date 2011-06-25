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
    public class StarIntel
    {

        public int          Age;      
        public Star         Star; // Consider a diferent name!
        public IntelLevel   IntelAmount;
        
        public const int UNSEEN = -1;
        
        /// <summary>
        /// Initializes a new instance of the StarReport class.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> being reported</param>
        public StarIntel(Star star, IntelLevel intelAmount)
        {   
            Age         = UNSEEN;            
            IntelAmount = IntelLevel.None;
            Star  = new Star();
            
            Update(star, intelAmount);            
        }
        
        public void Update(Star star, IntelLevel intelAmount)
        {
            // This controls what we update for this report.
            IntelAmount = intelAmount;
            
            // Information that is always available and doesn't
            // depend on scanning level.
            Star.Name     = star.Name; 
            Star.Type     = star.Type;
            Star.Position = star.Position; // Can this change? Random Events?
             
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
                
                // Non-pen can only detect fleets.
                // FIXME:(priority 3) Is this accurate? does it
                // also reveal the owner if it is or displays "???" ?
                Star.OrbitingFleets   = star.OrbitingFleets;
                Star.Starbase         = star.Starbase;
            }
            
            // If we are at least currently in orbit of the star
            // with no scanners.
            if (IntelAmount >= IntelLevel.InOrbit)
            {
                Star.Owner                = star.Owner;                
                Star.MineralConcentration = star.MineralConcentration;
                Star.Gravity              = star.Gravity;
                Star.Radiation            = star.Radiation;
                Star.Temperature          = star.Temperature;                    
            }
            
            // If we are have Pen-Scanners, or we are
            // in orbit with scanners.
            if (IntelAmount >= IntelLevel.InDeepScan)
            {                
                Star.Colonists = star.Colonists;    
            }
            
            // If the star is ours.
            if (IntelAmount >= IntelLevel.Owned)
            {                
                Star.OriginalGravity      = star.OriginalGravity;
                Star.OriginalRadiation    = star.OriginalRadiation;
                Star.OriginalTemperature  = star.OriginalTemperature;
                Star.Factories            = star.Factories;
                Star.Mines                = star.Mines;
                Star.ResourcesOnHand      = star.ResourcesOnHand;
                Star.ResearchAllocation   = star.ResearchAllocation;
                Star.ManufacturingQueue   = star.ManufacturingQueue;
                Star.OnlyLeftover         = star.OnlyLeftover;
                Star.Defenses             = star.Defenses;
                Star.DefenseType          = star.DefenseType;
                Star.ScanRange            = star.ScanRange;
                Star.ScannerType          = star.ScannerType;
                Star.ThisRace             = star.ThisRace;
            }
        }
        
        public StarIntel(XmlNode xmlnode)
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
                        case "star":
                            Star = new Star(node);
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
            XmlElement xmlelStarIntel = xmldoc.CreateElement("StarIntel");
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Age", Age.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelStarIntel, "IntelAmount", IntelAmount.ToString());

            xmlelStarIntel.AppendChild(Star.ToXml(xmldoc));

            return xmlelStarIntel;   
        }
    }
}
