namespace Nova.WinForms.Gui
{
   public partial class SelectionDetail
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
          this.planetDetail1 = new Nova.WinForms.Gui.PlanetDetail(starReports, playerFleets, researchBudget, playerRace, stateData);
          this.SuspendLayout();
          // 
          // planetDetail1
          // 
          this.planetDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.planetDetail1.Location = new System.Drawing.Point(0, 0);
          this.planetDetail1.Margin = new System.Windows.Forms.Padding(0);
          this.planetDetail1.Name = "planetDetail1";
          this.planetDetail1.Size = new System.Drawing.Size(361, 399);
          this.planetDetail1.TabIndex = 0;
         
          // 
          // SelectionDetail
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.planetDetail1);
          this.Margin = new System.Windows.Forms.Padding(0);
          this.Name = "SelectionDetail";
          this.Size = new System.Drawing.Size(361, 399);
          this.ResumeLayout(false);

      }

      #endregion

      private PlanetDetail planetDetail1;

   }
}
