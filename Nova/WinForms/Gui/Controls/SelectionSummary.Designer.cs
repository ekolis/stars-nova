namespace Nova.WinForms.Gui
{
   public partial class SelectionSummary
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.SelectedItem = new System.Windows.Forms.GroupBox();
         this.SuspendLayout();
         // 
         // SelectedItem
         // 
         this.SelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
         this.SelectedItem.Location = new System.Drawing.Point(0, 0);
         this.SelectedItem.Name = "SelectedItem";
         this.SelectedItem.Size = new System.Drawing.Size(359, 177);
         this.SelectedItem.TabIndex = 0;
         this.SelectedItem.TabStop = false;
         this.SelectedItem.Text = "Summary of Selected Item";
         // 
         // SelectionSummary
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.SelectedItem);
         this.Name = "SelectionSummary";
         this.Size = new System.Drawing.Size(359, 177);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox SelectedItem;
   }
}
