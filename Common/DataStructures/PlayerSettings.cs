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

namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
 
    /// <summary>
    /// This module holds the settings for a player. 
    /// </summary>
    [Serializable]
    public class PlayerSettings
    {
        public string RaceName; // The path & file name of the race.
        public string AiProgram; // The path & file name of the AI application or "Human"
        public ushort PlayerNumber; // The order number of the player from 1 - Global.MaxPlayers        
    
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public PlayerSettings()
        {
        }
        
        /// <summary>
        /// Load: constructor to load PlayerSettings from an XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a PlayerSettings representation (from a save file).</param>
        public PlayerSettings(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "racename":
                            RaceName = subnode.FirstChild.Value;
                            break;
                        case "aiprogram":
                            AiProgram = subnode.FirstChild.Value;
                            break;
                        case "playernumber":
                            PlayerNumber = ushort.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch
                {
                    // ignore incomplete or unset values
                }

                subnode = subnode.NextSibling;
            }
        }
        
        /// <summary>
        /// Save: Generate an XmlElement representation of the PlayerSettings.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument.</param>
        /// <returns>An XmlElement representing the PlayerSettings (to be written to file).</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelPlayerSettings = xmldoc.CreateElement("PlayerSettings");
            
            Global.SaveData(xmldoc, xmlelPlayerSettings, "RaceName", RaceName);
            Global.SaveData(xmldoc, xmlelPlayerSettings, "AiProgram", AiProgram);
            Global.SaveData(xmldoc, xmlelPlayerSettings, "PlayerNumber", PlayerNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
        
            return xmlelPlayerSettings;
        }
    }
}
