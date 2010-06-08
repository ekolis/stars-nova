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
// A control to display user messages.
// ===========================================================================
#endregion

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;
using Nova.Common;


namespace Nova.WinForms.Gui
{
    /// <summary>
    /// A control to display user messages.
    /// </summary>
    public class Messages : System.Windows.Forms.UserControl
    {
        private ArrayList messages = null;
        private int currentMessage = 0;
        private int turnYear = 0;

        #region VS-Generated Variables
        private System.Windows.Forms.GroupBox MessageForm;
        private System.Windows.Forms.Label messageBox;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private Button GotoButton;
        private System.ComponentModel.Container components = null;
        #endregion

        #region Construction and Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Messages()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        /// ----------------------------------------------------------------------------
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

        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MessageForm = new System.Windows.Forms.GroupBox();
            this.GotoButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.Label();
            this.MessageForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageForm
            // 
            this.MessageForm.Controls.Add(this.GotoButton);
            this.MessageForm.Controls.Add(this.previousButton);
            this.MessageForm.Controls.Add(this.nextButton);
            this.MessageForm.Controls.Add(this.messageBox);
            this.MessageForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageForm.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MessageForm.Location = new System.Drawing.Point(0, 0);
            this.MessageForm.Name = "MessageForm";
            this.MessageForm.Size = new System.Drawing.Size(350, 120);
            this.MessageForm.TabIndex = 0;
            this.MessageForm.TabStop = false;
            this.MessageForm.Text = "Year 2100 - No Messages";
            // 
            // GotoButton
            // 
            this.GotoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GotoButton.Location = new System.Drawing.Point(8, 84);
            this.GotoButton.Name = "GotoButton";
            this.GotoButton.Size = new System.Drawing.Size(75, 23);
            this.GotoButton.TabIndex = 3;
            this.GotoButton.Text = "Go To";
            this.GotoButton.Click += new System.EventHandler(this.GotoButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.previousButton.Location = new System.Drawing.Point(8, 53);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 2;
            this.previousButton.Text = "Previous";
            this.previousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.nextButton.Location = new System.Drawing.Point(8, 24);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageBox.Location = new System.Drawing.Point(104, 25);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(240, 86);
            this.messageBox.TabIndex = 0;
            // 
            // Messages
            // 
            this.Controls.Add(this.MessageForm);
            this.Name = "Messages";
            this.Size = new System.Drawing.Size(350, 120);
            this.MessageForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the Next button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process the previous button being pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
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


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Go to event button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void GotoButton_Click(object sender, EventArgs e)
        {
            Nova.Common.Message thisMessage = messages[currentMessage]
                                            as Nova.Common.Message;

            if (thisMessage.Event is BattleReport)
            {
                DoDialog(new BattleViewer(thisMessage.Event as BattleReport));
            }

        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display a message in the message control.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void SetMessage()
        {
            GotoButton.Enabled = false;

            StringBuilder title = new StringBuilder();
            title.AppendFormat("Year {0} - ", turnYear);

            if (messages.Count != 0)
            {
                title.AppendFormat("Message {0} of {1}",
                                   currentMessage + 1, messages.Count);
            }
            else
            {
                title.AppendFormat("No Messages");
            }

            MessageForm.Text = title.ToString();

            if (messages.Count > 0)
            {
                Nova.Common.Message thisMessage = new Nova.Common.Message();
                thisMessage = messages[currentMessage] as Nova.Common.Message;
                messageBox.Text = thisMessage.Text;

                if (thisMessage.Event != null)
                {
                    GotoButton.Enabled = true;
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// General dialog handling
        /// </summary>
        /// <param name="dialog">A dialog Form.</param>
        /// ----------------------------------------------------------------------------
        private void DoDialog(Form dialog)
        {
            dialog.ShowDialog();
            dialog.Dispose();
        }

        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set the turn year in the message control
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Year
        {
            get { return turnYear; }
            set { turnYear = value; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the messages to be displayed.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ArrayList MessageList
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

        #endregion
    }
}
