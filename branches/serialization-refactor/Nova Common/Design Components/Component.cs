// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale 2009
//
// This file defines all of the component types known (engines, scanners, etc.)
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace NovaCommon
{


// ============================================================================
// Component class defining features common to all component types (e.g.
// Mass, cost, etc.). One or more specific component properties are contained 
// in the Properties collection.
// ============================================================================

   [Serializable]
   public class Component : Item
   {
      public TechLevel RequiredTech   = new TechLevel();
      public Image     ComponentImage = null;
      public String    ImageFile      = null;
      public String    Description    = null;
      public RaceRestriction Restrictions = new RaceRestriction();
      public Dictionary<String, ComponentProperty> Properties = null;
      // This is the list of all Compont.Properties keys for the above dictionary.
      // Note that these are not the Component.Type, but the ComponentProperty.Type.
      // They are needed here for the component to locate or load its properties
      // into the dictionary.
      public static String[] propertyKeys =
      {   
          "Armor", "Beam Deflector", "Bomb", "Battle Movement", "Capacitor", "Cargo", "Cloak", 
          "Colonizer", "Computer", "Defense", "Deflector", "Energy Dampener", "Engine", "Fuel",
          "Gate", "Hull", "Hull Affinity", "Jammer", "Mass Driver", "Mine Layer", 
          "Mine Layer Efficiency", "Robot",
          "Orbital Adjuster", "Radiation", "Scanner", "Shield", "Tachyon Detector", "Terraforming",
          "Transport Ships Only", "Weapon"

      };
       
// ============================================================================
// Construction
// ============================================================================

      public Component()
      {
          Properties = new Dictionary<string, ComponentProperty>();
      }


// ============================================================================
// Copy (initialising) constructor.
// ============================================================================

      public Component(Component existing) : base(existing as Item)
      {
         this.Properties = new Dictionary<string, ComponentProperty>();
         this.RequiredTech   = new TechLevel(existing.RequiredTech);
         this.ComponentImage = existing.ComponentImage;
         this.ImageFile = existing.ImageFile;
         this.Description    = existing.Description;
         foreach (String key in existing.Properties.Keys)
         {
             this.Properties.Add(key, (ComponentProperty)((ComponentProperty)(existing.Properties[key])).Clone());
         }
         if (existing.Restrictions != null)
             this.Restrictions = new RaceRestriction(existing.Restrictions);
         else
             this.Restrictions = null;
      }

      /// ============================================================================
      /// <summary> Check if this coponent is available to some race.</summary>
      /// <param name="race">The race to test for availability.</param>
      /// <returns>true if the component is available to this race.</returns>
      /// ============================================================================
      public bool CheckAvailability(Race race)
      {
          foreach (String trait in AllTraits.TraitKeys)
          {
              bool hasTrait = race.HasTrait(trait);
              RaceAvailability availability = this.Restrictions.Availability(trait);
              if (availability == RaceAvailability.not_available && hasTrait) return false;
              if (availability == RaceAvailability.required && !hasTrait) return false;
          }
          return true;
      }

// ============================================================================
// Construction from an Xml node
// Precondition - node is a "Component" node in a Nova component data save file in xml format. 
// ============================================================================

      
      public Component(XmlNode node)
      {
          Properties = new Dictionary<string, ComponentProperty>();

          XmlNode subnode = node.FirstChild;
          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "name": 
                          this.Name = ((XmlText)subnode.FirstChild).Value; 
                          break;
                      case "type": 
                          this.Type = ((XmlText)subnode.FirstChild).Value; 
                          break;
                      case "mass":
                          this.Mass = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture); 
                          break;
                      case "resource": 
                          this.Cost = new Resources(subnode); 
                          break;
                      case "tech": 
                          this.RequiredTech = new TechLevel(subnode); 
                          break;
                      case "description":
                          XmlText xmltxtDescription = (XmlText)subnode.FirstChild;
                          if (xmltxtDescription != null)
                              this.Description = xmltxtDescription.Value; 
                          break;
                      case "race_restrictions": 
                          this.Restrictions = new RaceRestriction(subnode); 
                          break;
                      case "image":
                          {

                              // Paths are always stored in external files using forward slashes.
                              this.ImageFile = ((XmlText)subnode.FirstChild).Value.Replace('/', Path.DirectorySeparatorChar);
                              // relative or absolute path? we normally store the relative path but will handle loading either incase the file has been manually modified.
                              try
                              {
                                  FileInfo info = new FileInfo(this.ImageFile);
                                  if (info.Exists)
                                  {
                                      // was absolute, so keep as is and load up the image
                                      this.ComponentImage = new Bitmap(this.ImageFile);
                                  }
                                  else
                                  {
                                      // guess it is relative, so convert
                                      if (AllComponents.Graphics == "" || AllComponents.Graphics == "?")
                                      {
                                          // All atempts to locate the graphics have failed, so skip them.
                                      }
                                      else
                                      {
                                          this.ImageFile = Path.Combine(AllComponents.Graphics, this.ImageFile);
                                          info = new FileInfo(this.ImageFile);
                                      }
                                      if (info.Exists)
                                      {
                                          // now we have an absolute path, load the image
                                          this.ComponentImage = new Bitmap(this.ImageFile);

                                      }
                                      else
                                      {
                                          Report.Error("Unable to locate the image file " + this.ImageFile);
                                      }
                                  }
                              }
                              catch (System.NotSupportedException)
                              {
                                  // The path doesn't make sense, maybe it wasn't relative.
                                  // Don't change anything and don't load the image
                                  this.ImageFile = ((XmlText)subnode.FirstChild).Value.Replace('/', Path.DirectorySeparatorChar);
                                  this.ComponentImage = null;
                                  Report.Error("Unable to locate the image file " + this.ImageFile);
                              }


                              break;
                          }

                      case "property":
                          {
                              // Load the property. It may be of any type (Bomb, IntegerProperty, Hull, etc), so
                              // check the save file first to determine what to load, and use the appropriate constructor.
                              String propertyType = ((XmlText)subnode.SelectSingleNode("Type").FirstChild).Value;
                              ComponentProperty newProperty = null;
                              switch (propertyType.ToLower())
                              {
                                  case "armor":
                                      {
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }
                                  case "battle movement":
                                      {
                                          newProperty = new DoubleProperty(subnode);
                                          break;
                                      }
                                  case "beam deflector":
                                      {
                                          newProperty = new ProbabilityProperty(subnode);
                                          break;
                                      }
                                  case "bomb":
                                      {
                                          newProperty = new Bomb(subnode);
                                          break;
                                      }
                                  case "capacitor":
                                      {
                                          newProperty = new CapacitorProperty(subnode);
                                          break;
                                      }
                                  case "cloak":
                                      {
                                          newProperty = new ProbabilityProperty(subnode);
                                          break;
                                      }
                                  case "defense":
                                      {
                                          newProperty = new Defense(subnode);
                                          break;
                                      }
                                  case "energy dampener":
                                      {
                                          newProperty = new DoubleProperty(subnode);
                                          break;
                                      }

                                  case "cargo":
                                      {
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }
                                  case "colonizer":
                                      {
                                          newProperty = new Colonizer(subnode);
                                          break;
                                      }
                                  case "computer":
                                      {
                                          newProperty = new Computer(subnode);
                                          break;
                                      }
                                  case "engine":
                                      {
                                          newProperty = new Engine(subnode);
                                          break;
                                      }
                                  case "fuel":
                                      {
                                          newProperty = new Fuel(subnode);
                                          break;
                                      }
                                  case "gate":
                                      {
                                          newProperty = new Gate(subnode);
                                          break;
                                      }
                                  case "hull":
                                      {
                                          newProperty = new Hull(subnode);
                                          break;
                                      }
                                  case "hull affinity":
                                      {
                                          newProperty = new HullAffinity(subnode);
                                          break;
                                      }
                                  case "jammer":
                                      {
                                          newProperty = new ProbabilityProperty(subnode);
                                          break;
                                      }

                                  case "mass driver":
                                      {
                                          newProperty = new MassDriver(subnode);
                                          break;
                                      }
                                  case "mine layer":
                                      {
                                          newProperty = new MineLayer(subnode);
                                          break;
                                      }
                                  case "mine layer efficiency":
                                      {
                                          newProperty = new DoubleProperty(subnode);
                                          break;
                                      }
                                  case "mining robot":
                                      {
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }
                                  case "orbital adjuster":
                                      {
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }
                                  case "radiation":
                                      {
                                          newProperty = new Radiation(subnode);
                                          break;
                                      }
                                  case "shield":
                                      {
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }
                                  case "scanner":
                                      {
                                          newProperty = new Scanner(subnode);
                                          break;
                                      }
                                  case "tachyon detector":
                                      {
                                          newProperty = new ProbabilityProperty(subnode);
                                          break;
                                      }
                                  case "terraform":
                                      {
                                          newProperty = new Terraform(subnode);
                                          break;
                                      }
                                  case "transport ships only":
                                      {
                                          newProperty = new SimpleProperty(subnode);
                                          break;
                                      }
                                  case "weapon":
                                      {
                                          newProperty = new Weapon(subnode);
                                          break;
                                      }
                                  default:
                                      {
                                          // it is an error to arrive here, but try to recover using an integer property
                                          Report.Error("Component property type " + propertyType + " not recognised, using default constructor");
                                          newProperty = new IntegerProperty(subnode);
                                          break;
                                      }

                              }//switch on property
                              if (newProperty != null)
                              {
                                  this.Properties.Add(propertyType, newProperty);
                              }
                              break;
                          } //case "property"

                      }// switch on subnode.Name

              }//try


              catch (Exception e)
              {
                  Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
              }
              
                  subnode = subnode.NextSibling;
          }//while subnode != null
          // Report.Information("Name = '" + this.Name + "' Type = '" + this.Type + "'");
      }// component constructor

       public override void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Component
       }

       public override void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Component");

           writer.WriteElementString("Name", Name);
           writer.WriteElementString("Type", Type);
           writer.WriteElementString("Mass", Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));
           Cost.WriteXml(writer);
           RequiredTech.WriteXml(writer);
           if (Description != null) writer.WriteElementString("Description", Description);
           Restrictions.WriteXml(writer);
           // Image - convert the ImageFile to a relative path, so this program runs in other locations
           writer.WriteElementString("Image", Global.EvaluateRelativePath(AllComponents.Graphics, ImageFile).Replace(Path.DirectorySeparatorChar, '/'));

           // Properties
           foreach (String key in Properties.Keys)
           {
               // Property written here instead of inside each property object
               // to allow us to append the Type element.
               writer.WriteStartElement("Property");

               Properties[key].WriteXml(writer);

               writer.WriteElementString("Type", key);
               writer.WriteEndElement();
           }

           writer.WriteEndElement();
       }
   }
}


