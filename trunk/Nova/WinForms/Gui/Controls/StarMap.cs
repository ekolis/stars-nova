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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;

using Nova.Common;
using Nova.Client;
#endregion

namespace Nova.WinForms.Gui
{
    public partial class StarMap : UserControl
    {
        public const int BorderBuffer = 35;

        #region Variables
        private Bitmap      CursorBitmap   = null;
        private Intel       TurnData       = null;
        private ClientState StateData      = null;
        private Point       CursorPosition = new Point(0, 0);
        private Point       LastClick      = new Point(0, 0);
        private Point       Logical        = new Point(0, 0);  // Size of the logical co-ordinate system (size of the game universe).
        private Point       Origin         = new Point(0, 0);  // Top left starting point of the displayed map within the logical map.
        private Point       Center         = new Point(0, 0);  // Focal point of the displayed map.
        private Point       Extent         = new Point(0, 0);  // Extent of the currently displayed map, from Origin.
        private double      ZoomFactor     = 0.8;              // Is used to adjust the Extent of the map.
        private Graphics    graphics       = null;
        private bool        IsInitialised  = false;
        private bool        DisplayStarNames = true;
        private int         Hscroll        = 50; // 0 to 100, used to position the Center, initially centered
        private int         Vscroll        = 50; // 0 to 100, used to position the Center, initially centered
        private int         Selection      = 0;
        private Hashtable   VisibleFleets  = new Hashtable();
        private Hashtable   VisibleMinefields = new Hashtable();
        private Font        NameFont = null;
        System.Drawing.BufferedGraphicsContext bufferedContext = null;
        #endregion

        private Point[] triangle = {new Point(0,   0), new Point(-5, -10),
                                   new Point(5, -10)};


        #region Construction and Initialization

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public StarMap()
        {
            bufferedContext = new BufferedGraphicsContext();

            InitializeComponent();
            GameSettings.Restore();

            // Initial map size
            Logical.X = GameSettings.Data.MapWidth;
            Logical.Y = GameSettings.Data.MapHeight;

            Extent.X = (int) (Logical.X * ZoomFactor);
            Extent.Y = (int) (Logical.Y * ZoomFactor);

            CursorBitmap = Nova.Properties.Resources.Cursor;
            CursorBitmap.MakeTransparent(Color.Black);

            NameFont = new Font("Arial", (float)7.5, FontStyle.Regular,
                                GraphicsUnit.Point);

            MapPanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

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
            StateData = ClientState.Data;
            TurnData = StateData.InputTurn;
            IsInitialised = true;
            /*string sv = this.ZoomIn.Visible.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string se = this.ZoomIn.Enabled.ToString(System.Globalization.CultureInfo.InvariantCulture);*/

            HScrollBar.Enabled = true;
            VScrollBar.Enabled = true;

            ZoomIn.Enabled = true;
            ZoomIn.Visible = true;
            ZoomIn.Invalidate();
            ZoomIn.Refresh();

            DetermineVisibleFleets();
            DetermineVisibleMinefields();

            // center the view
            Hscroll = 50;
            Vscroll = 50;
            Center.X = Logical.X / 2;
            Center.Y = Logical.Y / 2;
            ZoomFactor = 0.8; // ensure the whole map can be seen.
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
            //MapPanel.SuspendLayout();
            MapPanel.Invalidate();
            //MapPanel.ResumeLayout();
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
            base.OnPaint(eventData); //added

            if (IsInitialised == false)
            {
                return;
            }

            //System.Drawing.Rectangle rect = new Rectangle(0,0,(int)eventData.Graphics.DpiX,(int)eventData.Graphics.DpiY);
            System.Drawing.Rectangle rect2 = new Rectangle(0, 0, MapPanel.Width, MapPanel.Height);
            System.Drawing.BufferedGraphics bg = bufferedContext.Allocate(eventData.Graphics, rect2);

            graphics = bg.Graphics;
            //graphics = eventData.Graphics;

            //graphics.DrawImage(Nova.Properties.Resources.Plasma, 0, 0);
            Point backgroundOrigin = LogicalToDevice(new Point(0, 0));
            Point backgroundExtent = LogicalToDeviceRelative(new Point(Logical.X, Logical.Y));
            graphics.DrawImage(Nova.Properties.Resources.Plasma, backgroundOrigin.X, backgroundOrigin.Y, backgroundExtent.X, backgroundExtent.Y);

            MapPanel.BackgroundImage = Nova.Properties.Resources.Plasma;
            MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

            Color lrScanColour = Color.FromArgb(128, 128, 0, 0);
            SolidBrush lrScanBrush = new SolidBrush(lrScanColour);

            Color srScanColour = Color.FromArgb(128, 128, 128, 0);
            SolidBrush srScanBrush = new SolidBrush(srScanColour);

            // (1a) Planetary long-range scanners.

            foreach (Star star in TurnData.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    DrawCircle(lrScanBrush, star.Position, star.ScanRange);
                }
            }

            // (1b) Fleet long-range scanners.

            foreach (Fleet fleet in VisibleFleets.Values)
            {
                if (fleet.Owner == StateData.RaceName)
                {
                    DrawCircle(lrScanBrush, fleet.Position, fleet.LongRangeScan);
                }
            }

            // (2) Fleet short-range scanners.

            foreach (Fleet fleet in VisibleFleets.Values)
            {
                if (fleet.Owner == StateData.RaceName)
                {
                    DrawCircle(srScanBrush, fleet.Position, fleet.ShortRangeScan);
                }
            }

            // (3) Minefields

            foreach (Minefield Minefield in VisibleMinefields.Values)
            {
                Color cb;
                Color cf;

                if (Minefield.Owner == ClientState.Data.RaceName)
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
                int radius = Minefield.NumberOfMines;
                DrawCircle(srMineBrush, Minefield.Position, radius);
            }


            // (4) Visible fleets.

            foreach (Fleet fleet in VisibleFleets.Values)
            {
                if (fleet.Type != "Starbase")
                {
                    DrawFleet(fleet);
                }
            }

            // (5) Stars plus starbases and orbiting fleet indications that are
            // the results of scans.

            foreach (Star star in TurnData.AllStars.Values)
            {
                DrawStar(star);
                DrawOrbitingFleets(star);
            }

            Point position;
            foreach (Fleet fleet in StateData.PlayerFleets)
            {
                if (fleet.InOrbit != null)
                {
                    position = LogicalToDevice(fleet.Position);
                    int size = 10;
                    graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                         position.Y - (size / 2), size, size);
                }
            }

            // (6) Cursor.

            position = LogicalToDevice(CursorPosition);
            position.X -= (CursorBitmap.Width / 2) + 1;
            position.Y += 2;
            graphics.DrawImage(CursorBitmap, position);

            // (7) Zoom/Scroll/Cursor info for debugging.
#if (DEBUG)
            Font F = new Font(ZoomIn.Font.Name, ZoomIn.Font.Size, ZoomIn.Font.Style, ZoomIn.Font.Unit);

            Color coordColour = Color.FromArgb(255, 255, 255, 0);
            SolidBrush coordBrush = new SolidBrush(coordColour);
            string s2 = "Cursor Location (logical): " + CursorPosition.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + CursorPosition.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(s2, F, coordBrush, 0, 20);
            string s = "Cursor Location (device): " + position.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(s, F, coordBrush, 0, 0);
            string zoomDebugMsg = "Zoom Facor: " + ZoomFactor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(zoomDebugMsg, F, coordBrush, 0, 40);
            string HScrollDebugMsg = "Hscroll: " + Hscroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(HScrollDebugMsg, F, coordBrush, 0, 60);
            string VScrollDebugMsg = "Vscroll: " + Vscroll.ToString(System.Globalization.CultureInfo.InvariantCulture);
            graphics.DrawString(VScrollDebugMsg, F, coordBrush, 0, 80);
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
            graphics.FillEllipse(brush, position.X - radius, position.Y - radius,
                                 radius * 2, radius * 2);
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

                if (fleet.Owner == StateData.RaceName)
                {
                    graphics.FillPolygon(Brushes.Blue, triangle);
                }
                else
                {
                    graphics.FillPolygon(Brushes.Red, triangle);
                }

                graphics.ResetTransform();
            }

            if (fleet.Owner == StateData.RaceName)
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
            StarReport report = StateData.StarReports[star.Name] as StarReport;
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

            if (owner == StateData.RaceName)
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

            if (DisplayStarNames)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                graphics.DrawString(
                star.Name, NameFont, Brushes.White, position, format);
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
            StarReport report = StateData.StarReports[star.Name] as StarReport;
            Point position = LogicalToDevice(star.Position);
            int size = 16;

            if (report == null)
            {
                return;
            }

            if (report.Starbase != null)
            {
                graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                     position.Y - (size / 2), size, size);
            }

            if (report.Age == 0 && report.OrbitingFleets)
            {
                size = 10;
                graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                     position.Y - (size / 2), size, size);
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

            foreach (Fleet fleet in TurnData.AllFleets.Values)
            {
                if (fleet.Owner == StateData.RaceName)
                {
                    VisibleFleets[fleet.Key] = fleet;
                    playersFleets.Add(fleet);
                }
            }

            // -------------------------------------------------------------------
            // (2) Not so easy. Fleets within the scanning range of the player's
            // Fleets.
            // -------------------------------------------------------------------

            foreach (Fleet fleet in playersFleets)
            {
                foreach (Fleet scan in TurnData.AllFleets.Values)
                {
                    double range = 0;
                    range = PointUtilities.Distance(fleet.Position, scan.Position);
                    if (range <= fleet.LongRangeScan)
                    {
                        VisibleFleets[scan.Key] = scan;
                    }
                }
            }

            // -------------------------------------------------------------------
            // (3) Now that we know how to deal with ship scanners planet scanners
            // are just the same.
            // -------------------------------------------------------------------

            foreach (Star star in TurnData.AllStars.Values)
            {
                if (star.Owner == StateData.RaceName)
                {
                    foreach (Fleet scanned in TurnData.AllFleets.Values)
                    {
                        if (PointUtilities.Distance(star.Position, scanned.Position)
                            <= star.ScanRange)
                        {
                            VisibleFleets[scanned.Key] = scanned;
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

            foreach (Fleet fleet in TurnData.AllFleets.Values)
            {
                if (fleet.Owner == StateData.RaceName)
                {
                    playersFleets.Add(fleet);
                }
            }

            // -------------------------------------------------------------------
            // (1) First the easy one. Minefields owned by the player.
            // -------------------------------------------------------------------

            foreach (Minefield minefield in TurnData.AllMinefields.Values)
            {
                if (minefield.Owner == StateData.RaceName)
                {
                    VisibleMinefields[minefield.Key] = minefield;
                }
            }

            // -------------------------------------------------------------------
            // (2) Not so easy. Minefields within the scanning range of the
            // player's ships.
            // -------------------------------------------------------------------

            foreach (Fleet fleet in playersFleets)
            {
                foreach (Minefield minefield in TurnData.AllMinefields.Values)
                {

                    bool isIn = PointUtilities.CirclesOverlap(fleet.Position,
                                                        minefield.Position,
                                                        fleet.LongRangeScan,
                                                        minefield.Radius);

                    if (isIn == true)
                    {
                        VisibleMinefields[minefield.Key] = minefield;
                    }
                }
            }

            // -------------------------------------------------------------------
            // (3) Now that we know how to deal with ship scanners planet scanners
            // are just the same.
            // -------------------------------------------------------------------

            foreach (Minefield minefield in TurnData.AllMinefields.Values)
            {
                foreach (Star star in TurnData.AllStars.Values)
                {
                    if (star.Owner == StateData.RaceName)
                    {

                        bool isIn = PointUtilities.CirclesOverlap(star.Position,
                                                            minefield.Position,
                                                            star.ScanRange,
                                                            minefield.Radius);

                        if (isIn == true)
                        {
                            VisibleMinefields[minefield.Key] = minefield;
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

            int MinLength = Math.Min(MapPanel.Size.Width, MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X - Origin.X) * MinLength / Extent.X;
            result.Y = (p.Y - Origin.Y) * MinLength / Extent.Y;

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

            int MinLength = Math.Min(MapPanel.Size.Width, MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * MinLength) / Extent.X /* + BorderBuffer */;
            result.Y = (p.Y * MinLength) / Extent.Y /* + BorderBuffer */;

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

            int MinLength = Math.Min(MapPanel.Size.Width, MapPanel.Size.Height); // maintain 1:1 aspect ratio

            result.X = (p.X * Extent.X / MinLength) + Origin.X;
            result.Y = (p.Y * Extent.Y / MinLength) + Origin.Y;

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
            ZoomFactor *= 1.6;
            ZoomOut.Enabled = true;
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
            ZoomFactor /= 1.6;
            if (ZoomFactor < 0.5)
            {
                ZoomFactor = 0.5;
                ZoomOut.Enabled = false;
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
            Extent.X = (int) (Logical.X / ZoomFactor);
            Extent.Y = (int) (Logical.Y / ZoomFactor);

            MapHorizontalScroll(Hscroll);
            MapVerticalScroll(Vscroll);

            MapPanel.Invalidate();


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
            Hscroll = value;

            Center.X = Logical.X * Hscroll / 100;
            Origin.X = Center.X - (Extent.X / 2);
            
            MapPanel.Invalidate();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Scroll the star map vertically.
        /// </summary>
        /// <param name="value">New Vscroll position.</param>
        /// ----------------------------------------------------------------------------
        private void MapVerticalScroll(int value)
        {
            Vscroll = value;
            Center.Y = Logical.Y * Vscroll / 100;
            Origin.Y = Center.Y - (Extent.Y / 2);

            MapPanel.Invalidate();
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

            public int CompareTo(Object rightHandSide)
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
            Graphics g = MapPanel.CreateGraphics();

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
            //MapPanel.SuspendLayout();
            MapPanel.Invalidate();
            //MapPanel.ResumeLayout();

            ArrayList nearObjects = FindNearObjects(position);
            if (nearObjects.Count == 0) return;

            // If the mouse hasn't moved since the last selection cycle through
            // the list of near objects. If it has, start at the beginning of the
            // list.

            if ((Math.Abs(LastClick.X - click.X) > 10) ||
                (Math.Abs(LastClick.Y - click.Y) > 10))
            {
                Selection = 0;
            }
            else
            {
                Selection++;
                if (Selection >= nearObjects.Count)
                {
                    Selection = 0;
                }
            }

            LastClick = click;
            SortableItem selected = nearObjects[Selection] as SortableItem;
            Item item = (Item)selected.Target;
            CursorPosition = item.Position;

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
            CursorPosition = position;

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
            MapPanel.Invalidate();
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

            foreach (Fleet fleet in TurnData.AllFleets.Values)
            {
                if (fleet.Type != "Starbase")
                {
                    if (PointUtilities.IsNear(fleet.Position, position))
                    {
                        SortableItem thisItem = new SortableItem();
                        thisItem.Target = fleet;
                        thisItem.Distance = PointUtilities.Distance
                           (position, fleet.Position);
                        nearObjects.Add(thisItem);
                    }
                }
            }

            foreach (Star star in TurnData.AllStars.Values)
            {
                if (PointUtilities.IsNear(star.Position, position))
                {

                    SortableItem thisItem = new SortableItem();
                    thisItem.Target = star;
                    thisItem.Distance = PointUtilities.Distance
                                            (position, star.Position);

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
                DisplayStarNames = true;
            }
            else
            {
                DisplayStarNames = false;
            }

            MapPanel.Invalidate();
        }

        #endregion
    }
}
