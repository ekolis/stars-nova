#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

namespace Nova.WinForms
{
   public partial class AboutBox
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

      #region Windows Form Designer generated code

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
          this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
          this.logoPictureBox = new System.Windows.Forms.PictureBox();
          this.labelProductName = new System.Windows.Forms.Label();
          this.labelVersion = new System.Windows.Forms.Label();
          this.Description = new System.Windows.Forms.TextBox();
          this.okButton = new System.Windows.Forms.Button();
          this.tableLayoutPanel.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
          this.SuspendLayout();
          // 
          // tableLayoutPanel
          // 
          this.tableLayoutPanel.ColumnCount = 2;
          this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
          this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
          this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
          this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
          this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
          this.tableLayoutPanel.Controls.Add(this.Description, 1, 2);
          this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
          this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
          this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
          this.tableLayoutPanel.Name = "tableLayoutPanel";
          this.tableLayoutPanel.RowCount = 4;
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.03687F));
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.608295F));
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
          this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
          this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
          this.tableLayoutPanel.TabIndex = 0;
          // 
          // logoPictureBox
          // 
          this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
          this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
          this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
          this.logoPictureBox.Name = "logoPictureBox";
          this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
          this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
          this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
          this.logoPictureBox.TabIndex = 12;
          this.logoPictureBox.TabStop = false;
          // 
          // labelProductName
          // 
          this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labelProductName.Location = new System.Drawing.Point(143, 0);
          this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
          this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
          this.labelProductName.Name = "labelProductName";
          this.labelProductName.Size = new System.Drawing.Size(271, 17);
          this.labelProductName.TabIndex = 19;
          this.labelProductName.Text = "Product Name";
          this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labelVersion
          // 
          this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labelVersion.Location = new System.Drawing.Point(143, 21);
          this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
          this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
          this.labelVersion.Name = "labelVersion";
          this.labelVersion.Size = new System.Drawing.Size(271, 17);
          this.labelVersion.TabIndex = 0;
          this.labelVersion.Text = "Version";
          this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // Description
          // 
          this.Description.Dock = System.Windows.Forms.DockStyle.Fill;
          this.Description.Location = new System.Drawing.Point(143, 45);
          this.Description.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
          this.Description.Multiline = true;
          this.Description.Name = "Description";
          this.Description.ReadOnly = true;
          this.Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
          this.Description.Size = new System.Drawing.Size(271, 159);
          this.Description.TabIndex = 23;
          this.Description.TabStop = false;
          this.Description.Text = "Description";
          // 
          // okButton
          // 
          this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.okButton.Location = new System.Drawing.Point(331, 235);
          this.okButton.Name = "okButton";
          this.okButton.Size = new System.Drawing.Size(83, 27);
          this.okButton.TabIndex = 24;
          this.okButton.Text = "&OK";
          // 
          // AboutBox
          // 
          this.AcceptButton = this.okButton;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(435, 283);
          this.Controls.Add(this.tableLayoutPanel);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "AboutBox";
          this.Padding = new System.Windows.Forms.Padding(9);
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "Stars! Nova - AboutBox";
          this.tableLayoutPanel.ResumeLayout(false);
          this.tableLayoutPanel.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
      private System.Windows.Forms.PictureBox logoPictureBox;
      private System.Windows.Forms.Label labelProductName;
      private System.Windows.Forms.Label labelVersion;
      private System.Windows.Forms.Button okButton;
      public System.Windows.Forms.TextBox Description;
   }
}