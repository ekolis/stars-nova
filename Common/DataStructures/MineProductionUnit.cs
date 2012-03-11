#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 stars-nova
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
    /// Implementation of ProductionUnit for constructing one mine.
    /// </summary>
    public class MineProductionUnit : IProductionUnit
    {
        private Resources cost;
        private Resources remainingCost; 
        
        public Resources Cost
        {
            get {return cost;}
        }
                
        public Resources RemainingCost
        {
            get {return remainingCost;}
        }
        
        public string Name
        {
            get {return "Mine";}
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="star">Star on which the mine is to be constructed.</param>
        public MineProductionUnit(Race race)
        {
            cost = race.GetMineResources();
            remainingCost = cost;
        }
        
        
        /// <summary>
        /// Load: Read in a ProductionUnit from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public MineProductionUnit(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "cost":
                            cost = new Resources(subnode.FirstChild);
                            break;
                            
                        case "remainingcost":
                            remainingCost = new Resources(subnode.FirstChild);
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
        /// Return true if this item will be skipped in production.
        /// </summary>
        public bool IsSkipped(Star star)
        {
            if (star.Mines >= star.GetOperableMines())
            {
                return true;
            }

            if (star.ResourcesOnHand.Energy == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Produce the mine.
        /// </summary>
        public bool Construct(Star star)
        {
            // Partial build.
            if (!(star.ResourcesOnHand >= remainingCost))
            {
                // used to temporarily store the amount of resources we are short
                Resources lacking = remainingCost - star.ResourcesOnHand; 
                
                // Normalized; 1.0 = 100%
                double percentBuildable = 1.0;
    
                // determine which resource limits production (i.e. is able to complete the smallest percentage of production)
                if (percentBuildable > (1 - ((double)lacking.Energy / remainingCost.Energy)) && lacking.Energy > 0)
                {
                    percentBuildable = 1 - ((double)lacking.Energy / remainingCost.Energy);
                }
    
                // What we spend on the partial builld.
                star.ResourcesOnHand -= remainingCost * percentBuildable;    
                remainingCost -= remainingCost * percentBuildable;
                
                return false;                
            }
            else // Fully build this unit.
            {
                star.ResourcesOnHand -= remainingCost;
                star.Mines++;
                return true;
            }  
        }
        
                
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelUnit = xmldoc.CreateElement("MineUnit");
            
            XmlElement xmlelCost = xmldoc.CreateElement("Cost");
            xmlelCost.AppendChild(cost.ToXml(xmldoc));
            xmlelUnit.AppendChild(xmlelCost);
            
            XmlElement xmlelRemCost = xmldoc.CreateElement("RemainingCost");
            xmlelRemCost.AppendChild(remainingCost.ToXml(xmldoc));
            xmlelUnit.AppendChild(xmlelRemCost);
            
            return xmlelUnit;
        }
    }
}
