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
// A simple gauge control.
// ===========================================================================
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Nova.ControlLibrary
{

    /// <summary>
    /// A simple gauge control.
    /// </summary>
    public class Gauge : System.Windows.Forms.UserControl
    {
        private readonly Font font              = new Font("Arial", 8);
        private readonly SolidBrush blackBrush  = new SolidBrush(Color.Black);
        private readonly StringFormat format    = new StringFormat();
        private SolidBrush barBrush = new SolidBrush(Color.LightGreen);
        private SolidBrush markerBrush = new SolidBrush(Color.Green);
        private bool showBarText;

        private double maximumValue;
        private double minimumValue;
        private double topValue;
        private double bottomValue;
        private int markerValue;
        private string barUnits;

        private System.Windows.Forms.Label bar;
        private System.ComponentModel.Container components = null;


        #region Construction and Dispose

        /// <summary>
        /// Initializes a new instance of the Gauge class.
        /// </summary>
        public Gauge()
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


        // ============================================================================
        // Designer Generated Code
        // ============================================================================
        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Bar
            // 
            this.bar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(152, 16);
            this.bar.TabIndex = 0;
            this.bar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bar.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // Gauge
            // 
            this.Controls.Add(this.bar);
            this.Name = "Gauge";
            this.Size = new System.Drawing.Size(152, 16);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw the gauge.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); // added

            if (maximumValue == 0) return;

            double gaugeSpan = maximumValue - minimumValue;
            double gaugeScale = this.bar.Size.Width / gaugeSpan;

            float barLeft = (float)((bottomValue - minimumValue) * gaugeScale);
            float barRight = (float)((topValue - minimumValue) * gaugeScale);

            float barWidth = barRight - barLeft;
            float barHeight = this.bar.Size.Height;

            e.Graphics.FillRectangle(this.barBrush, barLeft, 0, barWidth, barHeight);

            if (Marker != 0)
            {
                float markerWidth = (float)(barHeight / 1.5);
                float markerHeight = (float)(barHeight / 1.5);
                float markerStart = (float)(this.bar.Size.Width * Marker) / 100;

                e.Graphics.TranslateTransform(markerStart, 0);
                e.Graphics.RotateTransform(45);
                e.Graphics.FillRectangle(this.markerBrush, 0, 0, markerWidth, markerHeight);
                e.Graphics.ResetTransform();
            }

            if (this.showBarText)
            {
                RectangleF rect = new RectangleF(this.bar.Location, this.bar.Size);

                string description = topValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + " of "
                                   + maximumValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + barUnits;

                format.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(description, font, blackBrush, rect, format);
            }
        }

        #endregion

        // ============================================================================
        // Properties. Value, Marker, TopValue and BottomValue are expected to be set
        // at run time. The others are intended for use at design time.
        // ============================================================================
        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the value to display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Value to display."), Category("Nova")]
        public double Value
        {
            get
            {
                return topValue;
            }
            set
            {
                topValue = value;
                this.bar.Invalidate();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the top value of the bar display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Top value of bar display."), Category("Nova")]
        public double TopValue
        {
            get
            {
                return topValue;
            }
            set
            {
                topValue = value;
                this.bar.Invalidate();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or set the bottom value of the bar display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Botton value of bar display."), Category("Nova")]
        public double BottomValue
        {
            get
            {
                return bottomValue;
            }
            set
            {
                bottomValue = value;
                this.bar.Invalidate();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the marker position.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Marker position (%)."), Category("Nova")]
        public int Marker
        {
            get
            {
                return markerValue;
            }
            set
            {
                markerValue = value;
                this.bar.Invalidate();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the displayed text.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Display textual value of bar."), Category("Nova")]
        public bool ShowText
        {
            get { return this.showBarText; }
            set { this.showBarText = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the maximum value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Maximum value."), Category("Nova")]
        public double Maximum
        {
            get { return maximumValue; }
            set { maximumValue = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the minimum value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Minimum value."), Category("Nova")]
        public double Minimum
        {
            get { return minimumValue; }
            set { minimumValue = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the units to display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Units to display."), Category("Nova")]
        public string Units
        {
            get { return barUnits; }
            set { barUnits = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the bar colour.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Bar Colour."), Category("Nova")]
        public Color BarColour
        {
            get { return this.barBrush.Color; }
            set { this.barBrush = new SolidBrush(value); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set or Get the Marker Colour.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Marker Colour."), Category("Nova")]
        public Color MarkerColour
        {
            get { return this.markerBrush.Color; }
            set { this.markerBrush = new SolidBrush(value); }
        }

        #endregion Properties

    }
}
