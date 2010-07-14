namespace Nova.WinForms.Gui
{
   public partial class SelectionDetail
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
         this.detailPanel = new System.Windows.Forms.GroupBox();
         this.SuspendLayout();
         // 
         // DetailPanel
         // 
         this.detailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.detailPanel.Location = new System.Drawing.Point(0, 0);
         this.detailPanel.Name = "detailPanel";
         this.detailPanel.Size = new System.Drawing.Size(350, 330);
         this.detailPanel.TabIndex = 0;
         this.detailPanel.TabStop = false;
         this.detailPanel.Text = "Selection Detail";
         // 
         // SelectionDetail
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.detailPanel);
         this.Name = "SelectionDetail";
         this.Size = new System.Drawing.Size(350, 330);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox detailPanel;
   }
}
