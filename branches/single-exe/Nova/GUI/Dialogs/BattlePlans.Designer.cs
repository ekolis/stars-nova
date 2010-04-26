namespace Nova.Gui.Dialogs
{
   partial class BattlePlans
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.PlanList = new System.Windows.Forms.ListBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.ModifyPlan = new System.Windows.Forms.Button();
         this.NewPlan = new System.Windows.Forms.Button();
         this.label5 = new System.Windows.Forms.Label();
         this.PlanName = new System.Windows.Forms.TextBox();
         this.SecondaryTarget = new System.Windows.Forms.ComboBox();
         this.Attack = new System.Windows.Forms.ComboBox();
         this.label4 = new System.Windows.Forms.Label();
         this.Tactic = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.PrimaryTarget = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.DoneButton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.PlanList);
         this.groupBox1.Location = new System.Drawing.Point(13, 8);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(216, 298);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Available Plans";
         // 
         // PlanList
         // 
         this.PlanList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.PlanList.FormattingEnabled = true;
         this.PlanList.Location = new System.Drawing.Point(3, 16);
         this.PlanList.Name = "PlanList";
         this.PlanList.Size = new System.Drawing.Size(210, 277);
         this.PlanList.TabIndex = 0;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.ModifyPlan);
         this.groupBox2.Controls.Add(this.NewPlan);
         this.groupBox2.Controls.Add(this.label5);
         this.groupBox2.Controls.Add(this.PlanName);
         this.groupBox2.Controls.Add(this.SecondaryTarget);
         this.groupBox2.Controls.Add(this.Attack);
         this.groupBox2.Controls.Add(this.label4);
         this.groupBox2.Controls.Add(this.Tactic);
         this.groupBox2.Controls.Add(this.label3);
         this.groupBox2.Controls.Add(this.label2);
         this.groupBox2.Controls.Add(this.PrimaryTarget);
         this.groupBox2.Controls.Add(this.label1);
         this.groupBox2.Location = new System.Drawing.Point(242, 8);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(216, 298);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Plan Details";
         // 
         // ModifyPlan
         // 
         this.ModifyPlan.Enabled = false;
         this.ModifyPlan.Location = new System.Drawing.Point(128, 265);
         this.ModifyPlan.Name = "ModifyPlan";
         this.ModifyPlan.Size = new System.Drawing.Size(75, 23);
         this.ModifyPlan.TabIndex = 10;
         this.ModifyPlan.Text = "Modify";
         this.ModifyPlan.UseVisualStyleBackColor = true;
         // 
         // NewPlan
         // 
         this.NewPlan.Enabled = false;
         this.NewPlan.Location = new System.Drawing.Point(12, 265);
         this.NewPlan.Name = "NewPlan";
         this.NewPlan.Size = new System.Drawing.Size(75, 23);
         this.NewPlan.TabIndex = 0;
         this.NewPlan.Text = "New";
         this.NewPlan.UseVisualStyleBackColor = true;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(9, 17);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(35, 13);
         this.label5.TabIndex = 9;
         this.label5.Text = "Name";
         // 
         // PlanName
         // 
         this.PlanName.Location = new System.Drawing.Point(9, 34);
         this.PlanName.Name = "PlanName";
         this.PlanName.Size = new System.Drawing.Size(194, 20);
         this.PlanName.TabIndex = 1;
         // 
         // SecondaryTarget
         // 
         this.SecondaryTarget.FormattingEnabled = true;
         this.SecondaryTarget.Items.AddRange(new object[] {
            "Any",
            "Armed Ships",
            "Bombers",
            "Freighters",
            "None",
            "Starbase",
            "Unarmed Ships"});
         this.SecondaryTarget.Location = new System.Drawing.Point(9, 128);
         this.SecondaryTarget.Name = "SecondaryTarget";
         this.SecondaryTarget.Size = new System.Drawing.Size(194, 21);
         this.SecondaryTarget.TabIndex = 8;
         // 
         // Attack
         // 
         this.Attack.FormattingEnabled = true;
         this.Attack.Items.AddRange(new object[] {
            "Enemies",
            "Enemies and Neutrals",
            "Everyone"});
         this.Attack.Location = new System.Drawing.Point(9, 223);
         this.Attack.Name = "Attack";
         this.Attack.Size = new System.Drawing.Size(194, 21);
         this.Attack.TabIndex = 7;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(9, 207);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(38, 13);
         this.label4.TabIndex = 6;
         this.label4.Text = "Attack";
         // 
         // Tactic
         // 
         this.Tactic.FormattingEnabled = true;
         this.Tactic.Items.AddRange(new object[] {
            "Disengage",
            "Disengage if Challenged",
            "Maximise Damage",
            "Maximise Damage Ratio",
            "Maximise Net Damage",
            "Minimise Damage to Self"});
         this.Tactic.Location = new System.Drawing.Point(9, 173);
         this.Tactic.Name = "Tactic";
         this.Tactic.Size = new System.Drawing.Size(194, 21);
         this.Tactic.TabIndex = 5;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(9, 157);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(37, 13);
         this.label3.TabIndex = 4;
         this.label3.Text = "Tactic";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(9, 112);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(92, 13);
         this.label2.TabIndex = 2;
         this.label2.Text = "Secondary Target";
         // 
         // PrimaryTarget
         // 
         this.PrimaryTarget.FormattingEnabled = true;
         this.PrimaryTarget.Items.AddRange(new object[] {
            "Any",
            "Armed Ships",
            "Bombers",
            "Freighters",
            "None",
            "Starbase",
            "Unarmed Ships"});
         this.PrimaryTarget.Location = new System.Drawing.Point(9, 78);
         this.PrimaryTarget.Name = "PrimaryTarget";
         this.PrimaryTarget.Size = new System.Drawing.Size(194, 21);
         this.PrimaryTarget.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(9, 61);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(75, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Primary Target";
         // 
         // DoneButton
         // 
         this.DoneButton.Location = new System.Drawing.Point(370, 323);
         this.DoneButton.Name = "DoneButton";
         this.DoneButton.Size = new System.Drawing.Size(75, 23);
         this.DoneButton.TabIndex = 2;
         this.DoneButton.Text = "Done";
         this.DoneButton.UseVisualStyleBackColor = true;
         this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
         // 
         // BattlePlans
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(468, 356);
         this.Controls.Add(this.DoneButton);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "BattlePlans";
         this.Text = "Nova - Battle Plans";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ListBox PlanList;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ComboBox Attack;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox Tactic;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox PrimaryTarget;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox SecondaryTarget;
      private System.Windows.Forms.Button DoneButton;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox PlanName;
      private System.Windows.Forms.Button NewPlan;
      private System.Windows.Forms.Button ModifyPlan;
   }
}