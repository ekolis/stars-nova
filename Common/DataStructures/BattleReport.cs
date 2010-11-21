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
// This module contains the definition of a report on a battle;
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Drawing;
using System.Xml;

namespace Nova.Common
{
    [Serializable]
    public class BattleReport
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record a new stack position.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Movement
        {
            public string StackName = null;
            public Point Position   = new Point();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record a new target.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Target
        {
            public Ship TargetShip = null;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to destroy a ship in a given stack.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Destroy
        {
            public string ShipName  = null;
            public string StackName = null;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A class to record weapons being fired.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Serializable]
        public class Weapons
        {
            public double HitPower     = 0;
            public string Targeting    = null;
            public Target WeaponTarget = new Target();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Main battle report components.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string Location  = null;
        public int SpaceSize    = 0;
        public ArrayList Steps  = new ArrayList();
        public Hashtable Stacks = new Hashtable();
        public Hashtable Losses = new Hashtable();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleReport()
        {
        }

        #region Xml


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load: Initialising Constructor from an xml node.
        /// </summary>
        /// <param name="node">A <see cref="BattleReport"/> XmlNode from a Nova save file (xml document)</param>
        /// ----------------------------------------------------------------------------
        public BattleReport(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {

                    switch (subnode.Name.ToLower())
                    {
                        case "location":
                            Location = subnode.FirstChild.Value;
                            break;

                        case "spacesize":
                            SpaceSize = int.Parse(subnode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;

                        // TODO (priority 6) - load the rest of the battle report.
                    }
                }
                catch (Exception e)
                {
                    Report.Error("Error loading Battle Report : " + e.Message);
                }
                subnode = subnode.NextSibling;
            }
            
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate an XmlElement representation of the ShipDesign for saving to file.
        /// Note this uses the minimal approach of storing the ship hull object 
        /// (and recursing through all components). All figured values will need to be 
        /// recalculated on loading.
        /// </summary>
        /// <param name="xmldoc">The parent XmlDocument</param>
        /// <returns>An XmlElement representing the ShipDesign</returns>
        /// ----------------------------------------------------------------------------
        public new XmlElement ToXml(XmlDocument xmldoc)
        {
            
            XmlElement xmlelBattleReport = xmldoc.CreateElement("BattleReport");

            
            if (Location != null) Global.SaveData(xmldoc, xmlelBattleReport, "Location", Location);
            Global.SaveData(xmldoc, xmlelBattleReport, "SpaceSize", SpaceSize.ToString(System.Globalization.CultureInfo.InvariantCulture));


            // TODO (priority 6) - finish battle report ToXml()

            // TODO (priority 2) - remove when done

            // public ArrayList Steps  = new ArrayList();
            // public Hashtable Stacks = new Hashtable();
            // public Hashtable Losses = new Hashtable();

            return xmlelBattleReport;

        }


        #endregion
    }
}
