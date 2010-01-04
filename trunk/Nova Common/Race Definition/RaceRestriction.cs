// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// Ship class. Note that ships never exist in isolation, they are always part
// of a fleet. Consequently, all the movement attributes can be found in the
// fleet class.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    public enum RaceAvailability
    {
        not_available,
        not_required,
        required
    }

    [Serializable]
    public class RaceRestriction
    {

        private Hashtable Restrictions = new Hashtable();

        //============================================================================
        // Default Constructor
        //============================================================================
        public RaceRestriction()
        {
            foreach (String trait in AllTraits.TraitKeys)
            {
                Restrictions[trait] = (int)RaceAvailability.not_required;
            }
        }

        //============================================================================
        // Copy Constructor
        //============================================================================
        public RaceRestriction(RaceRestriction existing)
        {
            try
            {
                foreach (String trait in AllTraits.TraitKeys)
                {
                    Restrictions[trait] = (int) existing.Restrictions[trait];
                }
            }
            catch
            {
                foreach (String trait in AllTraits.TraitKeys)
                {
                    Restrictions[trait] = (int)RaceAvailability.not_required;
                }
            }
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "Race_Restrictions" node in a Nova compenent definition file (xml document).
        // ============================================================================

        public RaceRestriction(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
              try
              {
                  foreach (String key in AllTraits.TraitKeys)
                  {
                      if (subnode.Name.ToLower() == key.ToLower())
                      {
                          Restrictions[key] = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);

                      }
                  }
              }
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
            }
        }

        public void SetRestriction(String trait, RaceAvailability availability)
        {
            Restrictions[trait] = availability;
        }

        public RaceAvailability Availability(String trait)
        {
            try
            {
                return (RaceAvailability)Restrictions[trait];
            }
            catch (System.NullReferenceException)
            {
                // Report.Error("Nova has encountered a problem which it will attempt to bypass."+Environment.NewLine+"Details: RaceRestrictions.Availability - Null reference exception.");
            }

            return RaceAvailability.not_required;
        }



        // ============================================================================
        // Return an XmlElement representation of the Race Restrictions
        // ============================================================================
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelResource = xmldoc.CreateElement("Race_Restrictions");
            try
            {
                foreach (string key in AllTraits.TraitKeys)
                {
                    // not_required is the default, only save variations
                    if (Restrictions.ContainsKey(key) && (RaceAvailability)Restrictions[key] != RaceAvailability.not_required)
                    {
                        XmlElement xmlelTrait = xmldoc.CreateElement(key);
                        XmlText xmltxtTrait = xmldoc.CreateTextNode(((int)this.Restrictions[key]).ToString());
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

        // ============================================================================
        /// <summary>
        /// Return a printable String representation of the restrictions.
        /// </summary>
        // ============================================================================
        public new String ToString()
        {
            String inwords = String.Empty;

           
            try
            {
                int keyIndex = 0;
                foreach (string key in AllTraits.TraitKeys)
                {

                    // not_required is the default, only save variations
                    
                   if (Restrictions.ContainsKey(key) && (RaceAvailability)Restrictions[key] != RaceAvailability.not_required)
                   {
                       inwords += "This component ";
                       // <requires/is not available with>
                       if ((RaceAvailability)this.Restrictions[key] == RaceAvailability.not_available)
                       {
                           inwords += "is not available with";
                       }
                       else if ((RaceAvailability)this.Restrictions[key] == RaceAvailability.required)
                       {
                           inwords += "requires";
                       }

                       inwords += " the ";
                       //<primary/secondary
                       if (keyIndex < AllTraits.NUMBER_OF_PRIMARY_RACIAL_TRAITS)
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
                Report.Error("Failed to display race restrictions."+Environment.NewLine+"Details: RaceRestrictions.ToString() - Exception: "+e.Message);
            }
            
            return inwords;
        }

    }
}
