// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// The resources needed to construct a game item;
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NovaCommon
{
    // ===========================================================================
    // Class to hold environmental tolerance details
    // ===========================================================================

    [Serializable]
    public sealed class EnvironmentTolerance
    {
        public double Minimum = 0;
        public double Maximum = 0;

        public EnvironmentTolerance() { } // required for serialization
        public EnvironmentTolerance(double minv, double maxv)
        {
            Minimum = minv;
            Maximum = maxv;
        }

        // ============================================================================
        // Initialising Constructor from an xml node.
        // Precondition: node is a "EnvironmentTolerance" node in a Nova compenent definition file (xml document).
        // ============================================================================
        public EnvironmentTolerance(XmlNode node)
        {
            XmlNode subnode = node.FirstChild;
            while (subnode != null)
            {
                try
                {
                    switch (subnode.Name.ToLower())
                    {
                        case "min":
                            Minimum = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "max":
                            Maximum = double.Parse(((XmlText)subnode.FirstChild).Value, System.Globalization.CultureInfo.InvariantCulture);
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

        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelEnvironmentTolerance = xmldoc.CreateElement("EnvironmentTolerance");

            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Min", Minimum.ToString());
            Global.SaveData(xmldoc, xmlelEnvironmentTolerance, "Max", Maximum.ToString());
            return xmlelEnvironmentTolerance;
        }
    }


}
