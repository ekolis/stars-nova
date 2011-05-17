// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This creates a dialog to edit hulls.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System.Collections.Generic;
using Nova.Common.Components;

namespace Nova.WinForms.ComponentEditor
{
   public partial class HullDialog
   {
      /// <Summary>
      /// Required designer variable.
      /// </Summary>
      private System.ComponentModel.IContainer components = null;

      /// <Summary>
      /// Clean up any resources being used.
      /// </Summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HullDialog));
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.HullGrid = new ControlLibrary.HullGrid();
          this.buttonClose = new System.Windows.Forms.Button();
          this.groupBox2.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.HullGrid);
          this.groupBox2.Location = new System.Drawing.Point(12, 12);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(352, 360);
          this.groupBox2.TabIndex = 4;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Hull Layout Grid";
          // 
          // HullGrid
          // 
          this.HullGrid.ActiveModules = ((System.Collections.Generic.List<Nova.Common.Components.HullModule>)(resources.GetObject("HullGrid.ActiveModules")));
          this.HullGrid.HideEmptyModules = false;
          this.HullGrid.Location = new System.Drawing.Point(6, 17);
          this.HullGrid.Name = "HullGrid";
          this.HullGrid.Size = new System.Drawing.Size(340, 333);
          this.HullGrid.TabIndex = 0;
          // 
          // buttonClose
          // 
          this.buttonClose.Location = new System.Drawing.Point(153, 389);
          this.buttonClose.Name = "buttonClose";
          this.buttonClose.Size = new System.Drawing.Size(75, 23);
          this.buttonClose.TabIndex = 5;
          this.buttonClose.Text = "Close";
          this.buttonClose.UseVisualStyleBackColor = true;
          this.buttonClose.Click += new System.EventHandler(this.Close_Click);
          // 
          // HullDialog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(385, 435);
          this.Controls.Add(this.buttonClose);
          this.Controls.Add(this.groupBox2);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "HullDialog";
          this.Text = "Nova Hull Editor";
          this.groupBox2.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox2;
      public ControlLibrary.HullGrid HullGrid;
      private System.Windows.Forms.Button buttonClose;
   }
}
