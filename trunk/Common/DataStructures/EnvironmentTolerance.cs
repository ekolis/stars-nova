#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
// The environmental range a race can tollerate.
//
// ===========================================================================
#endregion

using System;
using System.Xml;

namespace Nova.Common
{

    /// <summary>
    /// Class to hold environmental tolerance details
    /// </summary>
    [Serializable]
    public class EnvironmentTolerance
    {
        private const string MinInternalIdentifier = "MinInternal";
        private const string MaxInternalIdentifier = "MaxInternal";
        private const string MinToleranceIdentifier = "MinTolerance";
        private const string MaxToleranceIdentifier = "MaxTolerance";
        private const string MinIdentifier = "Min";
        private const string MaxIdentifier = "Max";
        private const string ImmuneIdentifier = "Immune";

        private int minimumInternalValue = 15;
        private int maximumInternalValue = 85;
        private bool immune = false;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance() { }

        #endregion

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the Median value of an integer range.
        /// </summary>
        /// <remarks>
        /// FIXME (priority 3) - Mathematically this finds the mean, which in some
        /// circumstances is different from the Median. 
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public int Median()
        {
            return (int)(((MaximumValue - MinimumValue) / 2) + MinimumValue);
        }


        public int MaximumValue
        {
            get { return maximumInternalValue; }
            set { maximumInternalValue = value; }
        }

        public int MinimumValue
        {
            get { return minimumInternalValue; }
            set { minimumInternalValue = value; }
        }

        public bool Immune
        {
            get { return immune; }
            set { immune = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the optimum level as a percentage * 100 for this race Environment.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int OptimumLevel
        {
            get { return Median(); }
        }

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range * 100. 
        virtual protected int MakeInternalValue(double value)
        {
            return 0;
        }

        virtual protected string Format(int value)
        {
            return "N/A";
        }

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML.
        /// </summary>
        /// <param name="node">node is a "EnvironmentTolerance" <see cref="XmlNode"/> in 
        /// a Nova component definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public void FromXml(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name)
                    {
                        case MinIdentifier:
                            MinimumValue = MakeInternalValue(double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture));
                            break;
                        case MaxIdentifier:
                            MaximumValue = MakeInternalValue(double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture));
                            break;
                        case MinInternalIdentifier:
                            MinimumValue = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case MaxInternalIdentifier:
                            MaximumValue = int.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case ImmuneIdentifier:
                            Immune = bool.Parse(((XmlText)subnode.FirstChild).Value);
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save: Serialise this EnvironmentTolerance to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the EnvironmentTolerance</returns>
        /// ----------------------------------------------------------------------------
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelEnvironmentTolerance = xmldoc.CreateElement("EnvironmentTolerance");
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MinInternalIdentifier, MinimumValue);
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MaxInternalIdentifier, MaximumValue);
            // "correct" values for human readability only
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MinToleranceIdentifier, Format(MinimumValue));
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MaxToleranceIdentifier, Format(MaximumValue));
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, ImmuneIdentifier, Immune.ToString());
            return xmlelEnvironmentTolerance;
        }

        #endregion
    }
}
