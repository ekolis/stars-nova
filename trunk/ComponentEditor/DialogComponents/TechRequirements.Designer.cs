// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the GUI for selecting tech levels.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace ComponentEditor
{
   partial class TechRequirements
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
          this.WeaponsTechLevel = new System.Windows.Forms.NumericUpDown();
          this.PropulsionTechLevel = new System.Windows.Forms.NumericUpDown();
          this.ConstructionTechLevel = new System.Windows.Forms.NumericUpDown();
          this.ElectronicsTechLevel = new System.Windows.Forms.NumericUpDown();
          this.BioTechLevel = new System.Windows.Forms.NumericUpDown();
          this.EnergyTechLevel = new System.Windows.Forms.NumericUpDown();
          this.label8 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.label6 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.WeaponsTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.PropulsionTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ConstructionTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ElectronicsTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.BioTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.EnergyTechLevel)).BeginInit();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.groupBox1.Controls.Add(this.WeaponsTechLevel);
          this.groupBox1.Controls.Add(this.PropulsionTechLevel);
          this.groupBox1.Controls.Add(this.ConstructionTechLevel);
          this.groupBox1.Controls.Add(this.ElectronicsTechLevel);
          this.groupBox1.Controls.Add(this.BioTechLevel);
          this.groupBox1.Controls.Add(this.EnergyTechLevel);
          this.groupBox1.Controls.Add(this.label8);
          this.groupBox1.Controls.Add(this.label7);
          this.groupBox1.Controls.Add(this.label6);
          this.groupBox1.Controls.Add(this.label5);
          this.groupBox1.Controls.Add(this.label4);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Location = new System.Drawing.Point(4, 4);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(133, 185);
          this.groupBox1.TabIndex = 0;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Required Tech Levels";
          // 
          // WeaponsTechLevel
          // 
          this.WeaponsTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.WeaponsTechLevel.Location = new System.Drawing.Point(77, 45);
          this.WeaponsTechLevel.Name = "WeaponsTechLevel";
          this.WeaponsTechLevel.Size = new System.Drawing.Size(45, 20);
          this.WeaponsTechLevel.TabIndex = 2;
          this.WeaponsTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // PropulsionTechLevel
          // 
          this.PropulsionTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.PropulsionTechLevel.Location = new System.Drawing.Point(77, 71);
          this.PropulsionTechLevel.Name = "PropulsionTechLevel";
          this.PropulsionTechLevel.Size = new System.Drawing.Size(45, 20);
          this.PropulsionTechLevel.TabIndex = 3;
          this.PropulsionTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // ConstructionTechLevel
          // 
          this.ConstructionTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.ConstructionTechLevel.Location = new System.Drawing.Point(77, 97);
          this.ConstructionTechLevel.Name = "ConstructionTechLevel";
          this.ConstructionTechLevel.Size = new System.Drawing.Size(45, 20);
          this.ConstructionTechLevel.TabIndex = 4;
          this.ConstructionTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // ElectronicsTechLevel
          // 
          this.ElectronicsTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.ElectronicsTechLevel.Location = new System.Drawing.Point(77, 124);
          this.ElectronicsTechLevel.Name = "ElectronicsTechLevel";
          this.ElectronicsTechLevel.Size = new System.Drawing.Size(45, 20);
          this.ElectronicsTechLevel.TabIndex = 5;
          this.ElectronicsTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // BioTechLevel
          // 
          this.BioTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.BioTechLevel.Location = new System.Drawing.Point(77, 149);
          this.BioTechLevel.Name = "BioTechLevel";
          this.BioTechLevel.Size = new System.Drawing.Size(45, 20);
          this.BioTechLevel.TabIndex = 6;
          this.BioTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // EnergyTechLevel
          // 
          this.EnergyTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.EnergyTechLevel.Location = new System.Drawing.Point(77, 19);
          this.EnergyTechLevel.Name = "EnergyTechLevel";
          this.EnergyTechLevel.Size = new System.Drawing.Size(45, 20);
          this.EnergyTechLevel.TabIndex = 1;
          this.EnergyTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // label8
          // 
          this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label8.AutoSize = true;
          this.label8.Location = new System.Drawing.Point(6, 47);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(53, 13);
          this.label8.TabIndex = 7;
          this.label8.Text = "Weapons";
          // 
          // label7
          // 
          this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(6, 73);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(56, 13);
          this.label7.TabIndex = 6;
          this.label7.Text = "Propulsion";
          // 
          // label6
          // 
          this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(6, 99);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(66, 13);
          this.label6.TabIndex = 5;
          this.label6.Text = "Construction";
          // 
          // label5
          // 
          this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label5.AutoSize = true;
          this.label5.Location = new System.Drawing.Point(6, 126);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(59, 13);
          this.label5.TabIndex = 4;
          this.label5.Text = "Electronics";
          // 
          // label4
          // 
          this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label4.AutoSize = true;
          this.label4.Location = new System.Drawing.Point(6, 151);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(74, 13);
          this.label4.TabIndex = 3;
          this.label4.Text = "Biotechnology";
          // 
          // label1
          // 
          this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(6, 21);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(40, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Energy";
          // 
          // TechRequirements
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.groupBox1);
          this.Name = "TechRequirements";
          this.Size = new System.Drawing.Size(140, 193);
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.WeaponsTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.PropulsionTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ConstructionTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ElectronicsTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.BioTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.EnergyTechLevel)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.NumericUpDown EnergyTechLevel;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown WeaponsTechLevel;
      private System.Windows.Forms.NumericUpDown PropulsionTechLevel;
      private System.Windows.Forms.NumericUpDown ConstructionTechLevel;
      private System.Windows.Forms.NumericUpDown ElectronicsTechLevel;
      private System.Windows.Forms.NumericUpDown BioTechLevel;
   }
}
