// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.Client
{
   public partial class SelectRaceDialog
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectRaceDialog));
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.RaceList = new System.Windows.Forms.ListBox();
          this.okButton = new System.Windows.Forms.Button();
          this.raceCancelButton = new System.Windows.Forms.Button();
          this.groupBox1.SuspendLayout();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(12, 9);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(208, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Select the Race to play from the list below.";
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.RaceList);
          this.groupBox1.Controls.Add(this.okButton);
          this.groupBox1.Controls.Add(this.raceCancelButton);
          this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.groupBox1.Location = new System.Drawing.Point(0, 0);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(254, 392);
          this.groupBox1.TabIndex = 1;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Player Race Selection";
          // 
          // RaceList
          // 
          this.RaceList.FormattingEnabled = true;
          this.RaceList.Location = new System.Drawing.Point(6, 19);
          this.RaceList.Name = "RaceList";
          this.RaceList.Size = new System.Drawing.Size(230, 290);
          this.RaceList.TabIndex = 2;
          // 
          // okButton
          // 
          this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.okButton.Location = new System.Drawing.Point(6, 357);
          this.okButton.Name = "okButton";
          this.okButton.Size = new System.Drawing.Size(75, 23);
          this.okButton.TabIndex = 1;
          this.okButton.Text = "OK";
          this.okButton.UseVisualStyleBackColor = true;
          // 
          // raceCancelButton
          // 
          this.raceCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.raceCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.raceCancelButton.Location = new System.Drawing.Point(161, 357);
          this.raceCancelButton.Name = "raceCancelButton";
          this.raceCancelButton.Size = new System.Drawing.Size(75, 23);
          this.raceCancelButton.TabIndex = 0;
          this.raceCancelButton.Text = "Cancel";
          this.raceCancelButton.UseVisualStyleBackColor = true;
          // 
          // SelectRaceDialog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(254, 392);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.label1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "SelectRaceDialog";
          this.ShowInTaskbar = false;
          this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
          this.Text = "Stars! Nova - Select Race";
          this.groupBox1.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button okButton;
      private System.Windows.Forms.Button raceCancelButton;
      public System.Windows.Forms.ListBox RaceList;
   }
}