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

namespace Nova.WinForms.RaceDesigner
{
    using System;
    using System.Windows.Forms;

    /// <Summary>
    /// Race Designer utilities.
    /// </Summary>
    public class Utilities
    {
        #region Methods

        /// <Summary>
        /// This function just warns the user of the consequences of using the Cancel
        /// button.
        /// </Summary>
        /// <param name="parent"><see cref="IWin32Window"/></param>
        /// <returns>A <see cref="DialogResult"/>.</returns>
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

