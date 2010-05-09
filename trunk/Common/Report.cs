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
// Some error reporting utilities.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Nova.Common
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// Provides a variety of message pop ups.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public static class Report
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Report an error
        /// </summary>
        /// <param name="text">Message to display.</param>
        /// ----------------------------------------------------------------------------
        public static void Error(string text)
        {
            MessageBox.Show("Nova has encountered an error, but will continue anyway." + Environment.NewLine +
                "Details: " + text, "Nova - Error ", MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        }


        /// <summary>
        /// Report informations
        /// </summary>
        /// <param name="text">Message to display.</param>
        public static void Information(string text)
        {
            MessageBox.Show(text, "Nova - Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }


        /// <summary>
        /// Report a fatal error and terminate the application.
        /// </summary>
        /// <param name="text">Message to display.</param>
        public static void FatalError(string text)
        {
            MessageBox.Show(
              text + "\r\n\r\n(This error will terminate the program)",
              "Nova - Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            System.Threading.Thread.CurrentThread.Abort();
        }


        /// <summary>
        /// Report Debug Messages if in debugging mode. Otherwise do nothing
        /// </summary>
        /// <param name="text">Message to display.</param>
        public static void Debug(string text)
        {
#if (DEBUG)
            MessageBox.Show(text, "Nova - Debug", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
#endif
        }

    }
}
