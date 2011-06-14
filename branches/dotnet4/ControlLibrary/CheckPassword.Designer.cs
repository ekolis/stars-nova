namespace Nova.ControlLibrary
{
    /// <summary>
    /// 
    /// </summary>
   public partial class CheckPassword
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckPassword));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.password = new System.Windows.Forms.TextBox();
          this.label1 = new System.Windows.Forms.Label();
          this.okButton = new System.Windows.Forms.Button();
          this.cancel = new System.Windows.Forms.Button();
          this.groupBox1.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.password);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Controls.Add(this.okButton);
          this.groupBox1.Controls.Add(this.cancel);
          this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.groupBox1.Location = new System.Drawing.Point(0, 0);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(246, 115);
          this.groupBox1.TabIndex = 0;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "A Password is Required To Access This File";
          // 
          // PassWord
          // 
          this.password.Location = new System.Drawing.Point(12, 44);
          this.password.Name = "password";
          this.password.Size = new System.Drawing.Size(228, 20);
          this.password.TabIndex = 1;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(12, 22);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(198, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Please Enter The Password For This File";
          // 
          // OKButton
          // 
          this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.okButton.Location = new System.Drawing.Point(155, 81);
          this.okButton.Name = "okButton";
          this.okButton.Size = new System.Drawing.Size(75, 23);
          this.okButton.TabIndex = 3;
          this.okButton.Text = "OK";
          this.okButton.UseVisualStyleBackColor = true;
          this.okButton.Click += new System.EventHandler(this.OKButton_Click);
          // 
          // Cancel
          // 
          this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.cancel.Location = new System.Drawing.Point(27, 81);
          this.cancel.Name = "cancel";
          this.cancel.Size = new System.Drawing.Size(75, 23);
          this.cancel.TabIndex = 2;
          this.cancel.Text = "Cancel";
          this.cancel.UseVisualStyleBackColor = true;
          // 
          // CheckPassword
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(246, 115);
          this.Controls.Add(this.groupBox1);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "CheckPassword";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Nova Password Required";
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button okButton;
      private System.Windows.Forms.Button cancel;
      private System.Windows.Forms.TextBox password;
   }
}