#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;
    
    /// <summary>
    /// Cargo that may be carried by a ship (if it has a cargo pod).
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(CargoTypeConverter))]
    public class Cargo
    {
        /// <summary>
        /// Stores the different resources.
        /// </summary>
        private Dictionary<ResourceType, int> commodities = new Dictionary<ResourceType, int>()
        {
            { 
                ResourceType.Ironium, 0
            },
            {
                ResourceType.Boranium, 0
            },
            {
                ResourceType.Germanium, 0
            },
            {
                ResourceType.ColonistsInKilotons, 0
            } // Stored in KiloTons, not actual numbers.
        };
        
        public Dictionary<ResourceType, int> Commodities 
        {
            get 
            { 
                return commodities; 
            }
        }
        
        public int Ironium 
        {
            set 
            { 
                commodities[ResourceType.Ironium] = value; 
            }            
            
            get 
            { 
                return commodities[ResourceType.Ironium]; 
            }
        }
        
        public int Boranium 
        {
            set 
            { 
                commodities[ResourceType.Boranium] = value; 
            }   
         
            get 
            { 
                return commodities[ResourceType.Boranium]; 
            }
        }
        
        public int Germanium
        {
            set
            { 
                commodities[ResourceType.Germanium] = value; 
            }      
      
            get
            {
                return commodities[ResourceType.Germanium]; 
            }
        }
        
        /// <summary>
        /// Sets or Gets amount of Colonists in KiloTons.
        /// </summary>
        public int ColonistsInKilotons 
        {
            set
            { 
                commodities[ResourceType.ColonistsInKilotons] = value;
            }            
            get
            {
                return commodities[ResourceType.ColonistsInKilotons];
            }
        }
        
        /// <summary>
        /// Gets the amount of actual Colonists in the cargo.
        /// </summary>
        public int ColonistNumbers 
        {          
            get
            { 
                return commodities[ResourceType.ColonistsInKilotons] * Global.ColonistsPerKiloton;
            }
        }                
        
        /// <summary>
        /// Get the Mass of the cargo.
        /// </summary>
        public int Mass
        {
            get { return Ironium + Boranium + Germanium + ColonistsInKilotons; }
        }
        
        /// <summary>
        /// Provides array-like access to the different commodities via ResourceType Enum.
        /// </summary>
        public int this[ResourceType index]
        {
            get
            {
                return commodities.ContainsKey(index) ? commodities[index] : 0;
            }
            
            set
            {
                if (commodities.ContainsKey(index)) 
                {
                    commodities[index] = value;
                }
            }
        }
        
        /// <summary>
        /// Default constructor (needed if there is a copy constructor).
        /// </summary>
        public Cargo() 
        {
            Ironium = 0;
            Boranium = 0;
            Germanium = 0;
            ColonistsInKilotons = 0;
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">Cargo object to copy.</param>
        public Cargo(Cargo copy)
        {
            Ironium = copy.Ironium;
            Boranium = copy.Boranium;
            Germanium = copy.Germanium;
            ColonistsInKilotons = copy.ColonistsInKilotons;
        }

        /// <summary>
        /// Load from XML: initializing constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova xml document.
        /// </param>
        public Cargo(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "ironium":
                            {
                                Ironium = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "boranium":
                            {
                                Boranium = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "germanium":
                            {
                                Germanium = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "colonistsinkilotons":
                            {
                                ColonistsInKilotons = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }
        
        public void Add(Cargo rightCargo)
        {
            Ironium += rightCargo.Ironium;
            Boranium += rightCargo.Boranium;
            Germanium += rightCargo.Germanium;
            ColonistsInKilotons += rightCargo.ColonistsInKilotons;
        }
        
        public void Remove(Cargo rightCargo)
        {
            Ironium -= rightCargo.Ironium;
            Boranium -= rightCargo.Boranium;
            Germanium -= rightCargo.Germanium;
            ColonistsInKilotons -= rightCargo.ColonistsInKilotons;
        }
        
        /// <summary>
        /// Clears all cargo.
        /// </summary>
        public void Clear()
        {
            Ironium = 0;
            Boranium = 0;
            Germanium = 0;
            ColonistsInKilotons = 0;
        }
        
        /// <summary>
        /// Returns a Resource object containing the cargo.
        /// Colonists are excluded.
        /// </summary>
        public Resources ToResource()
        {
            return new Resources(Ironium, Boranium, Germanium, 0);
        }

        /// <summary>
        /// Save: Serialize this object to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Cargo.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCargo = xmldoc.CreateElement("Cargo");

            Global.SaveData(xmldoc, xmlelCargo, "Ironium", Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelCargo, "Boranium", Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelCargo, "Germanium", Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelCargo, "ColonistsInKilotons", ColonistsInKilotons.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelCargo;
        }
    }

    public class CargoTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Cargo cargo = value as Cargo;
            if (cargo == null)
            {
                return "0,0,0,0";
            }
            return string.Format("{0},{1},{2},{3}", cargo.Ironium, cargo.Boranium, cargo.Germanium, cargo.ColonistsInKilotons);
        }
    }
}
