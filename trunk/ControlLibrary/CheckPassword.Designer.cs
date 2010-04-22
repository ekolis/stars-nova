namespace ControlLibrary
{
   partial class CheckPassword
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckPassword));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.PassWord = new System.Windows.Forms.TextBox();
          this.label1 = new System.Windows.Forms.Label();
          this.OKButton = new System.Windows.Forms.Button();
          this.Cancel = new System.Windows.Forms.Button();
          this.groupBox1.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.PassWord);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Controls.Add(this.OKButton);
          this.groupBox1.Controls.Add(this.Cancel);
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
          this.PassWord.Location = new System.Drawing.Point(12, 44);
          this.PassWord.Name = "PassWord";
          this.PassWord.Size = new System.Drawing.Size(228, 20);
          this.PassWord.TabIndex = 1;
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
          this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.OKButton.Location = new System.Drawing.Point(155, 81);
          this.OKButton.Name = "OKButton";
          this.OKButton.Size = new System.Drawing.Size(75, 23);
          this.OKButton.TabIndex = 3;
          this.OKButton.Text = "OK";
          this.OKButton.UseVisualStyleBackColor = true;
          this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
          // 
          // Cancel
          // 
          this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.Cancel.Location = new System.Drawing.Point(27, 81);
          this.Cancel.Name = "Cancel";
          this.Cancel.Size = new System.Drawing.Size(75, 23);
          this.Cancel.TabIndex = 2;
          this.Cancel.Text = "Cancel";
          this.Cancel.UseVisualStyleBackColor = true;
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
      private System.Windows.Forms.Button OKButton;
      private System.Windows.Forms.Button Cancel;
      private System.Windows.Forms.TextBox PassWord;
   }
}