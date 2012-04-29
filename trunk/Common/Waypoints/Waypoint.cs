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

namespace Nova.Common.Waypoints
{
    using System;
    using System.Drawing;
    using System.Xml;

    using Nova.Common.DataStructures;
    
    /// <summary>
    /// Waypoints have a position (i.e. where to go), a destination description
    /// (e.g. a star name), a speed to go there and a task to do on arrival (e.g. 
    /// colonise).
    /// </summary>
    public class Waypoint
    {
        public NovaPoint Position {get; set;}
        public int WarpFactor {get; set;}
        public IWaypointTask Task {get; set;}
        public string Destination {get; set;}

        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Waypoint()
        {
            WarpFactor = 6;
            Task = new NoTask();
        }
        

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">A node is a "Waypoint" node Nova save file (xml document).
        /// </param>
        public Waypoint(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "destination":
                            Destination = mainNode.FirstChild.Value;
                            break;

                        case "warpfactor":
                            WarpFactor = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "position":
                            Position = new NovaPoint(mainNode);
                            break;
                            
                        default:
                            SetTask(mainNode.Name.ToString());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e.ToString());
                }

                mainNode = mainNode.NextSibling;
            }
        }
        
        public IWaypointTask SetTask(string taskName)
        {
            if (!taskName.Contains("Task"))
            {
                taskName += "Task";
            }
            
            taskName.Replace(" ", "");
            
            switch(taskName.ToLower())
            {
                case "colonisetask":
                    Task = new ColoniseTask();
                    break;
                case "invadetask":
                    Task = new InvadeTask();
                    break;
                case "layminestask":
                    Task = new LayMinesTask();
                    break;
                case "loadcargotask":
                    Task = new LoadTask();
                    break;
                case "scraptask":
                    Task = new ScrapTask();
                    break;
                case "unloadcargotask":
                    Task = new UnloadTask();
                    break;
                default:
                    Task = new NoTask();
                    break;
            }
            
            return Task;
        }
        

        /// <summary>
        /// Save: Serialise this Waypoint to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Waypoint.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelWaypoint = xmldoc.CreateElement("Waypoint");

            Global.SaveData(xmldoc, xmlelWaypoint, "Destination", Destination);
            
            if (Position != null && (Position.X != 0 || Position.Y != 0))
            {
                XmlElement xmlelPoint = xmldoc.CreateElement("Position");
                Global.SaveData(xmldoc, xmlelPoint, "X", Position.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                Global.SaveData(xmldoc, xmlelPoint, "Y", Position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelWaypoint.AppendChild(xmlelPoint);
            }
            
            Global.SaveData(xmldoc, xmlelWaypoint, "WarpFactor", WarpFactor.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlelWaypoint.AppendChild(Task.ToXml(xmldoc));

            return xmlelWaypoint;
        }        
    }
}
