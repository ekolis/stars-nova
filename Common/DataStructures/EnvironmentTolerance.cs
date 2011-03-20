#region Copyright Notice
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
// The environmental range a race can tollerate.
//
// TODO (priority 3) - What are the full environment ranges? Min&Max for each variable. Need a reference to where this is documented.

/* From http://www.starsfaq.com/advfaq/guts2.htm#4.11
 * 4.11) Guts of Planet Values

(I haven't included the explanation of how the formula was derived; if you're interested, go to deja news and look up "re: Race wizard - Hab studies" by Bill Butler, 1998/04/10

The full equation is:

Hab%=SQRT[(1-g)^2+(1-t)^2+(1-r)^2]*(1-x)*(1-y)*(1-z)/SQRT[3]

Where g,t,and r (standing for gravity, temperature, and radiation)are given by
Clicks_from_center/Total_clicks_from_center_to_edge

and where x,y, and z  are
x=g-1/2 for g>1/2       x=0 for g<1/2
y=t-1/2 for t>1/2         y=0 for t<1/2
z=r-1/2 for r>1/2         z=0 for r<1/2

The farther habs are from center, the less accurate the result of this equation will be.  However, the errors are small, so the predicted answer will always be within a percentage or two of the actual value.

Thanks to Bill Butler for the mathematical wizardry. 
 * */
// ===========================================================================
#endregion

using System;
using System.ComponentModel;
using System.Xml;
using Nova.Common.Converters;

namespace Nova.Common
{

    /// <summary>
    /// Class to hold environmental tolerance details
    /// </summary>
    [Serializable]
    public class EnvironmentTolerance
    {
        private double minimumRealValue = 0;
        private double maximumRealValue = 0;

        private int minimumInternalValue = 15;
        private int maximumInternalValue = 85;

        private string name = "";


        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance() { }

        #endregion

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

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

        private const string NameIdentifier = "Name";
        private const string MinInternalIdentifier = "MinInternal";
        private const string MaxInternalIdentifier = "MaxInternal";
        private const string MinToleranceIdentifier = "MinTolerance";
        private const string MaxToleranceIdentifier = "MaxTolerance";
        private const string MinIdentifier = "Min";
        private const string MaxIdentifier = "Max";

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
                        case NameIdentifier:
                            Name = ((XmlText)subnode.FirstChild).Value;
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
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, NameIdentifier, Name);
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MinInternalIdentifier, MinimumValue);
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MaxInternalIdentifier, MaximumValue);
            // "correct" values for human readability only
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MinToleranceIdentifier, Format(MinimumValue));
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, MaxToleranceIdentifier, Format(MaximumValue));
            return xmlelEnvironmentTolerance;
        }

        #endregion
    }
}
