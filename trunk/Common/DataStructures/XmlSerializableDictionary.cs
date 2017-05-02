#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// A Dictionary that can be serialized to Xml.
// This uses the default serialisation method and is not compatable with the 
// current Nova xml serialisation.
// ===========================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Collections;

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// A Dictionary which can be serialized to Xml.
    /// </summary>
    [Serializable]
    public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XmlSerializableDictionary()
        {
        }

        /// <summary>
        /// Determine if a given key is already in the dictionary.
        /// </summary>
        /// <param name="key">The key to test for.</param>
        /// <returns>Returns true if key is already in the dictionary, otherwise false.</returns>
        public bool Contains(TKey key)
        {
            return Keys.Contains(key);
        }

        /// <summary>
        /// Generate the XML Schema for XmlSerializableDictionary.
        /// </summary>
        /// <returns>The XML Schema for XmlSerializableDictionary".</returns>
        public XmlSchema GetSchema()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Read an XmlSerializableDictionary object from xml.
        /// </summary>
        /// <param name="reader">An <see cref="XmlReader"/> to read from.</param>
        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            reader.ReadStartElement(); // outer name of dictionary, e.g. </XmlSerializableDictionaryOfStringString>
            reader.ReadStartElement("Dictionary");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("Value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);
            }
            reader.ReadEndElement(); // </Dictionary>
            if (reader.NodeType == XmlNodeType.EndElement)
            {
                reader.ReadEndElement(); // </XmlSerializableDictionaryOfStringString>
            }
        }

        /// <summary>
        /// Write the XmlSerializableDictionary to Xml.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/> being written too.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            writer.WriteStartElement("Dictionary");
            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("Key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("Value");
                valueSerializer.Serialize(writer, this[key]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
