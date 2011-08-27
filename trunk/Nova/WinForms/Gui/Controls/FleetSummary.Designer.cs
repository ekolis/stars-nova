namespace Nova.WinForms.Gui
{
   public partial class FleetSummary
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.fleetShipCount = new System.Windows.Forms.Label();
         this.fleetSpeed = new System.Windows.Forms.Label();
         this.fleetMass = new System.Windows.Forms.Label();
         this.fleetImage = new System.Windows.Forms.PictureBox();
         this.label4 = new System.Windows.Forms.Label();
         this.fleetOwner = new System.Windows.Forms.Label();
         this.raceIcon = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.fleetImage)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.raceIcon)).BeginInit();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(13, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(120, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Number of ships in Fleet";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(13, 32);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(79, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "Fleet mass (kT)";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(13, 49);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(94, 13);
         this.label3.TabIndex = 2;
         this.label3.Text = "Fleet speed (warp)";
         // 
         // FleetShipCount
         // 
         this.fleetShipCount.Location = new System.Drawing.Point(135, 14);
         this.fleetShipCount.Name = "fleetShipCount";
         this.fleetShipCount.Size = new System.Drawing.Size(36, 14);
         this.fleetShipCount.TabIndex = 3;
         this.fleetShipCount.Text = "0";
         this.fleetShipCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetSpeed
         // 
         this.fleetSpeed.Location = new System.Drawing.Point(135, 49);
         this.fleetSpeed.Name = "fleetSpeed";
         this.fleetSpeed.Size = new System.Drawing.Size(36, 14);
         this.fleetSpeed.TabIndex = 4;
         this.fleetSpeed.Text = "0";
         this.fleetSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetMass
         // 
         this.fleetMass.Location = new System.Drawing.Point(135, 32);
         this.fleetMass.Name = "fleetMass";
         this.fleetMass.Size = new System.Drawing.Size(36, 14);
         this.fleetMass.TabIndex = 5;
         this.fleetMass.Text = "0";
         this.fleetMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetImage
         // 
         this.fleetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.fleetImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.fleetImage.Location = new System.Drawing.Point(258, 14);
         this.fleetImage.Name = "fleetImage";
         this.fleetImage.Size = new System.Drawing.Size(64, 64);
         this.fleetImage.TabIndex = 6;
         this.fleetImage.TabStop = false;
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(13, 127);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(69, 23);
         this.label4.TabIndex = 7;
         this.label4.Text = "Fleet Owner:";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FleetOwner
         // 
         this.fleetOwner.Location = new System.Drawing.Point(82, 127);
         this.fleetOwner.Name = "fleetOwner";
         this.fleetOwner.Size = new System.Drawing.Size(170, 23);
         this.fleetOwner.TabIndex = 8;
         this.fleetOwner.Text = "label5";
         this.fleetOwner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // RaceIcon
         // 
         this.raceIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.raceIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.raceIcon.Location = new System.Drawing.Point(258, 84);
         this.raceIcon.Name = "raceIcon";
         this.raceIcon.Size = new System.Drawing.Size(64, 64);
         this.raceIcon.TabIndex = 9;
         this.raceIcon.TabStop = false;
         // 
         // FleetSummary
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.raceIcon);
         this.Controls.Add(this.fleetOwner);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.fleetImage);
         this.Controls.Add(this.fleetMass);
         this.Controls.Add(this.fleetSpeed);
         this.Controls.Add(this.fleetShipCount);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Name = "FleetSummary";
         this.Size = new System.Drawing.Size(340, 157);
         ((System.ComponentModel.ISupportInitialize)(this.fleetImage)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.raceIcon)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label fleetShipCount;
      private System.Windows.Forms.Label fleetSpeed;
      private System.Windows.Forms.Label fleetMass;
      private System.Windows.Forms.PictureBox fleetImage;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label fleetOwner;
      private System.Windows.Forms.PictureBox raceIcon;
   }
}
