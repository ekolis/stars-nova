// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// This file contains (static) functions invoked to provide various utility
// functions (mainly to stop RaceDesigner.cs from getting too big).
// ============================================================================

using System;
using System.Windows.Forms;

namespace RaceDesigner
{

   public class Utilities
   {

// ============================================================================
// Calculate the cost of a range bar width. The impact on the advantage
// points is:
//
// Normal width  (70%)     0 points
// Maximum width (100%)  144 points (subtracted from balance)
// Minimum width (20%)   286 points (added to balance)
// ============================================================================

      public static int BarWidthCost(int leftValue, int rightValue)
      {
         int barWidth = rightValue - leftValue;
         int cost     = 0;

         if (barWidth > 70) {
            cost = ((barWidth - 70) * 144) / 30;
            cost *= -1;
         }
         else if (barWidth < 70) {
            cost  = ((70 - barWidth) * 286) / 50;
         }

         return cost;
      }


// ============================================================================
// Calculate the cost of a range bar position. The impact on the advantage
// points is:
//
// Normal position  (50%)     0 points
// Maximum position (100%)   130 points (added to balance)
// Minimum position (0%)     125 points (added to balance)
//
// Note that, because the bar has a minimum width of 20% only the values
// corresponding to 10% and 90% will ever be seen.
// ============================================================================

      public static int BarPositionCost(int leftValue, int rightValue)
      {
         int barPosition = leftValue + ((rightValue - leftValue) / 2);
         int cost     = 0;

         if (barPosition > 50) {
            cost = ((barPosition - 50) * 130) / 50;
         }
         else if (barPosition < 50) {
            cost = ((50 - barPosition) * 125) / 50;
         }

         return cost;
      }


// ============================================================================
// This function just warns the user of the consequences of using the Cancel
// button.
// ============================================================================

      public static DialogResult CancelWarning(IWin32Window parent)
      {
         string message = "This will discard your race definition. "
                        + "Are you sure you want to do this?";
      
         string caption = "Nova - Warning";

         MessageBoxButtons buttons = MessageBoxButtons.YesNo;

         DialogResult  result = MessageBox.Show(parent, message, caption, 
                                  buttons,
                                  MessageBoxIcon.Warning,
                                  MessageBoxDefaultButton.Button2); 

         return result;
      }


   }
}

