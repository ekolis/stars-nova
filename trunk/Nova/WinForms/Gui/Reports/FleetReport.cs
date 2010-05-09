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
// Fleet summary report.
// ===========================================================================
#endregion

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


namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Fleet summary report dialog class
    /// </summary>
    public partial class FleetReport : Form
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public FleetReport()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the display. We use unbound population because some of the fields
        /// need a little logic to decode (we don't just have a bunch of strings).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            const int numColumns = 11;
            Race race = ClientState.Data.PlayerRace;

            Hashtable allFleets = ClientState.Data.InputTurn.AllFleets;
            FleetGridView.Columns[6].Name = "Cargo";
            FleetGridView.AutoSize = true;

            foreach (Fleet fleet in allFleets.Values)
            {
                if (fleet.Owner == race.Name)
                {

                    if (fleet.Type == "Starbase")
                    {
                        continue;
                    }

                    string location;
                    if (fleet.InOrbit != null)
                    {
                        location = fleet.InOrbit.Name;
                    }
                    else
                    {
                        location = "Space at " + fleet.Position.ToString();
                    }

                    string destination = "-";
                    string eta = "-";
                    string task = "-";

                    if (fleet.Waypoints.Count > 1)
                    {
                        Waypoint waypoint = fleet.Waypoints[1] as Waypoint;

                        destination = waypoint.Destination;
                        if (waypoint.Task != "None")
                        {
                            task = waypoint.Task;
                        }

                        double distance = PointUtilities.Distance(
                                          waypoint.Position, fleet.Position);

                        double speed = waypoint.WarpFactor * waypoint.WarpFactor;
                        double time = distance / speed;

                        eta = time.ToString("F1");
                    }

                    Cargo cargo = fleet.Cargo;
                    StringBuilder cargoText = new StringBuilder();

                    cargoText.AppendFormat("{0} {1} {2} {3}", cargo.Ironium,
                                                              cargo.Boranium,
                                                              cargo.Germanium,
                                                              cargo.Colonists);
                    int i = 0;
                    string[] row = new string[numColumns];

                    row[i++] = fleet.Name;
                    row[i++] = location;
                    row[i++] = destination;
                    row[i++] = eta;
                    row[i++] = task;
                    row[i++] = fleet.FuelAvailable.ToString("f1");
                    row[i++] = cargoText.ToString();
                    row[i++] = fleet.FleetShips.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = "-";
                    row[i++] = fleet.BattlePlan;
                    row[i++] = fleet.TotalMass.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    FleetGridView.Rows.Add(row);
                }
            }

            FleetGridView.AutoResizeColumns();
        }



        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Override cell painting so we can colour the cargo types. Still to be
        /// implemented properly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void FleetGridView_CellPainting(object sender,
                      DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int cargoIndex = FleetGridView.Columns["Cargo"].Index;

            if (e.ColumnIndex != cargoIndex)
            {
                return;
            }

            Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                      e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                      e.CellBounds.Height - 4);

            Brush gridBrush = new SolidBrush(FleetGridView.GridColor);
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

            if (e.Value != null)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                Brushes.Crimson, e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            }

            e.Handled = true;
        }

        #endregion
    }
}
