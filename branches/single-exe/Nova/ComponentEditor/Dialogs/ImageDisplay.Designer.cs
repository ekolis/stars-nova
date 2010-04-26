// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the GUI for image selection.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.ComponentEditor.Dialogs
{
   partial class ImageDisplay
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
		  this.groupBox1 = new System.Windows.Forms.GroupBox();
		  this.SelectImage = new System.Windows.Forms.Button();
		  this.label1 = new System.Windows.Forms.Label();
		  this.ComponentImage = new System.Windows.Forms.PictureBox();
		  this.groupBox1.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.ComponentImage)).BeginInit();
		  this.SuspendLayout();
		  // 
		  // groupBox1
		  // 
		  this.groupBox1.Controls.Add(this.SelectImage);
		  this.groupBox1.Controls.Add(this.label1);
		  this.groupBox1.Controls.Add(this.ComponentImage);
		  this.groupBox1.Location = new System.Drawing.Point(3, 3);
		  this.groupBox1.Name = "groupBox1";
		  this.groupBox1.Size = new System.Drawing.Size(112, 160);
		  this.groupBox1.TabIndex = 0;
		  this.groupBox1.TabStop = false;
		  this.groupBox1.Text = "Image";
		  // 
		  // SelectImage
		  // 
		  this.SelectImage.Location = new System.Drawing.Point(9, 122);
		  this.SelectImage.Name = "SelectImage";
		  this.SelectImage.Size = new System.Drawing.Size(90, 23);
		  this.SelectImage.TabIndex = 2;
		  this.SelectImage.Text = "Select Image";
		  this.SelectImage.UseVisualStyleBackColor = true;
		  this.SelectImage.Click += new System.EventHandler(this.OnImageButtonClick);
		  // 
		  // label1
		  // 
		  this.label1.AutoSize = true;
		  this.label1.Location = new System.Drawing.Point(6, 95);
		  this.label1.Name = "label1";
		  this.label1.Size = new System.Drawing.Size(93, 13);
		  this.label1.TabIndex = 1;
		  this.label1.Text = "Component Image";
		  // 
		  // ComponentImage
		  // 
		  this.ComponentImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		  this.ComponentImage.Location = new System.Drawing.Point(24, 28);
		  this.ComponentImage.Name = "ComponentImage";
		  this.ComponentImage.Size = new System.Drawing.Size(64, 64);
		  this.ComponentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		  this.ComponentImage.TabIndex = 0;
		  this.ComponentImage.TabStop = false;
		  // 
		  // ImageDisplay
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.AutoSize = true;
		  this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		  this.Controls.Add(this.groupBox1);
		  this.MaximumSize = new System.Drawing.Size(118, 166);
		  this.MinimumSize = new System.Drawing.Size(118, 166);
		  this.Name = "ImageDisplay";
		  this.Size = new System.Drawing.Size(118, 166);
		  this.groupBox1.ResumeLayout(false);
		  this.groupBox1.PerformLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.ComponentImage)).EndInit();
		  this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.PictureBox ComponentImage;
      private System.Windows.Forms.Button SelectImage;
   }
}
