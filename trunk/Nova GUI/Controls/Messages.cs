// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// A control to display user messages.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;
using NovaCommon;


// ============================================================================
// A control to display user messages.
// ============================================================================

namespace Nova
{
   public class Messages : System.Windows.Forms.UserControl
   {
      private ArrayList messages       = null;
      private int       currentMessage = 0;
      private int       turnYear       = 0;

      private System.Windows.Forms.GroupBox MessageForm;
      private System.Windows.Forms.Label messageBox;
      private System.Windows.Forms.Button nextButton;
      private System.Windows.Forms.Button previousButton;
      private Button GotoButton;
      private System.ComponentModel.Container components = null;


// ============================================================================
// Construction.
// ============================================================================

      public Messages()
      {
         InitializeComponent();
      }


// ============================================================================
// Clean up any resources being used.
// ============================================================================

      protected override void Dispose(bool disposing)
      {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }


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
          this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
          // 
          // nextButton
          // 
          this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.nextButton.Location = new System.Drawing.Point(8, 24);
          this.nextButton.Name = "nextButton";
          this.nextButton.Size = new System.Drawing.Size(75, 23);
          this.nextButton.TabIndex = 1;
          this.nextButton.Text = "Next";
          this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
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


// ============================================================================
// ReadIntel the Next button being pressed.
// ============================================================================

      private void nextButton_Click(object sender, System.EventArgs e) 
      {
         if (currentMessage < messages.Count - 1) {
            currentMessage++;
            SetMessage();

            if (currentMessage == 1) {
               previousButton.Enabled = true;
            }

            if (currentMessage == messages.Count - 1) {
               nextButton.Enabled = false;
            }
         }
      }


// ============================================================================
// ReadIntel the previous button being pressed.
// ===========================================================================

      private void previousButton_Click(object sender, System.EventArgs e) 
      {
         if (currentMessage > 0) {
            currentMessage--;
            SetMessage();

            if (currentMessage == 0) {
               previousButton.Enabled = false;
            }
         }
         nextButton.Enabled = true;
      }


// ===========================================================================
// Display a message in the message control.
// ===========================================================================

      public void SetMessage()
      {
         GotoButton.Enabled = false;
         
         StringBuilder title = new StringBuilder();
         title.AppendFormat("Year {0} - ", turnYear);

         if (messages.Count != 0) {
            title.AppendFormat("Message {0} of {1}",
                               currentMessage + 1, messages.Count);
         }
         else {
            title.AppendFormat("No Messages");
         }
         
         MessageForm.Text = title.ToString();

         if (messages.Count > 0) {
            NovaCommon.Message thisMessage = messages[currentMessage] 
                                           as NovaCommon.Message;
            messageBox.Text = thisMessage.Text;

            if (thisMessage.Event != null) {
               GotoButton.Enabled = true;
            }
         }
      }


// ============================================================================
// Get and set the turn year in the message control;
// ============================================================================

      public int Year
      {
         get { return turnYear;  }
         set { turnYear = value; }
      }


// ============================================================================
// Set the messages to be displayed.
// ============================================================================

      public ArrayList MessageList
      {
         set {
            messages               = value;
            currentMessage         = 0;
            previousButton.Enabled = false;

            if (messages.Count > 1) {
               nextButton.Enabled = true;
            }
            else {
               nextButton.Enabled = false;
            }

            SetMessage();
         }
      }


// ============================================================================
// Go to event button pressed.
// ============================================================================

       private void GotoButton_Click(object sender, EventArgs e)
       {
          NovaCommon.Message thisMessage = messages[currentMessage] 
                                          as NovaCommon.Message;

          if (thisMessage.Event is BattleReport) {
             DoDialog(new BattleViewer(thisMessage.Event as BattleReport));
          }

       }


// ============================================================================
// General dialog handling
// ============================================================================

      private void DoDialog(Form dialog)
      {
         dialog.ShowDialog();
         dialog.Dispose();
      }

   }
}
