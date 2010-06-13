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
using System.Reflection;
using System.Collections;

namespace Nova.Common.Converters
{
    /// <summary>
    /// <see cref="TypeConverter"/> for <see cref="EnvironmentTolerance"/>.
    /// </summary>
    public class EnvironmentToleranceConverter : InstanceDescriptorConverter<EnvironmentTolerance>
    {
        protected override InstanceDescriptor ConvertToInstanceDescriptor(EnvironmentTolerance value)
        {
            ConstructorInfo constructor = typeof(EnvironmentTolerance).GetConstructor(new Type[]
                {
                    typeof(double),
                    typeof(double)
                });

            ICollection arguments = new object[]
                {
                    value.Minimum,
                    value.Maximum
                };

            return new InstanceDescriptor(constructor, arguments);
        }
    }
}
