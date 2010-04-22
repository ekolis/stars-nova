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
// Class defining the set of technology levels required before a component is
// required to gain access to a component.
// ===========================================================================
#endregion

using System;
using System.Xml;
using System.Collections;

namespace NovaCommon
{

    /// <summary>
    /// TechLevels Class.
    /// </summary>
    [Serializable]
    public class TechLevel
    {
        /// <summary>
        /// Enumeration of the different fields of technical research.
        /// </summary>
        public enum ResearchField { Biotechnology, Electronics, Energy, Propulsion, Weapons, Construction };

        //----------------------------------------------------------------------------

        // These members are private to hide the 
        // implementaion of the hashtable and force access through the enums, 
        // in order to prevent errors due to using string literals (e.g. "Biotech" vs "Biotechnology")
        private Hashtable TechValues = new Hashtable();
        // used for internal access to the Hashtable
        private static string[] ResearchKeys = {
         "Biotechnology", "Electronics", "Energy", 
         "Propulsion",    "Weapons",     "Construction" };

        #region Construction

        //----------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        //----------------------------------------------------------------------------
        public TechLevel()
        {
            foreach (string key in ResearchKeys)
            {
                TechValues[key] = 0;
            }
        }


        //----------------------------------------------------------------------------
        /// <summary>
        /// Constructor setting all levels to a specified value
        /// </summary>
        /// <param name="level">Level to set all techs too.</param>
        //----------------------------------------------------------------------------
        public TechLevel(int level)
        {
            foreach (string key in ResearchKeys)
            {
                TechValues[key] = level;
            }
        }


        //----------------------------------------------------------------------------
        /// <summary>
        /// Constructor setting all levels to individual values.
        /// </summary>
        /// <param name="biotechnology">Level to set the biotechnology.</param>
        /// <param name="electronics">Level to set the electronics.</param>
        /// <param name="energy">Level to set the energy.</param>
        /// <param name="propulsion">Level to set the propulsion.</param>
        /// <param name="weapons">Level to set the weapons.</param>
        /// <param name="construction">Level to set the construction.</param>
        //----------------------------------------------------------------------------
        public TechLevel(int biotechnology, int electronics, int energy, int propulsion, int weapons, int construction)
        {
            TechValues["Biotechnology"] = biotechnology;
            TechValues["Electronics"] = electronics;
            TechValues["Energy"] = energy;
            TechValues["Propulsion"] = propulsion;
            TechValues["Weapons"] = weapons;
            TechValues["Construction"] = construction;
        }


        //----------------------------------------------------------------------------
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="copy">object to copy</param>
        //----------------------------------------------------------------------------
        public TechLevel(TechLevel copy)
        {
            TechValues = copy.TechValues.Clone() as Hashtable;
        }

        #endregion

        #region Interfaces

        //----------------------------------------------------------------------------
        /// <summary>
        /// Provide a new TechLevel instance which is a copy of the current instance.
        /// </summary>
        /// <returns></returns>
        //----------------------------------------------------------------------------
        public TechLevel Clone()
        {
            return new TechLevel(this);
        }

        #endregion

        #region Operators

        //----------------------------------------------------------------------------
        /// <summary>
        /// Index operator to allow array type indexing to a TechLevel.
        /// </summary>
        /// <param name="index">A TechLevel.ResearchField</param>
        /// <returns>The current tech level.</returns>
        //----------------------------------------------------------------------------
        public int this[ResearchField index]
        {
            get
            {
                if (TechValues == null)
                {
                    throw new System.NullReferenceException("TechLevel.cs : index operator - attempt to index with no TechValues defined.");
                }
                int techLevel = -1;
                switch (index)
                {
                    case ResearchField.Biotechnology: techLevel = (int)TechValues["Biotechnology"]; break;
                    case ResearchField.Construction: techLevel = (int)TechValues["Construction"]; break;
                    case ResearchField.Electronics: techLevel = (int)TechValues["Electronics"]; break;
                    case ResearchField.Energy: techLevel = (int)TechValues["Energy"]; break;
                    case ResearchField.Propulsion: techLevel = (int)TechValues["Propulsion"]; break;
                    case ResearchField.Weapons: techLevel = (int)TechValues["Weapons"]; break;
                }
                if (techLevel == -1)
                    throw new System.ArgumentException("TechLevel.cs: indexing operator - Unknown field of research", index.ToString());
                return techLevel;
            }
            set
            {
                switch (index)
                {
                    case ResearchField.Biotechnology: TechValues["Biotechnology"] = value; break;
                    case ResearchField.Construction: TechValues["Construction"] = value; break;
                    case ResearchField.Electronics: TechValues["Electronics"] = value; break;
                    case ResearchField.Energy: TechValues["Energy"] = value; break;
                    case ResearchField.Propulsion: TechValues["Propulsion"] = value; break;
                    case ResearchField.Weapons: TechValues["Weapons"] = value; break;
                }
            }
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Allow <c>foreach</c> to work with TechLevel.
        /// </summary>
        //----------------------------------------------------------------------------
        public IEnumerator GetEnumerator()
        {
            foreach (int level in TechValues.Values)
            {
                yield return level;
            }
        }


        // =============================================================================
        // Note: For two tech levels A and B if any field in A is less than any coresponding
        //       field in B, then A < B is true. It is possible for (A < B) && (B < A) to
        //       be true for TechLevels. This is so that all fields must be met in order
        //       to obtain a particular component. 
        //
        //       For example a race may have 10 propulsion tech and 5 in weapons.
        //       A particular weapon may need 9 propulsion and 12 in weapons.
        //       The race has less tech than required for the weapon (weapons tech too low)
        //       But the weapon also requires less tech than the race has (in construction).
        //
        //       A > B is only true if for all tech fields in A the coresponding field in B
        //       is less than or equal to A and at least one field in A is greater than the
        //       corresponding field in B.
        //
        //       TODO (priority 3) - Given the complexity here some unit tests would be nice.
        // =============================================================================

        //----------------------------------------------------------------------------
        /// <summary>
        /// Return true if lhs >= rhs (for all fields).
        /// </summary>
        //----------------------------------------------------------------------------
        public static bool operator >=(TechLevel lhs, TechLevel rhs)
        {
            Hashtable lhsT = lhs.TechValues;
            Hashtable rhsT = rhs.TechValues;

            foreach (string key in TechLevel.ResearchKeys)
            {
                if ((int)lhsT[key] < (int)rhsT[key])
                {
                    return false;
                }
            }

            return true;
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Return true if lhs >= rhs for all fields and lhs > rhs for at least one field.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        //----------------------------------------------------------------------------
        public static bool operator >(TechLevel lhs, TechLevel rhs)
        {
            Hashtable lhsT = lhs.TechValues;
            Hashtable rhsT = rhs.TechValues;

            return ! (lhs <= rhs);
        
        }


        //----------------------------------------------------------------------------
        /// <summary>
        /// return true if lhs &lt; rhs in any field.
        /// </summary>
        //----------------------------------------------------------------------------
        public static bool operator <(TechLevel lhs, TechLevel rhs)
        {
            Hashtable lhsT = lhs.TechValues;
            Hashtable rhsT = rhs.TechValues;

            foreach (string key in TechLevel.ResearchKeys)
            {
                if ((int)lhsT[key] < (int)rhsT[key])
                {
                    return true;
                }
            }

            return false;
        }


        //----------------------------------------------------------------------------
        /// <summary>
        /// return true if lhs &lt; rhs in any field or lhs == rhs
        /// </summary>
         //----------------------------------------------------------------------------
        public static bool operator <=(TechLevel lhs, TechLevel rhs)
        {
            Hashtable lhsT = lhs.TechValues;
            Hashtable rhsT = rhs.TechValues;

            foreach (string key in TechLevel.ResearchKeys)
            {
                if ((int)lhsT[key] < (int)rhsT[key])
                {
                    return true;
                }
            }

            if (lhsT == rhsT) return true;

            return false;
        }

        #endregion

        #region Save Load Xml

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "tech" node in a Nova compenent definition file (xml document).
        // ============================================================================
        //----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        //----------------------------------------------------------------------------
        public TechLevel(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    foreach (String key in ResearchKeys)
                    {
                        if (subnode.Name.ToLower() == key.ToLower())
                        {
                            TechValues[key] = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);

                        }
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                subnode = subnode.NextSibling;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this property to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the Tech Level.</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelResource = xmldoc.CreateElement("Tech");

            foreach (string key in TechLevel.ResearchKeys)
            {
                XmlElement xmlelTech = xmldoc.CreateElement(key);
                XmlText xmltxtTech = xmldoc.CreateTextNode(((int)this.TechValues[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                xmlelTech.AppendChild(xmltxtTech);
                xmlelResource.AppendChild(xmlelTech);
            }

            return xmlelResource;
        }

        #endregion

    }
}
