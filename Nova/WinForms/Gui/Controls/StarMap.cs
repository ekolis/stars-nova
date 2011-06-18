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
using System.Diagnostics;

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


        #region Variables
        private readonly Bitmap cursorBitmap;
        private readonly Dictionary<string, Fleet> visibleFleets = new Dictionary<string, Fleet>();
        private readonly Dictionary<string, Minefield> visibleMinefields = new Dictionary<string, Minefield>();
        private readonly Font nameFont;
        
        private Intel turnData;
        private ClientState stateData;
        private NovaPoint cursorPosition = new Point(0, 0);
        private NovaPoint lastClick = new Point(0, 0);

        private NovaPoint logical = new Point(0, 0);  // Size of the logical coordinate system (size of the game universe).        
        private NovaPoint extent = new Point(0, 0);   // How big is the logical map in terms of Size
        private NovaPoint displayOffset = new NovaPoint(0, 0); // If extent is less then the panel size this is used to center the map in the panel
        private NovaPoint scrollOffset = new NovaPoint(0, 0); // Where the scroll bars are set to
        private double zoomFactor = 1.0;              // Is used to adjust the Extent of the map.


        private bool isInitialised;
        private bool displayStarNames = true;
        private bool displayBackground = true;
        private bool displayBorders = false;
        
        private int selection;
        private const double MIN_ZOOM = 0.2;
        private const double MAX_ZOOM = 5;

        #endregion


        #region Construction and Initialization

        /// <Summary>
        /// Initializes a new instance of the StarMap class.
        /// </Summary>
        public StarMap()
        {            
            InitializeComponent();

            MouseWheel += StarMap_MouseWheel;
            Resize += delegate { Zoom(); };

            this.MapPanel.Paint += MapPanel_Paint;
            
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


        void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawEverything(e.Graphics);
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
            
            float size = Math.Min(this.Size.Height, this.Size.Width);
            this.zoomFactor = 1.0;
            Zoom();
        }

        #endregion

        #region Drawing Methods

        /// <param name="graphics"></param>
        ///<Summary>
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
        private void DrawEverything(Graphics g)
        {
            
            if (this.isInitialised == false)
            {
                return;
            }
            
            // Erase previous drawings.
            g.Clear(Color.Black);     
   
            // (0) Draw the image backdrop and universe borders          
            NovaPoint backgroundOrigin = LogicalToDevice(new NovaPoint(0, 0));
            backgroundOrigin.Offset(-20,-20);
            NovaPoint backgroundExtent = LogicalToDevice(logical);
            backgroundExtent.Offset(40,40);
            

            Size renderSize = new Size();
            renderSize.Height = backgroundExtent.Y - backgroundOrigin.Y;
            renderSize.Width = backgroundExtent.X - backgroundOrigin.X;
            
            g.Clip = new Region(new Rectangle((Point)backgroundOrigin, renderSize));
            
            
            // This is the specified area which represents the playing universe      
            Rectangle targetArea = new Rectangle((Point)backgroundOrigin, renderSize); 
            
            if (this.displayBackground == true)
            {
                Image backdrop = Nova.Properties.Resources.Plasma;
                g.DrawImage(backdrop, targetArea);
                // Free the image after using it. This prevents a nasty
                // memory leak under Mono on Linux.
                backdrop.Dispose();
            }
            
            if (this.displayBorders == true)
            {
                g.DrawRectangle(new Pen(Brushes.DimGray), targetArea);
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
                    DrawCircle(g, lrScanBrush, (Point)star.Position, star.ScanRange);
                }
            }

            // (1b) Fleet long-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(g, lrScanBrush, (Point)fleet.Position, fleet.LongRangeScan);
                }
            }

            // (2) Fleet short-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(g, srScanBrush, (Point)fleet.Position, fleet.ShortRangeScan);
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
                DrawCircle(g, srMineBrush, (Point)minefield.Position, radius);
            }


            // (4) Visible fleets.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Type != "Starbase")
                {
                    DrawFleet(g, fleet);
                }
            }

            // (5) Stars plus starbases and orbiting fleet indications that are
            // the results of scans.

            foreach (Star star in this.turnData.AllStars.Values)
            {
                DrawStar(g, star);
                DrawOrbitingFleets(g, star);
            }

            // (6) Cursor.

            NovaPoint position = LogicalToDevice(this.cursorPosition);          
            position.Y += 5;
            g.TranslateTransform(position.X, position.Y);
            g.RotateTransform(180f);
            g.FillPolygon(Brushes.Yellow, cursorShape );
            g.ResetTransform();
            //g.DrawImage(this.cursorBitmap, (Point)position);

            // (7) Zoom/Scroll/Cursor info for debugging.
            DrawDebugInfo(g);
        }

        [Conditional("DEBUG")]
        private void DrawDebugInfo(Graphics g)
        {
            g.ResetClip();
            Font font = this.Font;

            Color coordColour = Color.FromArgb(255, 255, 255, 0);
            SolidBrush coordBrush = new SolidBrush(coordColour);
            string s2 = "Cursor Location (logical): " + this.cursorPosition.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + this.cursorPosition.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            g.DrawString(s2, font, coordBrush, 0, 20);                      
            string zoomDebugMsg = "Zoom Factor: " + this.zoomFactor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            g.DrawString(zoomDebugMsg, font, coordBrush, 0, 40);
            string scrollDebugMsg = "ScrollOffset: " + scrollOffset;
            g.DrawString(scrollDebugMsg, font, coordBrush, 0, 60);
            string extentStr = "Extent: " + extent;
            g.DrawString(extentStr, font, coordBrush, 0, 80);
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a filled circle using device coordinates.
        /// </Summary>
        /// <param name="brush"></param>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// ----------------------------------------------------------------------------
        private void FillCircle(Graphics g, Brush brush, Point position, int radius)
        {
            g.FillEllipse(
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
        /// <param name="logicalRadius"></param>
        /// ----------------------------------------------------------------------------
        private void DrawCircle(Graphics g, Brush brush, NovaPoint where, int logicalRadius)
        {
            if (logicalRadius == 0)
            {
                return;
            }

            NovaPoint position = LogicalToDevice(where);                        

            FillCircle(g, brush, (Point)position, (int)(logicalRadius * zoomFactor));
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Draw a fleet. We only draw fleets that are not in orbit. Indications of
        /// orbiting fleets are handled in the drawing of the Star.
        /// </Summary>
        /// <param name="fleet">The fleet to draw.</param>
        /// ----------------------------------------------------------------------------
        private void DrawFleet(Graphics g, Fleet fleet)
        {
            if (fleet.InOrbit == null)
            {
                NovaPoint position = LogicalToDevice(fleet.Position);

                g.TranslateTransform(position.X, position.Y);
                g.RotateTransform((float)fleet.Bearing);

                if (fleet.Owner == this.stateData.RaceName)
                {
                    g.FillPolygon(Brushes.Blue, triangle);
                }
                else
                {
                    g.FillPolygon(Brushes.Red, triangle);
                }

                g.ResetTransform();
            }

            if (fleet.Owner == this.stateData.RaceName)
            {
                Waypoint first = fleet.Waypoints[0];
                NovaPoint from = LogicalToDevice(first.Position);

                foreach (Waypoint waypoint in fleet.Waypoints)
                {
                    NovaPoint position = waypoint.Position;                 
        
                    g.DrawLine(Pens.Blue, (Point)from, (Point)LogicalToDevice(position));
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
        private void DrawStar(Graphics g, Star star)
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

            FillCircle(g, starBrush, (Point)position, size);

            // If the Star name display is turned on then add the name

            if (this.displayStarNames)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;               
                g.DrawString(star.Name, this.nameFont, Brushes.White, position.X, position.Y + 5, format);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Add an indication of a starbase (circle) or orbiting fleets (smaller
        /// circle) or both.
        /// </Summary>
        /// <param name="Star">The Star being drawn.</param>
        /// ----------------------------------------------------------------------------
        private void DrawOrbitingFleets(Graphics g, Star star)
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
                g.FillEllipse(
                    Brushes.Yellow,
                    position.X + 6,
                    position.Y - 6,
                    4,
                    4);
            }

            if (report.Age == 0 && report.OrbitingFleets)
            {
                int size = 12;
                g.DrawEllipse(
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
            
            result.X = (int)(p.X * zoomFactor) + displayOffset.X - scrollOffset.X;
            result.Y = (int)(p.Y * zoomFactor) + displayOffset.Y - scrollOffset.Y;

            return result;
        }


        private NovaPoint LogicalToExtent(NovaPoint p)
        {
            NovaPoint result = new NovaPoint();

            result.X = (int) (p.X*zoomFactor);
            result.Y = (int) (p.Y*zoomFactor);

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

            result.X = (int) ((p.X - displayOffset.X + scrollOffset.X)/zoomFactor);
            result.Y = (int) ((p.Y - displayOffset.Y + scrollOffset.Y)/zoomFactor);

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
            Zoom( 1.4 );
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
            Zoom( 1 / 1.4 );
        }


        /// <summary>
        /// Handle zooming via the mousewheel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StarMap_MouseWheel(object sender, MouseEventArgs e)
        {
            double zoomChange = 1 + (Math.Sign(e.Delta)) * 0.15;            
            Zoom(zoomChange, DeviceToLogical(new NovaPoint(e.X, e.Y) ) );
        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Zoom in or out of the Star map.
        /// </Summary>
        /// ----------------------------------------------------------------------------
        private void Zoom( double delta = 1.0, NovaPoint zoomCenter = null )
        {
            NovaPoint centerDisplay = new NovaPoint(MapPanel.Width / 2 , MapPanel.Height / 2);
            if (System.Object.ReferenceEquals(zoomCenter, null))
                zoomCenter = DeviceToLogical(centerDisplay);
            else
                centerDisplay = LogicalToDevice(zoomCenter);

            zoomFactor *= delta;
            this.zoomFactor = Math.Max(MIN_ZOOM, this.zoomFactor);
            this.zoomFactor = Math.Min(MAX_ZOOM, this.zoomFactor);
            this.zoomOut.Enabled = zoomFactor > MIN_ZOOM;
            this.zoomIn.Enabled = zoomFactor < MAX_ZOOM;

            this.extent.X = (int)(this.logical.X * this.zoomFactor);
            this.extent.Y = (int)(this.logical.Y * this.zoomFactor);

            // In the case where the Map Panel is bigger than what we want to display (i.e. extent)
            // then we add an offset to center the displayed map inside the panel
            // If extent is bigger then it's handled by the scroll offsets
            displayOffset.X = Math.Max((MapPanel.Width - extent.X) / 2, 0);
            displayOffset.Y = Math.Max((MapPanel.Height - extent.Y) / 2, 0);

            this.verticalScrollBar.Maximum = Math.Max(0, extent.Y - MapPanel.Height);
            this.horizontalScrollBar.Maximum = Math.Max(0, (extent.X - MapPanel.Width));

            // Try and recenter the center point displayed point;

            NovaPoint newCenterDisplay = LogicalToExtent(zoomCenter);

            scrollOffset.X = Math.Min(horizontalScrollBar.Maximum, Math.Max(0, newCenterDisplay.X - centerDisplay.X));
            scrollOffset.Y = Math.Min(verticalScrollBar.Maximum, Math.Max(0, newCenterDisplay.Y - centerDisplay.Y));

            horizontalScrollBar.Value = scrollOffset.X;
            verticalScrollBar.Value = scrollOffset.Y;

            //double oldMax = horizontalScrollBar.Maximum;
            
            //if (oldMax == 0)
            //    horizontalScrollBar.Value = 0;
            //else
            //    horizontalScrollBar.Value = (int)(horizontalScrollBar.Value/oldMax*horizontalScrollBar.Maximum);
            //scrollOffset.X = horizontalScrollBar.Value;

            //oldMax = verticalScrollBar.Maximum;
            
            //if (oldMax == 0)
            //    verticalScrollBar.Value = 0;
            //else
            //    verticalScrollBar.Value = (int)(verticalScrollBar.Value/oldMax*verticalScrollBar.Maximum);
            //scrollOffset.Y = verticalScrollBar.Value;);



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
            scrollOffset.X = e.NewValue;
            RefreshStarMap();
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
            scrollOffset.Y = e.NewValue;
            RefreshStarMap();
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

            
            fleet.Waypoints.Add(waypoint);
            RefreshStarMap();
            
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
            
            Zoom();
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
