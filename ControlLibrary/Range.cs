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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nova.Common;
using Nova.Common.DataStructures;

namespace Nova.ControlLibrary
{
    public class Range : System.Windows.Forms.UserControl
    {
        private enum TimerOptions
        {
            IllegalValue, MoveLeft, MoveRight, Shrink, Expand
        }

        #region Non-VS variables
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

        private Color boxColor = Color.Yellow;
        private SolidBrush boxBrush = new SolidBrush(Color.Yellow);
        private int boxMinimum = 0;
        private int boxMaximum = 100;
        private int boxLeftPosition = 15;
        private int boxRightPosition = 85;
        private int boxOldLeftPosition = 15;
        private int boxOldRightPosition = 85;
        private int boxMoveIncrement = 1;
        private TimerOptions timerAction;

        // values used in converting the bar position to environment values
        private double environmentMinimum = 0.0; // Temp -200; Rad 0.0; Grav 0.0;
        private double environmentMaximum = 100.0; // Temp 200; Rad 100; Grav 10;
        private string units = "Units";

        // Event and delegate definition for when the range is changed.
        // This event tells RaceDesigner to modify the race's advantage points
        public delegate void RangeChangedHandler(
            object sender,
            int newLeftValue,
            int newRightValue,
            int oldLeftValue,
            int oldRightValue);

        public event RangeChangedHandler RangeChanged;
        
        #endregion


        #region VS-created variables
        private System.Windows.Forms.Button leftScroll;
        private System.Windows.Forms.Label bar;
        private System.Windows.Forms.Button rightScroll;
        private System.Windows.Forms.Button expand;
        private System.Windows.Forms.Button contract;
        private System.Windows.Forms.GroupBox title;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label boxSpan;
        private System.ComponentModel.IContainer components;
        #endregion

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the Range class.
        /// </summary>
        public Range()
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
        /// <summary> Required method for Designer support - do not
        /// modify the contents of this method with the code editor.
        /// </summary>
        // ===========================================================================

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.leftScroll = new System.Windows.Forms.Button();
            this.bar = new System.Windows.Forms.Label();
            this.rightScroll = new System.Windows.Forms.Button();
            this.expand = new System.Windows.Forms.Button();
            this.contract = new System.Windows.Forms.Button();
            this.boxSpan = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.title.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftScroll
            // 
            this.leftScroll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.leftScroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftScroll.Location = new System.Drawing.Point(20, 24);
            this.leftScroll.Name = "leftScroll";
            this.leftScroll.Size = new System.Drawing.Size(20, 20);
            this.leftScroll.TabIndex = 1;
            this.leftScroll.Text = "<";
            this.leftScroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftScroll_MouseDown);
            this.leftScroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Bar
            // 
            this.bar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bar.Location = new System.Drawing.Point(48, 24);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(224, 20);
            this.bar.TabIndex = 2;
            this.bar.Paint += new System.Windows.Forms.PaintEventHandler(this.Bar_Paint);
            // 
            // RightScroll
            // 
            this.rightScroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightScroll.Location = new System.Drawing.Point(272, 23);
            this.rightScroll.Name = "rightScroll";
            this.rightScroll.Size = new System.Drawing.Size(20, 20);
            this.rightScroll.TabIndex = 3;
            this.rightScroll.Text = ">";
            this.rightScroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightScroll_MouseDown);
            this.rightScroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Expand
            // 
            this.expand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expand.Location = new System.Drawing.Point(40, 52);
            this.expand.Name = "expand";
            this.expand.Size = new System.Drawing.Size(40, 24);
            this.expand.TabIndex = 4;
            this.expand.Text = "< >";
            this.expand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Expand_MouseDown);
            this.expand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // Contract
            // 
            this.contract.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contract.Location = new System.Drawing.Point(224, 51);
            this.contract.Name = "contract";
            this.contract.Size = new System.Drawing.Size(40, 24);
            this.contract.TabIndex = 5;
            this.contract.Text = "> <";
            this.contract.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Contract_MouseDown);
            this.contract.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeMouseUp);
            // 
            // BoxSpan
            // 
            this.boxSpan.Location = new System.Drawing.Point(86, 52);
            this.boxSpan.Name = "boxSpan";
            this.boxSpan.Size = new System.Drawing.Size(129, 15);
            this.boxSpan.TabIndex = 6;
            this.boxSpan.Text = "0 to 0";
            this.boxSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            this.title.Controls.Add(this.rightScroll);
            this.title.Controls.Add(this.boxSpan);
            this.title.Controls.Add(this.expand);
            this.title.Controls.Add(this.contract);
            this.title.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.title.Location = new System.Drawing.Point(8, 1);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(308, 86);
            this.title.TabIndex = 7;
            this.title.TabStop = false;
            this.title.Text = "Not Defined";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Range
            // 
            this.Controls.Add(this.bar);
            this.Controls.Add(this.leftScroll);
            this.Controls.Add(this.title);
            this.Name = "Range";
            this.Size = new System.Drawing.Size(324, 95);
            this.title.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Paint the indicator part of the Bar control and the label displaying the
        /// numeric values. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Bar_Paint(object sender, PaintEventArgs e)
        {
            int fillHeight = this.bar.Size.Height;
            int fillY = 0;

            // Need to convert the values from actual environment values to drawing co-ordinates.
            int fillLeft = (int)((this.boxLeftPosition * this.bar.Size.Width) / GetBoxRange());
            int fillRight = (int)((this.boxRightPosition * this.bar.Size.Width) / GetBoxRange());
            int fillWidth = fillRight - fillLeft;

            string realRange;
            if (this.units == Gravity.GetUnit())
            {
                realRange = String.Format(
                    "{0}{2} to {1}{2}",
                    Gravity.BarPositionToEnvironmentValue(this.boxLeftPosition).ToString("F2"),
                    Gravity.BarPositionToEnvironmentValue(this.boxRightPosition).ToString("F2"),
                    Gravity.GetUnit());
            }
            else
            {
                realRange = String.Format(
                    "{0}{2} to {1}{2}",
                    BarPositionToEnvironmentValue(this.boxLeftPosition).ToString("F0"),
                    BarPositionToEnvironmentValue(this.boxRightPosition).ToString("F0"),
                    this.units);
            }

            this.boxSpan.Text = realRange;

            e.Graphics.FillRectangle(this.boxBrush, fillLeft, fillY, fillWidth, fillHeight);

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when the timer ticks. As only one button can be held down at a time
        /// we use the same timer rourine to process all four buttons with the desired
        /// action being held in the class variable TimerAction.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Timer1_Tick(object sender, System.EventArgs e)
        {
            int increment = this.boxMoveIncrement;

            this.boxOldRightPosition = this.boxRightPosition;
            this.boxOldLeftPosition = this.boxLeftPosition;

            switch (this.timerAction)
            {
                case TimerOptions.MoveLeft:
                    if (this.boxLeftPosition - increment < 0)
                    {
                        increment = this.boxLeftPosition;
                    }

                    this.boxLeftPosition -= increment;
                    this.boxRightPosition -= increment;
                    break;

                case TimerOptions.MoveRight:
                    if (this.boxRightPosition + increment > 100)
                    {
                        increment = 100 - this.boxRightPosition;
                    }

                    this.boxLeftPosition += increment;
                    this.boxRightPosition += increment;
                    break;

                case TimerOptions.Shrink:
                    int boxWidth = this.boxRightPosition - this.boxLeftPosition;
                    if (boxWidth - increment < 20)
                    {
                        increment = (boxWidth - 20) / 2;
                    }

                    this.boxLeftPosition += increment;
                    this.boxRightPosition -= increment;
                    break;

                case TimerOptions.Expand:
                    if (this.boxRightPosition + increment > this.boxMaximum)
                    {
                        increment = this.boxMaximum - this.boxRightPosition;
                    }

                    this.boxRightPosition += increment;
                    increment = this.boxMoveIncrement;

                    if (this.boxLeftPosition - increment < this.boxMinimum)
                    {
                        increment = this.boxLeftPosition;
                    }

                    this.boxLeftPosition -= increment;
                    break;
            }

            RangeChanged(this, this.boxLeftPosition, this.boxRightPosition, this.boxOldLeftPosition, this.boxOldRightPosition);

            this.bar.Invalidate();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Mouse down on the move indicator left button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void LeftScroll_MouseDown(object sender, MouseEventArgs e)
        {
            this.timerAction = TimerOptions.MoveLeft;
            timer1.Start();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Use this route for MouseUp on all four buttons. The action is always the
        /// same, just stop the timer.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RangeMouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Mouse down on the move indicator left button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RightScroll_MouseDown(object sender, MouseEventArgs e)
        {
            this.timerAction = TimerOptions.MoveRight;
            timer1.Start();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Shrink the range covered by the indicator bar.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Contract_MouseDown(object sender, MouseEventArgs e)
        {
            this.timerAction = TimerOptions.Shrink;
            timer1.Start();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Expand the range covered by the indicator bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void Expand_MouseDown(object sender, MouseEventArgs e)
        {
            this.timerAction = TimerOptions.Expand;
            timer1.Start();
        }

        #endregion Event Methods

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Conversion from bar position (1-100 scale) to environment value (EnvironmentMinimum - EnvironmentMaximum).
        /// </summary>
        /// <param name="pos">A bar position value from 1-100.</param>
        /// <returns>An environment value.</returns>
        /// ----------------------------------------------------------------------------
        private double BarPositionToEnvironmentValue(int pos)
        {
            return ((((double)(pos - this.boxMinimum)) * GetEnvironmentRange()) / (double)GetBoxRange()) + this.environmentMinimum;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Conversion from an environment value (EnvironmentMinimum - EnvironmentMaximum) to a bar position (1-100).
        /// </summary>
        /// <param name="env">An environment value.</param>
        /// <returns>A bar position (1-100).</returns>
        /// ----------------------------------------------------------------------------
        private int EnvironmentValueToBarPosition(double env)
        {
            return (int)((((env - this.environmentMinimum) * (double)GetBoxRange()) / GetEnvironmentRange()) + this.boxMinimum);
        }

        private int GetBoxRange()
        {
            return this.boxMaximum - this.boxMinimum;
        }

        private double GetEnvironmentRange()
        {
            return this.environmentMaximum - this.environmentMinimum;
        }

        #endregion Utility Methods

        #region Properties


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return or set the range minimum and maxium values and the upper and lower bounds
        /// of the current selection (in real environment units).
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnvironmentTolerance EnvironmentValues
        {
            get
            {
                double minimumValue = BarPositionToEnvironmentValue(this.boxLeftPosition);
                double maximumValue = BarPositionToEnvironmentValue(this.boxRightPosition);
                return new EnvironmentTolerance(minimumValue, maximumValue);
            }
            set
            {
                this.boxOldRightPosition = this.boxRightPosition;
                this.boxOldLeftPosition = this.boxLeftPosition;

                this.boxLeftPosition = EnvironmentValueToBarPosition(value.MinimumRealValue);
                this.boxRightPosition = EnvironmentValueToBarPosition(value.MaximumRealValue);

                if (RangeChanged != null)
                    RangeChanged(this, this.boxLeftPosition, this.boxRightPosition, this.boxOldLeftPosition, this.boxOldRightPosition);

                this.bar.Invalidate();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Units for range display numeric values. 
        /// </summary>
        /// <remarks>
        /// The call to Bar.Invalidate just ensures the control gets updated as we haven't changed any form
        /// components that would trigger a refresh.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        [Description("Units of range display."), Category("Nova")]
        public string RangeUnits
        {
            get
            {
                return this.units;
            }
            set
            {
                this.units = value;
                this.bar.Invalidate();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control title.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Title of range display."), Category("Nova")]
        public string RangeTitle
        {
            get { return this.title.Text; }
            set { this.title.Text = value; }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control maximum value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Maximum value of range bar."), Category("Nova")]
        public double RangeMaximum
        {
            get
            {
                return this.environmentMaximum;
            }
            set
            {
                this.environmentMaximum = value;
                this.bar.Invalidate();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control minimum value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Minimum value of range bar."), Category("Nova")]
        public double RangeMinimum
        {
            get
            {
                return this.environmentMinimum;
            }
            set
            {
                this.environmentMinimum = value;
                this.bar.Invalidate();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control uper value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Upper value of range bar."), Category("Nova")]
        public double BarUpper
        {
            get 
            { 
                return BarPositionToEnvironmentValue(this.boxRightPosition); 
            }
            set
            {

                this.boxOldRightPosition = this.boxRightPosition;
                this.boxRightPosition = EnvironmentValueToBarPosition(value);
                if (RangeChanged != null)
                    RangeChanged(this, this.boxLeftPosition, this.boxRightPosition, this.boxOldLeftPosition, this.boxOldRightPosition);
                this.bar.Invalidate();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control lower bar value.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Lower value of range bar."), Category("Nova")]
        public double BarLower
        {
            get
            {
                return BarPositionToEnvironmentValue(this.boxLeftPosition);
            }
            set
            {
                this.boxOldLeftPosition = this.boxLeftPosition;
                this.boxLeftPosition = EnvironmentValueToBarPosition(value);
                if (RangeChanged != null)
                    RangeChanged(this, this.boxLeftPosition, this.boxRightPosition, this.boxOldLeftPosition, this.boxOldRightPosition);
                this.bar.Invalidate();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set range control bar colour.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Description("Colour of range bar."), Category("Nova")]
        public Color RangeBarColor
        {
            get
            {
                return this.boxColor;
            }

            set
            {
                this.boxColor = value;
                this.boxBrush = new SolidBrush(this.boxColor);
                this.bar.Invalidate();
            }
        }

        #endregion Properties
    }
}

