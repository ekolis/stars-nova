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
// This file implements a range control class, used for representing a race's
// environmental tolerances. The control is 'dumb' and knows nothing about
// environmental tolerances. It simply shows a bar in a box which can
// expand from 1 to 100, as well as a title and caption.
//
// (RaceDesigner sets the title to the environment variable being changed,
// and uses the caption to show the race's tolerance range.)
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NovaCommon;

namespace ControlLibrary
{
    public class Range : System.Windows.Forms.UserControl
    {
        enum TimerOptions
        {
            IllegalValue, MoveLeft, MoveRight, Shrink, Expand
        }

        // ----------------------------------------------------------------------------
        // Non-VS variables:
        //
        // BoxBrush             Brush used to paint the indicator bar
        // BoxColor            The color of the range control indicator box
        // BoxLeftPosition     Position of left side of bar (%)
        // BoxMoveIncrement    How much to move the indicator box per timer tick
        // BoxRightPosition    Position of right side of bar (%)
        // BoxTitle            Title for range box
        // Environment         Indicates which trait is being manipulated
        // IndicatorBrush      Colour to be used for indicator within bar
        // BoxMinimum          Specifies the (real) minimum value of the range
        // BoxMaximum          Specifies the (real) maximum value of the range
        // TimerAction         Determines how the bar is manipulated on a timer tick
        // Units               Holds the units of the selected environment item
        // BoxOldLeftPosition  Previous value before a change (%)
        // BoxOldRightPosition Previous value before a change (%)
        // ----------------------------------------------------------------------------

        private Color BoxColor = Color.Yellow;
        private SolidBrush BoxBrush = new SolidBrush(Color.Yellow);
        private int BoxMinimum = 0;
        private int BoxMaximum = 100;
        private int BoxLeftPosition = 15;
        private int BoxRightPosition = 85;
        private int BoxOldLeftPosition = 15;
        private int BoxOldRightPosition = 85;
        private int BoxMoveIncrement = 1;
        private TimerOptions TimerAction;

        // values used in converting the bar position to environment values
        private double EnvironmentMinimum = 0.0; // Temp -200; Rad 0.0; Grav 0.0;
        private double EnvironmentMaximum = 100.0; // Temp 200; Rad 100; Grav 10;
        private string Units = "Units";

        // Event and delegate definition for when the range is changed.
        // This event tells RaceDesigner to modify the race's advantage points
        public delegate void RangeChangedHandler(Object sender,
                                                 int newLeftValue,
                                                 int newRightValue,
                                                 int oldLeftValue,
                                                 int oldRightValue);

        public event RangeChangedHandler RangeChanged;

        // ----------------------------------------------------------------------------
        // VS-created variables
        // ----------------------------------------------------------------------------

        private System.Windows.Forms.Button LeftScroll;
        private System.Windows.Forms.Label Bar;
        private System.Windows.Forms.Button RightScroll;
        private System.Windows.Forms.Button Expand;
        private System.Windows.Forms.Button Contract;
        private System.Windows.Forms.GroupBox Title;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label BoxSpan;
        private System.ComponentModel.IContainer components;

        #region Construction and Disposal

        /// <summary>
        /// Construction (and initialisation)
        /// </summary>
        public Range()
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

        // ===========================================================================
        #region Component Designer generated code
        /// <summary> Required method for Designer support - do not
        /// modify the contents of this method with the code editor.
        /// </summary>
        // ===========================================================================

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LeftScroll = new System.Windows.Forms.Button();
            this.Bar = new System.Windows.Forms.Label();
            this.RightScroll = new System.Windows.Forms.Button();
            this.Expand = new System.Windows.Forms.Button();
            this.Contract = new System.Windows.Forms.Button();
            this.BoxSpan = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftScroll
            // 
            this.LeftScroll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LeftScroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftScroll.Location = new System.Drawing.Point(20, 24);
            this.LeftScroll.Name = "LeftScroll";
            this.LeftScroll.Size = new System.Drawing.Size(20, 20);
            this.LeftScroll.TabIndex = 1;
            this.LeftScroll.Text = "<";
            this.LeftScroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftScroll_MouseDown);
            this.LeftScroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Bar
            // 
            this.Bar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Bar.Location = new System.Drawing.Point(48, 24);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(224, 20);
            this.Bar.TabIndex = 2;
            this.Bar.Paint += new System.Windows.Forms.PaintEventHandler(this.Bar_Paint);
            // 
            // RightScroll
            // 
            this.RightScroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightScroll.Location = new System.Drawing.Point(272, 23);
            this.RightScroll.Name = "RightScroll";
            this.RightScroll.Size = new System.Drawing.Size(20, 20);
            this.RightScroll.TabIndex = 3;
            this.RightScroll.Text = ">";
            this.RightScroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightScroll_MouseDown);
            this.RightScroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Expand
            // 
            this.Expand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expand.Location = new System.Drawing.Point(40, 52);
            this.Expand.Name = "Expand";
            this.Expand.Size = new System.Drawing.Size(40, 24);
            this.Expand.TabIndex = 4;
            this.Expand.Text = "< >";
            this.Expand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Expand_MouseDown);
            this.Expand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Contract
            // 
            this.Contract.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contract.Location = new System.Drawing.Point(224, 51);
            this.Contract.Name = "Contract";
            this.Contract.Size = new System.Drawing.Size(40, 24);
            this.Contract.TabIndex = 5;
            this.Contract.Text = "> <";
            this.Contract.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Contract_MouseDown);
            this.Contract.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // BoxSpan
            // 
            this.BoxSpan.Location = new System.Drawing.Point(86, 52);
            this.BoxSpan.Name = "BoxSpan";
            this.BoxSpan.Size = new System.Drawing.Size(129, 15);
            this.BoxSpan.TabIndex = 6;
            this.BoxSpan.Text = "0 to 0";
            this.BoxSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            this.Title.Controls.Add(this.RightScroll);
            this.Title.Controls.Add(this.BoxSpan);
            this.Title.Controls.Add(this.Expand);
            this.Title.Controls.Add(this.Contract);
            this.Title.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Title.Location = new System.Drawing.Point(8, 1);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(308, 86);
            this.Title.TabIndex = 7;
            this.Title.TabStop = false;
            this.Title.Text = "Not Defined";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Range
            // 
            this.Controls.Add(this.Bar);
            this.Controls.Add(this.LeftScroll);
            this.Controls.Add(this.Title);
            this.Name = "Range";
            this.Size = new System.Drawing.Size(324, 95);
            this.Title.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// <summary>
        /// Paint the indicator part of the Bar control and the label displaying the
        /// numeric values. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Bar_Paint(object sender, PaintEventArgs e)
        {
            int fillHeight = Bar.Size.Height;
            int fillY = 0;

            // Need to convert the values from actual environment values to drawing co-ordinates.
            int boxRange = BoxMaximum - BoxMinimum;
            if (boxRange == 0) boxRange = 100; // gaurd against div by 0
            int fillLeft = (int)((BoxLeftPosition * Bar.Size.Width) / boxRange);
            int fillRight = (int)((BoxRightPosition * Bar.Size.Width) / boxRange);
            int fillWidth = fillRight - fillLeft;


            string realRange = String.Format("{0} to {1} {2}",
                                BarPositionToEnvironmentValue(BoxLeftPosition).ToString("F1"),
                                BarPositionToEnvironmentValue(BoxRightPosition).ToString("F1"),
                                Units);

            BoxSpan.Text = realRange;

            e.Graphics.FillRectangle(BoxBrush, fillLeft, fillY,
                                     fillWidth, fillHeight);

        }


        /// <summary>
        /// Called when the timer ticks. As only one button can be held down at a time
        /// we use the same timer rourine to process all four buttons with the desired
        /// action being held in the class variable TimerAction.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            int increment = BoxMoveIncrement;

            BoxOldRightPosition = BoxRightPosition;
            BoxOldLeftPosition = BoxLeftPosition;

            switch (TimerAction)
            {
                case TimerOptions.MoveLeft:
                    if (BoxLeftPosition - increment < 0)
                    {
                        increment = BoxLeftPosition;
                    }

                    BoxLeftPosition -= increment;
                    BoxRightPosition -= increment;
                    break;

                case TimerOptions.MoveRight:
                    if (BoxRightPosition + increment > 100)
                    {
                        increment = 100 - BoxRightPosition;
                    }

                    BoxLeftPosition += increment;
                    BoxRightPosition += increment;
                    break;

                case TimerOptions.Shrink:
                    int boxWidth = BoxRightPosition - BoxLeftPosition;
                    if (boxWidth - increment < 20)
                    {
                        increment = (boxWidth - 20) / 2;
                    }

                    BoxLeftPosition += increment;
                    BoxRightPosition -= increment;
                    break;

                case TimerOptions.Expand:
                    if (BoxRightPosition + increment > BoxMaximum)
                    {
                        increment = BoxMaximum - BoxRightPosition;
                    }

                    BoxRightPosition += increment;
                    increment = BoxMoveIncrement;

                    if (BoxLeftPosition - increment < BoxMinimum)
                    {
                        increment = BoxLeftPosition;
                    }

                    BoxLeftPosition -= increment;
                    break;
            }

            RangeChanged(this, BoxLeftPosition, BoxRightPosition,
                               BoxOldLeftPosition, BoxOldRightPosition);

            Bar.Invalidate();
        }


        // ===========================================================================
        // Mouse down on the move indicator left button
        // ===========================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftScroll_MouseDown(object sender, MouseEventArgs e)
        {
            TimerAction = TimerOptions.MoveLeft;
            timer1.Start();
        }

        ///
        // ===========================================================================
        // Use this route for MouseUp on all four buttons. The action is always the
        // same, just stop the timer.
        // ===========================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RangeMouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }


        // ===========================================================================
        // Mouse down on the move indicator left button
        // ===========================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightScroll_MouseDown(object sender, MouseEventArgs e)
        {
            TimerAction = TimerOptions.MoveRight;
            timer1.Start();
        }


        // ===========================================================================
        // Shrink the range covered by the indicator bar
        // ===========================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contract_MouseDown(object sender, MouseEventArgs e)
        {
            TimerAction = TimerOptions.Shrink;
            timer1.Start();
        }


        // ===========================================================================
        // Expand the range covered by the indicator bar.
        // ===========================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Expand_MouseDown(object sender, MouseEventArgs e)
        {
            TimerAction = TimerOptions.Expand;
            timer1.Start();
        }

        #endregion Event Methods

        #region Utility Methods

        /// <summary>
        /// Conversion from bar position (1-100 scale) to environment value (EnvironmentMinimum - EnvironmentMaximum)
        /// </summary>
        /// <param name="pos">A bar position value from 1-100.</param>
        /// <returns>An environment value.</returns>
        private double BarPositionToEnvironmentValue(int pos)
        {
            double env = 0; // the environment value to be calculated.
            double environmentRange = EnvironmentMaximum - EnvironmentMinimum;
            double boxRange = (double)(BoxMaximum - BoxMinimum);
            if (boxRange == 0)
            {
                environmentRange = 1000; // gaurd against div by 0
            }
            else
            {
                env = ((double)(pos - BoxMinimum)) * environmentRange / boxRange + EnvironmentMinimum;
            }
            return env;
        }

        /// <summary>
        /// Conversion from an environment value (EnvironmentMinimum - EnvironmentMaximum) to a bar position (1-100)
        /// </summary>
        /// <param name="env">An environment value.</param>
        /// <returns>A bar position (1-100)</returns>
        private int EnvironmentValueToBarPosition(double env)
        {
            int pos = 0;
            double environmentRange = EnvironmentMaximum - EnvironmentMinimum;
            if (environmentRange == 0)
            {
                pos = 100; // gaurd against div by 0
            }
            else
            {
                double boxRange = (double)(BoxMaximum - BoxMinimum);
                pos = (int)((((env - EnvironmentMinimum) * boxRange) / environmentRange) + BoxMinimum);
            }
            return pos;
        }

        #endregion Utility Methods

        #region Properties


        /// <summary>
        /// Return or set the range minimum and maxium values and the upper and lower bounds
        /// of the current selection (in real environment units).
        /// </summary>
        public EnvironmentTolerance EnvironmentValues
        {
            get
            {
                double MinimumValue = BarPositionToEnvironmentValue(BoxLeftPosition);
                double MaximumValue = BarPositionToEnvironmentValue(BoxRightPosition);
                return new EnvironmentTolerance(MinimumValue, MaximumValue);
            }
            set
            {
                BoxOldRightPosition = BoxRightPosition;
                BoxOldLeftPosition = BoxLeftPosition;

                BoxLeftPosition = EnvironmentValueToBarPosition(value.Minimum);
                BoxRightPosition = EnvironmentValueToBarPosition(value.Maximum);

                if (RangeChanged != null)
                    RangeChanged(this, BoxLeftPosition, BoxRightPosition, BoxOldLeftPosition, BoxOldRightPosition);

                Bar.Invalidate();
            }
        }


        /// <summary>
        /// Units for range display numeric values. 
        /// </summary>
        /// <remarks>
        /// The call to Bar.Invalidate just ensures the control gets updated as we haven't changed any form
        /// components that would trigger a refresh.
        /// </remarks>
        [Description("Units of range display."), Category("Nova")]
        public string RangeUnits
        {
            get { return Units; }
            set { Units = value; Bar.Invalidate(); }
        }


        /// <summary>
        /// Get or Set range control title.
        /// </summary>
        [Description("Title of range display."), Category("Nova")]
        public string RangeTitle
        {
            get { return Title.Text; }
            set { Title.Text = value; }
        }


        /// <summary>
        /// Get or Set range control maximum value.
        /// </summary>
        [Description("Maximum value of range bar."), Category("Nova")]
        public double RangeMaximum
        {

            get { return EnvironmentMaximum; }
            set { EnvironmentMaximum = value; Bar.Invalidate(); }
        }

        /// <summary>
        /// Get or Set range control minimum value.
        /// </summary>
        [Description("Minimum value of range bar."), Category("Nova")]
        public double RangeMinimum
        {
            get { return EnvironmentMinimum; }
            set { EnvironmentMinimum = value; Bar.Invalidate(); }
        }

        /// <summary>
        /// Get or Set range control uper value.
        /// </summary>
        [Description("Upper value of range bar."), Category("Nova")]
        public double BarUpper
        {
            get { return BarPositionToEnvironmentValue(BoxRightPosition); }
            set
            {

                BoxOldRightPosition = BoxRightPosition;
                BoxRightPosition = EnvironmentValueToBarPosition(value);
                if (RangeChanged != null)
                    RangeChanged(this, BoxLeftPosition, BoxRightPosition, BoxOldLeftPosition, BoxOldRightPosition);
                Bar.Invalidate();
            }
        }

        /// <summary>
        /// Get or Set range control lower bar value.
        /// </summary>
        [Description("Lower value of range bar."), Category("Nova")]
        public double BarLower
        {
            get { return BarPositionToEnvironmentValue(BoxLeftPosition); }
            set
            {
                BoxOldLeftPosition = BoxLeftPosition;
                BoxLeftPosition = EnvironmentValueToBarPosition(value);
                if (RangeChanged != null)
                    RangeChanged(this, BoxLeftPosition, BoxRightPosition, BoxOldLeftPosition, BoxOldRightPosition);
                Bar.Invalidate();
            }
        }

        /// <summary>
        /// Get or Set range control bar colour
        /// </summary>
        [Description("Colour of range bar."), Category("Nova")]
        public Color RangeBarColor
        {
            get { return BoxColor; }

            set
            {
                BoxColor = value;
                BoxBrush = new SolidBrush(BoxColor);
                Bar.Invalidate();
            }
        }

        #endregion Properties
    }
}

