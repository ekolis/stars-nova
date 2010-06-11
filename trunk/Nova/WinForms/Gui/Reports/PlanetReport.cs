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
// Planet summary report.
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

using Nova.Common;
using Nova.Client;

namespace Nova.WinForms.Gui
{

    /// <summary>
    /// Planet summary report dialog class
    /// </summary>
    public partial class PlanetReport : Form
    {

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public PlanetReport()
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            const int NumColumns = 12;
            Race race = ClientState.Data.PlayerRace;

            PlanetGridView.Columns[8].Name = "Minerals";
            PlanetGridView.AutoSize = true;

            Hashtable allStars = ClientState.Data.InputTurn.AllStars;

            foreach (Star star in allStars.Values)
            {
                if (star.Owner == race.Name)
                {
                    string[] row = new string[NumColumns];

                    string starbase = "-";
                    if (star.Starbase != null)
                    {
                        starbase = star.Starbase.Name;
                    }

                    int i = 0;
                    row[i++] = star.Name;
                    row[i++] = starbase;
                    row[i++] = star.Colonists.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = star.Capacity(race).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = Math.Ceiling(star.HabitalValue(race) * 100).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = star.Mines.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    row[i++] = star.Factories.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Defenses.ComputeDefenseCoverage(star);
                    row[i++] = Defenses.SummaryCoverage.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    Nova.Common.Resources resources = star.ResourcesOnHand;
                    StringBuilder text = new StringBuilder();

                    text.AppendFormat("{0} {1} {2}", (int)resources.Ironium,
                                                     (int)resources.Boranium,
                                                     (int)resources.Germanium);

                    string energy = ((int)resources.Energy).ToString(System.Globalization.CultureInfo.InvariantCulture);

                    row[i++] = text.ToString();

                    resources = star.MineralConcentration;
                    text = new StringBuilder();

                    text.AppendFormat("{0} {1} {2}", (int)resources.Ironium,
                                                     (int)resources.Boranium,
                                                     (int)resources.Germanium);
                    row[i++] = text.ToString();
                    row[i++] = energy;

                    PlanetGridView.Rows.Add(row);
                }
            }

            PlanetGridView.AutoResizeColumns();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Override cell painting so we can colour the mineral types. Still to be
        /// implemented properly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ----------------------------------------------------------------------------
        private void PlanetGridView_CellPainting(object sender,
                     DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int mineralsIndex = PlanetGridView.Columns["Minerals"].Index;

            if (e.ColumnIndex != mineralsIndex)
            {
                return;
            }

            Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                      e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                      e.CellBounds.Height - 4);

            Brush gridBrush = new SolidBrush(PlanetGridView.GridColor);
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
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font,
                Brushes.Crimson, e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            }

            e.Handled = true;
        }

        #endregion

    }
}

