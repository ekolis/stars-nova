// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// Some error reporting utilities.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NovaCommon
{
    /// <summary>
    /// Provides a variety of message pop ups.
    /// </summary>
    public static class Report
    {

        /// <summary>
        /// Report an error
        /// </summary>
        /// <param name="text">Message to display.</param>
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
