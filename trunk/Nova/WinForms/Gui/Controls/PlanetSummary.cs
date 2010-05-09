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

using Nova.ControlLibrary;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Nova.Common;
using Nova.Client;

namespace Nova.WinForms.Gui
{

    /// <summary>
    /// The star system summary panel.
    /// </summary>
    public class PlanetSummary : System.Windows.Forms.UserControl
    {


        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Point[] marker = {new Point( 0, 0), new Point(5, 5), new Point(0, 10),
                        new Point(-5, 5), new Point(0, 0)};


        #region Designer Generated Variables
        private System.ComponentModel.Container components = null;
        private Label ReportAge;
        private Label PlanetValue;
        private Label Population;
        private Label label1;
        private Label label2;
        private Label label6;
        private Label label4;
        private Label label5;
        private Label label3;
        private Gauge IroniumGauge;
        private Gauge GermaniumGauge;
        private Gauge BoraniumGauge;
        private Gauge GravityGauge;
        private Gauge TemperatureGauge;
        private Gauge RadiationGauge;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label GravityLevel;
        private Label TemperatureLevel;
        private Label RadiationLevel;
        private Panel panel2;
        private Label label10;
        private Panel panel1;
        #endregion

        #region Construction and Disposal

        /// <summary>
        /// Construction.
        /// </summary>
        public PlanetSummary()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
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
            this.ReportAge = new System.Windows.Forms.Label();
            this.PlanetValue = new System.Windows.Forms.Label();
            this.Population = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GravityLevel = new System.Windows.Forms.Label();
            this.TemperatureLevel = new System.Windows.Forms.Label();
            this.RadiationLevel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IroniumGauge = new ControlLibrary.Gauge();
            this.BoraniumGauge = new ControlLibrary.Gauge();
            this.GermaniumGauge = new ControlLibrary.Gauge();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GravityGauge = new ControlLibrary.Gauge();
            this.TemperatureGauge = new ControlLibrary.Gauge();
            this.RadiationGauge = new ControlLibrary.Gauge();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportAge
            // 
            this.ReportAge.Location = new System.Drawing.Point(77, 0);
            this.ReportAge.Name = "ReportAge";
            this.ReportAge.Size = new System.Drawing.Size(120, 13);
            this.ReportAge.TabIndex = 25;
            this.ReportAge.Text = "Report is current";
            // 
            // PlanetValue
            // 
            this.PlanetValue.Location = new System.Drawing.Point(146, 15);
            this.PlanetValue.Name = "PlanetValue";
            this.PlanetValue.Size = new System.Drawing.Size(34, 13);
            this.PlanetValue.TabIndex = 27;
            this.PlanetValue.Text = "0";
            this.PlanetValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Population
            // 
            this.Population.Location = new System.Drawing.Point(187, 15);
            this.Population.Name = "Population";
            this.Population.Size = new System.Drawing.Size(120, 16);
            this.Population.TabIndex = 26;
            this.Population.Text = "Uninhabited";
            this.Population.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.GravityLevel.AutoSize = true;
            this.GravityLevel.Location = new System.Drawing.Point(308, 31);
            this.GravityLevel.Name = "GravityLevel";
            this.GravityLevel.Size = new System.Drawing.Size(19, 13);
            this.GravityLevel.TabIndex = 40;
            this.GravityLevel.Text = "1g";
            // 
            // TemperatureLevel
            // 
            this.TemperatureLevel.AutoSize = true;
            this.TemperatureLevel.Location = new System.Drawing.Point(308, 47);
            this.TemperatureLevel.Name = "TemperatureLevel";
            this.TemperatureLevel.Size = new System.Drawing.Size(31, 13);
            this.TemperatureLevel.TabIndex = 41;
            this.TemperatureLevel.Text = "1deg";
            // 
            // RadiationLevel
            // 
            this.RadiationLevel.AutoSize = true;
            this.RadiationLevel.Location = new System.Drawing.Point(308, 64);
            this.RadiationLevel.Name = "RadiationLevel";
            this.RadiationLevel.Size = new System.Drawing.Size(29, 13);
            this.RadiationLevel.TabIndex = 42;
            this.RadiationLevel.Text = "1mR";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.IroniumGauge);
            this.panel1.Controls.Add(this.BoraniumGauge);
            this.panel1.Controls.Add(this.GermaniumGauge);
            this.panel1.Location = new System.Drawing.Point(76, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 51);
            this.panel1.TabIndex = 43;
            // 
            // IroniumGauge
            // 
            this.IroniumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.IroniumGauge.BarColour = System.Drawing.Color.Blue;
            this.IroniumGauge.BottomValue = 0;
            this.IroniumGauge.Location = new System.Drawing.Point(0, 2);
            this.IroniumGauge.Marker = 0;
            this.IroniumGauge.MarkerColour = System.Drawing.Color.Blue;
            this.IroniumGauge.Maximum = 5000;
            this.IroniumGauge.Minimum = 0;
            this.IroniumGauge.Name = "IroniumGauge";
            this.IroniumGauge.ShowText = false;
            this.IroniumGauge.Size = new System.Drawing.Size(230, 10);
            this.IroniumGauge.TabIndex = 28;
            this.IroniumGauge.TopValue = 500;
            this.IroniumGauge.Units = null;
            this.IroniumGauge.Value = 500;
            // 
            // BoraniumGauge
            // 
            this.BoraniumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BoraniumGauge.BarColour = System.Drawing.Color.GreenYellow;
            this.BoraniumGauge.BottomValue = 0;
            this.BoraniumGauge.Location = new System.Drawing.Point(0, 19);
            this.BoraniumGauge.Marker = 0;
            this.BoraniumGauge.MarkerColour = System.Drawing.Color.GreenYellow;
            this.BoraniumGauge.Maximum = 5000;
            this.BoraniumGauge.Minimum = 0;
            this.BoraniumGauge.Name = "BoraniumGauge";
            this.BoraniumGauge.ShowText = false;
            this.BoraniumGauge.Size = new System.Drawing.Size(230, 10);
            this.BoraniumGauge.TabIndex = 29;
            this.BoraniumGauge.TopValue = 500;
            this.BoraniumGauge.Units = null;
            this.BoraniumGauge.Value = 500;
            // 
            // GermaniumGauge
            // 
            this.GermaniumGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GermaniumGauge.BarColour = System.Drawing.Color.Yellow;
            this.GermaniumGauge.BottomValue = 0;
            this.GermaniumGauge.Location = new System.Drawing.Point(0, 36);
            this.GermaniumGauge.Marker = 0;
            this.GermaniumGauge.MarkerColour = System.Drawing.Color.Gold;
            this.GermaniumGauge.Maximum = 5000;
            this.GermaniumGauge.Minimum = 0;
            this.GermaniumGauge.Name = "GermaniumGauge";
            this.GermaniumGauge.ShowText = false;
            this.GermaniumGauge.Size = new System.Drawing.Size(230, 10);
            this.GermaniumGauge.TabIndex = 30;
            this.GermaniumGauge.TopValue = 500;
            this.GermaniumGauge.Units = null;
            this.GermaniumGauge.Value = 500;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.GravityGauge);
            this.panel2.Controls.Add(this.TemperatureGauge);
            this.panel2.Controls.Add(this.RadiationGauge);
            this.panel2.Location = new System.Drawing.Point(77, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 51);
            this.panel2.TabIndex = 44;
            // 
            // GravityGauge
            // 
            this.GravityGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GravityGauge.BarColour = System.Drawing.Color.Aquamarine;
            this.GravityGauge.BottomValue = 2.5;
            this.GravityGauge.Location = new System.Drawing.Point(-2, 2);
            this.GravityGauge.Marker = 50;
            this.GravityGauge.MarkerColour = System.Drawing.Color.Green;
            this.GravityGauge.Maximum = 8;
            this.GravityGauge.Minimum = 0;
            this.GravityGauge.Name = "GravityGauge";
            this.GravityGauge.ShowText = false;
            this.GravityGauge.Size = new System.Drawing.Size(230, 10);
            this.GravityGauge.TabIndex = 31;
            this.GravityGauge.TopValue = 6;
            this.GravityGauge.Units = null;
            this.GravityGauge.Value = 6;
            // 
            // TemperatureGauge
            // 
            this.TemperatureGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.TemperatureGauge.BarColour = System.Drawing.Color.LightSteelBlue;
            this.TemperatureGauge.BottomValue = -100;
            this.TemperatureGauge.Location = new System.Drawing.Point(-2, 19);
            this.TemperatureGauge.Marker = 50;
            this.TemperatureGauge.MarkerColour = System.Drawing.Color.Blue;
            this.TemperatureGauge.Maximum = 200;
            this.TemperatureGauge.Minimum = -200;
            this.TemperatureGauge.Name = "TemperatureGauge";
            this.TemperatureGauge.ShowText = false;
            this.TemperatureGauge.Size = new System.Drawing.Size(230, 10);
            this.TemperatureGauge.TabIndex = 32;
            this.TemperatureGauge.TopValue = 100;
            this.TemperatureGauge.Units = null;
            this.TemperatureGauge.Value = 100;
            // 
            // RadiationGauge
            // 
            this.RadiationGauge.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.RadiationGauge.BarColour = System.Drawing.Color.Plum;
            this.RadiationGauge.BottomValue = 25;
            this.RadiationGauge.Location = new System.Drawing.Point(-2, 36);
            this.RadiationGauge.Marker = 50;
            this.RadiationGauge.MarkerColour = System.Drawing.Color.Red;
            this.RadiationGauge.Maximum = 100;
            this.RadiationGauge.Minimum = 0;
            this.RadiationGauge.Name = "RadiationGauge";
            this.RadiationGauge.ShowText = false;
            this.RadiationGauge.Size = new System.Drawing.Size(230, 10);
            this.RadiationGauge.TabIndex = 33;
            this.RadiationGauge.TopValue = 75;
            this.RadiationGauge.Units = null;
            this.RadiationGauge.Value = 75;
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
            this.Controls.Add(this.PlanetValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RadiationLevel);
            this.Controls.Add(this.TemperatureLevel);
            this.Controls.Add(this.GravityLevel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ReportAge);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Population);
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
                PlanetValue.Text = habValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + "%";

                if (habValue < 0)
                {
                    PlanetValue.ForeColor = Color.Red;
                }
                else
                {
                    PlanetValue.ForeColor = Color.Black;
                }

                StarReport report = ClientState.Data.StarReports[value.Name]
                                    as StarReport;

                if (report.Population == 0)
                {
                    Population.Text = "Uninhabited";
                }
                else
                {
                    Population.Text = "Population: " + report.Population;
                }

                if (report.Age == 0)
                {
                    ReportAge.Text = "Report is current";
                }
                else if (report.Age == 1)
                {
                    ReportAge.Text = "Report is 1 year old";
                }
                else
                {
                    ReportAge.Text = "Report is " + report.Age + " years old";
                }

                IroniumGauge.Value = report.StarResources.Ironium;
                BoraniumGauge.Value = report.StarResources.Boranium;
                GermaniumGauge.Value = report.StarResources.Germanium;

                IroniumGauge.Marker = (int)report.Concentration.Ironium;
                BoraniumGauge.Marker = (int)report.Concentration.Boranium;
                GermaniumGauge.Marker = (int)report.Concentration.Germanium;

                RadiationGauge.Marker = report.Radiation;
                GravityGauge.Marker = report.Gravity;
                TemperatureGauge.Marker = report.Temperature;

                double r = report.Radiation;
                double g = report.Gravity / 10.0;
                double t = -200 + ((400 * report.Temperature) / 100.0);

                RadiationLevel.Text = r.ToString(System.Globalization.CultureInfo.InvariantCulture) + "mR";
                GravityLevel.Text = g.ToString("F1") + "g";
                TemperatureLevel.Text = t.ToString(System.Globalization.CultureInfo.InvariantCulture) + "C";

                RadiationGauge.TopValue = race.RadiationTolerance.Maximum;
                RadiationGauge.BottomValue = race.RadiationTolerance.Minimum;

                GravityGauge.TopValue = race.GravityTolerance.Maximum;
                GravityGauge.BottomValue = race.GravityTolerance.Minimum;

                TemperatureGauge.TopValue = race.TemperatureTolerance.Maximum;
                TemperatureGauge.BottomValue = race.TemperatureTolerance.Minimum;
            }
        }

        #endregion

    }
}

