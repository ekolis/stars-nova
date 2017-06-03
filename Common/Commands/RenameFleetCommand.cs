#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project.
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

namespace Nova.Common.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// A command to pass to the server to rename a player's fleet.
    /// In Stars! players can name their own fleets anything they like, and that is what all players see. Can be used to dis-inform other players.
    /// </summary>
    public class RenameFleetCommand : ICommand
    {
        /// <summary>
        /// The key of the fleet to rename.
        /// </summary>
        public long FleetKey
        {
            get;
            set;
        }

        /// <summary>
        /// The new name to be applied to the fleet.
        /// </summary>
        public string NewName
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="fleet">The fleet to rename.</param>
        /// <param name="newName">The new name to give the fleet.</param>
        public RenameFleetCommand(Fleet fleet, string newName)
        {
            FleetKey = fleet.Key;
            NewName = newName;
        }

        /// <summary>
        /// Determine if this fleet rename is a valid command.
        /// </summary>
        /// <param name="empire">The empire issuing/requesting the fleet rename command.</param>
        /// <returns>True if the fleet can be renamed.</returns>
        public bool IsValid(EmpireData empire)
        {
            // check the fleet belongs to the race seeking to rename it
            if ( ! empire.OwnedFleets.ContainsKey(FleetKey))
            {
                return false;
            }

            // check the name is valie
            if (string.IsNullOrEmpty(NewName))
            {
                return false;
            }

            // otherwise, all is good
            return true;
        }

        /// <summary>
        /// Perform the rename command.
        /// </summary>
        /// <param name="empire">The empire issuing/requesting the fleet rename command.</param>
        public void ApplyToState(EmpireData empire)
        {
            if (this.IsValid(empire))
            {
                empire.OwnedFleets[FleetKey].Name = this.NewName;
            }
        }


        /// <summary>
        /// Load from XML: Initializing constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> defining a <see cref="RenameFleetCommand"/>.
        /// </param>
        public RenameFleetCommand(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            
            while (subnode != null)
            {
                switch (subnode.Name.ToLower())
                {
                    case "fleetkey":
                      FleetKey = long.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                    break;

                    case "newname":
                        NewName = subnode.FirstChild.Value;
                    break;
                }
            
                subnode = subnode.NextSibling;
            }   
        }


        /// <summary>
        /// Save: Serialize this <see cref="RenameFleetCommand"/> to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Command.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelCom = xmldoc.CreateElement("Command");
            xmlelCom.SetAttribute("Type", "RenameFleet");
            Global.SaveData(xmldoc, xmlelCom, "FleetKey", FleetKey.ToString("X"));
            Global.SaveData(xmldoc, xmlelCom, "NewName", NewName);

            return xmlelCom;    
        }
    }
}
