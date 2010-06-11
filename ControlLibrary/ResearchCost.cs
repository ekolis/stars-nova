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
// Race Designer component for the cost of research.
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.ControlLibrary
{
    public class ResearchCost : System.Windows.Forms.UserControl
    {
        public delegate void SelectionChangedHandler(object sender, int value);
        public event SelectionChangedHandler SelectionChanged;

        private int ResearchFactor = 100;

        private System.Windows.Forms.GroupBox groupbox;
        private System.Windows.Forms.RadioButton LessCost;
        private System.Windows.Forms.RadioButton StandardCost;
        private System.Windows.Forms.RadioButton ExtraCost;
        private System.ComponentModel.Container components = null;

        #region Construction and Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construtor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ResearchCost()
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

        private void InitializeComponent()
        {
            this.groupbox = new System.Windows.Forms.GroupBox();
            this.LessCost = new System.Windows.Forms.RadioButton();
            this.StandardCost = new System.Windows.Forms.RadioButton();
            this.ExtraCost = new System.Windows.Forms.RadioButton();
            this.groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupbox
            // 
            this.groupbox.Controls.Add(this.LessCost);
            this.groupbox.Controls.Add(this.StandardCost);
            this.groupbox.Controls.Add(this.ExtraCost);
            this.groupbox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupbox.Location = new System.Drawing.Point(8, 8);
            this.groupbox.Name = "groupbox";
            this.groupbox.Size = new System.Drawing.Size(176, 104);
            this.groupbox.TabIndex = 0;
            this.groupbox.TabStop = false;
            this.groupbox.Text = "Define Title in Designer";
            // 
            // LessCost
            // 
            this.LessCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LessCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LessCost.Location = new System.Drawing.Point(16, 72);
            this.LessCost.Name = "LessCost";
            this.LessCost.Size = new System.Drawing.Size(112, 24);
            this.LessCost.TabIndex = 2;
            this.LessCost.Text = "Costs 50% less";
            this.LessCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
            // 
            // StandardCost
            // 
            this.StandardCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.StandardCost.Checked = true;
            this.StandardCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StandardCost.Location = new System.Drawing.Point(16, 48);
            this.StandardCost.Name = "StandardCost";
            this.StandardCost.Size = new System.Drawing.Size(144, 24);
            this.StandardCost.TabIndex = 1;
            this.StandardCost.TabStop = true;
            this.StandardCost.Text = "Costs standard amount";
            this.StandardCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
            // 
            // ExtraCost
            // 
            this.ExtraCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExtraCost.Location = new System.Drawing.Point(16, 24);
            this.ExtraCost.Name = "ExtraCost";
            this.ExtraCost.Size = new System.Drawing.Size(112, 24);
            this.ExtraCost.TabIndex = 0;
            this.ExtraCost.Text = "Costs 70% Extra";
            this.ExtraCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
            // 
            // ResearchCost
            // 
            this.Controls.Add(this.groupbox);
            this.Name = "ResearchCost";
            this.Size = new System.Drawing.Size(200, 128);
            this.groupbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///  Called when one of the control radio button changes. Note that this 
        /// function relies on it subtracting the previous points value when a button
        /// is switched off.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Research_CheckChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            int value = 0;

            if (radioButton.Checked)
            {
                if (radioButton.Name == "ExtraCost")
                {
                    value = 49;
                    ResearchFactor = 150;
                }
                else if (radioButton.Name == "LessCost")
                {
                    value = -43;
                    ResearchFactor = 50;
                }
                else
                {
                    ResearchFactor = 100;
                }
            }
            else
            {
                if (radioButton.Name == "ExtraCost")
                {
                    value = -49;
                }
                else if (radioButton.Name == "LessCost")
                {
                    value = 43;
                }
            }

            SelectionChanged(this, value);
        }

        #endregion Event Methods

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set or Get the research cost.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Cost
        {
            get { return ResearchFactor; }
            set
            {
                switch (value)
                {
                    case 50:
                        LessCost.Checked = true;
                        StandardCost.Checked = false;
                        ExtraCost.Checked = false;
                        break;
                    case 100:
                        LessCost.Checked = false;
                        StandardCost.Checked = true;
                        ExtraCost.Checked = false;
                        break;
                    case 150:
                        LessCost.Checked = false;
                        StandardCost.Checked = false;
                        ExtraCost.Checked = true;
                        break;
                }
                ResearchFactor = value;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the control title.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Title of component box."), Category("Misc")]
        public string Title
        {
            set { groupbox.Text = value; }
            get { return groupbox.Text; }
        }

        #endregion Properties

    }
}
