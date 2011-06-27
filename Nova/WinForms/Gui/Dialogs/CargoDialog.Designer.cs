using System;

using Nova.Common;

namespace Nova.ControlLibrary
{
    /// <summary>
    /// A dialog for transferring cargo between a planet and a ship.
    /// </summary>
    public partial class CargoDialog : System.Windows.Forms.Form
    {
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.meterCargo = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.cargoIron = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.labelIron = new System.Windows.Forms.Label();
            this.labelBoron = new System.Windows.Forms.Label();
            this.cargoBoron = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.labelGerman = new System.Windows.Forms.Label();
            this.cargoGerman = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.labelColonists = new System.Windows.Forms.Label();
            this.cargoColonists = new Nova.WinForms.Gui.Controls.CargoMeterCounter();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(352, 270);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(271, 270);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "Resources On Hand";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(320, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 32);
            this.label2.TabIndex = 16;
            this.label2.Text = "Cargo Bay Content";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(41, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Cargo Bay";
            // 
            // meterCargo
            // 
            this.meterCargo.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.meterCargo.Location = new System.Drawing.Point(113, 235);
            this.meterCargo.Name = "meterCargo";
            this.meterCargo.Size = new System.Drawing.Size(213, 15);
            this.meterCargo.TabIndex = 19;
            this.meterCargo.Text = "cargoMeter1";
            // 
            // cargoIron
            // 
            this.cargoIron.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Ironium;
            this.cargoIron.Location = new System.Drawing.Point(110, 60);
            this.cargoIron.Maximum = 100;
            this.cargoIron.Name = "cargoIron";
            this.cargoIron.Reversed = true;
            this.cargoIron.Size = new System.Drawing.Size(269, 20);
            this.cargoIron.TabIndex = 20;
            this.cargoIron.Value = 0;
            // 
            // labelIron
            // 
            this.labelIron.Location = new System.Drawing.Point(4, 58);
            this.labelIron.Name = "labelIron";
            this.labelIron.Size = new System.Drawing.Size(100, 20);
            this.labelIron.TabIndex = 21;
            this.labelIron.Text = "999999KT";
            this.labelIron.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelBoron
            // 
            this.labelBoron.Location = new System.Drawing.Point(4, 102);
            this.labelBoron.Name = "labelBoron";
            this.labelBoron.Size = new System.Drawing.Size(100, 20);
            this.labelBoron.TabIndex = 23;
            this.labelBoron.Text = "999999KT";
            this.labelBoron.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cargoBoron
            // 
            this.cargoBoron.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Boranium;
            this.cargoBoron.Location = new System.Drawing.Point(110, 104);
            this.cargoBoron.Maximum = 100;
            this.cargoBoron.Name = "cargoBoron";
            this.cargoBoron.Reversed = true;
            this.cargoBoron.Size = new System.Drawing.Size(269, 20);
            this.cargoBoron.TabIndex = 22;
            this.cargoBoron.Value = 0;
            // 
            // labelGerman
            // 
            this.labelGerman.Location = new System.Drawing.Point(4, 150);
            this.labelGerman.Name = "labelGerman";
            this.labelGerman.Size = new System.Drawing.Size(100, 20);
            this.labelGerman.TabIndex = 25;
            this.labelGerman.Text = "999999KT";
            this.labelGerman.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cargoGerman
            // 
            this.cargoGerman.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Germanium;
            this.cargoGerman.Location = new System.Drawing.Point(110, 152);
            this.cargoGerman.Maximum = 100;
            this.cargoGerman.Name = "cargoGerman";
            this.cargoGerman.Reversed = true;
            this.cargoGerman.Size = new System.Drawing.Size(269, 20);
            this.cargoGerman.TabIndex = 24;
            this.cargoGerman.Value = 0;
            // 
            // labelColonists
            // 
            this.labelColonists.Location = new System.Drawing.Point(4, 194);
            this.labelColonists.Name = "labelColonists";
            this.labelColonists.Size = new System.Drawing.Size(100, 20);
            this.labelColonists.TabIndex = 27;
            this.labelColonists.Text = "999999KT";
            this.labelColonists.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cargoColonists
            // 
            this.cargoColonists.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Colonists;
            this.cargoColonists.Location = new System.Drawing.Point(110, 196);
            this.cargoColonists.Maximum = 100;
            this.cargoColonists.Name = "cargoColonists";
            this.cargoColonists.Reversed = true;
            this.cargoColonists.Size = new System.Drawing.Size(269, 20);
            this.cargoColonists.TabIndex = 26;
            this.cargoColonists.Value = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(122, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Ironium";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Boronium";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(122, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Germanium";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(122, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Colonists";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CargoDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(434, 302);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelColonists);
            this.Controls.Add(this.cargoColonists);
            this.Controls.Add(this.labelGerman);
            this.Controls.Add(this.cargoGerman);
            this.Controls.Add(this.labelBoron);
            this.Controls.Add(this.cargoBoron);
            this.Controls.Add(this.labelIron);
            this.Controls.Add(this.cargoIron);
            this.Controls.Add(this.meterCargo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CargoDialog";
            this.ShowInTaskbar = false;
            this.Text = "Cargo Transfer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.Container components = null;
        private WinForms.Gui.Controls.CargoMeter meterCargo;
        private WinForms.Gui.Controls.CargoMeterCounter cargoIron;
        private System.Windows.Forms.Label labelIron;
        private System.Windows.Forms.Label labelBoron;
        private WinForms.Gui.Controls.CargoMeterCounter cargoBoron;
        private System.Windows.Forms.Label labelGerman;
        private WinForms.Gui.Controls.CargoMeterCounter cargoGerman;
        private System.Windows.Forms.Label labelColonists;
        private WinForms.Gui.Controls.CargoMeterCounter cargoColonists;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}