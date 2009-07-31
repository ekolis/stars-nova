namespace NovaConsole
{
   partial class VictoryConditions
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VictoryConditions));
          this.label2 = new System.Windows.Forms.Label();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.HighestScore = new ControlLibrary.EnabledCounter();
          this.CapitalShips = new ControlLibrary.EnabledCounter();
          this.ProductionCapacity = new ControlLibrary.EnabledCounter();
          this.TotalScore = new ControlLibrary.EnabledCounter();
          this.NumberOfFields = new ControlLibrary.EnabledCounter();
          this.TechLevels = new ControlLibrary.EnabledCounter();
          this.PlanetsOwned = new ControlLibrary.EnabledCounter();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.MinimumGameTime = new System.Windows.Forms.NumericUpDown();
          this.label5 = new System.Windows.Forms.Label();
          this.TargetsToMeet = new System.Windows.Forms.NumericUpDown();
          this.label4 = new System.Windows.Forms.Label();
          this.CancelSelected = new System.Windows.Forms.Button();
          this.OKSelected = new System.Windows.Forms.Button();
          this.ExceedsSecondPlace = new ControlLibrary.EnabledCounter();
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.MinimumGameTime)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.TargetsToMeet)).BeginInit();
          this.SuspendLayout();
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Enabled = false;
          this.label2.Location = new System.Drawing.Point(69, 57);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(0, 13);
          this.label2.TabIndex = 3;
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.ExceedsSecondPlace);
          this.groupBox1.Controls.Add(this.HighestScore);
          this.groupBox1.Controls.Add(this.CapitalShips);
          this.groupBox1.Controls.Add(this.ProductionCapacity);
          this.groupBox1.Controls.Add(this.TotalScore);
          this.groupBox1.Controls.Add(this.NumberOfFields);
          this.groupBox1.Controls.Add(this.TechLevels);
          this.groupBox1.Controls.Add(this.PlanetsOwned);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Controls.Add(this.label2);
          this.groupBox1.Location = new System.Drawing.Point(12, 12);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(352, 253);
          this.groupBox1.TabIndex = 20;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Victory Conditions";
          // 
          // HighestScore
          // 
          this.HighestScore.ControlCounter = 100;
          this.HighestScore.ControlSelected = false;
          this.HighestScore.ControlText = "Has the highest score after (years)";
          this.HighestScore.Location = new System.Drawing.Point(17, 191);
          this.HighestScore.Maximum = 10000;
          this.HighestScore.Minimum = 20;
          this.HighestScore.Name = "HighestScore";
          this.HighestScore.Size = new System.Drawing.Size(329, 23);
          this.HighestScore.TabIndex = 28;
          this.HighestScore.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("HighestScore.Value")));
          // 
          // CapitalShips
          // 
          this.CapitalShips.ControlCounter = 100;
          this.CapitalShips.ControlSelected = false;
          this.CapitalShips.ControlText = "Number of capital ships";
          this.CapitalShips.Location = new System.Drawing.Point(17, 167);
          this.CapitalShips.Maximum = 10000;
          this.CapitalShips.Minimum = 1;
          this.CapitalShips.Name = "CapitalShips";
          this.CapitalShips.Size = new System.Drawing.Size(329, 23);
          this.CapitalShips.TabIndex = 27;
          this.CapitalShips.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("CapitalShips.Value")));
          // 
          // ProductionCapacity
          // 
          this.ProductionCapacity.ControlCounter = 100;
          this.ProductionCapacity.ControlSelected = false;
          this.ProductionCapacity.ControlText = "Has a production capacity of (in K resources)";
          this.ProductionCapacity.Location = new System.Drawing.Point(17, 143);
          this.ProductionCapacity.Maximum = 10000;
          this.ProductionCapacity.Minimum = 10;
          this.ProductionCapacity.Name = "ProductionCapacity";
          this.ProductionCapacity.Size = new System.Drawing.Size(329, 23);
          this.ProductionCapacity.TabIndex = 26;
          this.ProductionCapacity.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("ProductionCapacity.Value")));
          // 
          // TotalScore
          // 
          this.TotalScore.ControlCounter = 100;
          this.TotalScore.ControlSelected = false;
          this.TotalScore.ControlText = "Exceeds a score of";
          this.TotalScore.Location = new System.Drawing.Point(17, 119);
          this.TotalScore.Maximum = 10000;
          this.TotalScore.Minimum = 100;
          this.TotalScore.Name = "TotalScore";
          this.TotalScore.Size = new System.Drawing.Size(329, 23);
          this.TotalScore.TabIndex = 24;
          this.TotalScore.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("TotalScore.Value")));
          // 
          // NumberOfFields
          // 
          this.NumberOfFields.ControlCounter = 4;
          this.NumberOfFields.ControlSelected = false;
          this.NumberOfFields.ControlText = "In the following number of fields";
          this.NumberOfFields.Location = new System.Drawing.Point(17, 95);
          this.NumberOfFields.Maximum = 6;
          this.NumberOfFields.Minimum = 0;
          this.NumberOfFields.Name = "NumberOfFields";
          this.NumberOfFields.Size = new System.Drawing.Size(329, 23);
          this.NumberOfFields.TabIndex = 23;
          this.NumberOfFields.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("NumberOfFields.Value")));
          // 
          // TechLevels
          // 
          this.TechLevels.ControlCounter = 22;
          this.TechLevels.ControlSelected = false;
          this.TechLevels.ControlText = "Attains the following tech-level";
          this.TechLevels.Location = new System.Drawing.Point(17, 71);
          this.TechLevels.Maximum = 10000;
          this.TechLevels.Minimum = 5;
          this.TechLevels.Name = "TechLevels";
          this.TechLevels.Size = new System.Drawing.Size(329, 23);
          this.TechLevels.TabIndex = 22;
          this.TechLevels.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("TechLevels.Value")));
          // 
          // PlanetsOwned
          // 
          this.PlanetsOwned.ControlCounter = 60;
          this.PlanetsOwned.ControlSelected = false;
          this.PlanetsOwned.ControlText = "Owns the following number of planets (%)";
          this.PlanetsOwned.Location = new System.Drawing.Point(17, 47);
          this.PlanetsOwned.Maximum = 10000;
          this.PlanetsOwned.Minimum = 0;
          this.PlanetsOwned.Name = "PlanetsOwned";
          this.PlanetsOwned.Size = new System.Drawing.Size(329, 23);
          this.PlanetsOwned.TabIndex = 21;
          this.PlanetsOwned.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("PlanetsOwned.Value")));
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Enabled = false;
          this.label1.Location = new System.Drawing.Point(14, 28);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(165, 13);
          this.label1.TabIndex = 2;
          this.label1.Text = "Victory is declared when a player:";
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.MinimumGameTime);
          this.groupBox2.Controls.Add(this.label5);
          this.groupBox2.Controls.Add(this.TargetsToMeet);
          this.groupBox2.Controls.Add(this.label4);
          this.groupBox2.Location = new System.Drawing.Point(12, 280);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(352, 77);
          this.groupBox2.TabIndex = 24;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Main criteria";
          // 
          // MinimumGameTime
          // 
          this.MinimumGameTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.MinimumGameTime.Location = new System.Drawing.Point(277, 47);
          this.MinimumGameTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
          this.MinimumGameTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
          this.MinimumGameTime.Name = "MinimumGameTime";
          this.MinimumGameTime.Size = new System.Drawing.Size(69, 20);
          this.MinimumGameTime.TabIndex = 34;
          this.MinimumGameTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.MinimumGameTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
          // 
          // label5
          // 
          this.label5.Location = new System.Drawing.Point(17, 47);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(195, 20);
          this.label5.TabIndex = 33;
          this.label5.Text = "Minimum game time (years)";
          this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // TargetsToMeet
          // 
          this.TargetsToMeet.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.TargetsToMeet.Location = new System.Drawing.Point(277, 17);
          this.TargetsToMeet.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
          this.TargetsToMeet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.TargetsToMeet.Name = "TargetsToMeet";
          this.TargetsToMeet.Size = new System.Drawing.Size(69, 20);
          this.TargetsToMeet.TabIndex = 32;
          this.TargetsToMeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.TargetsToMeet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // label4
          // 
          this.label4.Location = new System.Drawing.Point(17, 17);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(195, 20);
          this.label4.TabIndex = 31;
          this.label4.Text = "Number of targets to meet";
          this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // CancelSelected
          // 
          this.CancelSelected.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.CancelSelected.Location = new System.Drawing.Point(12, 379);
          this.CancelSelected.Name = "CancelSelected";
          this.CancelSelected.Size = new System.Drawing.Size(75, 23);
          this.CancelSelected.TabIndex = 26;
          this.CancelSelected.Text = "Cancel";
          this.CancelSelected.UseVisualStyleBackColor = true;
          // 
          // OKSelected
          // 
          this.OKSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.OKSelected.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.OKSelected.Location = new System.Drawing.Point(289, 379);
          this.OKSelected.Name = "OKSelected";
          this.OKSelected.Size = new System.Drawing.Size(75, 23);
          this.OKSelected.TabIndex = 27;
          this.OKSelected.Text = "OK";
          this.OKSelected.UseVisualStyleBackColor = true;
          this.OKSelected.Click += new System.EventHandler(this.OKSelected_Click);
          // 
          // ExceedsSecondPlace
          // 
          this.ExceedsSecondPlace.ControlCounter = 100;
          this.ExceedsSecondPlace.ControlSelected = false;
          this.ExceedsSecondPlace.ControlText = "Exceeds second place score by (%)";
          this.ExceedsSecondPlace.Location = new System.Drawing.Point(17, 215);
          this.ExceedsSecondPlace.Maximum = 500;
          this.ExceedsSecondPlace.Minimum = 10;
          this.ExceedsSecondPlace.Name = "ExceedsSecondPlace";
          this.ExceedsSecondPlace.Size = new System.Drawing.Size(329, 23);
          this.ExceedsSecondPlace.TabIndex = 29;
          this.ExceedsSecondPlace.Value = ((ControlLibrary.EnabledValue)(resources.GetObject("ExceedsSecondPlace.Value")));
          // 
          // VictoryConditions
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(376, 414);
          this.Controls.Add(this.OKSelected);
          this.Controls.Add(this.CancelSelected);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.groupBox1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "VictoryConditions";
          this.Text = "Nova Victory Conditions";
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.groupBox2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.MinimumGameTime)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.TargetsToMeet)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button CancelSelected;
      private System.Windows.Forms.Button OKSelected;
      private ControlLibrary.EnabledCounter PlanetsOwned;
      private ControlLibrary.EnabledCounter CapitalShips;
      private ControlLibrary.EnabledCounter ProductionCapacity;
      private ControlLibrary.EnabledCounter TotalScore;
      private ControlLibrary.EnabledCounter NumberOfFields;
       private ControlLibrary.EnabledCounter TechLevels;
       private ControlLibrary.EnabledCounter HighestScore;
       private System.Windows.Forms.NumericUpDown TargetsToMeet;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.NumericUpDown MinimumGameTime;
       private ControlLibrary.EnabledCounter ExceedsSecondPlace;
   }
}