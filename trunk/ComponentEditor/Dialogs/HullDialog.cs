// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
// 
// Dialog for designing a hull.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

#region Using Statements
using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

namespace ComponentEditor
{
    public partial class HullDialog : Form
    {
        private Hashtable AllComponents = null;

        /// <summary>
        /// Construction.
        /// </summary>
        public HullDialog()
        {
            InitializeComponent();
            AllComponents = NovaCommon.AllComponents.Data.Components;
        }


        /// <summary>
        /// Exit button selected, close the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
