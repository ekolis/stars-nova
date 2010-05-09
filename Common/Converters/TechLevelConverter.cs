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

namespace Nova.Common.Converters
{
    /// <summary>
    /// <see cref="TypeConverter"/> for <see cref="TechLevel"/>.
    /// </summary>
    public class TechLevelConverter : InstanceDescriptorConverter<TechLevel>
    {
        protected override InstanceDescriptor ConvertToInstanceDescriptor(TechLevel value)
        {
            ConstructorInfo constructor = typeof(TechLevel).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) });
            return new InstanceDescriptor(constructor, new object[]  {
                value[TechLevel.ResearchField.Biotechnology],
                value[TechLevel.ResearchField.Electronics],
                value[TechLevel.ResearchField.Energy],
                value[TechLevel.ResearchField.Propulsion],
                value[TechLevel.ResearchField.Weapons],
                value[TechLevel.ResearchField.Construction]
            });
        }
    }
}
