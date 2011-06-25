namespace Nova.WinForms.Gui.Controls
{
    partial class CargoMeterCounter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numeric = new System.Windows.Forms.NumericUpDown();
            this.meterCargo = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.meterCargo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numeric, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(223, 20);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // numeric
            // 
            this.numeric.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numeric.AutoSize = true;
            this.numeric.Location = new System.Drawing.Point(5, 0);
            this.numeric.Margin = new System.Windows.Forms.Padding(0);
            this.numeric.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numeric.Name = "numeric";
            this.numeric.Size = new System.Drawing.Size(53, 20);
            this.numeric.TabIndex = 1;
            this.numeric.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            // 
            // meterCargo
            // 
            this.meterCargo.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.meterCargo.CargoLevels = null;
            this.meterCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meterCargo.Location = new System.Drawing.Point(61, 3);
            this.meterCargo.Name = "meterCargo";
            this.meterCargo.Size = new System.Drawing.Size(159, 14);
            this.meterCargo.TabIndex = 0;
            this.meterCargo.Text = "cargoMeter1";
            this.meterCargo.UserCanChangeValue = true;
            // 
            // CargoMeterCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CargoMeterCounter";
            this.Size = new System.Drawing.Size(223, 20);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CargoMeter meterCargo;
        private System.Windows.Forms.NumericUpDown numeric;
    }
}
