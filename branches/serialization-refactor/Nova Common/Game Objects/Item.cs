// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Base class for most game items.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Drawing;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{

// ===========================================================================
// Item class. It is used as the base class for most game items. It consists
// of the following members:
// 
// Cost     The resource cost to build (germanium, ironium, etc.).
// Mass     The mass of the item (in kT).
// Name     The name of the derived item, for example the name of a star.
// Owner    The race name of the owner of this item (null if no owner). 
// Type     The type of the derived item (e.g. "ship", "star", etc.
// Position Position of the Item (if any)
// ===========================================================================

   [Serializable]
   public class Item : IXmlSerializable
   {
      public  int       Mass     = 0;
      public  Resources Cost     = new Resources();
      public  string    Name     = null;
      public  string    Owner    = null;
      public  string    Type     = null;
      public  Point     Position = new Point(0, 0);
   

// ===========================================================================
// Default Construction
// ===========================================================================

      public Item()
      {
      }


// ===========================================================================
// Copy (initialising) constructor
// ===========================================================================

      public Item(Item existing)
      {
         if (existing == null) return;

         this.Mass     = existing.Mass;
         this.Name     = existing.Name;
         this.Owner    = existing.Owner;
         this.Type     = existing.Type;
         this.Position = existing.Position;
         this.Cost     = new Resources(existing.Cost);
      }

       /// <summary>
       /// Load: Initialising constructor from an XmlNode representing the Item (from a save file).
       /// </summary>
       /// <param name="node">An XmlNode representing the Item</param>
      public Item(XmlNode node)
      {
          // There are two acceptable entry points to this constructor. Either node is 
          // an Item node, or it is a parent node of an Item node. The second case is allowed
          // for loading objects which inherit from Item.

          // Check if this is an Item node, or a parent of an Item
          XmlNode itemNode;
          if (node.Name.ToLower() == "item")
          {
                 itemNode = node;
          }
          else
          {
           
              itemNode = node.SelectSingleNode("Item");
              if (itemNode == null)
              {
                  Report.FatalError("Item.cs: Item(XmlNode node) - could not find Item node.");
                  return;
              }
          }

          XmlNode subnode = itemNode.FirstChild;

          while (subnode != null)
          {
              try
              {
                  switch (subnode.Name.ToLower())
                  {
                      case "mass":
                          Mass = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          break;
                      case "name":
                          Name = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "owner":
                          Owner = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "type":
                          Type = ((XmlText)subnode.FirstChild).Value;
                          break;
                      case "position":
                          Position.X = int.Parse(((subnode.SelectSingleNode("X")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                          Position.Y = int.Parse(((subnode.SelectSingleNode("Y")).FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                              break;
                      case "resource":
                          Cost = new Resources(subnode);
                          break;
                  }
              }
                  
              catch (Exception e)
              {
                  Report.Error(e.Message + " \n Details: \n " + e.ToString());
              }
                   
              subnode = subnode.NextSibling;
          }

      }


// ============================================================================
// Return a key for use in hash tables to locate items.
// ============================================================================

      public virtual string Key 
      {
         get { return this.Owner + "/" + this.Name; }
      }

       public virtual XmlSchema GetSchema()
       {
           return null;
       }

       public virtual void ReadXml(XmlReader reader)
       {
           throw new NotImplementedException(); // TODO XML deserialization of Item
       }

       public virtual void WriteXml(XmlWriter writer)
       {
           writer.WriteStartElement("Item");

           if (Mass != 0) writer.WriteElementString("Mass", Mass.ToString(System.Globalization.CultureInfo.InvariantCulture));
           if (Cost != null) Cost.WriteXml(writer);
           if (Name != null) writer.WriteElementString("Name", Name);
           if (Owner != null) writer.WriteElementString("Owner", Owner);
           if (Type != null) writer.WriteElementString("Type", Type);

           if (Position.X != 0 || Position.Y != 0)
           {
               writer.WriteStartElement("Position");
               writer.WriteElementString("X", Position.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
               writer.WriteElementString("Y", Position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
               writer.WriteEndElement();
           }

           writer.WriteEndElement();
       }
   }
}
