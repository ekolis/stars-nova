// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Some general utilities for handling points.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Drawing;

namespace NovaCommon
{


// ============================================================================
// Point Utility Functions.
// ===========================================================================

   public class PointUtilities
   {

      private static Random RandomNumber = new Random();


// ============================================================================
// Return a random position within a Rectangle. A border (which may be zero) is
// applied to the area where point positions will not be allocated. This
// ensures that returned points are never too close to the edge of the
// rectangle.
// ============================================================================

      public static Point GetPositionInBox(Rectangle box, int boxBorder)
      {
         int   boxSize   = box.Width;
         Point position  = new Point(box.X, box.Y);

         position.X += RandomNumber.Next(boxBorder, boxSize - boxBorder);
         position.Y += RandomNumber.Next(boxBorder, boxSize - boxBorder);
      
         return position;
      }
   
// ============================================================================
// Determine if a point is within a bounding box. 
// ============================================================================

      public static bool InBox(Point p, Rectangle box)
      {
         Point upperLeft   = box.Location;
         Point bottomRight = box.Location;
         
         bottomRight.Offset(box.Width, box.Height);

         if (((p.X > upperLeft.X)  && (p.X < bottomRight.X)) && 
             ((p.Y > upperLeft.Y)  && (p.Y < bottomRight.Y)) ) {
            return true;
         }

         return false;
      }

// ============================================================================
// Determine if two circles overlap.
//
// For two circles (position x,y and radius r) x1,y1,r1 and x2,y2,r2, if:
//
// (x2-x1)^2+(y2-y1)^2 < (r2+r1)^2
//
// Then the circles overlap. 
// ============================================================================

      public static bool CirclesOverlap(Point p1,
                                        Point p2, 
                                        double r1,
                                        double r2)
      {
         double x1 = p1.X;
         double x2 = p2.X;
         double y1 = p1.Y;
         double y2 = p2.Y;

         double f1 = Math.Pow((x2 - x1), 2);
         double f2 = Math.Pow((y2 - y1), 2);
         double f3 = Math.Pow((r2 + r1), 2);

         if ((f1 + f2) < f3) {
            return true;
         }

         return false;
      }


// ============================================================================
// Determine if two positions are "near"
// ============================================================================

      public static bool IsNear (Point position1, Point position2) 
      {
         Point topCorner = position1;
         topCorner.Offset(-20, -20);
         Rectangle scanArea  = new Rectangle(topCorner, new Size(40, 40));

         if (InBox(position2, scanArea)) {
            return true;
         }

         return false;
      }


// ============================================================================
// Calculate the distance between two points,
// ============================================================================

      public static double Distance(Point start, Point end) 
      {
         double xo       = Math.Abs(start.X - end.X);
         double yo       = Math.Abs(start.Y - end.Y);
         double distance = Math.Sqrt(((xo * xo) + (yo * yo)));

         return distance;
      }


// ============================================================================
// Move a position some distance nearer to another point.
// FIXME - rounding can cause no movement to occur.
// ============================================================================

      public static Point MoveTo(Point from, Point to, double distance)
      {
         Point result = new Point();
         result       = from;

         double my    = to.Y - from.Y;
         double mx    = to.X - from.X;
         double theta = Math.Atan2(my, mx);
         double dx    = distance * Math.Cos(theta);
         double dy    = distance * Math.Sin(theta);

         result.X += (int) dx; 
         result.Y += (int) dy; 

         return result;
      }

      // ============================================================================
      // Move one square towards 'to' from 'from'.
      // Ships on the battle board always move one square at a time.
      // ============================================================================
      public static Point BattleMoveTo(Point from, Point to)
      {
          Point result = new Point();
          result = from;

          if (from.Y > to.Y) result.Y--;
          else if (from.Y < to.Y) result.Y++;

          if (from.X > to.X) result.X--;
          else if (from.X < to.X) result.X++;

          return result;
      }

   }
}

