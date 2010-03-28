// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This file defines the Hull Module component. A hull module is on of the 
// individuale grid squares that makes up a Hull.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.Serialization;

namespace NovaCommon
{

    // ============================================================================
    // The definition of the individual modules that make up a hull.
    // These are the slots which define what components may be fitted.
    // ============================================================================

    [Serializable]
    public class HullModule : ICloneable
    {
        public Component AllocatedComponent = null;
        public int CellNumber = -1;
        public int ComponentCount = 0;
        public int ComponentMaximum = 1;
        public string ComponentType = null;

        // ============================================================================
        // Construction
        // ============================================================================

        public HullModule()
        {
        }


        // ============================================================================
        // Copy constructor 
        // ============================================================================

        public HullModule(HullModule existing)
        {
            AllocatedComponent = existing.AllocatedComponent;
            CellNumber = existing.CellNumber;
            ComponentCount = existing.ComponentCount;
            ComponentMaximum = existing.ComponentMaximum;
            ComponentType = existing.ComponentType;

        }

        //============================================================================
        // Implement the ICloneable interface so modules can be cloned.
        //============================================================================
        public object Clone()
        {
            return new HullModule(this);
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is named "Module" within a "Property" node with Type=="Hull" 
        //               in a Nova compenent definition file (xml document).
        // ============================================================================
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


        // ============================================================================
        // Return an XmlElement representation of the HullModule
        // ============================================================================
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelModule = xmldoc.CreateElement("Module");

            // CellNumber
            XmlElement xmlelCellNumber = xmldoc.CreateElement("CellNumber");
            XmlText xmltxtCellNumber = xmldoc.CreateTextNode(this.CellNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelCellNumber.AppendChild(xmltxtCellNumber);
            xmlelModule.AppendChild(xmlelCellNumber);
            // ComponentCount
            XmlElement xmlelComponentCount = xmldoc.CreateElement("ComponentCount");
            XmlText xmltxtComponentCount = xmldoc.CreateTextNode(this.ComponentCount.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelComponentCount.AppendChild(xmltxtComponentCount);
            xmlelModule.AppendChild(xmlelComponentCount);
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
            // Allocated Component
            if (AllocatedComponent != null && AllocatedComponent.Name != null) Global.SaveData(xmldoc, xmlelModule, "AllocatedComponent", AllocatedComponent.Name);

            return xmlelModule;
        }
    }
}