// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Battle summary report.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

// ============================================================================
// Battle report dialog class
// ============================================================================

namespace Nova
{
   public partial class BattleReportDialog : Form
   {
      public BattleReportDialog()
      {
         InitializeComponent();
      }


// ============================================================================
// Populate the display. 
// ============================================================================

      private void OnLoad(object sender, EventArgs e)
      {
         const int numColumns = 6;

         BattleGridView.AutoSize = true;
         
         foreach (BattleReport report in ClientState.Data.InputTurn.Battles) {

            Hashtable countSides = new Hashtable();
            foreach (Fleet fleet in report.Stacks.Values) {
               countSides[fleet.Owner] = true;
            }

            int ourShips   = 0;
            int theirShips = 0;

            foreach (Fleet fleet in report.Stacks.Values) {
               if (fleet.Owner == ClientState.Data.RaceName) {
                  ourShips += fleet.FleetShips.Count;
               }
               else {
                  theirShips += fleet.FleetShips.Count;
               }
            }

            int ourLosses   = 0;
            int theirLosses = 0;

            foreach (string race in report.Losses.Keys) {
               if (race == ClientState.Data.RaceName) {
                  ourLosses += (int) report.Losses[race];
               }
               else {
                  theirLosses += (int) report.Losses[race];
               }
            }

            int      i   = 0;
            string[] row = new string[numColumns];
               
            row[i++] = report.Location;
            row[i++] = countSides.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
            row[i++] = ourShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
            row[i++] = theirShips.ToString(System.Globalization.CultureInfo.InvariantCulture);
            row[i++] = ourLosses.ToString(System.Globalization.CultureInfo.InvariantCulture);
            row[i++] = theirLosses.ToString(System.Globalization.CultureInfo.InvariantCulture);

            BattleGridView.Rows.Add(row);
         }

         BattleGridView.AutoResizeColumns();
      }

       // On double click open the battle viewer
      private void BattleGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
          int battleRow = e.RowIndex;

          if (ClientState.Data.InputTurn.Battles.Count == 0)
          {
              Report.Information("There are no battles to view.");
          }
          else if (ClientState.Data.InputTurn.Battles.Count <= battleRow)
          {
              Report.Information("Battle "+battleRow+" does not exist.");
          }
          else
          {
              BattleReport report = ClientState.Data.InputTurn.Battles[battleRow] as BattleReport;
              BattleViewer battleViewer = new BattleViewer(report);
              battleViewer.ShowDialog();
              battleViewer.Dispose();
          }
      }

   }
}
