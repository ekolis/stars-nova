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

        public int          Age;      
        public IntelLevel   IntelAmount;
        
        public const int UNSEEN = -1;
        
        public StarIntel() :
            base()
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the StarReport class.
        /// </summary>
        /// <param name="star">The <see cref="Star"/> being reported</param>
        public StarIntel(Star star, IntelLevel intelAmount) :
            base()
        {   
            Age         = UNSEEN;            
            IntelAmount = IntelLevel.None;
            
            Update(star, intelAmount);            
        } 
        
        public void Unsee()
        {
            if (Age != UNSEEN)
            {
                Age++;
            }    
        }
        
        public void Update(Star star, IntelLevel intelAmount)
        {
            if (star == null)
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
                // We keep the information we have, but age it.
                Unsee();
            }
            
            // If we are at least scanning with non-penetrating
            if (IntelAmount >= IntelLevel.InScan)
            {
                // We can at least see it, so set age to current.
                Age = 0;
                
                // Non-pen can only detect fleets.
                // FIXME:(priority 3) Is this accurate? does it
                // also reveal the owner if it is or displays "???" ?
                OrbitingFleets   = star.OrbitingFleets;
                Starbase         = star.Starbase;
            }
            
            // If we are at least currently in orbit of the star
            // with no scanners.
            if (IntelAmount >= IntelLevel.InOrbit)
            {
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
            }
        }
        
        public StarIntel LoadFromXml(XmlNode xmlnode)
        {
            StarIntel intel;
            Star star = null;
            
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
                            star = new Star(node);
                            break;
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }
                
                node = node.NextSibling;
            }
            intel = new StarIntel(star, IntelAmount);
            return intel;            
        }
        
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelStarIntel = xmldoc.CreateElement("StarIntel");
            
            Global.SaveData(xmldoc, xmlelStarIntel, "Age", Age.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Global.SaveData(xmldoc, xmlelStarIntel, "IntelAmount", IntelAmount.ToString());

            xmlelStarIntel.AppendChild(base.ToXml(xmldoc));

            return xmlelStarIntel;   
        }
    }
}
