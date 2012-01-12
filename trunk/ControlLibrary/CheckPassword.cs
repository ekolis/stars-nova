#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.ControlLibrary
{
    using System;
    using System.Windows.Forms;

    using Nova.Common;

    /// <summary>
    /// An object to control the checking of passwords.
    /// </summary>
    public partial class CheckPassword : Form
    {
        private readonly Race empireData;

        /// <summary>
        /// Initializes a new instance of the CheckPassword class.
        /// </summary>
        /// <param name="empireData">The <see cref="Race"/> whose password is being checked.</param>
        public CheckPassword(Race empireData)
        {
            InitializeComponent();
            this.empireData = empireData;
            CancelButton = this.cancel;
            AcceptButton = this.okButton;
        }

        /// <summary>
        /// OK Button Pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            string enteredPassword = this.password.Text;
            string oldPasswordHash = this.empireData.Password;
            string newPasswordHash = new PasswordUtility().CalculateHash(this.password.Text);

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
    }
}
