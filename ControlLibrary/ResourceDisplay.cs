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

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.ControlLibrary
{
    public class ResourceDisplay : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Ironium;
        private System.Windows.Forms.Label Germanium;
        private System.Windows.Forms.Label Boranium;
        private System.Windows.Forms.Label Energy;
        private System.ComponentModel.Container components = null;

        #region Construction and Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ResourceDisplay()
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
            this.Ironium = new System.Windows.Forms.Label();
            this.Energy = new System.Windows.Forms.Label();
            this.Germanium = new System.Windows.Forms.Label();
            this.Boranium = new System.Windows.Forms.Label();
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
            // Ironium
            // 
            this.Ironium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ironium.Location = new System.Drawing.Point(80, 0);
            this.Ironium.Name = "Ironium";
            this.Ironium.Size = new System.Drawing.Size(56, 16);
            this.Ironium.TabIndex = 7;
            this.Ironium.Text = "0";
            this.Ironium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Energy
            // 
            this.Energy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Energy.Location = new System.Drawing.Point(80, 48);
            this.Energy.Name = "Energy";
            this.Energy.Size = new System.Drawing.Size(56, 16);
            this.Energy.TabIndex = 8;
            this.Energy.Text = "0";
            this.Energy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Germanium
            // 
            this.Germanium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Germanium.Location = new System.Drawing.Point(80, 32);
            this.Germanium.Name = "Germanium";
            this.Germanium.Size = new System.Drawing.Size(56, 16);
            this.Germanium.TabIndex = 9;
            this.Germanium.Text = "0";
            this.Germanium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Boranium
            // 
            this.Boranium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Boranium.Location = new System.Drawing.Point(80, 16);
            this.Boranium.Name = "Boranium";
            this.Boranium.Size = new System.Drawing.Size(56, 16);
            this.Boranium.TabIndex = 10;
            this.Boranium.Text = "0";
            this.Boranium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ResourceDisplay
            // 
            this.Controls.Add(this.Boranium);
            this.Controls.Add(this.Germanium);
            this.Controls.Add(this.Energy);
            this.Controls.Add(this.Ironium);
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
        public Resources Value
        {
            set
            {
                try
                {
                    if (value == null) return;

                    Resources resources = value;

                    Ironium.Text = Convert.ToInt32(resources.Ironium).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    Boranium.Text = Convert.ToInt32(resources.Boranium).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    Germanium.Text = Convert.ToInt32(resources.Germanium).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    Energy.Text = Convert.ToInt32(resources.Energy).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    Report.Error("Unable to convert resource values.");
                }
            }

            get
            {
                Resources resources = new Resources();

                resources.Ironium = Convert.ToInt32(Ironium.Text);
                resources.Boranium = Convert.ToInt32(Boranium.Text);
                resources.Germanium = Convert.ToInt32(Germanium.Text);
                resources.Energy = Convert.ToInt32(Energy.Text);

                return resources;
            }
        }

        #endregion Properties
    }
}
