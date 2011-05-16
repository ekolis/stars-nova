namespace Nova.WinForms.Gui
{
   public partial class RenameFleet
   {
      /// <Summary>
      /// Required designer variable.
      /// </Summary>
      private System.ComponentModel.IContainer components = null;

      /// <Summary>
      /// Clean up any resources being used.
      /// </Summary>
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

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.ExistingName = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.newName = new System.Windows.Forms.TextBox();
         this.okButton = new System.Windows.Forms.Button();
         this.cancelRename = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.ExistingName);
         this.groupBox1.Location = new System.Drawing.Point(14, 12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(201, 44);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Existing Fleet Name";
         // 
         // ExistingName
         // 
         this.ExistingName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ExistingName.Location = new System.Drawing.Point(7, 16);
         this.ExistingName.Name = "ExistingName";
         this.ExistingName.Size = new System.Drawing.Size(189, 20);
         this.ExistingName.TabIndex = 0;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.newName);
         this.groupBox2.Location = new System.Drawing.Point(14, 60);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(201, 44);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "New Fleet Name";
         // 
         // NewName
         // 
         this.newName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.newName.Location = new System.Drawing.Point(6, 18);
         this.newName.Name = "newName";
         this.newName.Size = new System.Drawing.Size(189, 20);
         this.newName.TabIndex = 0;
         // 
         // OKButton
         // 
         this.okButton.Location = new System.Drawing.Point(237, 75);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(75, 23);
         this.okButton.TabIndex = 2;
         this.okButton.Text = "Rename";
         this.okButton.UseVisualStyleBackColor = true;
         this.okButton.Click += new System.EventHandler(this.OKButton_Click);
         // 
         // CancelRename
         // 
         this.cancelRename.Location = new System.Drawing.Point(236, 25);
         this.cancelRename.Name = "cancelRename";
         this.cancelRename.Size = new System.Drawing.Size(75, 23);
         this.cancelRename.TabIndex = 3;
         this.cancelRename.Text = "Cancel";
         this.cancelRename.UseVisualStyleBackColor = true;
         this.cancelRename.Click += new System.EventHandler(this.CancelRename_Click);
         // 
         // RenameFleet
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(323, 115);
         this.ControlBox = false;
         this.Controls.Add(this.cancelRename);
         this.Controls.Add(this.okButton);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "RenameFleet";
         this.Text = "Nova - Rename Fleet";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.TextBox newName;
      private System.Windows.Forms.Button okButton;
      private System.Windows.Forms.Button cancelRename;
      public System.Windows.Forms.Label ExistingName;

   }
}