// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// 
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.WinForms.Gui
{
    public partial class ResearchDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BiotechLevel = new System.Windows.Forms.Label();
            this.ElectronicsLevel = new System.Windows.Forms.Label();
            this.ConstructionLevel = new System.Windows.Forms.Label();
            this.PropulsionLevel = new System.Windows.Forms.Label();
            this.WeaponsLevel = new System.Windows.Forms.Label();
            this.EnergyLevel = new System.Windows.Forms.Label();
            this.BiotechButton = new System.Windows.Forms.RadioButton();
            this.ElectronicsButton = new System.Windows.Forms.RadioButton();
            this.ConstructionButton = new System.Windows.Forms.RadioButton();
            this.PropulsionButton = new System.Windows.Forms.RadioButton();
            this.WeaponsButton = new System.Windows.Forms.RadioButton();
            this.EnergyButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ResearchBenefits = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NumericResources = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ResourceBudget = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.AvailableResources = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CompletionTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CompletionResources = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BiotechLevel);
            this.groupBox1.Controls.Add(this.ElectronicsLevel);
            this.groupBox1.Controls.Add(this.ConstructionLevel);
            this.groupBox1.Controls.Add(this.PropulsionLevel);
            this.groupBox1.Controls.Add(this.WeaponsLevel);
            this.groupBox1.Controls.Add(this.EnergyLevel);
            this.groupBox1.Controls.Add(this.BiotechButton);
            this.groupBox1.Controls.Add(this.ElectronicsButton);
            this.groupBox1.Controls.Add(this.ConstructionButton);
            this.groupBox1.Controls.Add(this.PropulsionButton);
            this.groupBox1.Controls.Add(this.WeaponsButton);
            this.groupBox1.Controls.Add(this.EnergyButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Technology Status";
            // 
            // BiotechLevel
            // 
            this.BiotechLevel.AutoSize = true;
            this.BiotechLevel.Location = new System.Drawing.Point(173, 211);
            this.BiotechLevel.Name = "BiotechLevel";
            this.BiotechLevel.Size = new System.Drawing.Size(13, 13);
            this.BiotechLevel.TabIndex = 13;
            this.BiotechLevel.Text = "0";
            // 
            // ElectronicsLevel
            // 
            this.ElectronicsLevel.AutoSize = true;
            this.ElectronicsLevel.Location = new System.Drawing.Point(173, 180);
            this.ElectronicsLevel.Name = "ElectronicsLevel";
            this.ElectronicsLevel.Size = new System.Drawing.Size(13, 13);
            this.ElectronicsLevel.TabIndex = 12;
            this.ElectronicsLevel.Text = "0";
            // 
            // ConstructionLevel
            // 
            this.ConstructionLevel.AutoSize = true;
            this.ConstructionLevel.Location = new System.Drawing.Point(173, 149);
            this.ConstructionLevel.Name = "ConstructionLevel";
            this.ConstructionLevel.Size = new System.Drawing.Size(13, 13);
            this.ConstructionLevel.TabIndex = 11;
            this.ConstructionLevel.Text = "0";
            // 
            // PropulsionLevel
            // 
            this.PropulsionLevel.AutoSize = true;
            this.PropulsionLevel.Location = new System.Drawing.Point(173, 118);
            this.PropulsionLevel.Name = "PropulsionLevel";
            this.PropulsionLevel.Size = new System.Drawing.Size(13, 13);
            this.PropulsionLevel.TabIndex = 10;
            this.PropulsionLevel.Text = "0";
            // 
            // WeaponsLevel
            // 
            this.WeaponsLevel.AutoSize = true;
            this.WeaponsLevel.Location = new System.Drawing.Point(173, 89);
            this.WeaponsLevel.Name = "WeaponsLevel";
            this.WeaponsLevel.Size = new System.Drawing.Size(13, 13);
            this.WeaponsLevel.TabIndex = 9;
            this.WeaponsLevel.Text = "0";
            // 
            // EnergyLevel
            // 
            this.EnergyLevel.AutoSize = true;
            this.EnergyLevel.Location = new System.Drawing.Point(173, 56);
            this.EnergyLevel.Name = "EnergyLevel";
            this.EnergyLevel.Size = new System.Drawing.Size(13, 13);
            this.EnergyLevel.TabIndex = 8;
            this.EnergyLevel.Text = "0";
            // 
            // BiotechButton
            // 
            this.BiotechButton.AutoSize = true;
            this.BiotechButton.Location = new System.Drawing.Point(6, 207);
            this.BiotechButton.Name = "BiotechButton";
            this.BiotechButton.Size = new System.Drawing.Size(92, 17);
            this.BiotechButton.TabIndex = 7;
            this.BiotechButton.Tag = "6";
            this.BiotechButton.Text = "Biotechnology";
            this.BiotechButton.UseVisualStyleBackColor = true;
            this.BiotechButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // ElectronicsButton
            // 
            this.ElectronicsButton.AutoSize = true;
            this.ElectronicsButton.Location = new System.Drawing.Point(6, 176);
            this.ElectronicsButton.Name = "ElectronicsButton";
            this.ElectronicsButton.Size = new System.Drawing.Size(77, 17);
            this.ElectronicsButton.TabIndex = 6;
            this.ElectronicsButton.Tag = "5";
            this.ElectronicsButton.Text = "Electronics";
            this.ElectronicsButton.UseVisualStyleBackColor = true;
            this.ElectronicsButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // ConstructionButton
            // 
            this.ConstructionButton.AutoSize = true;
            this.ConstructionButton.Location = new System.Drawing.Point(6, 145);
            this.ConstructionButton.Name = "ConstructionButton";
            this.ConstructionButton.Size = new System.Drawing.Size(84, 17);
            this.ConstructionButton.TabIndex = 5;
            this.ConstructionButton.Tag = "4";
            this.ConstructionButton.Text = "Construction";
            this.ConstructionButton.UseVisualStyleBackColor = true;
            this.ConstructionButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // PropulsionButton
            // 
            this.PropulsionButton.AutoSize = true;
            this.PropulsionButton.Location = new System.Drawing.Point(6, 114);
            this.PropulsionButton.Name = "PropulsionButton";
            this.PropulsionButton.Size = new System.Drawing.Size(74, 17);
            this.PropulsionButton.TabIndex = 4;
            this.PropulsionButton.Tag = "3";
            this.PropulsionButton.Text = "Propulsion";
            this.PropulsionButton.UseVisualStyleBackColor = true;
            this.PropulsionButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // WeaponsButton
            // 
            this.WeaponsButton.AutoSize = true;
            this.WeaponsButton.Location = new System.Drawing.Point(6, 85);
            this.WeaponsButton.Name = "WeaponsButton";
            this.WeaponsButton.Size = new System.Drawing.Size(71, 17);
            this.WeaponsButton.TabIndex = 3;
            this.WeaponsButton.Tag = "2";
            this.WeaponsButton.Text = "Weapons";
            this.WeaponsButton.UseVisualStyleBackColor = true;
            this.WeaponsButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // EnergyButton
            // 
            this.EnergyButton.AutoSize = true;
            this.EnergyButton.Location = new System.Drawing.Point(6, 52);
            this.EnergyButton.Name = "EnergyButton";
            this.EnergyButton.Size = new System.Drawing.Size(58, 17);
            this.EnergyButton.TabIndex = 2;
            this.EnergyButton.Tag = "1";
            this.EnergyButton.Text = "Energy";
            this.EnergyButton.UseVisualStyleBackColor = true;
            this.EnergyButton.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(142, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Level";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field of Study";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(459, 274);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ResearchBenefits);
            this.groupBox2.Location = new System.Drawing.Point(250, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 108);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Expected Research Benefits";
            // 
            // ResearchBenefits
            // 
            this.ResearchBenefits.FormattingEnabled = true;
            this.ResearchBenefits.Location = new System.Drawing.Point(10, 20);
            this.ResearchBenefits.Name = "ResearchBenefits";
            this.ResearchBenefits.Size = new System.Drawing.Size(288, 82);
            this.ResearchBenefits.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NumericResources);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.ResourceBudget);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.AvailableResources);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.CompletionTime);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CompletionResources);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(250, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 108);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resource Allocation";
            // 
            // NumericResources
            // 
            this.NumericResources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericResources.Location = new System.Drawing.Point(236, 86);
            this.NumericResources.Name = "NumericResources";
            this.NumericResources.Size = new System.Drawing.Size(53, 13);
            this.NumericResources.TabIndex = 11;
            this.NumericResources.Text = "0";
            this.NumericResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Resources allocated per year:";
            // 
            // ResourceBudget
            // 
            this.ResourceBudget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceBudget.BackColor = System.Drawing.SystemColors.Info;
            this.ResourceBudget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResourceBudget.Location = new System.Drawing.Point(248, 70);
            this.ResourceBudget.Name = "ResourceBudget";
            this.ResourceBudget.Size = new System.Drawing.Size(53, 16);
            this.ResourceBudget.TabIndex = 9;
            this.ResourceBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ResourceBudget.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ResourceBudget.ValueChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Resources budgeted for research (%):";
            // 
            // AvailableResources
            // 
            this.AvailableResources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableResources.Location = new System.Drawing.Point(236, 52);
            this.AvailableResources.Name = "AvailableResources";
            this.AvailableResources.Size = new System.Drawing.Size(53, 13);
            this.AvailableResources.TabIndex = 7;
            this.AvailableResources.Text = "0";
            this.AvailableResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Resources available from all owned planets:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CompletionTime
            // 
            this.CompletionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CompletionTime.Location = new System.Drawing.Point(236, 35);
            this.CompletionTime.Name = "CompletionTime";
            this.CompletionTime.Size = new System.Drawing.Size(53, 13);
            this.CompletionTime.TabIndex = 4;
            this.CompletionTime.Text = "0";
            this.CompletionTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Estimated time to completion (years):";
            // 
            // CompletionResources
            // 
            this.CompletionResources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CompletionResources.Location = new System.Drawing.Point(236, 18);
            this.CompletionResources.Name = "CompletionResources";
            this.CompletionResources.Size = new System.Drawing.Size(53, 13);
            this.CompletionResources.TabIndex = 2;
            this.CompletionResources.Text = "0";
            this.CompletionResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Resources needed to research next level:";
            // 
            // ResearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 309);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResearchDialog";
            this.Text = "Research";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       private System.Windows.Forms.GroupBox groupBox1;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Button OKButton;
       private System.Windows.Forms.RadioButton BiotechButton;
       private System.Windows.Forms.RadioButton ElectronicsButton;
       private System.Windows.Forms.RadioButton ConstructionButton;
       private System.Windows.Forms.RadioButton PropulsionButton;
       private System.Windows.Forms.RadioButton WeaponsButton;
       private System.Windows.Forms.RadioButton EnergyButton;
       private System.Windows.Forms.GroupBox groupBox2;
       private System.Windows.Forms.Label BiotechLevel;
       private System.Windows.Forms.Label ElectronicsLevel;
       private System.Windows.Forms.Label ConstructionLevel;
       private System.Windows.Forms.Label PropulsionLevel;
       private System.Windows.Forms.Label WeaponsLevel;
       private System.Windows.Forms.Label EnergyLevel;
       private System.Windows.Forms.GroupBox groupBox3;
       private System.Windows.Forms.Label CompletionTime;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label CompletionResources;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.NumericUpDown ResourceBudget;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label AvailableResources;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.Label NumericResources;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.ListBox ResearchBenefits;



     }
}