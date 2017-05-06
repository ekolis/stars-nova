#region Copyright Notice
// ============================================================================
// Copyright (C) 2012 The Stars-Nova Project
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
    
    using Nova.Common.DataStructures;
    
    /// <summary>
    /// An object which has a position in Nova.
    /// </summary>
    public class Mappable : Item
    {
        /// <summary>
        /// Position of the Item (if any).
        /// </summary>
        public NovaPoint Position = new NovaPoint();
        
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Mappable()
        {
        }
        
        /// <summary>
        /// Copy (initializing) constructor. 
        /// </summary>
        /// <param name="existing"></param>
        public Mappable(Mappable existing) :
            base(existing)
        {
            if (existing == null)
            {
                return;
            }
            
            // Use a new object, no just a reference to the copy's NovaPoint
            Position = new NovaPoint(existing.Position);
        }
        
        /// <summary>
        /// Load: initializing constructor from an XmlNode representing the object (from a save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the Mappable object.</param>
        public Mappable(XmlNode node) :
            base(node)
        {
            if (node == null)
            {
                Report.FatalError("Mappable.cs: Mappable(XmlNode node) - node is null - no Mappable found.");
                return;
            }
            
            // Search for the first Mappable node in this Xml representation.
            XmlNode mapNode = null;
            while (node != null)
            {
                if ((mapNode = node.SelectSingleNode("Mappable")) != null)
                {
                    break;
                }
                
                node = node.FirstChild;
            }
            
            if (mapNode == null)
            {
                Report.FatalError("Mappable.cs: Mappable(XmlNode node) - could not find Mappable node, input file may be corrupt.");
                return;
            }
           
            XmlNode mainNode = mapNode.FirstChild;

            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "point":
                            Position = new NovaPoint(mainNode);
                            break;
                    }
                }

                catch (Exception e)
                {
                    Report.Error(e.Message + " \n Details: \n " + e.ToString());
                }

                mainNode = mainNode.NextSibling;
            }
        }
        
        /// <summary>
        /// Save: Return an XmlElement representation of the <see cref="Mappable"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Property.</returns>
        public new XmlElement ToXml(XmlDocument xmldoc)
        {          
            XmlElement xmlelMap = xmldoc.CreateElement("Mappable");
            xmlelMap.AppendChild(base.ToXml(xmldoc));
            xmlelMap.AppendChild(Position.ToXml(xmldoc));
            return xmlelMap;
        }
    }
}
