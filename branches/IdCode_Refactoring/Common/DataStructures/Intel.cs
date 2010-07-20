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
// This module contains the data that is generated by the Nova Console to
// generate a turn (including the very first one). This is the Intel sent to 
// the player.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
#endregion

// ============================================================================
// Manipulation of the turn data that is created by the Nova Console and read
// by the Nova GUI.
// ============================================================================

namespace Nova.Common
{
   [Serializable]
   public sealed class Intel
   {

// ============================================================================
// The data items created by the Nova Console and read by the Nova GUI.
// ============================================================================

      public int TurnYear = 2100;
      public Race MyRace = new Race();
      public ArrayList Messages = new ArrayList();
      public ArrayList Battles = new ArrayList();
      public ArrayList AllRaceNames = new ArrayList();
      public ArrayList AllScores = new ArrayList();
      public Hashtable RaceIcons = new Hashtable();
      public Hashtable AllFleets = new Hashtable();
      public Hashtable AllDesigns = new Hashtable();
      public Hashtable AllStars = new Hashtable();
      public Hashtable AllMinefields = new Hashtable();

      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Reset all data structures.
      /// </summary>
      /// ----------------------------------------------------------------------------
      public void Clear()
      {
          MyRace = null;
          AllDesigns.Clear();
          AllFleets.Clear();
          AllRaceNames.Clear();
          AllStars.Clear();
          AllMinefields.Clear();
          Battles.Clear();
          Messages.Clear();

          TurnYear = 2100;
      }

      // ============================================================================
      // Return an XmlElement representation of the Intel
      // ============================================================================      
       /*      public XmlElement ToXml(XmlDocument xmldoc)
   {
       System.Xml.XmlElement xmlelModule = xmldoc.CreateElement("Module");


       // CellNumber
       XmlElement xmlelCellNumber = xmldoc.CreateElement("CellNumber");
       XmlText xmltxtCellNumber = xmldoc.CreateTextNode(this.CellNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
       xmlelCellNumber.AppendChild(xmltxtCellNumber);
       xmlelModule.AppendChild(xmlelCellNumber);
       // ComponentCount
       XmlElement xmlelComponentCount = xmldoc.CreateElement("ComponentCount");
       XmlText xmltxtComponentCount = xmldoc.CreateTextNode(this.ComponentCount.ToString(System.Globalization.CultureInfo.InvariantCulture));
       xmlelComponentCount.AppendChild(xmltxtComponentCount);
       xmlelModule.AppendChild(xmlelComponentCount);
       // ComponentMaximum
       XmlElement xmlelComponentMaximum = xmldoc.CreateElement("ComponentMaximum");
       XmlText xmltxtComponentMaximum = xmldoc.CreateTextNode(this.ComponentMaximum.ToString(System.Globalization.CultureInfo.InvariantCulture));
       xmlelComponentMaximum.AppendChild(xmltxtComponentMaximum);
       xmlelModule.AppendChild(xmlelComponentMaximum);
       // ComponentType
       XmlElement xmlelComponentType = xmldoc.CreateElement("ComponentType");
       XmlText xmltxtComponentType = xmldoc.CreateTextNode(this.ComponentType);
       xmlelComponentType.AppendChild(xmltxtComponentType);
       xmlelModule.AppendChild(xmlelComponentType);

        

       return xmlelModule;
   }  */
   }

}
