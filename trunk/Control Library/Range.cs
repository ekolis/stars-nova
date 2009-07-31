// This file needs -*- c++ -*- mode
// ===========================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// This file implements a range bar class.
// ===========================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NovaCommon;

namespace ControlLibrary {


// ===========================================================================
// Range class.
// ===========================================================================

   public class Range : System.Windows.Forms.UserControl 
   {
      enum TimerOptions {
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

      private Color        BoxColor            = Color.Yellow;
      private SolidBrush   BoxBrush            = new SolidBrush(Color.Yellow);
      private double       BoxMinimum          = 1;
      private double       BoxMaximum          = 100;
      private int          BoxLeftPosition     = 15;
      private int          BoxRightPosition    = 85;
      private int          BoxOldLeftPosition  = 15;
      private int          BoxOldRightPosition = 85;
      private int          BoxMoveIncrement    = 2;
      private string       Units               = "Units";
      private TimerOptions TimerAction;

      // Event and delegate definition for when the range is changed.

      public delegate void RangeChangedHandler(Object sender, 
                                               int newLeftValue,
                                               int newRightValue,
                                               int oldLeftValue,
                                               int oldRightValue);

      public event         RangeChangedHandler RangeChanged;

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


// ===========================================================================
// Construction (and initialisation)
// ===========================================================================

   public Range() {
      InitializeComponent();
   }


// ===========================================================================
// Clean up any resources being used.
// ===========================================================================

   protected override void Dispose(bool disposing) {
      if (disposing) {
         if (components != null) {
            components.Dispose();
         }
      }
      base.Dispose(disposing);
   }


// ===========================================================================
#region Component Designer generated code
/// <summary> Required method for Designer support - do not
/// modify the contents of this method with the code editor.
/// </summary>
// ===========================================================================

   private void InitializeComponent() {
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

 
// ===========================================================================
// Function to convert a value on the bar indicator (%) into the real values
// expressed by the BoxMinimum and BoxMaximum bounds.
// ===========================================================================

      public double ConvertToRealUnits(int barValue) 
      {
         double rangeSpan = BoxMaximum - BoxMinimum;
         double realValue = BoxMinimum + ((rangeSpan * barValue) / 100.0);

         return realValue;
      }


// ===========================================================================
// Paint the indicator part of the Bar control and the label displaying the
// numeric values. The bar itself works in integer units of 0 to 100 (%)
// (scaled to suit the bar physical size). Real bar units are only computed
// when required.
// ===========================================================================

      private void Bar_Paint(object sender, PaintEventArgs e) 
      {
         int fillHeight = Bar.Size.Height;
         int fillY      = 0;
         int fillLeft   = (BoxLeftPosition  * Bar.Size.Width) / 100;
         int fillRight  = (BoxRightPosition * Bar.Size.Width) / 100;
         int fillWidth  = fillRight - fillLeft;

         e.Graphics.FillRectangle(BoxBrush, fillLeft, fillY, 
                                  fillWidth, fillHeight);

         double From = ConvertToRealUnits(BoxLeftPosition);
         double To   = ConvertToRealUnits(BoxRightPosition);

         string realRange = String.Format("{0} to {1} {2}", 
                                          From.ToString("F1"), 
                                          To.ToString("F1"),
                                          Units);

         BoxSpan.Text = realRange;
      }


// ===========================================================================
// Called when the timer ticks. As only one button can be held down at a time
// we use the same timer rourine to process all four buttons with the desired
// action being held in the class variable TimerAction.
// ===========================================================================

      private void timer1_Tick(object sender, System.EventArgs e) 
      {
         int increment = BoxMoveIncrement;

         BoxOldRightPosition = BoxRightPosition;
         BoxOldLeftPosition  = BoxLeftPosition;

         switch (TimerAction) {
            case TimerOptions.MoveLeft:
               if (BoxLeftPosition - increment < 0) {
                  increment = BoxLeftPosition;
               }
            
               BoxLeftPosition  -= increment;
               BoxRightPosition -= increment;
               break;

            case TimerOptions.MoveRight:
               if (BoxRightPosition + increment > 100) {
                  increment = 100 - BoxRightPosition;
               }

               BoxLeftPosition  += increment;
               BoxRightPosition += increment;
               break;

            case TimerOptions.Shrink:
               int boxWidth = BoxRightPosition - BoxLeftPosition;
               if (boxWidth - increment < 20) {
                  increment = (boxWidth - 20) / 2;
               }

               BoxLeftPosition  += increment;
               BoxRightPosition -= increment;
               break;

            case TimerOptions.Expand:
               if (BoxRightPosition + increment > 100) {
                  increment         = 100 - BoxRightPosition;
               }

               BoxRightPosition += increment;
               increment         = BoxMoveIncrement;

               if (BoxLeftPosition - increment < 0) {
                  increment = BoxLeftPosition;
               }

               BoxLeftPosition -= increment;
               break;
         }

         RangeChanged(this, BoxLeftPosition,    BoxRightPosition,
                            BoxOldLeftPosition, BoxOldRightPosition); 

         Bar.Invalidate();
      }


// ===========================================================================
// Mouse down on the move indicator left button
// ===========================================================================

      private void LeftScroll_MouseDown(object sender, MouseEventArgs e)
      {
         TimerAction = TimerOptions.MoveLeft;
         timer1.Start();
      }


// ===========================================================================
// Use this route for MouseUp on all four buttons. The action is always the
// same, just stop the timer.
// ===========================================================================

      private void RangeMouseUp(object sender, MouseEventArgs e) 
      {
         timer1.Stop();
      }


// ===========================================================================
// Mouse down on the move indicator left button
// ===========================================================================

      private void RightScroll_MouseDown(object sender, MouseEventArgs e)
      {
         TimerAction = TimerOptions.MoveRight;
         timer1.Start();
      }


// ===========================================================================
// Shrink the range covered by the indicator bar
// ===========================================================================

      private void Contract_MouseDown(object sender, MouseEventArgs e) 
      {
         TimerAction = TimerOptions.Shrink;
         timer1.Start();
      }


// ===========================================================================
// Expand the range covered by the indicator bar.
// ===========================================================================

      private void Expand_MouseDown(object sender, MouseEventArgs e)
      {
         TimerAction = TimerOptions.Expand;
         timer1.Start();
      }


// ===========================================================================
// Return or set the range minimum and maxium values and the upper and lower bounds
// of the current selection in real units.
// ===========================================================================

       public EnvironmentTolerance Values
       {
           get
           {
               double MinimumValue = ConvertToRealUnits(BoxLeftPosition);
               double MaximumValue = ConvertToRealUnits(BoxRightPosition);
               return new EnvironmentTolerance(MinimumValue, MaximumValue);
           }
           set
           {
               BoxMinimum = value.Minimum;
               BoxMaximum = value.Maximum;
           }
       }


// ===========================================================================
// Units for range display numeric values. The call to Bar.Invalidate just
// ensures the control gets updated as we haven't changed any form
// components that would trigger a refresh.
// ===========================================================================

      [Description("Units of range display."), Category("Nova")]
      public string RangeUnits {
         get { return Units;  }
         set { Units = value; Bar.Invalidate(); }
      }


// ===========================================================================
// Range display title.
// ===========================================================================

      [Description("Title of range display."), Category("Nova")]
      public string RangeTitle {
         get { return Title.Text;  }
         set { Title.Text = value; }
      }


// ===========================================================================
// Range Maxium and Minimum values (in real units)
// ===========================================================================

      [Description("Maximum value of range bar."), Category("Nova")]
      public double RangeMaximum {
         get { return BoxMaximum;  }
         set { BoxMaximum = value; Bar.Invalidate(); } 
      }

      [Description("Minimum value of range bar."), Category("Nova")]
      public double RangeMinimum {
         get { return BoxMinimum;  }
         set { BoxMinimum = value; Bar.Invalidate(); } 
      }


// ===========================================================================
// Select range control bar colour
// ===========================================================================

      [Description("Colour of range bar."), Category("Nova")]
      public Color RangeBarColuor {
         get { return BoxColor; }

         set { 
            BoxColor = value;
            BoxBrush = new SolidBrush(BoxColor);
            Bar.Invalidate();
         }
      }

   }
}

