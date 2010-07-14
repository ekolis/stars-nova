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
      /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
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
         this.checkBox = new System.Windows.Forms.CheckBox();
         this.upDownCounter = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.upDownCounter)).BeginInit();
         this.SuspendLayout();
         // 
         // CheckBox
         // 
         this.checkBox.AutoSize = true;
         this.checkBox.Location = new System.Drawing.Point(4, 4);
         this.checkBox.Name = "checkBox";
         this.checkBox.Size = new System.Drawing.Size(78, 17);
         this.checkBox.TabIndex = 0;
         this.checkBox.Text = "Check Box";
         this.checkBox.UseVisualStyleBackColor = true;
         // 
         // UpDownCounter
         // 
         this.upDownCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.upDownCounter.Location = new System.Drawing.Point(118, 2);
         this.upDownCounter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
         this.upDownCounter.Name = "upDownCounter";
         this.upDownCounter.Size = new System.Drawing.Size(68, 20);
         this.upDownCounter.TabIndex = 1;
         this.upDownCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // EnabledCounter
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.upDownCounter);
         this.Controls.Add(this.checkBox);
         this.Name = "EnabledCounter";
         this.Size = new System.Drawing.Size(186, 23);
         ((System.ComponentModel.ISupportInitialize)(this.upDownCounter)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.CheckBox checkBox;
      private System.Windows.Forms.NumericUpDown upDownCounter;
   }
}
