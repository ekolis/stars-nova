// This file needs -*- c++ -*- mode
// ===========================================================================
// Nova. (c) 2008 Ken Reed
//
/// Dialog for transferring cargo between a planet and a ship.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NovaCommon;


// ============================================================================
// Dialog for transferring cargo between a planet and a ship.
// ============================================================================

public class CargoDialog : System.Windows.Forms.Form
{
   private Fleet fleet;

   private System.Windows.Forms.Button cancelButton;
   private System.Windows.Forms.Button okButton;
   private ControlLibrary.CargoTransfer IroniumTransfer;
   private ControlLibrary.CargoTransfer boroniumTransfer;
   private ControlLibrary.CargoTransfer GermaniumTransfer;
   private ControlLibrary.CargoTransfer colonistsTransfer;
   private System.Windows.Forms.Label label1;
   private System.Windows.Forms.Label label2;
   private ControlLibrary.Gauge CargoBay;
   private System.Windows.Forms.Label label3;
   private System.ComponentModel.Container components = null;


// ============================================================================
// Construction.
// ============================================================================

   public CargoDialog()
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


#region Windows Form Designer generated code

/// <summary>
/// Required method for Designer support - do not modify
/// the contents of this method with the code editor.
/// </summary>

   private void InitializeComponent()
   {
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.IroniumTransfer = new ControlLibrary.CargoTransfer();
      this.boroniumTransfer = new ControlLibrary.CargoTransfer();
      this.GermaniumTransfer = new ControlLibrary.CargoTransfer();
      this.colonistsTransfer = new ControlLibrary.CargoTransfer();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.CargoBay = new ControlLibrary.Gauge();
      this.label3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cancelButton.Location = new System.Drawing.Point(352, 256);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 9;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.okButton.Location = new System.Drawing.Point(256, 256);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 10;
      this.okButton.Text = "OK";
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // IroniumTransfer
      // 
      this.IroniumTransfer.Location = new System.Drawing.Point(24, 48);
      this.IroniumTransfer.Maximum = 10;
      this.IroniumTransfer.Name = "IroniumTransfer";
      this.IroniumTransfer.Size = new System.Drawing.Size(384, 32);
      this.IroniumTransfer.TabIndex = 11;
      this.IroniumTransfer.Title = "Ironium";
      this.IroniumTransfer.Value = 0;
      // 
      // boroniumTransfer
      // 
      this.boroniumTransfer.Location = new System.Drawing.Point(24, 88);
      this.boroniumTransfer.Maximum = 10;
      this.boroniumTransfer.Name = "boroniumTransfer";
      this.boroniumTransfer.Size = new System.Drawing.Size(384, 32);
      this.boroniumTransfer.TabIndex = 12;
      this.boroniumTransfer.Title = "Boranium";
      this.boroniumTransfer.Value = 0;
      // 
      // GermaniumTransfer
      // 
      this.GermaniumTransfer.Location = new System.Drawing.Point(24, 128);
      this.GermaniumTransfer.Maximum = 10;
      this.GermaniumTransfer.Name = "GermaniumTransfer";
      this.GermaniumTransfer.Size = new System.Drawing.Size(384, 32);
      this.GermaniumTransfer.TabIndex = 13;
      this.GermaniumTransfer.Title = "Germanium";
      this.GermaniumTransfer.Value = 0;
      // 
      // colonistsTransfer
      // 
      this.colonistsTransfer.Location = new System.Drawing.Point(24, 168);
      this.colonistsTransfer.Maximum = 10;
      this.colonistsTransfer.Name = "colonistsTransfer";
      this.colonistsTransfer.Size = new System.Drawing.Size(384, 32);
      this.colonistsTransfer.TabIndex = 14;
      this.colonistsTransfer.Title = "Colonists";
      this.colonistsTransfer.Value = 0;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 32);
      this.label1.TabIndex = 15;
      this.label1.Text = "Resources On Hand";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(320, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(88, 32);
      this.label2.TabIndex = 16;
      this.label2.Text = "Cargo Bay Content";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // CargoBay
      // 
      this.CargoBay.BarColour = System.Drawing.Color.LightGreen;
      this.CargoBay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.CargoBay.Location = new System.Drawing.Point(96, 216);
      this.CargoBay.Maximum = 50;
      this.CargoBay.Name = "CargoBay";
      this.CargoBay.ShowText = true;
      this.CargoBay.Size = new System.Drawing.Size(152, 16);
      this.CargoBay.TabIndex = 17;
      this.CargoBay.Units = "kT";
      this.CargoBay.Value = 0;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(264, 216);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 16);
      this.label3.TabIndex = 18;
      this.label3.Text = "Cargo Bay";
      // 
      // CargoDialog
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(434, 288);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.CargoBay);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.colonistsTransfer);
      this.Controls.Add(this.GermaniumTransfer);
      this.Controls.Add(this.boroniumTransfer);
      this.Controls.Add(this.IroniumTransfer);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CargoDialog";
      this.ShowInTaskbar = false;
      this.Text = "Cargo Transfer";
      this.ResumeLayout(false);

   }
#endregion


// ============================================================================
// Process cancel button.
// ============================================================================

   private void cancelButton_Click(object sender, System.EventArgs e) 
   {
      Close();
   }


// ============================================================================
// Initialise the various fields in the dialog.
// ============================================================================

   public void SetTarget(Fleet targetFleet)
   {
      fleet = targetFleet;

      IroniumTransfer.Maximum     = fleet.CargoCapacity;
      boroniumTransfer.Maximum    = fleet.CargoCapacity;
      GermaniumTransfer.Maximum   = fleet.CargoCapacity;
      colonistsTransfer.Maximum   = fleet.CargoCapacity;

      IroniumTransfer.Value       = (int) fleet.Cargo.Ironium;
      boroniumTransfer.Value      = (int) fleet.Cargo.Boranium;
      GermaniumTransfer.Value     = (int) fleet.Cargo.Germanium;
      colonistsTransfer.Value     = (int) fleet.Cargo.Colonists;

      Star star                   = fleet.InOrbit;
      IroniumTransfer.Available   = (int) star.ResourcesOnHand.Ironium;
      boroniumTransfer.Available  = (int) star.ResourcesOnHand.Boranium;
      GermaniumTransfer.Available = (int) star.ResourcesOnHand.Germanium;
      colonistsTransfer.Available = (int) star.Colonists / 1000;

      IroniumTransfer.Limit       = CargoBay;
      boroniumTransfer.Limit      = CargoBay;
      GermaniumTransfer.Limit     = CargoBay;
      colonistsTransfer.Limit     = CargoBay;

      CargoBay.Maximum            = fleet.CargoCapacity;
      CargoBay.Value              = fleet.Cargo.Mass;
   }


// ============================================================================
// Process the OK button being pressed.
// ============================================================================

   private void okButton_Click(object sender, System.EventArgs e) 
   {
      fleet.Cargo.Ironium   = IroniumTransfer.Value;
      fleet.Cargo.Boranium  = boroniumTransfer.Value;
      fleet.Cargo.Germanium = GermaniumTransfer.Value;
      fleet.Cargo.Colonists = colonistsTransfer.Value;

      Star star = fleet.InOrbit;
      star.ResourcesOnHand.Ironium   -= IroniumTransfer.Taken;
      star.ResourcesOnHand.Boranium  -= boroniumTransfer.Taken;
      star.ResourcesOnHand.Germanium -= GermaniumTransfer.Taken;
      star.Colonists                 -= colonistsTransfer.Taken * 1000;

      Close();
   }

}
