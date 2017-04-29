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

namespace Nova.ControlLibrary
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ResearchCost : System.Windows.Forms.UserControl
    {
        public delegate void SelectionChangedHandler(object sender, int value);
        public event SelectionChangedHandler SelectionChanged;

        private int researchFactor = 100;

        private System.Windows.Forms.GroupBox groupbox;
        private System.Windows.Forms.RadioButton lessCost;
        private System.Windows.Forms.RadioButton standardCost;
        private System.Windows.Forms.RadioButton extraCost;
        private System.ComponentModel.Container components = null;

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the ResearchCost class.
        /// </summary>
        public ResearchCost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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

        #endregion


        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.groupbox = new System.Windows.Forms.GroupBox();
            this.lessCost = new System.Windows.Forms.RadioButton();
            this.standardCost = new System.Windows.Forms.RadioButton();
            this.extraCost = new System.Windows.Forms.RadioButton();
            this.groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupbox
            // 
            this.groupbox.Controls.Add(this.lessCost);
            this.groupbox.Controls.Add(this.standardCost);
            this.groupbox.Controls.Add(this.extraCost);
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
            this.lessCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lessCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lessCost.Location = new System.Drawing.Point(16, 72);
            this.lessCost.Name = "lessCost";
            this.lessCost.Size = new System.Drawing.Size(112, 24);
            this.lessCost.TabIndex = 2;
            this.lessCost.Text = "Costs 50% less";
            this.lessCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
            // 
            // StandardCost
            // 
            this.standardCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.standardCost.Checked = true;
            this.standardCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.standardCost.Location = new System.Drawing.Point(16, 48);
            this.standardCost.Name = "standardCost";
            this.standardCost.Size = new System.Drawing.Size(144, 24);
            this.standardCost.TabIndex = 1;
            this.standardCost.TabStop = true;
            this.standardCost.Text = "Costs standard amount";
            this.standardCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
            // 
            // ExtraCost
            // 
            this.extraCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extraCost.Location = new System.Drawing.Point(16, 24);
            this.extraCost.Name = "extraCost";
            this.extraCost.Size = new System.Drawing.Size(112, 24);
            this.extraCost.TabIndex = 0;
            this.extraCost.Text = "Costs 75% Extra";
            this.extraCost.CheckedChanged += new System.EventHandler(this.Research_CheckChanged);
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

            if (radioButton.Checked)
            {
                if (radioButton.Name == "extraCost")
                {
                    this.researchFactor = 175;
                }
                else if (radioButton.Name == "lessCost")
                {
                    this.researchFactor = 50;
                }
                else
                {
                    this.researchFactor = 100;
                }
            }

            SelectionChanged(this, this.researchFactor);
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
            get
            {
                return this.researchFactor;
            }
            set
            {
                switch (value)
                {
                    case 50:
                        this.lessCost.Checked = true;
                        this.standardCost.Checked = false;
                        this.extraCost.Checked = false;
                        break;
                    case 100:
                        this.lessCost.Checked = false;
                        this.standardCost.Checked = true;
                        this.extraCost.Checked = false;
                        break;
                    case 150: // deprecated, for backward compability / old race files only
                    case 175:
                        this.lessCost.Checked = false;
                        this.standardCost.Checked = false;
                        this.extraCost.Checked = true;
                        break;
                }
                this.researchFactor = value;
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
