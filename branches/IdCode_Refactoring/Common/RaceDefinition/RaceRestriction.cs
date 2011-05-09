﻿#region Copyright Notice
// ============================================================================
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
// This module allows components to be restricted to races with particular
// traits.
// This is implemented as a Hashtable of RaceAvailability enumerators indexed
// on the trait codes. For each trait the RaceAvailability is set to either
// not_available, not_required or required. By comparing a race's traits
// against this it can be determined if a component is available to a given
// race.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Xml;

namespace Nova.Common
{
    /// <summary>
    /// Enumeration of the ways a given trait can affect the availability of a component.
    /// </summary>
    public enum RaceAvailability
    {
        not_available, // A race with the given trait will not be able to use the given component.
        not_required,  // the given trait has not affect on the availability of the given component.
        required       // Only race's with the given trait will be able to use the given component.
    }

    /// <summary>
    /// An object to represent the race/traits a component is restricted to/from.
    /// </summary>
    [Serializable]
    public class RaceRestriction
    {
        private readonly Hashtable restrictions = new Hashtable();

        #region Construction

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RaceRestriction()
        {
            foreach (string trait in AllTraits.TraitKeys)
            {
                this.restrictions[trait] = (int)RaceAvailability.not_required;
            }
        }


        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="existing">An existing <see cref="RaceRestriction"/> to copy.</param>
        public RaceRestriction(RaceRestriction existing)
        {
            try
            {
                foreach (string trait in AllTraits.TraitKeys)
                {
                    this.restrictions[trait] = (int)existing.restrictions[trait];
                }
            }
            catch
            {
                foreach (string trait in AllTraits.TraitKeys)
                {
                    this.restrictions[trait] = (int)RaceAvailability.not_required;
                }
            }
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the availability for a particular trait.
        /// </summary>
        /// <param name="trait">The trait to set the availability-affect of.</param>
        /// <param name="availability">The affect on availability of this trait.</param>
        /// ----------------------------------------------------------------------------
        public void SetRestriction(string trait, RaceAvailability availability)
        {
            this.restrictions[trait] = availability;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the affect on availability of the given trait.
        /// </summary>
        /// <param name="trait">A trait code.</param>
        /// <returns>The affect on availability of the trait as a <see cref="RaceAvailability"/>.</returns>
        /// ----------------------------------------------------------------------------
        public RaceAvailability Availability(string trait)
        {
            try
            {
                return (RaceAvailability)this.restrictions[trait];
            }
            catch (System.NullReferenceException)
            {
                // if the given trait is not listed then the affect on availability is 'not_required'
                // so use the default return below.
            }

            return RaceAvailability.not_required;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a printable String representation of the restrictions.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public new string ToString()
        {
            string inwords = string.Empty;
           
            try
            {
                int keyIndex = 0;
                foreach (string key in AllTraits.TraitKeys)
                {

                    // not_required is the default, only save variations
                    
                   if (this.restrictions.ContainsKey(key) && (RaceAvailability)this.restrictions[key] != RaceAvailability.not_required)
                   {
                       inwords += "This component ";
                       // <requires/is not available with>
                       if ((RaceAvailability)this.restrictions[key] == RaceAvailability.not_available)
                       {
                           inwords += "is not available with";
                       }
                       else if ((RaceAvailability)this.restrictions[key] == RaceAvailability.required)
                       {
                           inwords += "requires";
                       }

                       inwords += " the ";
                       // <primary/secondary
                       if (keyIndex < AllTraits.NumberOfPrimaryRacialTraits)
                       {
                           inwords += "primary";
                       }
                       else
                       {
                           inwords += "secondary";
                       }
                       inwords += " racial trait '";
                       // <trait>
                       inwords += AllTraits.TraitString[keyIndex];
                       inwords += "'." + Environment.NewLine;
                   }

                   keyIndex++;

                }
            }
            catch (Exception e)
            {
                Report.Error("Failed to display race restrictions." + Environment.NewLine + "Details: RaceRestrictions.ToString(System.Globalization.CultureInfo.InvariantCulture) - Exception: " + e.Message);
            }
            
            return inwords;
        }

        #endregion

        #region Save Load Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the RaceRestriction class.
        /// Load a RaceRestriction from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within 
        /// a Nova xml file.
        /// </param>
        /// ----------------------------------------------------------------------------
        public RaceRestriction(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    foreach (string key in AllTraits.TraitKeys)
                    {
                        if (subnode.Name.ToLower() == key.ToLower())
                        {
                            this.restrictions[key] = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);

                        }
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
        /// Save: Serialise this RaceRestriction to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the RaceRestriction.</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelResource = xmldoc.CreateElement("Race_Restrictions");
            try
            {
                foreach (string key in AllTraits.TraitKeys)
                {
                    // not_required is the default, only save variations
                    if (this.restrictions.ContainsKey(key) && (RaceAvailability)this.restrictions[key] != RaceAvailability.not_required)
                    {
                        XmlElement xmlelTrait = xmldoc.CreateElement(key);
                        XmlText xmltxtTrait = xmldoc.CreateTextNode(((int)this.restrictions[key]).ToString(System.Globalization.CultureInfo.InvariantCulture));
                        xmlelTrait.AppendChild(xmltxtTrait);
                        xmlelResource.AppendChild(xmlelTrait);
                    }
                }
            }
            catch
            {
                Report.Error("Failed to represent Race Restrictions in xml.");
            }

            return xmlelResource;
        }

        #endregion

    }
}