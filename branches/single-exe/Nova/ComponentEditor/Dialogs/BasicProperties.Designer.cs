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

namespace Nova.ComponentEditor.Dialogs
{
   partial class BasicProperties
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
		  this.panel3 = new System.Windows.Forms.Panel();
		  this.label2 = new System.Windows.Forms.Label();
		  this.EnergyAmount = new System.Windows.Forms.NumericUpDown();
		  this.panel2 = new System.Windows.Forms.Panel();
		  this.label1 = new System.Windows.Forms.Label();
		  this.label4 = new System.Windows.Forms.Label();
		  this.label10 = new System.Windows.Forms.Label();
		  this.label5 = new System.Windows.Forms.Label();
		  this.label7 = new System.Windows.Forms.Label();
		  this.IroniumAmount = new System.Windows.Forms.NumericUpDown();
		  this.label6 = new System.Windows.Forms.Label();
		  this.GermaniumAmount = new System.Windows.Forms.NumericUpDown();
		  this.BoraniumAmount = new System.Windows.Forms.NumericUpDown();
		  this.panel1 = new System.Windows.Forms.Panel();
		  this.label3 = new System.Windows.Forms.Label();
		  this.label9 = new System.Windows.Forms.Label();
		  this.ComponentMass = new System.Windows.Forms.NumericUpDown();
		  this.groupBox1.SuspendLayout();
		  this.panel3.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.EnergyAmount)).BeginInit();
		  this.panel2.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.IroniumAmount)).BeginInit();
		  ((System.ComponentModel.ISupportInitialize)(this.GermaniumAmount)).BeginInit();
		  ((System.ComponentModel.ISupportInitialize)(this.BoraniumAmount)).BeginInit();
		  this.panel1.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.ComponentMass)).BeginInit();
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
		  this.panel3.Controls.Add(this.EnergyAmount);
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
		  this.EnergyAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.EnergyAmount.Location = new System.Drawing.Point(100, 7);
		  this.EnergyAmount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
		  this.EnergyAmount.Name = "EnergyAmount";
		  this.EnergyAmount.Size = new System.Drawing.Size(45, 20);
		  this.EnergyAmount.TabIndex = 5;
		  this.EnergyAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
		  this.panel2.Controls.Add(this.IroniumAmount);
		  this.panel2.Controls.Add(this.label6);
		  this.panel2.Controls.Add(this.GermaniumAmount);
		  this.panel2.Controls.Add(this.BoraniumAmount);
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
		  this.IroniumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.IroniumAmount.ForeColor = System.Drawing.Color.Blue;
		  this.IroniumAmount.Location = new System.Drawing.Point(100, 7);
		  this.IroniumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
		  this.IroniumAmount.Name = "IroniumAmount";
		  this.IroniumAmount.Size = new System.Drawing.Size(45, 20);
		  this.IroniumAmount.TabIndex = 2;
		  this.IroniumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
		  this.GermaniumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.GermaniumAmount.ForeColor = System.Drawing.Color.Goldenrod;
		  this.GermaniumAmount.Location = new System.Drawing.Point(100, 52);
		  this.GermaniumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
		  this.GermaniumAmount.Name = "GermaniumAmount";
		  this.GermaniumAmount.Size = new System.Drawing.Size(45, 20);
		  this.GermaniumAmount.TabIndex = 4;
		  this.GermaniumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		  // 
		  // BoraniumAmount
		  // 
		  this.BoraniumAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.BoraniumAmount.ForeColor = System.Drawing.Color.Green;
		  this.BoraniumAmount.Location = new System.Drawing.Point(100, 29);
		  this.BoraniumAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
		  this.BoraniumAmount.Name = "BoraniumAmount";
		  this.BoraniumAmount.Size = new System.Drawing.Size(45, 20);
		  this.BoraniumAmount.TabIndex = 3;
		  this.BoraniumAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		  // 
		  // panel1
		  // 
		  this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.panel1.Controls.Add(this.label3);
		  this.panel1.Controls.Add(this.label9);
		  this.panel1.Controls.Add(this.ComponentMass);
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
		  this.ComponentMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.ComponentMass.Location = new System.Drawing.Point(100, 5);
		  this.ComponentMass.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
		  this.ComponentMass.Name = "ComponentMass";
		  this.ComponentMass.Size = new System.Drawing.Size(45, 20);
		  this.ComponentMass.TabIndex = 1;
		  this.ComponentMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
		  ((System.ComponentModel.ISupportInitialize)(this.EnergyAmount)).EndInit();
		  this.panel2.ResumeLayout(false);
		  this.panel2.PerformLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.IroniumAmount)).EndInit();
		  ((System.ComponentModel.ISupportInitialize)(this.GermaniumAmount)).EndInit();
		  ((System.ComponentModel.ISupportInitialize)(this.BoraniumAmount)).EndInit();
		  this.panel1.ResumeLayout(false);
		  this.panel1.PerformLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.ComponentMass)).EndInit();
		  this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.NumericUpDown BoraniumAmount;
      private System.Windows.Forms.NumericUpDown EnergyAmount;
      private System.Windows.Forms.NumericUpDown ComponentMass;
      private System.Windows.Forms.NumericUpDown GermaniumAmount;
      private System.Windows.Forms.NumericUpDown IroniumAmount;
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
