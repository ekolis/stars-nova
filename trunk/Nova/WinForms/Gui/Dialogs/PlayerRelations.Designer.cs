namespace Nova.WinForms.Gui
{
   public partial class PlayerRelations
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
         this.empireList = new System.Windows.Forms.ListBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.enemyButton = new System.Windows.Forms.RadioButton();
         this.neutralButton = new System.Windows.Forms.RadioButton();
         this.friendButton = new System.Windows.Forms.RadioButton();
         this.doneButton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.empireList);
         this.groupBox1.Location = new System.Drawing.Point(8, 3);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(174, 182);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Players";
         // 
         // RaceList
         // 
         this.empireList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.empireList.FormattingEnabled = true;
         this.empireList.Location = new System.Drawing.Point(3, 16);
         this.empireList.Name = "raceList";
         this.empireList.Size = new System.Drawing.Size(168, 160);
         this.empireList.Sorted = true;
         this.empireList.TabIndex = 0;
         this.empireList.SelectedIndexChanged += new System.EventHandler(this.SelectedRaceChanged);
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.enemyButton);
         this.groupBox2.Controls.Add(this.neutralButton);
         this.groupBox2.Controls.Add(this.friendButton);
         this.groupBox2.Location = new System.Drawing.Point(189, 3);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(93, 94);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Relation";
         // 
         // EnemyButton
         // 
         this.enemyButton.AutoSize = true;
         this.enemyButton.Location = new System.Drawing.Point(7, 64);
         this.enemyButton.Name = "enemyButton";
         this.enemyButton.Size = new System.Drawing.Size(57, 17);
         this.enemyButton.TabIndex = 2;
         this.enemyButton.TabStop = true;
         this.enemyButton.Text = "Enemy";
         this.enemyButton.UseVisualStyleBackColor = true;
         this.enemyButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // NeutralButton
         // 
         this.neutralButton.AutoSize = true;
         this.neutralButton.Location = new System.Drawing.Point(7, 40);
         this.neutralButton.Name = "neutralButton";
         this.neutralButton.Size = new System.Drawing.Size(59, 17);
         this.neutralButton.TabIndex = 1;
         this.neutralButton.TabStop = true;
         this.neutralButton.Text = "Neutral";
         this.neutralButton.UseVisualStyleBackColor = true;
         this.neutralButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // FriendButton
         // 
         this.friendButton.AutoSize = true;
         this.friendButton.Location = new System.Drawing.Point(7, 16);
         this.friendButton.Name = "friendButton";
         this.friendButton.Size = new System.Drawing.Size(54, 17);
         this.friendButton.TabIndex = 0;
         this.friendButton.TabStop = true;
         this.friendButton.Text = "Friend";
         this.friendButton.UseVisualStyleBackColor = true;
         this.friendButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // DoneBUtton
         // 
         this.doneButton.Location = new System.Drawing.Point(207, 162);
         this.doneButton.Name = "doneButton";
         this.doneButton.Size = new System.Drawing.Size(75, 23);
         this.doneButton.TabIndex = 2;
         this.doneButton.Text = "Done";
         this.doneButton.UseVisualStyleBackColor = true;
         this.doneButton.Click += new System.EventHandler(this.DoneBUtton_Click);
         // 
         // PlayerRelations
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(290, 191);
         this.Controls.Add(this.doneButton);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "PlayerRelations";
         this.Text = "Nova - Player Relations";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ListBox empireList;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.RadioButton enemyButton;
      private System.Windows.Forms.RadioButton neutralButton;
      private System.Windows.Forms.RadioButton friendButton;
      private System.Windows.Forms.Button doneButton;
   }
}