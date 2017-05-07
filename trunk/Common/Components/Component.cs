#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011, 2012 The Stars-Nova Project
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
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Component class defining features common to all component types (e.g.
    /// Mass, cost, etc.). One or more specific component properties are contained 
    /// in the Properties collection.
    /// </summary>
    [Serializable]
    public class Component : Item
    {
        public TechLevel RequiredTech = new TechLevel();
        public Image ComponentImage;
        public string ImageFile = string.Empty;
        public string Description = string.Empty;
        public RaceRestriction Restrictions = new RaceRestriction();

        public Dictionary<string, ComponentProperty> Properties;

        // This is the list of all Compont.Properties keys for the above dictionary.
        // Note that these are not the Component.Type, but the ComponentProperty.Type.
        // They are defined here such that the component can locate or load its properties
        // into the dictionary.
        public static string[] PropertyKeys =
        {   
          "Armor", "Beam Deflector", "Bomb", "Battle Movement", "Capacitor", "Cargo", "Cloak", 
          "Colonizer", "Computer", "Defense", "Deflector", "Energy Dampener", "Engine", "Fuel",
          "Gate", "Hull", "Hull Affinity", "Jammer", "Mass Driver", "Mine Layer", 
          "Mine Layer Efficiency", "Robot",
          "Orbital Adjuster", "Radiation", "Scanner", "Shield", "Tachyon Detector", "Terraforming",
          "Transport Ships Only", "Weapon"
        };
        
        /// <summary>
        /// The mass of the component (in kT).
        /// </summary>
        public int Mass;

        /// <summary>
        /// The resource cost to build (germanium, ironium, etc.).
        /// </summary>
        public Resources Cost = new Resources();        

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Component()
        {
            Properties = new Dictionary<string, ComponentProperty>();
        }

        
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="existing"></param>
        public Component(Component copy)
            : base(copy)
        {
            Mass = copy.Mass;
            Cost = new Resources(copy.Cost);
            Properties = new Dictionary<string, ComponentProperty>();
            RequiredTech = new TechLevel(copy.RequiredTech);
            ComponentImage = copy.ComponentImage;
            ImageFile = copy.ImageFile;
            Description = copy.Description;
            foreach (string key in copy.Properties.Keys)
            {
                Properties.Add(key, (ComponentProperty)copy.Properties[key].Clone());
            }
            if (copy.Restrictions != null)
            {
                Restrictions = new RaceRestriction(copy.Restrictions);
            }
            else
            {
                Restrictions = null;
            }
        }


        /// <summary>
        /// Load from XML: Initializing constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova component definition file (xml document).
        /// </param>
        public Component(XmlNode node)
            : base(node)
        {
            Properties = new Dictionary<string, ComponentProperty>();

            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "mass":
                            Mass = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;                            
                        case "cost":
                            Cost = new Resources(mainNode);
                            break;
                        case "tech":
                            RequiredTech = new TechLevel(mainNode);
                            break;
                        case "description":
                            XmlText xmltxtDescription = (XmlText)mainNode.FirstChild;
                            if (xmltxtDescription != null)
                            {
                                this.Description = xmltxtDescription.Value;
                            }
                            break;
                        case "race_restrictions":
                            Restrictions = new RaceRestriction(mainNode);
                            break;
                        case "image":
                            {
                                // Paths are always stored in external files using forward slashes.
                                ImageFile = mainNode.FirstChild.Value;
                                ImageFile = ImageFile.Replace('/', Path.DirectorySeparatorChar);


                                // relative or absolute path? we normally store the relative path but will handle loading either incase the file has been manually modified.
                                try
                                {
                                    FileInfo info = new FileInfo(ImageFile);
                                    if (info.Exists)
                                    {
                                        // was absolute, so keep as is and load up the image
                                        ComponentImage = new Bitmap(ImageFile);
                                    }
                                    else
                                    {
                                        {
                                            string graphicsPath = FileSearcher.GetGraphicsPath();
                                            if (graphicsPath != null)
                                            {
                                                ImageFile = Path.Combine(graphicsPath, ImageFile);
                                                info = new FileInfo(ImageFile);
                                            }
                                        }
                                        
                                        if (info.Exists)
                                        {
                                            // now we have an absolute path, load the image
                                            ComponentImage = new Bitmap(ImageFile);
                                        }
                                        else
                                        {
                                            // No further action. FileSearcher will report an error (once only) if the graphics are not available.
                                        }
                                    }
                                }
                                catch (System.NotSupportedException)
                                {
                                    // The path doesn't make sense, maybe it wasn't relative.
                                    // Don't change anything and don't load the image
                                    ImageFile = mainNode.FirstChild.Value.Replace('/', Path.DirectorySeparatorChar);
                                    ComponentImage = null;
                                    Report.Error("Unable to locate the image file " + ImageFile);
                                }


                                break;
                            }

                        case "property":
                            {
                                // Load the property. It may be of any type (Bomb, IntegerProperty, Hull, etc), so
                                // check the save file first to determine what to load, and use the appropriate constructor.
                                string propertyType = mainNode.SelectSingleNode("Type").FirstChild.Value;
                                ComponentProperty newProperty;
                                switch (propertyType.ToLower())
                                {
                                    case "armor":
                                        {
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                    case "battle movement":
                                        {
                                            newProperty = new DoubleProperty(mainNode);
                                            break;
                                        }
                                    case "beam deflector":
                                        {
                                            newProperty = new ProbabilityProperty(mainNode);
                                            break;
                                        }
                                    case "bomb":
                                        {
                                            newProperty = new Bomb(mainNode);
                                            break;
                                        }
                                    case "capacitor":
                                        {
                                            newProperty = new CapacitorProperty(mainNode);
                                            break;
                                        }
                                    case "cloak":
                                        {
                                            newProperty = new ProbabilityProperty(mainNode);
                                            break;
                                        }
                                    case "defense":
                                        {
                                            newProperty = new Defense(mainNode);
                                            break;
                                        }
                                    case "energy dampener":
                                        {
                                            newProperty = new DoubleProperty(mainNode);
                                            break;
                                        }

                                    case "cargo":
                                        {
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                    case "colonizer":
                                        {
                                            newProperty = new Colonizer(mainNode);
                                            break;
                                        }
                                    case "computer":
                                        {
                                            newProperty = new Computer(mainNode);
                                            break;
                                        }
                                    case "engine":
                                        {
                                            newProperty = new Engine(mainNode);
                                            break;
                                        }
                                    case "fuel":
                                        {
                                            newProperty = new Fuel(mainNode);
                                            break;
                                        }
                                    case "gate":
                                        {
                                            newProperty = new Gate(mainNode);
                                            break;
                                        }
                                    case "hull":
                                        {
                                            newProperty = new Hull(mainNode);
                                            break;
                                        }
                                    case "hull affinity":
                                        {
                                            newProperty = new HullAffinity(mainNode);
                                            break;
                                        }
                                    case "jammer":
                                        {
                                            newProperty = new ProbabilityProperty(mainNode);
                                            break;
                                        }

                                    case "mass driver":
                                        {
                                            newProperty = new MassDriver(mainNode);
                                            break;
                                        }
                                    case "mine layer":
                                        {
                                            newProperty = new MineLayer(mainNode);
                                            break;
                                        }
                                    case "mine layer efficiency":
                                        {
                                            newProperty = new DoubleProperty(mainNode);
                                            break;
                                        }
                                    case "mining robot":
                                        {
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                    case "orbital adjuster":
                                        {
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                    case "radiation":
                                        {
                                            newProperty = new Radiation(mainNode);
                                            break;
                                        }
                                    case "shield":
                                        {
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                    case "scanner":
                                        {
                                            newProperty = new Scanner(mainNode);
                                            break;
                                        }
                                    case "tachyon detector":
                                        {
                                            newProperty = new ProbabilityProperty(mainNode);
                                            break;
                                        }
                                    case "terraform":
                                        {
                                            newProperty = new Terraform(mainNode);
                                            break;
                                        }
                                    case "transport ships only":
                                        {
                                            newProperty = new SimpleProperty(mainNode);
                                            break;
                                        }
                                    case "weapon":
                                        {
                                            newProperty = new Weapon(mainNode);
                                            break;
                                        }
                                    default:
                                        {
                                            // it is an error to arrive here, but try to recover using an integer property
                                            Report.Error("Component property type " + propertyType + " not recognised, using default constructor");
                                            newProperty = new IntegerProperty(mainNode);
                                            break;
                                        }
                                }
                                if (newProperty != null)
                                {
                                    this.Properties.Add(propertyType, newProperty);
                                }
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }
                mainNode = mainNode.NextSibling;
            }
        }
        
        
        /// <summary>
        /// Save: Serialize this component to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelComponent = xmldoc.CreateElement("Component");

            xmlelComponent.AppendChild(base.ToXml(xmldoc));
            
            Global.SaveData(xmldoc, xmlelComponent, "Mass", Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            xmlelComponent.AppendChild(Cost.ToXml(xmldoc));

            // Tech
            xmlelComponent.AppendChild(this.RequiredTech.ToXml(xmldoc));

            // Description
            if (Description != null)
            {
                Global.SaveData(xmldoc, xmlelComponent, "Description", Description);
            }

            // Race Restrictions
            xmlelComponent.AppendChild(this.Restrictions.ToXml(xmldoc));

            // Image - convert the ImageFile to a relative path, so this program runs in other locations
            XmlElement xmlelImage = xmldoc.CreateElement("Image");
            string graphicsPath = FileSearcher.GetGraphicsPath();
            try
            {
                XmlText xmltxtImage = xmldoc.CreateTextNode(Global.EvaluateRelativePath(FileSearcher.GetGraphicsPath(), this.ImageFile).Replace(Path.DirectorySeparatorChar, '/')); // Paths are always stored in external files using forward slashes.
                xmlelImage.AppendChild(xmltxtImage);
                xmlelComponent.AppendChild(xmlelImage);
            }
            catch (Exception)
            {
                // could be a null reference (graphicsPath == null) or a file IO Exception
                // Nova! does not know where the graphics are, they probably have not been loaded. The image location will not be saved.
            }

            // Properties
            foreach (string key in this.Properties.Keys)
            {
                XmlElement xmlelPropertyType = xmldoc.CreateElement("Type");
                XmlText xmltxtPropertyType = xmldoc.CreateTextNode(key);
                xmlelPropertyType.AppendChild(xmltxtPropertyType);

                XmlElement xmlelProperty = this.Properties[key].ToXml(xmldoc);
                xmlelProperty.AppendChild(xmlelPropertyType);

                xmlelComponent.AppendChild(xmlelProperty);
            }

            return xmlelComponent;
        }
    }
}


