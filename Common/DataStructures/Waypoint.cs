#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// The details of an individual Waypoint.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Runtime.Serialization;

namespace Nova.Common
{

    /// <summary>
    /// Waypoints have a position (i.e. where to go), a destination description
    /// (e.g. a star name), a speed to go there and a task to do on arrival (e.g. 
    /// colonise).
    /// </summary>
    [Serializable]
    public class Waypoint
    {
        public Point Position;
        public string Destination = null;
        public int WarpFactor = 6;
        public String Task = "None";

        /// <summary>
        /// Default constructor
        /// </summary>
        public Waypoint()
        {
        }

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">node is a "Waypoint" node Nova save file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public Waypoint(XmlNode node)
        {
            XmlSerializer x = new XmlSerializer(typeof(Waypoint));

            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "destination":
                            Destination = ((XmlText)subnode.FirstChild).Value;
                            break;

                        case "task":
                            Task = ((XmlText)subnode.FirstChild).Value;
                            break;

                        case "warpfactor":
                            WarpFactor = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "position":
                            Position.X = int.Parse(subnode.SelectSingleNode("X").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Position.Y = int.Parse(subnode.SelectSingleNode("Y").FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }

                subnode = subnode.NextSibling;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this Waypoint to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Waypoint</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelWaypoint = xmldoc.CreateElement("Waypoint");

            Global.SaveData(xmldoc, xmlelWaypoint, "Destination", this.Destination);
            Global.SaveData(xmldoc, xmlelWaypoint, "Task", this.Task);
            Global.SaveData(xmldoc, xmlelWaypoint, "WarpFactor", this.WarpFactor.ToString(System.Globalization.CultureInfo.InvariantCulture));
            // point
            if (Position.X != 0 || Position.Y != 0)
            {
                XmlElement xmlelPoint = xmldoc.CreateElement("Position");
                Global.SaveData(xmldoc, xmlelPoint, "X", Position.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                Global.SaveData(xmldoc, xmlelPoint, "Y", Position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelWaypoint.AppendChild(xmlelPoint);
            }


            return xmlelWaypoint;
        }

        #endregion

    }
}
