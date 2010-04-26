// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Fleet summary report.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

// ============================================================================
// Score summary report dialog class
// ============================================================================

namespace Nova.Gui.Reports
{
    public partial class ScoreReport : Form
    {
        public ScoreReport()
        {
            InitializeComponent();
        }


// ============================================================================
// Populate the display. 
// ============================================================================

      private void OnLoad(object sender, EventArgs e)
      {
         ArrayList allScores    = ClientState.Data.InputTurn.AllScores;
         ScoreGridView.AutoSize = true;

         foreach (ScoreRecord score in allScores) {
               int i             = 0;
               int numColumns    = 10;
               string[] row      = new string[numColumns];

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
