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
    [TypeConverter(typeof(EnvironmentToleranceConverter))]
    public sealed class EnvironmentTolerance
    {
        public double minimumRealValue = 0;
        public double maximumRealValue = 0;

        private int minimumInternalValue = 15;
        private int maximumInternalValue = 85;


        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance() { }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minv"></param>
        /// <param name="maxv"></param>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance(double minv, double maxv)
        {
            MinimumRealValue = minv;
            MaximumRealValue = maxv;
        }

        #endregion

        // ToDo: 1. Verwendung ersetzen, 2. InternalValue (auch) speichern, bzw. speichern von Anzeige-Wert und Verarbeitungs-Wert trennen, 3. auch bei RealValue InternalValue nutzen
        // 4. Anzeige-Wert überall über Gravity etc. berechnen lassen

        public double MinimumRealValue
        {
            get { return minimumRealValue; }
            set { minimumRealValue = value; }
        }

        public double MaximumRealValue
        {
            get { return maximumRealValue; }
            set { maximumRealValue = value; }
        }

        public int MinimumInternalValue
        {
            get { return minimumInternalValue; }
            set { minimumInternalValue = value; }
        }

        public int MaximumInternalValue
        {
            get { return maximumInternalValue; }
            set { maximumInternalValue = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the Median value of an integer range.
        /// </summary>
        /// <param name="tolerance">An <see cref="EnvironmentTolerance"/> to determine the Median of.</param>
        /// <remarks>
        /// FIXME (priority 3) - Mathematically this finds the mean, which in some
        /// circumstances is different from the Median. 
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public int Median()
        {
            return (int)(((MaximumRealValue - MinimumRealValue) / 2) + MinimumRealValue);
        }

        #region Load Save Xml

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">node is a "EnvironmentTolerance" <see cref="XmlNode"/> in 
        /// a Nova compenent definition file (xml document).
        /// </param>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance(XmlNode node)
        {
            MinimumRealValue = Global.ParseDoubleSubnode(node, "Min");
            MaximumRealValue = Global.ParseDoubleSubnode(node, "Max");
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
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Min", MinimumRealValue);
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Max", MaximumRealValue);
            return xmlelEnvironmentTolerance;
        }

        #endregion
    }
}
