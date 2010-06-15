namespace Nova.WinForms.Gui
{
   public partial class PlayerRelations
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
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.RaceList = new System.Windows.Forms.ListBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.EnemyButton = new System.Windows.Forms.RadioButton();
         this.NeutralButton = new System.Windows.Forms.RadioButton();
         this.FriendButton = new System.Windows.Forms.RadioButton();
         this.DoneBUtton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.RaceList);
         this.groupBox1.Location = new System.Drawing.Point(8, 3);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(174, 182);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Players";
         // 
         // RaceList
         // 
         this.RaceList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.RaceList.FormattingEnabled = true;
         this.RaceList.Location = new System.Drawing.Point(3, 16);
         this.RaceList.Name = "RaceList";
         this.RaceList.Size = new System.Drawing.Size(168, 160);
         this.RaceList.Sorted = true;
         this.RaceList.TabIndex = 0;
         this.RaceList.SelectedIndexChanged += new System.EventHandler(this.SelectedRaceChanged);
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.EnemyButton);
         this.groupBox2.Controls.Add(this.NeutralButton);
         this.groupBox2.Controls.Add(this.FriendButton);
         this.groupBox2.Location = new System.Drawing.Point(189, 3);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(93, 94);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Relation";
         // 
         // EnemyButton
         // 
         this.EnemyButton.AutoSize = true;
         this.EnemyButton.Location = new System.Drawing.Point(7, 64);
         this.EnemyButton.Name = "EnemyButton";
         this.EnemyButton.Size = new System.Drawing.Size(57, 17);
         this.EnemyButton.TabIndex = 2;
         this.EnemyButton.TabStop = true;
         this.EnemyButton.Text = "Enemy";
         this.EnemyButton.UseVisualStyleBackColor = true;
         this.EnemyButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // NeutralButton
         // 
         this.NeutralButton.AutoSize = true;
         this.NeutralButton.Location = new System.Drawing.Point(7, 40);
         this.NeutralButton.Name = "NeutralButton";
         this.NeutralButton.Size = new System.Drawing.Size(59, 17);
         this.NeutralButton.TabIndex = 1;
         this.NeutralButton.TabStop = true;
         this.NeutralButton.Text = "Neutral";
         this.NeutralButton.UseVisualStyleBackColor = true;
         this.NeutralButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // FriendButton
         // 
         this.FriendButton.AutoSize = true;
         this.FriendButton.Location = new System.Drawing.Point(7, 16);
         this.FriendButton.Name = "FriendButton";
         this.FriendButton.Size = new System.Drawing.Size(54, 17);
         this.FriendButton.TabIndex = 0;
         this.FriendButton.TabStop = true;
         this.FriendButton.Text = "Friend";
         this.FriendButton.UseVisualStyleBackColor = true;
         this.FriendButton.CheckedChanged += new System.EventHandler(this.RelationChanged);
         // 
         // DoneBUtton
         // 
         this.DoneBUtton.Location = new System.Drawing.Point(207, 162);
         this.DoneBUtton.Name = "DoneBUtton";
         this.DoneBUtton.Size = new System.Drawing.Size(75, 23);
         this.DoneBUtton.TabIndex = 2;
         this.DoneBUtton.Text = "Done";
         this.DoneBUtton.UseVisualStyleBackColor = true;
         this.DoneBUtton.Click += new System.EventHandler(this.DoneBUtton_Click);
         // 
         // PlayerRelations
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(290, 191);
         this.Controls.Add(this.DoneBUtton);
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
      private System.Windows.Forms.ListBox RaceList;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.RadioButton EnemyButton;
      private System.Windows.Forms.RadioButton NeutralButton;
      private System.Windows.Forms.RadioButton FriendButton;
      private System.Windows.Forms.Button DoneBUtton;
   }
}