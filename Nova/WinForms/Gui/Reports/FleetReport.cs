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
    /// Fleet Summary report dialog class
    /// </Summary>
    public partial class FleetReport : Form
    {
        private EmpireData empireState;
        
        /// <Summary>
        /// Initializes a new instance of the FleetReport class.
        /// </Summary>
        public FleetReport(EmpireData empireState)
        {
            this.empireState = empireState;
            
            InitializeComponent();
        }

        /// ----------------------------------------------------------------------------
        /// <Summary>
        /// Populate the display. We use unbound population because some of the fields
        /// need a little logic to decode (we don't just have a bunch of strings).
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            const int NumColumns = 11;

            this.fleetGridView.Columns[6].Name = "Cargo";
            this.fleetGridView.AutoSize = true;

            foreach (Fleet fleet in empireState.OwnedFleets.Values)
            {
                if (fleet.Owner == empireState.Id)
                {
                    if (fleet.Type == ItemType.Starbase)
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
                        Waypoint waypoint = fleet.Waypoints[1];

                        destination = waypoint.Destination;
                        if (waypoint.Task != WaypointTask.None)
                        {
                            task = waypoint.GetTask();
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
                    row[i++] = fleet.Tokens.Count.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = "-";
                    row[i++] = fleet.BattlePlan;
                    row[i++] = fleet.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    this.fleetGridView.Rows.Add(row);
                }
            }

            this.fleetGridView.AutoResizeColumns();
        }

    }
}
