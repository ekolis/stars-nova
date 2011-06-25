namespace Nova.WinForms.Gui.Dialogs
{
    partial class CargoTransferDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargoTransferDialog));
            this.labelFleet1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.labelFleet2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cargoIronRight = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoIronLeft = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoBoraniumLeft = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoBoraniumRight = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoGermaniumLeft = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoGermaniumRight = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoColonistsLeft = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoColonistsRight = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.fuelLeft = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.fuelRight = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.cargoMeterLeft = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.cargoMeterRight = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.SuspendLayout();
            // 
            // labelFleet1
            // 
            this.labelFleet1.Location = new System.Drawing.Point(29, 19);
            this.labelFleet1.Name = "labelFleet1";
            this.labelFleet1.Size = new System.Drawing.Size(108, 32);
            this.labelFleet1.TabIndex = 25;
            this.labelFleet1.Text = "Fleet #1";
            this.labelFleet1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(366, 253);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 20;
            this.okButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(447, 253);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancel";
            // 
            // labelFleet2
            // 
            this.labelFleet2.Location = new System.Drawing.Point(372, 19);
            this.labelFleet2.Name = "labelFleet2";
            this.labelFleet2.Size = new System.Drawing.Size(108, 32);
            this.labelFleet2.TabIndex = 29;
            this.labelFleet2.Text = "Fleet #2";
            this.labelFleet2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Ironium";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Boranium";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Germanium";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Colonists";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Fuel";
            // 
            // cargoIronRight
            // 
            this.cargoIronRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Ironium;
            this.cargoIronRight.Location = new System.Drawing.Point(291, 54);
            this.cargoIronRight.Maximum = 100;
            this.cargoIronRight.Name = "cargoIronRight";
            this.cargoIronRight.Reversed = false;
            this.cargoIronRight.Size = new System.Drawing.Size(230, 20);
            this.cargoIronRight.TabIndex = 35;
            this.cargoIronRight.Value = 0;
            // 
            // cargoIronLeft
            // 
            this.cargoIronLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Ironium;
            this.cargoIronLeft.Location = new System.Drawing.Point(9, 54);
            this.cargoIronLeft.Maximum = 100;
            this.cargoIronLeft.Name = "cargoIronLeft";
            this.cargoIronLeft.Reversed = true;
            this.cargoIronLeft.Size = new System.Drawing.Size(223, 20);
            this.cargoIronLeft.TabIndex = 36;
            this.cargoIronLeft.Value = 0;
            // 
            // cargoBoraniumLeft
            // 
            this.cargoBoraniumLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Boranium;
            this.cargoBoraniumLeft.Location = new System.Drawing.Point(9, 80);
            this.cargoBoraniumLeft.Maximum = 100;
            this.cargoBoraniumLeft.Name = "cargoBoraniumLeft";
            this.cargoBoraniumLeft.Reversed = true;
            this.cargoBoraniumLeft.Size = new System.Drawing.Size(223, 20);
            this.cargoBoraniumLeft.TabIndex = 38;
            this.cargoBoraniumLeft.Value = 0;
            // 
            // cargoBoraniumRight
            // 
            this.cargoBoraniumRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Boranium;
            this.cargoBoraniumRight.Location = new System.Drawing.Point(291, 80);
            this.cargoBoraniumRight.Maximum = 100;
            this.cargoBoraniumRight.Name = "cargoBoraniumRight";
            this.cargoBoraniumRight.Reversed = false;
            this.cargoBoraniumRight.Size = new System.Drawing.Size(230, 20);
            this.cargoBoraniumRight.TabIndex = 37;
            this.cargoBoraniumRight.Value = 0;
            // 
            // cargoGermaniumLeft
            // 
            this.cargoGermaniumLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Germanium;
            this.cargoGermaniumLeft.Location = new System.Drawing.Point(9, 106);
            this.cargoGermaniumLeft.Maximum = 100;
            this.cargoGermaniumLeft.Name = "cargoGermaniumLeft";
            this.cargoGermaniumLeft.Reversed = true;
            this.cargoGermaniumLeft.Size = new System.Drawing.Size(223, 20);
            this.cargoGermaniumLeft.TabIndex = 40;
            this.cargoGermaniumLeft.Value = 0;
            // 
            // cargoGermaniumRight
            // 
            this.cargoGermaniumRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Germanium;
            this.cargoGermaniumRight.Location = new System.Drawing.Point(291, 105);
            this.cargoGermaniumRight.Maximum = 100;
            this.cargoGermaniumRight.Name = "cargoGermaniumRight";
            this.cargoGermaniumRight.Reversed = false;
            this.cargoGermaniumRight.Size = new System.Drawing.Size(230, 20);
            this.cargoGermaniumRight.TabIndex = 39;
            this.cargoGermaniumRight.Value = 0;
            // 
            // cargoColonistsLeft
            // 
            this.cargoColonistsLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Colonists;
            this.cargoColonistsLeft.Location = new System.Drawing.Point(9, 132);
            this.cargoColonistsLeft.Maximum = 100;
            this.cargoColonistsLeft.Name = "cargoColonistsLeft";
            this.cargoColonistsLeft.Reversed = true;
            this.cargoColonistsLeft.Size = new System.Drawing.Size(223, 20);
            this.cargoColonistsLeft.TabIndex = 42;
            this.cargoColonistsLeft.Value = 0;
            // 
            // cargoColonistsRight
            // 
            this.cargoColonistsRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Colonists;
            this.cargoColonistsRight.Location = new System.Drawing.Point(291, 131);
            this.cargoColonistsRight.Maximum = 100;
            this.cargoColonistsRight.Name = "cargoColonistsRight";
            this.cargoColonistsRight.Reversed = false;
            this.cargoColonistsRight.Size = new System.Drawing.Size(231, 20);
            this.cargoColonistsRight.TabIndex = 41;
            this.cargoColonistsRight.Value = 0;
            // 
            // fuelLeft
            // 
            this.fuelLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.fuelLeft.Location = new System.Drawing.Point(9, 157);
            this.fuelLeft.Maximum = 100;
            this.fuelLeft.Name = "fuelLeft";
            this.fuelLeft.Reversed = true;
            this.fuelLeft.Size = new System.Drawing.Size(223, 20);
            this.fuelLeft.TabIndex = 44;
            this.fuelLeft.Value = 0;
            // 
            // fuelRight
            // 
            this.fuelRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.fuelRight.Location = new System.Drawing.Point(291, 157);
            this.fuelRight.Maximum = 100;
            this.fuelRight.Name = "fuelRight";
            this.fuelRight.Reversed = false;
            this.fuelRight.Size = new System.Drawing.Size(230, 20);
            this.fuelRight.TabIndex = 43;
            this.fuelRight.Value = 0;
            // 
            // cargoMeterLeft
            // 
            this.cargoMeterLeft.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.cargoMeterLeft.CargoLevels = ((Nova.Common.Cargo)(resources.GetObject("cargoMeterLeft.CargoLevels")));
            this.cargoMeterLeft.Location = new System.Drawing.Point(12, 193);
            this.cargoMeterLeft.Name = "cargoMeterLeft";
            this.cargoMeterLeft.Size = new System.Drawing.Size(166, 14);
            this.cargoMeterLeft.TabIndex = 45;
            this.cargoMeterLeft.Text = "cargoMeter1";
            // 
            // cargoMeterRight
            // 
            this.cargoMeterRight.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.cargoMeterRight.CargoLevels = ((Nova.Common.Cargo)(resources.GetObject("cargoMeterRight.CargoLevels")));
            this.cargoMeterRight.Location = new System.Drawing.Point(352, 193);
            this.cargoMeterRight.Name = "cargoMeterRight";
            this.cargoMeterRight.Size = new System.Drawing.Size(166, 14);
            this.cargoMeterRight.TabIndex = 46;
            this.cargoMeterRight.Text = "cargoMeter2";
            // 
            // CargoTransferDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(531, 288);
            this.Controls.Add(this.cargoMeterRight);
            this.Controls.Add(this.cargoMeterLeft);
            this.Controls.Add(this.fuelLeft);
            this.Controls.Add(this.fuelRight);
            this.Controls.Add(this.cargoColonistsLeft);
            this.Controls.Add(this.cargoColonistsRight);
            this.Controls.Add(this.cargoGermaniumLeft);
            this.Controls.Add(this.cargoGermaniumRight);
            this.Controls.Add(this.cargoBoraniumLeft);
            this.Controls.Add(this.cargoBoraniumRight);
            this.Controls.Add(this.cargoIronLeft);
            this.Controls.Add(this.cargoIronRight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelFleet2);
            this.Controls.Add(this.labelFleet1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CargoTransferDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Cargo Transfer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFleet1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label labelFleet2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Controls.CargoMeterCounter cargoIronRight;
        private Controls.CargoMeterCounter cargoIronLeft;
        private Controls.CargoMeterCounter cargoBoraniumLeft;
        private Controls.CargoMeterCounter cargoBoraniumRight;
        private Controls.CargoMeterCounter cargoGermaniumLeft;
        private Controls.CargoMeterCounter cargoGermaniumRight;
        private Controls.CargoMeterCounter cargoColonistsLeft;
        private Controls.CargoMeterCounter cargoColonistsRight;
        private Controls.CargoMeterCounter fuelLeft;
        private Controls.CargoMeterCounter fuelRight;
        private Controls.CargoMeter cargoMeterLeft;
        private Controls.CargoMeter cargoMeterRight;
    }
}