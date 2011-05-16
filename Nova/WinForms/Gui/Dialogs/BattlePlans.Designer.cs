namespace Nova.WinForms.Gui
{
   public partial class BattlePlans
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

      #region Windows Form Designer generated code

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.planList = new System.Windows.Forms.ListBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.modifyPlan = new System.Windows.Forms.Button();
         this.newPlan = new System.Windows.Forms.Button();
         this.label5 = new System.Windows.Forms.Label();
         this.planName = new System.Windows.Forms.TextBox();
         this.secondaryTarget = new System.Windows.Forms.ComboBox();
         this.attack = new System.Windows.Forms.ComboBox();
         this.label4 = new System.Windows.Forms.Label();
         this.tactic = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.primaryTarget = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.doneButton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.planList);
         this.groupBox1.Location = new System.Drawing.Point(13, 8);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(216, 298);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Available Plans";
         // 
         // PlanList
         // 
         this.planList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.planList.FormattingEnabled = true;
         this.planList.Location = new System.Drawing.Point(3, 16);
         this.planList.Name = "planList";
         this.planList.Size = new System.Drawing.Size(210, 277);
         this.planList.TabIndex = 0;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.modifyPlan);
         this.groupBox2.Controls.Add(this.newPlan);
         this.groupBox2.Controls.Add(this.label5);
         this.groupBox2.Controls.Add(this.planName);
         this.groupBox2.Controls.Add(this.secondaryTarget);
         this.groupBox2.Controls.Add(this.attack);
         this.groupBox2.Controls.Add(this.label4);
         this.groupBox2.Controls.Add(this.tactic);
         this.groupBox2.Controls.Add(this.label3);
         this.groupBox2.Controls.Add(this.label2);
         this.groupBox2.Controls.Add(this.primaryTarget);
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
         this.modifyPlan.Enabled = false;
         this.modifyPlan.Location = new System.Drawing.Point(128, 265);
         this.modifyPlan.Name = "modifyPlan";
         this.modifyPlan.Size = new System.Drawing.Size(75, 23);
         this.modifyPlan.TabIndex = 10;
         this.modifyPlan.Text = "Modify";
         this.modifyPlan.UseVisualStyleBackColor = true;
         // 
         // NewPlan
         // 
         this.newPlan.Enabled = false;
         this.newPlan.Location = new System.Drawing.Point(12, 265);
         this.newPlan.Name = "newPlan";
         this.newPlan.Size = new System.Drawing.Size(75, 23);
         this.newPlan.TabIndex = 0;
         this.newPlan.Text = "New";
         this.newPlan.UseVisualStyleBackColor = true;
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
         this.planName.Location = new System.Drawing.Point(9, 34);
         this.planName.Name = "planName";
         this.planName.Size = new System.Drawing.Size(194, 20);
         this.planName.TabIndex = 1;
         // 
         // SecondaryTarget
         // 
         this.secondaryTarget.FormattingEnabled = true;
         this.secondaryTarget.Items.AddRange(new object[] {
            "Any",
            "Armed Ships",
            "Bombers",
            "Freighters",
            "None",
            "Starbase",
            "Unarmed Ships"});
         this.secondaryTarget.Location = new System.Drawing.Point(9, 128);
         this.secondaryTarget.Name = "secondaryTarget";
         this.secondaryTarget.Size = new System.Drawing.Size(194, 21);
         this.secondaryTarget.TabIndex = 8;
         // 
         // Attack
         // 
         this.attack.FormattingEnabled = true;
         this.attack.Items.AddRange(new object[] {
            "Enemies",
            "Enemies and Neutrals",
            "Everyone"});
         this.attack.Location = new System.Drawing.Point(9, 223);
         this.attack.Name = "attack";
         this.attack.Size = new System.Drawing.Size(194, 21);
         this.attack.TabIndex = 7;
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
         this.tactic.FormattingEnabled = true;
         this.tactic.Items.AddRange(new object[] {
            "Disengage",
            "Disengage if Challenged",
            "Maximise Damage",
            "Maximise Damage Ratio",
            "Maximise Net Damage",
            "Minimise Damage to Self"});
         this.tactic.Location = new System.Drawing.Point(9, 173);
         this.tactic.Name = "tactic";
         this.tactic.Size = new System.Drawing.Size(194, 21);
         this.tactic.TabIndex = 5;
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
         this.primaryTarget.FormattingEnabled = true;
         this.primaryTarget.Items.AddRange(new object[] {
            "Any",
            "Armed Ships",
            "Bombers",
            "Freighters",
            "None",
            "Starbase",
            "Unarmed Ships"});
         this.primaryTarget.Location = new System.Drawing.Point(9, 78);
         this.primaryTarget.Name = "primaryTarget";
         this.primaryTarget.Size = new System.Drawing.Size(194, 21);
         this.primaryTarget.TabIndex = 1;
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
         this.doneButton.Location = new System.Drawing.Point(370, 323);
         this.doneButton.Name = "doneButton";
         this.doneButton.Size = new System.Drawing.Size(75, 23);
         this.doneButton.TabIndex = 2;
         this.doneButton.Text = "Done";
         this.doneButton.UseVisualStyleBackColor = true;
         this.doneButton.Click += new System.EventHandler(this.DoneButton_Click);
         // 
         // BattlePlans
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(468, 356);
         this.Controls.Add(this.doneButton);
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
      private System.Windows.Forms.ListBox planList;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ComboBox attack;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox tactic;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox primaryTarget;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox secondaryTarget;
      private System.Windows.Forms.Button doneButton;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox planName;
      private System.Windows.Forms.Button newPlan;
      private System.Windows.Forms.Button modifyPlan;
   }
}