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
        
    /// <summary>
    /// Description of MessageDisplay_Designer.
    /// </summary>
    public partial class MessageDisplay
    { 
        private Label messageBox;
        private Button nextButton;
        private Button previousButton;
        private Button gotoButton;
        private System.ComponentModel.Container components = null;
       
        /// <Summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </Summary>
        private void InitializeComponent()
        {
            gotoButton = new Button();
            previousButton = new Button();
            nextButton = new Button();
            messageBox = new Label();
            SuspendLayout();
            
            // 
            // GotoButton
            // 
            gotoButton.FlatStyle = FlatStyle.System;
            gotoButton.Location = new System.Drawing.Point(8, 84);
            gotoButton.Name = "gotoButton";
            gotoButton.Size = new System.Drawing.Size(75, 23);
            gotoButton.TabIndex = 3;
            gotoButton.Text = "Go To";
            gotoButton.Click += new System.EventHandler(GotoButton_Click);
            // 
            // previousButton
            // 
            previousButton.FlatStyle = FlatStyle.System;
            previousButton.Location = new System.Drawing.Point(8, 53);
            previousButton.Name = "previousButton";
            previousButton.Size = new System.Drawing.Size(75, 23);
            previousButton.TabIndex = 2;
            previousButton.Text = "Previous";
            previousButton.Click += new System.EventHandler(PreviousButton_Click);
            // 
            // nextButton
            // 
            nextButton.FlatStyle = FlatStyle.System;
            nextButton.Location = new System.Drawing.Point(8, 24);
            nextButton.Name = "nextButton";
            nextButton.Size = new System.Drawing.Size(75, 23);
            nextButton.TabIndex = 1;
            nextButton.Text = "Next";
            nextButton.Click += new System.EventHandler(NextButton_Click);
            // 
            // messageBox
            // 
            messageBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            messageBox.BackColor = System.Drawing.Color.White;
            messageBox.BorderStyle = BorderStyle.FixedSingle;
            messageBox.Location = new System.Drawing.Point(104, 25);
            messageBox.Name = "messageBox";
            messageBox.Size = new System.Drawing.Size(240, 86);
            messageBox.TabIndex = 0;
            // 
            // MessageDisplay (this)
            // 
            Controls.Add(gotoButton);
            Controls.Add(previousButton);
            Controls.Add(nextButton);
            Controls.Add(messageBox);            
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;            
            Name = "Messages";
            Size = new System.Drawing.Size(360, 116);
            Year = Global.StartingYear;
            Text = "Year " + Year.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - No Messages";
            ResumeLayout(false);
    
        }
    }
}
