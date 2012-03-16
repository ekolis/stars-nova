#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011, 2012 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    
    using Nova.Common;
    using Nova.Common.DataStructures;
    
    /// <Summary>
    /// A control to display user messages.
    /// </Summary>
    public partial class MessageDisplay : GroupBox
    {
        private List<Common.Message> messages;
        private int currentMessage;
        private int turnYear;
        
        /// <Summary>
        /// Get and set the turn year in the message control
        /// </Summary>
        public int Year
        {
            get { return turnYear; }
            set { turnYear = value; }
        }

        /// <Summary>
        /// Set the messages to be displayed.
        /// </Summary>
        public List<Common.Message> MessageList
        {
            set
            {
                messages = value;
                currentMessage = 0;
                previousButton.Enabled = false;

                if (messages.Count > 1)
                {
                    nextButton.Enabled = true;
                }
                else
                {
                    nextButton.Enabled = false;
                }

                SetMessage();
            }
        }

        /// <Summary>
        /// Initializes a new instance of the Messages class.
        /// </Summary>
        public MessageDisplay()
        {
            InitializeComponent();
        }

        /// <Summary>
        /// Clean up any resources being used.
        /// </Summary>
        /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        
        /// <Summary>
        /// Process the Next button being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NextButton_Click(object sender, System.EventArgs e)
        {
            if (currentMessage < messages.Count - 1)
            {
                currentMessage++;
                SetMessage();

                if (currentMessage == 1)
                {
                    previousButton.Enabled = true;
                }

                if (currentMessage == messages.Count - 1)
                {
                    nextButton.Enabled = false;
                }
            }
        }


        /// <Summary>
        /// Process the previous button being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PreviousButton_Click(object sender, System.EventArgs e)
        {
            if (currentMessage > 0)
            {
                currentMessage--;
                SetMessage();

                if (currentMessage == 0)
                {
                    previousButton.Enabled = false;
                }
            }
            nextButton.Enabled = true;
        }


        /// <Summary>
        /// Go to event button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void GotoButton_Click(object sender, EventArgs e)
        {
            Nova.Common.Message thisMessage = messages[currentMessage];

            if (thisMessage.Event is BattleReport)
            {
                DoDialog(new BattleViewer(thisMessage.Event as BattleReport));
            }

        }


        /// <Summary>
        /// Display a message in the message control.
        /// </Summary>
        public void SetMessage()
        {
            this.gotoButton.Enabled = false;

            StringBuilder title = new StringBuilder();
            title.AppendFormat("Year {0} - ", turnYear);

            if (messages.Count != 0)
            {
                title.AppendFormat("Message {0} of {1}", currentMessage + 1, messages.Count);
            }
            else
            {
                title.AppendFormat("No Messages");
            }

            Text = title.ToString();

            if (messages.Count > 0)
            {
                Nova.Common.Message thisMessage = new Nova.Common.Message();
                thisMessage = messages[currentMessage];
                messageBox.Text = thisMessage.Text;

                if (thisMessage.Event != null)
                {
                    gotoButton.Enabled = true;
                }
            }
        }


        /// <Summary>
        /// General dialog handling
        /// </Summary>
        /// <param name="dialog">A dialog Form.</param>
        private void DoDialog(Form dialog)
        {
            dialog.ShowDialog();
            dialog.Dispose();
        }
    }
}
