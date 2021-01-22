namespace ComponentEditor
{
    partial class CargoPodDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargoPodDialog));
            this.commonProperties1 = new Nova.WinForms.ComponentEditor.CommonProperties();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DoneButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Capacity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Capacity)).BeginInit();
            this.SuspendLayout();
            // 
            // commonProperties1
            // 
            this.commonProperties1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commonProperties1.Location = new System.Drawing.Point(0, 0);
            this.commonProperties1.Name = "commonProperties1";
            this.commonProperties1.Size = new System.Drawing.Size(583, 449);
            this.commonProperties1.TabIndex = 0;
            this.commonProperties1.Value = ((Nova.Common.Components.Component)(resources.GetObject("commonProperties1.Value")));
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(398, 379);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(398, 414);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(496, 414);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 3;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Capacity);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(398, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cargo Pod Properties";
            // 
            // Capacity
            // 
            this.Capacity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Capacity.Location = new System.Drawing.Point(98, 20);
            this.Capacity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Capacity.Name = "Capacity";
            this.Capacity.Size = new System.Drawing.Size(54, 20);
            this.Capacity.TabIndex = 1;
            this.Capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Capacity (kT)";
            // 
            // CargoPodDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 449);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.commonProperties1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CargoPodDialog";
            this.Text = "Nova Cargo Pod Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Capacity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.WinForms.ComponentEditor.CommonProperties commonProperties1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown Capacity;
        private System.Windows.Forms.Label label1;
    }
}