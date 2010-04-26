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
// Serializes and deserializes an object, or an entire graph of connected 
// objects, in binary format.
// ===========================================================================
#endregion

namespace NovaCommon
{
    #region Using Statements
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    #endregion

    //-------------------------------------------------------------------
    /// <summary>
    /// Serializes and deserializes an object, or an entire graph of connected objects, in binary format.
    /// </summary>
    //-------------------------------------------------------------------
    public static class Serializer
    {
        #region Methods
        //-------------------------------------------------------------------
        /// <summary>
        /// Deserializes the specified stream into an object graph.
        /// </summary>
        /// <param name="serializationStream">
        /// The stream from which to deserialize the object graph.
        /// </param>
        /// <returns>
        /// The top (root) of the object graph.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <i>serializationStream</i> is a null reference.
        /// </exception>
        //-------------------------------------------------------------------
        public static object Deserialize( Stream serializationStream )
        {
            Verify.NotNull( serializationStream, "serializationStream" );
            return Formatter.Deserialize( serializationStream );
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Serializes an object, or graph of connected objects, to the given stream.
        /// </summary>
        /// <param name="serializationStream">
        /// The stream to which the graph is to be serialized.
        /// </param>
        /// <param name="item">
        /// The object at the root of the graph to serialize.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <i>serializationStream</i> is a null reference.
        /// </exception>
        //-------------------------------------------------------------------
        public static void Serialize( Stream serializationStream, object item )
        {
            Verify.NotNull( serializationStream, "serializationStream" );
            Verify.NotNull( item, "item" );
            Formatter.Serialize( serializationStream, item );
        }
        #endregion

        #region Fields
        private static IFormatter formatter;
        #endregion

        #region Properties
        private static IFormatter Formatter
        {
            get
            {
                if( null == formatter )
                {
                    formatter = new BinaryFormatter();
                }
                return formatter;
            }
        }
        #endregion
    }
}
