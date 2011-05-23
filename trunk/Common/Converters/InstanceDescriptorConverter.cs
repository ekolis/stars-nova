#region Copyright Notice
// ============================================================================
// Copyright (C) 2010 stars-nova
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

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Nova.Common.Converters
{
    /// <summary>
    /// Generic <see cref="TypeConverter"/> for conversions to <see cref="InstanceDescriptor"/>.
    /// </summary>
    public abstract class InstanceDescriptorConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object result = null;
            if (destinationType == typeof(InstanceDescriptor))
            {
                result = ConvertToInstanceDescriptor((T)value);
            }

            if (result == null)
            {
                result = base.ConvertTo(context, culture, value, destinationType);
            }
            return result; 
        }

        /// <summary>
        /// Called by <see cref="ConvertTo"/> to if converting to an <see cref="InstanceDescriptor"/>.
        /// </summary>
        /// <param name="value">Source value to be converted to an <see cref="InstanceDescriptor"/>.</param>
        protected abstract InstanceDescriptor ConvertToInstanceDescriptor(T value);
    }
}
