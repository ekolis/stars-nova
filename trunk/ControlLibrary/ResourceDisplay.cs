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
// A user control for the display of a set of resources.
// ===========================================================================
#endregion

namespace Nova.ControlLibrary
{
    using System;
    using System.ComponentModel;
    using Nova.Common;

    public class ResourceDisplay : System.Windows.Forms.UserControl
    {        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        protected System.Windows.Forms.Label ironium;
        protected System.Windows.Forms.Label germanium;
        protected System.Windows.Forms.Label boranium;
        protected System.Windows.Forms.Label energy;
        private System.ComponentModel.Container components = null;

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the ResourceDisplay class.
        /// </summary>
        public ResourceDisplay()
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

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void InitializeComponent()
        {            
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ironium = new System.Windows.Forms.Label();
            this.energy = new System.Windows.Forms.Label();
            this.germanium = new System.Windows.Forms.Label();
            this.boranium = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ironium";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Resources";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Goldenrod;
            this.label3.Location = new System.Drawing.Point(0, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Germanium";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.YellowGreen;
            this.label4.Location = new System.Drawing.Point(0, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Boranium";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(136, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "kT";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(136, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "kT";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(136, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "kT";
            // 
            // ironium
            // 
            this.ironium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ironium.Location = new System.Drawing.Point(80, 0);
            this.ironium.Name = "ironium";
            this.ironium.Size = new System.Drawing.Size(56, 16);
            this.ironium.TabIndex = 7;
            this.ironium.Text = "0";
            this.ironium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // energy
            // 
            this.energy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.energy.Location = new System.Drawing.Point(80, 48);
            this.energy.Name = "energy";
            this.energy.Size = new System.Drawing.Size(56, 16);
            this.energy.TabIndex = 8;
            this.energy.Text = "0";
            this.energy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // germanium
            // 
            this.germanium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.germanium.Location = new System.Drawing.Point(80, 32);
            this.germanium.Name = "germanium";
            this.germanium.Size = new System.Drawing.Size(56, 16);
            this.germanium.TabIndex = 9;
            this.germanium.Text = "0";
            this.germanium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // boranium
            // 
            this.boranium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.boranium.Location = new System.Drawing.Point(80, 16);
            this.boranium.Name = "boranium";
            this.boranium.Size = new System.Drawing.Size(56, 16);
            this.boranium.TabIndex = 10;
            this.boranium.Text = "0";
            this.boranium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ResourceDisplay
            // 
            this.Controls.Add(this.boranium);
            this.Controls.Add(this.germanium);
            this.Controls.Add(this.energy);
            this.Controls.Add(this.ironium);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ResourceDisplay";
            this.Size = new System.Drawing.Size(150, 64);
            this.ResumeLayout(false);

        }
        #endregion

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the resources in the resource display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Browsable(false)]
        public virtual Resources Value
        {
            set
            {
                try
                {
                    if (value == null)
                    {
                        return;
                    }

                    Resources resources = value;

                    this.ironium.Text = resources.Ironium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.boranium.Text = resources.Boranium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.germanium.Text = resources.Germanium.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    this.energy.Text = resources.Energy.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    Report.Error("Unable to convert resource values.");
                }
            }

            get
            {
                Resources resources = new Resources();

                resources.Ironium = Convert.ToInt32(this.ironium.Text, System.Globalization.CultureInfo.CurrentCulture);
                resources.Boranium = Convert.ToInt32(this.boranium.Text, System.Globalization.CultureInfo.CurrentCulture);
                resources.Germanium = Convert.ToInt32(this.germanium.Text, System.Globalization.CultureInfo.CurrentCulture);
                resources.Energy = Convert.ToInt32(this.energy.Text, System.Globalization.CultureInfo.CurrentCulture);
                return resources;
            }
        }

        #endregion Properties
    }
}
