// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the main GUI for the component designer.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using Nova.Common.Components;
using Nova.WinForms.ComponentEditor;

namespace ComponentEditor
{
   partial class ShieldDialog
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShieldDialog));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.ArmourStrength = new System.Windows.Forms.NumericUpDown();
          this.label2 = new System.Windows.Forms.Label();
          this.ShieldStrength = new System.Windows.Forms.NumericUpDown();
          this.label1 = new System.Windows.Forms.Label();
          this.CommonProperties = new CommonProperties();
          this.DeleteButton = new System.Windows.Forms.Button();
          this.SaveButton = new System.Windows.Forms.Button();
          this.DoneButton = new System.Windows.Forms.Button();
          this.groupBox1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.ArmourStrength)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShieldStrength)).BeginInit();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.ArmourStrength);
          this.groupBox1.Controls.Add(this.label2);
          this.groupBox1.Controls.Add(this.ShieldStrength);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Location = new System.Drawing.Point(398, 191);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(180, 150);
          this.groupBox1.TabIndex = 4;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Shield Properties";
          // 
          // ArmourStrength
          // 
          this.ArmourStrength.Location = new System.Drawing.Point(119, 48);
          this.ArmourStrength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
          this.ArmourStrength.Name = "ArmourStrength";
          this.ArmourStrength.Size = new System.Drawing.Size(55, 20);
          this.ArmourStrength.TabIndex = 3;
          this.ArmourStrength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(7, 50);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(40, 13);
          this.label2.TabIndex = 2;
          this.label2.Text = "Armour";
          // 
          // ShieldStrength
          // 
          this.ShieldStrength.Location = new System.Drawing.Point(119, 18);
          this.ShieldStrength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
          this.ShieldStrength.Name = "ShieldStrength";
          this.ShieldStrength.Size = new System.Drawing.Size(55, 20);
          this.ShieldStrength.TabIndex = 1;
          this.ShieldStrength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(7, 20);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(47, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Strength";
          // 
          // CommonProperties
          // 
          this.CommonProperties.Location = new System.Drawing.Point(3, 3);
          this.CommonProperties.Name = "CommonProperties";
          this.CommonProperties.Size = new System.Drawing.Size(581, 458);
          this.CommonProperties.TabIndex = 1;
          this.CommonProperties.Value = ((Component)(resources.GetObject("CommonProperties.Value")));
          // 
          // DeleteButton
          // 
          this.DeleteButton.Location = new System.Drawing.Point(398, 392);
          this.DeleteButton.Name = "DeleteButton";
          this.DeleteButton.Size = new System.Drawing.Size(75, 23);
          this.DeleteButton.TabIndex = 5;
          this.DeleteButton.Text = "Delete";
          this.DeleteButton.UseVisualStyleBackColor = true;
          this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
          // 
          // SaveButton
          // 
          this.SaveButton.Location = new System.Drawing.Point(398, 424);
          this.SaveButton.Name = "SaveButton";
          this.SaveButton.Size = new System.Drawing.Size(75, 23);
          this.SaveButton.TabIndex = 6;
          this.SaveButton.Text = "Save";
          this.SaveButton.UseVisualStyleBackColor = true;
          this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
          // 
          // DoneButton
          // 
          this.DoneButton.Location = new System.Drawing.Point(497, 424);
          this.DoneButton.Name = "DoneButton";
          this.DoneButton.Size = new System.Drawing.Size(75, 23);
          this.DoneButton.TabIndex = 7;
          this.DoneButton.Text = "Done";
          this.DoneButton.UseVisualStyleBackColor = true;
          this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
          // 
          // ShieldDialog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(586, 459);
          this.Controls.Add(this.DoneButton);
          this.Controls.Add(this.SaveButton);
          this.Controls.Add(this.DeleteButton);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.CommonProperties);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "ShieldDialog";
          this.Text = "ShieldDialog";
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.ArmourStrength)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShieldStrength)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private CommonProperties CommonProperties;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.NumericUpDown ShieldStrength;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button DeleteButton;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button DoneButton;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.NumericUpDown ArmourStrength;

   }
}