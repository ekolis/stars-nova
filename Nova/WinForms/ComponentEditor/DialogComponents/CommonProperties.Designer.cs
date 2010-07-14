// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the GUI for the common properties in the component
// designerdialogs.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.WinForms.ComponentEditor
{
   public partial class CommonProperties
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonProperties));
          this.groupBox3 = new System.Windows.Forms.GroupBox();
          this.Description = new System.Windows.Forms.TextBox();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.ComponentName = new System.Windows.Forms.TextBox();
          this.scannerListGroupBox = new System.Windows.Forms.GroupBox();
          this.ComponentList = new System.Windows.Forms.ListBox();
          this.ComponentImage = new ComponentEditor.ImageDisplay();
          this.TechRequirements = new ComponentEditor.TechRequirements();
          this.BasicProperties = new ComponentEditor.BasicProperties();
          this.groupBox3.SuspendLayout();
          this.groupBox2.SuspendLayout();
          this.scannerListGroupBox.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox3
          // 
          this.groupBox3.Controls.Add(this.Description);
          this.groupBox3.Location = new System.Drawing.Point(6, 343);
          this.groupBox3.Name = "groupBox3";
          this.groupBox3.Size = new System.Drawing.Size(375, 106);
          this.groupBox3.TabIndex = 19;
          this.groupBox3.TabStop = false;
          this.groupBox3.Text = "Description";
          // 
          // Description
          // 
          this.Description.AcceptsTab = true;
          this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.Description.Location = new System.Drawing.Point(7, 20);
          this.Description.Multiline = true;
          this.Description.Name = "Description";
          this.Description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
          this.Description.Size = new System.Drawing.Size(362, 80);
          this.Description.TabIndex = 0;
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.ComponentName);
          this.groupBox2.Location = new System.Drawing.Point(6, 8);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(187, 51);
          this.groupBox2.TabIndex = 17;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Name";
          // 
          // ComponentName
          // 
          this.ComponentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.ComponentName.Location = new System.Drawing.Point(6, 19);
          this.ComponentName.Name = "ComponentName";
          this.ComponentName.Size = new System.Drawing.Size(175, 20);
          this.ComponentName.TabIndex = 9;
          // 
          // ScannerListGroupBox
          // 
          this.scannerListGroupBox.Controls.Add(this.ComponentList);
          this.scannerListGroupBox.Location = new System.Drawing.Point(6, 65);
          this.scannerListGroupBox.Name = "scannerListGroupBox";
          this.scannerListGroupBox.Size = new System.Drawing.Size(187, 271);
          this.scannerListGroupBox.TabIndex = 11;
          this.scannerListGroupBox.TabStop = false;
          this.scannerListGroupBox.Text = "Available Types";
          // 
          // ComponentList
          // 
          this.ComponentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.ComponentList.FormattingEnabled = true;
          this.ComponentList.Location = new System.Drawing.Point(6, 22);
          this.ComponentList.Name = "ComponentList";
          this.ComponentList.Size = new System.Drawing.Size(175, 238);
          this.ComponentList.Sorted = true;
          this.ComponentList.TabIndex = 2;
          this.ComponentList.SelectedIndexChanged += new System.EventHandler(this.ComponentList_SelectedIndexChanged);
          // 
          // ComponentImage
          // 
          this.ComponentImage.Image = null;
          this.ComponentImage.Location = new System.Drawing.Point(392, 4);
          this.ComponentImage.Name = "ComponentImage";
          this.ComponentImage.Size = new System.Drawing.Size(189, 179);
          this.ComponentImage.TabIndex = 18;
          // 
          // TechRequirements
          // 
          this.TechRequirements.Location = new System.Drawing.Point(197, 4);
          this.TechRequirements.Name = "TechRequirements";
          this.TechRequirements.Size = new System.Drawing.Size(189, 180);
          this.TechRequirements.TabIndex = 14;
          this.TechRequirements.Value = new Nova.Common.TechLevel(0, 0, 0, 0, 0, 0);
          // 
          // BasicProperties
          // 
          this.BasicProperties.Cost = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
          this.BasicProperties.Location = new System.Drawing.Point(199, 185);
          this.BasicProperties.Mass = 0;
          this.BasicProperties.Name = "BasicProperties";
          this.BasicProperties.Size = new System.Drawing.Size(187, 157);
          this.BasicProperties.TabIndex = 13;
          // 
          // CommonProperties
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.groupBox3);
          this.Controls.Add(this.ComponentImage);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.TechRequirements);
          this.Controls.Add(this.BasicProperties);
          this.Controls.Add(this.scannerListGroupBox);
          this.Name = "CommonProperties";
          this.Size = new System.Drawing.Size(581, 458);
          this.groupBox3.ResumeLayout(false);
          this.groupBox3.PerformLayout();
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          this.scannerListGroupBox.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.GroupBox scannerListGroupBox;
      public System.Windows.Forms.ListBox ComponentList;
      public System.Windows.Forms.TextBox ComponentName;
      public TechRequirements TechRequirements;
      public System.Windows.Forms.TextBox Description;
      public ImageDisplay ComponentImage;
      public BasicProperties BasicProperties;
   }
}
