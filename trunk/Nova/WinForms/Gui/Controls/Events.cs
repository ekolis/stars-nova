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
    using System.Drawing;

    #region Delegates

    using Nova.Common;

    /// <Summary>
    /// This is the hook to listen for a request for selection Detail.
    /// Objects who subscribe to this should return an Item corresponding
    /// to the currently selected Item (Fleet, Star, etc).
    /// </Summary>
    public delegate Item RequestSelection();

    /// <Summary>
    /// This is the hook to listen for changes in the cursor on the map.
    /// Objects who subscribe to this should respond to the cursor
    /// position change by using the CursorArgs supplied which holds
    /// the updated cursor position.
    /// </Summary>
    public delegate void CursorChanged(object sender, CursorArgs e);

    /// <Summary>
    /// This is the hook to listen for changes on the selected object.
    /// Objects who subscribe to this should respond to the new selection
    /// by using the SelectionArgs supplied which holds the updated
    /// selection data.
    /// </Summary>
    public delegate void SelectionChanged(object sender, SelectionArgs e);


    /// <Summary>
    /// This is the hook to listen for a new selected Star.
    /// Objects who subscribe to this should respond to the Star
    /// selection change by using the StarSelectionArgs supplied which hold
    /// the newly selected Star data.
    /// </Summary>
    public delegate void StarSelectionChanged(object sender, StarSelectionArgs e);


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
    public delegate void FleetSelectionChanged(object sender, FleetSelectionArgs e);

    /// <Summary>
    /// This is the hook to listen for when to update the starmap.
    /// This should be used to repain the map in certain cases, for example,
    /// when deleting a waypoint.
    /// </Summary>
    public delegate void RefreshStarMap();

    #endregion

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
    /// Holds data related to the current selection. 
    /// </Summary>
    public class SelectionArgs : System.EventArgs
    {
        public Item Item;

        public SelectionArgs(Item item)
        {
            this.Item = item;
        }
    }

    /// <Summary>
    /// Holds data related to the current Star selection. 
    /// </Summary>
    public class StarSelectionArgs : System.EventArgs
    {
        public Star Star;

        public StarSelectionArgs(Star star)
        {
            this.Star = star;
        }
    }


    /// <Summary>
    /// Holds data related to the current Fleet selection. 
    /// </Summary>
    public class FleetSelectionArgs : System.EventArgs
    {
        public Fleet Detail;
        public Fleet Summary;

        public FleetSelectionArgs(Fleet detail, Fleet summary)
        {
            this.Detail = detail;
            this.Summary = summary;
        }
    } 
}