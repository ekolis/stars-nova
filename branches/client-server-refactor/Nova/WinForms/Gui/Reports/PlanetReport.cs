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
        private Dictionary<string, StarIntel> allStarsIntel;
        private Race race;
        
        /// <Summary>
        /// Initializes a new instance of the PlanetReport class.
        /// </Summary>
        public PlanetReport(Dictionary<string, StarIntel> allStars, Race race)
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
                if (report.Star.Owner == race.Name)
                {
                    string[] row = new string[NumColumns];

                    string starbase = "-";
                    if (report.Star.Starbase != null)
                    {
                        starbase = report.Star.Starbase.Name;
                    }

                    int i = 0;
                    row[i++] = report.Star.Name;
                    row[i++] = starbase;
                    row[i++] = report.Star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Star.Capacity(race).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = Math.Ceiling(report.Star.HabitalValue(race) * 100).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = report.Star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Defenses.ComputeDefenseCoverage(report.Star);
                    row[i++] = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Nova.Common.Resources resources = report.Star.ResourcesOnHand;
                    StringBuilder text = new StringBuilder();

                    text.AppendFormat(
                        "{0} {1} {2}",
                        resources.Ironium,
                        resources.Boranium,
                        resources.Germanium);

                    string energy = ((int)resources.Energy).ToString(System.Globalization.CultureInfo.InvariantCulture);

                    row[i++] = text.ToString();

                    resources = report.Star.MineralConcentration;
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

