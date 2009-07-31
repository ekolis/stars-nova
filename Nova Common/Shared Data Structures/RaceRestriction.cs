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
        public const int NUMBER_OF_PRIMARY_RACIAL_TRAITS = 10;
        public static string[] TraitKeys = 
        {
            // 10 PRTs
            "HE", "SS", "WM", "CA", "IS", "SD", "PP", "IT", "AR", "JOAT",
            // 13 LRTs
            "IFE", "TT", "ARM", "ISB", "GR", "UR", "MA", "NRSE", "OBRM", "NAS", "LSP", "BET", "RS"
        };

        public static string[] TraitString = 
        {
            // 10 PRTs
            "Hyper Expansion", 
            "Supper Stealth", 
            "War Monger", 
            "Claim Adjuster", 
            "Inner Strength", 
            "Space Demolition", 
            "Packet Pysics", 
            "Interstellar Traveler", 
            "Artificial Reality", 
            "Jack of all Trades",
            // 13 LRTs
            "Improved Fuel Efficiency", 
            "Total Terraforming", 
            "Advanced Remote Mining", 
            "Improved Star Bases", 
            "Generalised Research", 
            "Ultimate Recycling", 
            "Mineral Alchemy", 
            "No Ram Scoop Eengines", 
            "Only Basic Remote Mining", 
            "No Advanced Scanners", 
            "Low Starting Population", 
            "Bleeding Edge Technology", 
            "Regenerating Shields"
        };

        private Hashtable Restrictions = new Hashtable();

        //============================================================================
        // Default Constructor
        //============================================================================
        public RaceRestriction()
        {
            foreach (String trait in RaceRestriction.TraitKeys)
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
                foreach (String trait in RaceRestriction.TraitKeys)
                {
                    Restrictions[trait] = (int) existing.Restrictions[trait];
                }
            }
            catch
            {
                foreach (String trait in RaceRestriction.TraitKeys)
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
                  foreach (String key in TraitKeys)
                  {
                      if (subnode.Name.ToLower() == key.ToLower())
                      {
                          Restrictions[key] = int.Parse(((XmlText)subnode.FirstChild).Value);

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
                foreach (string key in RaceRestriction.TraitKeys)
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
                foreach (string key in RaceRestriction.TraitKeys)
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
                       if (keyIndex < NUMBER_OF_PRIMARY_RACIAL_TRAITS)
                       {
                           inwords += "primary";
                       }
                       else
                       {
                           inwords += "secondary";
                       }
                       inwords += " racial trait '";
                       // <trait>
                       inwords += TraitString[keyIndex];
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
