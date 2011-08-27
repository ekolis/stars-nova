// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the GUI for the basic properties common to all components.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.WinForms.ComponentEditor
{
   public partial class BasicProperties
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
          this.panel3 = new System.Windows.Forms.Panel();
          this.label2 = new System.Windows.Forms.Label();
          this.energyAmount = new System.Windows.Forms.NumericUpDown();
          this.panel2 = new System.Windows.Forms.Panel();
          this.label1 = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.label10 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.ironiumAmount = new System.Windows.Forms.NumericUpDown();
          this.label6 = new System.Windows.Forms.Label();
          this.germaniumAmount = new System.Windows.Forms.NumericUpDown();
          this.boraniumAmount = new System.Windows.Forms.NumericUpDown();
          this.panel1 = new System.Windows.Forms.Panel();
          this.label3 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.componentMass = new System.Windows.Forms.NumericUpDown();
          this.groupBox1.SuspendLayout();
          this.panel3.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.energyAmount)).BeginInit();
          this.panel2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.ironiumAmount)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.germaniumAmount)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.boraniumAmount)).BeginInit();
          this.panel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.componentMass)).BeginInit();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.panel3);
          this.groupBox1.Controls.Add(this.panel2);
          this.groupBox1.Controls.Add(this.panel1);
          this.groupBox1.Location = new System.Drawing.Point(6, 8);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(162, 181);
          this.groupBox1.TabIndex = 0;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Basic Properties";
          // 
          // panel3
          // 
          this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.panel3.Controls.Add(this.label2);
          this.panel3.Controls.Add(this.energyAmount);
          this.panel3.Location = new System.Drawing.Point(9, 137);
          this.panel3.Name = "panel3";
          this.panel3.Size = new System.Drawing.Size(147, 34);
          this.panel3.TabIndex = 17;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(6, 9);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(58, 13);
          this.label2.TabIndex = 2;
          this.label2.Text = "Resources";
          // 
          // EnergyAmount
          // 
          this.energyAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.energyAmount.Location = new System.Drawing.Point(100, 7);
          this.energyAmount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
          this.energyAmount.Name = "energyAmount";
          this.energyAmount.Size = new System.Drawing.Size(45, 20);
          this.energyAmount.TabIndex = 5;
          this.energyAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // panel2
          // 
          this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.panel2.Controls.Add(this.label1);
          this.panel2.Controls.Add(this.label4);
          this.panel2.Controls.Add(this.label10);
          this.panel2.Controls.Add(this.label5);
          this.panel2.Controls.Add(this.label7);
          this.panel2.Controls.Add(this.ironiumAmount);
          this.panel2.Controls.Add(this.label6);
          this.panel2.Controls.Add(this.germaniumAmount);
          this.panel2.Controls.Add(this.boraniumAmount);
          this.panel2.Location = new System.Drawing.Point(9, 53);
          this.panel2.Name = "panel2";
          this.panel2.Size = new System.Drawing.Size(147, 79);
          this.panel2.TabIndex = 16;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.ForeColor = System.Drawing.Color.Blue;
          this.label1.Location = new System.Drawing.Point(6, 9);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(41, 13);
          this.label1.TabIndex = 1;
          this.label1.Text = "Ironium";
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
          this.label4.Location = new System.Drawing.Point(6, 54);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(60, 13);
          this.label4.TabIndex = 4;
          this.label4.Text = "Germanium";
          // 
          // label10
          // 
          this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label10.AutoSize = true;
          this.label10.Location = new System.Drawing.Point(68, 31);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(26, 13);
          this.label10.TabIndex = 14;
          this.label10.Text = "(kT)";
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.ForeColor = System.Drawing.Color.Green;
          this.label5.Location = new System.Drawing.Point(6, 31);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(51, 13);
          this.label5.TabIndex = 5;
          this.label5.Text = "Boranium";
          // 
          // label7
          // 
          this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(68, 54);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(26, 13);
          this.label7.TabIndex = 11;
          this.label7.Text = "(kT)";
          // 
          // IroniumAmount
          // 
          this.ironiumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.ironiumAmount.ForeColor = System.Drawing.Color.Blue;
          this.ironiumAmount.Location = new System.Drawing.Point(100, 7);
          this.ironiumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
          this.ironiumAmount.Name = "ironiumAmount";
          this.ironiumAmount.Size = new System.Drawing.Size(45, 20);
          this.ironiumAmount.TabIndex = 2;
          this.ironiumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // label6
          // 
          this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(68, 9);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(26, 13);
          this.label6.TabIndex = 10;
          this.label6.Text = "(kT)";
          // 
          // GermaniumAmount
          // 
          this.germaniumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.germaniumAmount.ForeColor = System.Drawing.Color.Goldenrod;
          this.germaniumAmount.Location = new System.Drawing.Point(100, 52);
          this.germaniumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
          this.germaniumAmount.Name = "germaniumAmount";
          this.germaniumAmount.Size = new System.Drawing.Size(45, 20);
          this.germaniumAmount.TabIndex = 4;
          this.germaniumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // BoraniumAmount
          // 
          this.boraniumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.boraniumAmount.ForeColor = System.Drawing.Color.Green;
          this.boraniumAmount.Location = new System.Drawing.Point(100, 29);
          this.boraniumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
          this.boraniumAmount.Name = "boraniumAmount";
          this.boraniumAmount.Size = new System.Drawing.Size(45, 20);
          this.boraniumAmount.TabIndex = 3;
          this.boraniumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // panel1
          // 
          this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.panel1.Controls.Add(this.label3);
          this.panel1.Controls.Add(this.label9);
          this.panel1.Controls.Add(this.componentMass);
          this.panel1.Location = new System.Drawing.Point(9, 19);
          this.panel1.Name = "panel1";
          this.panel1.Size = new System.Drawing.Size(147, 29);
          this.panel1.TabIndex = 15;
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(6, 7);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(32, 13);
          this.label3.TabIndex = 3;
          this.label3.Text = "Mass";
          // 
          // label9
          // 
          this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label9.AutoSize = true;
          this.label9.Location = new System.Drawing.Point(68, 7);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(26, 13);
          this.label9.TabIndex = 13;
          this.label9.Text = "(kT)";
          // 
          // ComponentMass
          // 
          this.componentMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.componentMass.Location = new System.Drawing.Point(100, 5);
          this.componentMass.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
          this.componentMass.Name = "componentMass";
          this.componentMass.Size = new System.Drawing.Size(45, 20);
          this.componentMass.TabIndex = 1;
          this.componentMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          // 
          // BasicProperties
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSize = true;
          this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
          this.Controls.Add(this.groupBox1);
          this.Name = "BasicProperties";
          this.Size = new System.Drawing.Size(171, 192);
          this.groupBox1.ResumeLayout(false);
          this.panel3.ResumeLayout(false);
          this.panel3.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.energyAmount)).EndInit();
          this.panel2.ResumeLayout(false);
          this.panel2.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.ironiumAmount)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.germaniumAmount)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.boraniumAmount)).EndInit();
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.componentMass)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.NumericUpDown boraniumAmount;
      private System.Windows.Forms.NumericUpDown energyAmount;
      private System.Windows.Forms.NumericUpDown componentMass;
      private System.Windows.Forms.NumericUpDown germaniumAmount;
      private System.Windows.Forms.NumericUpDown ironiumAmount;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Panel panel2;
   }
}
