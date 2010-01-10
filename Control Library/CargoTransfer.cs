// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ControlLibrary
{

// ============================================================================
// An individual slider used in the cargo transfer dialog.
// ============================================================================

   public class CargoTransfer : System.Windows.Forms.UserControl
   {
      private int   cargoAmount;
      private int   availableAmount;
      private int   initialAmount;
      private int   sourceAmount;
      private Gauge cargoBay;

      private System.Windows.Forms.Label planetAmount;
      private System.Windows.Forms.Label sliderTitle;
      private System.Windows.Forms.Label shipAmount;
      private System.Windows.Forms.TrackBar sliderBar;
      private System.ComponentModel.Container components = null;


// ============================================================================
// Construction.
// ============================================================================

      public CargoTransfer()
      {
        InitializeComponent();
      }


// ============================================================================
// Clean up any resources being used.
// ============================================================================

      protected override void Dispose(bool disposing)
      {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }

#region Component Designer generated code
     /// <summary> 
     /// Required method for Designer support - do not modify 
     /// the contents of this method with the code editor.
     /// </summary>
private void InitializeComponent()
     {
   this.planetAmount = new System.Windows.Forms.Label();
   this.sliderTitle = new System.Windows.Forms.Label();
   this.sliderBar = new System.Windows.Forms.TrackBar();
   this.shipAmount = new System.Windows.Forms.Label();
   ((System.ComponentModel.ISupportInitialize)(this.sliderBar)).BeginInit();
   this.SuspendLayout();
   // 
   // planetAmount
   // 
   this.planetAmount.Location = new System.Drawing.Point(8, 16);
   this.planetAmount.Name = "planetAmount";
   this.planetAmount.Size = new System.Drawing.Size(48, 16);
   this.planetAmount.TabIndex = 25;
   this.planetAmount.Text = "0 kT";
   this.planetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
   // 
   // sliderTitle
   // 
   this.sliderTitle.Location = new System.Drawing.Point(80, 0);
   this.sliderTitle.Name = "sliderTitle";
   this.sliderTitle.Size = new System.Drawing.Size(120, 16);
   this.sliderTitle.TabIndex = 22;
   this.sliderTitle.Text = "Ironium";
   // 
   // sliderBar
   // 
   this.sliderBar.AutoSize = false;
   this.sliderBar.Location = new System.Drawing.Point(64, 16);
   this.sliderBar.Name = "sliderBar";
   this.sliderBar.Size = new System.Drawing.Size(256, 16);
   this.sliderBar.TabIndex = 21;
   this.sliderBar.TickStyle = System.Windows.Forms.TickStyle.None;
   this.sliderBar.Scroll += new System.EventHandler(this.sliderBar_Scroll);
   // 
   // shipAmount
   // 
   this.shipAmount.Location = new System.Drawing.Point(328, 16);
   this.shipAmount.Name = "shipAmount";
   this.shipAmount.Size = new System.Drawing.Size(56, 16);
   this.shipAmount.TabIndex = 26;
   this.shipAmount.Text = "0 kT";
   // 
   // CargoTransfer
   // 
   this.Controls.Add(this.shipAmount);
   this.Controls.Add(this.planetAmount);
   this.Controls.Add(this.sliderTitle);
   this.Controls.Add(this.sliderBar);
   this.Name = "CargoTransfer";
   this.Size = new System.Drawing.Size(384, 32);
   ((System.ComponentModel.ISupportInitialize)(this.sliderBar)).EndInit();
   this.ResumeLayout(false);

}
#endregion


// ============================================================================
// This function is invoked when the slider bar is moved.
// ============================================================================

      private void sliderBar_Scroll(object sender, System.EventArgs e) 
      {
         // Get the cargo mass excluding the contribution made by this slider

         int cargoMass       = (int) cargoBay.Value - cargoAmount;
         int requestedCargo  = sliderBar.Value;

         // If the amount requested by this slider exceeds the capacity then
         // set this slider capacity to what can be stored

         if (cargoMass + requestedCargo > cargoBay.Maximum) { 
            requestedCargo = (int) cargoBay.Maximum - cargoMass;
         }

         // if there is less than this on the planet then just take what is
         // there

         if (requestedCargo > sourceAmount) {
            requestedCargo = sourceAmount;
         }

         cargoAmount        = requestedCargo;
         sliderBar.Value    = cargoAmount;
         shipAmount.Text    = cargoAmount.ToString(System.Globalization.CultureInfo.InvariantCulture) + " kT"; 
         cargoBay.Value     = cargoMass + cargoAmount;

         availableAmount    = sourceAmount + initialAmount - cargoAmount;
         planetAmount.Text  = availableAmount.ToString(System.Globalization.CultureInfo.InvariantCulture) + " kT";
      }


// ============================================================================
// Properties.
// ============================================================================

      [Description("Slider Title."), Category("User Data")]
      public string Title {
         get { return sliderTitle.Text; }
         set { sliderTitle.Text = value; }
      }

      /// Maximum slider value.

      public int Maximum {
         set { sliderBar.Maximum = value; }
         get { return sliderBar.Maximum;  }
      }

      /// Slider value.

      public int Value {
         set {
            cargoAmount     = value;
            initialAmount   = cargoAmount;
            sliderBar.Value = cargoAmount; 
            shipAmount.Text = cargoAmount.ToString(System.Globalization.CultureInfo.InvariantCulture) + " kT"; 
         }

         get { return cargoAmount; }
      }

      public int Available {
         set {
            sourceAmount      = value;
            planetAmount.Text = sourceAmount.ToString(System.Globalization.CultureInfo.InvariantCulture) + " kT";
         }
      }

      public Gauge Limit {
         set {
            cargoBay = value;
         }
      }

      public int Taken {
         get { return cargoAmount - initialAmount; }
      }
   }
}
