#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// Convenience functions to hide implementation (and save typing).
// ===========================================================================
#endregion

using System;
using System.Collections;
using NovaCommon;

namespace Nova.WinForms.Gui
{

    /// <summary>
    /// Convenience functions to hide implementation (and save typing).
    /// FIXME (priority 2) - doesn't save much as there is only one reference to this, for which we use a whole code file! 
    /// </summary>
    public class Utilities
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Force an update of the map display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void MapRefresh()
        {
            MainWindow.nova.MapControl.MapRefresh();
        }

    }
}
