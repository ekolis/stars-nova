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
    /// Implementation of ProductionUnit for factory building.
    /// </summary>
    public class FactoryProductionUnit : IProductionUnit
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
            get {return "Factory";}
        }
        
        
        /// <summary>
        /// initializing constructor.
        /// </summary>
        /// <param name="star">Star on which the factory is to be constructed.</param>
        public FactoryProductionUnit(Race race)
        {
            cost = race.GetFactoryResources();
            remainingCost = cost;
        }
        
        
        /// <summary>
        /// Load: Read in a ProductionUnit from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of a ProductionUnit</param>
        public FactoryProductionUnit(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                switch (mainNode.Name.ToLower())
                {
                    case "cost":
                        cost = new Resources(mainNode);
                        break;

                    case "remainingcost":
                        remainingCost = new Resources(mainNode);
                        break;
                }

                mainNode = mainNode.NextSibling;
            }
        }
        
        
        /// <summary>
        /// Returns true if this production item will be skipped.
        /// </summary>
        public bool IsSkipped(Star star)
        {
            if (star.Factories >= star.GetOperableFactories())
            {
                return true;
            }

            if (star.ResourcesOnHand.Energy <= 0 || star.ResourcesOnHand.Germanium <= 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Construct one factory.
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
                if (percentBuildable > (1 - ((double)lacking.Germanium / remainingCost.Germanium)) && lacking.Germanium > 0)
                {
                    percentBuildable = 1 - ((double)lacking.Germanium / remainingCost.Germanium);
                }
    
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
                star.Factories++;
                return true;
            }  
        }
        
                
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelUnit = xmldoc.CreateElement("FactoryUnit");
            
            xmlelUnit.AppendChild(cost.ToXml(xmldoc, "Cost"));
            
            xmlelUnit.AppendChild(remainingCost.ToXml(xmldoc, "RemainingCost"));
            
            return xmlelUnit;
        }
    }
}
