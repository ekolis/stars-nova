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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ControlLibrary
{

    public class Gauge : System.Windows.Forms.UserControl
    {
        private Font font              = new Font("Arial", 8);
        private SolidBrush BarBrush    = new SolidBrush(Color.LightGreen);
        private SolidBrush MarkerBrush = new SolidBrush(Color.Green);
        private SolidBrush blackBrush  = new SolidBrush(Color.Black);
        private StringFormat format    = new StringFormat();
        private bool ShowBarText       = false;

        private double maximumValue = 0;
        private double minimumValue = 0;
        private double topValue     = 0;
        private double bottomValue  = 0;
        private int markerValue     = 0;
        private string barUnits     = null;

        private System.Windows.Forms.Label Bar;
        private System.ComponentModel.Container components = null;


        #region Construction and Dispose
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Gauge()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
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
            this.Bar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Bar
            // 
            this.Bar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Bar.Location = new System.Drawing.Point(0, 0);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(152, 16);
            this.Bar.TabIndex = 0;
            this.Bar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Bar.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // Gauge
            // 
            this.Controls.Add(this.Bar);
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
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); //added

            if (maximumValue == 0) return;

            double gaugeSpan = maximumValue - minimumValue;
            double gaugeScale = Bar.Size.Width / gaugeSpan;

            float barLeft = (float)((bottomValue - minimumValue) * gaugeScale);
            float barRight = (float)((topValue - minimumValue) * gaugeScale);

            float barWidth = barRight - barLeft;
            float barHeight = Bar.Size.Height;

            e.Graphics.FillRectangle(BarBrush, barLeft, 0,
                                               barWidth, barHeight);

            if (Marker != 0)
            {
                float markerWidth = (float)(barHeight / 1.5);
                float markerHeight = (float)(barHeight / 1.5);
                float markerStart = (float)(Bar.Size.Width * Marker) / 100;

                e.Graphics.TranslateTransform(markerStart, 0);
                e.Graphics.RotateTransform(45);
                e.Graphics.FillRectangle(MarkerBrush, 0, 0,
                                                      markerWidth, markerHeight);
                e.Graphics.ResetTransform();
            }

            if (ShowBarText)
            {
                RectangleF rect = new RectangleF(Bar.Location, Bar.Size);

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
        /// Get or Set the value to display
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Value to display."), Category("Nova")]
        public double Value
        {
            get { return topValue; }
            set { topValue = value; Bar.Invalidate(); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the top value of the bar display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Top value of bar display."), Category("Nova")]
        public double TopValue
        {
            get { return topValue; }
            set { topValue = value; Bar.Invalidate(); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or set the bottom value of the bar display.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Botton value of bar display."), Category("Nova")]
        public double BottomValue
        {
            get { return bottomValue; }
            set { bottomValue = value; Bar.Invalidate(); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the marker position.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Marker position (%)."), Category("Nova")]
        public int Marker
        {
            get { return markerValue; }
            set { markerValue = value; Bar.Invalidate(); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the displayed text.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Display textual value of bar."), Category("Nova")]
        public bool ShowText
        {
            get { return ShowBarText; }
            set { ShowBarText = value; }
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
        public String Units
        {
            get { return barUnits; }
            set { barUnits = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the bar colour
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Bar Colour."), Category("Nova")]
        public Color BarColour
        {
            get { return BarBrush.Color; }
            set { BarBrush = new SolidBrush(value); }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set or Get the Marker Colour
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Marker Colour."), Category("Nova")]
        public Color MarkerColour
        {
            get { return MarkerBrush.Color; }
            set { MarkerBrush = new SolidBrush(value); }
        }

        #endregion Properties

    }
}
