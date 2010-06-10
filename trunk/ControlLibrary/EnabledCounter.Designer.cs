namespace Nova.ControlLibrary
{
   public partial class EnabledCounter
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
         this.CheckBox = new System.Windows.Forms.CheckBox();
         this.UpDownCounter = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.UpDownCounter)).BeginInit();
         this.SuspendLayout();
         // 
         // CheckBox
         // 
         this.CheckBox.AutoSize = true;
         this.CheckBox.Location = new System.Drawing.Point(4, 4);
         this.CheckBox.Name = "CheckBox";
         this.CheckBox.Size = new System.Drawing.Size(78, 17);
         this.CheckBox.TabIndex = 0;
         this.CheckBox.Text = "Check Box";
         this.CheckBox.UseVisualStyleBackColor = true;
         // 
         // UpDownCounter
         // 
         this.UpDownCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.UpDownCounter.Location = new System.Drawing.Point(118, 2);
         this.UpDownCounter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
         this.UpDownCounter.Name = "UpDownCounter";
         this.UpDownCounter.Size = new System.Drawing.Size(68, 20);
         this.UpDownCounter.TabIndex = 1;
         this.UpDownCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // EnabledCounter
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.UpDownCounter);
         this.Controls.Add(this.CheckBox);
         this.Name = "EnabledCounter";
         this.Size = new System.Drawing.Size(186, 23);
         ((System.ComponentModel.ISupportInitialize)(this.UpDownCounter)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.CheckBox CheckBox;
      private System.Windows.Forms.NumericUpDown UpDownCounter;
   }
}
