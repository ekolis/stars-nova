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
// Some general utilities for handling points.
// ===========================================================================
#endregion

using System;
using System.Drawing;

namespace NovaCommon
{
    /// <summary>
    /// Point Utility Functions.
    /// </summary>
    public class PointUtilities
    {

        private static Random RandomNumber = new Random();


        /// <summary>
        /// Return a random position within a Rectangle. A border (which may be zero) is
        /// applied to the area where point positions will not be allocated. This
        /// ensures that returned points are never too close to the edge of the
        /// rectangle.
        /// </summary>
        /// <param name="box">A <see cref="Rectangle"/> which will contain the point.</param>
        /// <param name="boxBorder">The minimum distance between the point and the edge of the box.</param>
        /// <returns>A <see cref="Point"/> within the box.</returns>
        public static Point GetPositionInBox(Rectangle box, int boxBorder)
        {
            int boxSize = box.Width;
            Point position = new Point(box.X, box.Y);

            position.X += RandomNumber.Next(boxBorder, boxSize - boxBorder);
            position.Y += RandomNumber.Next(boxBorder, boxSize - boxBorder);

            return position;
        }


        /// <summary>
        /// Determine if a point is within a bounding box. 
        /// </summary>
        /// <param name="p">The <see cref="Point"/> in question.</param>
        /// <param name="box">The <see cref="Rectangle"/> defining the space to check.</param>
        /// <returns>True if point p is in the box.</returns>
        public static bool InBox(Point p, Rectangle box)
        {
            Point upperLeft = box.Location;
            Point bottomRight = box.Location;

            bottomRight.Offset(box.Width, box.Height);

            if (((p.X > upperLeft.X) && (p.X < bottomRight.X)) &&
                ((p.Y > upperLeft.Y) && (p.Y < bottomRight.Y)))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Determine if two circles overlap.
        /// </summary>
        /// <remarks>
        /// For two circles (position x,y and radius r) x1,y1,r1 and x2,y2,r2, if:
        /// (x2-x1)^2+(y2-y1)^2 <!--<--> (r2+r1)^2
        /// Then the circles overlap. 
        /// </remarks>
        /// <param name="p1">Centre of the first circle.</param>
        /// <param name="p2">Centre of the second circle.</param>
        /// <param name="r1">Radius of the first circle.</param>
        /// <param name="r2">Radius of the second circle.</param>
        /// <returns></returns>
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

            if ((f1 + f2) < f3)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Determine if two positions are "near".
        /// </summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <returns></returns>
        public static bool IsNear(Point position1, Point position2)
        {
            Point topCorner = position1;
            topCorner.Offset(-20, -20);
            Rectangle scanArea = new Rectangle(topCorner, new Size(40, 40));

            if (InBox(position2, scanArea))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Calculate the distance between two points.
        /// </summary>
        /// <remarks>
        /// If comparing distances consider using DistanceSquare - it is much faster.
        /// </remarks>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double Distance(Point start, Point end)
        {
            double xo = start.X - end.X;
            double yo = start.Y - end.Y;
            double distance = Math.Sqrt(((xo * xo) + (yo * yo)));

            return distance;
        }


        /// <summary>
        /// Find the square of the distance between two points. 
        /// </summary>
        /// <remarks>
        /// This is much faster than finding the actual distance and just as useful when making comparisons.
        /// </remarks>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double DistanceSquare(Point start, Point end)
        {
            double xo = start.X - end.X;
            double yo = start.Y - end.Y;
            return (xo * xo) + (yo * yo);

        }


        /// <summary>
        /// Move a position some distance nearer to another point.
        /// </summary>
        /// <remarks>
        /// FIXME (priority 4) - rounding can cause no movement to occur. 
        /// Fix added, requires testing - Dan 4 Apr 10
        /// </remarks>
        /// <param name="from">The stating <see cref="Point"/></param>
        /// <param name="to">The destination <see cref="Point"/></param>
        /// <param name="distance">The actual distance to move.</param>
        /// <returns></returns>
        public static Point MoveTo(Point from, Point to, double distance)
        {
            Point result = new Point();
            result = from;

            double my    = to.Y - from.Y;
            double mx    = to.X - from.X;
            double theta = Math.Atan2(my, mx);
            double dx    = distance * Math.Cos(theta);
            double dy    = distance * Math.Sin(theta);

            result.X += (int)(dx+0.5);
            result.Y += (int)(dy+0.5);

            // Check for no movement due to rounding and correct.
            if (result.X == from.X && result.Y == from.Y && distance > 0.5)
            {
                if (my > mx)
                {
                    result.Y += (int)(distance + 0.5);
                }
                else
                {
                    result.X += (int)(distance + 0.5);
                }
            }

            return result;
        }


        /// <summary>
        /// Move one square towards 'to' from 'from'.
        /// </summary>
        /// <remarks>
        /// Ships on the battle board always move one square at a time.
        /// </remarks>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
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
