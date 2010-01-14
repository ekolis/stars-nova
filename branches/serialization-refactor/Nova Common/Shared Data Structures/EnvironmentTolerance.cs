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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaCommon
{
    // ===========================================================================
    // Class to hold environmental tolerance details
    // ===========================================================================

    [Serializable]
    public sealed class EnvironmentTolerance : IXmlSerializable
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException(); // TODO XML deserialization of EnvironmentTolerance
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("EnvironmentTolerance");

            writer.WriteElementString("Min", Minimum.ToString(System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteElementString("Max", Maximum.ToString(System.Globalization.CultureInfo.InvariantCulture));

            writer.WriteEndElement();
        }
    }


}
