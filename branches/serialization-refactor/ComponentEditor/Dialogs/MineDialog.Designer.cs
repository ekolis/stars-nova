namespace ComponentEditor.Dialogs
{
    partial class MineDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MineDialog));
            this.commonProperties1 = new ComponentEditor.CommonProperties();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DamagePerEngine = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.HitChance = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SafeSpeed = new System.Windows.Forms.NumericUpDown();
            this.MinesLayed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.DoneButton = new System.Windows.Forms.Button();
            this.DamagePerRamScoop = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MinFleetDamage = new System.Windows.Forms.NumericUpDown();
            this.MinRamScoopDamage = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DamagePerEngine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HitChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SafeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinesLayed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DamagePerRamScoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinFleetDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinRamScoopDamage)).BeginInit();
            this.SuspendLayout();
            // 
            // commonProperties1
            // 
            this.commonProperties1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commonProperties1.Location = new System.Drawing.Point(0, 0);
            this.commonProperties1.Name = "commonProperties1";
            this.commonProperties1.Size = new System.Drawing.Size(585, 449);
            this.commonProperties1.TabIndex = 0;
            this.commonProperties1.Value = ((NovaCommon.Component)(resources.GetObject("commonProperties1.Value")));
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MinRamScoopDamage);
            this.groupBox1.Controls.Add(this.MinFleetDamage);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DamagePerRamScoop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.DamagePerEngine);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.HitChance);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SafeSpeed);
            this.groupBox1.Controls.Add(this.MinesLayed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(398, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 226);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mine layer Properties";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Safe Speed";
            // 
            // DamagePerEngine
            // 
            this.DamagePerEngine.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.DamagePerEngine.Location = new System.Drawing.Point(118, 105);
            this.DamagePerEngine.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.DamagePerEngine.Name = "DamagePerEngine";
            this.DamagePerEngine.Size = new System.Drawing.Size(54, 20);
            this.DamagePerEngine.TabIndex = 4;
            this.DamagePerEngine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Damage Per Engine";
            // 
            // HitChance
            // 
            this.HitChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.HitChance.Location = new System.Drawing.Point(118, 79);
            this.HitChance.Name = "HitChance";
            this.HitChance.Size = new System.Drawing.Size(54, 20);
            this.HitChance.TabIndex = 3;
            this.HitChance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HitChance.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hit Chance";
            // 
            // SafeSpeed
            // 
            this.SafeSpeed.Location = new System.Drawing.Point(118, 53);
            this.SafeSpeed.Name = "SafeSpeed";
            this.SafeSpeed.Size = new System.Drawing.Size(54, 20);
            this.SafeSpeed.TabIndex = 2;
            this.SafeSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MinesLayed
            // 
            this.MinesLayed.Location = new System.Drawing.Point(118, 27);
            this.MinesLayed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MinesLayed.Name = "MinesLayed";
            this.MinesLayed.Size = new System.Drawing.Size(54, 20);
            this.MinesLayed.TabIndex = 1;
            this.MinesLayed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mines laid per year";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(395, 420);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(63, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(464, 420);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 23);
            this.DeleteButton.TabIndex = 9;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(530, 420);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(50, 23);
            this.DoneButton.TabIndex = 10;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            // 
            // DamagePerRamScoop
            // 
            this.DamagePerRamScoop.Location = new System.Drawing.Point(118, 131);
            this.DamagePerRamScoop.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.DamagePerRamScoop.Name = "DamagePerRamScoop";
            this.DamagePerRamScoop.Size = new System.Drawing.Size(54, 20);
            this.DamagePerRamScoop.TabIndex = 5;
            this.DamagePerRamScoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Ram Scoop Damage";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Min Damage to Fleet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Min Ram S. Damage";
            // 
            // MinFleetDamage
            // 
            this.MinFleetDamage.Location = new System.Drawing.Point(118, 157);
            this.MinFleetDamage.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.MinFleetDamage.Name = "MinFleetDamage";
            this.MinFleetDamage.Size = new System.Drawing.Size(54, 20);
            this.MinFleetDamage.TabIndex = 6;
            this.MinFleetDamage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MinRamScoopDamage
            // 
            this.MinRamScoopDamage.Location = new System.Drawing.Point(118, 183);
            this.MinRamScoopDamage.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.MinRamScoopDamage.Name = "MinRamScoopDamage";
            this.MinRamScoopDamage.Size = new System.Drawing.Size(54, 20);
            this.MinRamScoopDamage.TabIndex = 7;
            this.MinRamScoopDamage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MineDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 449);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.commonProperties1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MineDialog";
            this.Text = "Nova Mine layer Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DamagePerEngine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HitChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SafeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinesLayed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DamagePerRamScoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinFleetDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinRamScoopDamage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonProperties commonProperties1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown SafeSpeed;
        private System.Windows.Forms.NumericUpDown MinesLayed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown HitChance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DamagePerEngine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DamagePerRamScoop;
        private System.Windows.Forms.NumericUpDown MinRamScoopDamage;
        private System.Windows.Forms.NumericUpDown MinFleetDamage;
        private System.Windows.Forms.Label label7;
    }
}