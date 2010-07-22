#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This module provides the Planet Summary control.
// ===========================================================================
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{

    /// <summary>
    /// The star system summary panel.
    /// </summary>
    public class PlanetSummary : System.Windows.Forms.UserControl
    {
        #region Designer Generated Variables
        private System.ComponentModel.Container components = null;
        private Label reportAge;
        private Label planetValue;
        private Label population;
        private Label label1;
        private Label label2;
        private Label label6;
        private Label label4;
        private Label label5;
        private Label label3;
        private Gauge ironiumGauge;
        private Gauge germaniumGauge;
        private Gauge boraniumGauge;
        private Gauge gravityGauge;
        private Gauge temperatureGauge;
        private Gauge radiationGauge;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label gravityLevel;
        private Label temperatureLevel;
        private Label radiationLevel;
        private Panel panel2;
        private Label label10;
        private Panel panel1;
        #endregion

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the PlanetSummary class.
        /// </summary>
        public PlanetSummary()
        {
            InitializeComponent();
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

        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportAge = new System.Windows.Forms.Label();
            this.planetValue = new System.Windows.Forms.Label();
            this.population = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gravityLevel = new System.Windows.Forms.Label();
            this.temperatureLevel = new System.Windows.Forms.Label();
            this.radiationLevel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ironiumGauge = new ControlLibrary.Gauge();
            this.boraniumGauge = new ControlLibrary.Gauge();
            this.germaniumGauge = new ControlLibrary.Gauge();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gravityGauge = new ControlLibrary.Gauge();
            this.temperatureGauge = new ControlLibrary.Gauge();
            this.radiationGauge = new ControlLibrary.Gauge();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportAge
            // 
            this.reportAge.Location = new System.Drawing.Point(77, 0);
            this.reportAge.Name = "reportAge";
            this.reportAge.Size = new System.Drawing.Size(120, 13);
            this.reportAge.TabIndex = 25;
            this.reportAge.Text = "Report is current";
            // 
            // PlanetValue
            // 
            this.planetValue.Location = new System.Drawing.Point(146, 15);
            this.planetValue.Name = "planetValue";
            this.planetValue.Size = new System.Drawing.Size(34, 13);
            this.planetValue.TabIndex = 27;
            this.planetValue.Text = "0";
            this.planetValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Population
            // 
            this.population.Location = new System.Drawing.Point(187, 15);
            this.population.Name = "population";
            this.population.Size = new System.Drawing.Size(120, 16);
            this.population.TabIndex = 26;
            this.population.Text = "Uninhabited";
            this.population.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Germanium";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Ironium";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Radiation";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Gravity";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Temperature";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Boranium";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "0kT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "2500kT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(285, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "5MT";
            // 
            // GravityLevel
            // 
            this.gravityLevel.AutoSize = true;
            this.gravityLevel.Location = new System.Drawing.Point(308, 31);
            this.gravityLevel.Name = "gravityLevel";
            this.gravityLevel.Size = new System.Drawing.Size(19, 13);
            this.gravityLevel.TabIndex = 40;
            this.gravityLevel.Text = "1g";
            // 
            // TemperatureLevel
            // 
            this.temperatureLevel.AutoSize = true;
            this.temperatureLevel.Location = new System.Drawing.Point(308, 47);
            this.temperatureLevel.Name = "temperatureLevel";
            this.temperatureLevel.Size = new System.Drawing.Size(31, 13);
            this.temperatureLevel.TabIndex = 41;
            this.temperatureLevel.Text = "1deg";
            // 
            // RadiationLevel
            // 
            this.radiationLevel.AutoSize = true;
            this.radiationLevel.Location = new System.Drawing.Point(308, 64);
            this.radiationLevel.Name = "radiationLevel";
            this.radiationLevel.Size = new System.Drawing.Size(29, 13);
            this.radiationLevel.TabIndex = 42;
            this.radiationLevel.Text = "1mR";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ironiumGauge);
            this.panel1.Controls.Add(this.boraniumGauge);
            this.panel1.Controls.Add(this.germaniumGauge);
            this.panel1.Location = new System.Drawing.Point(76, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 51);
            this.panel1.TabIndex = 43;
            // 
            // IroniumGauge
            // 
            this.ironiumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ironiumGauge.BarColour = System.Drawing.Color.Blue;
            this.ironiumGauge.BottomValue = 0;
            this.ironiumGauge.Location = new System.Drawing.Point(0, 2);
            this.ironiumGauge.Marker = 0;
            this.ironiumGauge.MarkerColour = System.Drawing.Color.Blue;
            this.ironiumGauge.Maximum = 5000;
            this.ironiumGauge.Minimum = 0;
            this.ironiumGauge.Name = "ironiumGauge";
            this.ironiumGauge.ShowText = false;
            this.ironiumGauge.Size = new System.Drawing.Size(230, 10);
            this.ironiumGauge.TabIndex = 28;
            this.ironiumGauge.TopValue = 500;
            this.ironiumGauge.Units = null;
            this.ironiumGauge.Value = 500;
            // 
            // BoraniumGauge
            // 
            this.boraniumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.boraniumGauge.BarColour = System.Drawing.Color.GreenYellow;
            this.boraniumGauge.BottomValue = 0;
            this.boraniumGauge.Location = new System.Drawing.Point(0, 19);
            this.boraniumGauge.Marker = 0;
            this.boraniumGauge.MarkerColour = System.Drawing.Color.GreenYellow;
            this.boraniumGauge.Maximum = 5000;
            this.boraniumGauge.Minimum = 0;
            this.boraniumGauge.Name = "boraniumGauge";
            this.boraniumGauge.ShowText = false;
            this.boraniumGauge.Size = new System.Drawing.Size(230, 10);
            this.boraniumGauge.TabIndex = 29;
            this.boraniumGauge.TopValue = 500;
            this.boraniumGauge.Units = null;
            this.boraniumGauge.Value = 500;
            // 
            // GermaniumGauge
            // 
            this.germaniumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.germaniumGauge.BarColour = System.Drawing.Color.Yellow;
            this.germaniumGauge.BottomValue = 0;
            this.germaniumGauge.Location = new System.Drawing.Point(0, 36);
            this.germaniumGauge.Marker = 0;
            this.germaniumGauge.MarkerColour = System.Drawing.Color.Gold;
            this.germaniumGauge.Maximum = 5000;
            this.germaniumGauge.Minimum = 0;
            this.germaniumGauge.Name = "germaniumGauge";
            this.germaniumGauge.ShowText = false;
            this.germaniumGauge.Size = new System.Drawing.Size(230, 10);
            this.germaniumGauge.TabIndex = 30;
            this.germaniumGauge.TopValue = 500;
            this.germaniumGauge.Units = null;
            this.germaniumGauge.Value = 500;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.gravityGauge);
            this.panel2.Controls.Add(this.temperatureGauge);
            this.panel2.Controls.Add(this.radiationGauge);
            this.panel2.Location = new System.Drawing.Point(77, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 51);
            this.panel2.TabIndex = 44;
            // 
            // GravityGauge
            // 
            this.gravityGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gravityGauge.BarColour = System.Drawing.Color.Aquamarine;
            this.gravityGauge.BottomValue = 2.5;
            this.gravityGauge.Location = new System.Drawing.Point(-2, 2);
            this.gravityGauge.Marker = 50;
            this.gravityGauge.MarkerColour = System.Drawing.Color.Green;
            this.gravityGauge.Maximum = 8;
            this.gravityGauge.Minimum = 0;
            this.gravityGauge.Name = "gravityGauge";
            this.gravityGauge.ShowText = false;
            this.gravityGauge.Size = new System.Drawing.Size(230, 10);
            this.gravityGauge.TabIndex = 31;
            this.gravityGauge.TopValue = 6;
            this.gravityGauge.Units = null;
            this.gravityGauge.Value = 6;
            // 
            // TemperatureGauge
            // 
            this.temperatureGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.temperatureGauge.BarColour = System.Drawing.Color.LightSteelBlue;
            this.temperatureGauge.BottomValue = -100;
            this.temperatureGauge.Location = new System.Drawing.Point(-2, 19);
            this.temperatureGauge.Marker = 50;
            this.temperatureGauge.MarkerColour = System.Drawing.Color.Blue;
            this.temperatureGauge.Maximum = 200;
            this.temperatureGauge.Minimum = -200;
            this.temperatureGauge.Name = "temperatureGauge";
            this.temperatureGauge.ShowText = false;
            this.temperatureGauge.Size = new System.Drawing.Size(230, 10);
            this.temperatureGauge.TabIndex = 32;
            this.temperatureGauge.TopValue = 100;
            this.temperatureGauge.Units = null;
            this.temperatureGauge.Value = 100;
            // 
            // RadiationGauge
            // 
            this.radiationGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.radiationGauge.BarColour = System.Drawing.Color.Plum;
            this.radiationGauge.BottomValue = 25;
            this.radiationGauge.Location = new System.Drawing.Point(-2, 36);
            this.radiationGauge.Marker = 50;
            this.radiationGauge.MarkerColour = System.Drawing.Color.Red;
            this.radiationGauge.Maximum = 100;
            this.radiationGauge.Minimum = 0;
            this.radiationGauge.Name = "radiationGauge";
            this.radiationGauge.ShowText = false;
            this.radiationGauge.Size = new System.Drawing.Size(230, 10);
            this.radiationGauge.TabIndex = 33;
            this.radiationGauge.TopValue = 75;
            this.radiationGauge.Units = null;
            this.radiationGauge.Value = 75;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(77, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Planet Value";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlanetSummary
            // 
            this.Controls.Add(this.planetValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radiationLevel);
            this.Controls.Add(this.temperatureLevel);
            this.Controls.Add(this.gravityLevel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.reportAge);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.population);
            this.Name = "PlanetSummary";
            this.Size = new System.Drawing.Size(342, 157);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Properties

        /// <summary>
        /// Select the star whose details are to be displayed
        /// </summary>
        public Star Value
        {
            set
            {
                Race race = ClientState.Data.PlayerRace;
                int habValue = (int)Math.Ceiling(value.HabitalValue(race) * 100);
                this.planetValue.Text = habValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + "%";

                if (habValue < 0)
                {
                    this.planetValue.ForeColor = Color.Red;
                }
                else
                {
                    this.planetValue.ForeColor = Color.Black;
                }

                StarReport report = ClientState.Data.StarReports[value.Name]
                                    as StarReport;

                if (report.Population == 0)
                {
                    this.population.Text = "Uninhabited";
                }
                else
                {
                    this.population.Text = "Population: " + report.Population;
                }

                if (report.Age == 0)
                {
                    this.reportAge.Text = "Report is current";
                }
                else if (report.Age == 1)
                {
                    this.reportAge.Text = "Report is 1 year old";
                }
                else
                {
                    this.reportAge.Text = "Report is " + report.Age + " years old";
                }

                this.ironiumGauge.Value = report.StarResources.Ironium;
                this.boraniumGauge.Value = report.StarResources.Boranium;
                this.germaniumGauge.Value = report.StarResources.Germanium;

                this.ironiumGauge.Marker = (int)report.Concentration.Ironium;
                this.boraniumGauge.Marker = (int)report.Concentration.Boranium;
                this.germaniumGauge.Marker = (int)report.Concentration.Germanium;

                this.radiationGauge.Marker = report.Radiation;
                this.gravityGauge.Marker = report.Gravity;
                this.temperatureGauge.Marker = report.Temperature;

                double r = report.Radiation;
                double g = report.Gravity / 10.0;
                double t = -200 + ((400 * report.Temperature) / 100.0);

                this.radiationLevel.Text = r.ToString(System.Globalization.CultureInfo.InvariantCulture) + "mR";
                this.gravityLevel.Text = g.ToString("F1") + "g";
                this.temperatureLevel.Text = t.ToString(System.Globalization.CultureInfo.InvariantCulture) + "C";

                this.radiationGauge.TopValue = race.RadiationTolerance.Maximum;
                this.radiationGauge.BottomValue = race.RadiationTolerance.Minimum;

                this.gravityGauge.TopValue = race.GravityTolerance.Maximum;
                this.gravityGauge.BottomValue = race.GravityTolerance.Minimum;

                this.temperatureGauge.TopValue = race.TemperatureTolerance.Maximum;
                this.temperatureGauge.BottomValue = race.TemperatureTolerance.Minimum;
            }
        }

        #endregion

    }
}

