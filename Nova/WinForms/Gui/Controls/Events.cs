#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Drawing;

    using Nova.Common;
    using Nova.Common.DataStructures;

    /// <Summary>
    /// Holds data related to the current cursor position
    /// </Summary>
    public class CursorArgs : System.EventArgs
    {
        public Point Point;

        public CursorArgs(Point point)
        {
            this.Point = point;
        }
    }

    /// <Summary>
    /// Holds data related to the detailed selection. 
    /// </Summary>
    public class SelectionArgs : System.EventArgs
    {
        private Mappable obj;
        
        public NovaPoint Position
        {
            get
            {
                return obj.Position;
            }
        }
        
        public Mappable Selection
        {
            get
            {
                return obj;
            }
            
            set
            {
                obj = value;    
            }
        }

        public SelectionArgs(Mappable obj)
        {
            Selection = obj;
        }
    }
}