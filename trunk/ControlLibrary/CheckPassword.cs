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
// This module controls the checking of passwords.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.Common;
using System.Web.Security;


namespace Nova.ControlLibrary
{
    public partial class CheckPassword : Form
    {

        Race RaceData = null;

        #region Construction and Dispose

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="raceData">The <see cref="Race"/> whose password is being checked.</param>
        /// ----------------------------------------------------------------------------
        public CheckPassword(Race raceData)
        {
            InitializeComponent();
            RaceData = raceData;
            CancelButton = Cancel;
            AcceptButton = OKButton;
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// OK Button Pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OKButton_Click(object sender, EventArgs e)
        {
            string enteredPassword = PassWord.Text;
            string oldPasswordHash = RaceData.Password;

            string newPasswordHash = FormsAuthentication.
               HashPasswordForStoringInConfigFile(enteredPassword, "MD5");

            if (newPasswordHash != oldPasswordHash)
            {
                Report.Information("Incorrect password - Access denied");
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        #endregion

    }
}
