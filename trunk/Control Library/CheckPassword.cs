// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This module controls the checking of passwords.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NovaCommon;
using System.Web.Security;


namespace ControlLibrary
{
    public partial class CheckPassword : Form
    {

        Race RaceData = null;

        #region Construction and Dispose

        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="raceData">The <see cref="Race"/> whose password is being checked.</param>
        public CheckPassword(Race raceData)
        {
            InitializeComponent();
            RaceData = raceData;
            AcceptButton = OKButton;
        }


        /// <summary>
        /// Cancel Button Pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// OK Button Pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
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

            Dispose();
        }

        #endregion

    }
}
