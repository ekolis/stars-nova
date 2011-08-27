// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the GUI for selecting tech levels.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.WinForms.ComponentEditor
{
   public partial class TechRequirements
   {
      /// <Summary> 
      /// Required designer variable.
      /// </Summary>
      private System.ComponentModel.IContainer components = null;

      /// <Summary> 
      /// Clean up any resources being used.
      /// </Summary>
      /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <Summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.weaponsTechLevel = new System.Windows.Forms.NumericUpDown();
          this.propulsionTechLevel = new System.Windows.Forms.NumericUpDown();
          this.constructionTechLevel = new System.Windows.Forms.NumericUpDown();
          this.electronicsTechLevel = new System.Windows.Forms.NumericUpDown();
          this.bioTechLevel = new System.Windows.Forms.NumericUpDown();
          this.energyTechLevel = new System.Windows.Forms.NumericUpDown();
          this.label8 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.label6 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.weaponsTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.propulsionTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.constructionTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.electronicsTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.bioTechLevel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.energyTechLevel)).BeginInit();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.groupBox1.AutoSize = true;
          this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
          this.groupBox1.Controls.Add(this.weaponsTechLevel);
          this.groupBox1.Controls.Add(this.propulsionTechLevel);
          this.groupBox1.Controls.Add(this.constructionTechLevel);
          this.groupBox1.Controls.Add(this.electronicsTechLevel);
          this.groupBox1.Controls.Add(this.bioTechLevel);
          this.groupBox1.Controls.Add(this.energyTechLevel);
          this.groupBox1.Controls.Add(this.label8);
          this.groupBox1.Controls.Add(this.label7);
          this.groupBox1.Controls.Add(this.label6);
          this.groupBox1.Controls.Add(this.label5);
          this.groupBox1.Controls.Add(this.label4);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Location = new System.Drawing.Point(4, 4);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(133, 182);
          this.groupBox1.TabIndex = 0;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Required Tech Levels";
          // 
          // WeaponsTechLevel
          // 
          this.weaponsTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.weaponsTechLevel.Location = new System.Drawing.Point(77, 45);
          this.weaponsTechLevel.Name = "weaponsTechLevel";
          this.weaponsTechLevel.Size = new System.Drawing.Size(45, 20);
          this.weaponsTechLevel.TabIndex = 2;
          this.weaponsTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // PropulsionTechLevel
          // 
          this.propulsionTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.propulsionTechLevel.Location = new System.Drawing.Point(77, 71);
          this.propulsionTechLevel.Name = "propulsionTechLevel";
          this.propulsionTechLevel.Size = new System.Drawing.Size(45, 20);
          this.propulsionTechLevel.TabIndex = 3;
          this.propulsionTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // ConstructionTechLevel
          // 
          this.constructionTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.constructionTechLevel.Location = new System.Drawing.Point(77, 97);
          this.constructionTechLevel.Name = "constructionTechLevel";
          this.constructionTechLevel.Size = new System.Drawing.Size(45, 20);
          this.constructionTechLevel.TabIndex = 4;
          this.constructionTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // ElectronicsTechLevel
          // 
          this.electronicsTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.electronicsTechLevel.Location = new System.Drawing.Point(77, 124);
          this.electronicsTechLevel.Name = "electronicsTechLevel";
          this.electronicsTechLevel.Size = new System.Drawing.Size(45, 20);
          this.electronicsTechLevel.TabIndex = 5;
          this.electronicsTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // BioTechLevel
          // 
          this.bioTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.bioTechLevel.Location = new System.Drawing.Point(77, 149);
          this.bioTechLevel.Name = "bioTechLevel";
          this.bioTechLevel.Size = new System.Drawing.Size(45, 20);
          this.bioTechLevel.TabIndex = 6;
          this.bioTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // EnergyTechLevel
          // 
          this.energyTechLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.energyTechLevel.Location = new System.Drawing.Point(77, 19);
          this.energyTechLevel.Name = "energyTechLevel";
          this.energyTechLevel.Size = new System.Drawing.Size(45, 20);
          this.energyTechLevel.TabIndex = 1;
          this.energyTechLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
          ((System.ComponentModel.ISupportInitialize)(this.weaponsTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.propulsionTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.constructionTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.electronicsTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.bioTechLevel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.energyTechLevel)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.NumericUpDown energyTechLevel;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown weaponsTechLevel;
      private System.Windows.Forms.NumericUpDown propulsionTechLevel;
      private System.Windows.Forms.NumericUpDown constructionTechLevel;
      private System.Windows.Forms.NumericUpDown electronicsTechLevel;
      private System.Windows.Forms.NumericUpDown bioTechLevel;
   }
}
