#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.Common
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    
    /// <summary>
    /// Lists all possible item Types.
    /// </summary>
    public enum ItemType
    {
        None = 0,
        Defense, // FIXME; (priority 4) Disambiguate this two defense items!
        [Description("Planetary Installations")] PlanetaryInstallations,
        Hull,
        Engine,
        Mechanical,
        Electrical,
        Scanner,
        Terraforming,
        Orbital,
        Gate,
        [Description("Mining Robot")] MiningRobot,
        [Description("Mine Layer")] MineLayer,
        Shield,
        Armor,
        Bomb,
        Weapon,
        [Description("Beam Weapons")] BeamWeapons,
        Torpedoes,
        Ship,
        Starbase,
        Fleet,
        Star,
        [Description("Star Report")] StarIntel,
        [Description("Fleet Report")] FleetIntel
    }
    
    public static class ItemTypeExtensions
    {
        public static string ToDescription(this ItemType value)
        {
            FieldInfo info = value.GetType().GetField(value.ToString());
        
            DescriptionAttribute[] attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
        
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}