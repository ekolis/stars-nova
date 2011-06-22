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
    using System.Windows.Forms;
    
    using Nova.Client;
    using Nova.Common;
    using Nova.Common.DataStructures;
        
    /// <Summary>
    /// Battle report dialog class
    /// </Summary>
    public partial class BattleReportDialog : Form
    {
        private List<BattleReport> battles;
        private string raceName;
        
        /// <Summary>
        /// Initializes a new instance of the BattleReportDialog class.
        /// </Summary>
        public BattleReportDialog(List<BattleReport> battles, string raceName)
        {
            this.battles = battles;
            this.raceName = raceName;
            
            InitializeComponent();
        }

        /// <Summary>
        /// Populate the display. 
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            const int NumColumns = 6;

            this.battleGridView.AutoSize = true;

            foreach (BattleReport report in battles)
            {

                Dictionary<string, bool> countSides = new Dictionary<string, bool>();
                foreach (Fleet fleet in report.Stacks.Values)
                {
                    countSides[fleet.Owner] = true;
                }

                int ourShips = 0;
                int theirShips = 0;

                foreach (Fleet fleet in report.Stacks.Values)
                {
                    if (fleet.Owner == raceName)
                    {
                        ourShips += fleet.FleetShips.Count;
                    }
                    else
                    {
                        theirShips += fleet.FleetShips.Count;
                    }
                }

                int ourLosses = 0;
                int theirLosses = 0;

                foreach (string race in report.Losses.Keys)
                {
                    if (race == raceName)
                    {
                        ourLosses += (int)report.Losses[race];
                    }
                    else
                    {
                        theirLosses += (int)report.Losses[race];
                    }
                }

                int i = 0;
                string[] row = new string[NumColumns];

                row[i++] = report.Location;
                row[i++] = countSides.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = ourShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = theirShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = ourLosses.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = theirLosses.ToString(System.Globalization.CultureInfo.InvariantCulture);

                this.battleGridView.Rows.Add(row);
            }

            this.battleGridView.AutoResizeColumns();
        }

        /// <Summary>
        /// On double click open the battle viewer
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void BattleGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int battleRow = e.RowIndex;

            if (battles.Count == 0)
            {
                Report.Information("There are no battles to view.");
            }
            else if (battles.Count <= battleRow)
            {
                Report.Information("Battle " + battleRow + " does not exist.");
            }
            else
            {
                BattleReport report = battles[battleRow];
                BattleViewer battleViewer = new BattleViewer(report);
                battleViewer.ShowDialog();
                battleViewer.Dispose();
            }
        }
    }
}
