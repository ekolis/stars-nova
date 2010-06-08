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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Nova.Common;
using Nova.Client;


namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Score summary report dialog class
    /// </summary>
    public partial class ScoreReport : Form
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ScoreReport()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the display. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            ArrayList allScores = ClientState.Data.InputTurn.AllScores;
            ScoreGridView.AutoSize = true;

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

                ScoreGridView.Rows.Add(row);
            }

            ScoreGridView.AutoResizeColumns();
        }

    }
}
