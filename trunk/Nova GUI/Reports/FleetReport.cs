// This file needs -*- c++ -*- mode
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NovaCommon;


// ============================================================================
// Fleet summary report dialog class
// ============================================================================

namespace Nova
{
   public partial class FleetReport : Form
   {
      public FleetReport()
      {
         InitializeComponent();
      }


// ============================================================================
// Populate the display. We use unbound population because some of the fields
// need a little logic to decode (we don't just have a bunch of strings).
// ============================================================================

      private void OnLoad(object sender, EventArgs e)
      {
         const int numColumns = 11;
         Race      race       = GUIstate.Data.RaceData;

         Hashtable allFleets = GUIstate.Data.InputTurn.AllFleets;
         FleetGridView.Columns[6].Name = "Cargo";
         FleetGridView.AutoSize        = true;
         
         foreach (Fleet fleet in allFleets.Values) {
            if (fleet.Owner == race.Name) {

               if (fleet.Type == "Starbase") {
                  continue;
               }

               string location;
               if (fleet.InOrbit != null) {
                  location = fleet.InOrbit.Name;
               }
               else {
                  location = "Space at " + fleet.Position.ToString();
               }

               string destination = "-";
               string eta         = "-";
               string task        = "-";

               if (fleet.Waypoints.Count > 1){
                  Waypoint waypoint = fleet.Waypoints[1] as Waypoint;

                  destination = waypoint.Destination;
                  if (waypoint.Task != "None") {
                     task        = waypoint.Task;
                  }

                  double distance = PointUtilities.Distance(
                                    waypoint.Position, fleet.Position);
            
                  double speed = waypoint.WarpFactor * waypoint.WarpFactor;
                  double time  = distance / speed;
                  
                  eta = time.ToString("F1");
               }

               Cargo         cargo      = fleet.Cargo;
               StringBuilder cargoText  = new StringBuilder();

               cargoText.AppendFormat("{0} {1} {2} {3}", cargo.Ironium,
                                                         cargo.Boranium,
                                                         cargo.Germanium,
                                                         cargo.Colonists);
               int      i   = 0;
               string[] row = new string[numColumns];
               
               row[i++] = fleet.Name;
               row[i++] = location;
               row[i++] = destination;
               row[i++] = eta;
               row[i++] = task;
               row[i++] = fleet.FuelAvailable.ToString("f1");
               row[i++] = cargoText.ToString();
               row[i++] = fleet.FleetShips.Count.ToString();
               row[i++] = "-";
               row[i++] = fleet.BattlePlan;
               row[i++] = fleet.TotalMass.ToString();

               FleetGridView.Rows.Add(row);
            }
         }

         FleetGridView.AutoResizeColumns();
      }



// ============================================================================
// Override cell painting so we can colour the cargo types. Still to be
// implemented properly.
// ============================================================================

       private void FleetGridView_CellPainting(object sender, 
                    DataGridViewCellPaintingEventArgs e)
       {
          if (e.RowIndex < 0 || e.ColumnIndex < 0) {
             return;
          }

          int cargoIndex = FleetGridView.Columns["Cargo"].Index;

          if (e.ColumnIndex != cargoIndex) {
             return;
          }

           Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                     e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                     e.CellBounds.Height - 4);

           Brush gridBrush      = new SolidBrush(FleetGridView.GridColor);
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
