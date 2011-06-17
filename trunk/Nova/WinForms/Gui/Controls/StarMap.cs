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
// This module maintains the Star map.
// ===========================================================================
#endregion

using System.Collections.Generic;

namespace Nova.WinForms.Gui
{
    #region Using
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.DataStructures;
    #endregion
    

    /// <Summary>
    /// StarMap is the control which holds the actual playing map. 
    /// </Summary>
    public partial class StarMap : UserControl
    {
        /// <Summary>
        /// This event should be fired when the StarMap requests the current
        /// selection information. Mostly used to assert where it is
        /// a fleet or a Star.
        /// </Summary>
        public event RequestSelection RequestSelectionEvent;
        
        /// <Summary>
        /// This event should be fired when the users changes the
        /// selection in the map with the mouse. Use it to report
        /// selection changes to other components of the GUI.
        /// </Summary>
        public event SelectionChanged SelectionChangedEvent;
        
        /// <Summary>
        /// This event should be fired when the user Adds or
        /// Deletes a waypoint via shift click.
        /// </Summary>
        public event WaypointChanged WaypointChangedEvent;
        
        private readonly Point[] triangle = 
        { 
            new Point(0, 5), 
            new Point(-5, -5),
            new Point(5, -5) 
        };

        private readonly Point[] cursorShape = 
        { 
            new Point(0, 0), 
            new Point(-5, -12),
            new Point(0, -9),
            new Point(5, -12) 
        };

        private class MyGfx
        {
            public Graphics Graphics;
        }
        #region Variables
        private readonly Bitmap cursorBitmap;
        private readonly Dictionary<string, Fleet> visibleFleets = new Dictionary<string, Fleet>();
        private readonly Dictionary<string, Minefield> visibleMinefields = new Dictionary<string, Minefield>();
        private readonly Font nameFont;
        
        private MyGfx grafx; // This is the buffered graphic object.
        private Intel turnData;
        private ClientState stateData;
        private NovaPoint cursorPosition = new Point(0, 0);
        private NovaPoint lastClick = new Point(0, 0);
        private NovaPoint logical = new Point(0, 0);  // Size of the logical coordinate system (size of the game universe).
        private NovaPoint origin = new Point(0, 0);   // Top left starting Point of the displayed map within the logical map.
        private NovaPoint center = new Point(0, 0);   // Focal Point of the displayed map.
        private NovaPoint extent = new Point(0, 0);   // Extent of the currently displayed map, from Origin.
        private double zoomFactor = 0.8;              // Is used to adjust the Extent of the map.
        private bool isInitialised;
        private bool displayStarNames = true;
        private bool displayBackground = true;
        private bool displayBorders = false;
        private int horizontalScroll = 50; // 0 to 100, used to position the Center, initially centered
        private int verticalScroll = 50;   // 0 to 100, used to position the Center, initially centered
        private int selection;
        #endregion


        #region Construction and Initialization

        /// <Summary>
        /// Initializes a new instance of the StarMap class.
        /// </Summary>
        public StarMap()
        {
            // We need to handle the resize properly.
            this.Resize += new EventHandler(this.OnResize);
            
            InitializeComponent();
            
            GameSettings.Restore();

            // Initial map size
            this.logical.X = GameSettings.Data.MapWidth;
            this.logical.Y = GameSettings.Data.MapHeight;

            this.extent.X = (int)(this.logical.X * this.zoomFactor);
            this.extent.Y = (int)(this.logical.Y * this.zoomFactor);

            this.cursorBitmap = Nova.Properties.Resources.Cursor;
            this.cursorBitmap.MakeTransparent(Color.Black);

            this.nameFont = new Font("Arial", (float)7.5, FontStyle.Regular, GraphicsUnit.Point);                        
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Post-construction initialisation.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        public void Initialise()
        {
            this.stateData = ClientState.Data;
            this.turnData = this.stateData.InputTurn;
            this.isInitialised = true;

            this.horizontalScrollBar.Enabled = true;
            this.verticalScrollBar.Enabled = true;

            this.zoomIn.Enabled = true;
            this.zoomIn.Visible = true;
            this.zoomIn.Refresh();

            DetermineVisibleFleets();
            DetermineVisibleMinefields();

            // center the view
            this.horizontalScroll = 50;
            this.verticalScroll = 50;
            this.center.X = this.logical.X / 2;
            this.center.Y = this.logical.Y / 2;
            this.zoomFactor = 0.8; // ensure the whole map can be seen.
            SetZoom();
        }

        #endregion

        #region Drawing Methods
  
        /// <Summary>
        /// Draws every object on the playing map to the graphics buffer.
        /// </Summary>
        /// <remarks>
        /// This does not actually render the map. Only draws to
        /// the buffer which is drawn later
        /// 
        /// We just do simple painting so the order that things are drawn is important
        /// (otherwise items may get overwritten and become invisible):
        ///
        /// (1) All long-range scanners (planets and ships) owned by the player.
        /// (2) All short-range scanners (ships only) owned by the player.
        /// (3) Minefields visible to the player (with transparency)
        /// (4) All fleets visible to the player.
        /// (5) Stars (including a starbase and orbiting fleets indication).
        /// (6) The selection cursor.
        /// </remarks>
        /// <seealso cref="OnPaint"/>
        private void DrawEverything()
        {
            
            if (this.isInitialised == false)
            {
                return;
            }
            
            // Erase previous drawings.
            grafx.Graphics.Clear(System.Drawing.Color.Transparent);        
            
            // (0) Draw the image backdrop and universe borders          
            NovaPoint backgroundOrigin = LogicalToDevice(new NovaPoint(0, 0));
            NovaPoint backgroundExtent = LogicalToDeviceRelative(new NovaPoint(this.logical.X, this.logical.Y));
            
            Size renderSize = new Size();
            renderSize.Height = backgroundExtent.Y;
            renderSize.Width = backgroundExtent.X;
            
            // This is the specified area which represents the playing universe      
            Rectangle targetArea = new Rectangle((Point)backgroundOrigin, renderSize); 
            
            if (this.displayBackground == true)
            {
                Image backdrop = Nova.Properties.Resources.Plasma;
                grafx.Graphics.DrawImage(backdrop, targetArea);
                // Free the image after using it. This prevents a nasty
                // memory leak under Mono on Linux.
                backdrop.Dispose();
            }
            
            if (this.displayBorders == true)
            {
                grafx.Graphics.DrawRectangle(new Pen(Brushes.DimGray), targetArea);
            }
            
            Color lrScanColour = Color.FromArgb(128, 128, 0, 0);
            SolidBrush lrScanBrush = new SolidBrush(lrScanColour);

            Color srScanColour = Color.FromArgb(128, 128, 128, 0);
            SolidBrush srScanBrush = new SolidBrush(srScanColour);

            // (1a) Planetary long-range scanners.

            foreach (Star star in this.turnData.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    DrawCircle(lrScanBrush, (Point)star.Position, star.ScanRange);
                }
            }

            // (1b) Fleet long-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(lrScanBrush, (Point)fleet.Position, fleet.LongRangeScan);
                }
            }

            // (2) Fleet short-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(srScanBrush, (Point)fleet.Position, fleet.ShortRangeScan);
                }
            }

            // (3) Minefields

            foreach (Minefield minefield in this.visibleMinefields.Values)
            {
                Color cb;
                Color cf;

                if (minefield.Owner == ClientState.Data.RaceName)
                {
                    cb = Color.FromArgb(0, 0, 0, 0);
                    cf = Color.FromArgb(128, 0, 128, 0);
                }
                else
                {
                    cb = Color.FromArgb(0, 0, 0, 0);
                    cf = Color.FromArgb(128, 128, 0, 128);
                }


                HatchStyle style = HatchStyle.DiagonalCross | HatchStyle.Percent50;
                HatchBrush srMineBrush = new HatchBrush(style, cf, cb);
                int radius = minefield.Radius;
                DrawCircle(srMineBrush, (Point)minefield.Position, radius);
            }


            // (4) Visible fleets.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Type != "Starbase")
                {
                    DrawFleet(fleet);
                }
            }

            // (5) Stars plus starbases and orbiting fleet indications that are
            // the results of scans.

            foreach (Star star in this.turnData.AllStars.Values)
            {
                DrawStar(star);
                DrawOrbitingFleets(star);
            }

            // (6) Cursor.

            NovaPoint position = LogicalToDevice(this.cursorPosition);          
            position.Y += 5;
            grafx.Graphics.TranslateTransform(position.X, position.Y);
            grafx.Graphics.RotateTransform(180f);
            grafx.Graphics.FillPolygon(Brushes.Yellow, cursorShape );
            grafx.Graphics.ResetTransform();
            //grafx.Graphics.DrawImage(this.cursorBitmap, (Point)position);

            // (7) Zoom/Scroll/Cursor info for debugging.
#if (DEBUG)
            Font font = new Font(this.zoomIn.Font.Name, this.zoomIn.Font.Size, this.zoomIn.Font.Style, this.zoomIn.Font.Unit);

            Color coordColour = Color.FromArgb(255, 255, 255, 0);
            SolidBrush coordBrush = new SolidBrush(coordColour);
            string s2 = "Cursor Location (logical): " + this.cursorPosition.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + this.cursorPosition.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            grafx.Graphics.DrawString(s2, font, coordBrush, 0, 20);
            string s = "Cursor Location (device): " + position.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            grafx.Graphics.DrawString(s, font, coordBrush, 0, 0);
            string zoomDebugMsg = "Zoom Facor: " + this.zoomFactor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            grafx.Graphics.DrawString(zoomDebugMsg, font, coordBrush, 0, 40);
            string horizontalScrollDebugMsg = "Hscroll: " + this.horizontalScroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            grafx.Graphics.DrawString(horizontalScrollDebugMsg, font, coordBrush, 0, 60);
            string verticalScrollDebugMsg = "Vscroll: " + this.verticalScroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            grafx.Graphics.DrawString(verticalScrollDebugMsg, font, coordBrush, 0, 80);
#endif
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Paint the Star map window from the graphics buffer info.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="eventData"></param>
        /// ----------------------------------------------------------------------------
        private void OnPaint(object sender, PaintEventArgs e)
        {      
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            grafx = new MyGfx() {Graphics = e.Graphics};
            DrawEverything();
            // Here we render from the previously drawn buffer.
            //grafx.Render(e.Graphics);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a filled circle using device coordinates.
        /// </Summary>
        /// <param name="brush"></param>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// ----------------------------------------------------------------------------
        private void FillCircle(Brush brush, Point position, int radius)
        {
            grafx.Graphics.FillEllipse(
                brush,
                position.X - radius,
                position.Y - radius,
                radius * 2,
                radius * 2);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a filled circle using logical coordinates.
        /// </Summary>
        /// <param name="brush"></param>
        /// <param name="where"></param>
        /// <param name="radius"></param>
        /// ----------------------------------------------------------------------------
        private void DrawCircle(Brush brush, NovaPoint where, int radius)
        {
            if (radius == 0)
            {
                return;
            }

            NovaPoint position = LogicalToDevice(where);
            NovaPoint logical = new NovaPoint(radius, 0);
            NovaPoint device = LogicalToDeviceRelative(logical);

            FillCircle(brush, (Point)position, device.X);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a fleet. We only draw fleets that are not in orbit. Indications of
        /// orbiting fleets are handled in the drawing of the Star.
        /// </Summary>
        /// <param name="fleet">The fleet to draw.</param>
        /// ----------------------------------------------------------------------------
        private void DrawFleet(Fleet fleet)
        {
            if (fleet.InOrbit == null)
            {
                NovaPoint position = LogicalToDevice(fleet.Position);

                grafx.Graphics.TranslateTransform(position.X, position.Y);
                grafx.Graphics.RotateTransform((float)fleet.Bearing);

                if (fleet.Owner == this.stateData.RaceName)
                {
                    grafx.Graphics.FillPolygon(Brushes.Blue, triangle);
                }
                else
                {
                    grafx.Graphics.FillPolygon(Brushes.Red, triangle);
                }

                grafx.Graphics.ResetTransform();
            }

            if (fleet.Owner == this.stateData.RaceName)
            {
                Waypoint first = fleet.Waypoints[0];
                NovaPoint from = LogicalToDevice(first.Position);

                foreach (Waypoint waypoint in fleet.Waypoints)
                {
                    NovaPoint position = waypoint.Position;                 
        
                    grafx.Graphics.DrawLine(Pens.Blue, (Point)from, (Point)LogicalToDevice(position));
                    from = LogicalToDevice(position);
                }
            }
        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a Star. The Star is just a small circle which is a bit bigger if we've
        /// explored it. 
        /// </Summary>
        /// <remarks>
        /// The colour of the Star symbol is based on its Star report (reports for stars
        /// owned by the current player are always up-to-date). Unoccupied stars are
        /// white. Stars colonised by the player are green. Stars owned by other races
        /// are red.
        /// </remarks>
        /// <param name="Star">The Star sytem to draw.</param>
        /// ----------------------------------------------------------------------------
        private void DrawStar(Star star)
        {
            StarReport report;
            stateData.StarReports.TryGetValue(star.Name, out report);
            NovaPoint position = LogicalToDevice(star.Position);
            int size = 2;
            Brush starBrush = Brushes.White;
            string owner = "?";

            // Bigger symbol for explored stars.

            if (report != null)
            {
                size = 4;
                owner = report.Owner;
            }

            // Our stars are greenish, other's are red, unknown or uncolonised
            // stars are white.

            if (owner == this.stateData.RaceName)
            {
                starBrush = Brushes.GreenYellow;
            }
            else
            {
                if (owner != null && owner != "?")
                {
                    starBrush = Brushes.Red;
                }
            }

            FillCircle(starBrush, (Point)position, size);

            // If the Star name display is turned on then add the name

            if (this.displayStarNames)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;               
                grafx.Graphics.DrawString(star.Name, this.nameFont, Brushes.White, position.X, position.Y + 5, format);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Add an indication of a starbase (circle) or orbiting fleets (smaller
        /// circle) or both.
        /// </Summary>
        /// <param name="Star">The Star being drawn.</param>
        /// ----------------------------------------------------------------------------
        private void DrawOrbitingFleets(Star star)
        {
            StarReport report;
            stateData.StarReports.TryGetValue(star.Name, out report);
            NovaPoint position = LogicalToDevice(star.Position);
            
            if (report == null)
            {
                return;
            }

            if (report.Starbase != null)
            {
                grafx.Graphics.FillEllipse(
                    Brushes.Yellow,
                    position.X + 6,
                    position.Y - 6,
                    4,
                    4);
            }

            if (report.Age == 0 && report.OrbitingFleets)
            {
                int size = 12;
                grafx.Graphics.DrawEllipse(
                    Pens.White,
                    position.X - (size / 2),
                    position.Y - (size / 2),
                    size,
                    size);
            }
        }

        #endregion

        #region Determine Visible

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Build a list of all fleets that are visible to the player.
        /// </Summary>
        /// <remarks>
        /// This consists of:
        /// (1) Fleets owned by the player
        /// (2) Fleets within the range of scanners on ships owned by the player
        /// (3) Fleets within the range of scanners on planets owned by the player
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void DetermineVisibleFleets()
        {
            List<Fleet> playersFleets = new List<Fleet>();

            // -------------------------------------------------------------------
            // (1) First the easy one. Fleets owned by the player.
            // -------------------------------------------------------------------

            foreach (Fleet fleet in this.turnData.AllFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    this.visibleFleets[fleet.Key] = fleet;
                    playersFleets.Add(fleet);
                }
            }

            // -------------------------------------------------------------------
            // (2) Not so easy. Fleets within the scanning range of the player's
            // Fleets.
            // -------------------------------------------------------------------

            foreach (Fleet fleet in playersFleets)
            {
                foreach (Fleet scan in this.turnData.AllFleets.Values)
                {
                    double range = 0;
                    range = PointUtilities.Distance(fleet.Position, scan.Position);
                    if (range <= fleet.LongRangeScan)
                    {
                        this.visibleFleets[scan.Key] = scan;
                    }
                }
            }

            // -------------------------------------------------------------------
            // (3) Now that we know how to deal with ship scanners planet scanners
            // are just the same.
            // -------------------------------------------------------------------

            foreach (Star star in this.turnData.AllStars.Values)
            {
                if (star.Owner == this.stateData.RaceName)
                {
                    foreach (Fleet scanned in this.turnData.AllFleets.Values)
                    {
                        if (PointUtilities.Distance(star.Position, scanned.Position)
                            <= star.ScanRange)
                        {
                            this.visibleFleets[scanned.Key] = scanned;
                        }
                    }
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Build a list of all Minefields that are visible to the player.
        /// </Summary>
        /// <remarks>
        /// This consists
        /// of:
        ///
        /// (1) Minefields owned by the player
        /// (2) Minefiels within the range of scanners on ships owned by the player
        /// (3) Minefields within the range of scanners on planets owned by the player
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void DetermineVisibleMinefields()
        {
            List<Fleet> playersFleets = new List<Fleet>();

            foreach (Fleet fleet in this.turnData.AllFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    playersFleets.Add(fleet);
                }
            }

            // -------------------------------------------------------------------
            // (1) First the easy one. Minefields owned by the player.
            // -------------------------------------------------------------------

            foreach (Minefield minefield in this.turnData.AllMinefields.Values)
            {
                if (minefield.Owner == this.stateData.RaceName)
                {
                    this.visibleMinefields[minefield.Key] = minefield;
                }
            }

            // -------------------------------------------------------------------
            // (2) Not so easy. Minefields within the scanning range of the
            // player's ships.
            // -------------------------------------------------------------------

            foreach (Fleet fleet in playersFleets)
            {
                foreach (Minefield minefield in this.turnData.AllMinefields.Values)
                {

                    bool isIn = PointUtilities.CirclesOverlap(
                        fleet.Position,
                        minefield.Position,
                        fleet.LongRangeScan,
                        minefield.Radius);

                    if (isIn == true)
                    {
                        this.visibleMinefields[minefield.Key] = minefield;
                    }
                }
            }

            // -------------------------------------------------------------------
            // (3) Now that we know how to deal with ship scanners planet scanners
            // are just the same.
            // -------------------------------------------------------------------

            foreach (Minefield minefield in this.turnData.AllMinefields.Values)
            {
                foreach (Star star in this.turnData.AllStars.Values)
                {
                    if (star.Owner == this.stateData.RaceName)
                    {

                        bool isIn = PointUtilities.CirclesOverlap(
                            star.Position,
                            minefield.Position,
                            star.ScanRange,
                            minefield.Radius);

                        if (isIn == true)
                        {
                            this.visibleMinefields[minefield.Key] = minefield;
                        }

                    }
                }
            }
        }

        #endregion

        #region Coordinate conversions

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Convert logical coordinates to device coordintes.
        /// </Summary>
        /// <param name="p">The Point to convert.</param>
        /// <returns>A converted Point.</returns>
        /// ----------------------------------------------------------------------------
        private NovaPoint LogicalToDevice(NovaPoint p)
        {
            NovaPoint result = new NovaPoint();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X - this.origin.X) * minLength / this.extent.X;
            result.Y = (p.Y - this.origin.Y) * minLength / this.extent.Y;

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Convert logical coordinates to device coordinates by scaling only (all offsets are ignored).
        /// </Summary>
        /// <param name="p">The Point to convert.</param>
        /// <returns>The converted Point.</returns>
        /// ----------------------------------------------------------------------------
        private NovaPoint LogicalToDeviceRelative(NovaPoint p)
        {
            NovaPoint result = new NovaPoint();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * minLength) / this.extent.X /* + BorderBuffer */;
            result.Y = (p.Y * minLength) / this.extent.Y /* + BorderBuffer */;

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Convert device coordinates to logical coordinates.
        /// </Summary>
        /// <param name="p">The Point to convert.</param>
        /// <returns>The converted Point.</returns>
        /// ----------------------------------------------------------------------------
        private NovaPoint DeviceToLogical(NovaPoint p)
        {
            NovaPoint result = new NovaPoint();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * this.extent.X / minLength) + this.origin.X;
            result.Y = (p.Y * this.extent.Y / minLength) + this.origin.Y;

            return result;
        }

        #endregion

        #region Zoom

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Process a request to zoom in the Star map.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        public void ZoomInClick(object sender, System.EventArgs e)
        {
            this.zoomFactor *= 1.6;
            this.zoomOut.Enabled = true;
            SetZoom();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Process a request to zoom out the Star map.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        public void ZoomOutClick(object sender, System.EventArgs e)
        {
            this.zoomFactor /= 1.6;
            if (this.zoomFactor < 0.5)
            {
                this.zoomFactor = 0.5;
                this.zoomOut.Enabled = false;
            }

            SetZoom();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Zoom in or out of the Star map.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void SetZoom()
        {
            this.extent.X = (int)(this.logical.X / this.zoomFactor);
            this.extent.Y = (int)(this.logical.Y / this.zoomFactor);

            MapHorizontalScroll(this.horizontalScroll);
            MapVerticalScroll(this.verticalScroll);

            this.RefreshStarMap();


        }

        #endregion

        #region Scroll

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Horizontally scroll the Star map.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MapScrollH(object sender, ScrollEventArgs e)
        {
            MapHorizontalScroll(e.NewValue);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Vertically scroll the Star map.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MapScrollV(object sender, ScrollEventArgs e)
        {
            MapVerticalScroll(e.NewValue);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Scroll the Star map horizontally.
        /// </Summary>
        /// <param name="value">new Hscroll position</param>
        /// ----------------------------------------------------------------------------
        private void MapHorizontalScroll(int value)
        {
            this.horizontalScroll = value;

            this.center.X = this.logical.X * this.horizontalScroll / 100;
            this.origin.X = this.center.X - (this.extent.X / 2);
            
            this.RefreshStarMap();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Scroll the Star map vertically.
        /// </Summary>
        /// <param name="value">New Vscroll position.</param>
        /// ----------------------------------------------------------------------------
        private void MapVerticalScroll(int value)
        {
            this.verticalScroll = value;
            this.center.Y = this.logical.Y * this.verticalScroll / 100;
            this.origin.Y = this.center.Y - (this.extent.Y / 2);

            this.RefreshStarMap();
        }

        #endregion

        #region Resize
        
        private void OnResize(object sender, EventArgs e)
        {
            //// Re-create the graphics buffer for a new window size.
            //this.bufferedContext.MaximumBuffer = new Size(this.MapPanel.Size.Width + 1, this.MapPanel.Size.Height + 1);
            //if (this.grafx != null)
            //{
            //    this.grafx.Dispose();
            //    this.grafx = null;               
            //}
            //this.grafx = bufferedContext.Allocate(
            //    this.MapPanel.CreateGraphics(), 
            //    new Rectangle(0, 0, this.MapPanel.Size.Width, this.MapPanel.Size.Height));
           
            //this.RefreshStarMap();
        }    
        
        #endregion
        
        #region Interface IComparable

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// A sortable (by distance) version of the Item class.
        /// </Summary>
        /// <remarks>
        /// FIXME (priority 5) - this class shouldn't be hidden inside this module. Is there any reason not to implement this via inheritance or make Item itself sortable?
        /// </remarks>
        /// ----------------------------------------------------------------------------
        public class SortableItem : IComparable
        {
            public double Distance;
            public Item Target;

            public int CompareTo(object rightHandSide)
            {
                SortableItem rhs = (SortableItem)rightHandSide;
                return this.Distance.CompareTo(rhs.Distance);
            }
        }

        #endregion

        #region Mouse Events

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Process a mouse down event.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void StarMapMouse(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Shift) != 0)
                {
                    if ((Control.ModifierKeys & Keys.Control) != 0)
                    {
                        LeftShiftMouse(e, false);
                    }
                    else
                    {
                        LeftShiftMouse(e, true);
                    }
                }
                else
                {
                    LeftMouse(e);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Left Shift Mouse: Set Waypoints.
        /// </Summary>
        /// <param name="e"></param>
        /// <param name="snapToObject"></param>
        /// ----------------------------------------------------------------------------
        private void LeftShiftMouse(MouseEventArgs e, bool snapToObject)
        {
            Item item = RequestSelectionEvent();

            if (item == null || !(item is Fleet))
            {
                return;
            }

            // We've confirmed that the fleet Detail control is displayed. Now get
            // a handle directly to it so that we can access its parameters
            // directly without having to use the Detail Summary panel as an agent

            // FleetDetail FleetDetail = MainWindow.Nova.SelectionDetail.Control
            //                           as FleetDetail;

            NovaPoint click = new NovaPoint(e.X, e.Y);
            Fleet fleet = item as Fleet;
            NovaPoint position = DeviceToLogical(click);
            List<SortableItem> nearObjects = FindNearObjects(position);
            Waypoint waypoint = new Waypoint();

            waypoint.Position = position;
            waypoint.WarpFactor = 6;
            waypoint.Task = "None";

            // If there are no items near the selected position then set the
            // waypoint to just be a position in space. Otherwise, make the target
            // of the waypoint the selected Item.
            //
            // To Do: Handle multiple items at the target location

            if (nearObjects.Count == 0 || snapToObject == false)
            {
                waypoint.Destination = "Space at " + position.ToString();
                waypoint.Position = position;
            }
            else
            {
                SortableItem selected = nearObjects[0];
                Item target = selected.Target;
                waypoint.Position = target.Position;
                waypoint.Destination = target.Name;
            }

            // If the new waypoint is the same as the last one then do nothing.

            int lastIndex = fleet.Waypoints.Count - 1;
            Waypoint lastWaypoint = fleet.Waypoints[lastIndex];
            NovaPoint lastPosition = lastWaypoint.Position;

            if (waypoint.Destination == lastWaypoint.Destination)
            {
                return;
            }

            // Draw the new waypoint on the map and add it to the list of
            // waypoints for the fleet.

            NovaPoint from = LogicalToDevice(lastPosition);
            NovaPoint to = LogicalToDevice(waypoint.Position);

            this.grafx.Graphics.DrawLine(Pens.Yellow, (Point)from, (Point)to);

            fleet.Waypoints.Add(waypoint);
            this.MapPanel.Refresh();
            
            // Notify listeners.
            SelectionArgs selectionArgs = new SelectionArgs(item);
            
            SelectionChangedEvent(this, selectionArgs);            
            WaypointChangedEvent(this);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Left mouse button: select objects.
        /// </Summary>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void LeftMouse(MouseEventArgs e)
        {
            NovaPoint position = new NovaPoint();
            NovaPoint click = new NovaPoint();
            click.X = e.X;
            click.Y = e.Y;
            position = DeviceToLogical(click);

            List<SortableItem> nearObjects = FindNearObjects(position);
            if (nearObjects.Count == 0)
            {
                return;
            }

            // If the mouse hasn't moved since the last selection cycle through
            // the list of near objects. If it has, start at the beginning of the
            // list.

            if ((Math.Abs(this.lastClick.X - click.X) > 10) ||
                (Math.Abs(this.lastClick.Y - click.Y) > 10))
            {
                this.selection = 0;
            }
            else
            {
                this.selection++;
                if (this.selection >= nearObjects.Count)
                {
                    this.selection = 0;
                }
            }

            this.lastClick = click;
            SortableItem selected = nearObjects[this.selection];
            Item item = selected.Target;        

            SetCursor(item.Position);
   
            // Build an object to hold data.
            SelectionArgs selectionArgs = new SelectionArgs(item);
            
            // Raise the appropiate event. Controls interested (listening)
            // to this event and data will react in turn.
            SelectionChangedEvent(this, selectionArgs);
        }

        #endregion

        #region Misc Methods

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Set the position of the Star map selection cursor.
        /// </Summary>
        /// <param name="position">Whete the cursor is to be put.</param>
        /// ----------------------------------------------------------------------------
        public void SetCursor(NovaPoint position)
        {
            this.cursorPosition = position;

            // Set the scroll position so that the selected position of the cursor
            // in is the centre of the screen.
            /* removed this as I found it annoying - Dan 08 may 10. Would be Ok if it only moved when the selected object was out of view.
            // FIXME (priority 2) - The scroll bar position indicator doesn't move.

            float fractionX = position.X;
            float fractionY = position.Y;

            fractionX /= Logical.X;
            fractionY /= Logical.Y;

            Hscroll = (int)(fractionX * 100.0);
            Vscroll = (int)(fractionY * 100.0);
            MapPanel.VerticalScroll.Value = Hscroll;
            MapPanel.HorizontalScroll.Value = Vscroll;
            
            SetZoom();
            */
            this.RefreshStarMap();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Provides a list of objects within a certain distance from a position,
        /// ordered by distance.
        /// </Summary>
        /// <param name="position">Starting Point for the search.</param>
        /// <returns>A list of Fleet and Star objects.</returns>
        /// ----------------------------------------------------------------------------
        private List<SortableItem> FindNearObjects(NovaPoint position)
        {
            List<SortableItem> nearObjects = new List<SortableItem>();

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (!fleet.IsStarbase)
                {
                    if (PointUtilities.IsNear(fleet.Position, position))
                    {
                        SortableItem thisItem = new SortableItem();
                        thisItem.Target = fleet;
                        thisItem.Distance = PointUtilities.Distance(position, fleet.Position);
                        nearObjects.Add(thisItem);
                    }
                }
            }

            foreach (Star star in this.turnData.AllStars.Values)
            {
                if (PointUtilities.IsNear(star.Position, position))
                {

                    SortableItem thisItem = new SortableItem();
                    thisItem.Target = star;
                    thisItem.Distance = PointUtilities.Distance(position, star.Position);
                    nearObjects.Add(thisItem);
                }
            }

            nearObjects.Sort();
            return nearObjects;
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Toggle the display of the Star names.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void ToggleNames_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox toggleNames = sender as CheckBox;

            if (toggleNames.Checked)
            {
                this.displayStarNames = true;
            }
            else
            {
                this.displayStarNames = false;
            }

            this.RefreshStarMap();
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Toggle the display of the background image.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void ToggleBackground_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox toggleBackground = sender as CheckBox;

            if (toggleBackground.Checked)
            {
                this.displayBackground = true;
            }
            else
            {
                this.displayBackground = false;
            }

            this.RefreshStarMap();
        }
        
        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Toggle the display of universe borders.
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void ToggleBorders_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox toggleBorders = sender as CheckBox;

            if (toggleBorders.Checked)
            {
                this.displayBorders = true;
            }
            else
            {
                this.displayBorders = false;
            }

            this.RefreshStarMap();
        }

        #endregion
        
        #region Communication Events
        
        /// <Summary>
        /// This handles external events in which another GUI element
        /// changes the selection. StarMap can react accordingly and
        /// update it's cursor withour exposing internal variables.
        /// </Summary>
        public void ChangeCursor(object sender, CursorArgs e)
        {
            SetCursor(e.Point);
        }
        
        public void RefreshStarMap()
        {
            //this.DrawEverything();
            // Invalidate or Refresh?
            //this.MapPanel.Refresh();
            this.MapPanel.Invalidate();
        }
        
        #endregion

    }
}
