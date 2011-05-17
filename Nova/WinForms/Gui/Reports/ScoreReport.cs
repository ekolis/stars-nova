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
// This module proveds the Score Report
// ===========================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;


namespace Nova.WinForms.Gui
{
    /// <Summary>
    /// Score Summary report dialog class
    /// </Summary>
    public partial class ScoreReport : Form
    {
        /// <Summary>
        /// Initializes a new instance of the ScoreReport class.
        /// </Summary>
        public ScoreReport()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Populate the display. 
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            List<ScoreRecord> allScores = ClientState.Data.InputTurn.AllScores;
            this.scoreGridView.AutoSize = true;

            foreach (ScoreRecord score in allScores)
            {
                int i = 0;
                int numColumns = 10;
                string[] row = new string[numColumns];

                row[i++] = score.Race;
                row[i++] = score.Rank.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.Score.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.Planets.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.Starbases.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.UnarmedShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.EscortShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.CapitalShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.TechLevel.ToString(System.Globalization.CultureInfo.InvariantCulture);
                row[i++] = score.Resources.ToString(System.Globalization.CultureInfo.InvariantCulture);

                this.scoreGridView.Rows.Add(row);
            }

            this.scoreGridView.AutoResizeColumns();
        }

    }
}
