#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
// See PointUtilities class summary.
// ===========================================================================
#endregion

namespace Nova.Common
{
    using System;
    using System.Drawing;
    using Nova.Common.DataStructures;

    /// <summary>
    /// Some general utilities for handling points.
    /// </summary>
    /// <remarks>
    /// TODO (priority 4) - should these be merged with NovaPoint? - Dan 28 Nova 10
    /// </remarks>
    public static class PointUtilities
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Return a random position within a Rectangle. A border (which may be zero) is
        /// applied to the area where point positions will not be allocated. This
        /// ensures that returned points are never too close to the edge of the
        /// rectangle.
        /// </summary>
        /// <param name="box">A <see cref="Rectangle"/> which will contain the point.</param>
        /// <param name="boxBorder">The minimum distance between the point and the edge of the box.</param>
        /// <returns>A <see cref="Point"/> within the box.</returns>
        public static NovaPoint GetPositionInBox(Rectangle box, int boxBorder)
        {
            int boxSize = box.Width;
            NovaPoint position = new NovaPoint(box.X, box.Y);

            position.X += Random.Next(boxBorder, boxSize - boxBorder);
            position.Y += Random.Next(boxBorder, boxSize - boxBorder);

            return position;
        }

        /// <summary>
        /// Determine if a point is within a bounding box. 
        /// </summary>
        /// <param name="p">The <see cref="NovaPoint"/> in question.</param>
        /// <param name="box">The <see cref="Rectangle"/> defining the space to check.</param>
        /// <returns>True if point p is in the box.</returns>
        public static bool InBox(NovaPoint p, Rectangle box)
        {
            NovaPoint upperLeft = new NovaPoint(box.Location);
            NovaPoint bottomRight = new NovaPoint(box.Location);

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
        public static bool CirclesOverlap(NovaPoint p1, NovaPoint p2, double r1, double r2)
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
        /// Determine if two positions are within a 40x40 box.
        /// </summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <returns>true if position 2 is within a 40x40 box around position 1.</returns>
        public static bool IsNear(NovaPoint position1, NovaPoint position2)
        {
            NovaPoint topCorner = new NovaPoint(position1);
            topCorner.Offset(-20, -20);
            Rectangle scanArea = new Rectangle(topCorner.X, topCorner.Y, 40, 40);

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
        /// <param name="start">A point.</param>
        /// <param name="end">Another point.</param>
        /// <returns>Distance between the start and end points.</returns>
        /// TODO (priority 3) - Find calls to this function that could use DistanceSquare instead (for speed).
        public static double Distance(NovaPoint start, NovaPoint end)
        {
            double xo = start.X - end.X;
            double yo = start.Y - end.Y;
            double distance = Math.Sqrt(((xo * xo) + (yo * yo)));

            return distance;
        }

        /// <summary>
        /// Find the square of the distance between two points. 
        /// <para>
        /// This is much faster than finding the actual distance (as it avoids a sqare root calculation) and just as useful when making distance comparisons.
        /// </para>
        /// </summary>
        /// <param name="start">A point.</param>
        /// <param name="end">Another point.</param>
        /// <returns>The distance between start and end, squared.</returns>
        public static double DistanceSquare(NovaPoint start, NovaPoint end)
        {
            double xo = start.X - end.X;
            double yo = start.Y - end.Y;
            return (xo * xo) + (yo * yo);
        }

        /// <summary>
        /// Move a position some distance nearer to another point.
        /// </summary>
        /// <remarks>
        /// FIXME (priority 6) - rounding can cause no movement to occur. 
        /// Fix added, requires testing - Dan 4 Apr 10
        /// </remarks>
        /// <param name="from">The stating <see cref="NovaPoint"/></param>
        /// <param name="to">The destination <see cref="NovaPoint"/></param>
        /// <param name="distance">The actual distance to move.</param>
        /// <returns>If the distance between from and to is less than 'distance' then returns 'to.
        /// Otherwise a point 'distance' away from 'from' in the direction of 'to'.</returns>
        public static NovaPoint MoveTo(NovaPoint from, NovaPoint to, double distance)
        {
            NovaPoint result = new NovaPoint(from);

            double my = to.Y - from.Y;
            double mx = to.X - from.X;
            double theta = Math.Atan2(my, mx);
            double dx = distance * Math.Cos(theta);
            double dy = distance * Math.Sin(theta);

            result.X += (int)(dx + 0.5);
            result.Y += (int)(dy + 0.5);

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
        /// <param name="from">A starting position.</param>
        /// <param name="to">The destingation position.</param>
        /// <returns>A position 1 square closer to the destination (diagonal movement counts as 1).</returns>
        public static NovaPoint BattleMoveTo(NovaPoint from, NovaPoint to)
        {
            NovaPoint result = new NovaPoint(from);

            if (from.Y > to.Y) 
            {
                result.Y--;
            }
            else if (from.Y < to.Y)
            {
                result.Y++;
            }

            if (from.X > to.X)
            {
                result.X--;
            }
            else if (from.X < to.X)
            {
                result.X++;
            }

            return result;
        }
    }
}

