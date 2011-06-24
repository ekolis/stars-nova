#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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

namespace Nova.Common.Converters
{
    #region Using Statements
    using System;
    using System.Collections;
    using System.ComponentModel.Design.Serialization;
    using System.Reflection;
    using Nova.Common.Components;
    #endregion

    public class HullModuleConverter : InstanceDescriptorConverter<HullModule>
    {
        protected override InstanceDescriptor ConvertToInstanceDescriptor(HullModule value)
        {
            // int cellNumber, int componentMaximum, int componentCount, string componentType, string componentName)
            ConstructorInfo constructor = typeof(HullModule).GetConstructor(new Type[]
                {
                    typeof(int),
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(string)
                });

            ICollection arguments = new object[]
                {
                    value.CellNumber,
                    value.ComponentMaximum,
                    value.ComponentCount,
                    value.ComponentType,
                    (value.AllocatedComponent == null) ? null : value.AllocatedComponent.Name
                };

            return new InstanceDescriptor(constructor, arguments);
        }
    }
}