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

namespace Nova.Common.Components
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml;
    using Nova.Common.Converters;

    /// <summary>
    /// The definition of the individual modules that make up a hull.
    /// These are the slots which define what components may be fitted.
    /// </summary>
    ///
    [Serializable]
    [TypeConverter(typeof(HullModuleConverter))]
    public class HullModule : ICloneable
    {
        private int componentCount;
        public Component AllocatedComponent;
        public int CellNumber = -1;
        public int ComponentMaximum = 1;
        public string ComponentType;
        
        public int ComponentCount
        {
            get
            {
                if (componentCount == 0 && AllocatedComponent == null)
                {
                    return 0;
                }
                else if (componentCount == 0 && AllocatedComponent != null)
                {
                    return 1;
                }
                else
                {
                    return componentCount;
                }
            }
            set
            {
                componentCount = value;
            }
        }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public HullModule() 
        { 
        }
        

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing">The existing <see cref="HullModule"/> to copy.</param>
        public HullModule(HullModule copy)
        {
            AllocatedComponent = copy.AllocatedComponent;
            CellNumber = copy.CellNumber;
            ComponentCount = copy.ComponentCount;
            ComponentMaximum = copy.ComponentMaximum;
            ComponentType = copy.ComponentType;
        }
        

        public HullModule(int cellNumber, int componentMaximum, int componentCount, string componentType, string componentName)
        {
            CellNumber = cellNumber;
            ComponentMaximum = componentMaximum;
            ComponentCount = componentCount;
            ComponentType = componentType;
            AllocatedComponent = new Component();
            AllocatedComponent.Name = componentName;
        }


        /// <summary>
        /// Implement the ICloneable interface so modules can be cloned.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new HullModule(this);
        }
        
        /// <summary>
        /// Removes all allocated components on this module.
        /// </summary>
        public void Empty()
        {
            AllocatedComponent = null;
            ComponentCount = 0;            
        }
        

        /// <summary>
        /// Load: initializing Constructor from an xml node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> named "Module" within a "Property" node with Type=="Hull" 
        /// in a Nova component definition file (xml document).
        /// </param>
        public HullModule(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "cellnumber":
                            CellNumber = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "componentmaximum":
                            ComponentMaximum = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "componentcount":
                            ComponentCount = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "componenttype":
                            ComponentType = ((XmlText)subnode.FirstChild).Value;
                            break;

                        case "allocatedcomponent":
                            AllocatedComponent = new Component();
                            AllocatedComponent.Name = ((XmlText)subnode.FirstChild).Value;
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
        /// Serialize a <see cref="HullModule"/> to xml.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An XmlElement representation of the HullModule.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelModule = xmldoc.CreateElement("Module");

            // CellNumber
            XmlElement xmlelCellNumber = xmldoc.CreateElement("CellNumber");
            XmlText xmltxtCellNumber = xmldoc.CreateTextNode(this.CellNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelCellNumber.AppendChild(xmltxtCellNumber);
            xmlelModule.AppendChild(xmlelCellNumber);

            // ComponentCount
            Global.SaveData(xmldoc, xmlelModule, "ComponentCount", ComponentCount.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (ComponentCount > 0)
            {
                // Allocated Component
                if (AllocatedComponent != null && AllocatedComponent.Name != null)
                {
                    Global.SaveData(xmldoc, xmlelModule, "AllocatedComponent", AllocatedComponent.Name);
                }
                else
                {
                    Report.Error("Error saving hull module: data is inconsistent.");
                    throw new SerializationException("Error saving hull module: data is inconsistent.");
                }
            }

            // ComponentMaximum
            XmlElement xmlelComponentMaximum = xmldoc.CreateElement("ComponentMaximum");
            XmlText xmltxtComponentMaximum = xmldoc.CreateTextNode(this.ComponentMaximum.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelComponentMaximum.AppendChild(xmltxtComponentMaximum);
            xmlelModule.AppendChild(xmlelComponentMaximum);

            // ComponentType
            XmlElement xmlelComponentType = xmldoc.CreateElement("ComponentType");
            XmlText xmltxtComponentType = xmldoc.CreateTextNode(this.ComponentType);
            xmlelComponentType.AppendChild(xmltxtComponentType);
            xmlelModule.AppendChild(xmlelComponentType);

            return xmlelModule;
        }
    }
}