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

      /// <Summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
          this.planetDetail = new PlanetDetail(empireState, clientState);
          this.fleetDetail = new FleetDetail(clientState);
          this.SuspendLayout();
          // 
          // planetDetail1
          // 
          this.planetDetail.Dock = System.Windows.Forms.DockStyle.Fill;
          this.planetDetail.Location = new System.Drawing.Point(0, 0);
          this.planetDetail.Margin = new System.Windows.Forms.Padding(0);
          this.planetDetail.Name = "planetDetail1";
          this.planetDetail.Size = new System.Drawing.Size(361, 399);
          this.planetDetail.TabIndex = 0;   
          // 
          // fleetDetail1
          // 
          this.fleetDetail.Dock = System.Windows.Forms.DockStyle.Fill;
          this.fleetDetail.Location = new System.Drawing.Point(0, 0);
          this.fleetDetail.Margin = new System.Windows.Forms.Padding(0);
          this.fleetDetail.Name = "fleetDetail1";
          this.fleetDetail.Size = new System.Drawing.Size(361, 399);
          this.fleetDetail.TabIndex = 1;          
          // 
          // SelectionDetail
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.planetDetail);
          this.Controls.Add(this.fleetDetail);
          this.Margin = new System.Windows.Forms.Padding(0);
          this.Name = "SelectionDetail";
          this.Size = new System.Drawing.Size(361, 399);
          this.ResumeLayout(false);

      }

      #endregion

      private PlanetDetail planetDetail;
      private FleetDetail fleetDetail;

   }
}
