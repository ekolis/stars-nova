// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Planet summary report.
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

namespace Nova
{

// ============================================================================
// Planet summary report dialog class
// ============================================================================

   public partial class PlanetReport : Form
   {
      public PlanetReport()
      {
         InitializeComponent();
      }


// ============================================================================
// Populate the display. We use unbound population because some of the fields
// need a little logic to decode (we don't just have a bunch of strings).
// ============================================================================

      private void OnLoad(object sender, EventArgs e)
      {
         const int numColumns = 12;
         Race      race       = GuiState.Data.PlayerRace;

         PlanetGridView.Columns[8].Name = "Minerals";
         PlanetGridView.AutoSize        = true;

         Hashtable allStars = GuiState.Data.InputTurn.AllStars;
         
         foreach (Star star in allStars.Values) {
            if (star.Owner == race.Name) {
               string[] row = new string[numColumns];
               
               string starbase = "-";
               if (star.Starbase != null) {
                  starbase = star.Starbase.Name;
               }
               
               int  i = 0;
               row[i++] = star.Name;
               row[i++] = starbase;
               row[i++] = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
               row[i++] = star.Capacity(race).ToString(System.Globalization.CultureInfo.InvariantCulture);
               row[i++] = Math.Ceiling(star.HabitalValue(race)*100).ToString(System.Globalization.CultureInfo.InvariantCulture);
               row[i++] = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture);
               row[i++] = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture);

               Defenses.ComputeDefenseCoverage(star);
               row[i++] = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

               NovaCommon.Resources resources = star.ResourcesOnHand;
               StringBuilder text             = new StringBuilder();

               text.AppendFormat("{0} {1} {2}", (int) resources.Ironium,
                                                (int) resources.Boranium,
                                                (int) resources.Germanium);
               
               string energy = ((int) resources.Energy).ToString(System.Globalization.CultureInfo.InvariantCulture);

               row[i++] = text.ToString();

               resources = star.MineralConcentration;
               text      = new StringBuilder();

               text.AppendFormat("{0} {1} {2}", (int) resources.Ironium,
                                                (int) resources.Boranium,
                                                (int) resources.Germanium);
               row[i++] = text.ToString();
               row[i++] = energy;

               PlanetGridView.Rows.Add(row);
            }
         }

         PlanetGridView.AutoResizeColumns();
      }


// ============================================================================
// Override cell painting so we can colour the mineral types. Still to be
// implemented properly.
// ============================================================================

       private void PlanetGridView_CellPainting(object sender, 
                    DataGridViewCellPaintingEventArgs e)
       {
          if (e.RowIndex < 0 || e.ColumnIndex < 0) {
             return;
          }

          int mineralsIndex = PlanetGridView.Columns["Minerals"].Index;

          if (e.ColumnIndex != mineralsIndex) {
             return;
          }

           Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                     e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                     e.CellBounds.Height - 4);

           Brush gridBrush      = new SolidBrush(PlanetGridView.GridColor);
           Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor);

           Pen gridLinePen = new Pen(gridBrush);
    
           e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

          // Draw the grid lines (only the right and bottom lines;
          // DataGridView takes care of the others).

           e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                               e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                               e.CellBounds.Bottom - 1);
          
           e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                               e.CellBounds.Top, e.CellBounds.Right - 1,
                               e.CellBounds.Bottom);

          // Draw the text content of the cell.
          
          if (e.Value != null) {
             e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
             Brushes.Crimson, e.CellBounds.X + 2,
             e.CellBounds.Y + 2, StringFormat.GenericDefault);
          }

          e.Handled = true;
       }

   }
}

