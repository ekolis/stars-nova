// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This class will "chop" the space into the requested number of boxes and
// allocate a box on demand (removing the box from the list of available
// boxes).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections;
using System.Drawing;

namespace NovaConsole
{

/// ===========================================================================
/// SpaceAllocator chops up the available space into a number of boxes which
/// can be given out one-by-one.
/// ===========================================================================

   public class SpaceAllocator
   {
      public  int       GridAxisCount  = 0;

      private ArrayList AvailableBoxes = new ArrayList();
      private Random    RandomNumber   = new Random();


// ============================================================================
// Construction. Just determine the size of the grid we are going to need based
// on the number of items to place.
//
// If the requested number of items does not naturally allow a square grid of
// boxes to be created (e.g. 2, 3, 5, etc. don't - 4, 9, etc. do) then the
// requested number is rounded up to a number that does.
//
// We do this by the BFI method (Brute Force and Ignorance) - must talk to a
// mathematician about a better way to do this. 
// ============================================================================

      public SpaceAllocator(int numberOfItems)
      {
         while (true) {
            GridAxisCount = (int) Math.Sqrt(numberOfItems);
            if ((GridAxisCount * GridAxisCount) == numberOfItems) {
               break;
            }
            else {
               numberOfItems++;
            }
         }
      }


// ============================================================================
// Create the requested number of boxes. The boxes are stored as rectangles in
// the available list of boxes.
// ============================================================================
   
      public void AllocateSpace(int spaceSize)
      {
         // Find the size of a box side. This will allow us to find the
         // position of the top left corner of each box and, as we know the
         // length of the side we can determine the rectangle details.

         int   boxSide         = spaceSize / GridAxisCount;
         Size  boxSize         = new Size(boxSide, boxSide);
         Point currentPosition = new Point();

         for (int y = 0; y < GridAxisCount; y++) {
            currentPosition.Y = y * boxSide;

            for (int x = 0; x < GridAxisCount; x++) {
               currentPosition.X = x * boxSide;
               Rectangle box = new Rectangle(currentPosition, boxSize);
               AvailableBoxes.Add(box);               
            }
         }
      }


// ============================================================================
// Return one of the allocated boxes and remove the box from the list of
// available boxes.
// ============================================================================

      public Rectangle GetBox()
      {
         int boxNumber = RandomNumber.Next(0, AvailableBoxes.Count - 1);
         Rectangle box = (Rectangle) AvailableBoxes[boxNumber];

         AvailableBoxes.RemoveAt(boxNumber);
         return box;
      }

   }
}
