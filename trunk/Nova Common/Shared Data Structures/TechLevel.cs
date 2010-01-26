// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale 2010
//
// Class defining the set of technology levels required before a component is
// required to gain access to a component.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Xml;
using System.Collections;

namespace NovaCommon
{

// ===========================================================================
// TechLevels Class.
// ===========================================================================

   [Serializable]
   public class TechLevel
   {

      public enum ResearchField { Biotechnology, Electronics, Energy, Propulsion, Weapons, Construction };

//----------------------------------------------------------------------------

       // TODO (priority 3) - Make these members private to hide the 
       // implementaion of the hashtable and force access through the enums, 
       // in order to prevent errors due to using string literals (e.g. "Biotech" vs "Biotechnology")
      private Hashtable TechValues = new Hashtable();
      // used for internal access to the Hashtable
      private static string[] ResearchKeys = {
         "Biotechnology", "Electronics", "Energy", 
         "Propulsion",    "Weapons",     "Construction" };


//============================================================================
// Default Constructor
//============================================================================

      public TechLevel()
      {
         foreach (string key in ResearchKeys) {
            TechValues[key] = 0;
         }
      }



//============================================================================
// Constructor setting all levels to a specified value
//============================================================================

      public TechLevel(int level)
      {
         foreach (string key in ResearchKeys) {
            TechValues[key] = level;
         }
      }


//============================================================================
// Copy Constructor
//============================================================================

      public TechLevel(TechLevel copy)
      {
         TechValues = copy.TechValues.Clone() as Hashtable;
      }

      /// <summary>
      /// Index operator to allow array type indexing to a TechLevel.
      /// </summary>
      /// <param name="index">A TechLevel.ResearchField</param>
      /// <returns>The current tech level.</returns>
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

      public IEnumerator GetEnumerator()
      {
          foreach (int level in TechValues.Values)
          {
              yield return level;
          }
      }

// ============================================================================
// Initialising Constructor from an xml node.
// Precondition: node is a "tech" node in a Nova compenent definition file (xml document).
// ============================================================================

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
              catch
              {
                  // ignore incomplete or unset values
              }
              subnode = subnode.NextSibling;
          }
      }

      /// <summary>
      /// Provide a new TechLevel instance which is a copy of the current instance.
      /// </summary>
      /// <returns></returns>
      public TechLevel Clone()
      {
          return new TechLevel(this);
      }

      /// <summary>
      /// Set the level of an individual technology, without affecting the others.
      /// </summary>
      /// <param name="strTechName">The technology that is to be changed.</param>
      /// <param name="iLevel">The new level for that technology.</param>
      public void SetIndividualTechLevel(string strTechName, int iLevel)
      {
          TechValues[strTechName] = iLevel;
      }

//============================================================================
// See if a TechLevel set is greater than, or equal to, another.
// ============================================================================

      public static bool operator >=(TechLevel lhs, TechLevel rhs)
      {
         Hashtable lhsT = lhs.TechValues;
         Hashtable rhsT = rhs.TechValues;

         foreach (string key in TechLevel.ResearchKeys) {
            if ((int) lhsT[key] < (int) rhsT[key]) {
               return false;
            }
         }

         return true;
      }


//============================================================================
// See if a TechLevel set is greater than another. 
// ============================================================================

      public static bool operator >(TechLevel lhs, TechLevel rhs)
      {
         Hashtable lhsT   = lhs.TechValues;
         Hashtable rhsT   = rhs.TechValues;

         foreach (string key in TechLevel.ResearchKeys) {
            if ((int) lhsT[key] != 0) {
               if ((int) lhsT[key] <= (int) rhsT[key]) {
                  return false;
               }
            }
         }

         return true;
      }


//============================================================================
// See if a TechLevel set is less than another. The test is only applied to
// those fields that differ between the compared values.
// ============================================================================

      public static bool operator <(TechLevel lhs, TechLevel rhs)
      {
         Hashtable lhsT   = lhs.TechValues;
         Hashtable rhsT   = rhs.TechValues;
         bool      tested = false;

         foreach (string key in TechLevel.ResearchKeys) {
            if ((int) lhsT[key] != (int) rhsT[key]) {
               tested = true;
               if ((int) lhsT[key] > (int) rhsT[key]) {
                  return false;
               }
            }
         }

         return tested;
      }


//============================================================================
// See if a TechLevel set is less than, or equal to, another.
// ============================================================================

      public static bool operator <=(TechLevel lhs, TechLevel rhs)
      {
         Hashtable lhsT = lhs.TechValues;
         Hashtable rhsT = rhs.TechValues;

         foreach (string key in TechLevel.ResearchKeys) {
            if ((int) lhsT[key] > (int) rhsT[key]) {
               return false;
            }
         }

         return true;
      }

      // ============================================================================
      // Return an XmlElement representation of the Tech Level
      // ============================================================================
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
   }
}
