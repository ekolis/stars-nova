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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Fleet summary report dialog class
    /// </summary>
    public partial class FleetReport : Form
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the FleetReport class.
        /// </summary>
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
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            const int NumColumns = 11;
            Race race = ClientState.Data.PlayerRace;

            Hashtable allFleets = ClientState.Data.InputTurn.AllFleets;
            this.fleetGridView.Columns[6].Name = "Cargo";
            this.fleetGridView.AutoSize = true;

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

                    cargoText.AppendFormat(
                        "{0} {1} {2} {3}",
                        cargo.Ironium,
                        cargo.Boranium,
                        cargo.Germanium,
                        cargo.ColonistsInKilotons);

                    int i = 0;
                    string[] row = new string[NumColumns];

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
                    row[i++] = fleet.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    this.fleetGridView.Rows.Add(row);
                }
            }

            this.fleetGridView.AutoResizeColumns();
        }

        #endregion
    }
}
