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
// Dialog for designing a hull.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Windows.Forms;
#endregion

namespace Nova.WinForms.ComponentEditor
{
    public partial class HullDialog : Form
    {
        private Hashtable AllComponents = null;

        /// <summary>
        /// Initializes a new instance of the HullDialog class.
        /// </summary>
        public HullDialog()
        {
            InitializeComponent();
            AllComponents = Nova.Common.Components.AllComponents.Data.Components;
        }


        /// <summary>
        /// Exit button selected, close the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
