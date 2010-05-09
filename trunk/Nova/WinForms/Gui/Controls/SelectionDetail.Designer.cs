namespace Nova.WinForms.Gui
{
   partial class SelectionDetail
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
         this.DetailPanel = new System.Windows.Forms.GroupBox();
         this.SuspendLayout();
         // 
         // DetailPanel
         // 
         this.DetailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.DetailPanel.Location = new System.Drawing.Point(0, 0);
         this.DetailPanel.Name = "DetailPanel";
         this.DetailPanel.Size = new System.Drawing.Size(350, 330);
         this.DetailPanel.TabIndex = 0;
         this.DetailPanel.TabStop = false;
         this.DetailPanel.Text = "Selection Detail";
         // 
         // SelectionDetail
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.DetailPanel);
         this.Name = "SelectionDetail";
         this.Size = new System.Drawing.Size(350, 330);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox DetailPanel;
   }
}
