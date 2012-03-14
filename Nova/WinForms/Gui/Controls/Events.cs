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
// Delegates and args classes used by events in controls
// ===========================================================================
#endregion


namespace Nova.WinForms.Gui
{
    using System;
    using System.Drawing;

    using Nova.Common;

    /// <Summary>
    /// This is the hook to listen for a request for selection Detail.
    /// Objects who subscribe to this should return an Item corresponding
    /// to the currently selected Item (Fleet, Star, etc).
    /// </Summary>
    public delegate Mappable RequestSelection();

    /// <Summary>
    /// This is the hook to listen for changes in the cursor on the map.
    /// Objects who subscribe to this should respond to the cursor
    /// position change by using the CursorArgs supplied which holds
    /// the updated cursor position.
    /// </Summary>
    public delegate void CursorChanged(object sender, CursorArgs e);

    /// <Summary>
    /// This is the hook to listen for a new selected Star.
    /// Objects who subscribe to this should respond to the Star
    /// selection change by using the StarSelectionArgs supplied which hold
    /// the newly selected Star data.
    /// </Summary>
    public delegate void SummarySelectionChanged(object sender, SummarySelectionArgs e);


    /// <Summary>
    /// This is the hook to listen for changes on WayPoints.
    /// </Summary>
    public delegate void WaypointChanged(object sender);

    /// <Summary>
    /// This is the hook to listen for a new selected Fleet.
    /// Objects who subscribe to this should respond to the Fleet
    /// selection change by using the FleetSelectionArgs supplied which hold
    /// the newly selected Fleet data.
    /// </Summary>
    public delegate void DetailSelectionChanged(object sender, DetailSelectionArgs e);
    /// <Summary>
    /// This is the hook to listen for when to update the starmap.
    /// This should be used to repain the map in certain cases, for example,
    /// when deleting a waypoint.
    /// </Summary>
    public delegate void RefreshStarMap();

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
    /// Holds data related to the summary selection. 
    /// </Summary>
    public class SummarySelectionArgs : System.EventArgs
    {
        private Item summary;
        
        public Item Summary
        {
            get
            {
                return summary;
            }
            
            set
            {
                if (value.Type != ItemType.FleetIntel && value.Type != ItemType.StarIntel)
                {
                    throw new ArgumentException("Summary arguments can only be FleetIntel or StarIntel types");
                }
                else
                {
                    summary = value;    
                }
            }
        }

        public SummarySelectionArgs(Item summary)
        {
            Summary = summary;
        }
    }

    /// <Summary>
    /// Holds data related to the detailed selection. 
    /// </Summary>
    public class DetailSelectionArgs : System.EventArgs
    {
        private Item detail;
        
        public Item Detail
        {
            get
            {
                return detail;
            }
            
            set
            {
                if (value.Type != ItemType.Fleet && value.Type != ItemType.Star)
                {
                    throw new ArgumentException("Detail arguments can only be Fleet or Star types");
                }
                else
                {
                    detail = value;    
                }
            }
        }

        public DetailSelectionArgs(Item detail)
        {
            Detail = detail;
        }
    }
}