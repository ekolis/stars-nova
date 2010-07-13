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
// This file contains (static) functions invoked to provide various utility
// functions (mainly to stop RaceDesigner.cs from getting too big).
// ===========================================================================
#endregion

using System;
using System.Windows.Forms;

namespace Nova.WinForms.RaceDesigner
{
    /// <summary>
    /// Race Designer utilities.
    /// </summary>
    public class Utilities
    {

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the cost of a range bar width.
        /// </summary>
        /// <remarks>
        /// The impact on the advantage
        /// points is:
        ///
        /// Normal width  (70%)     0 points
        /// Maximum width (100%)  144 points (subtracted from balance)
        /// Minimum width (20%)   286 points (added to balance)
        /// </remarks>
        /// <param name="leftValue">Left most value of the bar.</param>
        /// <param name="rightValue">Right most value of the bar.</param>
        /// <returns>Cost in advantage points.</returns>
        /// ----------------------------------------------------------------------------
        public static int BarWidthCost(int leftValue, int rightValue)
        {
            int barWidth = rightValue - leftValue;
            int cost = 0;

            if (barWidth > 70)
            {
                cost = ((barWidth - 70) * 144) / 30;
                cost *= -1;
            }
            else if (barWidth < 70)
            {
                cost = ((70 - barWidth) * 286) / 50;
            }

            return cost;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Calculate the cost of a range bar position.
        /// </summary>
        /// <remarks>
        /// The impact on the advantage
        /// points is:
        ///
        /// Normal position  (50%)     0 points
        /// Maximum position (100%)   130 points (added to balance)
        /// Minimum position (0%)     125 points (added to balance)
        ///
        /// Note that, because the bar has a minimum width of 20% only the values
        /// corresponding to 10% and 90% will ever be seen.
        /// </remarks>
        /// <param name="leftValue">Left most value of the bar.</param>
        /// <param name="rightValue">Right most value of the bar.</param>
        /// <returns>Cost in advanatage points.</returns>
        /// ----------------------------------------------------------------------------
        public static int BarPositionCost(int leftValue, int rightValue)
        {
            int barPosition = leftValue + ((rightValue - leftValue) / 2);
            int cost = 0;

            if (barPosition > 50)
            {
                cost = ((barPosition - 50) * 130) / 50;
            }
            else if (barPosition < 50)
            {
                cost = ((50 - barPosition) * 125) / 50;
            }

            return cost;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function just warns the user of the consequences of using the Cancel
        /// button.
        /// </summary>
        /// <param name="parent"><see cref="IWin32Window"/></param>
        /// <returns>A <see cref="DialogResult"/>.</returns>
        /// ----------------------------------------------------------------------------
        public static DialogResult CancelWarning(IWin32Window parent)
        {
            string message = "This will discard your race definition. "
                           + "Are you sure you want to do this?";

            string caption = "Nova - Warning";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(
                parent,
                message,
                caption,
                buttons,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            return result;
        }

        #endregion

    }
}

