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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Nova.Common.Converters
{

    /// <summary>
    /// <see cref="TypeConverter"/> for <see cref="EnabledValue"/>.
    /// </summary>
    public class EnabledValueConverter : InstanceDescriptorConverter<EnabledValue>
    {
        protected override InstanceDescriptor ConvertToInstanceDescriptor(EnabledValue value)
        {
            ConstructorInfo constructor = typeof(EnabledValue).GetConstructor(new Type[]
                {
                    typeof(bool),
                    typeof(int)
                });

            ICollection arguments = new object[]
                {
                    value.IsChecked,
                    value.NumericValue
                };

            return new InstanceDescriptor(constructor, arguments);
        }
    }
}
