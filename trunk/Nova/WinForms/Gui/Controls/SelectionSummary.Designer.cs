namespace Nova.WinForms.Gui
{
   public partial class SelectionSummary
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

      #region Component Designer generated code

      /// <Summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
         this.selectedItem = new System.Windows.Forms.GroupBox();
         this.SuspendLayout();
         // 
         // SelectedItem
         // 
         this.selectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
         this.selectedItem.Location = new System.Drawing.Point(0, 0);
         this.selectedItem.Name = "selectedItem";
         this.selectedItem.Size = new System.Drawing.Size(359, 177);
         this.selectedItem.TabIndex = 0;
         this.selectedItem.TabStop = false;
         this.selectedItem.Text = "Summary of Selected Item";
         // 
         // SelectionSummary
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.selectedItem);
         this.Name = "SelectionSummary";
         this.Size = new System.Drawing.Size(359, 177);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox selectedItem;
   }
}
