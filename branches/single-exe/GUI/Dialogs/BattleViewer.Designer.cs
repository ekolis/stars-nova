namespace Nova
{
   partial class BattleViewer
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleViewer));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.BattlePanel = new System.Windows.Forms.Panel();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.groupBox6 = new System.Windows.Forms.GroupBox();
          this.WeaponPower = new System.Windows.Forms.Label();
          this.label8 = new System.Windows.Forms.Label();
          this.groupBox5 = new System.Windows.Forms.GroupBox();
          this.StepNumber = new System.Windows.Forms.Label();
          this.NextStep = new System.Windows.Forms.Button();
          this.groupBox4 = new System.Windows.Forms.GroupBox();
          this.TargetArmor = new System.Windows.Forms.Label();
          this.TargetShields = new System.Windows.Forms.Label();
          this.TargetOwner = new System.Windows.Forms.Label();
          this.TargetName = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.label6 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label3 = new System.Windows.Forms.Label();
          this.groupBox3 = new System.Windows.Forms.GroupBox();
          this.MovedTo = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.StackOwner = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.BattleLocation = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.ComponentTarget = new System.Windows.Forms.Label();
          this.label10 = new System.Windows.Forms.Label();
          this.Damage = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          this.groupBox6.SuspendLayout();
          this.groupBox5.SuspendLayout();
          this.groupBox4.SuspendLayout();
          this.groupBox3.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.BattlePanel);
          this.groupBox1.Location = new System.Drawing.Point(13, 13);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(600, 600);
          this.groupBox1.TabIndex = 0;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Battle View";
          // 
          // BattlePanel
          // 
          this.BattlePanel.BackColor = System.Drawing.Color.Black;
          this.BattlePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.BattlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
          this.BattlePanel.Location = new System.Drawing.Point(3, 16);
          this.BattlePanel.Name = "BattlePanel";
          this.BattlePanel.Size = new System.Drawing.Size(594, 581);
          this.BattlePanel.TabIndex = 0;
          this.BattlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
          // 
          // groupBox2
          // 
          this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.groupBox2.Controls.Add(this.groupBox6);
          this.groupBox2.Controls.Add(this.groupBox5);
          this.groupBox2.Controls.Add(this.groupBox4);
          this.groupBox2.Controls.Add(this.groupBox3);
          this.groupBox2.Controls.Add(this.BattleLocation);
          this.groupBox2.Controls.Add(this.label1);
          this.groupBox2.Location = new System.Drawing.Point(625, 13);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(275, 600);
          this.groupBox2.TabIndex = 1;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Battle Details";
          // 
          // groupBox6
          // 
          this.groupBox6.Controls.Add(this.Damage);
          this.groupBox6.Controls.Add(this.label10);
          this.groupBox6.Controls.Add(this.ComponentTarget);
          this.groupBox6.Controls.Add(this.label9);
          this.groupBox6.Controls.Add(this.WeaponPower);
          this.groupBox6.Controls.Add(this.label8);
          this.groupBox6.Location = new System.Drawing.Point(11, 258);
          this.groupBox6.Name = "groupBox6";
          this.groupBox6.Size = new System.Drawing.Size(259, 104);
          this.groupBox6.TabIndex = 6;
          this.groupBox6.TabStop = false;
          this.groupBox6.Text = "Weapon Discharge";
          // 
          // WeaponPower
          // 
          this.WeaponPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.WeaponPower.Location = new System.Drawing.Point(112, 25);
          this.WeaponPower.Name = "WeaponPower";
          this.WeaponPower.Size = new System.Drawing.Size(141, 18);
          this.WeaponPower.TabIndex = 9;
          this.WeaponPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label8
          // 
          this.label8.AutoSize = true;
          this.label8.Location = new System.Drawing.Point(11, 28);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(81, 13);
          this.label8.TabIndex = 5;
          this.label8.Text = "Weapon Power";
          this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // groupBox5
          // 
          this.groupBox5.Controls.Add(this.StepNumber);
          this.groupBox5.Controls.Add(this.NextStep);
          this.groupBox5.Location = new System.Drawing.Point(11, 509);
          this.groupBox5.Name = "groupBox5";
          this.groupBox5.Size = new System.Drawing.Size(259, 87);
          this.groupBox5.TabIndex = 5;
          this.groupBox5.TabStop = false;
          this.groupBox5.Text = "Replay Control";
          // 
          // StepNumber
          // 
          this.StepNumber.Location = new System.Drawing.Point(11, 20);
          this.StepNumber.Name = "StepNumber";
          this.StepNumber.Size = new System.Drawing.Size(100, 23);
          this.StepNumber.TabIndex = 3;
          this.StepNumber.Text = "Step 1 of 10";
          // 
          // NextStep
          // 
          this.NextStep.Location = new System.Drawing.Point(11, 58);
          this.NextStep.Name = "NextStep";
          this.NextStep.Size = new System.Drawing.Size(75, 23);
          this.NextStep.TabIndex = 2;
          this.NextStep.Text = "Next";
          this.NextStep.UseVisualStyleBackColor = true;
          this.NextStep.Click += new System.EventHandler(this.NextStep_Click);
          // 
          // groupBox4
          // 
          this.groupBox4.Controls.Add(this.TargetArmor);
          this.groupBox4.Controls.Add(this.TargetShields);
          this.groupBox4.Controls.Add(this.TargetOwner);
          this.groupBox4.Controls.Add(this.TargetName);
          this.groupBox4.Controls.Add(this.label7);
          this.groupBox4.Controls.Add(this.label6);
          this.groupBox4.Controls.Add(this.label5);
          this.groupBox4.Controls.Add(this.label3);
          this.groupBox4.Location = new System.Drawing.Point(10, 122);
          this.groupBox4.Name = "groupBox4";
          this.groupBox4.Size = new System.Drawing.Size(259, 129);
          this.groupBox4.TabIndex = 4;
          this.groupBox4.TabStop = false;
          this.groupBox4.Text = "Weapons Target";
          // 
          // TargetArmor
          // 
          this.TargetArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.TargetArmor.Location = new System.Drawing.Point(112, 98);
          this.TargetArmor.Name = "TargetArmor";
          this.TargetArmor.Size = new System.Drawing.Size(141, 18);
          this.TargetArmor.TabIndex = 8;
          this.TargetArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // TargetShields
          // 
          this.TargetShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.TargetShields.Location = new System.Drawing.Point(112, 74);
          this.TargetShields.Name = "TargetShields";
          this.TargetShields.Size = new System.Drawing.Size(141, 18);
          this.TargetShields.TabIndex = 7;
          this.TargetShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // TargetOwner
          // 
          this.TargetOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.TargetOwner.Location = new System.Drawing.Point(112, 50);
          this.TargetOwner.Name = "TargetOwner";
          this.TargetOwner.Size = new System.Drawing.Size(141, 18);
          this.TargetOwner.TabIndex = 6;
          this.TargetOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // TargetName
          // 
          this.TargetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.TargetName.Location = new System.Drawing.Point(112, 26);
          this.TargetName.Name = "TargetName";
          this.TargetName.Size = new System.Drawing.Size(141, 18);
          this.TargetName.TabIndex = 5;
          this.TargetName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(9, 29);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(59, 13);
          this.label7.TabIndex = 4;
          this.label7.Text = "Ship Name";
          this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(9, 101);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(40, 13);
          this.label6.TabIndex = 3;
          this.label6.Text = "Armor";
          this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.Location = new System.Drawing.Point(9, 77);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(41, 13);
          this.label5.TabIndex = 2;
          this.label5.Text = "Shields";
          this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(9, 53);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(62, 13);
          this.label3.TabIndex = 1;
          this.label3.Text = "Ship Owner";
          this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // groupBox3
          // 
          this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.groupBox3.Controls.Add(this.MovedTo);
          this.groupBox3.Controls.Add(this.label4);
          this.groupBox3.Controls.Add(this.StackOwner);
          this.groupBox3.Controls.Add(this.label2);
          this.groupBox3.Location = new System.Drawing.Point(10, 49);
          this.groupBox3.Name = "groupBox3";
          this.groupBox3.Size = new System.Drawing.Size(259, 66);
          this.groupBox3.TabIndex = 3;
          this.groupBox3.TabStop = false;
          this.groupBox3.Text = "Movement";
          // 
          // MovedTo
          // 
          this.MovedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.MovedTo.Location = new System.Drawing.Point(112, 37);
          this.MovedTo.Name = "MovedTo";
          this.MovedTo.Size = new System.Drawing.Size(141, 18);
          this.MovedTo.TabIndex = 3;
          this.MovedTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Location = new System.Drawing.Point(8, 40);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(56, 13);
          this.label4.TabIndex = 2;
          this.label4.Text = "Moved To";
          this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // StackOwner
          // 
          this.StackOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.StackOwner.Location = new System.Drawing.Point(112, 16);
          this.StackOwner.Name = "StackOwner";
          this.StackOwner.Size = new System.Drawing.Size(141, 18);
          this.StackOwner.TabIndex = 1;
          this.StackOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(7, 19);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(69, 13);
          this.label2.TabIndex = 0;
          this.label2.Text = "Stack Owner";
          this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // BattleLocation
          // 
          this.BattleLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.BattleLocation.Location = new System.Drawing.Point(102, 21);
          this.BattleLocation.Name = "BattleLocation";
          this.BattleLocation.Size = new System.Drawing.Size(167, 13);
          this.BattleLocation.TabIndex = 1;
          this.BattleLocation.Text = "Location";
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(7, 21);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(81, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Battle Location:";
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Location = new System.Drawing.Point(11, 50);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(95, 13);
          this.label9.TabIndex = 10;
          this.label9.Text = "Component Target";
          this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // ComponentTarget
          // 
          this.ComponentTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.ComponentTarget.Location = new System.Drawing.Point(112, 49);
          this.ComponentTarget.Name = "ComponentTarget";
          this.ComponentTarget.Size = new System.Drawing.Size(141, 18);
          this.ComponentTarget.TabIndex = 11;
          this.ComponentTarget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Location = new System.Drawing.Point(11, 77);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(47, 13);
          this.label10.TabIndex = 12;
          this.label10.Text = "Damage";
          this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // Damage
          // 
          this.Damage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.Damage.Location = new System.Drawing.Point(112, 74);
          this.Damage.Name = "Damage";
          this.Damage.Size = new System.Drawing.Size(141, 18);
          this.Damage.TabIndex = 13;
          this.Damage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // BattleViewer
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(908, 619);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.groupBox1);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "BattleViewer";
          this.Text = "Nova Battle Viewer";
          this.Load += new System.EventHandler(this.OnLoad);
          this.groupBox1.ResumeLayout(false);
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          this.groupBox6.ResumeLayout(false);
          this.groupBox6.PerformLayout();
          this.groupBox5.ResumeLayout(false);
          this.groupBox4.ResumeLayout(false);
          this.groupBox4.PerformLayout();
          this.groupBox3.ResumeLayout(false);
          this.groupBox3.PerformLayout();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Panel BattlePanel;
      private System.Windows.Forms.GroupBox groupBox2;
       private System.Windows.Forms.Label BattleLocation;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Button NextStep;
       private System.Windows.Forms.GroupBox groupBox3;
       private System.Windows.Forms.Label MovedTo;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label StackOwner;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.GroupBox groupBox4;
       private System.Windows.Forms.Label TargetArmor;
       private System.Windows.Forms.Label TargetShields;
       private System.Windows.Forms.Label TargetOwner;
       private System.Windows.Forms.Label TargetName;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.GroupBox groupBox5;
       private System.Windows.Forms.Label StepNumber;
       private System.Windows.Forms.GroupBox groupBox6;
       private System.Windows.Forms.Label WeaponPower;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label ComponentTarget;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.Label Damage;
       private System.Windows.Forms.Label label10;
   }
}