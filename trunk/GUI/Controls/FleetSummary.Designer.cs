namespace Nova
{
   partial class FleetSummary
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.FleetShipCount = new System.Windows.Forms.Label();
         this.FleetSpeed = new System.Windows.Forms.Label();
         this.FleetMass = new System.Windows.Forms.Label();
         this.FleetImage = new System.Windows.Forms.PictureBox();
         this.label4 = new System.Windows.Forms.Label();
         this.FleetOwner = new System.Windows.Forms.Label();
         this.RaceIcon = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.FleetImage)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.RaceIcon)).BeginInit();
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
         this.FleetShipCount.Location = new System.Drawing.Point(135, 14);
         this.FleetShipCount.Name = "FleetShipCount";
         this.FleetShipCount.Size = new System.Drawing.Size(36, 14);
         this.FleetShipCount.TabIndex = 3;
         this.FleetShipCount.Text = "0";
         this.FleetShipCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetSpeed
         // 
         this.FleetSpeed.Location = new System.Drawing.Point(135, 49);
         this.FleetSpeed.Name = "FleetSpeed";
         this.FleetSpeed.Size = new System.Drawing.Size(36, 14);
         this.FleetSpeed.TabIndex = 4;
         this.FleetSpeed.Text = "0";
         this.FleetSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetMass
         // 
         this.FleetMass.Location = new System.Drawing.Point(135, 32);
         this.FleetMass.Name = "FleetMass";
         this.FleetMass.Size = new System.Drawing.Size(36, 14);
         this.FleetMass.TabIndex = 5;
         this.FleetMass.Text = "0";
         this.FleetMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // FleetImage
         // 
         this.FleetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.FleetImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.FleetImage.Location = new System.Drawing.Point(258, 14);
         this.FleetImage.Name = "FleetImage";
         this.FleetImage.Size = new System.Drawing.Size(64, 64);
         this.FleetImage.TabIndex = 6;
         this.FleetImage.TabStop = false;
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
         this.FleetOwner.Location = new System.Drawing.Point(82, 127);
         this.FleetOwner.Name = "FleetOwner";
         this.FleetOwner.Size = new System.Drawing.Size(170, 23);
         this.FleetOwner.TabIndex = 8;
         this.FleetOwner.Text = "label5";
         this.FleetOwner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // RaceIcon
         // 
         this.RaceIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.RaceIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.RaceIcon.Location = new System.Drawing.Point(258, 84);
         this.RaceIcon.Name = "RaceIcon";
         this.RaceIcon.Size = new System.Drawing.Size(64, 64);
         this.RaceIcon.TabIndex = 9;
         this.RaceIcon.TabStop = false;
         // 
         // FleetSummary
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.RaceIcon);
         this.Controls.Add(this.FleetOwner);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.FleetImage);
         this.Controls.Add(this.FleetMass);
         this.Controls.Add(this.FleetSpeed);
         this.Controls.Add(this.FleetShipCount);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Name = "FleetSummary";
         this.Size = new System.Drawing.Size(340, 157);
         ((System.ComponentModel.ISupportInitialize)(this.FleetImage)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.RaceIcon)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label FleetShipCount;
      private System.Windows.Forms.Label FleetSpeed;
      private System.Windows.Forms.Label FleetMass;
      private System.Windows.Forms.PictureBox FleetImage;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label FleetOwner;
      private System.Windows.Forms.PictureBox RaceIcon;
   }
}
