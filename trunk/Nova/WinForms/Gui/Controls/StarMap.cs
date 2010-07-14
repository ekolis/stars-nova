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
// This module maintains the star map.
// ===========================================================================
#endregion

#region Using
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
#endregion

namespace Nova.WinForms.Gui
{
    public partial class StarMap : UserControl
    {
        private readonly Point[] triangle = 
        { 
            new Point(0, 0), 
            new Point(-5, -10),
            new Point(5, -10) 
        };

        public const int BorderBuffer = 35;

        #region Variables
        private readonly Bitmap cursorBitmap;
        private readonly Hashtable visibleFleets = new Hashtable();
        private readonly Hashtable visibleMinefields = new Hashtable();
        private readonly Font nameFont;
        private readonly BufferedGraphicsContext bufferedContext;
        private Intel turnData;
        private ClientState stateData;
        private Point cursorPosition = new Point(0, 0);
        private Point lastClick = new Point(0, 0);
        private Point logical = new Point(0, 0);  // Size of the logical co-ordinate system (size of the game universe).
        private Point origin = new Point(0, 0);  // Top left starting point of the displayed map within the logical map.
        private Point center = new Point(0, 0);  // Focal point of the displayed map.
        private Point extent = new Point(0, 0);  // Extent of the currently displayed map, from Origin.
        private double zoomFactor = 0.8;              // Is used to adjust the Extent of the map.
        private Graphics graphics;
        private bool isInitialised;
        private bool displayStarNames = true;
        private int horizontalScroll = 50; // 0 to 100, used to position the Center, initially centered
        private int verticalScroll = 50; // 0 to 100, used to position the Center, initially centered
        private int selection;
        #endregion


        #region Construction and Initialization

        /// <summary>
        /// Initializes a new instance of the StarMap class.
        /// </summary>
        public StarMap()
        {
            bufferedContext = new BufferedGraphicsContext();

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

            this.MapPanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            this.MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();  

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Post-construction initialisation.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void Initialise()
        {
            this.stateData = ClientState.Data;
            this.turnData = this.stateData.InputTurn;
            this.isInitialised = true;
            /*string sv = this.ZoomIn.Visible.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string se = this.ZoomIn.Enabled.ToString(System.Globalization.CultureInfo.InvariantCulture);*/

            this.horizontalScrollBar.Enabled = true;
            this.verticalScrollBar.Enabled = true;

            this.zoomIn.Enabled = true;
            this.zoomIn.Visible = true;
            this.zoomIn.Invalidate();
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Utility function to instigate a display refresh
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void MapRefresh()
        {
            this.MapPanel.Invalidate();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Paint the star map window.
        /// </summary>
        /// <remarks>
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
        /// <param name="sender"></param>
        /// <param name="eventData"></param>
        /// ----------------------------------------------------------------------------
        private void OnPaint(object sender, PaintEventArgs eventData)
        {
            base.OnPaint(eventData); // added

            if (this.isInitialised == false)
            {
                return;
            }

            System.Drawing.Rectangle rect2 = new Rectangle(0, 0, this.MapPanel.Width, this.MapPanel.Height);
            System.Drawing.BufferedGraphics bg = bufferedContext.Allocate(eventData.Graphics, rect2);

            graphics = bg.Graphics;

            Point backgroundOrigin = LogicalToDevice(new Point(0, 0));
            Point backgroundExtent = LogicalToDeviceRelative(new Point(this.logical.X, this.logical.Y));
            graphics.DrawImage(Nova.Properties.Resources.Plasma, backgroundOrigin.X, backgroundOrigin.Y, backgroundExtent.X, backgroundExtent.Y);

            this.MapPanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            this.MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

            Color lrScanColour = Color.FromArgb(128, 128, 0, 0);
            SolidBrush lrScanBrush = new SolidBrush(lrScanColour);

            Color srScanColour = Color.FromArgb(128, 128, 128, 0);
            SolidBrush srScanBrush = new SolidBrush(srScanColour);

            // (1a) Planetary long-range scanners.

            foreach (Star star in this.turnData.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    DrawCircle(lrScanBrush, star.Position, star.ScanRange);
                }
            }

            // (1b) Fleet long-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(lrScanBrush, fleet.Position, fleet.LongRangeScan);
                }
            }

            // (2) Fleet short-range scanners.

            foreach (Fleet fleet in this.visibleFleets.Values)
            {
                if (fleet.Owner == this.stateData.RaceName)
                {
                    DrawCircle(srScanBrush, fleet.Position, fleet.ShortRangeScan);
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
                DrawCircle(srMineBrush, minefield.Position, radius);
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

            Point position;
            foreach (Fleet fleet in this.stateData.PlayerFleets)
            {
                if (fleet.InOrbit != null)
                {
                    position = LogicalToDevice(fleet.Position);
                    int size = 10;
                    graphics.DrawEllipse(
                        Pens.White,
                        position.X - (size / 2),
                        position.Y - (size / 2),
                        size,
                        size);
                }
            }

            // (6) Cursor.

            position = LogicalToDevice(this.cursorPosition);
            position.X -= (this.cursorBitmap.Width / 2) + 1;
            position.Y += 2;
            graphics.DrawImage(this.cursorBitmap, position);

            // (7) Zoom/Scroll/Cursor info for debugging.
#if (DEBUG)
            Font font = new Font(this.zoomIn.Font.Name, this.zoomIn.Font.Size, this.zoomIn.Font.Style, this.zoomIn.Font.Unit);

            Color coordColour = Color.FromArgb(255, 255, 255, 0);
            SolidBrush coordBrush = new SolidBrush(coordColour);
            string s2 = "Cursor Location (logical): " + this.cursorPosition.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + this.cursorPosition.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(s2, font, coordBrush, 0, 20);
            string s = "Cursor Location (device): " + position.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(s, font, coordBrush, 0, 0);
            string zoomDebugMsg = "Zoom Facor: " + this.zoomFactor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(zoomDebugMsg, font, coordBrush, 0, 40);
            string horizontalScrollDebugMsg = "Hscroll: " + this.horizontalScroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(horizontalScrollDebugMsg, font, coordBrush, 0, 60);
            string verticalScrollDebugMsg = "Vscroll: " + this.verticalScroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(verticalScrollDebugMsg, font, coordBrush, 0, 80);
#endif

            bg.Render();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw a filled circle using device coordinates.
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// ----------------------------------------------------------------------------
        private void FillCircle(Brush brush, Point position, int radius)
        {
            graphics.FillEllipse(
                brush,
                position.X - radius,
                position.Y - radius,
                radius * 2,
                radius * 2);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw a filled circle using logical coordinates.
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="where"></param>
        /// <param name="radius"></param>
        /// ----------------------------------------------------------------------------
        private void DrawCircle(Brush brush, Point where, int radius)
        {
            if (radius == 0) return;

            Point position = LogicalToDevice(where);
            Point logical = new Point(radius, 0);
            Point device = LogicalToDeviceRelative(logical);

            FillCircle(brush, position, device.X);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw a fleet. We only draw fleets that are not in orbit. Indications of
        /// orbiting fleets are handled in the drawing of the star.
        /// </summary>
        /// <param name="fleet">The fleet to draw.</param>
        /// ----------------------------------------------------------------------------
        private void DrawFleet(Fleet fleet)
        {
            if (fleet.InOrbit == null)
            {
                Point position = LogicalToDevice(fleet.Position);

                graphics.TranslateTransform(position.X, position.Y);
                graphics.RotateTransform((float)fleet.Bearing);

                if (fleet.Owner == this.stateData.RaceName)
                {
                    graphics.FillPolygon(Brushes.Blue, triangle);
                }
                else
                {
                    graphics.FillPolygon(Brushes.Red, triangle);
                }

                graphics.ResetTransform();
            }

            if (fleet.Owner == this.stateData.RaceName)
            {
                Waypoint first = fleet.Waypoints[0] as Waypoint;
                Point from = LogicalToDevice(first.Position);

                foreach (Waypoint waypoint in fleet.Waypoints)
                {
                    Point position = waypoint.Position;
                    graphics.DrawLine(Pens.Blue, from, LogicalToDevice(position));
                    from = LogicalToDevice(position);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw a star. The star is just a small circle which is a bit bigger if we've
        /// explored it. 
        /// </summary>
        /// <remarks>
        /// The colour of the star symbol is based on its star report (reports for stars
        /// owned by the current player are always up-to-date). Unoccupied stars are
        /// white. Stars colonised by the player are green. Stars owned by other races
        /// are red.
        /// </remarks>
        /// <param name="star">The star sytem to draw.</param>
        /// ----------------------------------------------------------------------------
        private void DrawStar(Star star)
        {
            StarReport report = this.stateData.StarReports[star.Name] as StarReport;
            Point position = LogicalToDevice(star.Position);
            int size = 1;
            Brush starBrush = Brushes.White;
            string owner = "?";

            // Bigger symbol for explored stars.

            if (report != null)
            {
                size = 2;
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

            FillCircle(starBrush, position, size);

            // If the star name display is turned on then add the name

            if (this.displayStarNames)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                graphics.DrawString(
                star.Name, this.nameFont, Brushes.White, position, format);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add an indication of a starbase (circle) or orbiting fleets (smaller
        /// circle) or both.
        /// </summary>
        /// <param name="star">The star being drawn.</param>
        /// ----------------------------------------------------------------------------
        private void DrawOrbitingFleets(Star star)
        {
            StarReport report = this.stateData.StarReports[star.Name] as StarReport;
            Point position = LogicalToDevice(star.Position);
            int size = 16;

            if (report == null)
            {
                return;
            }

            if (report.Starbase != null)
            {
                graphics.DrawEllipse(
                    Pens.White,
                    position.X - (size / 2),
                    position.Y - (size / 2),
                    size,
                    size);
            }

            if (report.Age == 0 && report.OrbitingFleets)
            {
                size = 10;
                graphics.DrawEllipse(
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
        /// <summary>
        /// Build a list of all fleets that are visible to the player.
        /// </summary>
        /// <remarks>
        /// This consists of:
        /// (1) Fleets owned by the player
        /// (2) Fleets within the range of scanners on ships owned by the player
        /// (3) Fleets within the range of scanners on planets owned by the player
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void DetermineVisibleFleets()
        {
            ArrayList playersFleets = new ArrayList();

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
        /// <summary>
        /// Build a list of all Minefields that are visible to the player.
        /// </summary>
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
            ArrayList playersFleets = new ArrayList();

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
        /// <summary>
        /// Convert logical coordinates to device coordintes.
        /// </summary>
        /// <param name="p">The point to convert.</param>
        /// <returns>A converted point.</returns>
        /// ----------------------------------------------------------------------------
        private Point LogicalToDevice(Point p)
        {
            Point result = new Point();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X - this.origin.X) * minLength / this.extent.X;
            result.Y = (p.Y - this.origin.Y) * minLength / this.extent.Y;

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Convert logical coordinates to device coordinates by scaling only (all offsets are ignored).
        /// </summary>
        /// <param name="p">The point to convert.</param>
        /// <returns>The converted point.</returns>
        /// ----------------------------------------------------------------------------
        private Point LogicalToDeviceRelative(Point p)
        {
            Point result = new Point();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * minLength) / this.extent.X /* + BorderBuffer */;
            result.Y = (p.Y * minLength) / this.extent.Y /* + BorderBuffer */;

            return result;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Convert device coordinates to logical coordinates.
        /// </summary>
        /// <param name="p">The point to convert.</param>
        /// <returns>The converted point.</returns>
        /// ----------------------------------------------------------------------------
        private Point DeviceToLogical(Point p)
        {
            Point result = new Point();

            int minLength = Math.Min(this.MapPanel.Size.Width, this.MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * this.extent.X / minLength) + this.origin.X;
            result.Y = (p.Y * this.extent.Y / minLength) + this.origin.Y;

            return result;
        }

        #endregion

        #region Zoom

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process a request to zoom in the star map.
        /// </summary>
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
        /// <summary>
        /// Process a request to zoom out the star map.
        /// </summary>
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
        /// <summary>
        /// Zoom in or out of the star map.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void SetZoom()
        {
            this.extent.X = (int)(this.logical.X / this.zoomFactor);
            this.extent.Y = (int)(this.logical.Y / this.zoomFactor);

            MapHorizontalScroll(this.horizontalScroll);
            MapVerticalScroll(this.verticalScroll);

            this.MapPanel.Invalidate();


        }

        #endregion

        #region Scroll

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Horizontally scroll the star map.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MapScrollH(object sender, ScrollEventArgs e)
        {
            MapHorizontalScroll(e.NewValue);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Vertically scroll the star map.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void MapScrollV(object sender, ScrollEventArgs e)
        {
            MapVerticalScroll(e.NewValue);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Scroll the star map horizontally.
        /// </summary>
        /// <param name="value">new Hscroll position</param>
        /// ----------------------------------------------------------------------------
        private void MapHorizontalScroll(int value)
        {
            this.horizontalScroll = value;

            this.center.X = this.logical.X * this.horizontalScroll / 100;
            this.origin.X = this.center.X - (this.extent.X / 2);
            
            this.MapPanel.Invalidate();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Scroll the star map vertically.
        /// </summary>
        /// <param name="value">New Vscroll position.</param>
        /// ----------------------------------------------------------------------------
        private void MapVerticalScroll(int value)
        {
            this.verticalScroll = value;
            this.center.Y = this.logical.Y * this.verticalScroll / 100;
            this.origin.Y = this.center.Y - (this.extent.Y / 2);

            this.MapPanel.Invalidate();
        }

        #endregion

        #region Interface IComparable

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A sortable (by distance) version of the Item class.
        /// </summary>
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
        /// <summary>
        /// Process a mouse down event.
        /// </summary>
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
        /// <summary>
        /// Left Shift Mouse: Set Waypoints.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="snapToObject"></param>
        /// ----------------------------------------------------------------------------
        private void LeftShiftMouse(MouseEventArgs e, bool snapToObject)
        {
            Item item = MainWindow.Nova.SelectionDetail.Value;

            if (item == null || !(item is Fleet))
            {
                return;
            }

            // We've confirmed that the fleet detail control is displayed. Now get
            // a handle directly to it so that we can access its parameters
            // directly without having to use the detail summary panel as an agent

            FleetDetail fleetDetail = MainWindow.Nova.SelectionDetail.Control
                                      as FleetDetail;

            Point click = new Point(e.X, e.Y);
            Fleet fleet = item as Fleet;
            Point position = DeviceToLogical(click);
            ArrayList nearObjects = FindNearObjects(position);
            Waypoint waypoint = new Waypoint();

            waypoint.Position = position;
            waypoint.WarpFactor = 6;
            waypoint.Task = "None";

            // If there are no items near the selected position then set the
            // waypoint to just be a position in space. Otherwise, make the target
            // of the waypoint the selected item.
            //
            // To Do: Handle multiple items at the target location

            if (nearObjects.Count == 0 || snapToObject == false)
            {
                waypoint.Destination = "Space at " + position.ToString();
                waypoint.Position = position;
            }
            else
            {
                SortableItem selected = nearObjects[0] as SortableItem;
                Item target = selected.Target as Item;
                waypoint.Position = target.Position;
                waypoint.Destination = target.Name;
            }

            // If the new waypoint is the same as the last one then do nothing.

            int lastIndex = fleet.Waypoints.Count - 1;
            Waypoint lastWaypoint = fleet.Waypoints[lastIndex] as Waypoint;
            Point lastPosition = lastWaypoint.Position;

            if (waypoint.Destination == lastWaypoint.Destination)
            {
                return;
            }

            // Draw the new waypoint on the map and add it to the list of
            // waypoints for the fleet.

            Point from = LogicalToDevice(lastPosition);
            Point to = LogicalToDevice(waypoint.Position);
            Graphics g = this.MapPanel.CreateGraphics();

            g.DrawLine(Pens.Yellow, from, to);
            g.Dispose();

            fleet.Waypoints.Add(waypoint);
            fleetDetail.AddWaypoint(waypoint);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Left mouse button: select objects.
        /// </summary>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void LeftMouse(MouseEventArgs e)
        {
            Point position = new Point();
            Point click = new Point();
            click.X = e.X;
            click.Y = e.Y;
            position = DeviceToLogical(click);

            SetCursor(position);
            this.MapPanel.Invalidate();

            ArrayList nearObjects = FindNearObjects(position);
            if (nearObjects.Count == 0) return;

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
            SortableItem selected = nearObjects[this.selection] as SortableItem;
            Item item = (Item)selected.Target;
            this.cursorPosition = item.Position;

            MainWindow.Nova.SelectionSummary.Value = item;
            MainWindow.Nova.SelectionDetail.Value = item;
        }

        #endregion

        #region Misc Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the position of the star map selection cursor.
        /// </summary>
        /// <param name="position">Whete the cursor is to be put.</param>
        /// ----------------------------------------------------------------------------
        public void SetCursor(Point position)
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
            this.MapPanel.Invalidate();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Provides a list of objects within a certain distance from a position,
        /// ordered by distance.
        /// </summary>
        /// <param name="position">Starting point for the search.</param>
        /// <returns>ArrayList of Fleet and Star objects.</returns>
        /// ----------------------------------------------------------------------------
        private ArrayList FindNearObjects(Point position)
        {
            ArrayList nearObjects = new ArrayList();

            foreach (Fleet fleet in this.turnData.AllFleets.Values)
            {
                if (fleet.Type != "Starbase")
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
        /// <summary>
        /// Toggle the display of the star names.
        /// </summary>
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

            this.MapPanel.Invalidate();
        }

        #endregion
    }
}
