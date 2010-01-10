// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module maintains the star map.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;

namespace Nova
{   
   public partial class StarMap : UserControl
   {
      private Bitmap     CursorBitmap      = null;
      private Intel TurnData          = null;
      private GuiState   StateData         = null;
      private Point      CursorPosition    = new Point(0, 0);
      private Point      Extent            = new Point(0, 0);
      private Point      LastClick         = new Point(0, 0);
      private Point      Logical           = new Point(0, 0);
      private Point      Origin            = new Point(0, 0);
      private Graphics   graphics          = null;
      private bool       IsInitialised     = false;
      private bool       DisplayStarNames  = true;
      private int        Hscroll           = 0;
      private int        Vscroll           = 0;
      private int        ZoomFactor        = 1;
      private int        Selection         = 0;
      private Hashtable  VisibleFleets     = new Hashtable();
      private Hashtable  VisibleMinefields = new Hashtable();
      private Font       NameFont          = null;
      System.Drawing.BufferedGraphicsContext bg_ctxt = null;

      private  Point[] triangle = {new Point(0,   0), new Point(-5, -10),
                                   new Point(5, -10)};


// ============================================================================
// Construction 
// ============================================================================

      public StarMap()
      {
         bg_ctxt = new BufferedGraphicsContext();      

         InitializeComponent();

         Extent.X  = Global.UniverseSize;
         Extent.Y  = Global.UniverseSize;
         Logical.X = Global.UniverseSize;
         Logical.Y = Global.UniverseSize;

         CursorBitmap = Nova.Resources.Cursor;
         CursorBitmap.MakeTransparent(Color.Black);

         NameFont = new Font("Arial", (float) 7.5, FontStyle.Regular, 
                             GraphicsUnit.Point);

         MapPanel.BackgroundImage       = Nova.Resources.Plasma;
         MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

         //SetStyle(ControlStyles.UserPaint, true);
         //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
         //UpdateStyles();         
     }


// ============================================================================
// Post-construction initialisation.
// ============================================================================

      public void Initialise()
      {
         StateData     = GuiState.Data;          
         TurnData      = StateData.InputTurn;
         IsInitialised = true;
         /*string sv = this.ZoomIn.Visible.ToString(System.Globalization.CultureInfo.InvariantCulture);
         string se = this.ZoomIn.Enabled.ToString(System.Globalization.CultureInfo.InvariantCulture);
         ZoomIn.Enabled = true;
         ZoomIn.Visible = true;
         ZoomIn.Invalidate();
         ZoomIn.Refresh();*/


         DetermineVisibleFleets();
         DetermineVisibleMinefields();         
      }


// ============================================================================
// Paint the star map window.
//
// We just do simple painting so the order that things are drawn is important
// (otherwise items may get overwritten and become invisible):
//
// (1) All long-range scanners (planets and ships) owned by the player.
// (2) All short-range scanners (ships only) owned by the player.
// (3) Minefields visible to the player (with transparency)
// (4) All fleets visible to the player.
// (5) Stars (including a starbase and orbiting fleets indication).
// (6) The selection cursor.
// ============================================================================

      private  void OnPaint(object sender, PaintEventArgs eventData)
      {
         base.OnPaint(eventData); //added

         if (IsInitialised == false)
         {           
            return;
         }
                         
         //System.Drawing.Rectangle rect = new Rectangle(0,0,(int)eventData.Graphics.DpiX,(int)eventData.Graphics.DpiY);
         System.Drawing.Rectangle rect2 = new Rectangle(0, 0, MapPanel.Width, MapPanel.Height);
         System.Drawing.BufferedGraphics bg = bg_ctxt.Allocate(eventData.Graphics, rect2);
         
         graphics = bg.Graphics;
         //graphics = eventData.Graphics;

         graphics.DrawImage(Nova.Resources.Plasma, 0 , 0);

         MapPanel.BackgroundImage = Nova.Resources.Plasma;
         MapPanel.BackgroundImageLayout = ImageLayout.Stretch;

         Color      lrScanColour = Color.FromArgb(128, 128, 0, 0);
         SolidBrush lrScanBrush  = new SolidBrush(lrScanColour);

         Color      srScanColour = Color.FromArgb(128, 128, 128, 0);
         SolidBrush srScanBrush  = new SolidBrush(srScanColour);

         // (1a) Planetary long-range scanners.

         foreach (Star star in TurnData.AllStars.Values) {
            if (star.Owner == GuiState.Data.RaceName) {
               DrawCircle(lrScanBrush, star.Position, star.ScanRange);
            }
         }

         // (1b) Fleet long-range scanners.

         foreach (Fleet fleet in VisibleFleets.Values) {
            if (fleet.Owner == StateData.RaceName) {
               DrawCircle(lrScanBrush, fleet.Position, fleet.LongRangeScan);
            }
         }

         // (2) Fleet short-range scanners.

         foreach (Fleet fleet in VisibleFleets.Values) {
            if (fleet.Owner == StateData.RaceName) {
               DrawCircle(srScanBrush, fleet.Position, fleet.ShortRangeScan);
            }
         }

         // (3) Minefields

         foreach (Minefield Minefield in VisibleMinefields.Values) {
             Color cb;
             Color cf;

             if (Minefield.Owner == GuiState.Data.RaceName)
             {
                 cb = Color.FromArgb(0,   0,   0, 0);
                 cf = Color.FromArgb(128, 0, 128, 0);
             }
             else {
                 cb = Color.FromArgb(0, 0, 0, 0);
                 cf = Color.FromArgb(128, 128, 0, 128);
             }


            HatchStyle style = HatchStyle.DiagonalCross | HatchStyle.Percent50;
            HatchBrush srMineBrush = new HatchBrush(style, cf, cb);
            int radius             = Minefield.NumberOfMines; 
            DrawCircle(srMineBrush, Minefield.Position, radius);
         }


         // (4) Visible fleets.

         foreach (Fleet fleet in VisibleFleets.Values) {
            if (fleet.Type != "Starbase") {
               DrawFleet(fleet);
            }
         }

         // (5) Stars plus starbases and orbiting fleet indications that are
         // the results of scans.

         foreach (Star star in TurnData.AllStars.Values) {
            DrawStar(star);
            DrawOrbitingFleets(star);
         }

         Point position;
         foreach (Fleet fleet in StateData.PlayerFleets) {
            if (fleet.InOrbit != null) {
               position  = LogicalToDevice(fleet.Position);
               int size  = 10;
               graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                    position.Y - (size / 2), size, size);
            }
         }

         // (6) Cursor.

         position = LogicalToDevice(CursorPosition);
         position.X -= (CursorBitmap.Width / 2) + 1;
         position.Y += 2;
         graphics.DrawImage(CursorBitmap, position);
         //quick font
         Font F = new Font(ZoomIn.Font.Name, ZoomIn.Font.Size, ZoomIn.Font.Style, ZoomIn.Font.Unit);
         
         string s = position.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + position.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
         Color      coordColour = Color.FromArgb(255, 255, 255, 0);
         SolidBrush coordBrush  = new SolidBrush(coordColour);
         graphics.DrawString(s, F, coordBrush, 0, 0);
         string s2 = CursorPosition.X.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + CursorPosition.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
         graphics.DrawString(s2, F, coordBrush, 0, 20);


         bg.Render();
      }


// ============================================================================
// Draw a filled circle using device coordinates.
// ============================================================================

      private void FillCircle(Brush brush, Point position, int radius)
      {
         graphics.FillEllipse(brush, position.X - radius, position.Y - radius,
                              radius * 2, radius * 2);
      }


// ============================================================================
// Draw a filled circle using logical coordinates.
// ============================================================================

      private void DrawCircle(Brush brush, Point where, int radius)
      {
         if (radius == 0) return;

         Point position = LogicalToDevice(where);
         Point logical  = new Point(radius, 0);
         Point device   = LogicalToDeviceRelative(logical);

         FillCircle(brush, position, device.X);
      }


// ============================================================================
// Draw a fleet. We only draw fleets that are not in orbit. Indications of
// orbiting fleets are handled in the drawing of the star.
// ============================================================================

      private void DrawFleet(Fleet fleet)
      {
         if (fleet.InOrbit == null) {
            Point position = LogicalToDevice(fleet.Position);

            graphics.TranslateTransform(position.X, position.Y);
            graphics.RotateTransform((float)fleet.Bearing);

            if (fleet.Owner == StateData.RaceName) {
               graphics.FillPolygon(Brushes.Blue, triangle);
            }
            else {
               graphics.FillPolygon(Brushes.Red, triangle);
            }

            graphics.ResetTransform();
         }

         if (fleet.Owner == StateData.RaceName) {
            Waypoint first = fleet.Waypoints[0] as Waypoint;
            Point from     = LogicalToDevice(first.Position);

            foreach (Waypoint waypoint in fleet.Waypoints) {
               Point position = waypoint.Position;
               graphics.DrawLine(Pens.Blue, from, LogicalToDevice(position));
               from = LogicalToDevice(position);
            }
         }
      }


// ============================================================================
// Draw a star. The star is just a small circle which is a bit bigger if we've
// explored it. 
//
// The colour of the star symbol is based on its star report (reports for stars
// owned by the current player are always up-to-date). Unoccupied stars are
// white. Stars colonised by the player are green. Stars owned by other races
// are red.
// ============================================================================

      private void DrawStar(Star star)
      {
         StarReport report    = StateData.StarReports[star.Name] as StarReport;
         Point      position  = LogicalToDevice(star.Position);
         int        size      = 1;
         Brush      starBrush = Brushes.White;
         string     owner     = "?";

         // Bigger symbol for explored stars.

         if (report != null) {
            size  = 2;
            owner = report.Owner;
         }

         // Our stars are greenish, other's are red, unknown or uncolonised
         // stars are white.

         if (owner == StateData.RaceName) {
            starBrush = Brushes.GreenYellow;
         }
         else {
            if (owner != null && owner != "?") {
               starBrush = Brushes.Red;
            }
         }

         FillCircle(starBrush, position, size);

         // If the star name display is turned on then add the name

         if (DisplayStarNames) {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            graphics.DrawString(
            star.Name, NameFont, Brushes.White, position, format);
         }
      }


// ============================================================================
// Add an indication of a starbase (circle) or orbiting fleets (smaller
// circle) or both.
// ============================================================================

      private void DrawOrbitingFleets(Star star)
      {
         StarReport report    = StateData.StarReports[star.Name] as StarReport;
         Point      position  = LogicalToDevice(star.Position);
         int        size      = 16;

         if (report == null) {
            return;
         }

         if (report.Starbase != null) {
            graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                 position.Y - (size / 2), size, size);
         }

         if (report.Age == 0 && report.OrbitingFleets) {
            size = 10;
            graphics.DrawEllipse(Pens.White, position.X - (size / 2),
                                 position.Y - (size / 2), size, size);
         }
      }


// ============================================================================
// Build a list of all fleets that are visible to the player. This consists of:
//
// (1) Fleets owned by the player
// (2) Fleets within the range of scanners on ships owned by the player
// (3) Fleets within the range of scanners on planets owned by the player
// ============================================================================

      private void DetermineVisibleFleets()
      {
         ArrayList playersFleets = new ArrayList();

         // -------------------------------------------------------------------
         // (1) First the easy one. Fleets owned by the player.
         // -------------------------------------------------------------------

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            if (fleet.Owner == StateData.RaceName) {
               VisibleFleets[fleet.Key] = fleet;
               playersFleets.Add(fleet);
            }
         } 

         // -------------------------------------------------------------------
         // (2) Not so easy. Fleets within the scanning range of the player's
         // Fleets.
         // -------------------------------------------------------------------

         foreach (Fleet fleet in playersFleets) {
            foreach (Fleet scan in TurnData.AllFleets.Values) {
               double range = 0;
               range = PointUtilities.Distance(fleet.Position, scan.Position);
               if (range <= fleet.LongRangeScan) {
                  VisibleFleets[scan.Key] = scan;
               }
            }
         }

         // -------------------------------------------------------------------
         // (3) Now that we know how to deal with ship scanners planet scanners
         // are just the same.
         // -------------------------------------------------------------------

         foreach (Star star in TurnData.AllStars.Values) {
            if (star.Owner == StateData.RaceName) {
               foreach (Fleet scanned in TurnData.AllFleets.Values) {
                  if (PointUtilities.Distance(star.Position, scanned.Position)
                      <= star.ScanRange) {
                     VisibleFleets[scanned.Key] = scanned;
                  }
               }
            }
         }
      }


// ============================================================================
// Build a list of all Minefields that are visible to the player. This consists
// of:
//
// (1) Minefields owned by the player
// (2) Minefiels within the range of scanners on ships owned by the player
// (3) Minefields within the range of scanners on planets owned by the player
// ============================================================================

      private void DetermineVisibleMinefields()
      {
         ArrayList playersFleets = new ArrayList();

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            if (fleet.Owner == StateData.RaceName) {
               playersFleets.Add(fleet);
            }
         } 

         // -------------------------------------------------------------------
         // (1) First the easy one. Minefields owned by the player.
         // -------------------------------------------------------------------

         foreach (Minefield minefield in TurnData.AllMinefields.Values) {
            if (minefield.Owner == StateData.RaceName) {
               VisibleMinefields[minefield.Key] = minefield;
            }
         }

         // -------------------------------------------------------------------
         // (2) Not so easy. Minefields within the scanning range of the
         // player's ships.
         // -------------------------------------------------------------------

         foreach (Fleet fleet in playersFleets) {
            foreach (Minefield minefield in TurnData.AllMinefields.Values) {

               bool isIn = PointUtilities.CirclesOverlap(fleet.Position, 
                                                   minefield.Position,
                                                   fleet.LongRangeScan,
                                                   minefield.Radius);

               if (isIn == true) {
                  VisibleMinefields[minefield.Key] = minefield;
               }
            }
         }

         // -------------------------------------------------------------------
         // (3) Now that we know how to deal with ship scanners planet scanners
         // are just the same.
         // -------------------------------------------------------------------

         foreach (Minefield minefield in TurnData.AllMinefields.Values) {
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


// ============================================================================
// Convert logical coordinates to device coordintes.
// ============================================================================

      private Point LogicalToDevice(Point p)
      {
         Point result = new Point();

         result.X = ((p.X - Origin.X) * MapPanel.Size.Width) /  Extent.X;
         result.Y = ((p.Y - Origin.Y) * MapPanel.Size.Height) / Extent.Y;

         return result;
      }


// ============================================================================
// Convert logical coordinates to device coordinates.
// ============================================================================

      private   Point LogicalToDeviceRelative(Point p)
      {
         Point result = new Point();

         result.X = (p.X * MapPanel.Size.Width)  / Extent.X;
         result.Y = (p.Y * MapPanel.Size.Height) / Extent.Y;

         return result;
      }


// ============================================================================
// Convert device coordinates to logical coordinates.
// ============================================================================

      private  Point DeviceToLogical(Point p)
      {
         Point result = new Point();

         result.X = Origin.X + ((Extent.X * p.X) / MapPanel.Size.Width);
         result.Y = Origin.Y + ((Extent.Y * p.Y) / MapPanel.Size.Height);

         return result;
      }


// ============================================================================
// ReadIntel a request to zoom in the star map.
// ============================================================================

      public void ZoomInClick(object sender, System.EventArgs e)
      {
         ZoomFactor *= 2;
         if (ZoomFactor == 2) {
            HScrollBar.Enabled = true;
            VScrollBar.Enabled = true;
            ZoomOut.Enabled    = true;
         }
   
         SetZoom();
      }


// ============================================================================
// ReadIntel a request to zoom out the star map.
// ============================================================================

      public void ZoomOutClick(object sender, System.EventArgs e)
      {
         if (ZoomFactor == 1) {
            return;
         }
   
         ZoomFactor /= 2;
         if (ZoomFactor == 1) {
            HScrollBar.Enabled = false;
            VScrollBar.Enabled = false;
            ZoomOut.Enabled     = false;
         }
   
         SetZoom();
      }

   
// ============================================================================
// Zoom in or out of the star map.
// ============================================================================

      private void SetZoom()
      {
         Extent.X = Logical.X / ZoomFactor;
         Extent.Y = Logical.Y / ZoomFactor;

         MapHorizontalScroll(Hscroll); 
         MapVerticalScroll(Vscroll);   

         MapPanel.Invalidate();


      }


// ============================================================================
// Horizontally scroll the star map.
// ============================================================================

      private void MapScrollH(object sender, ScrollEventArgs e)
      {
         MapHorizontalScroll(e.NewValue*3);
      }


// ============================================================================
// Vertically scroll the star map.
// ============================================================================

      private void MapScrollV(object sender, ScrollEventArgs e)
      {
         MapVerticalScroll(e.NewValue*3);
      }


// ============================================================================
// Scroll the star map horizontally.
// ============================================================================

      private void MapHorizontalScroll(int value)
      {
         Hscroll = value;
         Origin.X = ((Logical.X - Extent.X) * Hscroll) / 100;
         MapPanel.Invalidate();
      }


// ============================================================================
// Scroll the star map vertically.
// ============================================================================

      private void MapVerticalScroll(int value)
      {
         Vscroll = value;
         Origin.Y = ((Logical.Y - Extent.Y) * Vscroll) / 100;
         MapPanel.Invalidate();
      }


// ============================================================================
// A sortable (by distance) version of the Item class.
// ============================================================================

      public class SortableItem : IComparable
      {
         public double Distance;
         public Item   Target;

         public int CompareTo(Object rightHandSide)
         {
            SortableItem rhs = (SortableItem) rightHandSide;
            return this.Distance.CompareTo(rhs.Distance);
         }
      }


// ============================================================================
// Set the position of the star map selection cursor.
// ============================================================================

      public void SetCursor(Point position)
      {
         CursorPosition = position;

         // Set the scroll position so that the selected position of the cursor
         // in is the centre of the screen.

         float fractionX = position.X;
         float fractionY = position.Y;

         fractionX /= Logical.X;
         fractionY /= Logical.Y;

         Hscroll = (int) (fractionX * 100.0);
         Vscroll = (int) (fractionY * 100.0);

         MapPanel.Invalidate();
      }


// ============================================================================
// ReadIntel a mouse down event.
// ============================================================================

      private void StarMapMouse(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left) {
            if ((Control.ModifierKeys & Keys.Shift) != 0) {
               if ((Control.ModifierKeys & Keys.Control) != 0) {
                  LeftShiftMouse(e, false);
               }
               else {
                  LeftShiftMouse(e, true);
               }
            } 
            else {
               LeftMouse(e);
            }
         }
      }


// ============================================================================
// Left Shift Mouse: Set Waypoints.
// ============================================================================

      private  void LeftShiftMouse(MouseEventArgs e, bool snapToObject)
      {
         Item item = MainWindow.nova.SelectionDetail.Value;

         if (item == null || !(item is Fleet)) {
            return;
         }

         // We've confirmed that the fleet detail control is displayed. Now get
         // a handle directly to it so that we can access its parameters
         // directly without having to use the detail summary panel as an agent

         FleetDetail fleetDetail = MainWindow.nova.SelectionDetail.Control
                                   as FleetDetail;

         Point click           = new Point(e.X, e.Y);
         Fleet fleet           = item as Fleet;
         Point position        = DeviceToLogical(click);
         ArrayList nearObjects = FindNearObjects(position);
         Waypoint waypoint     = new Waypoint();

         waypoint.Position   = position;
         waypoint.WarpFactor = 6;
         waypoint.Task       = "None";

         // If there are no items near the selected position then set the
         // waypoint to just be a position in space. Otherwise, make the target
         // of the waypoint the selected item.
         //
         // To Do: Handle multiple items at the target location

         if (nearObjects.Count == 0 || snapToObject == false) {
            waypoint.Destination = "Space at " + position.ToString();
            waypoint.Position = position;
         }
         else {
            SortableItem selected = nearObjects[0] as SortableItem;
            Item target           = selected.Target as Item;
            waypoint.Position     = target.Position;
            waypoint.Destination  = target.Name;
         }

         // If the new waypoint is the same as the last one then do nothing.

         int lastIndex         = fleet.Waypoints.Count - 1;
         Waypoint lastWaypoint = fleet.Waypoints[lastIndex] as Waypoint;
         Point lastPosition    = lastWaypoint.Position;

         if (waypoint.Destination == lastWaypoint.Destination) {
            return;
         }

         // Draw the new waypoint on the map and add it to the list of
         // waypoints for the fleet.

         Point from    = LogicalToDevice(lastPosition);
         Point to      = LogicalToDevice(waypoint.Position);
         Graphics g    = MapPanel.CreateGraphics();

         g.DrawLine(Pens.Yellow, from, to);
         g.Dispose();

         fleet.Waypoints.Add(waypoint);
         fleetDetail.AddWaypoint(waypoint);
      }


// ============================================================================
// Left mouse button: select objects.
// ============================================================================

      private  void LeftMouse(MouseEventArgs e)
      {
         Point position = new Point();
         Point click    = new Point();
         click.X        = e.X;
         click.Y        = e.Y;
         position       = DeviceToLogical(click);

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
             (Math.Abs(LastClick.Y - click.Y) > 10)) {
            Selection = 0;
         }
         else {
            Selection++;
            if (Selection >= nearObjects.Count) {
               Selection = 0;
            }
         }

         LastClick             = click;
         SortableItem selected = nearObjects[Selection] as SortableItem;
         Item item             = (Item) selected.Target;
         CursorPosition        = item.Position;

         MainWindow.nova.SelectionSummary.Value = item;
         MainWindow.nova.SelectionDetail.Value  = item;
      }


// ============================================================================
// Provides a list of objects within a certain distance from a position,
// ordered by distance. 
// ============================================================================

      private  ArrayList FindNearObjects(Point position)
      {
         ArrayList nearObjects = new ArrayList();

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            if (fleet.Type != "Starbase") {
               if (PointUtilities.IsNear(fleet.Position, position)) {
                  SortableItem thisItem = new SortableItem();
                  thisItem.Target       = fleet;
                  thisItem.Distance     = PointUtilities.Distance
                     (position, fleet.Position);
                  nearObjects.Add(thisItem);
               }
            }
         }

         foreach (Star star in TurnData.AllStars.Values) {
            if (PointUtilities.IsNear(star.Position, position)) {

               SortableItem thisItem = new SortableItem();
               thisItem.Target       = star;
               thisItem.Distance     = PointUtilities.Distance
                                       (position, star.Position);

               nearObjects.Add(thisItem);
            }
         }

         nearObjects.Sort();
         return nearObjects;
      }


// ============================================================================
// Toggle the display of the star names.
// ============================================================================

      private void ToggleNames_CheckedChanged(object sender, EventArgs e)
      {
         CheckBox toggleNames = sender as CheckBox;

         if (toggleNames.Checked) {
            DisplayStarNames = true;
         }
         else {
            DisplayStarNames = false;
         }

         MapPanel.Invalidate();
      }


// ============================================================================
// Utility function to instigate a display refresh
// ============================================================================

      public void MapRefresh()
      {
         //MapPanel.SuspendLayout();
         MapPanel.Invalidate();
         //MapPanel.ResumeLayout();
      }         
   }
}
