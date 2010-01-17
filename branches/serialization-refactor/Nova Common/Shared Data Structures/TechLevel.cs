// ============================================================================
// Nova. (c) 2008 Ken Reed
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
      public Hashtable TechValues = new Hashtable();

      public static string[] ResearchKeys = {
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
// Default Constructor
//============================================================================

      public void SetIndividualTechLevel(string strTechName, int iLevel)
      {
            TechValues[strTechName] = iLevel;
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
