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
    /// Keeps details of the data needed for the Score report.
    /// </summary>
    [Serializable]
    public sealed class ScoreRecord : IComparable
    {
        public int EmpireId;
        public int Rank;
        public int Score;
        public int Planets;
        public int Starbases;
        public int UnarmedShips;
        public int EscortShips;
        public int CapitalShips;
        public int TechLevel;
        public int Resources;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScoreRecord()
        {
        }

        /// <summary>
        /// Provide a sort by rank function.
        /// </summary>
        /// <param name="rightHandSide"></param>
        public int CompareTo(object rightHandSide)
        {
            ScoreRecord rhs = (ScoreRecord)rightHandSide;
            return rhs.Score.CompareTo(this.Score);
        }

        /// <summary>
        /// Load from XML: initializing constructor from an XML node.
        /// </summary>
        /// <param name="xmlnode">An <see cref="XmlNode"/> within 
        /// a Nova game file (xml document).
        /// </param>
        public ScoreRecord(XmlNode xmlnode)
        {
            XmlNode subnode = xmlnode.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "empire":
                            EmpireId = int.Parse(subnode.FirstChild.Value, System.Globalization.NumberStyles.HexNumber);
                            break;

                        case "rank":
                            Rank = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "score":
                            Score = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "planets":
                            Planets = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "starbases":
                            Starbases = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "unarmedships":
                            UnarmedShips = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "escortships":
                            EscortShips = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "capitalships":
                            CapitalShips = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "techlevel":
                            TechLevel = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        case "resources":
                            Resources = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Report.FatalError(e.Message + "\n Details: \n" + e);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialize this object to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the ScoreRecord.</returns>
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelScoreRecord = xmldoc.CreateElement("ScoreRecord");

            // Race;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Empire", EmpireId.ToString("X"));

            // Rank;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Rank", Rank.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Score;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Score", Score.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Planets;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Planets", Planets.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Starbases;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Starbases", Starbases.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // UnarmedShips;
            Global.SaveData(xmldoc, xmlelScoreRecord, "UnarmedShips", UnarmedShips.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // EscortShips;
            Global.SaveData(xmldoc, xmlelScoreRecord, "EscortShips", EscortShips.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // CapitalShips;
            Global.SaveData(xmldoc, xmlelScoreRecord, "CapitalShips", CapitalShips.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // TechLevel;
            Global.SaveData(xmldoc, xmlelScoreRecord, "TechLevel", TechLevel.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Resources;
            Global.SaveData(xmldoc, xmlelScoreRecord, "Resources", Resources.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelScoreRecord;
        }
    }
}
