namespace Nova.WinForms.Gui
{
   public partial class RenameFleet
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
         if (disposing && (components != null)) {
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
         this.ExistingName = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.NewName = new System.Windows.Forms.TextBox();
         this.OKButton = new System.Windows.Forms.Button();
         this.CancelRename = new System.Windows.Forms.Button();
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
         this.groupBox2.Controls.Add(this.NewName);
         this.groupBox2.Location = new System.Drawing.Point(14, 60);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(201, 44);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "New Fleet Name";
         // 
         // NewName
         // 
         this.NewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.NewName.Location = new System.Drawing.Point(6, 18);
         this.NewName.Name = "NewName";
         this.NewName.Size = new System.Drawing.Size(189, 20);
         this.NewName.TabIndex = 0;
         // 
         // OKButton
         // 
         this.OKButton.Location = new System.Drawing.Point(237, 75);
         this.OKButton.Name = "OKButton";
         this.OKButton.Size = new System.Drawing.Size(75, 23);
         this.OKButton.TabIndex = 2;
         this.OKButton.Text = "Rename";
         this.OKButton.UseVisualStyleBackColor = true;
         this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
         // 
         // CancelRename
         // 
         this.CancelRename.Location = new System.Drawing.Point(236, 25);
         this.CancelRename.Name = "CancelRename";
         this.CancelRename.Size = new System.Drawing.Size(75, 23);
         this.CancelRename.TabIndex = 3;
         this.CancelRename.Text = "Cancel";
         this.CancelRename.UseVisualStyleBackColor = true;
         this.CancelRename.Click += new System.EventHandler(this.CancelRename_Click);
         // 
         // RenameFleet
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(323, 115);
         this.ControlBox = false;
         this.Controls.Add(this.CancelRename);
         this.Controls.Add(this.OKButton);
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
      private System.Windows.Forms.TextBox NewName;
      private System.Windows.Forms.Button OKButton;
      private System.Windows.Forms.Button CancelRename;
      public System.Windows.Forms.Label ExistingName;

   }
}