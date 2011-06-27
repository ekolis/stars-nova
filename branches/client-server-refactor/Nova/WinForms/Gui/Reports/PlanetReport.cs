#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;

    /// <Summary>
    /// Planet Summary report dialog class
    /// </Summary>
    public partial class PlanetReport : Form
    {
        private StarIntelList allStarsIntel;
        private Race race;
        
        /// <Summary>
        /// Initializes a new instance of the PlanetReport class.
        /// </Summary>
        public PlanetReport(StarIntelList allStars, Race race)
        {
            this.allStarsIntel = allStars;
            this.race = race;
            
            InitializeComponent();
        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Populate the display. We use unbound population because some of the fields
        /// need a little logic to decode (we don't just have a bunch of strings).
        /// </Summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            const int NumColumns = 12;

            this.planetGridView.Columns[8].Name = "Minerals";
            this.planetGridView.AutoSize = true;

            foreach (StarIntel report in allStarsIntel.Values)
            {
                if (report.Owner == race.Name)
                {
                    string[] row = new string[NumColumns];

                    string starbase = "-";
                    if (report.Starbase != null)
                    {
                        starbase = report.Starbase.Name;
                    }

                    int i = 0;
                    row[i++] = report.Name;
                    row[i++] = starbase;
                    row[i++] = report.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Capacity(race).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = Math.Ceiling(report.HabitalValue(race) * 100).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Defenses.ComputeDefenseCoverage(report);
                    row[i++] = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Nova.Common.Resources resources = report.ResourcesOnHand;
                    StringBuilder text = new StringBuilder();

                    text.AppendFormat(
                        "{0} {1} {2}",
                        resources.Ironium,
                        resources.Boranium,
                        resources.Germanium);

                    string energy = ((int)resources.Energy).ToString(System.Globalization.CultureInfo.InvariantCulture);

                    row[i++] = text.ToString();

                    resources = report.MineralConcentration;
                    text = new StringBuilder();

                    text.AppendFormat(
                        "{0} {1} {2}",
                        resources.Ironium,
                        resources.Boranium,
                        resources.Germanium);
                    row[i++] = text.ToString();
                    row[i++] = energy;

                    this.planetGridView.Rows.Add(row);
                }
            }

            this.planetGridView.AutoResizeColumns();
        }
    }
}

