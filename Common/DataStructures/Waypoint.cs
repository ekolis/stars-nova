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
// See Waypoint class summary.
// ===========================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Drawing;
    using System.Xml;
    using System.Xml.Serialization;

    using Nova.Common.DataStructures;

    public enum WaypointTask
    {
        None,
        Colonise,
        Invade,
        Scrap,
        LayMines,
        UnloadCargo,
        LoadCargo,
    }
    /// <summary>
    /// Waypoints have a position (i.e. where to go), a destination description
    /// (e.g. a star name), a speed to go there and a task to do on arrival (e.g. 
    /// colonise).
    /// </summary>
    [Serializable]
    public class Waypoint
    {
        public NovaPoint Position;
        public string Destination;
        public int WarpFactor = 6;
        public WaypointTask Task = WaypointTask.None;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Waypoint()
        {
        }

        #region Load Save Xml

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">A node is a "Waypoint" node Nova save file (xml document).
        /// </param>
        public Waypoint(XmlNode node)
        {
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
                            SetTask(((XmlText)subnode.FirstChild).Value);
                            break;

                        case "warpfactor":
                            WarpFactor = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "position":
                            Position = new NovaPoint(subnode);
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

        /// <summary>
        /// Save: Serialise this Waypoint to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Waypoint.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelWaypoint = xmldoc.CreateElement("Waypoint");

            Global.SaveData(xmldoc, xmlelWaypoint, "Destination", this.Destination);
            Global.SaveData(xmldoc, xmlelWaypoint, "Task", this.GetTask());
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

        public string GetTask()
        {
            if (Task == WaypointTask.UnloadCargo)
            {
                return "Unload Cargo";
            }
            else if (Task == WaypointTask.LayMines)
            {
                return "Lay Mines";
            }
            else
            {
                return Enum.GetName(typeof(WaypointTask), Task);
            }
        }
        public void SetTask(string task)
        {
            if (task == "Colonise")
            {
                Task = WaypointTask.Colonise;
            }
            else if (task == "Invade")
            {
                Task = WaypointTask.Invade;
            }
            else if (task == "Scrap")
            {
                Task = WaypointTask.Scrap;
            }
            else if (task == "Unload Cargo")
            {
                Task = WaypointTask.UnloadCargo;
            }
            else if (task == "Lay Mines")
            {
                Task = WaypointTask.LayMines;
            }
        }
    }
}
