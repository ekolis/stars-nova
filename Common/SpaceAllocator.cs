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
// This class will "chop" the space into the requested number of boxes and
// allocate a box on demand (removing the box from the list of available
// boxes).
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Drawing;

namespace Nova.Common
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// SpaceAllocator chops up the available space into a number of boxes which
    /// can be given out one-by-one.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public class SpaceAllocator
    {
        public int GridAxisCount;

        private readonly ArrayList availableBoxes = new ArrayList();
        private readonly Random random = new Random();


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// <para>Construction
        /// </para><para>
        /// Just determine the size of the grid we are going to need based
        /// on the number of items to place.
        /// </para><para>
        /// If the requested number of items does not naturally allow a square grid of
        /// boxes to be created (e.g. 2, 3, 5, etc. don't - 4, 9, etc. do) then the
        /// requested number is rounded up to a number that does.
        /// </para></summary>
        /// <param name="numberOfItems">The number of items to be distributed in the allocatable space.</param>
        /// ----------------------------------------------------------------------------
        public SpaceAllocator(int numberOfItems)
        {
            GridAxisCount = (int)Math.Sqrt(numberOfItems);
            if ((GridAxisCount * GridAxisCount) != numberOfItems)
            {
                GridAxisCount++;
                numberOfItems = GridAxisCount * GridAxisCount;
            }
            if (GridAxisCount <= 0)
            {
                GridAxisCount = 1;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        ///  Create the requested number of boxes. The boxes are stored as rectangles in
        /// the available list of boxes.
        /// </summary>
        /// <param name="spaceSize">The length of one side of the allocatable space (assumed to be a square).</param>
        /// ----------------------------------------------------------------------------
        public void AllocateSpace(int spaceSize)
        {
            // Find the size of a box side. This will allow us to find the
            // position of the top left corner of each box and, as we know the
            // length of the side we can determine the rectangle details.

            if (GridAxisCount <= 0)
            {
                GridAxisCount = 1;
            }
            int boxSide = spaceSize / GridAxisCount;
            Size boxSize = new Size(boxSide, boxSide);
            Point currentPosition = new Point();

            for (int y = 0; y < GridAxisCount; y++)
            {
                currentPosition.Y = y * boxSide;

                for (int x = 0; x < GridAxisCount; x++)
                {
                    currentPosition.X = x * boxSide;
                    Rectangle box = new Rectangle(currentPosition, boxSize);
                    this.availableBoxes.Add(box);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return one of the allocated boxes and remove the box from the list of
        /// available boxes.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public Rectangle GetBox()
        {
            int boxNumber = this.random.Next(0, this.availableBoxes.Count - 1);
            Rectangle box = (Rectangle)this.availableBoxes[boxNumber];

            this.availableBoxes.RemoveAt(boxNumber);
            return box;
        }

    }
}
