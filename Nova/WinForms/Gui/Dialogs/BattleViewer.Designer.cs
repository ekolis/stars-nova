namespace Nova.WinForms.Gui
{
   public partial class BattleViewer
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

      #region Windows Form Designer generated code

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleViewer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.battlePanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stackGroupBox = new System.Windows.Forms.GroupBox();
            this.stackKey = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.topTokenArmor = new System.Windows.Forms.Label();
            this.stackOwner = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stackShields = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.stackDesign = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.damage = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.componentTarget = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.weaponPower = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.stepNumber = new System.Windows.Forms.Label();
            this.nextStep = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.targetArmor = new System.Windows.Forms.Label();
            this.targetShields = new System.Windows.Forms.Label();
            this.targetOwner = new System.Windows.Forms.Label();
            this.targetDesign = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.movedFrom = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.movedTo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.battleLocation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.stackQuantity = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.stackGroupBox.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.battlePanel);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 600);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Battle View";
            // 
            // battlePanel
            // 
            this.battlePanel.BackColor = System.Drawing.Color.Black;
            this.battlePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.battlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.battlePanel.Location = new System.Drawing.Point(3, 16);
            this.battlePanel.Name = "battlePanel";
            this.battlePanel.Size = new System.Drawing.Size(594, 581);
            this.battlePanel.TabIndex = 0;
            this.battlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.stackGroupBox);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.battleLocation);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(625, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(653, 658);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Battle Details";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(19, 49);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(241, 53);
            this.textBox1.TabIndex = 8;
            this.textBox1.Tag = "";
            this.textBox1.Text = "Extra fields added for debugging the combat engine. Will clean up later.";
            // 
            // stackGroupBox
            // 
            this.stackGroupBox.Controls.Add(this.stackQuantity);
            this.stackGroupBox.Controls.Add(this.label16);
            this.stackGroupBox.Controls.Add(this.stackKey);
            this.stackGroupBox.Controls.Add(this.label12);
            this.stackGroupBox.Controls.Add(this.label15);
            this.stackGroupBox.Controls.Add(this.topTokenArmor);
            this.stackGroupBox.Controls.Add(this.stackOwner);
            this.stackGroupBox.Controls.Add(this.label11);
            this.stackGroupBox.Controls.Add(this.label2);
            this.stackGroupBox.Controls.Add(this.stackShields);
            this.stackGroupBox.Controls.Add(this.label13);
            this.stackGroupBox.Controls.Add(this.stackDesign);
            this.stackGroupBox.Location = new System.Drawing.Point(10, 116);
            this.stackGroupBox.Name = "stackGroupBox";
            this.stackGroupBox.Size = new System.Drawing.Size(252, 226);
            this.stackGroupBox.TabIndex = 7;
            this.stackGroupBox.TabStop = false;
            this.stackGroupBox.Text = "Stack";
            // 
            // stackKey
            // 
            this.stackKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackKey.Location = new System.Drawing.Point(105, 35);
            this.stackKey.Name = "stackKey";
            this.stackKey.Size = new System.Drawing.Size(141, 18);
            this.stackKey.TabIndex = 11;
            this.stackKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Stack.Key";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 142);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Total Token Armor";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // topTokenArmor
            // 
            this.topTokenArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topTokenArmor.Location = new System.Drawing.Point(105, 139);
            this.topTokenArmor.Name = "topTokenArmor";
            this.topTokenArmor.Size = new System.Drawing.Size(141, 18);
            this.topTokenArmor.TabIndex = 8;
            this.topTokenArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stackOwner
            // 
            this.stackOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackOwner.Location = new System.Drawing.Point(105, 13);
            this.stackOwner.Name = "stackOwner";
            this.stackOwner.Size = new System.Drawing.Size(141, 18);
            this.stackOwner.TabIndex = 1;
            this.stackOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Stack Shields";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Stack Owner";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stackShields
            // 
            this.stackShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackShields.Location = new System.Drawing.Point(105, 114);
            this.stackShields.Name = "stackShields";
            this.stackShields.Size = new System.Drawing.Size(141, 18);
            this.stackShields.TabIndex = 6;
            this.stackShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Design";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stackDesign
            // 
            this.stackDesign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackDesign.Location = new System.Drawing.Point(105, 88);
            this.stackDesign.Name = "stackDesign";
            this.stackDesign.Size = new System.Drawing.Size(141, 18);
            this.stackDesign.TabIndex = 4;
            this.stackDesign.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.damage);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.componentTarget);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.weaponPower);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(337, 353);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(259, 166);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Weapon Discharge";
            // 
            // damage
            // 
            this.damage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.damage.Location = new System.Drawing.Point(112, 74);
            this.damage.Name = "damage";
            this.damage.Size = new System.Drawing.Size(141, 18);
            this.damage.TabIndex = 13;
            this.damage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // componentTarget
            // 
            this.componentTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.componentTarget.Location = new System.Drawing.Point(112, 49);
            this.componentTarget.Name = "componentTarget";
            this.componentTarget.Size = new System.Drawing.Size(141, 18);
            this.componentTarget.TabIndex = 11;
            this.componentTarget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // weaponPower
            // 
            this.weaponPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponPower.Location = new System.Drawing.Point(112, 25);
            this.weaponPower.Name = "weaponPower";
            this.weaponPower.Size = new System.Drawing.Size(141, 18);
            this.weaponPower.TabIndex = 9;
            this.weaponPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.groupBox5.Controls.Add(this.stepNumber);
            this.groupBox5.Controls.Add(this.nextStep);
            this.groupBox5.Location = new System.Drawing.Point(10, 565);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(259, 87);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Replay Control";
            // 
            // stepNumber
            // 
            this.stepNumber.Location = new System.Drawing.Point(11, 20);
            this.stepNumber.Name = "stepNumber";
            this.stepNumber.Size = new System.Drawing.Size(100, 23);
            this.stepNumber.TabIndex = 3;
            this.stepNumber.Text = "Step 1 of 10";
            // 
            // nextStep
            // 
            this.nextStep.Location = new System.Drawing.Point(11, 58);
            this.nextStep.Name = "nextStep";
            this.nextStep.Size = new System.Drawing.Size(75, 23);
            this.nextStep.TabIndex = 2;
            this.nextStep.Text = "Next";
            this.nextStep.UseVisualStyleBackColor = true;
            this.nextStep.Click += new System.EventHandler(this.NextStep_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.targetArmor);
            this.groupBox4.Controls.Add(this.targetShields);
            this.groupBox4.Controls.Add(this.targetOwner);
            this.groupBox4.Controls.Add(this.targetDesign);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(10, 364);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 129);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Weapons Target";
            // 
            // targetArmor
            // 
            this.targetArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.targetArmor.Location = new System.Drawing.Point(112, 98);
            this.targetArmor.Name = "targetArmor";
            this.targetArmor.Size = new System.Drawing.Size(141, 18);
            this.targetArmor.TabIndex = 8;
            this.targetArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetShields
            // 
            this.targetShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.targetShields.Location = new System.Drawing.Point(112, 74);
            this.targetShields.Name = "targetShields";
            this.targetShields.Size = new System.Drawing.Size(141, 18);
            this.targetShields.TabIndex = 7;
            this.targetShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetOwner
            // 
            this.targetOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.targetOwner.Location = new System.Drawing.Point(112, 50);
            this.targetOwner.Name = "targetOwner";
            this.targetOwner.Size = new System.Drawing.Size(141, 18);
            this.targetOwner.TabIndex = 6;
            this.targetOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetDesign
            // 
            this.targetDesign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.targetDesign.Location = new System.Drawing.Point(112, 26);
            this.targetDesign.Name = "targetDesign";
            this.targetDesign.Size = new System.Drawing.Size(141, 18);
            this.targetDesign.TabIndex = 5;
            this.targetDesign.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Ship Design";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
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
            this.groupBox3.Controls.Add(this.movedFrom);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.movedTo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(337, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 111);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Movement";
            // 
            // movedFrom
            // 
            this.movedFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.movedFrom.Location = new System.Drawing.Point(112, 40);
            this.movedFrom.Name = "movedFrom";
            this.movedFrom.Size = new System.Drawing.Size(141, 18);
            this.movedFrom.TabIndex = 5;
            this.movedFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Moved From";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // movedTo
            // 
            this.movedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.movedTo.Location = new System.Drawing.Point(112, 70);
            this.movedTo.Name = "movedTo";
            this.movedTo.Size = new System.Drawing.Size(141, 18);
            this.movedTo.TabIndex = 3;
            this.movedTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Moved To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // battleLocation
            // 
            this.battleLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.battleLocation.Location = new System.Drawing.Point(102, 21);
            this.battleLocation.Name = "battleLocation";
            this.battleLocation.Size = new System.Drawing.Size(545, 13);
            this.battleLocation.TabIndex = 1;
            this.battleLocation.Text = "Location";
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
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 65);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Quantity";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stackQuantity
            // 
            this.stackQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackQuantity.Location = new System.Drawing.Point(105, 62);
            this.stackQuantity.Name = "stackQuantity";
            this.stackQuantity.Size = new System.Drawing.Size(141, 18);
            this.stackQuantity.TabIndex = 13;
            this.stackQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BattleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 677);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BattleViewer";
            this.Text = "Stars! Nova - Battle Viewer";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.stackGroupBox.ResumeLayout(false);
            this.stackGroupBox.PerformLayout();
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
      private System.Windows.Forms.Panel battlePanel;
      private System.Windows.Forms.GroupBox groupBox2;
       private System.Windows.Forms.Label battleLocation;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Button nextStep;
       private System.Windows.Forms.GroupBox groupBox3;
       private System.Windows.Forms.Label movedTo;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label stackOwner;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.GroupBox groupBox4;
       private System.Windows.Forms.Label targetArmor;
       private System.Windows.Forms.Label targetShields;
       private System.Windows.Forms.Label targetOwner;
       private System.Windows.Forms.Label targetDesign;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.GroupBox groupBox5;
       private System.Windows.Forms.Label stepNumber;
       private System.Windows.Forms.GroupBox groupBox6;
       private System.Windows.Forms.Label weaponPower;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label componentTarget;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.Label damage;
       private System.Windows.Forms.Label label10;
       private System.Windows.Forms.GroupBox stackGroupBox;
       private System.Windows.Forms.Label label15;
       private System.Windows.Forms.Label topTokenArmor;
       private System.Windows.Forms.Label label11;
       private System.Windows.Forms.Label stackShields;
       private System.Windows.Forms.Label label13;
       private System.Windows.Forms.Label stackDesign;
       private System.Windows.Forms.TextBox textBox1;
       private System.Windows.Forms.Label stackKey;
       private System.Windows.Forms.Label label12;
       private System.Windows.Forms.Label movedFrom;
       private System.Windows.Forms.Label label14;
       private System.Windows.Forms.Label stackQuantity;
       private System.Windows.Forms.Label label16;
   }
}